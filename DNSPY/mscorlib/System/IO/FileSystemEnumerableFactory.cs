using System;
using System.Collections.Generic;

namespace System.IO
{
	// Token: 0x0200018D RID: 397
	internal static class FileSystemEnumerableFactory
	{
		// Token: 0x0600188B RID: 6283 RVA: 0x0005037C File Offset: 0x0004E57C
		internal static IEnumerable<string> CreateFileNameIterator(string path, string originalUserPath, string searchPattern, bool includeFiles, bool includeDirs, SearchOption searchOption, bool checkHost)
		{
			SearchResultHandler<string> searchResultHandler = new StringResultHandler(includeFiles, includeDirs);
			return new FileSystemEnumerableIterator<string>(path, originalUserPath, searchPattern, searchOption, searchResultHandler, checkHost);
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x000503A0 File Offset: 0x0004E5A0
		internal static IEnumerable<FileInfo> CreateFileInfoIterator(string path, string originalUserPath, string searchPattern, SearchOption searchOption)
		{
			SearchResultHandler<FileInfo> searchResultHandler = new FileInfoResultHandler();
			return new FileSystemEnumerableIterator<FileInfo>(path, originalUserPath, searchPattern, searchOption, searchResultHandler, true);
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x000503C0 File Offset: 0x0004E5C0
		internal static IEnumerable<DirectoryInfo> CreateDirectoryInfoIterator(string path, string originalUserPath, string searchPattern, SearchOption searchOption)
		{
			SearchResultHandler<DirectoryInfo> searchResultHandler = new DirectoryInfoResultHandler();
			return new FileSystemEnumerableIterator<DirectoryInfo>(path, originalUserPath, searchPattern, searchOption, searchResultHandler, true);
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x000503E0 File Offset: 0x0004E5E0
		internal static IEnumerable<FileSystemInfo> CreateFileSystemInfoIterator(string path, string originalUserPath, string searchPattern, SearchOption searchOption)
		{
			SearchResultHandler<FileSystemInfo> searchResultHandler = new FileSystemInfoResultHandler();
			return new FileSystemEnumerableIterator<FileSystemInfo>(path, originalUserPath, searchPattern, searchOption, searchResultHandler, true);
		}
	}
}
