using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200042E RID: 1070
	internal struct SessionMask
	{
		// Token: 0x06003563 RID: 13667 RVA: 0x000CEE4B File Offset: 0x000CD04B
		public SessionMask(SessionMask m)
		{
			this.m_mask = m.m_mask;
		}

		// Token: 0x06003564 RID: 13668 RVA: 0x000CEE59 File Offset: 0x000CD059
		public SessionMask(uint mask = 0U)
		{
			this.m_mask = mask & 15U;
		}

		// Token: 0x06003565 RID: 13669 RVA: 0x000CEE65 File Offset: 0x000CD065
		public bool IsEqualOrSupersetOf(SessionMask m)
		{
			return (this.m_mask | m.m_mask) == this.m_mask;
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06003566 RID: 13670 RVA: 0x000CEE7C File Offset: 0x000CD07C
		public static SessionMask All
		{
			get
			{
				return new SessionMask(15U);
			}
		}

		// Token: 0x06003567 RID: 13671 RVA: 0x000CEE85 File Offset: 0x000CD085
		public static SessionMask FromId(int perEventSourceSessionId)
		{
			return new SessionMask(1U << perEventSourceSessionId);
		}

		// Token: 0x06003568 RID: 13672 RVA: 0x000CEE92 File Offset: 0x000CD092
		public ulong ToEventKeywords()
		{
			return (ulong)this.m_mask << 44;
		}

		// Token: 0x06003569 RID: 13673 RVA: 0x000CEE9E File Offset: 0x000CD09E
		public static SessionMask FromEventKeywords(ulong m)
		{
			return new SessionMask((uint)(m >> 44));
		}

		// Token: 0x170007F3 RID: 2035
		public bool this[int perEventSourceSessionId]
		{
			get
			{
				return ((ulong)this.m_mask & (ulong)(1L << (perEventSourceSessionId & 31))) > 0UL;
			}
			set
			{
				if (value)
				{
					this.m_mask |= 1U << perEventSourceSessionId;
					return;
				}
				this.m_mask &= ~(1U << perEventSourceSessionId);
			}
		}

		// Token: 0x0600356C RID: 13676 RVA: 0x000CEEEC File Offset: 0x000CD0EC
		public static SessionMask operator |(SessionMask m1, SessionMask m2)
		{
			return new SessionMask(m1.m_mask | m2.m_mask);
		}

		// Token: 0x0600356D RID: 13677 RVA: 0x000CEF00 File Offset: 0x000CD100
		public static SessionMask operator &(SessionMask m1, SessionMask m2)
		{
			return new SessionMask(m1.m_mask & m2.m_mask);
		}

		// Token: 0x0600356E RID: 13678 RVA: 0x000CEF14 File Offset: 0x000CD114
		public static SessionMask operator ^(SessionMask m1, SessionMask m2)
		{
			return new SessionMask(m1.m_mask ^ m2.m_mask);
		}

		// Token: 0x0600356F RID: 13679 RVA: 0x000CEF28 File Offset: 0x000CD128
		public static SessionMask operator ~(SessionMask m)
		{
			return new SessionMask(15U & ~m.m_mask);
		}

		// Token: 0x06003570 RID: 13680 RVA: 0x000CEF39 File Offset: 0x000CD139
		public static explicit operator ulong(SessionMask m)
		{
			return (ulong)m.m_mask;
		}

		// Token: 0x06003571 RID: 13681 RVA: 0x000CEF42 File Offset: 0x000CD142
		public static explicit operator uint(SessionMask m)
		{
			return m.m_mask;
		}

		// Token: 0x040017BF RID: 6079
		private uint m_mask;

		// Token: 0x040017C0 RID: 6080
		internal const int SHIFT_SESSION_TO_KEYWORD = 44;

		// Token: 0x040017C1 RID: 6081
		internal const uint MASK = 15U;

		// Token: 0x040017C2 RID: 6082
		internal const uint MAX = 4U;
	}
}
