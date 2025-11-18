using System;

namespace Server.Auth.Network.ServerPacket
{
	// Token: 0x02000030 RID: 48
	public class PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK : AuthServerPacket
	{
		// Token: 0x0600009E RID: 158 RVA: 0x000029CE File Offset: 0x00000BCE
		public PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(string string_1)
		{
			this.string_0 = string_1;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00006DD4 File Offset: 0x00004FD4
		public override void Write()
		{
			base.WriteH(3079);
			base.WriteH(0);
			base.WriteD(0);
			base.WriteH((ushort)this.string_0.Length);
			base.WriteN(this.string_0, this.string_0.Length, "UTF-16LE");
			base.WriteD(2);
		}

		// Token: 0x04000064 RID: 100
		private readonly string string_0;
	}
}
