using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000651 RID: 1617
	[ComVisible(true)]
	[Serializable]
	public struct MethodToken
	{
		// Token: 0x06004C07 RID: 19463 RVA: 0x00112F88 File Offset: 0x00111188
		internal MethodToken(int str)
		{
			this.m_method = str;
		}

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x06004C08 RID: 19464 RVA: 0x00112F91 File Offset: 0x00111191
		public int Token
		{
			get
			{
				return this.m_method;
			}
		}

		// Token: 0x06004C09 RID: 19465 RVA: 0x00112F99 File Offset: 0x00111199
		public override int GetHashCode()
		{
			return this.m_method;
		}

		// Token: 0x06004C0A RID: 19466 RVA: 0x00112FA1 File Offset: 0x001111A1
		public override bool Equals(object obj)
		{
			return obj is MethodToken && this.Equals((MethodToken)obj);
		}

		// Token: 0x06004C0B RID: 19467 RVA: 0x00112FB9 File Offset: 0x001111B9
		public bool Equals(MethodToken obj)
		{
			return obj.m_method == this.m_method;
		}

		// Token: 0x06004C0C RID: 19468 RVA: 0x00112FC9 File Offset: 0x001111C9
		public static bool operator ==(MethodToken a, MethodToken b)
		{
			return a.Equals(b);
		}

		// Token: 0x06004C0D RID: 19469 RVA: 0x00112FD3 File Offset: 0x001111D3
		public static bool operator !=(MethodToken a, MethodToken b)
		{
			return !(a == b);
		}

		// Token: 0x06004C0E RID: 19470 RVA: 0x00112FDF File Offset: 0x001111DF
		// Note: this type is marked as 'beforefieldinit'.
		static MethodToken()
		{
		}

		// Token: 0x04001F5B RID: 8027
		public static readonly MethodToken Empty;

		// Token: 0x04001F5C RID: 8028
		internal int m_method;
	}
}
