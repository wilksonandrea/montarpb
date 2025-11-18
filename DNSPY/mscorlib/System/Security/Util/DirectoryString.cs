using System;
using System.Collections;

namespace System.Security.Util
{
	// Token: 0x02000381 RID: 897
	[Serializable]
	internal class DirectoryString : SiteString
	{
		// Token: 0x06002CA2 RID: 11426 RVA: 0x000A6F1F File Offset: 0x000A511F
		public DirectoryString()
		{
			this.m_site = "";
			this.m_separatedSite = new ArrayList();
		}

		// Token: 0x06002CA3 RID: 11427 RVA: 0x000A6F3D File Offset: 0x000A513D
		public DirectoryString(string directory, bool checkForIllegalChars)
		{
			this.m_site = directory;
			this.m_checkForIllegalChars = checkForIllegalChars;
			this.m_separatedSite = this.CreateSeparatedString(directory);
		}

		// Token: 0x06002CA4 RID: 11428 RVA: 0x000A6F60 File Offset: 0x000A5160
		private ArrayList CreateSeparatedString(string directory)
		{
			if (directory == null || directory.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidDirectoryOnUrl"));
			}
			ArrayList arrayList = new ArrayList();
			string[] array = directory.Split(DirectoryString.m_separators);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null && !array[i].Equals(""))
				{
					if (array[i].Equals("*"))
					{
						if (i != array.Length - 1)
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_InvalidDirectoryOnUrl"));
						}
						arrayList.Add(array[i]);
					}
					else
					{
						if (this.m_checkForIllegalChars && array[i].IndexOfAny(DirectoryString.m_illegalDirectoryCharacters) != -1)
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_InvalidDirectoryOnUrl"));
						}
						arrayList.Add(array[i]);
					}
				}
			}
			return arrayList;
		}

		// Token: 0x06002CA5 RID: 11429 RVA: 0x000A7025 File Offset: 0x000A5225
		public virtual bool IsSubsetOf(DirectoryString operand)
		{
			return this.IsSubsetOf(operand, true);
		}

		// Token: 0x06002CA6 RID: 11430 RVA: 0x000A7030 File Offset: 0x000A5230
		public virtual bool IsSubsetOf(DirectoryString operand, bool ignoreCase)
		{
			if (operand == null)
			{
				return false;
			}
			if (operand.m_separatedSite.Count == 0)
			{
				return this.m_separatedSite.Count == 0 || (this.m_separatedSite.Count > 0 && string.Compare((string)this.m_separatedSite[0], "*", StringComparison.Ordinal) == 0);
			}
			if (this.m_separatedSite.Count == 0)
			{
				return string.Compare((string)operand.m_separatedSite[0], "*", StringComparison.Ordinal) == 0;
			}
			return base.IsSubsetOf(operand, ignoreCase);
		}

		// Token: 0x06002CA7 RID: 11431 RVA: 0x000A70C3 File Offset: 0x000A52C3
		// Note: this type is marked as 'beforefieldinit'.
		static DirectoryString()
		{
		}

		// Token: 0x040011E8 RID: 4584
		private bool m_checkForIllegalChars;

		// Token: 0x040011E9 RID: 4585
		private new static char[] m_separators = new char[] { '/' };

		// Token: 0x040011EA RID: 4586
		protected static char[] m_illegalDirectoryCharacters = new char[] { '\\', ':', '*', '?', '"', '<', '>', '|' };
	}
}
