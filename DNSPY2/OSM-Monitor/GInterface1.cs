using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// Token: 0x02000039 RID: 57
[CompilerGenerated]
[Guid("AF230D27-BABA-4E42-ACED-F524F22CFCE2")]
[TypeIdentifier]
[ComImport]
public interface GInterface1
{
	// Token: 0x1700002F RID: 47
	// (get) Token: 0x0600012D RID: 301
	// (set) Token: 0x0600012E RID: 302
	[DispId(1)]
	string String_0
	{
		[DispId(1)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
		[DispId(1)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[param: MarshalAs(UnmanagedType.BStr)]
		[param: In]
		set;
	}

	// Token: 0x0600012F RID: 303
	void _VtblGap1_2();

	// Token: 0x17000030 RID: 48
	// (get) Token: 0x06000130 RID: 304
	// (set) Token: 0x06000131 RID: 305
	[DispId(3)]
	string String_1
	{
		[DispId(3)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
		[DispId(3)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[param: MarshalAs(UnmanagedType.BStr)]
		[param: In]
		set;
	}

	// Token: 0x06000132 RID: 306
	void _VtblGap2_14();

	// Token: 0x17000031 RID: 49
	// (get) Token: 0x06000133 RID: 307
	// (set) Token: 0x06000134 RID: 308
	[DispId(11)]
	GEnum4 GEnum4_0
	{
		[DispId(11)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[DispId(11)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[param: In]
		set;
	}

	// Token: 0x06000135 RID: 309
	void _VtblGap3_2();

	// Token: 0x17000032 RID: 50
	// (get) Token: 0x06000136 RID: 310
	// (set) Token: 0x06000137 RID: 311
	[DispId(13)]
	string String_2
	{
		[DispId(13)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
		[DispId(13)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[param: MarshalAs(UnmanagedType.BStr)]
		[param: In]
		set;
	}

	// Token: 0x17000033 RID: 51
	// (get) Token: 0x06000138 RID: 312
	// (set) Token: 0x06000139 RID: 313
	[DispId(14)]
	bool Boolean_0
	{
		[DispId(14)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[DispId(14)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[param: In]
		set;
	}

	// Token: 0x0600013A RID: 314
	void _VtblGap4_6();

	// Token: 0x17000034 RID: 52
	// (get) Token: 0x0600013B RID: 315
	// (set) Token: 0x0600013C RID: 316
	[DispId(18)]
	GEnum3 GEnum3_0
	{
		[DispId(18)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[DispId(18)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[param: In]
		set;
	}
}
