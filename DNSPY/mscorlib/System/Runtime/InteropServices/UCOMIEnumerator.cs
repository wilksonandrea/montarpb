using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000981 RID: 2433
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IEnumerator instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("496B0ABF-CDEE-11d3-88E8-00902754C43A")]
	internal interface UCOMIEnumerator
	{
		// Token: 0x0600628F RID: 25231
		bool MoveNext();

		// Token: 0x17001116 RID: 4374
		// (get) Token: 0x06006290 RID: 25232
		object Current { get; }

		// Token: 0x06006291 RID: 25233
		void Reset();
	}
}
