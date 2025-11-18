using System;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200087E RID: 2174
	internal class RemotingSurrogate : ISerializationSurrogate
	{
		// Token: 0x06005C5E RID: 23646 RVA: 0x00143730 File Offset: 0x00141930
		[SecurityCritical]
		public virtual void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			if (RemotingServices.IsTransparentProxy(obj))
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(obj);
				realProxy.GetObjectData(info, context);
				return;
			}
			RemotingServices.GetObjectData(obj, info, context);
		}

		// Token: 0x06005C5F RID: 23647 RVA: 0x00143779 File Offset: 0x00141979
		[SecurityCritical]
		public virtual object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_PopulateData"));
		}

		// Token: 0x06005C60 RID: 23648 RVA: 0x0014378A File Offset: 0x0014198A
		public RemotingSurrogate()
		{
		}
	}
}
