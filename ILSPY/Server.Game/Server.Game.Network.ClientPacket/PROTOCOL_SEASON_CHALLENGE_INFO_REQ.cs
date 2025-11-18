using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_SEASON_CHALLENGE_INFO_REQ : GameClientPacket
{
	public override void Read()
	{
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player != null)
			{
				Client.SendPacket(new PROTOCOL_SEASON_CHALLENGE_INFO_ACK(player));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(GetType().Name + "; " + ex.Message, LoggerType.Error, ex);
		}
	}
}
