using System;

namespace System.Globalization
{
	// Token: 0x020003A0 RID: 928
	internal sealed class AppDomainSortingSetupInfo
	{
		// Token: 0x06002DA2 RID: 11682 RVA: 0x000AE8C1 File Offset: 0x000ACAC1
		internal AppDomainSortingSetupInfo()
		{
		}

		// Token: 0x06002DA3 RID: 11683 RVA: 0x000AE8CC File Offset: 0x000ACACC
		internal AppDomainSortingSetupInfo(AppDomainSortingSetupInfo copy)
		{
			this._useV2LegacySorting = copy._useV2LegacySorting;
			this._useV4LegacySorting = copy._useV4LegacySorting;
			this._pfnIsNLSDefinedString = copy._pfnIsNLSDefinedString;
			this._pfnCompareStringEx = copy._pfnCompareStringEx;
			this._pfnLCMapStringEx = copy._pfnLCMapStringEx;
			this._pfnFindNLSStringEx = copy._pfnFindNLSStringEx;
			this._pfnFindStringOrdinal = copy._pfnFindStringOrdinal;
			this._pfnCompareStringOrdinal = copy._pfnCompareStringOrdinal;
			this._pfnGetNLSVersionEx = copy._pfnGetNLSVersionEx;
		}

		// Token: 0x04001297 RID: 4759
		internal IntPtr _pfnIsNLSDefinedString;

		// Token: 0x04001298 RID: 4760
		internal IntPtr _pfnCompareStringEx;

		// Token: 0x04001299 RID: 4761
		internal IntPtr _pfnLCMapStringEx;

		// Token: 0x0400129A RID: 4762
		internal IntPtr _pfnFindNLSStringEx;

		// Token: 0x0400129B RID: 4763
		internal IntPtr _pfnCompareStringOrdinal;

		// Token: 0x0400129C RID: 4764
		internal IntPtr _pfnGetNLSVersionEx;

		// Token: 0x0400129D RID: 4765
		internal IntPtr _pfnFindStringOrdinal;

		// Token: 0x0400129E RID: 4766
		internal bool _useV2LegacySorting;

		// Token: 0x0400129F RID: 4767
		internal bool _useV4LegacySorting;
	}
}
