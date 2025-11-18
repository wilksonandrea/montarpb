using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000065 RID: 101
	public class PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK : GameServerPacket
	{
		// Token: 0x06000113 RID: 275 RVA: 0x00003305 File Offset: 0x00001505
		public PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK(int int_1, byte byte_1, ushort ushort_1, float float_3, float float_4, float float_5)
		{
			this.byte_0 = byte_1;
			this.int_0 = int_1;
			this.ushort_0 = ushort_1;
			this.float_0 = float_3;
			this.float_1 = float_4;
			this.float_2 = float_5;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000CFBC File Offset: 0x0000B1BC
		public override void Write()
		{
			base.WriteH(5157);
			base.WriteD(this.int_0);
			base.WriteC(this.byte_0);
			base.WriteH(this.ushort_0);
			base.WriteT(this.float_0);
			base.WriteT(this.float_1);
			base.WriteT(this.float_2);
		}

		// Token: 0x040000CA RID: 202
		private readonly int int_0;

		// Token: 0x040000CB RID: 203
		private readonly float float_0;

		// Token: 0x040000CC RID: 204
		private readonly float float_1;

		// Token: 0x040000CD RID: 205
		private readonly float float_2;

		// Token: 0x040000CE RID: 206
		private readonly byte byte_0;

		// Token: 0x040000CF RID: 207
		private readonly ushort ushort_0;
	}
}
