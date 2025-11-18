using System;
using Plugin.Core.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000046 RID: 70
	public class PROTOCOL_AUTH_SHOP_RECV_GIFT_ACK : GameServerPacket
	{
		// Token: 0x060000CD RID: 205 RVA: 0x00002EF6 File Offset: 0x000010F6
		public PROTOCOL_AUTH_SHOP_RECV_GIFT_ACK(MessageModel messageModel_1)
		{
			this.messageModel_0 = messageModel_1;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000B0EC File Offset: 0x000092EC
		public override void Write()
		{
			base.WriteH(1079);
			base.WriteD((uint)this.messageModel_0.ObjectId);
			base.WriteD((uint)this.messageModel_0.SenderId);
			base.WriteD((int)this.messageModel_0.State);
			base.WriteD((uint)this.messageModel_0.ExpireDate);
			base.WriteC((byte)(this.messageModel_0.SenderName.Length + 1));
			base.WriteS(this.messageModel_0.SenderName, this.messageModel_0.SenderName.Length + 1);
			base.WriteC(6);
			base.WriteS("EVENT", 6);
		}

		// Token: 0x0400008D RID: 141
		private readonly MessageModel messageModel_0;
	}
}
