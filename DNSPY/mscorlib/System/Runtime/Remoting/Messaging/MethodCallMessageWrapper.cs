using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000875 RID: 2165
	[SecurityCritical]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	public class MethodCallMessageWrapper : InternalMessageWrapper, IMethodCallMessage, IMethodMessage, IMessage
	{
		// Token: 0x06005C05 RID: 23557 RVA: 0x00142A4E File Offset: 0x00140C4E
		public MethodCallMessageWrapper(IMethodCallMessage msg)
			: base(msg)
		{
			this._msg = msg;
			this._args = this._msg.Args;
		}

		// Token: 0x17000FBE RID: 4030
		// (get) Token: 0x06005C06 RID: 23558 RVA: 0x00142A6F File Offset: 0x00140C6F
		// (set) Token: 0x06005C07 RID: 23559 RVA: 0x00142A7C File Offset: 0x00140C7C
		public virtual string Uri
		{
			[SecurityCritical]
			get
			{
				return this._msg.Uri;
			}
			set
			{
				this._msg.Properties[Message.UriKey] = value;
			}
		}

		// Token: 0x17000FBF RID: 4031
		// (get) Token: 0x06005C08 RID: 23560 RVA: 0x00142A94 File Offset: 0x00140C94
		public virtual string MethodName
		{
			[SecurityCritical]
			get
			{
				return this._msg.MethodName;
			}
		}

		// Token: 0x17000FC0 RID: 4032
		// (get) Token: 0x06005C09 RID: 23561 RVA: 0x00142AA1 File Offset: 0x00140CA1
		public virtual string TypeName
		{
			[SecurityCritical]
			get
			{
				return this._msg.TypeName;
			}
		}

		// Token: 0x17000FC1 RID: 4033
		// (get) Token: 0x06005C0A RID: 23562 RVA: 0x00142AAE File Offset: 0x00140CAE
		public virtual object MethodSignature
		{
			[SecurityCritical]
			get
			{
				return this._msg.MethodSignature;
			}
		}

		// Token: 0x17000FC2 RID: 4034
		// (get) Token: 0x06005C0B RID: 23563 RVA: 0x00142ABB File Offset: 0x00140CBB
		public virtual LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get
			{
				return this._msg.LogicalCallContext;
			}
		}

		// Token: 0x17000FC3 RID: 4035
		// (get) Token: 0x06005C0C RID: 23564 RVA: 0x00142AC8 File Offset: 0x00140CC8
		public virtual MethodBase MethodBase
		{
			[SecurityCritical]
			get
			{
				return this._msg.MethodBase;
			}
		}

		// Token: 0x17000FC4 RID: 4036
		// (get) Token: 0x06005C0D RID: 23565 RVA: 0x00142AD5 File Offset: 0x00140CD5
		public virtual int ArgCount
		{
			[SecurityCritical]
			get
			{
				if (this._args != null)
				{
					return this._args.Length;
				}
				return 0;
			}
		}

		// Token: 0x06005C0E RID: 23566 RVA: 0x00142AE9 File Offset: 0x00140CE9
		[SecurityCritical]
		public virtual string GetArgName(int index)
		{
			return this._msg.GetArgName(index);
		}

		// Token: 0x06005C0F RID: 23567 RVA: 0x00142AF7 File Offset: 0x00140CF7
		[SecurityCritical]
		public virtual object GetArg(int argNum)
		{
			return this._args[argNum];
		}

		// Token: 0x17000FC5 RID: 4037
		// (get) Token: 0x06005C10 RID: 23568 RVA: 0x00142B01 File Offset: 0x00140D01
		// (set) Token: 0x06005C11 RID: 23569 RVA: 0x00142B09 File Offset: 0x00140D09
		public virtual object[] Args
		{
			[SecurityCritical]
			get
			{
				return this._args;
			}
			set
			{
				this._args = value;
			}
		}

		// Token: 0x17000FC6 RID: 4038
		// (get) Token: 0x06005C12 RID: 23570 RVA: 0x00142B12 File Offset: 0x00140D12
		public virtual bool HasVarArgs
		{
			[SecurityCritical]
			get
			{
				return this._msg.HasVarArgs;
			}
		}

		// Token: 0x17000FC7 RID: 4039
		// (get) Token: 0x06005C13 RID: 23571 RVA: 0x00142B1F File Offset: 0x00140D1F
		public virtual int InArgCount
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

		// Token: 0x06005C14 RID: 23572 RVA: 0x00142B41 File Offset: 0x00140D41
		[SecurityCritical]
		public virtual object GetInArg(int argNum)
		{
			if (this._argMapper == null)
			{
				this._argMapper = new ArgMapper(this, false);
			}
			return this._argMapper.GetArg(argNum);
		}

		// Token: 0x06005C15 RID: 23573 RVA: 0x00142B64 File Offset: 0x00140D64
		[SecurityCritical]
		public virtual string GetInArgName(int index)
		{
			if (this._argMapper == null)
			{
				this._argMapper = new ArgMapper(this, false);
			}
			return this._argMapper.GetArgName(index);
		}

		// Token: 0x17000FC8 RID: 4040
		// (get) Token: 0x06005C16 RID: 23574 RVA: 0x00142B87 File Offset: 0x00140D87
		public virtual object[] InArgs
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

		// Token: 0x17000FC9 RID: 4041
		// (get) Token: 0x06005C17 RID: 23575 RVA: 0x00142BA9 File Offset: 0x00140DA9
		public virtual IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				if (this._properties == null)
				{
					this._properties = new MethodCallMessageWrapper.MCMWrapperDictionary(this, this._msg.Properties);
				}
				return this._properties;
			}
		}

		// Token: 0x0400299C RID: 10652
		private IMethodCallMessage _msg;

		// Token: 0x0400299D RID: 10653
		private IDictionary _properties;

		// Token: 0x0400299E RID: 10654
		private ArgMapper _argMapper;

		// Token: 0x0400299F RID: 10655
		private object[] _args;

		// Token: 0x02000C7B RID: 3195
		private class MCMWrapperDictionary : Hashtable
		{
			// Token: 0x060070C0 RID: 28864 RVA: 0x0018478B File Offset: 0x0018298B
			public MCMWrapperDictionary(IMethodCallMessage msg, IDictionary idict)
			{
				this._mcmsg = msg;
				this._idict = idict;
			}

			// Token: 0x17001353 RID: 4947
			public override object this[object key]
			{
				[SecuritySafeCritical]
				get
				{
					string text = key as string;
					if (text != null)
					{
						if (text == "__Uri")
						{
							return this._mcmsg.Uri;
						}
						if (text == "__MethodName")
						{
							return this._mcmsg.MethodName;
						}
						if (text == "__MethodSignature")
						{
							return this._mcmsg.MethodSignature;
						}
						if (text == "__TypeName")
						{
							return this._mcmsg.TypeName;
						}
						if (text == "__Args")
						{
							return this._mcmsg.Args;
						}
					}
					return this._idict[key];
				}
				[SecuritySafeCritical]
				set
				{
					string text = key as string;
					if (text != null)
					{
						if (text == "__MethodName" || text == "__MethodSignature" || text == "__TypeName" || text == "__Args")
						{
							throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
						}
						this._idict[key] = value;
					}
				}
			}

			// Token: 0x04003810 RID: 14352
			private IMethodCallMessage _mcmsg;

			// Token: 0x04003811 RID: 14353
			private IDictionary _idict;
		}
	}
}
