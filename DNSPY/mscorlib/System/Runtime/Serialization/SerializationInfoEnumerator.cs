using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x02000745 RID: 1861
	[ComVisible(true)]
	public sealed class SerializationInfoEnumerator : IEnumerator
	{
		// Token: 0x0600522D RID: 21037 RVA: 0x001208AD File Offset: 0x0011EAAD
		internal SerializationInfoEnumerator(string[] members, object[] info, Type[] types, int numItems)
		{
			this.m_members = members;
			this.m_data = info;
			this.m_types = types;
			this.m_numItems = numItems - 1;
			this.m_currItem = -1;
			this.m_current = false;
		}

		// Token: 0x0600522E RID: 21038 RVA: 0x001208E2 File Offset: 0x0011EAE2
		public bool MoveNext()
		{
			if (this.m_currItem < this.m_numItems)
			{
				this.m_currItem++;
				this.m_current = true;
			}
			else
			{
				this.m_current = false;
			}
			return this.m_current;
		}

		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x0600522F RID: 21039 RVA: 0x00120918 File Offset: 0x0011EB18
		object IEnumerator.Current
		{
			get
			{
				if (!this.m_current)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
				}
				return new SerializationEntry(this.m_members[this.m_currItem], this.m_data[this.m_currItem], this.m_types[this.m_currItem]);
			}
		}

		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x06005230 RID: 21040 RVA: 0x00120970 File Offset: 0x0011EB70
		public SerializationEntry Current
		{
			get
			{
				if (!this.m_current)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
				}
				return new SerializationEntry(this.m_members[this.m_currItem], this.m_data[this.m_currItem], this.m_types[this.m_currItem]);
			}
		}

		// Token: 0x06005231 RID: 21041 RVA: 0x001209C1 File Offset: 0x0011EBC1
		public void Reset()
		{
			this.m_currItem = -1;
			this.m_current = false;
		}

		// Token: 0x17000D8C RID: 3468
		// (get) Token: 0x06005232 RID: 21042 RVA: 0x001209D1 File Offset: 0x0011EBD1
		public string Name
		{
			get
			{
				if (!this.m_current)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
				}
				return this.m_members[this.m_currItem];
			}
		}

		// Token: 0x17000D8D RID: 3469
		// (get) Token: 0x06005233 RID: 21043 RVA: 0x001209F8 File Offset: 0x0011EBF8
		public object Value
		{
			get
			{
				if (!this.m_current)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
				}
				return this.m_data[this.m_currItem];
			}
		}

		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x06005234 RID: 21044 RVA: 0x00120A1F File Offset: 0x0011EC1F
		public Type ObjectType
		{
			get
			{
				if (!this.m_current)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
				}
				return this.m_types[this.m_currItem];
			}
		}

		// Token: 0x04002463 RID: 9315
		private string[] m_members;

		// Token: 0x04002464 RID: 9316
		private object[] m_data;

		// Token: 0x04002465 RID: 9317
		private Type[] m_types;

		// Token: 0x04002466 RID: 9318
		private int m_numItems;

		// Token: 0x04002467 RID: 9319
		private int m_currItem;

		// Token: 0x04002468 RID: 9320
		private bool m_current;
	}
}
