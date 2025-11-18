using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.SQL;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_MEMBER_CONTEXT_REQ : GameClientPacket
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
				int num = ((player.ClanId == 0) ? player.FindClanId : player.ClanId);
				int int_;
				int int_2;
				if (num == 0)
				{
					int_ = -1;
					int_2 = 0;
				}
				else
				{
					int_ = 0;
					int_2 = DaoManagerSQL.GetClanPlayers(num);
				}
				Client.SendPacket(new PROTOCOL_CS_MEMBER_CONTEXT_ACK(int_, int_2));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
