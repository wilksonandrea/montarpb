using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	// Token: 0x020001D3 RID: 467
	[ComVisible(true)]
	public interface ISecurityEncodable
	{
		// Token: 0x06001C77 RID: 7287
		SecurityElement ToXml();

		// Token: 0x06001C78 RID: 7288
		void FromXml(SecurityElement e);
	}
}
