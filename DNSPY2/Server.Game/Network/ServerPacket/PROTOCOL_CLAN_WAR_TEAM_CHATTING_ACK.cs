using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000098 RID: 152
	public class PROTOCOL_CLAN_WAR_TEAM_CHATTING_ACK : GameServerPacket
	{
		// Token: 0x06000189 RID: 393 RVA: 0x0000394E File Offset: 0x00001B4E
		public PROTOCOL_CLAN_WAR_TEAM_CHATTING_ACK(string string_2, string string_3)
		{
			this.string_1 = string_2;
			this.string_0 = string_3;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00003964 File Offset: 0x00001B64
		public PROTOCOL_CLAN_WAR_TEAM_CHATTING_ACK(int int_2, int int_3)
		{
			this.int_0 = int_2;
			this.int_1 = int_3;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000F2D0 File Offset: 0x0000D4D0
		public override void Write()
		{
			base.WriteH(6929);
			base.WriteC((byte)this.int_0);
			if (this.int_0 == 0)
			{
				base.WriteC((byte)(this.string_1.Length + 1));
				base.WriteN(this.string_1, this.string_1.Length + 2, "UTF-16LE");
				base.WriteC((byte)(this.string_0.Length + 1));
				base.WriteN(this.string_0, this.string_0.Length + 2, "UTF-16LE");
				return;
			}
			base.WriteD(this.int_1);
		}

		// Token: 0x04000122 RID: 290
		private readonly int int_0;

		// Token: 0x04000123 RID: 291
		private readonly int int_1;

		// Token: 0x04000124 RID: 292
		private readonly string string_0;

		// Token: 0x04000125 RID: 293
		private readonly string string_1;
	}
}
