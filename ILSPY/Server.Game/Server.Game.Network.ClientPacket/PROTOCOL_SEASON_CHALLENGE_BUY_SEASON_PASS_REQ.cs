using System;
using Plugin.Core;
using Plugin.Core.Enums;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_REQ : GameClientPacket
{
	public override void Read()
	{
	}

	public override void Run()
	{
		try
		{
			CLogger.Print(GetType().Name + " CALLLED!", LoggerType.Warning);
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_MATCH_CLAN_SEASON_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
