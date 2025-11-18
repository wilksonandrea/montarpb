using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x0200016E RID: 366
	public class PROTOCOL_BATTLE_READYBATTLE_REQ : GameClientPacket
	{
		// Token: 0x060003AC RID: 940 RVA: 0x00005202 File Offset: 0x00003402
		public override void Read()
		{
			this.viewerType_0 = (ViewerType)base.ReadC();
			base.ReadD();
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0001C774 File Offset: 0x0001A974
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					ChannelModel channelModel;
					SlotModel slotModel;
					if (room != null && room.GetLeader() != null && room.GetChannel(out channelModel) && room.GetSlot(player.SlotId, out slotModel))
					{
						if (slotModel.Equipment == null)
						{
							this.Client.SendPacket(new PROTOCOL_BATTLE_READYBATTLE_ACK(2147487915U));
						}
						else
						{
							MapMatch mapLimit = SystemMapXML.GetMapLimit((int)room.MapId, (int)room.Rule);
							if (mapLimit != null)
							{
								bool flag = room.IsBotMode();
								if (slotModel.ViewType != this.viewerType_0)
								{
									slotModel.ViewType = this.viewerType_0;
								}
								slotModel.SpecGM = (slotModel.ViewType == ViewerType.SpecGM && player.IsGM()) || (room.RoomType == RoomCondition.Ace && (slotModel.Id < 0 || slotModel.Id > 1));
								if (flag || !ConfigLoader.TournamentRule || !AllUtils.ClassicModeCheck(player, room))
								{
									int num = 0;
									int num2 = 0;
									int num3 = 0;
									AllUtils.GetReadyPlayers(room, ref num2, ref num3, ref num);
									if (room.LeaderSlot == player.SlotId)
									{
										if (room.State == RoomState.READY || room.State == RoomState.COUNTDOWN)
										{
											uint num4;
											uint num5;
											if (mapLimit.Limit == 8 && AllUtils.Check4vs4(room, true, ref num2, ref num3, ref num))
											{
												this.Client.SendPacket(new PROTOCOL_ROOM_UNREADY_4VS4_ACK());
											}
											else if (AllUtils.ClanMatchCheck(room, channelModel.Type, num, out num4))
											{
												this.Client.SendPacket(new PROTOCOL_BATTLE_READYBATTLE_ACK(num4));
											}
											else if (AllUtils.CompetitiveMatchCheck(player, room, out num5))
											{
												this.Client.SendPacket(new PROTOCOL_BATTLE_READYBATTLE_ACK(num5));
											}
											else if (num == 0 && (flag || room.RoomType == RoomCondition.Tutorial))
											{
												room.ChangeSlotState(slotModel, SlotState.READY, false);
												room.StartBattle(false);
												room.UpdateSlotsInfo();
											}
											else if (!flag && num > 0)
											{
												room.ChangeSlotState(slotModel, SlotState.READY, false);
												if (room.BalanceType != TeamBalance.None)
												{
													AllUtils.TryBalanceTeams(room);
												}
												if (room.ThisModeHaveCD())
												{
													if (room.State == RoomState.READY)
													{
														SlotModel[] array = new SlotModel[]
														{
															room.GetSlot(0),
															room.GetSlot(1)
														};
														if (room.RoomType == RoomCondition.Ace && (array[0].State != SlotState.READY || array[1].State != SlotState.READY))
														{
															this.Client.SendPacket(new PROTOCOL_BATTLE_READYBATTLE_ACK(2147487753U));
															room.ChangeSlotState(room.LeaderSlot, SlotState.NORMAL, false);
															room.StopCountDown(CountDownEnum.StopByHost, true);
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
														room.ChangeSlotState(room.LeaderSlot, SlotState.NORMAL, false);
														room.StopCountDown(CountDownEnum.StopByHost, true);
													}
												}
												else
												{
													room.StartBattle(false);
												}
												room.UpdateSlotsInfo();
											}
											else if (num == 0 && !flag)
											{
												this.Client.SendPacket(new PROTOCOL_BATTLE_READYBATTLE_ACK(2147487753U));
											}
										}
									}
									else
									{
										if (room.Slots[room.LeaderSlot].State >= SlotState.LOAD && room.IsPreparing())
										{
											if (slotModel.State != SlotState.NORMAL)
											{
												goto IL_454;
											}
											if (mapLimit.Limit == 8 && AllUtils.Check4vs4(room, false, ref num2, ref num2, ref num))
											{
												this.Client.SendPacket(new PROTOCOL_ROOM_UNREADY_4VS4_ACK());
												return;
											}
											if (room.BalanceType != TeamBalance.None && !flag)
											{
												AllUtils.TryBalancePlayer(room, player, true, ref slotModel);
											}
											room.ChangeSlotState(slotModel, SlotState.LOAD, true);
											slotModel.SetMissionsClone(player.Mission);
											this.Client.SendPacket(new PROTOCOL_BATTLE_READYBATTLE_ACK((uint)slotModel.State));
											this.Client.SendPacket(new PROTOCOL_BATTLE_START_GAME_ACK(room));
											using (PROTOCOL_BATTLE_START_GAME_TRANS_ACK protocol_BATTLE_START_GAME_TRANS_ACK = new PROTOCOL_BATTLE_START_GAME_TRANS_ACK(room, slotModel, player.Title))
											{
												room.SendPacketToPlayers(protocol_BATTLE_START_GAME_TRANS_ACK, SlotState.READY, 1, slotModel.Id);
												goto IL_454;
											}
										}
										if (slotModel.State == SlotState.NORMAL)
										{
											room.ChangeSlotState(slotModel, SlotState.READY, true);
										}
										else if (slotModel.State == SlotState.READY)
										{
											room.ChangeSlotState(slotModel, SlotState.NORMAL, false);
											if (room.State == RoomState.COUNTDOWN && room.GetPlayingPlayers((room.LeaderSlot % 2 == 0) ? TeamEnum.CT_TEAM : TeamEnum.FR_TEAM, SlotState.READY, 0) == 0)
											{
												room.ChangeSlotState(room.LeaderSlot, SlotState.NORMAL, false);
												room.StopCountDown(CountDownEnum.StopByPlayer, true);
											}
											room.UpdateSlotsInfo();
										}
									}
									IL_454:;
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_BATTLE_READYBATTLE_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BATTLE_READYBATTLE_REQ()
		{
		}

		// Token: 0x0400029F RID: 671
		private ViewerType viewerType_0;
	}
}
