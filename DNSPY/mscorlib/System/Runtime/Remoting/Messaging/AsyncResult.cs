using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000857 RID: 2135
	[ComVisible(true)]
	public class AsyncResult : IAsyncResult, IMessageSink
	{
		// Token: 0x06005A6E RID: 23150 RVA: 0x0013DBBA File Offset: 0x0013BDBA
		[SecurityCritical]
		internal AsyncResult(Message m)
		{
			m.GetAsyncBeginInfo(out this._acbd, out this._asyncState);
			this._asyncDelegate = (Delegate)m.GetThisPtr();
		}

		// Token: 0x17000F1B RID: 3867
		// (get) Token: 0x06005A6F RID: 23151 RVA: 0x0013DBE5 File Offset: 0x0013BDE5
		public virtual bool IsCompleted
		{
			get
			{
				return this._isCompleted;
			}
		}

		// Token: 0x17000F1C RID: 3868
		// (get) Token: 0x06005A70 RID: 23152 RVA: 0x0013DBED File Offset: 0x0013BDED
		public virtual object AsyncDelegate
		{
			get
			{
				return this._asyncDelegate;
			}
		}

		// Token: 0x17000F1D RID: 3869
		// (get) Token: 0x06005A71 RID: 23153 RVA: 0x0013DBF5 File Offset: 0x0013BDF5
		public virtual object AsyncState
		{
			get
			{
				return this._asyncState;
			}
		}

		// Token: 0x17000F1E RID: 3870
		// (get) Token: 0x06005A72 RID: 23154 RVA: 0x0013DBFD File Offset: 0x0013BDFD
		public virtual bool CompletedSynchronously
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000F1F RID: 3871
		// (get) Token: 0x06005A73 RID: 23155 RVA: 0x0013DC00 File Offset: 0x0013BE00
		// (set) Token: 0x06005A74 RID: 23156 RVA: 0x0013DC08 File Offset: 0x0013BE08
		public bool EndInvokeCalled
		{
			get
			{
				return this._endInvokeCalled;
			}
			set
			{
				this._endInvokeCalled = value;
			}
		}

		// Token: 0x06005A75 RID: 23157 RVA: 0x0013DC14 File Offset: 0x0013BE14
		private void FaultInWaitHandle()
		{
			lock (this)
			{
				if (this._AsyncWaitHandle == null)
				{
					this._AsyncWaitHandle = new ManualResetEvent(false);
				}
			}
		}

		// Token: 0x17000F20 RID: 3872
		// (get) Token: 0x06005A76 RID: 23158 RVA: 0x0013DC60 File Offset: 0x0013BE60
		public virtual WaitHandle AsyncWaitHandle
		{
			get
			{
				this.FaultInWaitHandle();
				return this._AsyncWaitHandle;
			}
		}

		// Token: 0x06005A77 RID: 23159 RVA: 0x0013DC6E File Offset: 0x0013BE6E
		public virtual void SetMessageCtrl(IMessageCtrl mc)
		{
			this._mc = mc;
		}

		// Token: 0x06005A78 RID: 23160 RVA: 0x0013DC78 File Offset: 0x0013BE78
		[SecurityCritical]
		public virtual IMessage SyncProcessMessage(IMessage msg)
		{
			if (msg == null)
			{
				this._replyMsg = new ReturnMessage(new RemotingException(Environment.GetResourceString("Remoting_NullMessage")), new ErrorMessage());
			}
			else if (!(msg is IMethodReturnMessage))
			{
				this._replyMsg = new ReturnMessage(new RemotingException(Environment.GetResourceString("Remoting_Message_BadType")), new ErrorMessage());
			}
			else
			{
				this._replyMsg = msg;
			}
			this._isCompleted = true;
			this.FaultInWaitHandle();
			this._AsyncWaitHandle.Set();
			if (this._acbd != null)
			{
				this._acbd(this);
			}
			return null;
		}

		// Token: 0x06005A79 RID: 23161 RVA: 0x0013DD07 File Offset: 0x0013BF07
		[SecurityCritical]
		public virtual IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
		}

		// Token: 0x17000F21 RID: 3873
		// (get) Token: 0x06005A7A RID: 23162 RVA: 0x0013DD18 File Offset: 0x0013BF18
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x06005A7B RID: 23163 RVA: 0x0013DD1B File Offset: 0x0013BF1B
		public virtual IMessage GetReplyMessage()
		{
			return this._replyMsg;
		}

		// Token: 0x0400290D RID: 10509
		private IMessageCtrl _mc;

		// Token: 0x0400290E RID: 10510
		private AsyncCallback _acbd;

		// Token: 0x0400290F RID: 10511
		private IMessage _replyMsg;

		// Token: 0x04002910 RID: 10512
		private bool _isCompleted;

		// Token: 0x04002911 RID: 10513
		private bool _endInvokeCalled;

		// Token: 0x04002912 RID: 10514
		private ManualResetEvent _AsyncWaitHandle;

		// Token: 0x04002913 RID: 10515
		private Delegate _asyncDelegate;

		// Token: 0x04002914 RID: 10516
		private object _asyncState;
	}
}
