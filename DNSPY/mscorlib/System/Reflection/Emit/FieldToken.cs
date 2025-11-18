using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200063E RID: 1598
	[ComVisible(true)]
	[Serializable]
	public struct FieldToken
	{
		// Token: 0x06004A99 RID: 19097 RVA: 0x0010D725 File Offset: 0x0010B925
		internal FieldToken(int field, Type fieldClass)
		{
			this.m_fieldTok = field;
			this.m_class = fieldClass;
		}

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x06004A9A RID: 19098 RVA: 0x0010D735 File Offset: 0x0010B935
		public int Token
		{
			get
			{
				return this.m_fieldTok;
			}
		}

		// Token: 0x06004A9B RID: 19099 RVA: 0x0010D73D File Offset: 0x0010B93D
		public override int GetHashCode()
		{
			return this.m_fieldTok;
		}

		// Token: 0x06004A9C RID: 19100 RVA: 0x0010D745 File Offset: 0x0010B945
		public override bool Equals(object obj)
		{
			return obj is FieldToken && this.Equals((FieldToken)obj);
		}

		// Token: 0x06004A9D RID: 19101 RVA: 0x0010D75D File Offset: 0x0010B95D
		public bool Equals(FieldToken obj)
		{
			return obj.m_fieldTok == this.m_fieldTok && obj.m_class == this.m_class;
		}

		// Token: 0x06004A9E RID: 19102 RVA: 0x0010D77D File Offset: 0x0010B97D
		public static bool operator ==(FieldToken a, FieldToken b)
		{
			return a.Equals(b);
		}

		// Token: 0x06004A9F RID: 19103 RVA: 0x0010D787 File Offset: 0x0010B987
		public static bool operator !=(FieldToken a, FieldToken b)
		{
			return !(a == b);
		}

		// Token: 0x06004AA0 RID: 19104 RVA: 0x0010D793 File Offset: 0x0010B993
		// Note: this type is marked as 'beforefieldinit'.
		static FieldToken()
		{
		}

		// Token: 0x04001EC3 RID: 7875
		public static readonly FieldToken Empty;

		// Token: 0x04001EC4 RID: 7876
		internal int m_fieldTok;

		// Token: 0x04001EC5 RID: 7877
		internal object m_class;
	}
}
