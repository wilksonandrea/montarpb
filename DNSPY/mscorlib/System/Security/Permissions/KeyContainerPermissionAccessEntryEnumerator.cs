using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000317 RID: 791
	[ComVisible(true)]
	[Serializable]
	public sealed class KeyContainerPermissionAccessEntryEnumerator : IEnumerator
	{
		// Token: 0x060027DE RID: 10206 RVA: 0x00090C0A File Offset: 0x0008EE0A
		private KeyContainerPermissionAccessEntryEnumerator()
		{
		}

		// Token: 0x060027DF RID: 10207 RVA: 0x00090C12 File Offset: 0x0008EE12
		internal KeyContainerPermissionAccessEntryEnumerator(KeyContainerPermissionAccessEntryCollection entries)
		{
			this.m_entries = entries;
			this.m_current = -1;
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x060027E0 RID: 10208 RVA: 0x00090C28 File Offset: 0x0008EE28
		public KeyContainerPermissionAccessEntry Current
		{
			get
			{
				return this.m_entries[this.m_current];
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x060027E1 RID: 10209 RVA: 0x00090C3B File Offset: 0x0008EE3B
		object IEnumerator.Current
		{
			get
			{
				return this.m_entries[this.m_current];
			}
		}

		// Token: 0x060027E2 RID: 10210 RVA: 0x00090C4E File Offset: 0x0008EE4E
		public bool MoveNext()
		{
			if (this.m_current == this.m_entries.Count - 1)
			{
				return false;
			}
			this.m_current++;
			return true;
		}

		// Token: 0x060027E3 RID: 10211 RVA: 0x00090C76 File Offset: 0x0008EE76
		public void Reset()
		{
			this.m_current = -1;
		}

		// Token: 0x04000F72 RID: 3954
		private KeyContainerPermissionAccessEntryCollection m_entries;

		// Token: 0x04000F73 RID: 3955
		private int m_current;
	}
}
