using System;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000973 RID: 2419
	[Guid("F1C3BF78-C3E4-11d3-88E7-00902754C43A")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true)]
	public interface ITypeLibConverter
	{
		// Token: 0x0600623F RID: 25151
		AssemblyBuilder ConvertTypeLibToAssembly([MarshalAs(UnmanagedType.Interface)] object typeLib, string asmFileName, TypeLibImporterFlags flags, ITypeLibImporterNotifySink notifySink, byte[] publicKey, StrongNameKeyPair keyPair, string asmNamespace, Version asmVersion);

		// Token: 0x06006240 RID: 25152
		[return: MarshalAs(UnmanagedType.Interface)]
		object ConvertAssemblyToTypeLib(Assembly assembly, string typeLibName, TypeLibExporterFlags flags, ITypeLibExporterNotifySink notifySink);

		// Token: 0x06006241 RID: 25153
		bool GetPrimaryInteropAssembly(Guid g, int major, int minor, int lcid, out string asmName, out string asmCodeBase);

		// Token: 0x06006242 RID: 25154
		AssemblyBuilder ConvertTypeLibToAssembly([MarshalAs(UnmanagedType.Interface)] object typeLib, string asmFileName, int flags, ITypeLibImporterNotifySink notifySink, byte[] publicKey, StrongNameKeyPair keyPair, bool unsafeInterfaces);
	}
}
