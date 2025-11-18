using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x0200081B RID: 2075
	[SecurityCritical]
	[AttributeUsage(AttributeTargets.Class)]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	[Serializable]
	public class SynchronizationAttribute : ContextAttribute, IContributeServerContextSink, IContributeClientContextSink
	{
		// Token: 0x17000EBD RID: 3773
		// (get) Token: 0x060058E9 RID: 22761 RVA: 0x00139100 File Offset: 0x00137300
		// (set) Token: 0x060058EA RID: 22762 RVA: 0x00139108 File Offset: 0x00137308
		public virtual bool Locked
		{
			get
			{
				return this._locked;
			}
			set
			{
				this._locked = value;
			}
		}

		// Token: 0x17000EBE RID: 3774
		// (get) Token: 0x060058EB RID: 22763 RVA: 0x00139111 File Offset: 0x00137311
		public virtual bool IsReEntrant
		{
			get
			{
				return this._bReEntrant;
			}
		}

		// Token: 0x17000EBF RID: 3775
		// (get) Token: 0x060058EC RID: 22764 RVA: 0x00139119 File Offset: 0x00137319
		// (set) Token: 0x060058ED RID: 22765 RVA: 0x00139121 File Offset: 0x00137321
		internal string SyncCallOutLCID
		{
			get
			{
				return this._syncLcid;
			}
			set
			{
				this._syncLcid = value;
			}
		}

		// Token: 0x17000EC0 RID: 3776
		// (get) Token: 0x060058EE RID: 22766 RVA: 0x0013912A File Offset: 0x0013732A
		internal ArrayList AsyncCallOutLCIDList
		{
			get
			{
				return this._asyncLcidList;
			}
		}

		// Token: 0x060058EF RID: 22767 RVA: 0x00139134 File Offset: 0x00137334
		internal bool IsKnownLCID(IMessage reqMsg)
		{
			string logicalCallID = ((LogicalCallContext)reqMsg.Properties[Message.CallContextKey]).RemotingData.LogicalCallID;
			return logicalCallID.Equals(this._syncLcid) || this._asyncLcidList.Contains(logicalCallID);
		}

		// Token: 0x060058F0 RID: 22768 RVA: 0x0013917D File Offset: 0x0013737D
		public SynchronizationAttribute()
			: this(4, false)
		{
		}

		// Token: 0x060058F1 RID: 22769 RVA: 0x00139187 File Offset: 0x00137387
		public SynchronizationAttribute(bool reEntrant)
			: this(4, reEntrant)
		{
		}

		// Token: 0x060058F2 RID: 22770 RVA: 0x00139191 File Offset: 0x00137391
		public SynchronizationAttribute(int flag)
			: this(flag, false)
		{
		}

		// Token: 0x060058F3 RID: 22771 RVA: 0x0013919B File Offset: 0x0013739B
		public SynchronizationAttribute(int flag, bool reEntrant)
			: base("Synchronization")
		{
			this._bReEntrant = reEntrant;
			if (flag - 1 <= 1 || flag == 4 || flag == 8)
			{
				this._flavor = flag;
				return;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"), "flag");
		}

		// Token: 0x060058F4 RID: 22772 RVA: 0x001391D9 File Offset: 0x001373D9
		internal void Dispose()
		{
			if (this._waitHandle != null)
			{
				this._waitHandle.Unregister(null);
			}
		}

		// Token: 0x060058F5 RID: 22773 RVA: 0x001391F0 File Offset: 0x001373F0
		[SecurityCritical]
		[ComVisible(true)]
		public override bool IsContextOK(Context ctx, IConstructionCallMessage msg)
		{
			if (ctx == null)
			{
				throw new ArgumentNullException("ctx");
			}
			if (msg == null)
			{
				throw new ArgumentNullException("msg");
			}
			bool flag = true;
			if (this._flavor == 8)
			{
				flag = false;
			}
			else
			{
				SynchronizationAttribute synchronizationAttribute = (SynchronizationAttribute)ctx.GetProperty("Synchronization");
				if ((this._flavor == 1 && synchronizationAttribute != null) || (this._flavor == 4 && synchronizationAttribute == null))
				{
					flag = false;
				}
				if (this._flavor == 4)
				{
					this._cliCtxAttr = synchronizationAttribute;
				}
			}
			return flag;
		}

		// Token: 0x060058F6 RID: 22774 RVA: 0x00139264 File Offset: 0x00137464
		[SecurityCritical]
		[ComVisible(true)]
		public override void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg)
		{
			if (this._flavor == 1 || this._flavor == 2 || ctorMsg == null)
			{
				return;
			}
			if (this._cliCtxAttr != null)
			{
				ctorMsg.ContextProperties.Add(this._cliCtxAttr);
				this._cliCtxAttr = null;
				return;
			}
			ctorMsg.ContextProperties.Add(this);
		}

		// Token: 0x060058F7 RID: 22775 RVA: 0x001392B8 File Offset: 0x001374B8
		internal virtual void InitIfNecessary()
		{
			lock (this)
			{
				if (this._asyncWorkEvent == null)
				{
					this._asyncWorkEvent = new AutoResetEvent(false);
					this._workItemQueue = new Queue();
					this._asyncLcidList = new ArrayList();
					WaitOrTimerCallback waitOrTimerCallback = new WaitOrTimerCallback(this.DispatcherCallBack);
					this._waitHandle = ThreadPool.RegisterWaitForSingleObject(this._asyncWorkEvent, waitOrTimerCallback, null, SynchronizationAttribute._timeOut, false);
				}
			}
		}

		// Token: 0x060058F8 RID: 22776 RVA: 0x00139340 File Offset: 0x00137540
		private void DispatcherCallBack(object stateIgnored, bool ignored)
		{
			Queue workItemQueue = this._workItemQueue;
			WorkItem workItem;
			lock (workItemQueue)
			{
				workItem = (WorkItem)this._workItemQueue.Dequeue();
			}
			this.ExecuteWorkItem(workItem);
			this.HandleWorkCompletion();
		}

		// Token: 0x060058F9 RID: 22777 RVA: 0x00139398 File Offset: 0x00137598
		internal virtual void HandleThreadExit()
		{
			this.HandleWorkCompletion();
		}

		// Token: 0x060058FA RID: 22778 RVA: 0x001393A0 File Offset: 0x001375A0
		internal virtual void HandleThreadReEntry()
		{
			WorkItem workItem = new WorkItem(null, null, null);
			workItem.SetDummy();
			this.HandleWorkRequest(workItem);
		}

		// Token: 0x060058FB RID: 22779 RVA: 0x001393C4 File Offset: 0x001375C4
		internal virtual void HandleWorkCompletion()
		{
			WorkItem workItem = null;
			bool flag = false;
			Queue workItemQueue = this._workItemQueue;
			lock (workItemQueue)
			{
				if (this._workItemQueue.Count >= 1)
				{
					workItem = (WorkItem)this._workItemQueue.Peek();
					flag = true;
					workItem.SetSignaled();
				}
				else
				{
					this._locked = false;
				}
			}
			if (flag)
			{
				if (workItem.IsAsync())
				{
					this._asyncWorkEvent.Set();
					return;
				}
				WorkItem workItem2 = workItem;
				lock (workItem2)
				{
					Monitor.Pulse(workItem);
				}
			}
		}

		// Token: 0x060058FC RID: 22780 RVA: 0x0013947C File Offset: 0x0013767C
		internal virtual void HandleWorkRequest(WorkItem work)
		{
			if (!this.IsNestedCall(work._reqMsg))
			{
				if (work.IsAsync())
				{
					bool flag = true;
					Queue workItemQueue = this._workItemQueue;
					lock (workItemQueue)
					{
						work.SetWaiting();
						this._workItemQueue.Enqueue(work);
						if (!this._locked && this._workItemQueue.Count == 1)
						{
							work.SetSignaled();
							this._locked = true;
							this._asyncWorkEvent.Set();
						}
						return;
					}
				}
				lock (work)
				{
					Queue workItemQueue2 = this._workItemQueue;
					bool flag;
					lock (workItemQueue2)
					{
						if (!this._locked && this._workItemQueue.Count == 0)
						{
							this._locked = true;
							flag = false;
						}
						else
						{
							flag = true;
							work.SetWaiting();
							this._workItemQueue.Enqueue(work);
						}
					}
					if (flag)
					{
						Monitor.Wait(work);
						if (!work.IsDummy())
						{
							this.DispatcherCallBack(null, true);
							return;
						}
						Queue workItemQueue3 = this._workItemQueue;
						lock (workItemQueue3)
						{
							this._workItemQueue.Dequeue();
							return;
						}
					}
					if (!work.IsDummy())
					{
						work.SetSignaled();
						this.ExecuteWorkItem(work);
						this.HandleWorkCompletion();
					}
					return;
				}
			}
			work.SetSignaled();
			work.Execute();
		}

		// Token: 0x060058FD RID: 22781 RVA: 0x0013961C File Offset: 0x0013781C
		internal void ExecuteWorkItem(WorkItem work)
		{
			work.Execute();
		}

		// Token: 0x060058FE RID: 22782 RVA: 0x00139624 File Offset: 0x00137824
		internal bool IsNestedCall(IMessage reqMsg)
		{
			bool flag = false;
			if (!this.IsReEntrant)
			{
				string syncCallOutLCID = this.SyncCallOutLCID;
				if (syncCallOutLCID != null)
				{
					LogicalCallContext logicalCallContext = (LogicalCallContext)reqMsg.Properties[Message.CallContextKey];
					if (logicalCallContext != null && syncCallOutLCID.Equals(logicalCallContext.RemotingData.LogicalCallID))
					{
						flag = true;
					}
				}
				if (!flag && this.AsyncCallOutLCIDList.Count > 0)
				{
					LogicalCallContext logicalCallContext2 = (LogicalCallContext)reqMsg.Properties[Message.CallContextKey];
					if (this.AsyncCallOutLCIDList.Contains(logicalCallContext2.RemotingData.LogicalCallID))
					{
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x060058FF RID: 22783 RVA: 0x001396B8 File Offset: 0x001378B8
		[SecurityCritical]
		public virtual IMessageSink GetServerContextSink(IMessageSink nextSink)
		{
			this.InitIfNecessary();
			return new SynchronizedServerContextSink(this, nextSink);
		}

		// Token: 0x06005900 RID: 22784 RVA: 0x001396D4 File Offset: 0x001378D4
		[SecurityCritical]
		public virtual IMessageSink GetClientContextSink(IMessageSink nextSink)
		{
			this.InitIfNecessary();
			return new SynchronizedClientContextSink(this, nextSink);
		}

		// Token: 0x06005901 RID: 22785 RVA: 0x001396F0 File Offset: 0x001378F0
		// Note: this type is marked as 'beforefieldinit'.
		static SynchronizationAttribute()
		{
		}

		// Token: 0x04002883 RID: 10371
		public const int NOT_SUPPORTED = 1;

		// Token: 0x04002884 RID: 10372
		public const int SUPPORTED = 2;

		// Token: 0x04002885 RID: 10373
		public const int REQUIRED = 4;

		// Token: 0x04002886 RID: 10374
		public const int REQUIRES_NEW = 8;

		// Token: 0x04002887 RID: 10375
		private const string PROPERTY_NAME = "Synchronization";

		// Token: 0x04002888 RID: 10376
		private static readonly int _timeOut = -1;

		// Token: 0x04002889 RID: 10377
		[NonSerialized]
		internal AutoResetEvent _asyncWorkEvent;

		// Token: 0x0400288A RID: 10378
		[NonSerialized]
		private RegisteredWaitHandle _waitHandle;

		// Token: 0x0400288B RID: 10379
		[NonSerialized]
		internal Queue _workItemQueue;

		// Token: 0x0400288C RID: 10380
		[NonSerialized]
		internal bool _locked;

		// Token: 0x0400288D RID: 10381
		internal bool _bReEntrant;

		// Token: 0x0400288E RID: 10382
		internal int _flavor;

		// Token: 0x0400288F RID: 10383
		[NonSerialized]
		private SynchronizationAttribute _cliCtxAttr;

		// Token: 0x04002890 RID: 10384
		[NonSerialized]
		private string _syncLcid;

		// Token: 0x04002891 RID: 10385
		[NonSerialized]
		private ArrayList _asyncLcidList;
	}
}
