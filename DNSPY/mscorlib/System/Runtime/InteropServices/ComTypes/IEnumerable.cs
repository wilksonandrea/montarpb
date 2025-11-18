using System;
using System.Collections;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A29 RID: 2601
	[Guid("496B0ABE-CDEE-11d3-88E8-00902754C43A")]
	internal interface IEnumerable
	{
		// Token: 0x06006621 RID: 26145
		[DispId(-4)]
		IEnumerator GetEnumerator();
	}
}
