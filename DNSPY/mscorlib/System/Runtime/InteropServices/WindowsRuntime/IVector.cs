using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A1B RID: 2587
	[Guid("913337e9-11a1-4345-a3a2-4e7f956e222d")]
	[ComImport]
	internal interface IVector<T> : IIterable<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x060065D0 RID: 26064
		T GetAt(uint index);

		// Token: 0x1700117F RID: 4479
		// (get) Token: 0x060065D1 RID: 26065
		uint Size { get; }

		// Token: 0x060065D2 RID: 26066
		IReadOnlyList<T> GetView();

		// Token: 0x060065D3 RID: 26067
		bool IndexOf(T value, out uint index);

		// Token: 0x060065D4 RID: 26068
		void SetAt(uint index, T value);

		// Token: 0x060065D5 RID: 26069
		void InsertAt(uint index, T value);

		// Token: 0x060065D6 RID: 26070
		void RemoveAt(uint index);

		// Token: 0x060065D7 RID: 26071
		void Append(T value);

		// Token: 0x060065D8 RID: 26072
		void RemoveAtEnd();

		// Token: 0x060065D9 RID: 26073
		void Clear();

		// Token: 0x060065DA RID: 26074
		uint GetMany(uint startIndex, [Out] T[] items);

		// Token: 0x060065DB RID: 26075
		void ReplaceAll(T[] items);
	}
}
