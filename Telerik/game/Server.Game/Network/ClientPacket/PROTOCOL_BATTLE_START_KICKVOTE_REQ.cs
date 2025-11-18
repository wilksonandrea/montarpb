using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_BATTLE_START_KICKVOTE_REQ : GameClientPacket
	{
		private int int_0;

		private int int_1;

		private uint uint_0;

		public PROTOCOL_BATTLE_START_KICKVOTE_REQ()
		{
		}

		public override void Read()
		{
			this.int_1 = base.ReadC();
			this.int_0 = base.ReadC();
		}

		public override void Run()
		{
			int ınt32;
			int ınt321;
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
							room.GetPlayingPlayers(true, out ınt32, out ınt321);
							if (player.Rank < ConfigLoader.MinRankVote && !player.IsGM())
							{
								this.uint_0 = -2147479324;
							}
							else if (room.VoteTime.IsTimer())
							{
								this.uint_0 = -2147479328;
							}
							else if (slot.NextVoteDate > DateTimeUtil.Now())
							{
								this.uint_0 = -2147479327;
							}
							this.Client.SendPacket(new PROTOCOL_BATTLE_SUGGEST_KICKVOTE_ACK(this.uint_0));
							if (this.uint_0 <= 0)
							{
								slot.NextVoteDate = DateTimeUtil.Now().AddMinutes(1);
								room.VoteKick = new VoteKickModel(slot.Id, this.int_1)
								{
									Motive = this.int_0
								};
								for (int i = 0; i < 18; i++)
								{
									room.VoteKick.TotalArray[i] = room.Slots[i].State == SlotState.BATTLE;
								}
								using (PROTOCOL_BATTLE_START_KICKVOTE_ACK pROTOCOLBATTLESTARTKICKVOTEACK = new PROTOCOL_BATTLE_START_KICKVOTE_ACK(room.VoteKick))
								{
									room.SendPacketToPlayers(pROTOCOLBATTLESTARTKICKVOTEACK, SlotState.BATTLE, 0, player.SlotId, this.int_1);
								}
								room.StartVote();
							}
							else
							{
								return;
							}
						}
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_BATTLE_START_KICKVOTE_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}