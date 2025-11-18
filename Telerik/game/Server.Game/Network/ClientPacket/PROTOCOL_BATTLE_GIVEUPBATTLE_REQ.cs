using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_BATTLE_GIVEUPBATTLE_REQ : GameClientPacket
	{
		private bool bool_0;

		private long long_0;

		public PROTOCOL_BATTLE_GIVEUPBATTLE_REQ()
		{
		}

		public override void Read()
		{
			this.long_0 = (long)base.ReadD();
		}

		public override void Run()
		{
			SlotModel slotModel;
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					if (room != null && room.State >= RoomState.LOADING && room.GetSlot(player.SlotId, out slotModel) && slotModel.State >= SlotState.LOAD)
					{
						bool flag = room.IsBotMode();
						AllUtils.FreepassEffect(player, slotModel, room, flag);
						if (room.VoteTime.IsTimer() && room.VoteKick != null && room.VoteKick.VictimIdx == slotModel.Id)
						{
							room.VoteTime.StopJob();
							room.VoteKick = null;
							using (PROTOCOL_BATTLE_NOTIFY_KICKVOTE_CANCEL_ACK pROTOCOLBATTLENOTIFYKICKVOTECANCELACK = new PROTOCOL_BATTLE_NOTIFY_KICKVOTE_CANCEL_ACK())
							{
								room.SendPacketToPlayers(pROTOCOLBATTLENOTIFYKICKVOTECANCELACK, SlotState.BATTLE, 0, slotModel.Id);
							}
						}
						AllUtils.ResetSlotInfo(room, slotModel, true);
						int ınt32 = 0;
						int ınt321 = 0;
						int ınt322 = 0;
						int ınt323 = 0;
						SlotModel[] slots = room.Slots;
						for (int i = 0; i < (int)slots.Length; i++)
						{
							SlotModel slotModel1 = slots[i];
							if (slotModel1.State >= SlotState.LOAD)
							{
								if (slotModel1.Team != TeamEnum.FR_TEAM)
								{
									ınt323++;
								}
								else
								{
									ınt322++;
								}
								if (slotModel1.State == SlotState.BATTLE)
								{
									if (slotModel1.Team != TeamEnum.FR_TEAM)
									{
										ınt321++;
									}
									else
									{
										ınt32++;
									}
								}
							}
						}
						if (slotModel.Id == room.LeaderSlot)
						{
							if (flag)
							{
								if (ınt32 > 0 || ınt321 > 0)
								{
									AllUtils.LeaveHostGiveBattlePVE(room, player);
								}
								else
								{
									AllUtils.LeaveHostEndBattlePVE(room, player);
								}
							}
							else if ((room.State != RoomState.BATTLE || ınt32 != 0 && ınt321 != 0) && (room.State > RoomState.PRE_BATTLE || ınt322 != 0 && ınt323 != 0))
							{
								AllUtils.LeaveHostGiveBattlePVP(room, player);
							}
							else
							{
								AllUtils.LeaveHostEndBattlePVP(room, player, ınt32, ınt321, out this.bool_0);
							}
						}
						else if (flag)
						{
							AllUtils.LeavePlayerQuitBattle(room, player);
						}
						else if ((room.State != RoomState.BATTLE || ınt32 != 0 && ınt321 != 0) && (room.State > RoomState.PRE_BATTLE || ınt322 != 0 && ınt323 != 0))
						{
							AllUtils.LeavePlayerQuitBattle(room, player);
						}
						else
						{
							AllUtils.LeavePlayerEndBattlePVP(room, player, ınt32, ınt321, out this.bool_0);
						}
						this.Client.SendPacket(new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(player, 0));
						if (!this.bool_0 && room.State == RoomState.BATTLE)
						{
							AllUtils.BattleEndRoundPlayersCount(room);
						}
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_BATTLE_GIVEUPBATTLE_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}