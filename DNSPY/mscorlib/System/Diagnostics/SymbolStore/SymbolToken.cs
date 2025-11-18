using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	// Token: 0x02000409 RID: 1033
	[ComVisible(true)]
	public struct SymbolToken
	{
		// Token: 0x060033EA RID: 13290 RVA: 0x000C64C0 File Offset: 0x000C46C0
		public SymbolToken(int val)
		{
			this.m_token = val;
		}

		// Token: 0x060033EB RID: 13291 RVA: 0x000C64C9 File Offset: 0x000C46C9
		public int GetToken()
		{
			return this.m_token;
		}

		// Token: 0x060033EC RID: 13292 RVA: 0x000C64D1 File Offset: 0x000C46D1
		public override int GetHashCode()
		{
			return this.m_token;
		}

		// Token: 0x060033ED RID: 13293 RVA: 0x000C64D9 File Offset: 0x000C46D9
		public override bool Equals(object obj)
		{
			return obj is SymbolToken && this.Equals((SymbolToken)obj);
		}

		// Token: 0x060033EE RID: 13294 RVA: 0x000C64F1 File Offset: 0x000C46F1
		public bool Equals(SymbolToken obj)
		{
			return obj.m_token == this.m_token;
		}

		// Token: 0x060033EF RID: 13295 RVA: 0x000C6501 File Offset: 0x000C4701
		public static bool operator ==(SymbolToken a, SymbolToken b)
		{
			return a.Equals(b);
		}

		// Token: 0x060033F0 RID: 13296 RVA: 0x000C650B File Offset: 0x000C470B
		public static bool operator !=(SymbolToken a, SymbolToken b)
		{
			return !(a == b);
		}

		// Token: 0x04001707 RID: 5895
		internal int m_token;
	}
}
