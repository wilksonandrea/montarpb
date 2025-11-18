using Plugin.Core.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_AUTH_SHOP_RECV_GIFT_ACK : GameServerPacket
{
	private readonly MessageModel messageModel_0;

	public PROTOCOL_AUTH_SHOP_RECV_GIFT_ACK(MessageModel messageModel_1)
	{
		messageModel_0 = messageModel_1;
	}

	public override void Write()
	{
		WriteH(1079);
		WriteD((uint)messageModel_0.ObjectId);
		WriteD((uint)messageModel_0.SenderId);
		WriteD((int)messageModel_0.State);
		WriteD((uint)messageModel_0.ExpireDate);
		WriteC((byte)(messageModel_0.SenderName.Length + 1));
		WriteS(messageModel_0.SenderName, messageModel_0.SenderName.Length + 1);
		WriteC(6);
		WriteS("EVENT", 6);
	}
}
