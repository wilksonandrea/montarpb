using System;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_USER_TITLE_INFO_ACK : GameServerPacket
{
	private readonly Account account_0;

	public PROTOCOL_BASE_USER_TITLE_INFO_ACK(Account account_1)
	{
		account_0 = account_1;
	}

	public override void Write()
	{
		WriteH(2383);
		WriteB(BitConverter.GetBytes(account_0.PlayerId), 0, 4);
		WriteQ(account_0.Title.Flags);
		WriteC((byte)account_0.Title.Equiped1);
		WriteC((byte)account_0.Title.Equiped2);
		WriteC((byte)account_0.Title.Equiped3);
		WriteD(account_0.Title.Slots);
	}
}
