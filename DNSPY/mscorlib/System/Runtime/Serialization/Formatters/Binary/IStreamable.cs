using System;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000782 RID: 1922
	internal interface IStreamable
	{
		// Token: 0x060053C7 RID: 21447
		[SecurityCritical]
		void Read(__BinaryParser input);

		// Token: 0x060053C8 RID: 21448
		void Write(__BinaryWriter sout);
	}
}
