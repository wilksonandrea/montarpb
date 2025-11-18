using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ClientPacket
{
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

		public PROTOCOL_ROOM_CREATE_REQ()
		{
		}

		public override void Read()
		{
			base.ReadD();
			this.string_0 = base.ReadU(46);
			this.mapIdEnum_0 = (MapIdEnum)base.ReadC();
			this.mapRules_0 = (MapRules)base.ReadC();
			this.stageOptions_0 = (StageOptions)base.ReadC();
			this.roomCondition_0 = (RoomCondition)base.ReadC();
			this.roomState_0 = (RoomState)base.ReadC();
			this.int_3 = base.ReadC();
			this.int_0 = base.ReadC();
			this.int_1 = base.ReadC();
			this.roomWeaponsFlag_0 = (RoomWeaponsFlag)base.ReadH();
			this.roomStageFlag_0 = (RoomStageFlag)base.ReadD();
			base.ReadH();
			this.int_4 = base.ReadD();
			base.ReadH();
			this.string_2 = base.ReadU(66);
			this.int_2 = base.ReadD();
			this.byte_2 = base.ReadC();
			this.byte_3 = base.ReadC();
			this.teamBalance_0 = (TeamBalance)base.ReadH();
			this.byte_0 = base.ReadB(24);
			this.byte_8 = base.ReadC();
			this.byte_1 = base.ReadB(4);
			this.byte_7 = base.ReadC();
			base.ReadH();
			this.string_1 = base.ReadS(4);
			this.byte_4 = base.ReadC();
			this.byte_5 = base.ReadC();
			this.byte_6 = base.ReadC();
		}

		public override void Run()
		{
			object byte2;
			object byte3;
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					ChannelModel channel = player.GetChannel();
					if (channel == null || player.Nickname.Length == 0 || player.Room != null || player.Match != null)
					{
						this.uint_0 = -2147483648;
					}
					else
					{
						lock (channel.Rooms)
						{
							int ınt32 = 0;
							while (true)
							{
								if (ınt32 < channel.MaxRooms)
								{
									if (channel.GetRoom(ınt32) == null)
									{
										RoomModel roomModel = new RoomModel(ınt32, channel)
										{
											Name = this.string_0,
											MapId = this.mapIdEnum_0,
											Rule = this.mapRules_0,
											Stage = this.stageOptions_0,
											RoomType = this.roomCondition_0
										};
										roomModel.GenerateSeed();
										roomModel.State = (this.roomState_0 < RoomState.READY ? RoomState.READY : this.roomState_0);
										roomModel.LeaderName = (this.string_2.Equals("") || !this.string_2.Equals(player.Nickname) ? player.Nickname : this.string_2);
										roomModel.Ping = this.int_1;
										roomModel.WeaponsFlag = this.roomWeaponsFlag_0;
										roomModel.Flag = this.roomStageFlag_0;
										roomModel.NewInt = this.int_4;
										bool flag = roomModel.IsBotMode();
										bool flag1 = flag;
										if (!flag || roomModel.ChannelType != ChannelType.Clan)
										{
											roomModel.KillTime = this.int_2;
											RoomModel roomModel1 = roomModel;
											if (channel.Type == ChannelType.Clan)
											{
												byte2 = 1;
											}
											else
											{
												byte2 = this.byte_2;
											}
											roomModel1.Limit = (byte)byte2;
											RoomModel roomModel2 = roomModel;
											if (roomModel.RoomType == RoomCondition.Ace)
											{
												byte3 = 142;
											}
											else
											{
												byte3 = this.byte_3;
											}
											roomModel2.WatchRuleFlag = (byte)byte3;
											roomModel.BalanceType = (channel.Type == ChannelType.Clan || roomModel.RoomType == RoomCondition.Ace ? TeamBalance.None : this.teamBalance_0);
											roomModel.RandomMaps = this.byte_0;
											roomModel.CountdownIG = this.byte_8;
											roomModel.LeaderAddr = this.byte_1;
											roomModel.KillCam = this.byte_7;
											roomModel.Password = this.string_1;
											if (flag1)
											{
												roomModel.AiCount = this.byte_4;
												roomModel.AiLevel = this.byte_5;
												roomModel.AiType = this.byte_6;
											}
											roomModel.SetSlotCount(this.int_0, true, false);
											roomModel.CountPlayers = this.int_3;
											roomModel.CountMaxSlots = this.int_0;
											if (roomModel.AddPlayer(player) >= 0)
											{
												player.ResetPages();
												channel.AddRoom(roomModel);
												this.Client.SendPacket(new PROTOCOL_ROOM_CREATE_ACK(this.uint_0, roomModel));
												if (!roomModel.IsBotMode())
												{
													break;
												}
												roomModel.ChangeSlotState(1, SlotState.CLOSE, true);
												roomModel.ChangeSlotState(3, SlotState.CLOSE, true);
												roomModel.ChangeSlotState(5, SlotState.CLOSE, true);
												roomModel.ChangeSlotState(7, SlotState.CLOSE, true);
												this.Client.SendPacket(new PROTOCOL_ROOM_GET_SLOTINFO_ACK(roomModel));
												break;
											}
										}
										else
										{
											this.uint_0 = -2147479427;
											break;
										}
									}
									ınt32++;
								}
								else
								{
									break;
								}
							}
						}
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_LOBBY_CREATE_ROOM_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}