using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ : GameClientPacket
{
	private string string_0;

	private string string_1;

	private MapIdEnum mapIdEnum_0;

	private MapRules mapRules_0;

	private StageOptions stageOptions_0;

	private TeamBalance teamBalance_0;

	private byte[] byte_0;

	private byte[] byte_1;

	private int int_0;

	private int int_1;

	private int int_2;

	private int int_3;

	private int int_4;

	private byte byte_2;

	private byte byte_3;

	private byte byte_4;

	private byte byte_5;

	private byte byte_6;

	private byte byte_7;

	private RoomCondition roomCondition_0;

	private RoomState roomState_0;

	private RoomWeaponsFlag roomWeaponsFlag_0;

	private RoomStageFlag roomStageFlag_0;

	public override void Read()
	{
		ReadD();
		string_0 = ReadU(46);
		mapIdEnum_0 = (MapIdEnum)ReadC();
		mapRules_0 = (MapRules)ReadC();
		stageOptions_0 = (StageOptions)ReadC();
		roomCondition_0 = (RoomCondition)ReadC();
		roomState_0 = (RoomState)ReadC();
		int_4 = ReadC();
		int_1 = ReadC();
		int_2 = ReadC();
		roomWeaponsFlag_0 = (RoomWeaponsFlag)ReadH();
		roomStageFlag_0 = (RoomStageFlag)ReadD();
		ReadH();
		int_0 = ReadD();
		ReadH();
		string_1 = ReadU(66);
		int_3 = ReadD();
		byte_2 = ReadC();
		byte_3 = ReadC();
		teamBalance_0 = (TeamBalance)ReadH();
		byte_0 = ReadB(24);
		byte_6 = ReadC();
		byte_1 = ReadB(4);
		byte_7 = ReadC();
		ReadH();
		byte_4 = ReadC();
		byte_5 = ReadC();
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
			RoomModel room = player.Room;
			if (room == null || room.LeaderSlot != player.SlotId)
			{
				return;
			}
			bool flag = !room.Name.Equals(string_0);
			bool flag2 = room.Rule != mapRules_0 || room.Stage != stageOptions_0 || room.RoomType != roomCondition_0;
			room.Name = string_0;
			room.MapId = mapIdEnum_0;
			room.Rule = mapRules_0;
			room.Stage = stageOptions_0;
			room.RoomType = roomCondition_0;
			room.Ping = int_2;
			room.Flag = roomStageFlag_0;
			room.NewInt = int_0;
			room.KillTime = int_3;
			room.Limit = byte_2;
			room.WatchRuleFlag = (byte)((room.RoomType == RoomCondition.Ace) ? 142 : byte_3);
			room.BalanceType = ((room.RoomType != RoomCondition.Ace) ? teamBalance_0 : TeamBalance.None);
			room.BalanceType = teamBalance_0;
			room.RandomMaps = byte_0;
			room.CountdownIG = byte_6;
			room.LeaderAddr = byte_1;
			room.KillCam = byte_7;
			room.AiCount = byte_4;
			room.AiLevel = byte_5;
			room.SetSlotCount(int_1, IsCreateRoom: false, IsUpdateRoom: true);
			room.CountPlayers = int_4;
			if (roomState_0 < RoomState.READY || string_1.Equals("") || !string_1.Equals(player.Nickname) || flag || flag2 || roomWeaponsFlag_0 != room.WeaponsFlag || int_1 != room.CountMaxSlots)
			{
				room.State = ((roomState_0 < RoomState.READY) ? RoomState.READY : roomState_0);
				room.LeaderName = ((string_1.Equals("") || !string_1.Equals(player.Nickname)) ? player.Nickname : string_1);
				room.WeaponsFlag = roomWeaponsFlag_0;
				room.CountMaxSlots = int_1;
				room.CountdownIG = 0;
				if (room.ResetReadyPlayers() > 0)
				{
					room.UpdateSlotsInfo();
				}
			}
			room.UpdateRoomInfo();
			using PROTOCOL_ROOM_CHANGE_ROOM_OPTIONINFO_ACK packet = new PROTOCOL_ROOM_CHANGE_ROOM_OPTIONINFO_ACK(room);
			room.SendPacketToPlayers(packet);
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_BATTLE_CHANGE_ROOMINFO_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
