using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200010C RID: 268
	public class PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK : GameServerPacket
	{
		// Token: 0x0600028D RID: 653 RVA: 0x00004A04 File Offset: 0x00002C04
		public PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(string string_1)
		{
			this.string_0 = string_1;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00013FA8 File Offset: 0x000121A8
		public override void Write()
		{
			base.WriteH(3079);
			base.WriteH(0);
			base.WriteD(0);
			base.WriteH((ushort)this.string_0.Length);
			base.WriteN(this.string_0, this.string_0.Length, "UTF-16LE");
			base.WriteD(2);
		}

		// Token: 0x040001EE RID: 494
		private readonly string string_0;
	}
}
