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
using System.Collections.Generic;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_BATTLE_NOTIFY_CURRENT_KICKVOTE_REQ : GameClientPacket
	{
		private byte byte_0;

		public PROTOCOL_BATTLE_NOTIFY_CURRENT_KICKVOTE_REQ()
		{
		}

		public override void Read()
		{
			this.byte_0 = base.ReadC();
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
					if (room != null && room.State == RoomState.BATTLE && room.VoteTime.IsTimer() && room.VoteKick != null && room.GetSlot(player.SlotId, out slotModel))
					{
						if (slotModel.State == SlotState.BATTLE)
						{
							VoteKickModel voteKick = room.VoteKick;
							if (!voteKick.Votes.Contains(player.SlotId))
							{
								lock (voteKick.Votes)
								{
									voteKick.Votes.Add(slotModel.Id);
								}
								if (this.byte_0 != 0)
								{
									VoteKickModel denie = voteKick;
									denie.Denie = denie.Denie + 1;
								}
								else
								{
									VoteKickModel accept = voteKick;
									accept.Accept = accept.Accept + 1;
									if ((int)slotModel.Team != voteKick.VictimIdx % 2)
									{
										VoteKickModel enemies = voteKick;
										enemies.Enemies = enemies.Enemies + 1;
									}
									else
									{
										VoteKickModel allies = voteKick;
										allies.Allies = allies.Allies + 1;
									}
								}
								if (voteKick.Votes.Count < voteKick.GetInGamePlayers())
								{
									using (PROTOCOL_BATTLE_NOTIFY_CURRENT_KICKVOTE_ACK pROTOCOLBATTLENOTIFYCURRENTKICKVOTEACK = new PROTOCOL_BATTLE_NOTIFY_CURRENT_KICKVOTE_ACK(voteKick))
									{
										room.SendPacketToPlayers(pROTOCOLBATTLENOTIFYCURRENTKICKVOTEACK, SlotState.BATTLE, 0);
									}
								}
								else
								{
									room.VoteTime.StopJob();
									AllUtils.VotekickResult(room);
								}
								return;
							}
							else
							{
								this.Client.SendPacket(new PROTOCOL_BATTLE_VOTE_KICKVOTE_ACK(-2147479311));
								return;
							}
						}
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_BATTLE_NOTIFY_CURRENT_KICKVOTE_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}