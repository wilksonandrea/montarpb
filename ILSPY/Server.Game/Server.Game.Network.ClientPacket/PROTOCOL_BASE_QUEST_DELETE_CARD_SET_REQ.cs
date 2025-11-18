using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_QUEST_DELETE_CARD_SET_REQ : GameClientPacket
{
	private uint uint_0;

	private int int_0;

	public override void Read()
	{
		int_0 = ReadC();
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player == null)
			{
				return;
			}
			PlayerMissions mission = player.Mission;
			bool flag = false;
			if (int_0 >= 3 || (int_0 == 0 && mission.Mission1 == 0) || (int_0 == 1 && mission.Mission2 == 0) || (int_0 == 2 && mission.Mission3 == 0))
			{
				flag = true;
			}
			if (!flag && DaoManagerSQL.UpdatePlayerMissionId(player.PlayerId, 0, int_0) && ComDiv.UpdateDB("player_missions", "owner_id", player.PlayerId, new string[2]
			{
				$"card{int_0 + 1}",
				$"mission{int_0 + 1}_raw"
			}, 0, new byte[0]))
			{
				if (int_0 == 0)
				{
					mission.Mission1 = 0;
					mission.Card1 = 0;
					mission.List1 = new byte[40];
				}
				else if (int_0 == 1)
				{
					mission.Mission2 = 0;
					mission.Card2 = 0;
					mission.List2 = new byte[40];
				}
				else if (int_0 == 2)
				{
					mission.Mission3 = 0;
					mission.Card3 = 0;
					mission.List3 = new byte[40];
				}
			}
			else
			{
				uint_0 = 2147487824u;
			}
			Client.SendPacket(new PROTOCOL_BASE_QUEST_DELETE_CARD_SET_ACK(uint_0, player));
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_BASE_QUEST_DELETE_CARD_SET_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
