using System;

namespace System.Security
{
	// Token: 0x020001C8 RID: 456
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	[Obsolete("SecurityTreatAsSafe is only used for .NET 2.0 transparency compatibility.  Please use the SecuritySafeCriticalAttribute instead.")]
	public sealed class SecurityTreatAsSafeAttribute : Attribute
	{
		// Token: 0x06001C25 RID: 7205 RVA: 0x00060C19 File Offset: 0x0005EE19
		public SecurityTreatAsSafeAttribute()
		{
		}
	}
}
