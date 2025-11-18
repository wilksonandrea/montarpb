using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200065E RID: 1630
	[ComVisible(true)]
	[Serializable]
	public struct ParameterToken
	{
		// Token: 0x06004CCF RID: 19663 RVA: 0x00116D64 File Offset: 0x00114F64
		internal ParameterToken(int tkParam)
		{
			this.m_tkParameter = tkParam;
		}

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x06004CD0 RID: 19664 RVA: 0x00116D6D File Offset: 0x00114F6D
		public int Token
		{
			get
			{
				return this.m_tkParameter;
			}
		}

		// Token: 0x06004CD1 RID: 19665 RVA: 0x00116D75 File Offset: 0x00114F75
		public override int GetHashCode()
		{
			return this.m_tkParameter;
		}

		// Token: 0x06004CD2 RID: 19666 RVA: 0x00116D7D File Offset: 0x00114F7D
		public override bool Equals(object obj)
		{
			return obj is ParameterToken && this.Equals((ParameterToken)obj);
		}

		// Token: 0x06004CD3 RID: 19667 RVA: 0x00116D95 File Offset: 0x00114F95
		public bool Equals(ParameterToken obj)
		{
			return obj.m_tkParameter == this.m_tkParameter;
		}

		// Token: 0x06004CD4 RID: 19668 RVA: 0x00116DA5 File Offset: 0x00114FA5
		public static bool operator ==(ParameterToken a, ParameterToken b)
		{
			return a.Equals(b);
		}

		// Token: 0x06004CD5 RID: 19669 RVA: 0x00116DAF File Offset: 0x00114FAF
		public static bool operator !=(ParameterToken a, ParameterToken b)
		{
			return !(a == b);
		}

		// Token: 0x06004CD6 RID: 19670 RVA: 0x00116DBB File Offset: 0x00114FBB
		// Note: this type is marked as 'beforefieldinit'.
		static ParameterToken()
		{
		}

		// Token: 0x04002197 RID: 8599
		public static readonly ParameterToken Empty;

		// Token: 0x04002198 RID: 8600
		internal int m_tkParameter;
	}
}
