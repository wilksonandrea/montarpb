using System;
using System.Collections;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000865 RID: 2149
	internal class MCMDictionary : MessageDictionary
	{
		// Token: 0x06005B0C RID: 23308 RVA: 0x0013F293 File Offset: 0x0013D493
		public MCMDictionary(IMethodCallMessage msg, IDictionary idict)
			: base(MCMDictionary.MCMkeys, idict)
		{
			this._mcmsg = msg;
		}

		// Token: 0x06005B0D RID: 23309 RVA: 0x0013F2A8 File Offset: 0x0013D4A8
		[SecuritySafeCritical]
		internal override object GetMessageValue(int i)
		{
			switch (i)
			{
			case 0:
				return this._mcmsg.Uri;
			case 1:
				return this._mcmsg.MethodName;
			case 2:
				return this._mcmsg.MethodSignature;
			case 3:
				return this._mcmsg.TypeName;
			case 4:
				return this._mcmsg.Args;
			case 5:
				return this.FetchLogicalCallContext();
			default:
				throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
			}
		}

		// Token: 0x06005B0E RID: 23310 RVA: 0x0013F328 File Offset: 0x0013D528
		[SecurityCritical]
		private LogicalCallContext FetchLogicalCallContext()
		{
			Message message = this._mcmsg as Message;
			if (message != null)
			{
				return message.GetLogicalCallContext();
			}
			MethodCall methodCall = this._mcmsg as MethodCall;
			if (methodCall != null)
			{
				return methodCall.GetLogicalCallContext();
			}
			throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
		}

		// Token: 0x06005B0F RID: 23311 RVA: 0x0013F370 File Offset: 0x0013D570
		[SecurityCritical]
		internal override void SetSpecialKey(int keyNum, object value)
		{
			Message message = this._mcmsg as Message;
			MethodCall methodCall = this._mcmsg as MethodCall;
			if (keyNum != 0)
			{
				if (keyNum != 1)
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
				}
				if (message != null)
				{
					message.SetLogicalCallContext((LogicalCallContext)value);
					return;
				}
				throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
			}
			else
			{
				if (message != null)
				{
					message.Uri = (string)value;
					return;
				}
				if (methodCall != null)
				{
					methodCall.Uri = (string)value;
					return;
				}
				throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
			}
		}

		// Token: 0x06005B10 RID: 23312 RVA: 0x0013F3FE File Offset: 0x0013D5FE
		// Note: this type is marked as 'beforefieldinit'.
		static MCMDictionary()
		{
		}

		// Token: 0x04002946 RID: 10566
		public static string[] MCMkeys = new string[] { "__Uri", "__MethodName", "__MethodSignature", "__TypeName", "__Args", "__CallContext" };

		// Token: 0x04002947 RID: 10567
		internal IMethodCallMessage _mcmsg;
	}
}
