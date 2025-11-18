using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000972 RID: 2418
	[Guid("F1C3BF77-C3E4-11d3-88E7-00902754C43A")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true)]
	public interface ITypeLibExporterNotifySink
	{
		// Token: 0x0600623D RID: 25149
		void ReportEvent(ExporterEventKind eventKind, int eventCode, string eventMsg);

		// Token: 0x0600623E RID: 25150
		[return: MarshalAs(UnmanagedType.Interface)]
		object ResolveRef(Assembly assembly);
	}
}
