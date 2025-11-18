using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200078C RID: 1932
	internal sealed class BinaryCrossAppDomainMap : IStreamable
	{
		// Token: 0x060053FE RID: 21502 RVA: 0x00127B6B File Offset: 0x00125D6B
		internal BinaryCrossAppDomainMap()
		{
		}

		// Token: 0x060053FF RID: 21503 RVA: 0x00127B73 File Offset: 0x00125D73
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(18);
			sout.WriteInt32(this.crossAppDomainArrayIndex);
		}

		// Token: 0x06005400 RID: 21504 RVA: 0x00127B89 File Offset: 0x00125D89
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.crossAppDomainArrayIndex = input.ReadInt32();
		}

		// Token: 0x06005401 RID: 21505 RVA: 0x00127B97 File Offset: 0x00125D97
		public void Dump()
		{
		}

		// Token: 0x06005402 RID: 21506 RVA: 0x00127B99 File Offset: 0x00125D99
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x040025EC RID: 9708
		internal int crossAppDomainArrayIndex;
	}
}
