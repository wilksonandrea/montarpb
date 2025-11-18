using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009D0 RID: 2512
	[Serializable]
	internal sealed class DictionaryKeyEnumerator<TKey, TValue> : IEnumerator<TKey>, IDisposable, IEnumerator
	{
		// Token: 0x060063EF RID: 25583 RVA: 0x00154BF6 File Offset: 0x00152DF6
		public DictionaryKeyEnumerator(IDictionary<TKey, TValue> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this.dictionary = dictionary;
			this.enumeration = dictionary.GetEnumerator();
		}

		// Token: 0x060063F0 RID: 25584 RVA: 0x00154C1F File Offset: 0x00152E1F
		void IDisposable.Dispose()
		{
			this.enumeration.Dispose();
		}

		// Token: 0x060063F1 RID: 25585 RVA: 0x00154C2C File Offset: 0x00152E2C
		public bool MoveNext()
		{
			return this.enumeration.MoveNext();
		}

		// Token: 0x17001147 RID: 4423
		// (get) Token: 0x060063F2 RID: 25586 RVA: 0x00154C39 File Offset: 0x00152E39
		object IEnumerator.Current
		{
			get
			{
				return ((IEnumerator<TKey>)this).Current;
			}
		}

		// Token: 0x17001148 RID: 4424
		// (get) Token: 0x060063F3 RID: 25587 RVA: 0x00154C48 File Offset: 0x00152E48
		public TKey Current
		{
			get
			{
				KeyValuePair<TKey, TValue> keyValuePair = this.enumeration.Current;
				return keyValuePair.Key;
			}
		}

		// Token: 0x060063F4 RID: 25588 RVA: 0x00154C68 File Offset: 0x00152E68
		public void Reset()
		{
			this.enumeration = this.dictionary.GetEnumerator();
		}

		// Token: 0x04002CEE RID: 11502
		private readonly IDictionary<TKey, TValue> dictionary;

		// Token: 0x04002CEF RID: 11503
		private IEnumerator<KeyValuePair<TKey, TValue>> enumeration;
	}
}
