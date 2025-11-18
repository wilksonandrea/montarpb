using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200063C RID: 1596
	[ComVisible(true)]
	[Serializable]
	public struct EventToken
	{
		// Token: 0x06004A77 RID: 19063 RVA: 0x0010D311 File Offset: 0x0010B511
		internal EventToken(int str)
		{
			this.m_event = str;
		}

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x06004A78 RID: 19064 RVA: 0x0010D31A File Offset: 0x0010B51A
		public int Token
		{
			get
			{
				return this.m_event;
			}
		}

		// Token: 0x06004A79 RID: 19065 RVA: 0x0010D322 File Offset: 0x0010B522
		public override int GetHashCode()
		{
			return this.m_event;
		}

		// Token: 0x06004A7A RID: 19066 RVA: 0x0010D32A File Offset: 0x0010B52A
		public override bool Equals(object obj)
		{
			return obj is EventToken && this.Equals((EventToken)obj);
		}

		// Token: 0x06004A7B RID: 19067 RVA: 0x0010D342 File Offset: 0x0010B542
		public bool Equals(EventToken obj)
		{
			return obj.m_event == this.m_event;
		}

		// Token: 0x06004A7C RID: 19068 RVA: 0x0010D352 File Offset: 0x0010B552
		public static bool operator ==(EventToken a, EventToken b)
		{
			return a.Equals(b);
		}

		// Token: 0x06004A7D RID: 19069 RVA: 0x0010D35C File Offset: 0x0010B55C
		public static bool operator !=(EventToken a, EventToken b)
		{
			return !(a == b);
		}

		// Token: 0x06004A7E RID: 19070 RVA: 0x0010D368 File Offset: 0x0010B568
		// Note: this type is marked as 'beforefieldinit'.
		static EventToken()
		{
		}

		// Token: 0x04001EBB RID: 7867
		public static readonly EventToken Empty;

		// Token: 0x04001EBC RID: 7868
		internal int m_event;
	}
}
