using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_GET_USER_SUBTASK_REQ : GameClientPacket
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
			if (player != null)
			{
				PlayerSession player2 = player.GetChannel().GetPlayer(int_0);
				if (player2 != null)
				{
					Client.SendPacket(new PROTOCOL_BASE_GET_USER_SUBTASK_ACK(player2));
				}
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_BASE_GET_USER_SUBTASK_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
