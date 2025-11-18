using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A19 RID: 2585
	[Guid("6a79e863-4300-459a-9966-cbb660963ee1")]
	[ComImport]
	internal interface IIterator<T>
	{
		// Token: 0x1700117B RID: 4475
		// (get) Token: 0x060065C9 RID: 26057
		T Current { get; }

		// Token: 0x1700117C RID: 4476
		// (get) Token: 0x060065CA RID: 26058
		bool HasCurrent { get; }

		// Token: 0x060065CB RID: 26059
		bool MoveNext();

		// Token: 0x060065CC RID: 26060
		int GetMany([Out] T[] items);
	}
}
