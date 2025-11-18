using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000646 RID: 1606
	[ComVisible(true)]
	[Serializable]
	public struct Label
	{
		// Token: 0x06004B0C RID: 19212 RVA: 0x0010FC04 File Offset: 0x0010DE04
		internal Label(int label)
		{
			this.m_label = label;
		}

		// Token: 0x06004B0D RID: 19213 RVA: 0x0010FC0D File Offset: 0x0010DE0D
		internal int GetLabelValue()
		{
			return this.m_label;
		}

		// Token: 0x06004B0E RID: 19214 RVA: 0x0010FC15 File Offset: 0x0010DE15
		public override int GetHashCode()
		{
			return this.m_label;
		}

		// Token: 0x06004B0F RID: 19215 RVA: 0x0010FC1D File Offset: 0x0010DE1D
		public override bool Equals(object obj)
		{
			return obj is Label && this.Equals((Label)obj);
		}

		// Token: 0x06004B10 RID: 19216 RVA: 0x0010FC35 File Offset: 0x0010DE35
		public bool Equals(Label obj)
		{
			return obj.m_label == this.m_label;
		}

		// Token: 0x06004B11 RID: 19217 RVA: 0x0010FC45 File Offset: 0x0010DE45
		public static bool operator ==(Label a, Label b)
		{
			return a.Equals(b);
		}

		// Token: 0x06004B12 RID: 19218 RVA: 0x0010FC4F File Offset: 0x0010DE4F
		public static bool operator !=(Label a, Label b)
		{
			return !(a == b);
		}

		// Token: 0x04001F0D RID: 7949
		internal int m_label;
	}
}
