using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ : GameClientPacket
{
	private int int_0 = -1;

	private int int_1;

	private int int_2;

	private int int_3;

	private string string_0;

	private StageOptions stageOptions_0;

	private MapIdEnum mapIdEnum_0;

	private MapRules mapRules_0;

	private RoomCondition roomCondition_0;

	private RoomWeaponsFlag roomWeaponsFlag_0;

	private RoomStageFlag roomStageFlag_0;

	public override void Read()
	{
		int_3 = ReadH();
		ReadD();
		ReadD();
		ReadH();
		string_0 = ReadU(46);
		mapIdEnum_0 = (MapIdEnum)ReadC();
		mapRules_0 = (MapRules)ReadC();
		stageOptions_0 = (StageOptions)ReadC();
		roomCondition_0 = (RoomCondition)ReadC();
		ReadC();
		ReadC();
		int_1 = ReadC();
		int_2 = ReadC();
		roomWeaponsFlag_0 = (RoomWeaponsFlag)ReadH();
		roomStageFlag_0 = (RoomStageFlag)ReadD();
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player == null || player.ClanId == 0)
			{
				return;
			}
			ChannelModel channel = player.GetChannel();
			MatchModel match = player.Match;
			if (channel == null || match == null)
			{
				return;
			}
			MatchModel match2 = channel.GetMatch(int_3);
			if (match2 == null)
			{
				return;
			}
			lock (channel.Rooms)
			{
				for (int i = 0; i < channel.MaxRooms; i++)
				{
					if (channel.GetRoom(i) == null)
					{
						RoomModel roomModel = new RoomModel(i, channel)
						{
							Name = string_0,
							MapId = mapIdEnum_0,
							Rule = mapRules_0,
							Stage = stageOptions_0,
							RoomType = roomCondition_0
						};
						roomModel.SetSlotCount(int_1, IsCreateRoom: true, IsUpdateRoom: false);
						roomModel.Ping = int_2;
						roomModel.WeaponsFlag = roomWeaponsFlag_0;
						roomModel.Flag = roomStageFlag_0;
						roomModel.Password = "";
						roomModel.KillTime = 3;
						if (roomModel.AddPlayer(player) >= 0)
						{
							channel.AddRoom(roomModel);
							Client.SendPacket(new PROTOCOL_ROOM_CREATE_ACK(0u, roomModel));
							int_0 = i;
							return;
						}
					}
				}
			}
			using (PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK pROTOCOL_CLAN_WAR_ENEMY_INFO_ACK = new PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK(match2))
			{
				using PROTOCOL_CLAN_WAR_JOIN_ROOM_ACK pROTOCOL_CLAN_WAR_JOIN_ROOM_ACK = new PROTOCOL_CLAN_WAR_JOIN_ROOM_ACK(match2, int_0, 0);
				byte[] completeBytes = pROTOCOL_CLAN_WAR_ENEMY_INFO_ACK.GetCompleteBytes("PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ-1");
				byte[] completeBytes2 = pROTOCOL_CLAN_WAR_JOIN_ROOM_ACK.GetCompleteBytes("PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ-2");
				foreach (Account allPlayer in match.GetAllPlayers(match.Leader))
				{
					allPlayer.SendCompletePacket(completeBytes, pROTOCOL_CLAN_WAR_ENEMY_INFO_ACK.GetType().Name);
					allPlayer.SendCompletePacket(completeBytes2, pROTOCOL_CLAN_WAR_JOIN_ROOM_ACK.GetType().Name);
					if (allPlayer.Match != null)
					{
						match.Slots[allPlayer.MatchSlot].State = SlotMatchState.Ready;
					}
				}
			}
			using PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK pROTOCOL_CLAN_WAR_ENEMY_INFO_ACK2 = new PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK(match);
			using PROTOCOL_CLAN_WAR_JOIN_ROOM_ACK pROTOCOL_CLAN_WAR_JOIN_ROOM_ACK2 = new PROTOCOL_CLAN_WAR_JOIN_ROOM_ACK(match, int_0, 1);
			byte[] completeBytes3 = pROTOCOL_CLAN_WAR_ENEMY_INFO_ACK2.GetCompleteBytes("PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ-3");
			byte[] completeBytes4 = pROTOCOL_CLAN_WAR_JOIN_ROOM_ACK2.GetCompleteBytes("PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ-4");
			foreach (Account allPlayer2 in match2.GetAllPlayers())
			{
				allPlayer2.SendCompletePacket(completeBytes3, pROTOCOL_CLAN_WAR_ENEMY_INFO_ACK2.GetType().Name);
				allPlayer2.SendCompletePacket(completeBytes4, pROTOCOL_CLAN_WAR_JOIN_ROOM_ACK2.GetType().Name);
				if (allPlayer2.Match != null)
				{
					match.Slots[allPlayer2.MatchSlot].State = SlotMatchState.Ready;
				}
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
