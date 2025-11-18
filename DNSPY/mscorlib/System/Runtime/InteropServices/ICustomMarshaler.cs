using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200094D RID: 2381
	[ComVisible(true)]
	public interface ICustomMarshaler
	{
		// Token: 0x060060B1 RID: 24753
		object MarshalNativeToManaged(IntPtr pNativeData);

		// Token: 0x060060B2 RID: 24754
		IntPtr MarshalManagedToNative(object ManagedObj);

		// Token: 0x060060B3 RID: 24755
		void CleanUpNativeData(IntPtr pNativeData);

		// Token: 0x060060B4 RID: 24756
		void CleanUpManagedData(object ManagedObj);

		// Token: 0x060060B5 RID: 24757
		int GetNativeDataSize();
	}
}
