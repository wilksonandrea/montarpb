using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008B6 RID: 2230
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Method)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class CompilationRelaxationsAttribute : Attribute
	{
		// Token: 0x06005DAB RID: 23979 RVA: 0x001495A0 File Offset: 0x001477A0
		[__DynamicallyInvokable]
		public CompilationRelaxationsAttribute(int relaxations)
		{
			this.m_relaxations = relaxations;
		}

		// Token: 0x06005DAC RID: 23980 RVA: 0x001495AF File Offset: 0x001477AF
		public CompilationRelaxationsAttribute(CompilationRelaxations relaxations)
		{
			this.m_relaxations = (int)relaxations;
		}

		// Token: 0x17001013 RID: 4115
		// (get) Token: 0x06005DAD RID: 23981 RVA: 0x001495BE File Offset: 0x001477BE
		[__DynamicallyInvokable]
		public int CompilationRelaxations
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_relaxations;
			}
		}

		// Token: 0x04002A1D RID: 10781
		private int m_relaxations;
	}
}
