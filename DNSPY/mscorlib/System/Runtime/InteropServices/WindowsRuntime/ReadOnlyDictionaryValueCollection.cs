using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009E9 RID: 2537
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	internal sealed class ReadOnlyDictionaryValueCollection<TKey, TValue> : IEnumerable<TValue>, IEnumerable
	{
		// Token: 0x0600649A RID: 25754 RVA: 0x00156B13 File Offset: 0x00154D13
		public ReadOnlyDictionaryValueCollection(IReadOnlyDictionary<TKey, TValue> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this.dictionary = dictionary;
		}

		// Token: 0x0600649B RID: 25755 RVA: 0x00156B30 File Offset: 0x00154D30
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<TValue>)this).GetEnumerator();
		}

		// Token: 0x0600649C RID: 25756 RVA: 0x00156B38 File Offset: 0x00154D38
		public IEnumerator<TValue> GetEnumerator()
		{
			return new ReadOnlyDictionaryValueEnumerator<TKey, TValue>(this.dictionary);
		}

		// Token: 0x04002CFD RID: 11517
		private readonly IReadOnlyDictionary<TKey, TValue> dictionary;
	}
}
