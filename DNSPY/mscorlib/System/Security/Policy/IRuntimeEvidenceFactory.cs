using System;
using System.Collections.Generic;

namespace System.Security.Policy
{
	// Token: 0x0200035A RID: 858
	internal interface IRuntimeEvidenceFactory
	{
		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06002A7C RID: 10876
		IEvidenceFactory Target { get; }

		// Token: 0x06002A7D RID: 10877
		IEnumerable<EvidenceBase> GetFactorySuppliedEvidence();

		// Token: 0x06002A7E RID: 10878
		EvidenceBase GenerateEvidence(Type evidenceType);
	}
}
