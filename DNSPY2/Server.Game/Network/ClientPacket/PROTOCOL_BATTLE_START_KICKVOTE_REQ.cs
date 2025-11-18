using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000173 RID: 371
	public class PROTOCOL_BATTLE_START_KICKVOTE_REQ : GameClientPacket
	{
		// Token: 0x060003BB RID: 955 RVA: 0x00005235 File Offset: 0x00003435
		public override void Read()
		{
			this.int_1 = (int)base.ReadC();
			this.int_0 = (int)base.ReadC();
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0001D604 File Offset: 0x0001B804
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					if (room != null && room.State == RoomState.BATTLE && player.SlotId != this.int_1)
					{
						SlotModel slot = room.GetSlot(player.SlotId);
						if (slot != null && slot.State == SlotState.BATTLE && room.GetSlot(this.int_1).State == SlotState.BATTLE)
						{
							int num;
							int num2;
							room.GetPlayingPlayers(true, out num, out num2);
							if (player.Rank < ConfigLoader.MinRankVote && !player.IsGM())
							{
								this.uint_0 = 2147487972U;
							}
							else if (room.VoteTime.IsTimer())
							{
								this.uint_0 = 2147487968U;
							}
							else if (slot.NextVoteDate > DateTimeUtil.Now())
							{
								this.uint_0 = 2147487969U;
							}
							this.Client.SendPacket(new PROTOCOL_BATTLE_SUGGEST_KICKVOTE_ACK(this.uint_0));
							if (this.uint_0 <= 0U)
							{
								slot.NextVoteDate = DateTimeUtil.Now().AddMinutes(1.0);
								VoteKickModel voteKickModel = new VoteKickModel(slot.Id, this.int_1)
								{
									Motive = this.int_0
								};
								room.VoteKick = voteKickModel;
								for (int i = 0; i < 18; i++)
								{
									room.VoteKick.TotalArray[i] = room.Slots[i].State == SlotState.BATTLE;
								}
								using (PROTOCOL_BATTLE_START_KICKVOTE_ACK protocol_BATTLE_START_KICKVOTE_ACK = new PROTOCOL_BATTLE_START_KICKVOTE_ACK(room.VoteKick))
								{
									room.SendPacketToPlayers(protocol_BATTLE_START_KICKVOTE_ACK, SlotState.BATTLE, 0, player.SlotId, this.int_1);
								}
								room.StartVote();
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_BATTLE_START_KICKVOTE_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BATTLE_START_KICKVOTE_REQ()
		{
		}

		// Token: 0x040002A4 RID: 676
		private int int_0;

		// Token: 0x040002A5 RID: 677
		private int int_1;

		// Token: 0x040002A6 RID: 678
		private uint uint_0;
	}
}
