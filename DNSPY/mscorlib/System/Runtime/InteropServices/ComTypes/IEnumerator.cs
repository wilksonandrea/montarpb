using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A2A RID: 2602
	[Guid("496B0ABF-CDEE-11d3-88E8-00902754C43A")]
	internal interface IEnumerator
	{
		// Token: 0x06006622 RID: 26146
		bool MoveNext();

		// Token: 0x1700118A RID: 4490
		// (get) Token: 0x06006623 RID: 26147
		object Current { get; }

		// Token: 0x06006624 RID: 26148
		void Reset();
	}
}
