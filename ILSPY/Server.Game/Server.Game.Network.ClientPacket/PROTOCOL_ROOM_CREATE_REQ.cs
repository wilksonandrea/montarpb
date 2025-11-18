using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_ROOM_CREATE_REQ : GameClientPacket
{
	private uint uint_0;

	private string string_0;

	private string string_1;

	private string string_2;

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

	private byte byte_8;

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
		int_3 = ReadC();
		int_0 = ReadC();
		int_1 = ReadC();
		roomWeaponsFlag_0 = (RoomWeaponsFlag)ReadH();
		roomStageFlag_0 = (RoomStageFlag)ReadD();
		ReadH();
		int_4 = ReadD();
		ReadH();
		string_2 = ReadU(66);
		int_2 = ReadD();
		byte_2 = ReadC();
		byte_3 = ReadC();
		teamBalance_0 = (TeamBalance)ReadH();
		byte_0 = ReadB(24);
		byte_8 = ReadC();
		byte_1 = ReadB(4);
		byte_7 = ReadC();
		ReadH();
		string_1 = ReadS(4);
		byte_4 = ReadC();
		byte_5 = ReadC();
		byte_6 = ReadC();
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
			ChannelModel channel = player.GetChannel();
			if (channel != null && player.Nickname.Length != 0 && player.Room == null && player.Match == null)
			{
				lock (channel.Rooms)
				{
					int num = 0;
					RoomModel roomModel;
					while (true)
					{
						if (num >= channel.MaxRooms)
						{
							return;
						}
						if (channel.GetRoom(num) == null)
						{
							roomModel = new RoomModel(num, channel)
							{
								Name = string_0,
								MapId = mapIdEnum_0,
								Rule = mapRules_0,
								Stage = stageOptions_0,
								RoomType = roomCondition_0
							};
							roomModel.GenerateSeed();
							roomModel.State = ((roomState_0 < RoomState.READY) ? RoomState.READY : roomState_0);
							roomModel.LeaderName = ((string_2.Equals("") || !string_2.Equals(player.Nickname)) ? player.Nickname : string_2);
							roomModel.Ping = int_1;
							roomModel.WeaponsFlag = roomWeaponsFlag_0;
							roomModel.Flag = roomStageFlag_0;
							roomModel.NewInt = int_4;
							bool flag;
							if ((flag = roomModel.IsBotMode()) && roomModel.ChannelType == ChannelType.Clan)
							{
								uint_0 = 2147487869u;
								return;
							}
							roomModel.KillTime = int_2;
							roomModel.Limit = (byte)((channel.Type == ChannelType.Clan) ? 1 : byte_2);
							roomModel.WatchRuleFlag = (byte)((roomModel.RoomType == RoomCondition.Ace) ? 142 : byte_3);
							roomModel.BalanceType = ((channel.Type != ChannelType.Clan && roomModel.RoomType != RoomCondition.Ace) ? teamBalance_0 : TeamBalance.None);
							roomModel.RandomMaps = byte_0;
							roomModel.CountdownIG = byte_8;
							roomModel.LeaderAddr = byte_1;
							roomModel.KillCam = byte_7;
							roomModel.Password = string_1;
							if (flag)
							{
								roomModel.AiCount = byte_4;
								roomModel.AiLevel = byte_5;
								roomModel.AiType = byte_6;
							}
							roomModel.SetSlotCount(int_0, IsCreateRoom: true, IsUpdateRoom: false);
							roomModel.CountPlayers = int_3;
							roomModel.CountMaxSlots = int_0;
							if (roomModel.AddPlayer(player) >= 0)
							{
								break;
							}
						}
						num++;
					}
					player.ResetPages();
					channel.AddRoom(roomModel);
					Client.SendPacket(new PROTOCOL_ROOM_CREATE_ACK(uint_0, roomModel));
					if (roomModel.IsBotMode())
					{
						roomModel.ChangeSlotState(1, SlotState.CLOSE, SendInfo: true);
						roomModel.ChangeSlotState(3, SlotState.CLOSE, SendInfo: true);
						roomModel.ChangeSlotState(5, SlotState.CLOSE, SendInfo: true);
						roomModel.ChangeSlotState(7, SlotState.CLOSE, SendInfo: true);
						Client.SendPacket(new PROTOCOL_ROOM_GET_SLOTINFO_ACK(roomModel));
					}
					return;
				}
			}
			uint_0 = 2147483648u;
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_LOBBY_CREATE_ROOM_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
