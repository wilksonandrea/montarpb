using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Resources
{
	// Token: 0x0200038E RID: 910
	[ComVisible(true)]
	public interface IResourceReader : IEnumerable, IDisposable
	{
		// Token: 0x06002CF2 RID: 11506
		void Close();

		// Token: 0x06002CF3 RID: 11507
		IDictionaryEnumerator GetEnumerator();
	}
}
