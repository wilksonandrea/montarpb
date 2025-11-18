using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A00 RID: 2560
	internal class IStringableHelper
	{
		// Token: 0x060064F8 RID: 25848 RVA: 0x00157B14 File Offset: 0x00155D14
		internal static string ToString(object obj)
		{
			IStringable stringable = obj as IStringable;
			if (stringable != null)
			{
				return stringable.ToString();
			}
			return obj.ToString();
		}

		// Token: 0x060064F9 RID: 25849 RVA: 0x00157B38 File Offset: 0x00155D38
		public IStringableHelper()
		{
		}
	}
}
