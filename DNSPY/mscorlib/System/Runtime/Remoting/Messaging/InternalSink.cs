using System;
using System.Collections;
using System.Runtime.Remoting.Activation;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000883 RID: 2179
	[Serializable]
	internal class InternalSink
	{
		// Token: 0x06005C7E RID: 23678 RVA: 0x001444D0 File Offset: 0x001426D0
		[SecurityCritical]
		internal static IMessage ValidateMessage(IMessage reqMsg)
		{
			IMessage message = null;
			if (reqMsg == null)
			{
				message = new ReturnMessage(new ArgumentNullException("reqMsg"), null);
			}
			return message;
		}

		// Token: 0x06005C7F RID: 23679 RVA: 0x001444F4 File Offset: 0x001426F4
		[SecurityCritical]
		internal static IMessage DisallowAsyncActivation(IMessage reqMsg)
		{
			if (reqMsg is IConstructionCallMessage)
			{
				return new ReturnMessage(new RemotingException(Environment.GetResourceString("Remoting_Activation_AsyncUnsupported")), null);
			}
			return null;
		}

		// Token: 0x06005C80 RID: 23680 RVA: 0x00144518 File Offset: 0x00142718
		[SecurityCritical]
		internal static Identity GetIdentity(IMessage reqMsg)
		{
			Identity identity = null;
			if (reqMsg is IInternalMessage)
			{
				identity = ((IInternalMessage)reqMsg).IdentityObject;
			}
			else if (reqMsg is InternalMessageWrapper)
			{
				identity = (Identity)((InternalMessageWrapper)reqMsg).GetIdentityObject();
			}
			if (identity == null)
			{
				string uri = InternalSink.GetURI(reqMsg);
				identity = IdentityHolder.ResolveIdentity(uri);
				if (identity == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Remoting_ServerObjectNotFound", new object[] { uri }));
				}
			}
			return identity;
		}

		// Token: 0x06005C81 RID: 23681 RVA: 0x00144588 File Offset: 0x00142788
		[SecurityCritical]
		internal static ServerIdentity GetServerIdentity(IMessage reqMsg)
		{
			ServerIdentity serverIdentity = null;
			bool flag = false;
			IInternalMessage internalMessage = reqMsg as IInternalMessage;
			if (internalMessage != null)
			{
				serverIdentity = ((IInternalMessage)reqMsg).ServerIdentityObject;
				flag = true;
			}
			else if (reqMsg is InternalMessageWrapper)
			{
				serverIdentity = (ServerIdentity)((InternalMessageWrapper)reqMsg).GetServerIdentityObject();
			}
			if (serverIdentity == null)
			{
				string uri = InternalSink.GetURI(reqMsg);
				Identity identity = IdentityHolder.ResolveIdentity(uri);
				if (identity is ServerIdentity)
				{
					serverIdentity = (ServerIdentity)identity;
					if (flag)
					{
						internalMessage.ServerIdentityObject = serverIdentity;
					}
				}
			}
			return serverIdentity;
		}

		// Token: 0x06005C82 RID: 23682 RVA: 0x001445FC File Offset: 0x001427FC
		[SecurityCritical]
		internal static string GetURI(IMessage msg)
		{
			string text = null;
			IMethodMessage methodMessage = msg as IMethodMessage;
			if (methodMessage != null)
			{
				text = methodMessage.Uri;
			}
			else
			{
				IDictionary properties = msg.Properties;
				if (properties != null)
				{
					text = (string)properties["__Uri"];
				}
			}
			return text;
		}

		// Token: 0x06005C83 RID: 23683 RVA: 0x0014463A File Offset: 0x0014283A
		public InternalSink()
		{
		}
	}
}
