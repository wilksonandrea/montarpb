using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BATTLE_NOTIFY_CURRENT_KICKVOTE_REQ : GameClientPacket
{
	private byte byte_0;

	public override void Read()
	{
		byte_0 = ReadC();
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
			if (room == null || room.State != RoomState.BATTLE || !room.VoteTime.IsTimer() || room.VoteKick == null || !room.GetSlot(player.SlotId, out var Slot) || Slot.State != SlotState.BATTLE)
			{
				return;
			}
			VoteKickModel voteKick = room.VoteKick;
			if (voteKick.Votes.Contains(player.SlotId))
			{
				Client.SendPacket(new PROTOCOL_BATTLE_VOTE_KICKVOTE_ACK(2147487985u));
				return;
			}
			lock (voteKick.Votes)
			{
				voteKick.Votes.Add(Slot.Id);
			}
			if (byte_0 == 0)
			{
				voteKick.Accept++;
				if (Slot.Team == (TeamEnum)(voteKick.VictimIdx % 2))
				{
					voteKick.Allies++;
				}
				else
				{
					voteKick.Enemies++;
				}
			}
			else
			{
				voteKick.Denie++;
			}
			if (voteKick.Votes.Count >= voteKick.GetInGamePlayers())
			{
				room.VoteTime.StopJob();
				AllUtils.VotekickResult(room);
				return;
			}
			using PROTOCOL_BATTLE_NOTIFY_CURRENT_KICKVOTE_ACK packet = new PROTOCOL_BATTLE_NOTIFY_CURRENT_KICKVOTE_ACK(voteKick);
			room.SendPacketToPlayers(packet, SlotState.BATTLE, 0);
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_BATTLE_NOTIFY_CURRENT_KICKVOTE_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
