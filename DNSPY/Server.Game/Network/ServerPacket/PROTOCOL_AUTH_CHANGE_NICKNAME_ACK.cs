using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200002E RID: 46
	public class PROTOCOL_AUTH_CHANGE_NICKNAME_ACK : GameServerPacket
	{
		// Token: 0x06000098 RID: 152 RVA: 0x00002BBF File Offset: 0x00000DBF
		public PROTOCOL_AUTH_CHANGE_NICKNAME_ACK(string string_1)
		{
			this.string_0 = string_1;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00002BCE File Offset: 0x00000DCE
		public override void Write()
		{
			base.WriteH(1836);
			base.WriteC((byte)this.string_0.Length);
			base.WriteU(this.string_0, this.string_0.Length * 2);
		}

		// Token: 0x0400005A RID: 90
		private readonly string string_0;
	}
}
