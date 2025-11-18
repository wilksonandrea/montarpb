using System;

namespace System.Security.Util
{
	// Token: 0x0200037F RID: 895
	internal struct TokenBasedSetEnumerator
	{
		// Token: 0x06002C79 RID: 11385 RVA: 0x000A5DCB File Offset: 0x000A3FCB
		public bool MoveNext()
		{
			return this._tb != null && this._tb.MoveNext(ref this);
		}

		// Token: 0x06002C7A RID: 11386 RVA: 0x000A5DE3 File Offset: 0x000A3FE3
		public void Reset()
		{
			this.Index = -1;
			this.Current = null;
		}

		// Token: 0x06002C7B RID: 11387 RVA: 0x000A5DF3 File Offset: 0x000A3FF3
		public TokenBasedSetEnumerator(TokenBasedSet tb)
		{
			this.Index = -1;
			this.Current = null;
			this._tb = tb;
		}

		// Token: 0x040011D9 RID: 4569
		public object Current;

		// Token: 0x040011DA RID: 4570
		public int Index;

		// Token: 0x040011DB RID: 4571
		private TokenBasedSet _tb;
	}
}
