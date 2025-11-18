using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting
{
	// Token: 0x020007CE RID: 1998
	internal class ServerIdentity : Identity
	{
		// Token: 0x06005695 RID: 22165 RVA: 0x00133074 File Offset: 0x00131274
		internal Type GetLastCalledType(string newTypeName)
		{
			ServerIdentity.LastCalledType lastCalledType = this._lastCalledType;
			if (lastCalledType == null)
			{
				return null;
			}
			string typeName = lastCalledType.typeName;
			Type type = lastCalledType.type;
			if (typeName == null || type == null)
			{
				return null;
			}
			if (typeName.Equals(newTypeName))
			{
				return type;
			}
			return null;
		}

		// Token: 0x06005696 RID: 22166 RVA: 0x001330B8 File Offset: 0x001312B8
		internal void SetLastCalledType(string newTypeName, Type newType)
		{
			this._lastCalledType = new ServerIdentity.LastCalledType
			{
				typeName = newTypeName,
				type = newType
			};
		}

		// Token: 0x06005697 RID: 22167 RVA: 0x001330E0 File Offset: 0x001312E0
		[SecurityCritical]
		internal void SetHandle()
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				Monitor.Enter(this, ref flag);
				if (!this._srvIdentityHandle.IsAllocated)
				{
					this._srvIdentityHandle = new GCHandle(this, GCHandleType.Normal);
				}
				else
				{
					this._srvIdentityHandle.Target = this;
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this);
				}
			}
		}

		// Token: 0x06005698 RID: 22168 RVA: 0x00133140 File Offset: 0x00131340
		[SecurityCritical]
		internal void ResetHandle()
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				Monitor.Enter(this, ref flag);
				this._srvIdentityHandle.Target = null;
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this);
				}
			}
		}

		// Token: 0x06005699 RID: 22169 RVA: 0x00133184 File Offset: 0x00131384
		internal GCHandle GetHandle()
		{
			return this._srvIdentityHandle;
		}

		// Token: 0x0600569A RID: 22170 RVA: 0x0013318C File Offset: 0x0013138C
		[SecurityCritical]
		internal ServerIdentity(MarshalByRefObject obj, Context serverCtx)
			: base(obj is ContextBoundObject)
		{
			if (obj != null)
			{
				if (!RemotingServices.IsTransparentProxy(obj))
				{
					this._srvType = obj.GetType();
				}
				else
				{
					RealProxy realProxy = RemotingServices.GetRealProxy(obj);
					this._srvType = realProxy.GetProxiedType();
				}
			}
			this._srvCtx = serverCtx;
			this._serverObjectChain = null;
			this._stackBuilderSink = null;
		}

		// Token: 0x0600569B RID: 22171 RVA: 0x001331E9 File Offset: 0x001313E9
		[SecurityCritical]
		internal ServerIdentity(MarshalByRefObject obj, Context serverCtx, string uri)
			: this(obj, serverCtx)
		{
			base.SetOrCreateURI(uri, true);
		}

		// Token: 0x17000E37 RID: 3639
		// (get) Token: 0x0600569C RID: 22172 RVA: 0x001331FB File Offset: 0x001313FB
		internal Context ServerContext
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this._srvCtx;
			}
		}

		// Token: 0x0600569D RID: 22173 RVA: 0x00133203 File Offset: 0x00131403
		internal void SetSingleCallObjectMode()
		{
			this._flags |= 512;
		}

		// Token: 0x0600569E RID: 22174 RVA: 0x00133217 File Offset: 0x00131417
		internal void SetSingletonObjectMode()
		{
			this._flags |= 1024;
		}

		// Token: 0x0600569F RID: 22175 RVA: 0x0013322B File Offset: 0x0013142B
		internal bool IsSingleCall()
		{
			return (this._flags & 512) != 0;
		}

		// Token: 0x060056A0 RID: 22176 RVA: 0x0013323C File Offset: 0x0013143C
		internal bool IsSingleton()
		{
			return (this._flags & 1024) != 0;
		}

		// Token: 0x060056A1 RID: 22177 RVA: 0x00133250 File Offset: 0x00131450
		[SecurityCritical]
		internal IMessageSink GetServerObjectChain(out MarshalByRefObject obj)
		{
			obj = null;
			if (!this.IsSingleCall())
			{
				if (this._serverObjectChain == null)
				{
					bool flag = false;
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						Monitor.Enter(this, ref flag);
						if (this._serverObjectChain == null)
						{
							MarshalByRefObject tporObject = base.TPOrObject;
							this._serverObjectChain = this._srvCtx.CreateServerObjectChain(tporObject);
						}
					}
					finally
					{
						if (flag)
						{
							Monitor.Exit(this);
						}
					}
				}
				return this._serverObjectChain;
			}
			MarshalByRefObject marshalByRefObject;
			IMessageSink messageSink;
			if (this._tpOrObject != null && this._firstCallDispatched == 0 && Interlocked.CompareExchange(ref this._firstCallDispatched, 1, 0) == 0)
			{
				marshalByRefObject = (MarshalByRefObject)this._tpOrObject;
				messageSink = this._serverObjectChain;
				if (messageSink == null)
				{
					messageSink = this._srvCtx.CreateServerObjectChain(marshalByRefObject);
				}
			}
			else
			{
				marshalByRefObject = (MarshalByRefObject)Activator.CreateInstance(this._srvType, true);
				string objectUri = RemotingServices.GetObjectUri(marshalByRefObject);
				if (objectUri != null)
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_WellKnown_CtorCantMarshal"), base.URI));
				}
				if (!RemotingServices.IsTransparentProxy(marshalByRefObject))
				{
					marshalByRefObject.__RaceSetServerIdentity(this);
				}
				else
				{
					RealProxy realProxy = RemotingServices.GetRealProxy(marshalByRefObject);
					realProxy.IdentityObject = this;
				}
				messageSink = this._srvCtx.CreateServerObjectChain(marshalByRefObject);
			}
			obj = marshalByRefObject;
			return messageSink;
		}

		// Token: 0x17000E38 RID: 3640
		// (get) Token: 0x060056A2 RID: 22178 RVA: 0x00133380 File Offset: 0x00131580
		// (set) Token: 0x060056A3 RID: 22179 RVA: 0x00133388 File Offset: 0x00131588
		internal Type ServerType
		{
			get
			{
				return this._srvType;
			}
			set
			{
				this._srvType = value;
			}
		}

		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x060056A4 RID: 22180 RVA: 0x00133391 File Offset: 0x00131591
		// (set) Token: 0x060056A5 RID: 22181 RVA: 0x00133399 File Offset: 0x00131599
		internal bool MarshaledAsSpecificType
		{
			get
			{
				return this._bMarshaledAsSpecificType;
			}
			set
			{
				this._bMarshaledAsSpecificType = value;
			}
		}

		// Token: 0x060056A6 RID: 22182 RVA: 0x001333A4 File Offset: 0x001315A4
		[SecurityCritical]
		internal IMessageSink RaceSetServerObjectChain(IMessageSink serverObjectChain)
		{
			if (this._serverObjectChain == null)
			{
				bool flag = false;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					Monitor.Enter(this, ref flag);
					if (this._serverObjectChain == null)
					{
						this._serverObjectChain = serverObjectChain;
					}
				}
				finally
				{
					if (flag)
					{
						Monitor.Exit(this);
					}
				}
			}
			return this._serverObjectChain;
		}

		// Token: 0x060056A7 RID: 22183 RVA: 0x001333FC File Offset: 0x001315FC
		[SecurityCritical]
		internal bool AddServerSideDynamicProperty(IDynamicProperty prop)
		{
			if (this._dphSrv == null)
			{
				DynamicPropertyHolder dynamicPropertyHolder = new DynamicPropertyHolder();
				bool flag = false;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					Monitor.Enter(this, ref flag);
					if (this._dphSrv == null)
					{
						this._dphSrv = dynamicPropertyHolder;
					}
				}
				finally
				{
					if (flag)
					{
						Monitor.Exit(this);
					}
				}
			}
			return this._dphSrv.AddDynamicProperty(prop);
		}

		// Token: 0x060056A8 RID: 22184 RVA: 0x00133460 File Offset: 0x00131660
		[SecurityCritical]
		internal bool RemoveServerSideDynamicProperty(string name)
		{
			if (this._dphSrv == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_PropNotFound"));
			}
			return this._dphSrv.RemoveDynamicProperty(name);
		}

		// Token: 0x17000E3A RID: 3642
		// (get) Token: 0x060056A9 RID: 22185 RVA: 0x00133486 File Offset: 0x00131686
		internal ArrayWithSize ServerSideDynamicSinks
		{
			[SecurityCritical]
			get
			{
				if (this._dphSrv == null)
				{
					return null;
				}
				return this._dphSrv.DynamicSinks;
			}
		}

		// Token: 0x060056AA RID: 22186 RVA: 0x0013349D File Offset: 0x0013169D
		[SecurityCritical]
		internal override void AssertValid()
		{
			if (base.TPOrObject != null)
			{
				RemotingServices.IsTransparentProxy(base.TPOrObject);
			}
		}

		// Token: 0x040027A8 RID: 10152
		internal Context _srvCtx;

		// Token: 0x040027A9 RID: 10153
		internal IMessageSink _serverObjectChain;

		// Token: 0x040027AA RID: 10154
		internal StackBuilderSink _stackBuilderSink;

		// Token: 0x040027AB RID: 10155
		internal DynamicPropertyHolder _dphSrv;

		// Token: 0x040027AC RID: 10156
		internal Type _srvType;

		// Token: 0x040027AD RID: 10157
		private ServerIdentity.LastCalledType _lastCalledType;

		// Token: 0x040027AE RID: 10158
		internal bool _bMarshaledAsSpecificType;

		// Token: 0x040027AF RID: 10159
		internal int _firstCallDispatched;

		// Token: 0x040027B0 RID: 10160
		internal GCHandle _srvIdentityHandle;

		// Token: 0x02000C6D RID: 3181
		private class LastCalledType
		{
			// Token: 0x060070AC RID: 28844 RVA: 0x001845B6 File Offset: 0x001827B6
			public LastCalledType()
			{
			}

			// Token: 0x040037E6 RID: 14310
			public string typeName;

			// Token: 0x040037E7 RID: 14311
			public Type type;
		}
	}
}
