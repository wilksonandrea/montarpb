using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009E8 RID: 2536
	[Serializable]
	internal sealed class ReadOnlyDictionaryKeyEnumerator<TKey, TValue> : IEnumerator<TKey>, IDisposable, IEnumerator
	{
		// Token: 0x06006494 RID: 25748 RVA: 0x00156A8E File Offset: 0x00154C8E
		public ReadOnlyDictionaryKeyEnumerator(IReadOnlyDictionary<TKey, TValue> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this.dictionary = dictionary;
			this.enumeration = dictionary.GetEnumerator();
		}

		// Token: 0x06006495 RID: 25749 RVA: 0x00156AB7 File Offset: 0x00154CB7
		void IDisposable.Dispose()
		{
			this.enumeration.Dispose();
		}

		// Token: 0x06006496 RID: 25750 RVA: 0x00156AC4 File Offset: 0x00154CC4
		public bool MoveNext()
		{
			return this.enumeration.MoveNext();
		}

		// Token: 0x17001153 RID: 4435
		// (get) Token: 0x06006497 RID: 25751 RVA: 0x00156AD1 File Offset: 0x00154CD1
		object IEnumerator.Current
		{
			get
			{
				return ((IEnumerator<TKey>)this).Current;
			}
		}

		// Token: 0x17001154 RID: 4436
		// (get) Token: 0x06006498 RID: 25752 RVA: 0x00156AE0 File Offset: 0x00154CE0
		public TKey Current
		{
			get
			{
				KeyValuePair<TKey, TValue> keyValuePair = this.enumeration.Current;
				return keyValuePair.Key;
			}
		}

		// Token: 0x06006499 RID: 25753 RVA: 0x00156B00 File Offset: 0x00154D00
		public void Reset()
		{
			this.enumeration = this.dictionary.GetEnumerator();
		}

		// Token: 0x04002CFB RID: 11515
		private readonly IReadOnlyDictionary<TKey, TValue> dictionary;

		// Token: 0x04002CFC RID: 11516
		private IEnumerator<KeyValuePair<TKey, TValue>> enumeration;
	}
}
