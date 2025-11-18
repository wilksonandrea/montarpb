using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[ComImport]
[CompilerGenerated]
[Guid("9C4C6277-5027-441E-AFAE-CA1F542DA009")]
[TypeIdentifier]
public interface GInterface2 : IEnumerable
{
	void _VtblGap1_1();

	[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	[DispId(2)]
	void imethod_0([In][MarshalAs(UnmanagedType.Interface)] GInterface1 ginterface1_0);

	[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	[DispId(3)]
	void imethod_1([In][MarshalAs(UnmanagedType.BStr)] string string_0);
}
