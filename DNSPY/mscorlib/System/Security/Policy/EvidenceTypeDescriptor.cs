using System;

namespace System.Security.Policy
{
	// Token: 0x02000352 RID: 850
	[Serializable]
	internal sealed class EvidenceTypeDescriptor
	{
		// Token: 0x06002A51 RID: 10833 RVA: 0x0009CA3B File Offset: 0x0009AC3B
		public EvidenceTypeDescriptor()
		{
		}

		// Token: 0x06002A52 RID: 10834 RVA: 0x0009CA44 File Offset: 0x0009AC44
		private EvidenceTypeDescriptor(EvidenceTypeDescriptor descriptor)
		{
			this.m_hostCanGenerate = descriptor.m_hostCanGenerate;
			if (descriptor.m_assemblyEvidence != null)
			{
				this.m_assemblyEvidence = descriptor.m_assemblyEvidence.Clone();
			}
			if (descriptor.m_hostEvidence != null)
			{
				this.m_hostEvidence = descriptor.m_hostEvidence.Clone();
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06002A53 RID: 10835 RVA: 0x0009CA95 File Offset: 0x0009AC95
		// (set) Token: 0x06002A54 RID: 10836 RVA: 0x0009CA9D File Offset: 0x0009AC9D
		public EvidenceBase AssemblyEvidence
		{
			get
			{
				return this.m_assemblyEvidence;
			}
			set
			{
				this.m_assemblyEvidence = value;
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06002A55 RID: 10837 RVA: 0x0009CAA6 File Offset: 0x0009ACA6
		// (set) Token: 0x06002A56 RID: 10838 RVA: 0x0009CAAE File Offset: 0x0009ACAE
		public bool Generated
		{
			get
			{
				return this.m_generated;
			}
			set
			{
				this.m_generated = value;
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06002A57 RID: 10839 RVA: 0x0009CAB7 File Offset: 0x0009ACB7
		// (set) Token: 0x06002A58 RID: 10840 RVA: 0x0009CABF File Offset: 0x0009ACBF
		public bool HostCanGenerate
		{
			get
			{
				return this.m_hostCanGenerate;
			}
			set
			{
				this.m_hostCanGenerate = value;
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06002A59 RID: 10841 RVA: 0x0009CAC8 File Offset: 0x0009ACC8
		// (set) Token: 0x06002A5A RID: 10842 RVA: 0x0009CAD0 File Offset: 0x0009ACD0
		public EvidenceBase HostEvidence
		{
			get
			{
				return this.m_hostEvidence;
			}
			set
			{
				this.m_hostEvidence = value;
			}
		}

		// Token: 0x06002A5B RID: 10843 RVA: 0x0009CAD9 File Offset: 0x0009ACD9
		public EvidenceTypeDescriptor Clone()
		{
			return new EvidenceTypeDescriptor(this);
		}

		// Token: 0x0400113F RID: 4415
		[NonSerialized]
		private bool m_hostCanGenerate;

		// Token: 0x04001140 RID: 4416
		[NonSerialized]
		private bool m_generated;

		// Token: 0x04001141 RID: 4417
		private EvidenceBase m_hostEvidence;

		// Token: 0x04001142 RID: 4418
		private EvidenceBase m_assemblyEvidence;
	}
}
