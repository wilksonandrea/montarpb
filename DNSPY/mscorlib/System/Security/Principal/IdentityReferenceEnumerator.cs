using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	// Token: 0x02000336 RID: 822
	[ComVisible(false)]
	internal class IdentityReferenceEnumerator : IEnumerator<IdentityReference>, IDisposable, IEnumerator
	{
		// Token: 0x06002914 RID: 10516 RVA: 0x00097378 File Offset: 0x00095578
		internal IdentityReferenceEnumerator(IdentityReferenceCollection collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._Collection = collection;
			this._Current = -1;
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06002915 RID: 10517 RVA: 0x0009739C File Offset: 0x0009559C
		object IEnumerator.Current
		{
			get
			{
				return this._Collection.Identities[this._Current];
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06002916 RID: 10518 RVA: 0x000973B4 File Offset: 0x000955B4
		public IdentityReference Current
		{
			get
			{
				return ((IEnumerator)this).Current as IdentityReference;
			}
		}

		// Token: 0x06002917 RID: 10519 RVA: 0x000973C1 File Offset: 0x000955C1
		public bool MoveNext()
		{
			this._Current++;
			return this._Current < this._Collection.Count;
		}

		// Token: 0x06002918 RID: 10520 RVA: 0x000973E4 File Offset: 0x000955E4
		public void Reset()
		{
			this._Current = -1;
		}

		// Token: 0x06002919 RID: 10521 RVA: 0x000973ED File Offset: 0x000955ED
		public void Dispose()
		{
		}

		// Token: 0x04001092 RID: 4242
		private int _Current;

		// Token: 0x04001093 RID: 4243
		private readonly IdentityReferenceCollection _Collection;
	}
}
