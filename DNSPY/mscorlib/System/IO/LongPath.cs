using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.IO
{
	// Token: 0x020001AB RID: 427
	[ComVisible(false)]
	internal static class LongPath
	{
		// Token: 0x06001ADF RID: 6879 RVA: 0x0005A326 File Offset: 0x00058526
		[SecurityCritical]
		internal static string NormalizePath(string path)
		{
			return LongPath.NormalizePath(path, true);
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x0005A32F File Offset: 0x0005852F
		[SecurityCritical]
		internal static string NormalizePath(string path, bool fullCheck)
		{
			return Path.NormalizePath(path, fullCheck, 32767);
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x0005A340 File Offset: 0x00058540
		internal static string InternalCombine(string path1, string path2)
		{
			bool flag;
			string text = LongPath.TryRemoveLongPathPrefix(path1, out flag);
			string text2 = Path.InternalCombine(text, path2);
			if (flag)
			{
				text2 = Path.AddLongPathPrefix(text2);
			}
			return text2;
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x0005A36C File Offset: 0x0005856C
		internal static int GetRootLength(string path)
		{
			bool flag;
			string text = LongPath.TryRemoveLongPathPrefix(path, out flag);
			int num = Path.GetRootLength(text);
			if (flag)
			{
				num += 4;
			}
			return num;
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x0005A394 File Offset: 0x00058594
		internal static bool IsPathRooted(string path)
		{
			string text = Path.RemoveLongPathPrefix(path);
			return Path.IsPathRooted(text);
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x0005A3B0 File Offset: 0x000585B0
		[SecurityCritical]
		internal static string GetPathRoot(string path)
		{
			if (path == null)
			{
				return null;
			}
			bool flag;
			string text = LongPath.TryRemoveLongPathPrefix(path, out flag);
			text = LongPath.NormalizePath(text, false);
			string text2 = path.Substring(0, LongPath.GetRootLength(text));
			if (flag)
			{
				text2 = Path.AddLongPathPrefix(text2);
			}
			return text2;
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x0005A3EC File Offset: 0x000585EC
		[SecurityCritical]
		internal static string GetDirectoryName(string path)
		{
			if (path != null)
			{
				bool flag;
				string text = LongPath.TryRemoveLongPathPrefix(path, out flag);
				Path.CheckInvalidPathChars(text, false);
				path = LongPath.NormalizePath(text, false);
				int rootLength = LongPath.GetRootLength(text);
				int num = text.Length;
				if (num > rootLength)
				{
					num = text.Length;
					if (num == rootLength)
					{
						return null;
					}
					while (num > rootLength && text[--num] != Path.DirectorySeparatorChar && text[num] != Path.AltDirectorySeparatorChar)
					{
					}
					string text2 = text.Substring(0, num);
					if (flag)
					{
						text2 = Path.AddLongPathPrefix(text2);
					}
					return text2;
				}
			}
			return null;
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x0005A472 File Offset: 0x00058672
		internal static string TryRemoveLongPathPrefix(string path, out bool removed)
		{
			removed = Path.HasLongPathPrefix(path);
			if (!removed)
			{
				return path;
			}
			return Path.RemoveLongPathPrefix(path);
		}
	}
}
