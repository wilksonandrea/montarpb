using System;
using System.Collections;
using System.Runtime.Remoting.Activation;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000863 RID: 2147
	internal class CCMDictionary : MessageDictionary
	{
		// Token: 0x06005B02 RID: 23298 RVA: 0x0013EE5E File Offset: 0x0013D05E
		public CCMDictionary(IConstructionCallMessage msg, IDictionary idict)
			: base(CCMDictionary.CCMkeys, idict)
		{
			this._ccmsg = msg;
		}

		// Token: 0x06005B03 RID: 23299 RVA: 0x0013EE74 File Offset: 0x0013D074
		[SecuritySafeCritical]
		internal override object GetMessageValue(int i)
		{
			switch (i)
			{
			case 0:
				return this._ccmsg.Uri;
			case 1:
				return this._ccmsg.MethodName;
			case 2:
				return this._ccmsg.MethodSignature;
			case 3:
				return this._ccmsg.TypeName;
			case 4:
				return this._ccmsg.Args;
			case 5:
				return this.FetchLogicalCallContext();
			case 6:
				return this._ccmsg.CallSiteActivationAttributes;
			case 7:
				return null;
			case 8:
				return this._ccmsg.ContextProperties;
			case 9:
				return this._ccmsg.Activator;
			case 10:
				return this._ccmsg.ActivationTypeName;
			default:
				throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
			}
		}

		// Token: 0x06005B04 RID: 23300 RVA: 0x0013EF3C File Offset: 0x0013D13C
		[SecurityCritical]
		private LogicalCallContext FetchLogicalCallContext()
		{
			ConstructorCallMessage constructorCallMessage = this._ccmsg as ConstructorCallMessage;
			if (constructorCallMessage != null)
			{
				return constructorCallMessage.GetLogicalCallContext();
			}
			if (this._ccmsg is ConstructionCall)
			{
				return ((MethodCall)this._ccmsg).GetLogicalCallContext();
			}
			throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
		}

		// Token: 0x06005B05 RID: 23301 RVA: 0x0013EF8C File Offset: 0x0013D18C
		[SecurityCritical]
		internal override void SetSpecialKey(int keyNum, object value)
		{
			if (keyNum == 0)
			{
				((ConstructorCallMessage)this._ccmsg).Uri = (string)value;
				return;
			}
			if (keyNum != 1)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
			}
			((ConstructorCallMessage)this._ccmsg).SetLogicalCallContext((LogicalCallContext)value);
		}

		// Token: 0x06005B06 RID: 23302 RVA: 0x0013EFE0 File Offset: 0x0013D1E0
		// Note: this type is marked as 'beforefieldinit'.
		static CCMDictionary()
		{
		}

		// Token: 0x04002940 RID: 10560
		public static string[] CCMkeys = new string[]
		{
			"__Uri", "__MethodName", "__MethodSignature", "__TypeName", "__Args", "__CallContext", "__CallSiteActivationAttributes", "__ActivationType", "__ContextProperties", "__Activator",
			"__ActivationTypeName"
		};

		// Token: 0x04002941 RID: 10561
		internal IConstructionCallMessage _ccmsg;
	}
}
