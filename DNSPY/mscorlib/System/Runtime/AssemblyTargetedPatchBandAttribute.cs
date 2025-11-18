using System;

namespace System.Runtime
{
	// Token: 0x0200071A RID: 1818
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyTargetedPatchBandAttribute : Attribute
	{
		// Token: 0x06005136 RID: 20790 RVA: 0x0011E447 File Offset: 0x0011C647
		public AssemblyTargetedPatchBandAttribute(string targetedPatchBand)
		{
			this.m_targetedPatchBand = targetedPatchBand;
		}

		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x06005137 RID: 20791 RVA: 0x0011E456 File Offset: 0x0011C656
		public string TargetedPatchBand
		{
			get
			{
				return this.m_targetedPatchBand;
			}
		}

		// Token: 0x04002403 RID: 9219
		private string m_targetedPatchBand;
	}
}
