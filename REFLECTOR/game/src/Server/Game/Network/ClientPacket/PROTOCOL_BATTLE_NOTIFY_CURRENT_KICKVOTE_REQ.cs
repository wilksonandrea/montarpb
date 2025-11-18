namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Server.Game.Data.Models;
    using Server.Game.Data.Utils;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_BATTLE_NOTIFY_CURRENT_KICKVOTE_REQ : GameClientPacket
    {
        private byte byte_0;

        public override void Read()
        {
            this.byte_0 = base.ReadC();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    SlotModel model2;
                    RoomModel room = player.Room;
                    if (((room != null) && ((room.State == RoomState.BATTLE) && (room.VoteTime.IsTimer() && ((room.VoteKick != null) && room.GetSlot(player.SlotId, out model2))))) && (model2.State == SlotState.BATTLE))
                    {
                        VoteKickModel voteKick = room.VoteKick;
                        if (voteKick.Votes.Contains(player.SlotId))
                        {
                            base.Client.SendPacket(new PROTOCOL_BATTLE_VOTE_KICKVOTE_ACK(0x800010f1));
                        }
                        else
                        {
                            List<int> votes = voteKick.Votes;
                            lock (votes)
                            {
                                voteKick.Votes.Add(model2.Id);
                            }
                            if (this.byte_0 != 0)
                            {
                                voteKick.Denie++;
                            }
                            else
                            {
                                voteKick.Accept++;
                                if (model2.Team == (voteKick.VictimIdx % 2))
                                {
                                    voteKick.Allies++;
                                }
                                else
                                {
                                    voteKick.Enemies++;
                                }
                            }
                            if (voteKick.Votes.Count >= voteKick.GetInGamePlayers())
                            {
                                room.VoteTime.StopJob();
                                AllUtils.VotekickResult(room);
                            }
                            else
                            {
                                using (PROTOCOL_BATTLE_NOTIFY_CURRENT_KICKVOTE_ACK protocol_battle_notify_current_kickvote_ack = new PROTOCOL_BATTLE_NOTIFY_CURRENT_KICKVOTE_ACK(voteKick))
                                {
                                    room.SendPacketToPlayers(protocol_battle_notify_current_kickvote_ack, SlotState.BATTLE, 0);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_BATTLE_NOTIFY_CURRENT_KICKVOTE_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

