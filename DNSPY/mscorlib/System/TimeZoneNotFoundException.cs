using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System
{
	// Token: 0x02000147 RID: 327
	[TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public class TimeZoneNotFoundException : Exception
	{
		// Token: 0x060013E7 RID: 5095 RVA: 0x0003BF99 File Offset: 0x0003A199
		public TimeZoneNotFoundException(string message)
			: base(message)
		{
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x0003BFA2 File Offset: 0x0003A1A2
		public TimeZoneNotFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x0003BFAC File Offset: 0x0003A1AC
		protected TimeZoneNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x0003BFB6 File Offset: 0x0003A1B6
		public TimeZoneNotFoundException()
		{
		}
	}
}
