using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200078B RID: 1931
	internal sealed class BinaryCrossAppDomainString : IStreamable
	{
		// Token: 0x060053F9 RID: 21497 RVA: 0x00127B18 File Offset: 0x00125D18
		internal BinaryCrossAppDomainString()
		{
		}

		// Token: 0x060053FA RID: 21498 RVA: 0x00127B20 File Offset: 0x00125D20
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(19);
			sout.WriteInt32(this.objectId);
			sout.WriteInt32(this.value);
		}

		// Token: 0x060053FB RID: 21499 RVA: 0x00127B42 File Offset: 0x00125D42
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.objectId = input.ReadInt32();
			this.value = input.ReadInt32();
		}

		// Token: 0x060053FC RID: 21500 RVA: 0x00127B5C File Offset: 0x00125D5C
		public void Dump()
		{
		}

		// Token: 0x060053FD RID: 21501 RVA: 0x00127B5E File Offset: 0x00125D5E
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x040025EA RID: 9706
		internal int objectId;

		// Token: 0x040025EB RID: 9707
		internal int value;
	}
}
