using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000164 RID: 356
	public class PROTOCOL_BATTLE_GIVEUPBATTLE_REQ : GameClientPacket
	{
		// Token: 0x0600038E RID: 910 RVA: 0x0000515A File Offset: 0x0000335A
		public override void Read()
		{
			this.long_0 = (long)base.ReadD();
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0001B900 File Offset: 0x00019B00
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					SlotModel slotModel;
					if (room != null && room.State >= RoomState.LOADING && room.GetSlot(player.SlotId, out slotModel) && slotModel.State >= SlotState.LOAD)
					{
						bool flag = room.IsBotMode();
						AllUtils.FreepassEffect(player, slotModel, room, flag);
						if (room.VoteTime.IsTimer() && room.VoteKick != null && room.VoteKick.VictimIdx == slotModel.Id)
						{
							room.VoteTime.StopJob();
							room.VoteKick = null;
							using (PROTOCOL_BATTLE_NOTIFY_KICKVOTE_CANCEL_ACK protocol_BATTLE_NOTIFY_KICKVOTE_CANCEL_ACK = new PROTOCOL_BATTLE_NOTIFY_KICKVOTE_CANCEL_ACK())
							{
								room.SendPacketToPlayers(protocol_BATTLE_NOTIFY_KICKVOTE_CANCEL_ACK, SlotState.BATTLE, 0, slotModel.Id);
							}
						}
						AllUtils.ResetSlotInfo(room, slotModel, true);
						int num = 0;
						int num2 = 0;
						int num3 = 0;
						int num4 = 0;
						foreach (SlotModel slotModel2 in room.Slots)
						{
							if (slotModel2.State >= SlotState.LOAD)
							{
								if (slotModel2.Team == TeamEnum.FR_TEAM)
								{
									num3++;
								}
								else
								{
									num4++;
								}
								if (slotModel2.State == SlotState.BATTLE)
								{
									if (slotModel2.Team == TeamEnum.FR_TEAM)
									{
										num++;
									}
									else
									{
										num2++;
									}
								}
							}
						}
						if (slotModel.Id == room.LeaderSlot)
						{
							if (flag)
							{
								if (num <= 0 && num2 <= 0)
								{
									AllUtils.LeaveHostEndBattlePVE(room, player);
								}
								else
								{
									AllUtils.LeaveHostGiveBattlePVE(room, player);
								}
							}
							else if ((room.State == RoomState.BATTLE && (num == 0 || num2 == 0)) || (room.State <= RoomState.PRE_BATTLE && (num3 == 0 || num4 == 0)))
							{
								AllUtils.LeaveHostEndBattlePVP(room, player, num, num2, out this.bool_0);
							}
							else
							{
								AllUtils.LeaveHostGiveBattlePVP(room, player);
							}
						}
						else if (!flag)
						{
							if ((room.State == RoomState.BATTLE && (num == 0 || num2 == 0)) || (room.State <= RoomState.PRE_BATTLE && (num3 == 0 || num4 == 0)))
							{
								AllUtils.LeavePlayerEndBattlePVP(room, player, num, num2, out this.bool_0);
							}
							else
							{
								AllUtils.LeavePlayerQuitBattle(room, player);
							}
						}
						else
						{
							AllUtils.LeavePlayerQuitBattle(room, player);
						}
						this.Client.SendPacket(new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(player, 0));
						if (!this.bool_0 && room.State == RoomState.BATTLE)
						{
							AllUtils.BattleEndRoundPlayersCount(room);
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_BATTLE_GIVEUPBATTLE_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BATTLE_GIVEUPBATTLE_REQ()
		{
		}

		// Token: 0x04000288 RID: 648
		private bool bool_0;

		// Token: 0x04000289 RID: 649
		private long long_0;
	}
}
