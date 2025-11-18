using System;

namespace System.Runtime.Serialization
{
	// Token: 0x02000750 RID: 1872
	[Serializable]
	internal class FixupHolderList
	{
		// Token: 0x060052B9 RID: 21177 RVA: 0x0012296B File Offset: 0x00120B6B
		internal FixupHolderList()
			: this(2)
		{
		}

		// Token: 0x060052BA RID: 21178 RVA: 0x00122974 File Offset: 0x00120B74
		internal FixupHolderList(int startingSize)
		{
			this.m_count = 0;
			this.m_values = new FixupHolder[startingSize];
		}

		// Token: 0x060052BB RID: 21179 RVA: 0x00122990 File Offset: 0x00120B90
		internal virtual void Add(long id, object fixupInfo)
		{
			if (this.m_count == this.m_values.Length)
			{
				this.EnlargeArray();
			}
			this.m_values[this.m_count].m_id = id;
			FixupHolder[] values = this.m_values;
			int count = this.m_count;
			this.m_count = count + 1;
			values[count].m_fixupInfo = fixupInfo;
		}

		// Token: 0x060052BC RID: 21180 RVA: 0x001229E4 File Offset: 0x00120BE4
		internal virtual void Add(FixupHolder fixup)
		{
			if (this.m_count == this.m_values.Length)
			{
				this.EnlargeArray();
			}
			FixupHolder[] values = this.m_values;
			int count = this.m_count;
			this.m_count = count + 1;
			values[count] = fixup;
		}

		// Token: 0x060052BD RID: 21181 RVA: 0x00122A20 File Offset: 0x00120C20
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
			FixupHolder[] array = new FixupHolder[num];
			Array.Copy(this.m_values, array, this.m_count);
			this.m_values = array;
		}

		// Token: 0x040024AD RID: 9389
		internal const int InitialSize = 2;

		// Token: 0x040024AE RID: 9390
		internal FixupHolder[] m_values;

		// Token: 0x040024AF RID: 9391
		internal int m_count;
	}
}
