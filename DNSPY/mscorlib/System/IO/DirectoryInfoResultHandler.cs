using System;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32;

namespace System.IO
{
	// Token: 0x02000193 RID: 403
	internal class DirectoryInfoResultHandler : SearchResultHandler<DirectoryInfo>
	{
		// Token: 0x060018AF RID: 6319 RVA: 0x00050CBB File Offset: 0x0004EEBB
		[SecurityCritical]
		internal override bool IsResultIncluded(ref Win32Native.WIN32_FIND_DATA findData)
		{
			return findData.IsNormalDirectory;
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x00050CC3 File Offset: 0x0004EEC3
		[SecurityCritical]
		internal override DirectoryInfo CreateObject(Directory.SearchData searchData, ref Win32Native.WIN32_FIND_DATA findData)
		{
			return DirectoryInfoResultHandler.CreateDirectoryInfo(searchData, ref findData);
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x00050CCC File Offset: 0x0004EECC
		[SecurityCritical]
		internal static DirectoryInfo CreateDirectoryInfo(Directory.SearchData searchData, ref Win32Native.WIN32_FIND_DATA findData)
		{
			string cFileName = findData.cFileName;
			string text = Path.CombineNoChecks(searchData.fullPath, cFileName);
			if (!CodeAccessSecurityEngine.QuickCheckForAllDemands())
			{
				new FileIOPermission(FileIOPermissionAccess.Read, new string[] { text + "\\." }, false, false).Demand();
			}
			DirectoryInfo directoryInfo = new DirectoryInfo(text, cFileName);
			directoryInfo.InitializeFrom(ref findData);
			return directoryInfo;
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x00050D25 File Offset: 0x0004EF25
		public DirectoryInfoResultHandler()
		{
		}
	}
}
