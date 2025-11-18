using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Serialization.Formatters
{
	// Token: 0x02000763 RID: 1891
	[ComVisible(true)]
	public interface IFieldInfo
	{
		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x0600530B RID: 21259
		// (set) Token: 0x0600530C RID: 21260
		string[] FieldNames
		{
			[SecurityCritical]
			get;
			[SecurityCritical]
			set;
		}

		// Token: 0x17000DBF RID: 3519
		// (get) Token: 0x0600530D RID: 21261
		// (set) Token: 0x0600530E RID: 21262
		Type[] FieldTypes
		{
			[SecurityCritical]
			get;
			[SecurityCritical]
			set;
		}
	}
}
