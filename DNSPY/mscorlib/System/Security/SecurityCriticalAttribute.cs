using System;

namespace System.Security
{
	// Token: 0x020001C7 RID: 455
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	[__DynamicallyInvokable]
	public sealed class SecurityCriticalAttribute : Attribute
	{
		// Token: 0x06001C22 RID: 7202 RVA: 0x00060BFA File Offset: 0x0005EDFA
		[__DynamicallyInvokable]
		public SecurityCriticalAttribute()
		{
		}

		// Token: 0x06001C23 RID: 7203 RVA: 0x00060C02 File Offset: 0x0005EE02
		public SecurityCriticalAttribute(SecurityCriticalScope scope)
		{
			this._val = scope;
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06001C24 RID: 7204 RVA: 0x00060C11 File Offset: 0x0005EE11
		[Obsolete("SecurityCriticalScope is only used for .NET 2.0 transparency compatibility.")]
		public SecurityCriticalScope Scope
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x040009C6 RID: 2502
		private SecurityCriticalScope _val;
	}
}
