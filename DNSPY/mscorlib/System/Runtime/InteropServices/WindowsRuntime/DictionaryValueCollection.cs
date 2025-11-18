using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009D1 RID: 2513
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	internal sealed class DictionaryValueCollection<TKey, TValue> : ICollection<TValue>, IEnumerable<TValue>, IEnumerable
	{
		// Token: 0x060063F5 RID: 25589 RVA: 0x00154C7B File Offset: 0x00152E7B
		public DictionaryValueCollection(IDictionary<TKey, TValue> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this.dictionary = dictionary;
		}

		// Token: 0x060063F6 RID: 25590 RVA: 0x00154C98 File Offset: 0x00152E98
		public void CopyTo(TValue[] array, int index)
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
				array[num++] = keyValuePair.Value;
			}
		}

		// Token: 0x17001149 RID: 4425
		// (get) Token: 0x060063F7 RID: 25591 RVA: 0x00154D50 File Offset: 0x00152F50
		public int Count
		{
			get
			{
				return this.dictionary.Count;
			}
		}

		// Token: 0x1700114A RID: 4426
		// (get) Token: 0x060063F8 RID: 25592 RVA: 0x00154D5D File Offset: 0x00152F5D
		bool ICollection<TValue>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060063F9 RID: 25593 RVA: 0x00154D60 File Offset: 0x00152F60
		void ICollection<TValue>.Add(TValue item)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_ValueCollectionSet"));
		}

		// Token: 0x060063FA RID: 25594 RVA: 0x00154D71 File Offset: 0x00152F71
		void ICollection<TValue>.Clear()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_ValueCollectionSet"));
		}

		// Token: 0x060063FB RID: 25595 RVA: 0x00154D84 File Offset: 0x00152F84
		public bool Contains(TValue item)
		{
			EqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
			foreach (TValue tvalue in this)
			{
				if (@default.Equals(item, tvalue))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060063FC RID: 25596 RVA: 0x00154DDC File Offset: 0x00152FDC
		bool ICollection<TValue>.Remove(TValue item)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_ValueCollectionSet"));
		}

		// Token: 0x060063FD RID: 25597 RVA: 0x00154DED File Offset: 0x00152FED
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<TValue>)this).GetEnumerator();
		}

		// Token: 0x060063FE RID: 25598 RVA: 0x00154DF5 File Offset: 0x00152FF5
		public IEnumerator<TValue> GetEnumerator()
		{
			return new DictionaryValueEnumerator<TKey, TValue>(this.dictionary);
		}

		// Token: 0x04002CF0 RID: 11504
		private readonly IDictionary<TKey, TValue> dictionary;
	}
}
