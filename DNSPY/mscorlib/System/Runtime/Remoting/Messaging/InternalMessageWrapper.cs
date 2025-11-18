using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000874 RID: 2164
	[SecurityCritical]
	[ComVisible(true)]
	public class InternalMessageWrapper
	{
		// Token: 0x06005C02 RID: 23554 RVA: 0x001429C8 File Offset: 0x00140BC8
		public InternalMessageWrapper(IMessage msg)
		{
			this.WrappedMessage = msg;
		}

		// Token: 0x06005C03 RID: 23555 RVA: 0x001429D8 File Offset: 0x00140BD8
		[SecurityCritical]
		internal object GetIdentityObject()
		{
			IInternalMessage internalMessage = this.WrappedMessage as IInternalMessage;
			if (internalMessage != null)
			{
				return internalMessage.IdentityObject;
			}
			InternalMessageWrapper internalMessageWrapper = this.WrappedMessage as InternalMessageWrapper;
			if (internalMessageWrapper != null)
			{
				return internalMessageWrapper.GetIdentityObject();
			}
			return null;
		}

		// Token: 0x06005C04 RID: 23556 RVA: 0x00142A14 File Offset: 0x00140C14
		[SecurityCritical]
		internal object GetServerIdentityObject()
		{
			IInternalMessage internalMessage = this.WrappedMessage as IInternalMessage;
			if (internalMessage != null)
			{
				return internalMessage.ServerIdentityObject;
			}
			InternalMessageWrapper internalMessageWrapper = this.WrappedMessage as InternalMessageWrapper;
			if (internalMessageWrapper != null)
			{
				return internalMessageWrapper.GetServerIdentityObject();
			}
			return null;
		}

		// Token: 0x0400299B RID: 10651
		protected IMessage WrappedMessage;
	}
}
