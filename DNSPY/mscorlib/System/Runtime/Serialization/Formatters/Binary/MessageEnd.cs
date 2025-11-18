using System;
using System.Diagnostics;
using System.IO;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000794 RID: 1940
	internal sealed class MessageEnd : IStreamable
	{
		// Token: 0x0600542E RID: 21550 RVA: 0x00128592 File Offset: 0x00126792
		internal MessageEnd()
		{
		}

		// Token: 0x0600542F RID: 21551 RVA: 0x0012859A File Offset: 0x0012679A
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(11);
		}

		// Token: 0x06005430 RID: 21552 RVA: 0x001285A4 File Offset: 0x001267A4
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
		}

		// Token: 0x06005431 RID: 21553 RVA: 0x001285A6 File Offset: 0x001267A6
		public void Dump()
		{
		}

		// Token: 0x06005432 RID: 21554 RVA: 0x001285A8 File Offset: 0x001267A8
		public void Dump(Stream sout)
		{
		}

		// Token: 0x06005433 RID: 21555 RVA: 0x001285AC File Offset: 0x001267AC
		[Conditional("_LOGGING")]
		private void DumpInternal(Stream sout)
		{
			if (BCLDebug.CheckEnabled("BINARY") && sout != null && sout.CanSeek)
			{
				long length = sout.Length;
			}
		}
	}
}
