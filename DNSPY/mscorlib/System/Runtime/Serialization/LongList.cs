using System;

namespace System.Runtime.Serialization
{
	// Token: 0x02000751 RID: 1873
	[Serializable]
	internal class LongList
	{
		// Token: 0x060052BE RID: 21182 RVA: 0x00122A7A File Offset: 0x00120C7A
		internal LongList()
			: this(2)
		{
		}

		// Token: 0x060052BF RID: 21183 RVA: 0x00122A83 File Offset: 0x00120C83
		internal LongList(int startingSize)
		{
			this.m_count = 0;
			this.m_totalItems = 0;
			this.m_values = new long[startingSize];
		}

		// Token: 0x060052C0 RID: 21184 RVA: 0x00122AA8 File Offset: 0x00120CA8
		internal void Add(long value)
		{
			if (this.m_totalItems == this.m_values.Length)
			{
				this.EnlargeArray();
			}
			long[] values = this.m_values;
			int totalItems = this.m_totalItems;
			this.m_totalItems = totalItems + 1;
			values[totalItems] = value;
			this.m_count++;
		}

		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x060052C1 RID: 21185 RVA: 0x00122AF2 File Offset: 0x00120CF2
		internal int Count
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x060052C2 RID: 21186 RVA: 0x00122AFA File Offset: 0x00120CFA
		internal void StartEnumeration()
		{
			this.m_currentItem = -1;
		}

		// Token: 0x060052C3 RID: 21187 RVA: 0x00122B04 File Offset: 0x00120D04
		internal bool MoveNext()
		{
			int num;
			do
			{
				num = this.m_currentItem + 1;
				this.m_currentItem = num;
			}
			while (num < this.m_totalItems && this.m_values[this.m_currentItem] == -1L);
			return this.m_currentItem != this.m_totalItems;
		}

		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x060052C4 RID: 21188 RVA: 0x00122B4C File Offset: 0x00120D4C
		internal long Current
		{
			get
			{
				return this.m_values[this.m_currentItem];
			}
		}

		// Token: 0x060052C5 RID: 21189 RVA: 0x00122B5C File Offset: 0x00120D5C
		internal bool RemoveElement(long value)
		{
			int num = 0;
			while (num < this.m_totalItems && this.m_values[num] != value)
			{
				num++;
			}
			if (num == this.m_totalItems)
			{
				return false;
			}
			this.m_values[num] = -1L;
			return true;
		}

		// Token: 0x060052C6 RID: 21190 RVA: 0x00122B9C File Offset: 0x00120D9C
		private void EnlargeArray()
		{
			int num = this.m_values.Length * 2;
			if (num < 0)
			{
				if (num == 2147483647)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_TooManyElements"));
				}
				num = int.MaxValue;
			}
			long[] array = new long[num];
			Array.Copy(this.m_values, array, this.m_count);
			this.m_values = array;
		}

		// Token: 0x040024B0 RID: 9392
		private const int InitialSize = 2;

		// Token: 0x040024B1 RID: 9393
		private long[] m_values;

		// Token: 0x040024B2 RID: 9394
		private int m_count;

		// Token: 0x040024B3 RID: 9395
		private int m_totalItems;

		// Token: 0x040024B4 RID: 9396
		private int m_currentItem;
	}
}
