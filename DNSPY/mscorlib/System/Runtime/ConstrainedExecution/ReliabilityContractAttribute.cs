using System;

namespace System.Runtime.ConstrainedExecution
{
	// Token: 0x0200072E RID: 1838
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Interface, Inherited = false)]
	public sealed class ReliabilityContractAttribute : Attribute
	{
		// Token: 0x0600517A RID: 20858 RVA: 0x0011EF60 File Offset: 0x0011D160
		public ReliabilityContractAttribute(Consistency consistencyGuarantee, Cer cer)
		{
			this._consistency = consistencyGuarantee;
			this._cer = cer;
		}

		// Token: 0x17000D6F RID: 3439
		// (get) Token: 0x0600517B RID: 20859 RVA: 0x0011EF76 File Offset: 0x0011D176
		public Consistency ConsistencyGuarantee
		{
			get
			{
				return this._consistency;
			}
		}

		// Token: 0x17000D70 RID: 3440
		// (get) Token: 0x0600517C RID: 20860 RVA: 0x0011EF7E File Offset: 0x0011D17E
		public Cer Cer
		{
			get
			{
				return this._cer;
			}
		}

		// Token: 0x04002440 RID: 9280
		private Consistency _consistency;

		// Token: 0x04002441 RID: 9281
		private Cer _cer;
	}
}
