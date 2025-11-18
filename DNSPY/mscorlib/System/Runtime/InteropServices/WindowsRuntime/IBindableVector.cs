using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A1E RID: 2590
	[Guid("393de7de-6fd0-4c0d-bb71-47244a113e93")]
	[ComImport]
	internal interface IBindableVector : IBindableIterable
	{
		// Token: 0x060065EC RID: 26092
		object GetAt(uint index);

		// Token: 0x17001182 RID: 4482
		// (get) Token: 0x060065ED RID: 26093
		uint Size { get; }

		// Token: 0x060065EE RID: 26094
		IBindableVectorView GetView();

		// Token: 0x060065EF RID: 26095
		bool IndexOf(object value, out uint index);

		// Token: 0x060065F0 RID: 26096
		void SetAt(uint index, object value);

		// Token: 0x060065F1 RID: 26097
		void InsertAt(uint index, object value);

		// Token: 0x060065F2 RID: 26098
		void RemoveAt(uint index);

		// Token: 0x060065F3 RID: 26099
		void Append(object value);

		// Token: 0x060065F4 RID: 26100
		void RemoveAtEnd();

		// Token: 0x060065F5 RID: 26101
		void Clear();
	}
}
