using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005EC RID: 1516
	[ComVisible(true)]
	public interface ICustomAttributeProvider
	{
		// Token: 0x06004655 RID: 18005
		object[] GetCustomAttributes(Type attributeType, bool inherit);

		// Token: 0x06004656 RID: 18006
		object[] GetCustomAttributes(bool inherit);

		// Token: 0x06004657 RID: 18007
		bool IsDefined(Type attributeType, bool inherit);
	}
}
