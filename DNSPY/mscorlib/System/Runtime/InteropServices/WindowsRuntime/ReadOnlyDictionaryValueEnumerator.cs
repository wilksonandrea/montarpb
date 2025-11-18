using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009EA RID: 2538
	[Serializable]
	internal sealed class ReadOnlyDictionaryValueEnumerator<TKey, TValue> : IEnumerator<TValue>, IDisposable, IEnumerator
	{
		// Token: 0x0600649D RID: 25757 RVA: 0x00156B45 File Offset: 0x00154D45
		public ReadOnlyDictionaryValueEnumerator(IReadOnlyDictionary<TKey, TValue> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this.dictionary = dictionary;
			this.enumeration = dictionary.GetEnumerator();
		}

		// Token: 0x0600649E RID: 25758 RVA: 0x00156B6E File Offset: 0x00154D6E
		void IDisposable.Dispose()
		{
			this.enumeration.Dispose();
		}

		// Token: 0x0600649F RID: 25759 RVA: 0x00156B7B File Offset: 0x00154D7B
		public bool MoveNext()
		{
			return this.enumeration.MoveNext();
		}

		// Token: 0x17001155 RID: 4437
		// (get) Token: 0x060064A0 RID: 25760 RVA: 0x00156B88 File Offset: 0x00154D88
		object IEnumerator.Current
		{
			get
			{
				return ((IEnumerator<TValue>)this).Current;
			}
		}

		// Token: 0x17001156 RID: 4438
		// (get) Token: 0x060064A1 RID: 25761 RVA: 0x00156B98 File Offset: 0x00154D98
		public TValue Current
		{
			get
			{
				KeyValuePair<TKey, TValue> keyValuePair = this.enumeration.Current;
				return keyValuePair.Value;
			}
		}

		// Token: 0x060064A2 RID: 25762 RVA: 0x00156BB8 File Offset: 0x00154DB8
		public void Reset()
		{
			this.enumeration = this.dictionary.GetEnumerator();
		}

		// Token: 0x04002CFE RID: 11518
		private readonly IReadOnlyDictionary<TKey, TValue> dictionary;

		// Token: 0x04002CFF RID: 11519
		private IEnumerator<KeyValuePair<TKey, TValue>> enumeration;
	}
}
