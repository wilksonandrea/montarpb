using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// Token: 0x0200003A RID: 58
[CompilerGenerated]
[Guid("9C4C6277-5027-441E-AFAE-CA1F542DA009")]
[TypeIdentifier]
[ComImport]
public interface GInterface2 : IEnumerable
{
	// Token: 0x0600013D RID: 317
	void _VtblGap1_1();

	// Token: 0x0600013E RID: 318
	[DispId(2)]
	[MethodImpl(MethodImplOptions.InternalCall)]
	void imethod_0([MarshalAs(UnmanagedType.Interface)] [In] GInterface1 ginterface1_0);

	// Token: 0x0600013F RID: 319
	[DispId(3)]
	[MethodImpl(MethodImplOptions.InternalCall)]
	void imethod_1([MarshalAs(UnmanagedType.BStr)] [In] string string_0);
}
