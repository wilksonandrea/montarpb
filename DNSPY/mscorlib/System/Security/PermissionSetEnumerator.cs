using System;
using System.Collections;

namespace System.Security
{
	// Token: 0x020001DB RID: 475
	internal class PermissionSetEnumerator : IEnumerator
	{
		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06001CBE RID: 7358 RVA: 0x00062250 File Offset: 0x00060450
		public object Current
		{
			get
			{
				return this.enm.Current;
			}
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x0006225D File Offset: 0x0006045D
		public bool MoveNext()
		{
			return this.enm.MoveNext();
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x0006226A File Offset: 0x0006046A
		public void Reset()
		{
			this.enm.Reset();
		}

		// Token: 0x06001CC1 RID: 7361 RVA: 0x00062277 File Offset: 0x00060477
		internal PermissionSetEnumerator(PermissionSet permSet)
		{
			this.enm = new PermissionSetEnumeratorInternal(permSet);
		}

		// Token: 0x04000A09 RID: 2569
		private PermissionSetEnumeratorInternal enm;
	}
}
