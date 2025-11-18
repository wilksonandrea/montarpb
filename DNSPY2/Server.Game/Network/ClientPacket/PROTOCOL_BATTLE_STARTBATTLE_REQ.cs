using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000172 RID: 370
	public class PROTOCOL_BATTLE_STARTBATTLE_REQ : GameClientPacket
	{
		// Token: 0x060003B8 RID: 952 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0001D2CC File Offset: 0x0001B4CC
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					ChannelModel channelModel;
					if (room != null && room.GetLeader() != null && room.GetChannel(out channelModel))
					{
						if (!room.IsPreparing())
						{
							this.Client.SendPacket(new PROTOCOL_SERVER_MESSAGE_KICK_BATTLE_PLAYER_ACK((EventErrorEnum)2147487755U));
							this.Client.SendPacket(new PROTOCOL_BATTLE_STARTBATTLE_ACK());
							room.ChangeSlotState(player.SlotId, SlotState.NORMAL, true);
						}
						else
						{
							bool flag = room.IsBotMode();
							SlotModel slot = room.GetSlot(player.SlotId);
							if (slot != null)
							{
								if (slot.State == SlotState.PRESTART)
								{
									room.ChangeSlotState(slot, SlotState.BATTLE_READY, true);
									slot.StopTiming();
									if (flag)
									{
										this.Client.SendPacket(new PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_ACK(room));
									}
									this.Client.SendPacket(new PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK(room, flag));
									int num = 0;
									int num2 = 0;
									int num3 = 0;
									int num4 = 0;
									int num5 = 0;
									int num6 = 0;
									foreach (SlotModel slotModel in room.Slots)
									{
										if (slotModel.State >= SlotState.LOAD)
										{
											num4++;
											if (slotModel.Team == TeamEnum.FR_TEAM)
											{
												num5++;
											}
											else
											{
												num6++;
											}
											if (slotModel.State >= SlotState.BATTLE_READY)
											{
												num++;
												if (slotModel.Team == TeamEnum.FR_TEAM)
												{
													num3++;
												}
												else
												{
													num2++;
												}
											}
										}
									}
									bool flag2 = room.State == RoomState.BATTLE;
									bool flag3 = room.GetSlot(room.LeaderSlot).State >= SlotState.BATTLE_READY && flag && ((room.LeaderSlot % 2 == 0 && num3 > num5 / 2) || (room.LeaderSlot % 2 == 1 && num2 > num6 / 2));
									bool flag4 = room.GetSlot(room.LeaderSlot).State >= SlotState.BATTLE_READY && num2 > num6 / 2 && num3 > num5 / 2;
									bool flag5 = room.GetSlot(room.LeaderSlot).State >= SlotState.BATTLE_READY && room.RoomType == RoomCondition.FreeForAll && num >= 2 && num4 >= 2;
									bool flag6 = channelModel.Type == ChannelType.Clan && num == num3 + num2;
									bool flag7 = room.Competitive && num == num3 + num2;
									if (flag2 || flag3 || flag4 || flag5 || flag6 || flag7)
									{
										if (flag6)
										{
											CLogger.Print(string.Format("Starting Clan War Match with '{0}' players. FR: {1} CT: {2}", num, num3, num2), LoggerType.Warning, null);
										}
										if (flag7)
										{
											CLogger.Print(string.Format("Starting Competitive Match with '{0}' players. FR: {1} CT: {2}", num, num3, num2), LoggerType.Warning, null);
										}
										room.SpawnReadyPlayers();
									}
									return;
								}
							}
							this.Client.SendPacket(new PROTOCOL_SERVER_MESSAGE_KICK_BATTLE_PLAYER_ACK((EventErrorEnum)2147487755U));
							this.Client.SendPacket(new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(player, 0));
							room.ChangeSlotState(slot, SlotState.NORMAL, true);
							AllUtils.BattleEndPlayersCount(room, flag);
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_BATTLE_STARTBATTLE_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BATTLE_STARTBATTLE_REQ()
		{
		}
	}
}
