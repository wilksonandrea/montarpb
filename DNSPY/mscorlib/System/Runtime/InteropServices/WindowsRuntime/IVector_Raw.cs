using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A1C RID: 2588
	[Guid("913337e9-11a1-4345-a3a2-4e7f956e222d")]
	[ComImport]
	internal interface IVector_Raw<T> : IIterable<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x060065DC RID: 26076
		T GetAt(uint index);

		// Token: 0x17001180 RID: 4480
		// (get) Token: 0x060065DD RID: 26077
		uint Size { get; }

		// Token: 0x060065DE RID: 26078
		IVectorView<T> GetView();

		// Token: 0x060065DF RID: 26079
		bool IndexOf(T value, out uint index);

		// Token: 0x060065E0 RID: 26080
		void SetAt(uint index, T value);

		// Token: 0x060065E1 RID: 26081
		void InsertAt(uint index, T value);

		// Token: 0x060065E2 RID: 26082
		void RemoveAt(uint index);

		// Token: 0x060065E3 RID: 26083
		void Append(T value);

		// Token: 0x060065E4 RID: 26084
		void RemoveAtEnd();

		// Token: 0x060065E5 RID: 26085
		void Clear();

		// Token: 0x060065E6 RID: 26086
		uint GetMany(uint startIndex, [Out] T[] items);

		// Token: 0x060065E7 RID: 26087
		void ReplaceAll(T[] items);
	}
}
