using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Threading
{
	// Token: 0x020004FF RID: 1279
	[TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public class LockRecursionException : Exception
	{
		// Token: 0x06003C5F RID: 15455 RVA: 0x000E40A5 File Offset: 0x000E22A5
		[__DynamicallyInvokable]
		public LockRecursionException()
		{
		}

		// Token: 0x06003C60 RID: 15456 RVA: 0x000E40AD File Offset: 0x000E22AD
		[__DynamicallyInvokable]
		public LockRecursionException(string message)
			: base(message)
		{
		}

		// Token: 0x06003C61 RID: 15457 RVA: 0x000E40B6 File Offset: 0x000E22B6
		protected LockRecursionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06003C62 RID: 15458 RVA: 0x000E40C0 File Offset: 0x000E22C0
		[__DynamicallyInvokable]
		public LockRecursionException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
