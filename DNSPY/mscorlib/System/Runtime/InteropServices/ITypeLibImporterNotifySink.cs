using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000971 RID: 2417
	[Guid("F1C3BF76-C3E4-11d3-88E7-00902754C43A")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true)]
	public interface ITypeLibImporterNotifySink
	{
		// Token: 0x0600623B RID: 25147
		void ReportEvent(ImporterEventKind eventKind, int eventCode, string eventMsg);

		// Token: 0x0600623C RID: 25148
		Assembly ResolveRef([MarshalAs(UnmanagedType.Interface)] object typeLib);
	}
}
