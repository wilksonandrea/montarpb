using System;

namespace System.Runtime.Serialization
{
	// Token: 0x02000752 RID: 1874
	internal class ObjectHolderList
	{
		// Token: 0x060052C7 RID: 21191 RVA: 0x00122BF6 File Offset: 0x00120DF6
		internal ObjectHolderList()
			: this(8)
		{
		}

		// Token: 0x060052C8 RID: 21192 RVA: 0x00122BFF File Offset: 0x00120DFF
		internal ObjectHolderList(int startingSize)
		{
			this.m_count = 0;
			this.m_values = new ObjectHolder[startingSize];
		}

		// Token: 0x060052C9 RID: 21193 RVA: 0x00122C1C File Offset: 0x00120E1C
		internal virtual void Add(ObjectHolder value)
		{
			if (this.m_count == this.m_values.Length)
			{
				this.EnlargeArray();
			}
			ObjectHolder[] values = this.m_values;
			int count = this.m_count;
			this.m_count = count + 1;
			values[count] = value;
		}

		// Token: 0x060052CA RID: 21194 RVA: 0x00122C58 File Offset: 0x00120E58
		internal ObjectHolderListEnumerator GetFixupEnumerator()
		{
			return new ObjectHolderListEnumerator(this, true);
		}

		// Token: 0x060052CB RID: 21195 RVA: 0x00122C64 File Offset: 0x00120E64
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
			ObjectHolder[] array = new ObjectHolder[num];
			Array.Copy(this.m_values, array, this.m_count);
			this.m_values = array;
		}

		// Token: 0x17000DAD RID: 3501
		// (get) Token: 0x060052CC RID: 21196 RVA: 0x00122CBE File Offset: 0x00120EBE
		internal int Version
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x060052CD RID: 21197 RVA: 0x00122CC6 File Offset: 0x00120EC6
		internal int Count
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x040024B5 RID: 9397
		internal const int DefaultInitialSize = 8;

		// Token: 0x040024B6 RID: 9398
		internal ObjectHolder[] m_values;

		// Token: 0x040024B7 RID: 9399
		internal int m_count;
	}
}
