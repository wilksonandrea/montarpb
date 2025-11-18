using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection
{
	// Token: 0x020005FF RID: 1535
	internal struct MetadataEnumResult
	{
		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x0600469A RID: 18074 RVA: 0x00102947 File Offset: 0x00100B47
		public int Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x17000AB2 RID: 2738
		public unsafe int this[int index]
		{
			[SecurityCritical]
			get
			{
				if (this.largeResult != null)
				{
					return this.largeResult[index];
				}
				fixed (int* ptr = &this.smallResult.FixedElementField)
				{
					int* ptr2 = ptr;
					return ptr2[index];
				}
			}
		}

		// Token: 0x04001D58 RID: 7512
		private int[] largeResult;

		// Token: 0x04001D59 RID: 7513
		private int length;

		// Token: 0x04001D5A RID: 7514
		[FixedBuffer(typeof(int), 16)]
		private MetadataEnumResult.<smallResult>e__FixedBuffer smallResult;

		// Token: 0x02000C3C RID: 3132
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 64)]
		public struct <smallResult>e__FixedBuffer
		{
			// Token: 0x04003742 RID: 14146
			public int FixedElementField;
		}
	}
}
