using System;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000839 RID: 2105
	[Serializable]
	internal class CrossAppDomainData
	{
		// Token: 0x17000EE9 RID: 3817
		// (get) Token: 0x060059E7 RID: 23015 RVA: 0x0013CB72 File Offset: 0x0013AD72
		internal virtual IntPtr ContextID
		{
			get
			{
				return new IntPtr((int)this._ContextID);
			}
		}

		// Token: 0x17000EEA RID: 3818
		// (get) Token: 0x060059E8 RID: 23016 RVA: 0x0013CB84 File Offset: 0x0013AD84
		internal virtual int DomainID
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this._DomainID;
			}
		}

		// Token: 0x17000EEB RID: 3819
		// (get) Token: 0x060059E9 RID: 23017 RVA: 0x0013CB8C File Offset: 0x0013AD8C
		internal virtual string ProcessGuid
		{
			get
			{
				return this._processGuid;
			}
		}

		// Token: 0x060059EA RID: 23018 RVA: 0x0013CB94 File Offset: 0x0013AD94
		internal CrossAppDomainData(IntPtr ctxId, int domainID, string processGuid)
		{
			this._DomainID = domainID;
			this._processGuid = processGuid;
			this._ContextID = ctxId.ToInt32();
		}

		// Token: 0x060059EB RID: 23019 RVA: 0x0013CBC8 File Offset: 0x0013ADC8
		internal bool IsFromThisProcess()
		{
			return Identity.ProcessGuid.Equals(this._processGuid);
		}

		// Token: 0x060059EC RID: 23020 RVA: 0x0013CBDA File Offset: 0x0013ADDA
		[SecurityCritical]
		internal bool IsFromThisAppDomain()
		{
			return this.IsFromThisProcess() && Thread.GetDomain().GetId() == this._DomainID;
		}

		// Token: 0x040028F1 RID: 10481
		private object _ContextID = 0;

		// Token: 0x040028F2 RID: 10482
		private int _DomainID;

		// Token: 0x040028F3 RID: 10483
		private string _processGuid;
	}
}
