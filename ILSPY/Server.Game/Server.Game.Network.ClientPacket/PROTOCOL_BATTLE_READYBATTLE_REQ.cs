using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BATTLE_READYBATTLE_REQ : GameClientPacket
{
	private ViewerType viewerType_0;

	public override void Read()
	{
		viewerType_0 = (ViewerType)ReadC();
		ReadD();
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
			if (room == null || room.GetLeader() == null || !room.GetChannel(out var Channel) || !room.GetSlot(player.SlotId, out var Slot))
			{
				return;
			}
			if (Slot.Equipment == null)
			{
				Client.SendPacket(new PROTOCOL_BATTLE_READYBATTLE_ACK(2147487915u));
				return;
			}
			MapMatch mapLimit = SystemMapXML.GetMapLimit((int)room.MapId, (int)room.Rule);
			if (mapLimit == null)
			{
				return;
			}
			bool flag = room.IsBotMode();
			if (Slot.ViewType != viewerType_0)
			{
				Slot.ViewType = viewerType_0;
			}
			Slot.SpecGM = (Slot.ViewType == ViewerType.SpecGM && player.IsGM()) || (room.RoomType == RoomCondition.Ace && (Slot.Id < 0 || Slot.Id > 1));
			if (!flag && ConfigLoader.TournamentRule && AllUtils.ClassicModeCheck(player, room))
			{
				return;
			}
			int TotalEnemys = 0;
			int FRPlayers = 0;
			int CTPlayers = 0;
			AllUtils.GetReadyPlayers(room, ref FRPlayers, ref CTPlayers, ref TotalEnemys);
			if (room.LeaderSlot == player.SlotId)
			{
				if (room.State != RoomState.READY && room.State != RoomState.COUNTDOWN)
				{
					return;
				}
				uint Error;
				uint Error2;
				if (mapLimit.Limit == 8 && AllUtils.Check4vs4(room, IsLeader: true, ref FRPlayers, ref CTPlayers, ref TotalEnemys))
				{
					Client.SendPacket(new PROTOCOL_ROOM_UNREADY_4VS4_ACK());
				}
				else if (AllUtils.ClanMatchCheck(room, Channel.Type, TotalEnemys, out Error))
				{
					Client.SendPacket(new PROTOCOL_BATTLE_READYBATTLE_ACK(Error));
				}
				else if (AllUtils.CompetitiveMatchCheck(player, room, out Error2))
				{
					Client.SendPacket(new PROTOCOL_BATTLE_READYBATTLE_ACK(Error2));
				}
				else if (TotalEnemys == 0 && (flag || room.RoomType == RoomCondition.Tutorial))
				{
					room.ChangeSlotState(Slot, SlotState.READY, SendInfo: false);
					room.StartBattle(UpdateInfo: false);
					room.UpdateSlotsInfo();
				}
				else if (!flag && TotalEnemys > 0)
				{
					room.ChangeSlotState(Slot, SlotState.READY, SendInfo: false);
					if (room.BalanceType != 0)
					{
						AllUtils.TryBalanceTeams(room);
					}
					if (room.ThisModeHaveCD())
					{
						if (room.State == RoomState.READY)
						{
							SlotModel[] array = new SlotModel[2]
							{
								room.GetSlot(0),
								room.GetSlot(1)
							};
							if (room.RoomType == RoomCondition.Ace && (array[0].State != SlotState.READY || array[1].State != SlotState.READY))
							{
								Client.SendPacket(new PROTOCOL_BATTLE_READYBATTLE_ACK(2147487753u));
								room.ChangeSlotState(room.LeaderSlot, SlotState.NORMAL, SendInfo: false);
								room.StopCountDown(CountDownEnum.StopByHost);
							}
							else
							{
								room.State = RoomState.COUNTDOWN;
								room.UpdateRoomInfo();
								room.StartCountDown();
							}
						}
						else if (room.State == RoomState.COUNTDOWN)
						{
							room.ChangeSlotState(room.LeaderSlot, SlotState.NORMAL, SendInfo: false);
							room.StopCountDown(CountDownEnum.StopByHost);
						}
					}
					else
					{
						room.StartBattle(UpdateInfo: false);
					}
					room.UpdateSlotsInfo();
				}
				else if (TotalEnemys == 0 && !flag)
				{
					Client.SendPacket(new PROTOCOL_BATTLE_READYBATTLE_ACK(2147487753u));
				}
			}
			else if (room.Slots[room.LeaderSlot].State >= SlotState.LOAD && room.IsPreparing())
			{
				if (Slot.State != SlotState.NORMAL)
				{
					return;
				}
				if (mapLimit.Limit != 8 || !AllUtils.Check4vs4(room, IsLeader: false, ref FRPlayers, ref FRPlayers, ref TotalEnemys))
				{
					if (room.BalanceType != 0 && !flag)
					{
						AllUtils.TryBalancePlayer(room, player, InBattle: true, ref Slot);
					}
					room.ChangeSlotState(Slot, SlotState.LOAD, SendInfo: true);
					Slot.SetMissionsClone(player.Mission);
					Client.SendPacket(new PROTOCOL_BATTLE_READYBATTLE_ACK((uint)Slot.State));
					Client.SendPacket(new PROTOCOL_BATTLE_START_GAME_ACK(room));
					using PROTOCOL_BATTLE_START_GAME_TRANS_ACK packet = new PROTOCOL_BATTLE_START_GAME_TRANS_ACK(room, Slot, player.Title);
					room.SendPacketToPlayers(packet, SlotState.READY, 1, Slot.Id);
					return;
				}
				Client.SendPacket(new PROTOCOL_ROOM_UNREADY_4VS4_ACK());
			}
			else if (Slot.State == SlotState.NORMAL)
			{
				room.ChangeSlotState(Slot, SlotState.READY, SendInfo: true);
			}
			else if (Slot.State == SlotState.READY)
			{
				room.ChangeSlotState(Slot, SlotState.NORMAL, SendInfo: false);
				if (room.State == RoomState.COUNTDOWN && room.GetPlayingPlayers((room.LeaderSlot % 2 == 0) ? TeamEnum.CT_TEAM : TeamEnum.FR_TEAM, SlotState.READY, 0) == 0)
				{
					room.ChangeSlotState(room.LeaderSlot, SlotState.NORMAL, SendInfo: false);
					room.StopCountDown(CountDownEnum.StopByPlayer);
				}
				room.UpdateSlotsInfo();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_BATTLE_READYBATTLE_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
