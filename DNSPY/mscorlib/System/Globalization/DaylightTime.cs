using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	// Token: 0x020003B4 RID: 948
	[ComVisible(true)]
	[Serializable]
	public class DaylightTime
	{
		// Token: 0x06002F44 RID: 12100 RVA: 0x000B5724 File Offset: 0x000B3924
		private DaylightTime()
		{
		}

		// Token: 0x06002F45 RID: 12101 RVA: 0x000B572C File Offset: 0x000B392C
		public DaylightTime(DateTime start, DateTime end, TimeSpan delta)
		{
			this.m_start = start;
			this.m_end = end;
			this.m_delta = delta;
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06002F46 RID: 12102 RVA: 0x000B5749 File Offset: 0x000B3949
		public DateTime Start
		{
			get
			{
				return this.m_start;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06002F47 RID: 12103 RVA: 0x000B5751 File Offset: 0x000B3951
		public DateTime End
		{
			get
			{
				return this.m_end;
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06002F48 RID: 12104 RVA: 0x000B5759 File Offset: 0x000B3959
		public TimeSpan Delta
		{
			get
			{
				return this.m_delta;
			}
		}

		// Token: 0x04001406 RID: 5126
		internal DateTime m_start;

		// Token: 0x04001407 RID: 5127
		internal DateTime m_end;

		// Token: 0x04001408 RID: 5128
		internal TimeSpan m_delta;
	}
}
