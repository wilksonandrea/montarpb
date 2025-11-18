using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x02000733 RID: 1843
	[ComVisible(true)]
	public interface IDeserializationCallback
	{
		// Token: 0x060051AC RID: 20908
		void OnDeserialization(object sender);
	}
}
