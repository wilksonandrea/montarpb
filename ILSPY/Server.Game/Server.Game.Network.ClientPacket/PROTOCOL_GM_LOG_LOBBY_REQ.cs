using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_GM_LOG_LOBBY_REQ : GameClientPacket
{
	private int int_0;

	public override void Read()
	{
		int_0 = ReadD();
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player != null && player.IsGM())
			{
				long playerId = player.GetChannel().GetPlayer(int_0).PlayerId;
				if (playerId > 0L)
				{
					Client.SendPacket(new PROTOCOL_GM_LOG_LOBBY_ACK(0u, playerId));
				}
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_GM_LOG_LOBBY_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
