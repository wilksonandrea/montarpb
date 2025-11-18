using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Collections
{
	// Token: 0x0200048B RID: 1163
	[Obsolete("Please use StringComparer instead.")]
	[ComVisible(true)]
	[Serializable]
	public class CaseInsensitiveHashCodeProvider : IHashCodeProvider
	{
		// Token: 0x06003776 RID: 14198 RVA: 0x000D53B8 File Offset: 0x000D35B8
		public CaseInsensitiveHashCodeProvider()
		{
			this.m_text = CultureInfo.CurrentCulture.TextInfo;
		}

		// Token: 0x06003777 RID: 14199 RVA: 0x000D53D0 File Offset: 0x000D35D0
		public CaseInsensitiveHashCodeProvider(CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			this.m_text = culture.TextInfo;
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06003778 RID: 14200 RVA: 0x000D53F2 File Offset: 0x000D35F2
		public static CaseInsensitiveHashCodeProvider Default
		{
			get
			{
				return new CaseInsensitiveHashCodeProvider(CultureInfo.CurrentCulture);
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06003779 RID: 14201 RVA: 0x000D53FE File Offset: 0x000D35FE
		public static CaseInsensitiveHashCodeProvider DefaultInvariant
		{
			get
			{
				if (CaseInsensitiveHashCodeProvider.m_InvariantCaseInsensitiveHashCodeProvider == null)
				{
					CaseInsensitiveHashCodeProvider.m_InvariantCaseInsensitiveHashCodeProvider = new CaseInsensitiveHashCodeProvider(CultureInfo.InvariantCulture);
				}
				return CaseInsensitiveHashCodeProvider.m_InvariantCaseInsensitiveHashCodeProvider;
			}
		}

		// Token: 0x0600377A RID: 14202 RVA: 0x000D5424 File Offset: 0x000D3624
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			string text = obj as string;
			if (text == null)
			{
				return obj.GetHashCode();
			}
			return this.m_text.GetCaseInsensitiveHashCode(text);
		}

		// Token: 0x040018B7 RID: 6327
		private TextInfo m_text;

		// Token: 0x040018B8 RID: 6328
		private static volatile CaseInsensitiveHashCodeProvider m_InvariantCaseInsensitiveHashCodeProvider;
	}
}
