using System;
using System.Collections;
using System.Globalization;

namespace System.Security.Util
{
	// Token: 0x0200037C RID: 892
	[Serializable]
	internal class SiteString
	{
		// Token: 0x06002C32 RID: 11314 RVA: 0x000A44E4 File Offset: 0x000A26E4
		protected internal SiteString()
		{
		}

		// Token: 0x06002C33 RID: 11315 RVA: 0x000A44EC File Offset: 0x000A26EC
		public SiteString(string site)
		{
			this.m_separatedSite = SiteString.CreateSeparatedSite(site);
			this.m_site = site;
		}

		// Token: 0x06002C34 RID: 11316 RVA: 0x000A4507 File Offset: 0x000A2707
		private SiteString(string site, ArrayList separatedSite)
		{
			this.m_separatedSite = separatedSite;
			this.m_site = site;
		}

		// Token: 0x06002C35 RID: 11317 RVA: 0x000A4520 File Offset: 0x000A2720
		private static ArrayList CreateSeparatedSite(string site)
		{
			if (site == null || site.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSite"));
			}
			ArrayList arrayList = new ArrayList();
			int num = -1;
			int num2 = site.IndexOf('[');
			if (num2 == 0)
			{
				num = site.IndexOf(']', num2 + 1);
			}
			if (num != -1)
			{
				string text = site.Substring(num2 + 1, num - num2 - 1);
				arrayList.Add(text);
				return arrayList;
			}
			string[] array = site.Split(SiteString.m_separators);
			for (int i = array.Length - 1; i > -1; i--)
			{
				if (array[i] == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSite"));
				}
				if (array[i].Equals(""))
				{
					if (i != array.Length - 1)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSite"));
					}
				}
				else if (array[i].Equals("*"))
				{
					if (i != 0)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSite"));
					}
					arrayList.Add(array[i]);
				}
				else
				{
					if (!SiteString.AllLegalCharacters(array[i]))
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSite"));
					}
					arrayList.Add(array[i]);
				}
			}
			return arrayList;
		}

		// Token: 0x06002C36 RID: 11318 RVA: 0x000A4648 File Offset: 0x000A2848
		private static bool AllLegalCharacters(string str)
		{
			foreach (char c in str)
			{
				if (!SiteString.IsLegalDNSChar(c) && !SiteString.IsNetbiosSplChar(c))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002C37 RID: 11319 RVA: 0x000A4681 File Offset: 0x000A2881
		private static bool IsLegalDNSChar(char c)
		{
			return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9') || c == '-';
		}

		// Token: 0x06002C38 RID: 11320 RVA: 0x000A46AC File Offset: 0x000A28AC
		private static bool IsNetbiosSplChar(char c)
		{
			if (c <= '@')
			{
				switch (c)
				{
				case '!':
				case '#':
				case '$':
				case '%':
				case '&':
				case '\'':
				case '(':
				case ')':
				case '-':
				case '.':
					break;
				case '"':
				case '*':
				case '+':
				case ',':
					return false;
				default:
					if (c != '@')
					{
						return false;
					}
					break;
				}
			}
			else if (c != '^' && c != '_')
			{
				switch (c)
				{
				case '{':
				case '}':
				case '~':
					break;
				case '|':
					return false;
				default:
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002C39 RID: 11321 RVA: 0x000A472E File Offset: 0x000A292E
		public override string ToString()
		{
			return this.m_site;
		}

		// Token: 0x06002C3A RID: 11322 RVA: 0x000A4736 File Offset: 0x000A2936
		public override bool Equals(object o)
		{
			return o != null && o is SiteString && this.Equals((SiteString)o, true);
		}

		// Token: 0x06002C3B RID: 11323 RVA: 0x000A4754 File Offset: 0x000A2954
		public override int GetHashCode()
		{
			TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
			return textInfo.GetCaseInsensitiveHashCode(this.m_site);
		}

		// Token: 0x06002C3C RID: 11324 RVA: 0x000A4778 File Offset: 0x000A2978
		internal bool Equals(SiteString ss, bool ignoreCase)
		{
			if (this.m_site == null)
			{
				return ss.m_site == null;
			}
			return ss.m_site != null && this.IsSubsetOf(ss, ignoreCase) && ss.IsSubsetOf(this, ignoreCase);
		}

		// Token: 0x06002C3D RID: 11325 RVA: 0x000A47AA File Offset: 0x000A29AA
		public virtual SiteString Copy()
		{
			return new SiteString(this.m_site, this.m_separatedSite);
		}

		// Token: 0x06002C3E RID: 11326 RVA: 0x000A47BD File Offset: 0x000A29BD
		public virtual bool IsSubsetOf(SiteString operand)
		{
			return this.IsSubsetOf(operand, true);
		}

		// Token: 0x06002C3F RID: 11327 RVA: 0x000A47C8 File Offset: 0x000A29C8
		public virtual bool IsSubsetOf(SiteString operand, bool ignoreCase)
		{
			StringComparison stringComparison = (ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
			if (operand == null)
			{
				return false;
			}
			if (this.m_separatedSite.Count == operand.m_separatedSite.Count && this.m_separatedSite.Count == 0)
			{
				return true;
			}
			if (this.m_separatedSite.Count < operand.m_separatedSite.Count - 1)
			{
				return false;
			}
			if (this.m_separatedSite.Count > operand.m_separatedSite.Count && operand.m_separatedSite.Count > 0 && !operand.m_separatedSite[operand.m_separatedSite.Count - 1].Equals("*"))
			{
				return false;
			}
			if (string.Compare(this.m_site, operand.m_site, stringComparison) == 0)
			{
				return true;
			}
			for (int i = 0; i < operand.m_separatedSite.Count - 1; i++)
			{
				if (string.Compare((string)this.m_separatedSite[i], (string)operand.m_separatedSite[i], stringComparison) != 0)
				{
					return false;
				}
			}
			if (this.m_separatedSite.Count < operand.m_separatedSite.Count)
			{
				return operand.m_separatedSite[operand.m_separatedSite.Count - 1].Equals("*");
			}
			return this.m_separatedSite.Count != operand.m_separatedSite.Count || string.Compare((string)this.m_separatedSite[this.m_separatedSite.Count - 1], (string)operand.m_separatedSite[this.m_separatedSite.Count - 1], stringComparison) == 0 || operand.m_separatedSite[operand.m_separatedSite.Count - 1].Equals("*");
		}

		// Token: 0x06002C40 RID: 11328 RVA: 0x000A4986 File Offset: 0x000A2B86
		public virtual SiteString Intersect(SiteString operand)
		{
			if (operand == null)
			{
				return null;
			}
			if (this.IsSubsetOf(operand))
			{
				return this.Copy();
			}
			if (operand.IsSubsetOf(this))
			{
				return operand.Copy();
			}
			return null;
		}

		// Token: 0x06002C41 RID: 11329 RVA: 0x000A49AE File Offset: 0x000A2BAE
		public virtual SiteString Union(SiteString operand)
		{
			if (operand == null)
			{
				return this;
			}
			if (this.IsSubsetOf(operand))
			{
				return operand.Copy();
			}
			if (operand.IsSubsetOf(this))
			{
				return this.Copy();
			}
			return null;
		}

		// Token: 0x06002C42 RID: 11330 RVA: 0x000A49D6 File Offset: 0x000A2BD6
		// Note: this type is marked as 'beforefieldinit'.
		static SiteString()
		{
		}

		// Token: 0x040011C6 RID: 4550
		protected string m_site;

		// Token: 0x040011C7 RID: 4551
		protected ArrayList m_separatedSite;

		// Token: 0x040011C8 RID: 4552
		protected static char[] m_separators = new char[] { '.' };
	}
}
