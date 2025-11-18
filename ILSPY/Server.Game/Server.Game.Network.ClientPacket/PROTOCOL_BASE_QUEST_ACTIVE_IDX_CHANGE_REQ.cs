using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_REQ : GameClientPacket
{
	private int int_0;

	private int int_1;

	private int int_2;

	public override void Read()
	{
		int_1 = ReadC();
		int_0 = ReadC();
		int_2 = ReadUH();
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
			DBQuery dBQuery = new DBQuery();
			PlayerMissions mission = player.Mission;
			if (mission.GetCard(int_1) != int_0)
			{
				if (int_1 == 0)
				{
					mission.Card1 = int_0;
				}
				else if (int_1 == 1)
				{
					mission.Card2 = int_0;
				}
				else if (int_1 == 2)
				{
					mission.Card3 = int_0;
				}
				else if (int_1 == 3)
				{
					mission.Card4 = int_0;
				}
				dBQuery.AddQuery($"card{int_1 + 1}", int_0);
			}
			mission.SelectedCard = int_2 == 65535;
			if (mission.ActualMission != int_1)
			{
				dBQuery.AddQuery("current_mission", int_1);
				mission.ActualMission = int_1;
			}
			ComDiv.UpdateDB("player_missions", "owner_id", Client.PlayerId, dBQuery.GetTables(), dBQuery.GetValues());
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
