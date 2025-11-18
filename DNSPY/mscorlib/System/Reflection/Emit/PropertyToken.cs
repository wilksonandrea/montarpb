using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000660 RID: 1632
	[ComVisible(true)]
	[Serializable]
	public struct PropertyToken
	{
		// Token: 0x06004CF9 RID: 19705 RVA: 0x00117134 File Offset: 0x00115334
		internal PropertyToken(int str)
		{
			this.m_property = str;
		}

		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x06004CFA RID: 19706 RVA: 0x0011713D File Offset: 0x0011533D
		public int Token
		{
			get
			{
				return this.m_property;
			}
		}

		// Token: 0x06004CFB RID: 19707 RVA: 0x00117145 File Offset: 0x00115345
		public override int GetHashCode()
		{
			return this.m_property;
		}

		// Token: 0x06004CFC RID: 19708 RVA: 0x0011714D File Offset: 0x0011534D
		public override bool Equals(object obj)
		{
			return obj is PropertyToken && this.Equals((PropertyToken)obj);
		}

		// Token: 0x06004CFD RID: 19709 RVA: 0x00117165 File Offset: 0x00115365
		public bool Equals(PropertyToken obj)
		{
			return obj.m_property == this.m_property;
		}

		// Token: 0x06004CFE RID: 19710 RVA: 0x00117175 File Offset: 0x00115375
		public static bool operator ==(PropertyToken a, PropertyToken b)
		{
			return a.Equals(b);
		}

		// Token: 0x06004CFF RID: 19711 RVA: 0x0011717F File Offset: 0x0011537F
		public static bool operator !=(PropertyToken a, PropertyToken b)
		{
			return !(a == b);
		}

		// Token: 0x06004D00 RID: 19712 RVA: 0x0011718B File Offset: 0x0011538B
		// Note: this type is marked as 'beforefieldinit'.
		static PropertyToken()
		{
		}

		// Token: 0x040021A3 RID: 8611
		public static readonly PropertyToken Empty;

		// Token: 0x040021A4 RID: 8612
		internal int m_property;
	}
}
