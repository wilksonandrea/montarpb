using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Serialization
{
	// Token: 0x02000738 RID: 1848
	[ComVisible(true)]
	public interface ISerializationSurrogate
	{
		// Token: 0x060051C8 RID: 20936
		[SecurityCritical]
		void GetObjectData(object obj, SerializationInfo info, StreamingContext context);

		// Token: 0x060051C9 RID: 20937
		[SecurityCritical]
		object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector);
	}
}
