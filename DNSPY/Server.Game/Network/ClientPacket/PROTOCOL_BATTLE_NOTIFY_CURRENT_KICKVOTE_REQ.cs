using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x0200016C RID: 364
	public class PROTOCOL_BATTLE_NOTIFY_CURRENT_KICKVOTE_REQ : GameClientPacket
	{
		// Token: 0x060003A6 RID: 934 RVA: 0x000051C2 File Offset: 0x000033C2
		public override void Read()
		{
			this.byte_0 = base.ReadC();
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0001C310 File Offset: 0x0001A510
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					SlotModel slotModel;
					if (room != null && room.State == RoomState.BATTLE && room.VoteTime.IsTimer() && room.VoteKick != null && room.GetSlot(player.SlotId, out slotModel))
					{
						if (slotModel.State == SlotState.BATTLE)
						{
							VoteKickModel voteKick = room.VoteKick;
							if (voteKick.Votes.Contains(player.SlotId))
							{
								this.Client.SendPacket(new PROTOCOL_BATTLE_VOTE_KICKVOTE_ACK(2147487985U));
							}
							else
							{
								List<int> votes = voteKick.Votes;
								lock (votes)
								{
									voteKick.Votes.Add(slotModel.Id);
								}
								if (this.byte_0 == 0)
								{
									VoteKickModel voteKickModel = voteKick;
									int num = voteKickModel.Accept;
									voteKickModel.Accept = num + 1;
									if (slotModel.Team == (TeamEnum)(voteKick.VictimIdx % 2))
									{
										VoteKickModel voteKickModel2 = voteKick;
										num = voteKickModel2.Allies;
										voteKickModel2.Allies = num + 1;
									}
									else
									{
										VoteKickModel voteKickModel3 = voteKick;
										num = voteKickModel3.Enemies;
										voteKickModel3.Enemies = num + 1;
									}
								}
								else
								{
									VoteKickModel voteKickModel4 = voteKick;
									int num = voteKickModel4.Denie;
									voteKickModel4.Denie = num + 1;
								}
								if (voteKick.Votes.Count >= voteKick.GetInGamePlayers())
								{
									room.VoteTime.StopJob();
									AllUtils.VotekickResult(room);
								}
								else
								{
									using (PROTOCOL_BATTLE_NOTIFY_CURRENT_KICKVOTE_ACK protocol_BATTLE_NOTIFY_CURRENT_KICKVOTE_ACK = new PROTOCOL_BATTLE_NOTIFY_CURRENT_KICKVOTE_ACK(voteKick))
									{
										room.SendPacketToPlayers(protocol_BATTLE_NOTIFY_CURRENT_KICKVOTE_ACK, SlotState.BATTLE, 0);
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_BATTLE_NOTIFY_CURRENT_KICKVOTE_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BATTLE_NOTIFY_CURRENT_KICKVOTE_REQ()
		{
		}

		// Token: 0x0400029A RID: 666
		private byte byte_0;
	}
}
