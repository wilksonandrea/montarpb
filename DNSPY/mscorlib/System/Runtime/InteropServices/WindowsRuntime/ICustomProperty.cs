using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A12 RID: 2578
	[Guid("30DA92C0-23E8-42A0-AE7C-734A0E5D2782")]
	[ComImport]
	internal interface ICustomProperty
	{
		// Token: 0x17001173 RID: 4467
		// (get) Token: 0x060065AC RID: 26028
		Type Type { get; }

		// Token: 0x17001174 RID: 4468
		// (get) Token: 0x060065AD RID: 26029
		string Name { get; }

		// Token: 0x060065AE RID: 26030
		object GetValue(object target);

		// Token: 0x060065AF RID: 26031
		void SetValue(object target, object value);

		// Token: 0x060065B0 RID: 26032
		object GetValue(object target, object indexValue);

		// Token: 0x060065B1 RID: 26033
		void SetValue(object target, object value, object indexValue);

		// Token: 0x17001175 RID: 4469
		// (get) Token: 0x060065B2 RID: 26034
		bool CanWrite { get; }

		// Token: 0x17001176 RID: 4470
		// (get) Token: 0x060065B3 RID: 26035
		bool CanRead { get; }
	}
}
