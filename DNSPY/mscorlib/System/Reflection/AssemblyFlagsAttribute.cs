using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005C5 RID: 1477
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class AssemblyFlagsAttribute : Attribute
	{
		// Token: 0x06004477 RID: 17527 RVA: 0x000FC30E File Offset: 0x000FA50E
		[Obsolete("This constructor has been deprecated. Please use AssemblyFlagsAttribute(AssemblyNameFlags) instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		[CLSCompliant(false)]
		public AssemblyFlagsAttribute(uint flags)
		{
			this.m_flags = (AssemblyNameFlags)flags;
		}

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x06004478 RID: 17528 RVA: 0x000FC31D File Offset: 0x000FA51D
		[Obsolete("This property has been deprecated. Please use AssemblyFlags instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		[CLSCompliant(false)]
		public uint Flags
		{
			get
			{
				return (uint)this.m_flags;
			}
		}

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06004479 RID: 17529 RVA: 0x000FC325 File Offset: 0x000FA525
		[__DynamicallyInvokable]
		public int AssemblyFlags
		{
			[__DynamicallyInvokable]
			get
			{
				return (int)this.m_flags;
			}
		}

		// Token: 0x0600447A RID: 17530 RVA: 0x000FC32D File Offset: 0x000FA52D
		[Obsolete("This constructor has been deprecated. Please use AssemblyFlagsAttribute(AssemblyNameFlags) instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		public AssemblyFlagsAttribute(int assemblyFlags)
		{
			this.m_flags = (AssemblyNameFlags)assemblyFlags;
		}

		// Token: 0x0600447B RID: 17531 RVA: 0x000FC33C File Offset: 0x000FA53C
		[__DynamicallyInvokable]
		public AssemblyFlagsAttribute(AssemblyNameFlags assemblyFlags)
		{
			this.m_flags = assemblyFlags;
		}

		// Token: 0x04001C14 RID: 7188
		private AssemblyNameFlags m_flags;
	}
}
