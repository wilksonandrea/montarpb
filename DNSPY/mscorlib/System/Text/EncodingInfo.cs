using System;

namespace System.Text
{
	// Token: 0x02000A76 RID: 2678
	[Serializable]
	public sealed class EncodingInfo
	{
		// Token: 0x0600687F RID: 26751 RVA: 0x00160932 File Offset: 0x0015EB32
		internal EncodingInfo(int codePage, string name, string displayName)
		{
			this.iCodePage = codePage;
			this.strEncodingName = name;
			this.strDisplayName = displayName;
		}

		// Token: 0x170011DC RID: 4572
		// (get) Token: 0x06006880 RID: 26752 RVA: 0x0016094F File Offset: 0x0015EB4F
		public int CodePage
		{
			get
			{
				return this.iCodePage;
			}
		}

		// Token: 0x170011DD RID: 4573
		// (get) Token: 0x06006881 RID: 26753 RVA: 0x00160957 File Offset: 0x0015EB57
		public string Name
		{
			get
			{
				return this.strEncodingName;
			}
		}

		// Token: 0x170011DE RID: 4574
		// (get) Token: 0x06006882 RID: 26754 RVA: 0x0016095F File Offset: 0x0015EB5F
		public string DisplayName
		{
			get
			{
				return this.strDisplayName;
			}
		}

		// Token: 0x06006883 RID: 26755 RVA: 0x00160967 File Offset: 0x0015EB67
		public Encoding GetEncoding()
		{
			return Encoding.GetEncoding(this.iCodePage);
		}

		// Token: 0x06006884 RID: 26756 RVA: 0x00160974 File Offset: 0x0015EB74
		public override bool Equals(object value)
		{
			EncodingInfo encodingInfo = value as EncodingInfo;
			return encodingInfo != null && this.CodePage == encodingInfo.CodePage;
		}

		// Token: 0x06006885 RID: 26757 RVA: 0x0016099B File Offset: 0x0015EB9B
		public override int GetHashCode()
		{
			return this.CodePage;
		}

		// Token: 0x04002EBD RID: 11965
		private int iCodePage;

		// Token: 0x04002EBE RID: 11966
		private string strEncodingName;

		// Token: 0x04002EBF RID: 11967
		private string strDisplayName;
	}
}
