using System;
using System.Collections;
using System.Reflection;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Proxies;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000862 RID: 2146
	internal class ConstructorCallMessage : IConstructionCallMessage, IMethodCallMessage, IMethodMessage, IMessage
	{
		// Token: 0x06005AE0 RID: 23264 RVA: 0x0013EA82 File Offset: 0x0013CC82
		private ConstructorCallMessage()
		{
		}

		// Token: 0x06005AE1 RID: 23265 RVA: 0x0013EA8A File Offset: 0x0013CC8A
		[SecurityCritical]
		internal ConstructorCallMessage(object[] callSiteActivationAttributes, object[] womAttr, object[] typeAttr, RuntimeType serverType)
		{
			this._activationType = serverType;
			this._activationTypeName = RemotingServices.GetDefaultQualifiedTypeName(this._activationType);
			this._callSiteActivationAttributes = callSiteActivationAttributes;
			this._womGlobalAttributes = womAttr;
			this._typeAttributes = typeAttr;
		}

		// Token: 0x06005AE2 RID: 23266 RVA: 0x0013EAC0 File Offset: 0x0013CCC0
		[SecurityCritical]
		public object GetThisPtr()
		{
			if (this._message != null)
			{
				return this._message.GetThisPtr();
			}
			throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
		}

		// Token: 0x17000F45 RID: 3909
		// (get) Token: 0x06005AE3 RID: 23267 RVA: 0x0013EAE5 File Offset: 0x0013CCE5
		public object[] CallSiteActivationAttributes
		{
			[SecurityCritical]
			get
			{
				return this._callSiteActivationAttributes;
			}
		}

		// Token: 0x06005AE4 RID: 23268 RVA: 0x0013EAED File Offset: 0x0013CCED
		internal object[] GetWOMAttributes()
		{
			return this._womGlobalAttributes;
		}

		// Token: 0x06005AE5 RID: 23269 RVA: 0x0013EAF5 File Offset: 0x0013CCF5
		internal object[] GetTypeAttributes()
		{
			return this._typeAttributes;
		}

		// Token: 0x17000F46 RID: 3910
		// (get) Token: 0x06005AE6 RID: 23270 RVA: 0x0013EAFD File Offset: 0x0013CCFD
		public Type ActivationType
		{
			[SecurityCritical]
			get
			{
				if (this._activationType == null && this._activationTypeName != null)
				{
					this._activationType = RemotingServices.InternalGetTypeFromQualifiedTypeName(this._activationTypeName, false);
				}
				return this._activationType;
			}
		}

		// Token: 0x17000F47 RID: 3911
		// (get) Token: 0x06005AE7 RID: 23271 RVA: 0x0013EB2D File Offset: 0x0013CD2D
		public string ActivationTypeName
		{
			[SecurityCritical]
			get
			{
				return this._activationTypeName;
			}
		}

		// Token: 0x17000F48 RID: 3912
		// (get) Token: 0x06005AE8 RID: 23272 RVA: 0x0013EB35 File Offset: 0x0013CD35
		public IList ContextProperties
		{
			[SecurityCritical]
			get
			{
				if (this._contextProperties == null)
				{
					this._contextProperties = new ArrayList();
				}
				return this._contextProperties;
			}
		}

		// Token: 0x17000F49 RID: 3913
		// (get) Token: 0x06005AE9 RID: 23273 RVA: 0x0013EB50 File Offset: 0x0013CD50
		// (set) Token: 0x06005AEA RID: 23274 RVA: 0x0013EB75 File Offset: 0x0013CD75
		public string Uri
		{
			[SecurityCritical]
			get
			{
				if (this._message != null)
				{
					return this._message.Uri;
				}
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
			}
			set
			{
				if (this._message != null)
				{
					this._message.Uri = value;
					return;
				}
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
			}
		}

		// Token: 0x17000F4A RID: 3914
		// (get) Token: 0x06005AEB RID: 23275 RVA: 0x0013EB9B File Offset: 0x0013CD9B
		public string MethodName
		{
			[SecurityCritical]
			get
			{
				if (this._message != null)
				{
					return this._message.MethodName;
				}
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
			}
		}

		// Token: 0x17000F4B RID: 3915
		// (get) Token: 0x06005AEC RID: 23276 RVA: 0x0013EBC0 File Offset: 0x0013CDC0
		public string TypeName
		{
			[SecurityCritical]
			get
			{
				if (this._message != null)
				{
					return this._message.TypeName;
				}
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
			}
		}

		// Token: 0x17000F4C RID: 3916
		// (get) Token: 0x06005AED RID: 23277 RVA: 0x0013EBE5 File Offset: 0x0013CDE5
		public object MethodSignature
		{
			[SecurityCritical]
			get
			{
				if (this._message != null)
				{
					return this._message.MethodSignature;
				}
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
			}
		}

		// Token: 0x17000F4D RID: 3917
		// (get) Token: 0x06005AEE RID: 23278 RVA: 0x0013EC0A File Offset: 0x0013CE0A
		public MethodBase MethodBase
		{
			[SecurityCritical]
			get
			{
				if (this._message != null)
				{
					return this._message.MethodBase;
				}
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
			}
		}

		// Token: 0x17000F4E RID: 3918
		// (get) Token: 0x06005AEF RID: 23279 RVA: 0x0013EC2F File Offset: 0x0013CE2F
		public int InArgCount
		{
			[SecurityCritical]
			get
			{
				if (this._argMapper == null)
				{
					this._argMapper = new ArgMapper(this, false);
				}
				return this._argMapper.ArgCount;
			}
		}

		// Token: 0x06005AF0 RID: 23280 RVA: 0x0013EC51 File Offset: 0x0013CE51
		[SecurityCritical]
		public object GetInArg(int argNum)
		{
			if (this._argMapper == null)
			{
				this._argMapper = new ArgMapper(this, false);
			}
			return this._argMapper.GetArg(argNum);
		}

		// Token: 0x06005AF1 RID: 23281 RVA: 0x0013EC74 File Offset: 0x0013CE74
		[SecurityCritical]
		public string GetInArgName(int index)
		{
			if (this._argMapper == null)
			{
				this._argMapper = new ArgMapper(this, false);
			}
			return this._argMapper.GetArgName(index);
		}

		// Token: 0x17000F4F RID: 3919
		// (get) Token: 0x06005AF2 RID: 23282 RVA: 0x0013EC97 File Offset: 0x0013CE97
		public object[] InArgs
		{
			[SecurityCritical]
			get
			{
				if (this._argMapper == null)
				{
					this._argMapper = new ArgMapper(this, false);
				}
				return this._argMapper.Args;
			}
		}

		// Token: 0x17000F50 RID: 3920
		// (get) Token: 0x06005AF3 RID: 23283 RVA: 0x0013ECB9 File Offset: 0x0013CEB9
		public int ArgCount
		{
			[SecurityCritical]
			get
			{
				if (this._message != null)
				{
					return this._message.ArgCount;
				}
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
			}
		}

		// Token: 0x06005AF4 RID: 23284 RVA: 0x0013ECDE File Offset: 0x0013CEDE
		[SecurityCritical]
		public object GetArg(int argNum)
		{
			if (this._message != null)
			{
				return this._message.GetArg(argNum);
			}
			throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
		}

		// Token: 0x06005AF5 RID: 23285 RVA: 0x0013ED04 File Offset: 0x0013CF04
		[SecurityCritical]
		public string GetArgName(int index)
		{
			if (this._message != null)
			{
				return this._message.GetArgName(index);
			}
			throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
		}

		// Token: 0x17000F51 RID: 3921
		// (get) Token: 0x06005AF6 RID: 23286 RVA: 0x0013ED2A File Offset: 0x0013CF2A
		public bool HasVarArgs
		{
			[SecurityCritical]
			get
			{
				if (this._message != null)
				{
					return this._message.HasVarArgs;
				}
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
			}
		}

		// Token: 0x17000F52 RID: 3922
		// (get) Token: 0x06005AF7 RID: 23287 RVA: 0x0013ED4F File Offset: 0x0013CF4F
		public object[] Args
		{
			[SecurityCritical]
			get
			{
				if (this._message != null)
				{
					return this._message.Args;
				}
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
			}
		}

		// Token: 0x17000F53 RID: 3923
		// (get) Token: 0x06005AF8 RID: 23288 RVA: 0x0013ED74 File Offset: 0x0013CF74
		public IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				if (this._properties == null)
				{
					object obj = new CCMDictionary(this, new Hashtable());
					Interlocked.CompareExchange(ref this._properties, obj, null);
				}
				return (IDictionary)this._properties;
			}
		}

		// Token: 0x17000F54 RID: 3924
		// (get) Token: 0x06005AF9 RID: 23289 RVA: 0x0013EDAE File Offset: 0x0013CFAE
		// (set) Token: 0x06005AFA RID: 23290 RVA: 0x0013EDB6 File Offset: 0x0013CFB6
		public IActivator Activator
		{
			[SecurityCritical]
			get
			{
				return this._activator;
			}
			[SecurityCritical]
			set
			{
				this._activator = value;
			}
		}

		// Token: 0x17000F55 RID: 3925
		// (get) Token: 0x06005AFB RID: 23291 RVA: 0x0013EDBF File Offset: 0x0013CFBF
		public LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get
			{
				return this.GetLogicalCallContext();
			}
		}

		// Token: 0x17000F56 RID: 3926
		// (get) Token: 0x06005AFC RID: 23292 RVA: 0x0013EDC7 File Offset: 0x0013CFC7
		// (set) Token: 0x06005AFD RID: 23293 RVA: 0x0013EDD4 File Offset: 0x0013CFD4
		internal bool ActivateInContext
		{
			get
			{
				return (this._iFlags & 1) != 0;
			}
			set
			{
				this._iFlags = (value ? (this._iFlags | 1) : (this._iFlags & -2));
			}
		}

		// Token: 0x06005AFE RID: 23294 RVA: 0x0013EDF2 File Offset: 0x0013CFF2
		[SecurityCritical]
		internal void SetFrame(MessageData msgData)
		{
			this._message = new Message();
			this._message.InitFields(msgData);
		}

		// Token: 0x06005AFF RID: 23295 RVA: 0x0013EE0B File Offset: 0x0013D00B
		[SecurityCritical]
		internal LogicalCallContext GetLogicalCallContext()
		{
			if (this._message != null)
			{
				return this._message.GetLogicalCallContext();
			}
			throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
		}

		// Token: 0x06005B00 RID: 23296 RVA: 0x0013EE30 File Offset: 0x0013D030
		[SecurityCritical]
		internal LogicalCallContext SetLogicalCallContext(LogicalCallContext ctx)
		{
			if (this._message != null)
			{
				return this._message.SetLogicalCallContext(ctx);
			}
			throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
		}

		// Token: 0x06005B01 RID: 23297 RVA: 0x0013EE56 File Offset: 0x0013D056
		internal Message GetMessage()
		{
			return this._message;
		}

		// Token: 0x04002934 RID: 10548
		private object[] _callSiteActivationAttributes;

		// Token: 0x04002935 RID: 10549
		private object[] _womGlobalAttributes;

		// Token: 0x04002936 RID: 10550
		private object[] _typeAttributes;

		// Token: 0x04002937 RID: 10551
		[NonSerialized]
		private RuntimeType _activationType;

		// Token: 0x04002938 RID: 10552
		private string _activationTypeName;

		// Token: 0x04002939 RID: 10553
		private IList _contextProperties;

		// Token: 0x0400293A RID: 10554
		private int _iFlags;

		// Token: 0x0400293B RID: 10555
		private Message _message;

		// Token: 0x0400293C RID: 10556
		private object _properties;

		// Token: 0x0400293D RID: 10557
		private ArgMapper _argMapper;

		// Token: 0x0400293E RID: 10558
		private IActivator _activator;

		// Token: 0x0400293F RID: 10559
		private const int CCM_ACTIVATEINCONTEXT = 1;
	}
}
