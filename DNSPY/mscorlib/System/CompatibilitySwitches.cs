using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x020000C0 RID: 192
	[FriendAccessAllowed]
	internal static class CompatibilitySwitches
	{
		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000B03 RID: 2819 RVA: 0x00022CB2 File Offset: 0x00020EB2
		public static bool IsCompatibilityBehaviorDefined
		{
			get
			{
				return CompatibilitySwitches.s_AreSwitchesSet;
			}
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x00022CBC File Offset: 0x00020EBC
		private static bool IsCompatibilitySwitchSet(string compatibilitySwitch)
		{
			bool? flag = AppDomain.CurrentDomain.IsCompatibilitySwitchSet(compatibilitySwitch);
			return flag != null && flag.Value;
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x00022CE7 File Offset: 0x00020EE7
		internal static void InitializeSwitches()
		{
			CompatibilitySwitches.s_isNetFx40TimeSpanLegacyFormatMode = CompatibilitySwitches.IsCompatibilitySwitchSet("NetFx40_TimeSpanLegacyFormatMode");
			CompatibilitySwitches.s_isNetFx40LegacySecurityPolicy = CompatibilitySwitches.IsCompatibilitySwitchSet("NetFx40_LegacySecurityPolicy");
			CompatibilitySwitches.s_isNetFx45LegacyManagedDeflateStream = CompatibilitySwitches.IsCompatibilitySwitchSet("NetFx45_LegacyManagedDeflateStream");
			CompatibilitySwitches.s_AreSwitchesSet = true;
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000B06 RID: 2822 RVA: 0x00022D1C File Offset: 0x00020F1C
		public static bool IsAppEarlierThanSilverlight4
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x00022D1F File Offset: 0x00020F1F
		public static bool IsAppEarlierThanWindowsPhone8
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000B08 RID: 2824 RVA: 0x00022D22 File Offset: 0x00020F22
		public static bool IsAppEarlierThanWindowsPhoneMango
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000B09 RID: 2825 RVA: 0x00022D25 File Offset: 0x00020F25
		public static bool IsNetFx40TimeSpanLegacyFormatMode
		{
			get
			{
				return CompatibilitySwitches.s_isNetFx40TimeSpanLegacyFormatMode;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000B0A RID: 2826 RVA: 0x00022D2C File Offset: 0x00020F2C
		public static bool IsNetFx40LegacySecurityPolicy
		{
			get
			{
				return CompatibilitySwitches.s_isNetFx40LegacySecurityPolicy;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x00022D33 File Offset: 0x00020F33
		public static bool IsNetFx45LegacyManagedDeflateStream
		{
			get
			{
				return CompatibilitySwitches.s_isNetFx45LegacyManagedDeflateStream;
			}
		}

		// Token: 0x04000460 RID: 1120
		private static bool s_AreSwitchesSet;

		// Token: 0x04000461 RID: 1121
		private static bool s_isNetFx40TimeSpanLegacyFormatMode;

		// Token: 0x04000462 RID: 1122
		private static bool s_isNetFx40LegacySecurityPolicy;

		// Token: 0x04000463 RID: 1123
		private static bool s_isNetFx45LegacyManagedDeflateStream;
	}
}
