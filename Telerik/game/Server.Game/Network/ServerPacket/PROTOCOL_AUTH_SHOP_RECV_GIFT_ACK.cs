using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_AUTH_SHOP_RECV_GIFT_ACK : GameServerPacket
	{
		private readonly MessageModel messageModel_0;

		public PROTOCOL_AUTH_SHOP_RECV_GIFT_ACK(MessageModel messageModel_1)
		{
			this.messageModel_0 = messageModel_1;
		}

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
	}
}