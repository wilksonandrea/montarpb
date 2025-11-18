using System;
using System.Collections;
using System.Threading;

namespace System.Reflection
{
	// Token: 0x020005AF RID: 1455
	internal struct CerHashtable<K, V> where K : class
	{
		// Token: 0x0600436A RID: 17258 RVA: 0x000FA2F0 File Offset: 0x000F84F0
		private static int GetHashCodeHelper(K key)
		{
			string text = key as string;
			if (text == null)
			{
				return key.GetHashCode();
			}
			return text.GetLegacyNonRandomizedHashCode();
		}

		// Token: 0x0600436B RID: 17259 RVA: 0x000FA320 File Offset: 0x000F8520
		private void Rehash(int newSize)
		{
			CerHashtable<K, V>.Table table = new CerHashtable<K, V>.Table(newSize);
			CerHashtable<K, V>.Table table2 = this.m_Table;
			if (table2 != null)
			{
				K[] keys = table2.m_keys;
				V[] values = table2.m_values;
				for (int i = 0; i < keys.Length; i++)
				{
					K k = keys[i];
					if (k != null)
					{
						table.Insert(k, values[i]);
					}
				}
			}
			Volatile.Write<CerHashtable<K, V>.Table>(ref this.m_Table, table);
		}

		// Token: 0x170009E8 RID: 2536
		internal V this[K key]
		{
			get
			{
				CerHashtable<K, V>.Table table = Volatile.Read<CerHashtable<K, V>.Table>(ref this.m_Table);
				if (table == null)
				{
					return default(V);
				}
				int num = CerHashtable<K, V>.GetHashCodeHelper(key);
				if (num < 0)
				{
					num = ~num;
				}
				K[] keys = table.m_keys;
				int num2 = num % keys.Length;
				for (;;)
				{
					K k = Volatile.Read<K>(ref keys[num2]);
					if (k == null)
					{
						goto IL_7F;
					}
					if (k.Equals(key))
					{
						break;
					}
					num2++;
					if (num2 >= keys.Length)
					{
						num2 -= keys.Length;
					}
				}
				return table.m_values[num2];
				IL_7F:
				return default(V);
			}
			set
			{
				CerHashtable<K, V>.Table table = this.m_Table;
				if (table != null)
				{
					int num = 2 * (table.m_count + 1);
					if (num >= table.m_keys.Length)
					{
						this.Rehash(num);
					}
				}
				else
				{
					this.Rehash(7);
				}
				this.m_Table.Insert(key, value);
			}
		}

		// Token: 0x04001BF6 RID: 7158
		private CerHashtable<K, V>.Table m_Table;

		// Token: 0x04001BF7 RID: 7159
		private const int MinSize = 7;

		// Token: 0x02000C39 RID: 3129
		private class Table
		{
			// Token: 0x06007041 RID: 28737 RVA: 0x00182B40 File Offset: 0x00180D40
			internal Table(int size)
			{
				size = HashHelpers.GetPrime(size);
				this.m_keys = new K[size];
				this.m_values = new V[size];
			}

			// Token: 0x06007042 RID: 28738 RVA: 0x00182B68 File Offset: 0x00180D68
			internal void Insert(K key, V value)
			{
				int num = CerHashtable<K, V>.GetHashCodeHelper(key);
				if (num < 0)
				{
					num = ~num;
				}
				K[] keys = this.m_keys;
				int num2 = num % keys.Length;
				for (;;)
				{
					K k = keys[num2];
					if (k == null)
					{
						break;
					}
					num2++;
					if (num2 >= keys.Length)
					{
						num2 -= keys.Length;
					}
				}
				this.m_count++;
				this.m_values[num2] = value;
				Volatile.Write<K>(ref keys[num2], key);
			}

			// Token: 0x04003734 RID: 14132
			internal K[] m_keys;

			// Token: 0x04003735 RID: 14133
			internal V[] m_values;

			// Token: 0x04003736 RID: 14134
			internal int m_count;
		}
	}
}
