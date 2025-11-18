using System;

namespace System.Reflection.Emit
{
	// Token: 0x02000652 RID: 1618
	internal sealed class InternalModuleBuilder : RuntimeModule
	{
		// Token: 0x06004C0F RID: 19471 RVA: 0x00112FE1 File Offset: 0x001111E1
		private InternalModuleBuilder()
		{
		}

		// Token: 0x06004C10 RID: 19472 RVA: 0x00112FE9 File Offset: 0x001111E9
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is InternalModuleBuilder)
			{
				return this == obj;
			}
			return obj.Equals(this);
		}

		// Token: 0x06004C11 RID: 19473 RVA: 0x00113004 File Offset: 0x00111204
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
