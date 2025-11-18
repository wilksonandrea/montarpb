using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008BC RID: 2236
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
	[FriendAccessAllowed]
	internal sealed class FriendAccessAllowedAttribute : Attribute
	{
		// Token: 0x06005DB8 RID: 23992 RVA: 0x00149633 File Offset: 0x00147833
		public FriendAccessAllowedAttribute()
		{
		}
	}
}
