using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_BATTLE_STARTBATTLE_REQ : GameClientPacket
	{
		public PROTOCOL_BATTLE_STARTBATTLE_REQ()
		{
		}

		public override void Read()
		{
		}

		public override void Run()
		{
			ChannelModel channelModel;
			bool flag;
			bool flag1;
			bool flag2;
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					if (room != null && room.GetLeader() != null && room.GetChannel(out channelModel))
					{
						if (room.IsPreparing())
						{
							bool flag3 = room.IsBotMode();
							SlotModel slot = room.GetSlot(player.SlotId);
							if (slot != null)
							{
								if (slot.State == SlotState.PRESTART)
								{
									room.ChangeSlotState(slot, SlotState.BATTLE_READY, true);
									slot.StopTiming();
									if (flag3)
									{
										this.Client.SendPacket(new PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_ACK(room));
									}
									this.Client.SendPacket(new PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK(room, flag3));
									int ınt32 = 0;
									int ınt321 = 0;
									int ınt322 = 0;
									int ınt323 = 0;
									int ınt324 = 0;
									int ınt325 = 0;
									SlotModel[] slots = room.Slots;
									for (int i = 0; i < (int)slots.Length; i++)
									{
										SlotModel slotModel = slots[i];
										if (slotModel.State >= SlotState.LOAD)
										{
											ınt323++;
											if (slotModel.Team != TeamEnum.FR_TEAM)
											{
												ınt325++;
											}
											else
											{
												ınt324++;
											}
											if (slotModel.State >= SlotState.BATTLE_READY)
											{
												ınt32++;
												if (slotModel.Team != TeamEnum.FR_TEAM)
												{
													ınt321++;
												}
												else
												{
													ınt322++;
												}
											}
										}
									}
									bool state = room.State == RoomState.BATTLE;
									if (!(room.GetSlot(room.LeaderSlot).State >= SlotState.BATTLE_READY & flag3))
									{
										flag = false;
									}
									else if (room.LeaderSlot % 2 != 0 || ınt322 <= ınt324 / 2)
									{
										flag = (room.LeaderSlot % 2 != 1 ? false : ınt321 > ınt325 / 2);
									}
									else
									{
										flag = true;
									}
									bool flag4 = flag;
									if (room.GetSlot(room.LeaderSlot).State < SlotState.BATTLE_READY)
									{
										flag1 = false;
									}
									else
									{
										flag1 = (ınt321 <= ınt325 / 2 ? false : ınt322 > ınt324 / 2);
									}
									bool flag5 = flag1;
									if (room.GetSlot(room.LeaderSlot).State < SlotState.BATTLE_READY || room.RoomType != RoomCondition.FreeForAll)
									{
										flag2 = false;
									}
									else
									{
										flag2 = (ınt32 < 2 ? false : ınt323 >= 2);
									}
									bool flag6 = flag2;
									bool flag7 = (channelModel.Type != ChannelType.Clan ? false : ınt32 == ınt322 + ınt321);
									bool flag8 = (!room.Competitive ? false : ınt32 == ınt322 + ınt321);
									if (state | flag4 | flag5 | flag6 | flag7 | flag8)
									{
										if (flag7)
										{
											CLogger.Print(string.Format("Starting Clan War Match with '{0}' players. FR: {1} CT: {2}", ınt32, ınt322, ınt321), LoggerType.Warning, null);
										}
										if (flag8)
										{
											CLogger.Print(string.Format("Starting Competitive Match with '{0}' players. FR: {1} CT: {2}", ınt32, ınt322, ınt321), LoggerType.Warning, null);
										}
										room.SpawnReadyPlayers();
									}
									return;
								}
							}
							this.Client.SendPacket(new PROTOCOL_SERVER_MESSAGE_KICK_BATTLE_PLAYER_ACK(EventErrorEnum.BATTLE_FIRST_HOLE));
							this.Client.SendPacket(new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(player, 0));
							room.ChangeSlotState(slot, SlotState.NORMAL, true);
							AllUtils.BattleEndPlayersCount(room, flag3);
						}
						else
						{
							this.Client.SendPacket(new PROTOCOL_SERVER_MESSAGE_KICK_BATTLE_PLAYER_ACK(EventErrorEnum.BATTLE_FIRST_HOLE));
							this.Client.SendPacket(new PROTOCOL_BATTLE_STARTBATTLE_ACK());
							room.ChangeSlotState(player.SlotId, SlotState.NORMAL, true);
						}
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_BATTLE_STARTBATTLE_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}