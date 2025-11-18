using System;

namespace System.Runtime.Versioning
{
	// Token: 0x02000720 RID: 1824
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	public sealed class ComponentGuaranteesAttribute : Attribute
	{
		// Token: 0x0600515B RID: 20827 RVA: 0x0011EB08 File Offset: 0x0011CD08
		public ComponentGuaranteesAttribute(ComponentGuaranteesOptions guarantees)
		{
			this._guarantees = guarantees;
		}

		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x0600515C RID: 20828 RVA: 0x0011EB17 File Offset: 0x0011CD17
		public ComponentGuaranteesOptions Guarantees
		{
			get
			{
				return this._guarantees;
			}
		}

		// Token: 0x04002416 RID: 9238
		private ComponentGuaranteesOptions _guarantees;
	}
}
