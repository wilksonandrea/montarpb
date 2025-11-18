using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x0200017C RID: 380
	public class PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ : GameClientPacket
	{
		// Token: 0x060003D8 RID: 984 RVA: 0x0001E960 File Offset: 0x0001CB60
		public override void Read()
		{
			this.int_3 = (int)base.ReadH();
			base.ReadD();
			base.ReadD();
			base.ReadH();
			this.string_0 = base.ReadU(46);
			this.mapIdEnum_0 = (MapIdEnum)base.ReadC();
			this.mapRules_0 = (MapRules)base.ReadC();
			this.stageOptions_0 = (StageOptions)base.ReadC();
			this.roomCondition_0 = (RoomCondition)base.ReadC();
			base.ReadC();
			base.ReadC();
			this.int_1 = (int)base.ReadC();
			this.int_2 = (int)base.ReadC();
			this.roomWeaponsFlag_0 = (RoomWeaponsFlag)base.ReadH();
			this.roomStageFlag_0 = (RoomStageFlag)base.ReadD();
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0001EA0C File Offset: 0x0001CC0C
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null && player.ClanId != 0)
				{
					ChannelModel channel = player.GetChannel();
					MatchModel match = player.Match;
					if (channel != null && match != null)
					{
						MatchModel match2 = channel.GetMatch(this.int_3);
						if (match2 != null)
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
										roomModel.SetSlotCount(this.int_1, true, false);
										roomModel.Ping = this.int_2;
										roomModel.WeaponsFlag = this.roomWeaponsFlag_0;
										roomModel.Flag = this.roomStageFlag_0;
										roomModel.Password = "";
										roomModel.KillTime = 3;
										if (roomModel.AddPlayer(player) >= 0)
										{
											channel.AddRoom(roomModel);
											this.Client.SendPacket(new PROTOCOL_ROOM_CREATE_ACK(0U, roomModel));
											this.int_0 = i;
											return;
										}
									}
								}
							}
							using (PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK protocol_CLAN_WAR_ENEMY_INFO_ACK = new PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK(match2))
							{
								using (PROTOCOL_CLAN_WAR_JOIN_ROOM_ACK protocol_CLAN_WAR_JOIN_ROOM_ACK = new PROTOCOL_CLAN_WAR_JOIN_ROOM_ACK(match2, this.int_0, 0))
								{
									byte[] completeBytes = protocol_CLAN_WAR_ENEMY_INFO_ACK.GetCompleteBytes("PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ-1");
									byte[] completeBytes2 = protocol_CLAN_WAR_JOIN_ROOM_ACK.GetCompleteBytes("PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ-2");
									foreach (Account account in match.GetAllPlayers(match.Leader))
									{
										account.SendCompletePacket(completeBytes, protocol_CLAN_WAR_ENEMY_INFO_ACK.GetType().Name);
										account.SendCompletePacket(completeBytes2, protocol_CLAN_WAR_JOIN_ROOM_ACK.GetType().Name);
										if (account.Match != null)
										{
											match.Slots[account.MatchSlot].State = SlotMatchState.Ready;
										}
									}
								}
							}
							using (PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK protocol_CLAN_WAR_ENEMY_INFO_ACK2 = new PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK(match))
							{
								using (PROTOCOL_CLAN_WAR_JOIN_ROOM_ACK protocol_CLAN_WAR_JOIN_ROOM_ACK2 = new PROTOCOL_CLAN_WAR_JOIN_ROOM_ACK(match, this.int_0, 1))
								{
									byte[] completeBytes3 = protocol_CLAN_WAR_ENEMY_INFO_ACK2.GetCompleteBytes("PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ-3");
									byte[] completeBytes4 = protocol_CLAN_WAR_JOIN_ROOM_ACK2.GetCompleteBytes("PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ-4");
									foreach (Account account2 in match2.GetAllPlayers())
									{
										account2.SendCompletePacket(completeBytes3, protocol_CLAN_WAR_ENEMY_INFO_ACK2.GetType().Name);
										account2.SendCompletePacket(completeBytes4, protocol_CLAN_WAR_JOIN_ROOM_ACK2.GetType().Name);
										if (account2.Match != null)
										{
											match.Slots[account2.MatchSlot].State = SlotMatchState.Ready;
										}
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000528C File Offset: 0x0000348C
		public PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ()
		{
		}

		// Token: 0x040002BF RID: 703
		private int int_0 = -1;

		// Token: 0x040002C0 RID: 704
		private int int_1;

		// Token: 0x040002C1 RID: 705
		private int int_2;

		// Token: 0x040002C2 RID: 706
		private int int_3;

		// Token: 0x040002C3 RID: 707
		private string string_0;

		// Token: 0x040002C4 RID: 708
		private StageOptions stageOptions_0;

		// Token: 0x040002C5 RID: 709
		private MapIdEnum mapIdEnum_0;

		// Token: 0x040002C6 RID: 710
		private MapRules mapRules_0;

		// Token: 0x040002C7 RID: 711
		private RoomCondition roomCondition_0;

		// Token: 0x040002C8 RID: 712
		private RoomWeaponsFlag roomWeaponsFlag_0;

		// Token: 0x040002C9 RID: 713
		private RoomStageFlag roomStageFlag_0;
	}
}
