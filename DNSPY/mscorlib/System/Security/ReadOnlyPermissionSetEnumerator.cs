using System;
using System.Collections;

namespace System.Security
{
	// Token: 0x020001E7 RID: 487
	internal sealed class ReadOnlyPermissionSetEnumerator : IEnumerator
	{
		// Token: 0x06001D87 RID: 7559 RVA: 0x00066B28 File Offset: 0x00064D28
		internal ReadOnlyPermissionSetEnumerator(IEnumerator permissionSetEnumerator)
		{
			this.m_permissionSetEnumerator = permissionSetEnumerator;
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06001D88 RID: 7560 RVA: 0x00066B38 File Offset: 0x00064D38
		public object Current
		{
			get
			{
				IPermission permission = this.m_permissionSetEnumerator.Current as IPermission;
				if (permission == null)
				{
					return null;
				}
				return permission.Copy();
			}
		}

		// Token: 0x06001D89 RID: 7561 RVA: 0x00066B61 File Offset: 0x00064D61
		public bool MoveNext()
		{
			return this.m_permissionSetEnumerator.MoveNext();
		}

		// Token: 0x06001D8A RID: 7562 RVA: 0x00066B6E File Offset: 0x00064D6E
		public void Reset()
		{
			this.m_permissionSetEnumerator.Reset();
		}

		// Token: 0x04000A49 RID: 2633
		private IEnumerator m_permissionSetEnumerator;
	}
}
