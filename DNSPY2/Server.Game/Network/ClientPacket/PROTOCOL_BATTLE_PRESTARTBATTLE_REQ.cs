using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x0200016D RID: 365
	public class PROTOCOL_BATTLE_PRESTARTBATTLE_REQ : GameClientPacket
	{
		// Token: 0x060003A9 RID: 937 RVA: 0x000051D0 File Offset: 0x000033D0
		public override void Read()
		{
			this.mapIdEnum_0 = (MapIdEnum)base.ReadC();
			this.mapRules_0 = (MapRules)base.ReadC();
			this.stageOptions_0 = (StageOptions)base.ReadC();
			this.roomCondition_0 = (RoomCondition)base.ReadC();
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0001C508 File Offset: 0x0001A708
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					if (room != null)
					{
						if (room.Stage == this.stageOptions_0 && room.RoomType == this.roomCondition_0 && room.MapId == this.mapIdEnum_0)
						{
							if (room.Rule == this.mapRules_0)
							{
								SlotModel slot = room.GetSlot(player.SlotId);
								if (slot == null || !room.IsPreparing() || room.UdpServer == null || slot.State < SlotState.LOAD)
								{
									this.Client.SendPacket(new PROTOCOL_BATTLE_STARTBATTLE_ACK());
									room.ChangeSlotState(slot, SlotState.NORMAL, true);
									AllUtils.BattleEndPlayersCount(room, room.IsBotMode());
									slot.StopTiming();
									return;
								}
								Account leader = room.GetLeader();
								if (leader == null)
								{
									this.Client.SendPacket(new PROTOCOL_SERVER_MESSAGE_KICK_BATTLE_PLAYER_ACK((EventErrorEnum)2147487755U));
									this.Client.SendPacket(new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(player, 0));
									room.ChangeSlotState(slot, SlotState.NORMAL, true);
									AllUtils.BattleEndPlayersCount(room, room.IsBotMode());
									slot.StopTiming();
									return;
								}
								if (string.IsNullOrEmpty(player.PublicIP.ToString()))
								{
									this.Client.SendPacket(new PROTOCOL_SERVER_MESSAGE_KICK_BATTLE_PLAYER_ACK((EventErrorEnum)2147487752U));
									this.Client.SendPacket(new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(player, 0));
									room.ChangeSlotState(slot, SlotState.NORMAL, true);
									AllUtils.BattleEndPlayersCount(room, room.IsBotMode());
									slot.StopTiming();
									return;
								}
								slot.PreStartDate = DateTimeUtil.Now();
								if (slot.Id == room.LeaderSlot)
								{
									room.State = RoomState.PRE_BATTLE;
									room.UpdateRoomInfo();
								}
								room.ChangeSlotState(slot, SlotState.PRESTART, true);
								this.Client.SendPacket(new PROTOCOL_BATTLE_PRESTARTBATTLE_ACK(player, true));
								if (slot.Id != room.LeaderSlot)
								{
									leader.SendPacket(new PROTOCOL_BATTLE_PRESTARTBATTLE_ACK(player, false));
								}
								room.StartCounter(1, player, slot);
								goto IL_221;
							}
						}
						this.Client.SendPacket(new PROTOCOL_SERVER_MESSAGE_KICK_BATTLE_PLAYER_ACK((EventErrorEnum)2147487754U));
						this.Client.SendPacket(new PROTOCOL_BATTLE_PRESTARTBATTLE_ACK());
						room.ChangeSlotState(player.SlotId, SlotState.NORMAL, true);
						AllUtils.BattleEndPlayersCount(room, room.IsBotMode());
					}
					IL_221:;
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_BATTLE_PRESTARTBATTLE_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BATTLE_PRESTARTBATTLE_REQ()
		{
		}

		// Token: 0x0400029B RID: 667
		private StageOptions stageOptions_0;

		// Token: 0x0400029C RID: 668
		private MapRules mapRules_0;

		// Token: 0x0400029D RID: 669
		private MapIdEnum mapIdEnum_0;

		// Token: 0x0400029E RID: 670
		private RoomCondition roomCondition_0;
	}
}
