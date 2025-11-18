using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000602 RID: 1538
	// (Invoke) Token: 0x060046DA RID: 18138
	[ComVisible(true)]
	[Serializable]
	public delegate bool MemberFilter(MemberInfo m, object filterCriteria);
}
