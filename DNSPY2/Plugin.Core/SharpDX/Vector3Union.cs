using System;
using System.Runtime.InteropServices;

namespace Plugin.Core.SharpDX
{
	// Token: 0x02000041 RID: 65
	[StructLayout(LayoutKind.Explicit)]
	public struct Vector3Union
	{
		// Token: 0x040000D3 RID: 211
		[FieldOffset(0)]
		public Vector3 Vec3;

		// Token: 0x040000D4 RID: 212
		[FieldOffset(0)]
		public RawVector3 RawVec3;
	}
}
