using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x0200060D RID: 1549
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum PortableExecutableKinds
	{
		// Token: 0x04001DB7 RID: 7607
		NotAPortableExecutableImage = 0,
		// Token: 0x04001DB8 RID: 7608
		ILOnly = 1,
		// Token: 0x04001DB9 RID: 7609
		Required32Bit = 2,
		// Token: 0x04001DBA RID: 7610
		PE32Plus = 4,
		// Token: 0x04001DBB RID: 7611
		Unmanaged32Bit = 8,
		// Token: 0x04001DBC RID: 7612
		[ComVisible(false)]
		Preferred32Bit = 16
	}
}
