using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_SERVER_MESSAGE_INVITED_ACK : GameServerPacket
{
	private readonly Account account_0;

	private readonly RoomModel roomModel_0;

	public PROTOCOL_SERVER_MESSAGE_INVITED_ACK(Account account_1, RoomModel roomModel_1)
	{
		account_0 = account_1;
		roomModel_0 = roomModel_1;
	}

	public override void Write()
	{
		WriteH(3077);
		WriteU(account_0.Nickname, 66);
		WriteD(roomModel_0.RoomId);
		WriteQ(account_0.PlayerId);
		WriteS(roomModel_0.Password, 4);
	}
}
