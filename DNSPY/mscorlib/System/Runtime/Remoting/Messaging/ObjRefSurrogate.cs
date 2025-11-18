using System;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200087F RID: 2175
	internal class ObjRefSurrogate : ISerializationSurrogate
	{
		// Token: 0x06005C61 RID: 23649 RVA: 0x00143792 File Offset: 0x00141992
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
			((ObjRef)obj).GetObjectData(info, context);
			info.AddValue("fIsMarshalled", 0);
		}

		// Token: 0x06005C62 RID: 23650 RVA: 0x001437C9 File Offset: 0x001419C9
		[SecurityCritical]
		public virtual object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_PopulateData"));
		}

		// Token: 0x06005C63 RID: 23651 RVA: 0x001437DA File Offset: 0x001419DA
		public ObjRefSurrogate()
		{
		}
	}
}
