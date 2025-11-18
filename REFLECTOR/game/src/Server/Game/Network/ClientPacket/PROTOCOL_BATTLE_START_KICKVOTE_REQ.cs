namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Utility;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_BATTLE_START_KICKVOTE_REQ : GameClientPacket
    {
        private int int_0;
        private int int_1;
        private uint uint_0;

        public override void Read()
        {
            this.int_1 = base.ReadC();
            this.int_0 = base.ReadC();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    RoomModel room = player.Room;
                    if (((room != null) && (room.State == RoomState.BATTLE)) && (player.SlotId != this.int_1))
                    {
                        SlotModel slot = room.GetSlot(player.SlotId);
                        if (((slot != null) && (slot.State == SlotState.BATTLE)) && (room.GetSlot(this.int_1).State == SlotState.BATTLE))
                        {
                            int num;
                            int num2;
                            room.GetPlayingPlayers(true, out num, out num2);
                            if ((player.Rank < ConfigLoader.MinRankVote) && !player.IsGM())
                            {
                                this.uint_0 = 0x800010e4;
                            }
                            else if (room.VoteTime.IsTimer())
                            {
                                this.uint_0 = 0x800010e0;
                            }
                            else if (slot.NextVoteDate > DateTimeUtil.Now())
                            {
                                this.uint_0 = 0x800010e1;
                            }
                            base.Client.SendPacket(new PROTOCOL_BATTLE_SUGGEST_KICKVOTE_ACK(this.uint_0));
                            if (this.uint_0 <= 0)
                            {
                                slot.NextVoteDate = DateTimeUtil.Now().AddMinutes(1.0);
                                VoteKickModel model1 = new VoteKickModel(slot.Id, this.int_1);
                                model1.Motive = this.int_0;
                                room.VoteKick = model1;
                                int index = 0;
                                while (true)
                                {
                                    if (index >= 0x12)
                                    {
                                        using (PROTOCOL_BATTLE_START_KICKVOTE_ACK protocol_battle_start_kickvote_ack = new PROTOCOL_BATTLE_START_KICKVOTE_ACK(room.VoteKick))
                                        {
                                            room.SendPacketToPlayers(protocol_battle_start_kickvote_ack, SlotState.BATTLE, 0, player.SlotId, this.int_1);
                                        }
                                        room.StartVote();
                                        break;
                                    }
                                    room.VoteKick.TotalArray[index] = room.Slots[index].State == SlotState.BATTLE;
                                    index++;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_BATTLE_START_KICKVOTE_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

