using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CLAN_WAR_CANCEL_MATCHMAKING_REQ : GameClientPacket
{
	public override void Read()
	{
	}

	public override void Run()
	{
		try
		{
			Client.SendPacket(new PROTOCOL_CLAN_WAR_CANCEL_MATCHMAKING_ACK());
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_CLAN_WAR_CANCEL_MATCHMAKING_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
