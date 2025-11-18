using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	// Token: 0x02000358 RID: 856
	[ComVisible(true)]
	public interface IMembershipCondition : ISecurityEncodable, ISecurityPolicyEncodable
	{
		// Token: 0x06002A77 RID: 10871
		bool Check(Evidence evidence);

		// Token: 0x06002A78 RID: 10872
		IMembershipCondition Copy();

		// Token: 0x06002A79 RID: 10873
		string ToString();

		// Token: 0x06002A7A RID: 10874
		bool Equals(object obj);
	}
}
