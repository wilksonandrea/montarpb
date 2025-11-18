using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x020006A9 RID: 1705
	internal struct StoreOperationMetadataProperty
	{
		// Token: 0x06004FDC RID: 20444 RVA: 0x0011C816 File Offset: 0x0011AA16
		public StoreOperationMetadataProperty(Guid PropertySet, string Name)
		{
			this = new StoreOperationMetadataProperty(PropertySet, Name, null);
		}

		// Token: 0x06004FDD RID: 20445 RVA: 0x0011C821 File Offset: 0x0011AA21
		public StoreOperationMetadataProperty(Guid PropertySet, string Name, string Value)
		{
			this.GuidPropertySet = PropertySet;
			this.Name = Name;
			this.Value = Value;
			this.ValueSize = ((Value != null) ? new IntPtr((Value.Length + 1) * 2) : IntPtr.Zero);
		}

		// Token: 0x0400224C RID: 8780
		public Guid GuidPropertySet;

		// Token: 0x0400224D RID: 8781
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Name;

		// Token: 0x0400224E RID: 8782
		[MarshalAs(UnmanagedType.SysUInt)]
		public IntPtr ValueSize;

		// Token: 0x0400224F RID: 8783
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Value;
	}
}
