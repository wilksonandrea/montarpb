using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000662 RID: 1634
	[ComVisible(true)]
	public struct SignatureToken
	{
		// Token: 0x06004D39 RID: 19769 RVA: 0x00118368 File Offset: 0x00116568
		internal SignatureToken(int str, ModuleBuilder mod)
		{
			this.m_signature = str;
			this.m_moduleBuilder = mod;
		}

		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x06004D3A RID: 19770 RVA: 0x00118378 File Offset: 0x00116578
		public int Token
		{
			get
			{
				return this.m_signature;
			}
		}

		// Token: 0x06004D3B RID: 19771 RVA: 0x00118380 File Offset: 0x00116580
		public override int GetHashCode()
		{
			return this.m_signature;
		}

		// Token: 0x06004D3C RID: 19772 RVA: 0x00118388 File Offset: 0x00116588
		public override bool Equals(object obj)
		{
			return obj is SignatureToken && this.Equals((SignatureToken)obj);
		}

		// Token: 0x06004D3D RID: 19773 RVA: 0x001183A0 File Offset: 0x001165A0
		public bool Equals(SignatureToken obj)
		{
			return obj.m_signature == this.m_signature;
		}

		// Token: 0x06004D3E RID: 19774 RVA: 0x001183B0 File Offset: 0x001165B0
		public static bool operator ==(SignatureToken a, SignatureToken b)
		{
			return a.Equals(b);
		}

		// Token: 0x06004D3F RID: 19775 RVA: 0x001183BA File Offset: 0x001165BA
		public static bool operator !=(SignatureToken a, SignatureToken b)
		{
			return !(a == b);
		}

		// Token: 0x06004D40 RID: 19776 RVA: 0x001183C6 File Offset: 0x001165C6
		// Note: this type is marked as 'beforefieldinit'.
		static SignatureToken()
		{
		}

		// Token: 0x040021AC RID: 8620
		public static readonly SignatureToken Empty;

		// Token: 0x040021AD RID: 8621
		internal int m_signature;

		// Token: 0x040021AE RID: 8622
		internal ModuleBuilder m_moduleBuilder;
	}
}
