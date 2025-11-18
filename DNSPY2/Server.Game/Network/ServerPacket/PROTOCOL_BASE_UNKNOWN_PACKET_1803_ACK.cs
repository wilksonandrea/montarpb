using System;
using Plugin.Core.Utility;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000020 RID: 32
	public class PROTOCOL_BASE_UNKNOWN_PACKET_1803_ACK : GameServerPacket
	{
		// Token: 0x0600007C RID: 124 RVA: 0x0000297E File Offset: 0x00000B7E
		public PROTOCOL_BASE_UNKNOWN_PACKET_1803_ACK(string string_2, string string_3)
		{
			this.string_0 = string_2;
			this.string_1 = string_3;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000A204 File Offset: 0x00008404
		public override void Write()
		{
			base.WriteH(1803);
			base.WriteD(94767);
			base.WriteD(100950);
			base.WriteD(0);
			base.WriteD(983299);
			base.WriteC(0);
			base.WriteC(0);
			base.WriteC(0);
			base.WriteC(3);
			base.WriteC(8);
			base.WriteB(Bitwise.HexStringToByteArray("47 00 4D 00 00 00 45 00 56 00 45 00 4E 00 54 00 5F 00 38 00 00 00"));
			base.WriteD(56);
			base.WriteC(1);
			base.WriteD(180214952);
			base.WriteB(Bitwise.HexStringToByteArray("81 E0 D0 03 09 04 15 00 80 22"));
		}

		// Token: 0x04000043 RID: 67
		private readonly string string_0;

		// Token: 0x04000044 RID: 68
		private readonly string string_1;
	}
}
