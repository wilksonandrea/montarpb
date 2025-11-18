using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace System.Collections.ObjectModel
{
	// Token: 0x020004B7 RID: 1207
	[DebuggerTypeProxy(typeof(Mscorlib_DictionaryDebugView<, >))]
	[DebuggerDisplay("Count = {Count}")]
	[__DynamicallyInvokable]
	[Serializable]
	public class ReadOnlyDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>
	{
		// Token: 0x060039EE RID: 14830 RVA: 0x000DD0D0 File Offset: 0x000DB2D0
		[__DynamicallyInvokable]
		public ReadOnlyDictionary(IDictionary<TKey, TValue> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this.m_dictionary = dictionary;
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x060039EF RID: 14831 RVA: 0x000DD0ED File Offset: 0x000DB2ED
		[__DynamicallyInvokable]
		protected IDictionary<TKey, TValue> Dictionary
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_dictionary;
			}
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x060039F0 RID: 14832 RVA: 0x000DD0F5 File Offset: 0x000DB2F5
		[__DynamicallyInvokable]
		public ReadOnlyDictionary<TKey, TValue>.KeyCollection Keys
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_keys == null)
				{
					this.m_keys = new ReadOnlyDictionary<TKey, TValue>.KeyCollection(this.m_dictionary.Keys);
				}
				return this.m_keys;
			}
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x060039F1 RID: 14833 RVA: 0x000DD11B File Offset: 0x000DB31B
		[__DynamicallyInvokable]
		public ReadOnlyDictionary<TKey, TValue>.ValueCollection Values
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_values == null)
				{
					this.m_values = new ReadOnlyDictionary<TKey, TValue>.ValueCollection(this.m_dictionary.Values);
				}
				return this.m_values;
			}
		}

		// Token: 0x060039F2 RID: 14834 RVA: 0x000DD141 File Offset: 0x000DB341
		[__DynamicallyInvokable]
		public bool ContainsKey(TKey key)
		{
			return this.m_dictionary.ContainsKey(key);
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x060039F3 RID: 14835 RVA: 0x000DD14F File Offset: 0x000DB34F
		[__DynamicallyInvokable]
		ICollection<TKey> IDictionary<TKey, TValue>.Keys
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Keys;
			}
		}

		// Token: 0x060039F4 RID: 14836 RVA: 0x000DD157 File Offset: 0x000DB357
		[__DynamicallyInvokable]
		public bool TryGetValue(TKey key, out TValue value)
		{
			return this.m_dictionary.TryGetValue(key, out value);
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x060039F5 RID: 14837 RVA: 0x000DD166 File Offset: 0x000DB366
		[__DynamicallyInvokable]
		ICollection<TValue> IDictionary<TKey, TValue>.Values
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Values;
			}
		}

		// Token: 0x170008BE RID: 2238
		[__DynamicallyInvokable]
		public TValue this[TKey key]
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_dictionary[key];
			}
		}

		// Token: 0x060039F7 RID: 14839 RVA: 0x000DD17C File Offset: 0x000DB37C
		[__DynamicallyInvokable]
		void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x060039F8 RID: 14840 RVA: 0x000DD185 File Offset: 0x000DB385
		[__DynamicallyInvokable]
		bool IDictionary<TKey, TValue>.Remove(TKey key)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			return false;
		}

		// Token: 0x170008BF RID: 2239
		[__DynamicallyInvokable]
		TValue IDictionary<TKey, TValue>.this[TKey key]
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_dictionary[key];
			}
			[__DynamicallyInvokable]
			set
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x060039FB RID: 14843 RVA: 0x000DD1A6 File Offset: 0x000DB3A6
		[__DynamicallyInvokable]
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_dictionary.Count;
			}
		}

		// Token: 0x060039FC RID: 14844 RVA: 0x000DD1B3 File Offset: 0x000DB3B3
		[__DynamicallyInvokable]
		bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
		{
			return this.m_dictionary.Contains(item);
		}

		// Token: 0x060039FD RID: 14845 RVA: 0x000DD1C1 File Offset: 0x000DB3C1
		[__DynamicallyInvokable]
		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			this.m_dictionary.CopyTo(array, arrayIndex);
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x060039FE RID: 14846 RVA: 0x000DD1D0 File Offset: 0x000DB3D0
		[__DynamicallyInvokable]
		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return true;
			}
		}

		// Token: 0x060039FF RID: 14847 RVA: 0x000DD1D3 File Offset: 0x000DB3D3
		[__DynamicallyInvokable]
		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x06003A00 RID: 14848 RVA: 0x000DD1DC File Offset: 0x000DB3DC
		[__DynamicallyInvokable]
		void ICollection<KeyValuePair<TKey, TValue>>.Clear()
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x06003A01 RID: 14849 RVA: 0x000DD1E5 File Offset: 0x000DB3E5
		[__DynamicallyInvokable]
		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			return false;
		}

		// Token: 0x06003A02 RID: 14850 RVA: 0x000DD1EF File Offset: 0x000DB3EF
		[__DynamicallyInvokable]
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return this.m_dictionary.GetEnumerator();
		}

		// Token: 0x06003A03 RID: 14851 RVA: 0x000DD1FC File Offset: 0x000DB3FC
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.m_dictionary.GetEnumerator();
		}

		// Token: 0x06003A04 RID: 14852 RVA: 0x000DD209 File Offset: 0x000DB409
		private static bool IsCompatibleKey(object key)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			return key is TKey;
		}

		// Token: 0x06003A05 RID: 14853 RVA: 0x000DD21D File Offset: 0x000DB41D
		[__DynamicallyInvokable]
		void IDictionary.Add(object key, object value)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x06003A06 RID: 14854 RVA: 0x000DD226 File Offset: 0x000DB426
		[__DynamicallyInvokable]
		void IDictionary.Clear()
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x06003A07 RID: 14855 RVA: 0x000DD22F File Offset: 0x000DB42F
		[__DynamicallyInvokable]
		bool IDictionary.Contains(object key)
		{
			return ReadOnlyDictionary<TKey, TValue>.IsCompatibleKey(key) && this.ContainsKey((TKey)((object)key));
		}

		// Token: 0x06003A08 RID: 14856 RVA: 0x000DD248 File Offset: 0x000DB448
		[__DynamicallyInvokable]
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			IDictionary dictionary = this.m_dictionary as IDictionary;
			if (dictionary != null)
			{
				return dictionary.GetEnumerator();
			}
			return new ReadOnlyDictionary<TKey, TValue>.DictionaryEnumerator(this.m_dictionary);
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06003A09 RID: 14857 RVA: 0x000DD27B File Offset: 0x000DB47B
		[__DynamicallyInvokable]
		bool IDictionary.IsFixedSize
		{
			[__DynamicallyInvokable]
			get
			{
				return true;
			}
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06003A0A RID: 14858 RVA: 0x000DD27E File Offset: 0x000DB47E
		[__DynamicallyInvokable]
		bool IDictionary.IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return true;
			}
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06003A0B RID: 14859 RVA: 0x000DD281 File Offset: 0x000DB481
		[__DynamicallyInvokable]
		ICollection IDictionary.Keys
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Keys;
			}
		}

		// Token: 0x06003A0C RID: 14860 RVA: 0x000DD289 File Offset: 0x000DB489
		[__DynamicallyInvokable]
		void IDictionary.Remove(object key)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06003A0D RID: 14861 RVA: 0x000DD292 File Offset: 0x000DB492
		[__DynamicallyInvokable]
		ICollection IDictionary.Values
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Values;
			}
		}

		// Token: 0x170008C6 RID: 2246
		[__DynamicallyInvokable]
		object IDictionary.this[object key]
		{
			[__DynamicallyInvokable]
			get
			{
				if (ReadOnlyDictionary<TKey, TValue>.IsCompatibleKey(key))
				{
					return this[(TKey)((object)key)];
				}
				return null;
			}
			[__DynamicallyInvokable]
			set
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
		}

		// Token: 0x06003A10 RID: 14864 RVA: 0x000DD2C0 File Offset: 0x000DB4C0
		[__DynamicallyInvokable]
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (array.Rank != 1)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
			}
			if (array.GetLowerBound(0) != 0)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
			}
			if (index < 0 || index > array.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (array.Length - index < this.Count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
			}
			KeyValuePair<TKey, TValue>[] array2 = array as KeyValuePair<TKey, TValue>[];
			if (array2 != null)
			{
				this.m_dictionary.CopyTo(array2, index);
				return;
			}
			DictionaryEntry[] array3 = array as DictionaryEntry[];
			if (array3 != null)
			{
				using (IEnumerator<KeyValuePair<TKey, TValue>> enumerator = this.m_dictionary.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<TKey, TValue> keyValuePair = enumerator.Current;
						array3[index++] = new DictionaryEntry(keyValuePair.Key, keyValuePair.Value);
					}
					return;
				}
			}
			object[] array4 = array as object[];
			if (array4 == null)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
			}
			try
			{
				foreach (KeyValuePair<TKey, TValue> keyValuePair2 in this.m_dictionary)
				{
					array4[index++] = new KeyValuePair<TKey, TValue>(keyValuePair2.Key, keyValuePair2.Value);
				}
			}
			catch (ArrayTypeMismatchException)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
			}
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06003A11 RID: 14865 RVA: 0x000DD42C File Offset: 0x000DB62C
		[__DynamicallyInvokable]
		bool ICollection.IsSynchronized
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06003A12 RID: 14866 RVA: 0x000DD430 File Offset: 0x000DB630
		[__DynamicallyInvokable]
		object ICollection.SyncRoot
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_syncRoot == null)
				{
					ICollection collection = this.m_dictionary as ICollection;
					if (collection != null)
					{
						this.m_syncRoot = collection.SyncRoot;
					}
					else
					{
						Interlocked.CompareExchange<object>(ref this.m_syncRoot, new object(), null);
					}
				}
				return this.m_syncRoot;
			}
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06003A13 RID: 14867 RVA: 0x000DD47A File Offset: 0x000DB67A
		[__DynamicallyInvokable]
		IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Keys;
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06003A14 RID: 14868 RVA: 0x000DD482 File Offset: 0x000DB682
		[__DynamicallyInvokable]
		IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Values;
			}
		}

		// Token: 0x04001938 RID: 6456
		private readonly IDictionary<TKey, TValue> m_dictionary;

		// Token: 0x04001939 RID: 6457
		[NonSerialized]
		private object m_syncRoot;

		// Token: 0x0400193A RID: 6458
		[NonSerialized]
		private ReadOnlyDictionary<TKey, TValue>.KeyCollection m_keys;

		// Token: 0x0400193B RID: 6459
		[NonSerialized]
		private ReadOnlyDictionary<TKey, TValue>.ValueCollection m_values;

		// Token: 0x02000BDF RID: 3039
		[Serializable]
		private struct DictionaryEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06006EE9 RID: 28393 RVA: 0x0017E0FB File Offset: 0x0017C2FB
			public DictionaryEnumerator(IDictionary<TKey, TValue> dictionary)
			{
				this.m_dictionary = dictionary;
				this.m_enumerator = this.m_dictionary.GetEnumerator();
			}

			// Token: 0x170012FD RID: 4861
			// (get) Token: 0x06006EEA RID: 28394 RVA: 0x0017E118 File Offset: 0x0017C318
			public DictionaryEntry Entry
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this.m_enumerator.Current;
					object obj = keyValuePair.Key;
					keyValuePair = this.m_enumerator.Current;
					return new DictionaryEntry(obj, keyValuePair.Value);
				}
			}

			// Token: 0x170012FE RID: 4862
			// (get) Token: 0x06006EEB RID: 28395 RVA: 0x0017E15C File Offset: 0x0017C35C
			public object Key
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this.m_enumerator.Current;
					return keyValuePair.Key;
				}
			}

			// Token: 0x170012FF RID: 4863
			// (get) Token: 0x06006EEC RID: 28396 RVA: 0x0017E184 File Offset: 0x0017C384
			public object Value
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this.m_enumerator.Current;
					return keyValuePair.Value;
				}
			}

			// Token: 0x17001300 RID: 4864
			// (get) Token: 0x06006EED RID: 28397 RVA: 0x0017E1A9 File Offset: 0x0017C3A9
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x06006EEE RID: 28398 RVA: 0x0017E1B6 File Offset: 0x0017C3B6
			public bool MoveNext()
			{
				return this.m_enumerator.MoveNext();
			}

			// Token: 0x06006EEF RID: 28399 RVA: 0x0017E1C3 File Offset: 0x0017C3C3
			public void Reset()
			{
				this.m_enumerator.Reset();
			}

			// Token: 0x040035EE RID: 13806
			private readonly IDictionary<TKey, TValue> m_dictionary;

			// Token: 0x040035EF RID: 13807
			private IEnumerator<KeyValuePair<TKey, TValue>> m_enumerator;
		}

		// Token: 0x02000BE0 RID: 3040
		[DebuggerTypeProxy(typeof(Mscorlib_CollectionDebugView<>))]
		[DebuggerDisplay("Count = {Count}")]
		[__DynamicallyInvokable]
		[Serializable]
		public sealed class KeyCollection : ICollection<TKey>, IEnumerable<TKey>, IEnumerable, ICollection, IReadOnlyCollection<TKey>
		{
			// Token: 0x06006EF0 RID: 28400 RVA: 0x0017E1D0 File Offset: 0x0017C3D0
			internal KeyCollection(ICollection<TKey> collection)
			{
				if (collection == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
				}
				this.m_collection = collection;
			}

			// Token: 0x06006EF1 RID: 28401 RVA: 0x0017E1E8 File Offset: 0x0017C3E8
			[__DynamicallyInvokable]
			void ICollection<TKey>.Add(TKey item)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}

			// Token: 0x06006EF2 RID: 28402 RVA: 0x0017E1F1 File Offset: 0x0017C3F1
			[__DynamicallyInvokable]
			void ICollection<TKey>.Clear()
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}

			// Token: 0x06006EF3 RID: 28403 RVA: 0x0017E1FA File Offset: 0x0017C3FA
			[__DynamicallyInvokable]
			bool ICollection<TKey>.Contains(TKey item)
			{
				return this.m_collection.Contains(item);
			}

			// Token: 0x06006EF4 RID: 28404 RVA: 0x0017E208 File Offset: 0x0017C408
			[__DynamicallyInvokable]
			public void CopyTo(TKey[] array, int arrayIndex)
			{
				this.m_collection.CopyTo(array, arrayIndex);
			}

			// Token: 0x17001301 RID: 4865
			// (get) Token: 0x06006EF5 RID: 28405 RVA: 0x0017E217 File Offset: 0x0017C417
			[__DynamicallyInvokable]
			public int Count
			{
				[__DynamicallyInvokable]
				get
				{
					return this.m_collection.Count;
				}
			}

			// Token: 0x17001302 RID: 4866
			// (get) Token: 0x06006EF6 RID: 28406 RVA: 0x0017E224 File Offset: 0x0017C424
			[__DynamicallyInvokable]
			bool ICollection<TKey>.IsReadOnly
			{
				[__DynamicallyInvokable]
				get
				{
					return true;
				}
			}

			// Token: 0x06006EF7 RID: 28407 RVA: 0x0017E227 File Offset: 0x0017C427
			[__DynamicallyInvokable]
			bool ICollection<TKey>.Remove(TKey item)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
				return false;
			}

			// Token: 0x06006EF8 RID: 28408 RVA: 0x0017E231 File Offset: 0x0017C431
			[__DynamicallyInvokable]
			public IEnumerator<TKey> GetEnumerator()
			{
				return this.m_collection.GetEnumerator();
			}

			// Token: 0x06006EF9 RID: 28409 RVA: 0x0017E23E File Offset: 0x0017C43E
			[__DynamicallyInvokable]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.m_collection.GetEnumerator();
			}

			// Token: 0x06006EFA RID: 28410 RVA: 0x0017E24B File Offset: 0x0017C44B
			[__DynamicallyInvokable]
			void ICollection.CopyTo(Array array, int index)
			{
				ReadOnlyDictionaryHelpers.CopyToNonGenericICollectionHelper<TKey>(this.m_collection, array, index);
			}

			// Token: 0x17001303 RID: 4867
			// (get) Token: 0x06006EFB RID: 28411 RVA: 0x0017E25A File Offset: 0x0017C45A
			[__DynamicallyInvokable]
			bool ICollection.IsSynchronized
			{
				[__DynamicallyInvokable]
				get
				{
					return false;
				}
			}

			// Token: 0x17001304 RID: 4868
			// (get) Token: 0x06006EFC RID: 28412 RVA: 0x0017E260 File Offset: 0x0017C460
			[__DynamicallyInvokable]
			object ICollection.SyncRoot
			{
				[__DynamicallyInvokable]
				get
				{
					if (this.m_syncRoot == null)
					{
						ICollection collection = this.m_collection as ICollection;
						if (collection != null)
						{
							this.m_syncRoot = collection.SyncRoot;
						}
						else
						{
							Interlocked.CompareExchange<object>(ref this.m_syncRoot, new object(), null);
						}
					}
					return this.m_syncRoot;
				}
			}

			// Token: 0x040035F0 RID: 13808
			private readonly ICollection<TKey> m_collection;

			// Token: 0x040035F1 RID: 13809
			[NonSerialized]
			private object m_syncRoot;
		}

		// Token: 0x02000BE1 RID: 3041
		[DebuggerTypeProxy(typeof(Mscorlib_CollectionDebugView<>))]
		[DebuggerDisplay("Count = {Count}")]
		[__DynamicallyInvokable]
		[Serializable]
		public sealed class ValueCollection : ICollection<TValue>, IEnumerable<TValue>, IEnumerable, ICollection, IReadOnlyCollection<TValue>
		{
			// Token: 0x06006EFD RID: 28413 RVA: 0x0017E2AA File Offset: 0x0017C4AA
			internal ValueCollection(ICollection<TValue> collection)
			{
				if (collection == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
				}
				this.m_collection = collection;
			}

			// Token: 0x06006EFE RID: 28414 RVA: 0x0017E2C2 File Offset: 0x0017C4C2
			[__DynamicallyInvokable]
			void ICollection<TValue>.Add(TValue item)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}

			// Token: 0x06006EFF RID: 28415 RVA: 0x0017E2CB File Offset: 0x0017C4CB
			[__DynamicallyInvokable]
			void ICollection<TValue>.Clear()
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}

			// Token: 0x06006F00 RID: 28416 RVA: 0x0017E2D4 File Offset: 0x0017C4D4
			[__DynamicallyInvokable]
			bool ICollection<TValue>.Contains(TValue item)
			{
				return this.m_collection.Contains(item);
			}

			// Token: 0x06006F01 RID: 28417 RVA: 0x0017E2E2 File Offset: 0x0017C4E2
			[__DynamicallyInvokable]
			public void CopyTo(TValue[] array, int arrayIndex)
			{
				this.m_collection.CopyTo(array, arrayIndex);
			}

			// Token: 0x17001305 RID: 4869
			// (get) Token: 0x06006F02 RID: 28418 RVA: 0x0017E2F1 File Offset: 0x0017C4F1
			[__DynamicallyInvokable]
			public int Count
			{
				[__DynamicallyInvokable]
				get
				{
					return this.m_collection.Count;
				}
			}

			// Token: 0x17001306 RID: 4870
			// (get) Token: 0x06006F03 RID: 28419 RVA: 0x0017E2FE File Offset: 0x0017C4FE
			[__DynamicallyInvokable]
			bool ICollection<TValue>.IsReadOnly
			{
				[__DynamicallyInvokable]
				get
				{
					return true;
				}
			}

			// Token: 0x06006F04 RID: 28420 RVA: 0x0017E301 File Offset: 0x0017C501
			[__DynamicallyInvokable]
			bool ICollection<TValue>.Remove(TValue item)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
				return false;
			}

			// Token: 0x06006F05 RID: 28421 RVA: 0x0017E30B File Offset: 0x0017C50B
			[__DynamicallyInvokable]
			public IEnumerator<TValue> GetEnumerator()
			{
				return this.m_collection.GetEnumerator();
			}

			// Token: 0x06006F06 RID: 28422 RVA: 0x0017E318 File Offset: 0x0017C518
			[__DynamicallyInvokable]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.m_collection.GetEnumerator();
			}

			// Token: 0x06006F07 RID: 28423 RVA: 0x0017E325 File Offset: 0x0017C525
			[__DynamicallyInvokable]
			void ICollection.CopyTo(Array array, int index)
			{
				ReadOnlyDictionaryHelpers.CopyToNonGenericICollectionHelper<TValue>(this.m_collection, array, index);
			}

			// Token: 0x17001307 RID: 4871
			// (get) Token: 0x06006F08 RID: 28424 RVA: 0x0017E334 File Offset: 0x0017C534
			[__DynamicallyInvokable]
			bool ICollection.IsSynchronized
			{
				[__DynamicallyInvokable]
				get
				{
					return false;
				}
			}

			// Token: 0x17001308 RID: 4872
			// (get) Token: 0x06006F09 RID: 28425 RVA: 0x0017E338 File Offset: 0x0017C538
			[__DynamicallyInvokable]
			object ICollection.SyncRoot
			{
				[__DynamicallyInvokable]
				get
				{
					if (this.m_syncRoot == null)
					{
						ICollection collection = this.m_collection as ICollection;
						if (collection != null)
						{
							this.m_syncRoot = collection.SyncRoot;
						}
						else
						{
							Interlocked.CompareExchange<object>(ref this.m_syncRoot, new object(), null);
						}
					}
					return this.m_syncRoot;
				}
			}

			// Token: 0x040035F2 RID: 13810
			private readonly ICollection<TValue> m_collection;

			// Token: 0x040035F3 RID: 13811
			[NonSerialized]
			private object m_syncRoot;
		}
	}
}
