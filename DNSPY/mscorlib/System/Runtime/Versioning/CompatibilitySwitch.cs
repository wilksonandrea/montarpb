using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.Versioning
{
	// Token: 0x02000729 RID: 1833
	public static class CompatibilitySwitch
	{
		// Token: 0x06005170 RID: 20848 RVA: 0x0011EF02 File Offset: 0x0011D102
		[SecurityCritical]
		public static bool IsEnabled(string compatibilitySwitchName)
		{
			return CompatibilitySwitch.IsEnabledInternalCall(compatibilitySwitchName, true);
		}

		// Token: 0x06005171 RID: 20849 RVA: 0x0011EF0B File Offset: 0x0011D10B
		[SecurityCritical]
		public static string GetValue(string compatibilitySwitchName)
		{
			return CompatibilitySwitch.GetValueInternalCall(compatibilitySwitchName, true);
		}

		// Token: 0x06005172 RID: 20850 RVA: 0x0011EF14 File Offset: 0x0011D114
		[SecurityCritical]
		internal static bool IsEnabledInternal(string compatibilitySwitchName)
		{
			return CompatibilitySwitch.IsEnabledInternalCall(compatibilitySwitchName, false);
		}

		// Token: 0x06005173 RID: 20851 RVA: 0x0011EF1D File Offset: 0x0011D11D
		[SecurityCritical]
		internal static string GetValueInternal(string compatibilitySwitchName)
		{
			return CompatibilitySwitch.GetValueInternalCall(compatibilitySwitchName, false);
		}

		// Token: 0x06005174 RID: 20852
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetAppContextOverridesInternalCall();

		// Token: 0x06005175 RID: 20853
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsEnabledInternalCall(string compatibilitySwitchName, bool onlyDB);

		// Token: 0x06005176 RID: 20854
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetValueInternalCall(string compatibilitySwitchName, bool onlyDB);
	}
}
