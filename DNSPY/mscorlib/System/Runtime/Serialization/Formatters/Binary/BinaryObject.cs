using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000787 RID: 1927
	internal sealed class BinaryObject : IStreamable
	{
		// Token: 0x060053DE RID: 21470 RVA: 0x00126EAF File Offset: 0x001250AF
		internal BinaryObject()
		{
		}

		// Token: 0x060053DF RID: 21471 RVA: 0x00126EB7 File Offset: 0x001250B7
		internal void Set(int objectId, int mapId)
		{
			this.objectId = objectId;
			this.mapId = mapId;
		}

		// Token: 0x060053E0 RID: 21472 RVA: 0x00126EC7 File Offset: 0x001250C7
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(1);
			sout.WriteInt32(this.objectId);
			sout.WriteInt32(this.mapId);
		}

		// Token: 0x060053E1 RID: 21473 RVA: 0x00126EE8 File Offset: 0x001250E8
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.objectId = input.ReadInt32();
			this.mapId = input.ReadInt32();
		}

		// Token: 0x060053E2 RID: 21474 RVA: 0x00126F02 File Offset: 0x00125102
		public void Dump()
		{
		}

		// Token: 0x060053E3 RID: 21475 RVA: 0x00126F04 File Offset: 0x00125104
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x040025CD RID: 9677
		internal int objectId;

		// Token: 0x040025CE RID: 9678
		internal int mapId;
	}
}
