using System;
using System.Runtime.Serialization;

namespace System.Globalization
{
	// Token: 0x020003BF RID: 959
	[Serializable]
	internal class EraInfo
	{
		// Token: 0x06002F83 RID: 12163 RVA: 0x000B6568 File Offset: 0x000B4768
		internal EraInfo(int era, int startYear, int startMonth, int startDay, int yearOffset, int minEraYear, int maxEraYear)
		{
			this.era = era;
			this.yearOffset = yearOffset;
			this.minEraYear = minEraYear;
			this.maxEraYear = maxEraYear;
			this.ticks = new DateTime(startYear, startMonth, startDay).Ticks;
		}

		// Token: 0x06002F84 RID: 12164 RVA: 0x000B65B4 File Offset: 0x000B47B4
		internal EraInfo(int era, int startYear, int startMonth, int startDay, int yearOffset, int minEraYear, int maxEraYear, string eraName, string abbrevEraName, string englishEraName)
		{
			this.era = era;
			this.yearOffset = yearOffset;
			this.minEraYear = minEraYear;
			this.maxEraYear = maxEraYear;
			this.ticks = new DateTime(startYear, startMonth, startDay).Ticks;
			this.eraName = eraName;
			this.abbrevEraName = abbrevEraName;
			this.englishEraName = englishEraName;
		}

		// Token: 0x04001435 RID: 5173
		internal int era;

		// Token: 0x04001436 RID: 5174
		internal long ticks;

		// Token: 0x04001437 RID: 5175
		internal int yearOffset;

		// Token: 0x04001438 RID: 5176
		internal int minEraYear;

		// Token: 0x04001439 RID: 5177
		internal int maxEraYear;

		// Token: 0x0400143A RID: 5178
		[OptionalField(VersionAdded = 4)]
		internal string eraName;

		// Token: 0x0400143B RID: 5179
		[OptionalField(VersionAdded = 4)]
		internal string abbrevEraName;

		// Token: 0x0400143C RID: 5180
		[OptionalField(VersionAdded = 4)]
		internal string englishEraName;
	}
}
