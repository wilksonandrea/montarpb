using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A1A RID: 2586
	[Guid("6a1d6c07-076d-49f2-8314-f52c9c9a8331")]
	[ComImport]
	internal interface IBindableIterator
	{
		// Token: 0x1700117D RID: 4477
		// (get) Token: 0x060065CD RID: 26061
		object Current { get; }

		// Token: 0x1700117E RID: 4478
		// (get) Token: 0x060065CE RID: 26062
		bool HasCurrent { get; }

		// Token: 0x060065CF RID: 26063
		bool MoveNext();
	}
}
