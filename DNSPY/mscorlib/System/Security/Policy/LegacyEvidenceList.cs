using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;

namespace System.Security.Policy
{
	// Token: 0x02000351 RID: 849
	[Serializable]
	internal sealed class LegacyEvidenceList : EvidenceBase, IEnumerable<EvidenceBase>, IEnumerable, ILegacyEvidenceAdapter
	{
		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06002A4A RID: 10826 RVA: 0x0009C993 File Offset: 0x0009AB93
		public object EvidenceObject
		{
			get
			{
				if (this.m_legacyEvidenceList.Count <= 0)
				{
					return null;
				}
				return this.m_legacyEvidenceList[0];
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06002A4B RID: 10827 RVA: 0x0009C9B4 File Offset: 0x0009ABB4
		public Type EvidenceType
		{
			get
			{
				ILegacyEvidenceAdapter legacyEvidenceAdapter = this.m_legacyEvidenceList[0] as ILegacyEvidenceAdapter;
				if (legacyEvidenceAdapter != null)
				{
					return legacyEvidenceAdapter.EvidenceType;
				}
				return this.m_legacyEvidenceList[0].GetType();
			}
		}

		// Token: 0x06002A4C RID: 10828 RVA: 0x0009C9EE File Offset: 0x0009ABEE
		public void Add(EvidenceBase evidence)
		{
			this.m_legacyEvidenceList.Add(evidence);
		}

		// Token: 0x06002A4D RID: 10829 RVA: 0x0009C9FC File Offset: 0x0009ABFC
		public IEnumerator<EvidenceBase> GetEnumerator()
		{
			return this.m_legacyEvidenceList.GetEnumerator();
		}

		// Token: 0x06002A4E RID: 10830 RVA: 0x0009CA0E File Offset: 0x0009AC0E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.m_legacyEvidenceList.GetEnumerator();
		}

		// Token: 0x06002A4F RID: 10831 RVA: 0x0009CA20 File Offset: 0x0009AC20
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override EvidenceBase Clone()
		{
			return base.Clone();
		}

		// Token: 0x06002A50 RID: 10832 RVA: 0x0009CA28 File Offset: 0x0009AC28
		public LegacyEvidenceList()
		{
		}

		// Token: 0x0400113E RID: 4414
		private List<EvidenceBase> m_legacyEvidenceList = new List<EvidenceBase>();
	}
}
