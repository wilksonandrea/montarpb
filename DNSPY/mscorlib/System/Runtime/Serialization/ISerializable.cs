using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Serialization
{
	// Token: 0x02000737 RID: 1847
	[ComVisible(true)]
	public interface ISerializable
	{
		// Token: 0x060051C7 RID: 20935
		[SecurityCritical]
		void GetObjectData(SerializationInfo info, StreamingContext context);
	}
}
