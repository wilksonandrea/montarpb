using System;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace System.Security
{
	// Token: 0x020001D4 RID: 468
	[ComVisible(true)]
	public interface ISecurityPolicyEncodable
	{
		// Token: 0x06001C79 RID: 7289
		SecurityElement ToXml(PolicyLevel level);

		// Token: 0x06001C7A RID: 7290
		void FromXml(SecurityElement e, PolicyLevel level);
	}
}
