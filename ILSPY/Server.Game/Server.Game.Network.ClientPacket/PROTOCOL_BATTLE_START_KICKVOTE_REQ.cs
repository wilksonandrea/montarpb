using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BATTLE_START_KICKVOTE_REQ : GameClientPacket
{
	private int int_0;

	private int int_1;

	private uint uint_0;

	public override void Read()
	{
		int_1 = ReadC();
		int_0 = ReadC();
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player == null)
			{
				return;
			}
			RoomModel room = player.Room;
			if (room == null || room.State != RoomState.BATTLE || player.SlotId == int_1)
			{
				return;
			}
			SlotModel slot = room.GetSlot(player.SlotId);
			if (slot == null || slot.State != SlotState.BATTLE || room.GetSlot(int_1).State != SlotState.BATTLE)
			{
				return;
			}
			room.GetPlayingPlayers(InBattle: true, out var _, out var _);
			if (player.Rank < ConfigLoader.MinRankVote && !player.IsGM())
			{
				uint_0 = 2147487972u;
			}
			else if (room.VoteTime.IsTimer())
			{
				uint_0 = 2147487968u;
			}
			else if (slot.NextVoteDate > DateTimeUtil.Now())
			{
				uint_0 = 2147487969u;
			}
			Client.SendPacket(new PROTOCOL_BATTLE_SUGGEST_KICKVOTE_ACK(uint_0));
			if (uint_0 == 0)
			{
				slot.NextVoteDate = DateTimeUtil.Now().AddMinutes(1.0);
				VoteKickModel voteKick = new VoteKickModel(slot.Id, int_1)
				{
					Motive = int_0
				};
				room.VoteKick = voteKick;
				for (int i = 0; i < 18; i++)
				{
					room.VoteKick.TotalArray[i] = room.Slots[i].State == SlotState.BATTLE;
				}
				using (PROTOCOL_BATTLE_START_KICKVOTE_ACK packet = new PROTOCOL_BATTLE_START_KICKVOTE_ACK(room.VoteKick))
				{
					room.SendPacketToPlayers(packet, SlotState.BATTLE, 0, player.SlotId, int_1);
				}
				room.StartVote();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_BATTLE_START_KICKVOTE_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
