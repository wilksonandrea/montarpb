using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000675 RID: 1653
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("285a8860-c84a-11d7-850f-005cd062464f")]
	[ComImport]
	internal interface ICDF
	{
		// Token: 0x06004F30 RID: 20272
		ISection GetRootSection(uint SectionId);

		// Token: 0x06004F31 RID: 20273
		ISectionEntry GetRootSectionEntry(uint SectionId);

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x06004F32 RID: 20274
		object _NewEnum
		{
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x06004F33 RID: 20275
		uint Count { get; }

		// Token: 0x06004F34 RID: 20276
		object GetItem(uint SectionId);
	}
}
