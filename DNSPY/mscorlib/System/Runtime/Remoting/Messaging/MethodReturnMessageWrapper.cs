using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000876 RID: 2166
	[SecurityCritical]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	public class MethodReturnMessageWrapper : InternalMessageWrapper, IMethodReturnMessage, IMethodMessage, IMessage
	{
		// Token: 0x06005C18 RID: 23576 RVA: 0x00142BD0 File Offset: 0x00140DD0
		public MethodReturnMessageWrapper(IMethodReturnMessage msg)
			: base(msg)
		{
			this._msg = msg;
			this._args = this._msg.Args;
			this._returnValue = this._msg.ReturnValue;
			this._exception = this._msg.Exception;
		}

		// Token: 0x17000FCA RID: 4042
		// (get) Token: 0x06005C19 RID: 23577 RVA: 0x00142C1E File Offset: 0x00140E1E
		// (set) Token: 0x06005C1A RID: 23578 RVA: 0x00142C2B File Offset: 0x00140E2B
		public string Uri
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

		// Token: 0x17000FCB RID: 4043
		// (get) Token: 0x06005C1B RID: 23579 RVA: 0x00142C43 File Offset: 0x00140E43
		public virtual string MethodName
		{
			[SecurityCritical]
			get
			{
				return this._msg.MethodName;
			}
		}

		// Token: 0x17000FCC RID: 4044
		// (get) Token: 0x06005C1C RID: 23580 RVA: 0x00142C50 File Offset: 0x00140E50
		public virtual string TypeName
		{
			[SecurityCritical]
			get
			{
				return this._msg.TypeName;
			}
		}

		// Token: 0x17000FCD RID: 4045
		// (get) Token: 0x06005C1D RID: 23581 RVA: 0x00142C5D File Offset: 0x00140E5D
		public virtual object MethodSignature
		{
			[SecurityCritical]
			get
			{
				return this._msg.MethodSignature;
			}
		}

		// Token: 0x17000FCE RID: 4046
		// (get) Token: 0x06005C1E RID: 23582 RVA: 0x00142C6A File Offset: 0x00140E6A
		public virtual LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get
			{
				return this._msg.LogicalCallContext;
			}
		}

		// Token: 0x17000FCF RID: 4047
		// (get) Token: 0x06005C1F RID: 23583 RVA: 0x00142C77 File Offset: 0x00140E77
		public virtual MethodBase MethodBase
		{
			[SecurityCritical]
			get
			{
				return this._msg.MethodBase;
			}
		}

		// Token: 0x17000FD0 RID: 4048
		// (get) Token: 0x06005C20 RID: 23584 RVA: 0x00142C84 File Offset: 0x00140E84
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

		// Token: 0x06005C21 RID: 23585 RVA: 0x00142C98 File Offset: 0x00140E98
		[SecurityCritical]
		public virtual string GetArgName(int index)
		{
			return this._msg.GetArgName(index);
		}

		// Token: 0x06005C22 RID: 23586 RVA: 0x00142CA6 File Offset: 0x00140EA6
		[SecurityCritical]
		public virtual object GetArg(int argNum)
		{
			return this._args[argNum];
		}

		// Token: 0x17000FD1 RID: 4049
		// (get) Token: 0x06005C23 RID: 23587 RVA: 0x00142CB0 File Offset: 0x00140EB0
		// (set) Token: 0x06005C24 RID: 23588 RVA: 0x00142CB8 File Offset: 0x00140EB8
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

		// Token: 0x17000FD2 RID: 4050
		// (get) Token: 0x06005C25 RID: 23589 RVA: 0x00142CC1 File Offset: 0x00140EC1
		public virtual bool HasVarArgs
		{
			[SecurityCritical]
			get
			{
				return this._msg.HasVarArgs;
			}
		}

		// Token: 0x17000FD3 RID: 4051
		// (get) Token: 0x06005C26 RID: 23590 RVA: 0x00142CCE File Offset: 0x00140ECE
		public virtual int OutArgCount
		{
			[SecurityCritical]
			get
			{
				if (this._argMapper == null)
				{
					this._argMapper = new ArgMapper(this, true);
				}
				return this._argMapper.ArgCount;
			}
		}

		// Token: 0x06005C27 RID: 23591 RVA: 0x00142CF0 File Offset: 0x00140EF0
		[SecurityCritical]
		public virtual object GetOutArg(int argNum)
		{
			if (this._argMapper == null)
			{
				this._argMapper = new ArgMapper(this, true);
			}
			return this._argMapper.GetArg(argNum);
		}

		// Token: 0x06005C28 RID: 23592 RVA: 0x00142D13 File Offset: 0x00140F13
		[SecurityCritical]
		public virtual string GetOutArgName(int index)
		{
			if (this._argMapper == null)
			{
				this._argMapper = new ArgMapper(this, true);
			}
			return this._argMapper.GetArgName(index);
		}

		// Token: 0x17000FD4 RID: 4052
		// (get) Token: 0x06005C29 RID: 23593 RVA: 0x00142D36 File Offset: 0x00140F36
		public virtual object[] OutArgs
		{
			[SecurityCritical]
			get
			{
				if (this._argMapper == null)
				{
					this._argMapper = new ArgMapper(this, true);
				}
				return this._argMapper.Args;
			}
		}

		// Token: 0x17000FD5 RID: 4053
		// (get) Token: 0x06005C2A RID: 23594 RVA: 0x00142D58 File Offset: 0x00140F58
		// (set) Token: 0x06005C2B RID: 23595 RVA: 0x00142D60 File Offset: 0x00140F60
		public virtual Exception Exception
		{
			[SecurityCritical]
			get
			{
				return this._exception;
			}
			set
			{
				this._exception = value;
			}
		}

		// Token: 0x17000FD6 RID: 4054
		// (get) Token: 0x06005C2C RID: 23596 RVA: 0x00142D69 File Offset: 0x00140F69
		// (set) Token: 0x06005C2D RID: 23597 RVA: 0x00142D71 File Offset: 0x00140F71
		public virtual object ReturnValue
		{
			[SecurityCritical]
			get
			{
				return this._returnValue;
			}
			set
			{
				this._returnValue = value;
			}
		}

		// Token: 0x17000FD7 RID: 4055
		// (get) Token: 0x06005C2E RID: 23598 RVA: 0x00142D7A File Offset: 0x00140F7A
		public virtual IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				if (this._properties == null)
				{
					this._properties = new MethodReturnMessageWrapper.MRMWrapperDictionary(this, this._msg.Properties);
				}
				return this._properties;
			}
		}

		// Token: 0x040029A0 RID: 10656
		private IMethodReturnMessage _msg;

		// Token: 0x040029A1 RID: 10657
		private IDictionary _properties;

		// Token: 0x040029A2 RID: 10658
		private ArgMapper _argMapper;

		// Token: 0x040029A3 RID: 10659
		private object[] _args;

		// Token: 0x040029A4 RID: 10660
		private object _returnValue;

		// Token: 0x040029A5 RID: 10661
		private Exception _exception;

		// Token: 0x02000C7C RID: 3196
		private class MRMWrapperDictionary : Hashtable
		{
			// Token: 0x060070C3 RID: 28867 RVA: 0x001848B0 File Offset: 0x00182AB0
			public MRMWrapperDictionary(IMethodReturnMessage msg, IDictionary idict)
			{
				this._mrmsg = msg;
				this._idict = idict;
			}

			// Token: 0x17001354 RID: 4948
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
							return this._mrmsg.Uri;
						}
						if (text == "__MethodName")
						{
							return this._mrmsg.MethodName;
						}
						if (text == "__MethodSignature")
						{
							return this._mrmsg.MethodSignature;
						}
						if (text == "__TypeName")
						{
							return this._mrmsg.TypeName;
						}
						if (text == "__Return")
						{
							return this._mrmsg.ReturnValue;
						}
						if (text == "__OutArgs")
						{
							return this._mrmsg.OutArgs;
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
						if (text == "__MethodName" || text == "__MethodSignature" || text == "__TypeName" || text == "__Return" || text == "__OutArgs")
						{
							throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
						}
						this._idict[key] = value;
					}
				}
			}

			// Token: 0x04003812 RID: 14354
			private IMethodReturnMessage _mrmsg;

			// Token: 0x04003813 RID: 14355
			private IDictionary _idict;
		}
	}
}
