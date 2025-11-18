using System;
using System.Configuration.Assemblies;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005C4 RID: 1476
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	public sealed class AssemblyAlgorithmIdAttribute : Attribute
	{
		// Token: 0x06004474 RID: 17524 RVA: 0x000FC2E8 File Offset: 0x000FA4E8
		public AssemblyAlgorithmIdAttribute(AssemblyHashAlgorithm algorithmId)
		{
			this.m_algId = (uint)algorithmId;
		}

		// Token: 0x06004475 RID: 17525 RVA: 0x000FC2F7 File Offset: 0x000FA4F7
		[CLSCompliant(false)]
		public AssemblyAlgorithmIdAttribute(uint algorithmId)
		{
			this.m_algId = algorithmId;
		}

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06004476 RID: 17526 RVA: 0x000FC306 File Offset: 0x000FA506
		[CLSCompliant(false)]
		public uint AlgorithmId
		{
			get
			{
				return this.m_algId;
			}
		}

		// Token: 0x04001C13 RID: 7187
		private uint m_algId;
	}
}
