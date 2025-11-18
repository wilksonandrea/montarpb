using System;

namespace System.Security
{
	// Token: 0x020001BC RID: 444
	internal interface ISecurityElementFactory
	{
		// Token: 0x06001BCF RID: 7119
		SecurityElement CreateSecurityElement();

		// Token: 0x06001BD0 RID: 7120
		object Copy();

		// Token: 0x06001BD1 RID: 7121
		string GetTag();

		// Token: 0x06001BD2 RID: 7122
		string Attribute(string attributeName);
	}
}
