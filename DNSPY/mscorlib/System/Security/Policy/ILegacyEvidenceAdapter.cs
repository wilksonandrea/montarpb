using System;

namespace System.Security.Policy
{
	// Token: 0x0200034F RID: 847
	internal interface ILegacyEvidenceAdapter
	{
		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06002A42 RID: 10818
		object EvidenceObject { get; }

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06002A43 RID: 10819
		Type EvidenceType { get; }
	}
}
