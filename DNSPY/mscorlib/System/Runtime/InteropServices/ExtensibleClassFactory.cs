using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000965 RID: 2405
	[ComVisible(true)]
	public sealed class ExtensibleClassFactory
	{
		// Token: 0x0600622A RID: 25130 RVA: 0x0014F591 File Offset: 0x0014D791
		private ExtensibleClassFactory()
		{
		}

		// Token: 0x0600622B RID: 25131
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void RegisterObjectCreationCallback(ObjectCreationDelegate callback);
	}
}
