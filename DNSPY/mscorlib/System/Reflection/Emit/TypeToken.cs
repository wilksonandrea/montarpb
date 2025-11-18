using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000669 RID: 1641
	[ComVisible(true)]
	[Serializable]
	public struct TypeToken
	{
		// Token: 0x06004EBC RID: 20156 RVA: 0x0011BA35 File Offset: 0x00119C35
		internal TypeToken(int str)
		{
			this.m_class = str;
		}

		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x06004EBD RID: 20157 RVA: 0x0011BA3E File Offset: 0x00119C3E
		public int Token
		{
			get
			{
				return this.m_class;
			}
		}

		// Token: 0x06004EBE RID: 20158 RVA: 0x0011BA46 File Offset: 0x00119C46
		public override int GetHashCode()
		{
			return this.m_class;
		}

		// Token: 0x06004EBF RID: 20159 RVA: 0x0011BA4E File Offset: 0x00119C4E
		public override bool Equals(object obj)
		{
			return obj is TypeToken && this.Equals((TypeToken)obj);
		}

		// Token: 0x06004EC0 RID: 20160 RVA: 0x0011BA66 File Offset: 0x00119C66
		public bool Equals(TypeToken obj)
		{
			return obj.m_class == this.m_class;
		}

		// Token: 0x06004EC1 RID: 20161 RVA: 0x0011BA76 File Offset: 0x00119C76
		public static bool operator ==(TypeToken a, TypeToken b)
		{
			return a.Equals(b);
		}

		// Token: 0x06004EC2 RID: 20162 RVA: 0x0011BA80 File Offset: 0x00119C80
		public static bool operator !=(TypeToken a, TypeToken b)
		{
			return !(a == b);
		}

		// Token: 0x06004EC3 RID: 20163 RVA: 0x0011BA8C File Offset: 0x00119C8C
		// Note: this type is marked as 'beforefieldinit'.
		static TypeToken()
		{
		}

		// Token: 0x040021DB RID: 8667
		public static readonly TypeToken Empty;

		// Token: 0x040021DC RID: 8668
		internal int m_class;
	}
}
