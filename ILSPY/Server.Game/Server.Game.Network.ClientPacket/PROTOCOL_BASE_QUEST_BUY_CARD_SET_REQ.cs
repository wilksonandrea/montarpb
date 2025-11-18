using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_QUEST_BUY_CARD_SET_REQ : GameClientPacket
{
	private int int_0;

	private EventErrorEnum eventErrorEnum_0;

	public override void Read()
	{
		int_0 = ReadC();
		ReadC();
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
			if (mission != null && mission.Mission1 != int_0 && mission.Mission2 != int_0 && mission.Mission3 != int_0)
			{
				MissionStore mission2 = MissionConfigXML.GetMission(int_0);
				if (mission2 != null && ShopManager.GetItemId(mission2.ItemId) != null)
				{
					if (mission.Mission1 == 0)
					{
						if (DaoManagerSQL.UpdatePlayerMissionId(player.PlayerId, int_0, 0))
						{
							mission.Mission1 = int_0;
							mission.List1 = new byte[40];
							mission.ActualMission = 0;
							mission.Card1 = 0;
						}
						else
						{
							eventErrorEnum_0 = EventErrorEnum.MISSION_FAIL_BUY_CARD_BY_NO_CARD_INFO;
						}
					}
					else if (mission.Mission2 == 0)
					{
						if (DaoManagerSQL.UpdatePlayerMissionId(player.PlayerId, int_0, 1))
						{
							mission.Mission2 = int_0;
							mission.List2 = new byte[40];
							mission.ActualMission = 1;
							mission.Card2 = 0;
						}
						else
						{
							eventErrorEnum_0 = EventErrorEnum.MISSION_FAIL_BUY_CARD_BY_NO_CARD_INFO;
						}
					}
					else if (mission.Mission3 == 0)
					{
						if (DaoManagerSQL.UpdatePlayerMissionId(player.PlayerId, int_0, 2))
						{
							mission.Mission3 = int_0;
							mission.List3 = new byte[40];
							mission.ActualMission = 2;
							mission.Card3 = 0;
						}
						else
						{
							eventErrorEnum_0 = EventErrorEnum.MISSION_FAIL_BUY_CARD_BY_NO_CARD_INFO;
						}
					}
					else
					{
						eventErrorEnum_0 = EventErrorEnum.MISSION_LIMIT_CARD_COUNT;
					}
				}
				else
				{
					CLogger.Print("There is an error on Mission Config. Please check the configuration!", LoggerType.Warning);
					eventErrorEnum_0 = EventErrorEnum.MISSION_NO_POINT_TO_GET_ITEM;
				}
				int priceGold = ShopManager.GetItemId(mission2.ItemId).PriceGold;
				if (eventErrorEnum_0 == EventErrorEnum.SUCCESS && 0 <= player.Gold - priceGold)
				{
					if (priceGold != 0 && !DaoManagerSQL.UpdateAccountGold(player.PlayerId, player.Gold - priceGold))
					{
						eventErrorEnum_0 = EventErrorEnum.MISSION_FAIL_BUY_CARD_BY_NO_CARD_INFO;
					}
					else
					{
						player.Gold -= priceGold;
					}
				}
				else
				{
					eventErrorEnum_0 = EventErrorEnum.MISSION_NO_POINT_TO_GET_ITEM;
				}
				Client.SendPacket(new PROTOCOL_BASE_QUEST_BUY_CARD_SET_ACK(eventErrorEnum_0, player));
			}
			else
			{
				Client.SendPacket(new PROTOCOL_BASE_QUEST_BUY_CARD_SET_ACK(EventErrorEnum.MISSION_NO_POINT_TO_GET_ITEM, null));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(GetType().Name + ": " + ex.Message, LoggerType.Error, ex);
		}
	}
}
