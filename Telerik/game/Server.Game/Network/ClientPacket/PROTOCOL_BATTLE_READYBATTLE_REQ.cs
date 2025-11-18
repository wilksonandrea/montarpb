using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.XML;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_BATTLE_READYBATTLE_REQ : GameClientPacket
	{
		private ViewerType viewerType_0;

		public PROTOCOL_BATTLE_READYBATTLE_REQ()
		{
		}

		public override void Read()
		{
			this.viewerType_0 = (ViewerType)base.ReadC();
			base.ReadD();
		}

		public override void Run()
		{
			ChannelModel channelModel;
			SlotModel viewerType0;
			uint uInt32;
			uint uInt321;
			bool flag;
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					if (room != null && room.GetLeader() != null && room.GetChannel(out channelModel) && room.GetSlot(player.SlotId, out viewerType0))
					{
						if (viewerType0.Equipment != null)
						{
							MapMatch mapLimit = SystemMapXML.GetMapLimit((int)room.MapId, (int)room.Rule);
							if (mapLimit != null)
							{
								bool flag1 = room.IsBotMode();
								if (viewerType0.ViewType != this.viewerType_0)
								{
									viewerType0.ViewType = this.viewerType_0;
								}
								SlotModel slotModel = viewerType0;
								if (viewerType0.ViewType == ViewerType.SpecGM && player.IsGM())
								{
									flag = true;
								}
								else if (room.RoomType != RoomCondition.Ace)
								{
									flag = false;
								}
								else
								{
									flag = (viewerType0.Id < 0 ? true : viewerType0.Id > 1);
								}
								slotModel.SpecGM = flag;
								if (flag1 || !ConfigLoader.TournamentRule || !AllUtils.ClassicModeCheck(player, room))
								{
									int ınt32 = 0;
									int ınt321 = 0;
									int ınt322 = 0;
									AllUtils.GetReadyPlayers(room, ref ınt321, ref ınt322, ref ınt32);
									if (room.LeaderSlot != player.SlotId)
									{
										if (room.Slots[room.LeaderSlot].State >= SlotState.LOAD && room.IsPreparing())
										{
											if (viewerType0.State == SlotState.NORMAL)
											{
												if (mapLimit.Limit != 8 || !AllUtils.Check4vs4(room, false, ref ınt321, ref ınt321, ref ınt32))
												{
													if (room.BalanceType != TeamBalance.None && !flag1)
													{
														AllUtils.TryBalancePlayer(room, player, true, ref viewerType0);
													}
													room.ChangeSlotState(viewerType0, SlotState.LOAD, true);
													viewerType0.SetMissionsClone(player.Mission);
													this.Client.SendPacket(new PROTOCOL_BATTLE_READYBATTLE_ACK((uint)viewerType0.State));
													this.Client.SendPacket(new PROTOCOL_BATTLE_START_GAME_ACK(room));
													using (PROTOCOL_BATTLE_START_GAME_TRANS_ACK pROTOCOLBATTLESTARTGAMETRANSACK = new PROTOCOL_BATTLE_START_GAME_TRANS_ACK(room, viewerType0, player.Title))
													{
														room.SendPacketToPlayers(pROTOCOLBATTLESTARTGAMETRANSACK, SlotState.READY, 1, viewerType0.Id);
													}
												}
												else
												{
													this.Client.SendPacket(new PROTOCOL_ROOM_UNREADY_4VS4_ACK());
													return;
												}
											}
										}
										else if (viewerType0.State == SlotState.NORMAL)
										{
											room.ChangeSlotState(viewerType0, SlotState.READY, true);
										}
										else if (viewerType0.State == SlotState.READY)
										{
											room.ChangeSlotState(viewerType0, SlotState.NORMAL, false);
											if (room.State == RoomState.COUNTDOWN && room.GetPlayingPlayers((TeamEnum)(room.LeaderSlot % 2 == 0), SlotState.READY, 0) == 0)
											{
												room.ChangeSlotState(room.LeaderSlot, SlotState.NORMAL, false);
												room.StopCountDown(CountDownEnum.StopByPlayer, true);
											}
											room.UpdateSlotsInfo();
										}
									}
									else if (room.State != RoomState.READY && room.State != RoomState.COUNTDOWN)
									{
										return;
									}
									else if (mapLimit.Limit == 8 && AllUtils.Check4vs4(room, true, ref ınt321, ref ınt322, ref ınt32))
									{
										this.Client.SendPacket(new PROTOCOL_ROOM_UNREADY_4VS4_ACK());
										return;
									}
									else if (AllUtils.ClanMatchCheck(room, channelModel.Type, ınt32, out uInt32))
									{
										this.Client.SendPacket(new PROTOCOL_BATTLE_READYBATTLE_ACK(uInt32));
										return;
									}
									else if (AllUtils.CompetitiveMatchCheck(player, room, out uInt321))
									{
										this.Client.SendPacket(new PROTOCOL_BATTLE_READYBATTLE_ACK(uInt321));
										return;
									}
									else if (ınt32 == 0 && (flag1 || room.RoomType == RoomCondition.Tutorial))
									{
										room.ChangeSlotState(viewerType0, SlotState.READY, false);
										room.StartBattle(false);
										room.UpdateSlotsInfo();
									}
									else if (!flag1 && ınt32 > 0)
									{
										room.ChangeSlotState(viewerType0, SlotState.READY, false);
										if (room.BalanceType != TeamBalance.None)
										{
											AllUtils.TryBalanceTeams(room);
										}
										if (!room.ThisModeHaveCD())
										{
											room.StartBattle(false);
										}
										else if (room.State == RoomState.READY)
										{
											SlotModel[] slot = new SlotModel[] { room.GetSlot(0), room.GetSlot(1) };
											if (room.RoomType != RoomCondition.Ace || slot[0].State == SlotState.READY && slot[1].State == SlotState.READY)
											{
												room.State = RoomState.COUNTDOWN;
												room.UpdateRoomInfo();
												room.StartCountDown();
											}
											else
											{
												this.Client.SendPacket(new PROTOCOL_BATTLE_READYBATTLE_ACK(-2147479543));
												room.ChangeSlotState(room.LeaderSlot, SlotState.NORMAL, false);
												room.StopCountDown(CountDownEnum.StopByHost, true);
											}
										}
										else if (room.State == RoomState.COUNTDOWN)
										{
											room.ChangeSlotState(room.LeaderSlot, SlotState.NORMAL, false);
											room.StopCountDown(CountDownEnum.StopByHost, true);
										}
										room.UpdateSlotsInfo();
									}
									else if (ınt32 == 0 && !flag1)
									{
										this.Client.SendPacket(new PROTOCOL_BATTLE_READYBATTLE_ACK(-2147479543));
									}
								}
							}
						}
						else
						{
							this.Client.SendPacket(new PROTOCOL_BATTLE_READYBATTLE_ACK(-2147479381));
						}
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_BATTLE_READYBATTLE_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}