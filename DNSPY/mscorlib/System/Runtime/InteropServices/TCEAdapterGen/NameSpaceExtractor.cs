using System;

namespace System.Runtime.InteropServices.TCEAdapterGen
{
	// Token: 0x020009C6 RID: 2502
	internal static class NameSpaceExtractor
	{
		// Token: 0x060063BC RID: 25532 RVA: 0x001543CC File Offset: 0x001525CC
		public static string ExtractNameSpace(string FullyQualifiedTypeName)
		{
			int num = FullyQualifiedTypeName.LastIndexOf(NameSpaceExtractor.NameSpaceSeperator);
			if (num == -1)
			{
				return "";
			}
			return FullyQualifiedTypeName.Substring(0, num);
		}

		// Token: 0x060063BD RID: 25533 RVA: 0x001543F7 File Offset: 0x001525F7
		// Note: this type is marked as 'beforefieldinit'.
		static NameSpaceExtractor()
		{
		}

		// Token: 0x04002CDD RID: 11485
		private static char NameSpaceSeperator = '.';
	}
}
