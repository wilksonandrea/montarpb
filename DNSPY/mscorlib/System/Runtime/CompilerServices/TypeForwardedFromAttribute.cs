using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008E3 RID: 2275
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false, AllowMultiple = false)]
	[__DynamicallyInvokable]
	public sealed class TypeForwardedFromAttribute : Attribute
	{
		// Token: 0x06005DE1 RID: 24033 RVA: 0x00149820 File Offset: 0x00147A20
		private TypeForwardedFromAttribute()
		{
		}

		// Token: 0x06005DE2 RID: 24034 RVA: 0x00149828 File Offset: 0x00147A28
		[__DynamicallyInvokable]
		public TypeForwardedFromAttribute(string assemblyFullName)
		{
			if (string.IsNullOrEmpty(assemblyFullName))
			{
				throw new ArgumentNullException("assemblyFullName");
			}
			this.assemblyFullName = assemblyFullName;
		}

		// Token: 0x1700101E RID: 4126
		// (get) Token: 0x06005DE3 RID: 24035 RVA: 0x0014984A File Offset: 0x00147A4A
		[__DynamicallyInvokable]
		public string AssemblyFullName
		{
			[__DynamicallyInvokable]
			get
			{
				return this.assemblyFullName;
			}
		}

		// Token: 0x04002A42 RID: 10818
		private string assemblyFullName;
	}
}
