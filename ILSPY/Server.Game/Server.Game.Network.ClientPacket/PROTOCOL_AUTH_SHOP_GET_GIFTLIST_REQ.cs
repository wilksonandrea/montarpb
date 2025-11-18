using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_AUTH_SHOP_GET_GIFTLIST_REQ : GameClientPacket
{
	public override void Read()
	{
	}

	public override void Run()
	{
		try
		{
			Client.SendPacket(new PROTOCOL_AUTH_SHOP_GET_GIFTLIST_ACK(2148110592u));
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
