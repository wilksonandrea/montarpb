using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_LOBBY_NEW_MYINFO_REQ : GameClientPacket
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
				Client.SendPacket(new PROTOCOL_LOBBY_NEW_MYINFO_ACK(player));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_LOBBY_NEW_MYINFO_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
