using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000663 RID: 1635
	[ComVisible(true)]
	[Serializable]
	public struct StringToken
	{
		// Token: 0x06004D41 RID: 19777 RVA: 0x001183C8 File Offset: 0x001165C8
		internal StringToken(int str)
		{
			this.m_string = str;
		}

		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x06004D42 RID: 19778 RVA: 0x001183D1 File Offset: 0x001165D1
		public int Token
		{
			get
			{
				return this.m_string;
			}
		}

		// Token: 0x06004D43 RID: 19779 RVA: 0x001183D9 File Offset: 0x001165D9
		public override int GetHashCode()
		{
			return this.m_string;
		}

		// Token: 0x06004D44 RID: 19780 RVA: 0x001183E1 File Offset: 0x001165E1
		public override bool Equals(object obj)
		{
			return obj is StringToken && this.Equals((StringToken)obj);
		}

		// Token: 0x06004D45 RID: 19781 RVA: 0x001183F9 File Offset: 0x001165F9
		public bool Equals(StringToken obj)
		{
			return obj.m_string == this.m_string;
		}

		// Token: 0x06004D46 RID: 19782 RVA: 0x00118409 File Offset: 0x00116609
		public static bool operator ==(StringToken a, StringToken b)
		{
			return a.Equals(b);
		}

		// Token: 0x06004D47 RID: 19783 RVA: 0x00118413 File Offset: 0x00116613
		public static bool operator !=(StringToken a, StringToken b)
		{
			return !(a == b);
		}

		// Token: 0x040021AF RID: 8623
		internal int m_string;
	}
}
