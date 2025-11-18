using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000673 RID: 1651
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("285a8861-c84a-11d7-850f-005cd062464f")]
	[ComImport]
	internal interface ISectionEntry
	{
		// Token: 0x06004F2A RID: 20266
		object GetField(uint fieldId);

		// Token: 0x06004F2B RID: 20267
		string GetFieldName(uint fieldId);
	}
}
