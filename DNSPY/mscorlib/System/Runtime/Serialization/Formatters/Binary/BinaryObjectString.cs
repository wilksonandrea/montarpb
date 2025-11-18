using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200078A RID: 1930
	internal sealed class BinaryObjectString : IStreamable
	{
		// Token: 0x060053F3 RID: 21491 RVA: 0x00127AB6 File Offset: 0x00125CB6
		internal BinaryObjectString()
		{
		}

		// Token: 0x060053F4 RID: 21492 RVA: 0x00127ABE File Offset: 0x00125CBE
		internal void Set(int objectId, string value)
		{
			this.objectId = objectId;
			this.value = value;
		}

		// Token: 0x060053F5 RID: 21493 RVA: 0x00127ACE File Offset: 0x00125CCE
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(6);
			sout.WriteInt32(this.objectId);
			sout.WriteString(this.value);
		}

		// Token: 0x060053F6 RID: 21494 RVA: 0x00127AEF File Offset: 0x00125CEF
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.objectId = input.ReadInt32();
			this.value = input.ReadString();
		}

		// Token: 0x060053F7 RID: 21495 RVA: 0x00127B09 File Offset: 0x00125D09
		public void Dump()
		{
		}

		// Token: 0x060053F8 RID: 21496 RVA: 0x00127B0B File Offset: 0x00125D0B
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x040025E8 RID: 9704
		internal int objectId;

		// Token: 0x040025E9 RID: 9705
		internal string value;
	}
}
