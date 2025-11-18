using System;
using System.Collections;

namespace System.Security.AccessControl
{
	// Token: 0x0200020A RID: 522
	public sealed class AceEnumerator : IEnumerator
	{
		// Token: 0x06001E88 RID: 7816 RVA: 0x0006AA37 File Offset: 0x00068C37
		internal AceEnumerator(GenericAcl collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._acl = collection;
			this.Reset();
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06001E89 RID: 7817 RVA: 0x0006AA5A File Offset: 0x00068C5A
		object IEnumerator.Current
		{
			get
			{
				if (this._current == -1 || this._current >= this._acl.Count)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Arg_InvalidOperationException"));
				}
				return this._acl[this._current];
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06001E8A RID: 7818 RVA: 0x0006AA99 File Offset: 0x00068C99
		public GenericAce Current
		{
			get
			{
				return ((IEnumerator)this).Current as GenericAce;
			}
		}

		// Token: 0x06001E8B RID: 7819 RVA: 0x0006AAA6 File Offset: 0x00068CA6
		public bool MoveNext()
		{
			this._current++;
			return this._current < this._acl.Count;
		}

		// Token: 0x06001E8C RID: 7820 RVA: 0x0006AAC9 File Offset: 0x00068CC9
		public void Reset()
		{
			this._current = -1;
		}

		// Token: 0x04000B04 RID: 2820
		private int _current;

		// Token: 0x04000B05 RID: 2821
		private readonly GenericAcl _acl;
	}
}
