using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000010 RID: 16
	public class PROTOCOL_BATTLE_MISSION_SEIZE_ACK : GameServerPacket
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00002715 File Offset: 0x00000915
		public PROTOCOL_BATTLE_MISSION_SEIZE_ACK(int int_1, byte byte_1)
		{
			this.int_0 = int_1;
			this.byte_0 = byte_1;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000272B File Offset: 0x0000092B
		public override void Write()
		{
			base.WriteH(5292);
			base.WriteD(this.int_0);
			base.WriteC(this.byte_0);
		}

		// Token: 0x04000023 RID: 35
		private readonly int int_0;

		// Token: 0x04000024 RID: 36
		private readonly byte byte_0;
	}
}
