using System;

namespace System.Security
{
	// Token: 0x020001BE RID: 446
	[Serializable]
	internal sealed class SecurityDocumentElement : ISecurityElementFactory
	{
		// Token: 0x06001C00 RID: 7168 RVA: 0x000604C9 File Offset: 0x0005E6C9
		internal SecurityDocumentElement(SecurityDocument document, int position)
		{
			this.m_document = document;
			this.m_position = position;
		}

		// Token: 0x06001C01 RID: 7169 RVA: 0x000604DF File Offset: 0x0005E6DF
		SecurityElement ISecurityElementFactory.CreateSecurityElement()
		{
			return this.m_document.GetElement(this.m_position, true);
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x000604F3 File Offset: 0x0005E6F3
		object ISecurityElementFactory.Copy()
		{
			return new SecurityDocumentElement(this.m_document, this.m_position);
		}

		// Token: 0x06001C03 RID: 7171 RVA: 0x00060506 File Offset: 0x0005E706
		string ISecurityElementFactory.GetTag()
		{
			return this.m_document.GetTagForElement(this.m_position);
		}

		// Token: 0x06001C04 RID: 7172 RVA: 0x00060519 File Offset: 0x0005E719
		string ISecurityElementFactory.Attribute(string attributeName)
		{
			return this.m_document.GetAttributeForElement(this.m_position, attributeName);
		}

		// Token: 0x040009B7 RID: 2487
		private int m_position;

		// Token: 0x040009B8 RID: 2488
		private SecurityDocument m_document;
	}
}
