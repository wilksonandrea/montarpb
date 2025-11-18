using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000611 RID: 1553
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	[ComVisible(true)]
	public sealed class ObfuscateAssemblyAttribute : Attribute
	{
		// Token: 0x060047F5 RID: 18421 RVA: 0x00105B8C File Offset: 0x00103D8C
		public ObfuscateAssemblyAttribute(bool assemblyIsPrivate)
		{
			this.m_assemblyIsPrivate = assemblyIsPrivate;
		}

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x060047F6 RID: 18422 RVA: 0x00105BA2 File Offset: 0x00103DA2
		public bool AssemblyIsPrivate
		{
			get
			{
				return this.m_assemblyIsPrivate;
			}
		}

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x060047F7 RID: 18423 RVA: 0x00105BAA File Offset: 0x00103DAA
		// (set) Token: 0x060047F8 RID: 18424 RVA: 0x00105BB2 File Offset: 0x00103DB2
		public bool StripAfterObfuscation
		{
			get
			{
				return this.m_strip;
			}
			set
			{
				this.m_strip = value;
			}
		}

		// Token: 0x04001DCB RID: 7627
		private bool m_assemblyIsPrivate;

		// Token: 0x04001DCC RID: 7628
		private bool m_strip = true;
	}
}
