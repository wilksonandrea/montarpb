using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001C8 RID: 456
	public class PROTOCOL_ROOM_CREATE_REQ : GameClientPacket
	{
		// Token: 0x060004D4 RID: 1236 RVA: 0x000255F0 File Offset: 0x000237F0
		public override void Read()
		{
			base.ReadD();
			this.string_0 = base.ReadU(46);
			this.mapIdEnum_0 = (MapIdEnum)base.ReadC();
			this.mapRules_0 = (MapRules)base.ReadC();
			this.stageOptions_0 = (StageOptions)base.ReadC();
			this.roomCondition_0 = (RoomCondition)base.ReadC();
			this.roomState_0 = (RoomState)base.ReadC();
			this.int_3 = (int)base.ReadC();
			this.int_0 = (int)base.ReadC();
			this.int_1 = (int)base.ReadC();
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

		// Token: 0x060004D5 RID: 1237 RVA: 0x00025750 File Offset: 0x00023950
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					ChannelModel channel = player.GetChannel();
					if (channel != null && player.Nickname.Length != 0 && player.Room == null && player.Match == null)
					{
						List<RoomModel> rooms = channel.Rooms;
						lock (rooms)
						{
							for (int i = 0; i < channel.MaxRooms; i++)
							{
								if (channel.GetRoom(i) == null)
								{
									RoomModel roomModel = new RoomModel(i, channel)
									{
										Name = this.string_0,
										MapId = this.mapIdEnum_0,
										Rule = this.mapRules_0,
										Stage = this.stageOptions_0,
										RoomType = this.roomCondition_0
									};
									roomModel.GenerateSeed();
									roomModel.State = ((this.roomState_0 < RoomState.READY) ? RoomState.READY : this.roomState_0);
									roomModel.LeaderName = ((this.string_2.Equals("") || !this.string_2.Equals(player.Nickname)) ? player.Nickname : this.string_2);
									roomModel.Ping = this.int_1;
									roomModel.WeaponsFlag = this.roomWeaponsFlag_0;
									roomModel.Flag = this.roomStageFlag_0;
									roomModel.NewInt = this.int_4;
									bool flag2;
									if ((flag2 = roomModel.IsBotMode()) && roomModel.ChannelType == ChannelType.Clan)
									{
										this.uint_0 = 2147487869U;
									}
									else
									{
										roomModel.KillTime = this.int_2;
										roomModel.Limit = ((channel.Type == ChannelType.Clan) ? 1 : this.byte_2);
										roomModel.WatchRuleFlag = ((roomModel.RoomType == RoomCondition.Ace) ? 142 : this.byte_3);
										roomModel.BalanceType = ((channel.Type == ChannelType.Clan || roomModel.RoomType == RoomCondition.Ace) ? TeamBalance.None : this.teamBalance_0);
										roomModel.RandomMaps = this.byte_0;
										roomModel.CountdownIG = this.byte_8;
										roomModel.LeaderAddr = this.byte_1;
										roomModel.KillCam = this.byte_7;
										roomModel.Password = this.string_1;
										if (flag2)
										{
											roomModel.AiCount = this.byte_4;
											roomModel.AiLevel = this.byte_5;
											roomModel.AiType = this.byte_6;
										}
										roomModel.SetSlotCount(this.int_0, true, false);
										roomModel.CountPlayers = this.int_3;
										roomModel.CountMaxSlots = this.int_0;
										if (roomModel.AddPlayer(player) < 0)
										{
											goto IL_267;
										}
										player.ResetPages();
										channel.AddRoom(roomModel);
										this.Client.SendPacket(new PROTOCOL_ROOM_CREATE_ACK(this.uint_0, roomModel));
										if (roomModel.IsBotMode())
										{
											roomModel.ChangeSlotState(1, SlotState.CLOSE, true);
											roomModel.ChangeSlotState(3, SlotState.CLOSE, true);
											roomModel.ChangeSlotState(5, SlotState.CLOSE, true);
											roomModel.ChangeSlotState(7, SlotState.CLOSE, true);
											this.Client.SendPacket(new PROTOCOL_ROOM_GET_SLOTINFO_ACK(roomModel));
										}
									}
									break;
								}
								IL_267:;
							}
						}
					}
					else
					{
						this.uint_0 = 2147483648U;
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_LOBBY_CREATE_ROOM_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_ROOM_CREATE_REQ()
		{
		}

		// Token: 0x0400035C RID: 860
		private uint uint_0;

		// Token: 0x0400035D RID: 861
		private string string_0;

		// Token: 0x0400035E RID: 862
		private string string_1;

		// Token: 0x0400035F RID: 863
		private string string_2;

		// Token: 0x04000360 RID: 864
		private MapIdEnum mapIdEnum_0;

		// Token: 0x04000361 RID: 865
		private MapRules mapRules_0;

		// Token: 0x04000362 RID: 866
		private StageOptions stageOptions_0;

		// Token: 0x04000363 RID: 867
		private TeamBalance teamBalance_0;

		// Token: 0x04000364 RID: 868
		private byte[] byte_0;

		// Token: 0x04000365 RID: 869
		private byte[] byte_1;

		// Token: 0x04000366 RID: 870
		private int int_0;

		// Token: 0x04000367 RID: 871
		private int int_1;

		// Token: 0x04000368 RID: 872
		private int int_2;

		// Token: 0x04000369 RID: 873
		private int int_3;

		// Token: 0x0400036A RID: 874
		private int int_4;

		// Token: 0x0400036B RID: 875
		private byte byte_2;

		// Token: 0x0400036C RID: 876
		private byte byte_3;

		// Token: 0x0400036D RID: 877
		private byte byte_4;

		// Token: 0x0400036E RID: 878
		private byte byte_5;

		// Token: 0x0400036F RID: 879
		private byte byte_6;

		// Token: 0x04000370 RID: 880
		private byte byte_7;

		// Token: 0x04000371 RID: 881
		private byte byte_8;

		// Token: 0x04000372 RID: 882
		private RoomCondition roomCondition_0;

		// Token: 0x04000373 RID: 883
		private RoomState roomState_0;

		// Token: 0x04000374 RID: 884
		private RoomWeaponsFlag roomWeaponsFlag_0;

		// Token: 0x04000375 RID: 885
		private RoomStageFlag roomStageFlag_0;
	}
}
