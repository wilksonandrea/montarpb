using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000671 RID: 1649
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("285a8871-c84a-11d7-850f-005cd062464f")]
	[ComImport]
	internal interface ISectionWithStringKey
	{
		// Token: 0x06004F27 RID: 20263
		void Lookup([MarshalAs(UnmanagedType.LPWStr)] string wzStringKey, [MarshalAs(UnmanagedType.Interface)] out object ppUnknown);

		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x06004F28 RID: 20264
		bool IsCaseInsensitive { get; }
	}
}
