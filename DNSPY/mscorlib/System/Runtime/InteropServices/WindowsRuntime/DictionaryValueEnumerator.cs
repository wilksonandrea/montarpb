using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009D2 RID: 2514
	[Serializable]
	internal sealed class DictionaryValueEnumerator<TKey, TValue> : IEnumerator<TValue>, IDisposable, IEnumerator
	{
		// Token: 0x060063FF RID: 25599 RVA: 0x00154E02 File Offset: 0x00153002
		public DictionaryValueEnumerator(IDictionary<TKey, TValue> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this.dictionary = dictionary;
			this.enumeration = dictionary.GetEnumerator();
		}

		// Token: 0x06006400 RID: 25600 RVA: 0x00154E2B File Offset: 0x0015302B
		void IDisposable.Dispose()
		{
			this.enumeration.Dispose();
		}

		// Token: 0x06006401 RID: 25601 RVA: 0x00154E38 File Offset: 0x00153038
		public bool MoveNext()
		{
			return this.enumeration.MoveNext();
		}

		// Token: 0x1700114B RID: 4427
		// (get) Token: 0x06006402 RID: 25602 RVA: 0x00154E45 File Offset: 0x00153045
		object IEnumerator.Current
		{
			get
			{
				return ((IEnumerator<TValue>)this).Current;
			}
		}

		// Token: 0x1700114C RID: 4428
		// (get) Token: 0x06006403 RID: 25603 RVA: 0x00154E54 File Offset: 0x00153054
		public TValue Current
		{
			get
			{
				KeyValuePair<TKey, TValue> keyValuePair = this.enumeration.Current;
				return keyValuePair.Value;
			}
		}

		// Token: 0x06006404 RID: 25604 RVA: 0x00154E74 File Offset: 0x00153074
		public void Reset()
		{
			this.enumeration = this.dictionary.GetEnumerator();
		}

		// Token: 0x04002CF1 RID: 11505
		private readonly IDictionary<TKey, TValue> dictionary;

		// Token: 0x04002CF2 RID: 11506
		private IEnumerator<KeyValuePair<TKey, TValue>> enumeration;
	}
}
