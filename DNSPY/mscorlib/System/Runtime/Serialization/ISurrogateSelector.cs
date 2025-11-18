using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Serialization
{
	// Token: 0x02000739 RID: 1849
	[ComVisible(true)]
	public interface ISurrogateSelector
	{
		// Token: 0x060051CA RID: 20938
		[SecurityCritical]
		void ChainSelector(ISurrogateSelector selector);

		// Token: 0x060051CB RID: 20939
		[SecurityCritical]
		ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector selector);

		// Token: 0x060051CC RID: 20940
		[SecurityCritical]
		ISurrogateSelector GetNextSelector();
	}
}
