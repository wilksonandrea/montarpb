using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009CF RID: 2511
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	internal sealed class DictionaryKeyCollection<TKey, TValue> : ICollection<TKey>, IEnumerable<TKey>, IEnumerable
	{
		// Token: 0x060063E5 RID: 25573 RVA: 0x00154ABA File Offset: 0x00152CBA
		public DictionaryKeyCollection(IDictionary<TKey, TValue> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this.dictionary = dictionary;
		}

		// Token: 0x060063E6 RID: 25574 RVA: 0x00154AD8 File Offset: 0x00152CD8
		public void CopyTo(TKey[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (array.Length <= index && this.Count > 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_IndexOutOfRangeException"));
			}
			if (array.Length - index < this.dictionary.Count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InsufficientSpaceToCopyCollection"));
			}
			int num = index;
			foreach (KeyValuePair<TKey, TValue> keyValuePair in this.dictionary)
			{
				array[num++] = keyValuePair.Key;
			}
		}

		// Token: 0x17001145 RID: 4421
		// (get) Token: 0x060063E7 RID: 25575 RVA: 0x00154B90 File Offset: 0x00152D90
		public int Count
		{
			get
			{
				return this.dictionary.Count;
			}
		}

		// Token: 0x17001146 RID: 4422
		// (get) Token: 0x060063E8 RID: 25576 RVA: 0x00154B9D File Offset: 0x00152D9D
		bool ICollection<TKey>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060063E9 RID: 25577 RVA: 0x00154BA0 File Offset: 0x00152DA0
		void ICollection<TKey>.Add(TKey item)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_KeyCollectionSet"));
		}

		// Token: 0x060063EA RID: 25578 RVA: 0x00154BB1 File Offset: 0x00152DB1
		void ICollection<TKey>.Clear()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_KeyCollectionSet"));
		}

		// Token: 0x060063EB RID: 25579 RVA: 0x00154BC2 File Offset: 0x00152DC2
		public bool Contains(TKey item)
		{
			return this.dictionary.ContainsKey(item);
		}

		// Token: 0x060063EC RID: 25580 RVA: 0x00154BD0 File Offset: 0x00152DD0
		bool ICollection<TKey>.Remove(TKey item)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_KeyCollectionSet"));
		}

		// Token: 0x060063ED RID: 25581 RVA: 0x00154BE1 File Offset: 0x00152DE1
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<TKey>)this).GetEnumerator();
		}

		// Token: 0x060063EE RID: 25582 RVA: 0x00154BE9 File Offset: 0x00152DE9
		public IEnumerator<TKey> GetEnumerator()
		{
			return new DictionaryKeyEnumerator<TKey, TValue>(this.dictionary);
		}

		// Token: 0x04002CED RID: 11501
		private readonly IDictionary<TKey, TValue> dictionary;
	}
}
