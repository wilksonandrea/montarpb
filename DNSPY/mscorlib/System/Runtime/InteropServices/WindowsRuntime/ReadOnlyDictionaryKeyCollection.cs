using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009E7 RID: 2535
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	internal sealed class ReadOnlyDictionaryKeyCollection<TKey, TValue> : IEnumerable<TKey>, IEnumerable
	{
		// Token: 0x06006491 RID: 25745 RVA: 0x00156A5C File Offset: 0x00154C5C
		public ReadOnlyDictionaryKeyCollection(IReadOnlyDictionary<TKey, TValue> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this.dictionary = dictionary;
		}

		// Token: 0x06006492 RID: 25746 RVA: 0x00156A79 File Offset: 0x00154C79
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<TKey>)this).GetEnumerator();
		}

		// Token: 0x06006493 RID: 25747 RVA: 0x00156A81 File Offset: 0x00154C81
		public IEnumerator<TKey> GetEnumerator()
		{
			return new ReadOnlyDictionaryKeyEnumerator<TKey, TValue>(this.dictionary);
		}

		// Token: 0x04002CFA RID: 11514
		private readonly IReadOnlyDictionary<TKey, TValue> dictionary;
	}
}
