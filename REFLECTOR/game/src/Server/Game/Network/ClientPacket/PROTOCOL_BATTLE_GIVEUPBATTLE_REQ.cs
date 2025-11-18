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

    public class PROTOCOL_BATTLE_GIVEUPBATTLE_REQ : GameClientPacket
    {
        private bool bool_0;
        private long long_0;

        public override void Read()
        {
            this.long_0 = base.ReadD();
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
                    if (((room != null) && ((room.State >= RoomState.LOADING) && room.GetSlot(player.SlotId, out model2))) && (model2.State >= SlotState.LOAD))
                    {
                        bool flag = room.IsBotMode();
                        AllUtils.FreepassEffect(player, model2, room, flag);
                        if (room.VoteTime.IsTimer() && ((room.VoteKick != null) && (room.VoteKick.VictimIdx == model2.Id)))
                        {
                            room.VoteTime.StopJob();
                            room.VoteKick = null;
                            using (PROTOCOL_BATTLE_NOTIFY_KICKVOTE_CANCEL_ACK protocol_battle_notify_kickvote_cancel_ack = new PROTOCOL_BATTLE_NOTIFY_KICKVOTE_CANCEL_ACK())
                            {
                                room.SendPacketToPlayers(protocol_battle_notify_kickvote_cancel_ack, SlotState.BATTLE, 0, model2.Id);
                            }
                        }
                        AllUtils.ResetSlotInfo(room, model2, true);
                        int teamFR = 0;
                        int teamCT = 0;
                        int num3 = 0;
                        int num4 = 0;
                        SlotModel[] slots = room.Slots;
                        int index = 0;
                        while (true)
                        {
                            if (index >= slots.Length)
                            {
                                if (model2.Id != room.LeaderSlot)
                                {
                                    if (flag)
                                    {
                                        AllUtils.LeavePlayerQuitBattle(room, player);
                                    }
                                    else if (((room.State == RoomState.BATTLE) && ((teamFR == 0) || (teamCT == 0))) || ((room.State <= RoomState.PRE_BATTLE) && ((num3 == 0) || (num4 == 0))))
                                    {
                                        AllUtils.LeavePlayerEndBattlePVP(room, player, teamFR, teamCT, out this.bool_0);
                                    }
                                    else
                                    {
                                        AllUtils.LeavePlayerQuitBattle(room, player);
                                    }
                                }
                                else if (flag)
                                {
                                    if ((teamFR <= 0) && (teamCT <= 0))
                                    {
                                        AllUtils.LeaveHostEndBattlePVE(room, player);
                                    }
                                    else
                                    {
                                        AllUtils.LeaveHostGiveBattlePVE(room, player);
                                    }
                                }
                                else if (((room.State == RoomState.BATTLE) && ((teamFR == 0) || (teamCT == 0))) || ((room.State <= RoomState.PRE_BATTLE) && ((num3 == 0) || (num4 == 0))))
                                {
                                    AllUtils.LeaveHostEndBattlePVP(room, player, teamFR, teamCT, out this.bool_0);
                                }
                                else
                                {
                                    AllUtils.LeaveHostGiveBattlePVP(room, player);
                                }
                                base.Client.SendPacket(new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(player, 0));
                                if (!this.bool_0 && (room.State == RoomState.BATTLE))
                                {
                                    AllUtils.BattleEndRoundPlayersCount(room);
                                }
                                break;
                            }
                            SlotModel model3 = slots[index];
                            if (model3.State >= SlotState.LOAD)
                            {
                                if (model3.Team == TeamEnum.FR_TEAM)
                                {
                                    num3++;
                                }
                                else
                                {
                                    num4++;
                                }
                                if (model3.State == SlotState.BATTLE)
                                {
                                    if (model3.Team == TeamEnum.FR_TEAM)
                                    {
                                        teamFR++;
                                    }
                                    else
                                    {
                                        teamCT++;
                                    }
                                }
                            }
                            index++;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_BATTLE_GIVEUPBATTLE_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

