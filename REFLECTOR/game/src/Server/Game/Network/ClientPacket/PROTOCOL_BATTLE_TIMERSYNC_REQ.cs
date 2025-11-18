namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Utility;
    using Server.Game.Data.Models;
    using Server.Game.Data.Utils;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class PROTOCOL_BATTLE_TIMERSYNC_REQ : GameClientPacket
    {
        private float float_0;
        private uint uint_0;
        private int int_0;
        private int int_1;
        private int int_2;
        private int int_3;

        private void method_0(Account account_0, RoomModel roomModel_0, SlotModel slotModel_0, bool bool_0)
        {
            if (!bool_0)
            {
                slotModel_0.Latency = this.int_2;
                slotModel_0.Ping = this.int_0;
                slotModel_0.FailLatencyTimes = (slotModel_0.Latency < ConfigLoader.MaxLatency) ? 0 : (slotModel_0.FailLatencyTimes + 1);
                if (ConfigLoader.IsDebugPing && (ComDiv.GetDuration(account_0.LastPingDebug) >= ConfigLoader.PingUpdateTimeSeconds))
                {
                    account_0.LastPingDebug = DateTimeUtil.Now();
                    account_0.SendPacket(new PROTOCOL_LOBBY_CHATTING_ACK("Server", 0, 5, false, $"{this.int_2}ms ({this.int_0} bar)"));
                }
                if (slotModel_0.FailLatencyTimes < ConfigLoader.MaxRepeatLatency)
                {
                    AllUtils.RoomPingSync(roomModel_0);
                }
                else
                {
                    CLogger.Print($"Player: '{account_0.Nickname}' (Id: {slotModel_0.PlayerId}) kicked due to high latency. ({slotModel_0.Latency}/{ConfigLoader.MaxLatency}ms)", LoggerType.Warning, null);
                    base.Client.Close(500, true, false);
                }
            }
        }

        private void method_1(RoomModel roomModel_0, bool bool_0)
        {
            Class4 class2 = new Class4 {
                roomModel_0 = roomModel_0
            };
            try
            {
                if (!class2.roomModel_0.IsDinoMode(""))
                {
                    if (!class2.roomModel_0.ThisModeHaveRounds())
                    {
                        if (class2.roomModel_0.RoomType != RoomCondition.Ace)
                        {
                            List<Account> allPlayers = class2.roomModel_0.GetAllPlayers(SlotState.READY, 1);
                            if (allPlayers.Count > 0)
                            {
                                TeamEnum winnerTeam = AllUtils.GetWinnerTeam(class2.roomModel_0);
                                class2.roomModel_0.CalculateResult(winnerTeam, bool_0);
                                using (PROTOCOL_BATTLE_MISSION_ROUND_END_ACK protocol_battle_mission_round_end_ack2 = new PROTOCOL_BATTLE_MISSION_ROUND_END_ACK(class2.roomModel_0, winnerTeam, RoundEndType.TimeOut))
                                {
                                    int num2;
                                    int num3;
                                    byte[] buffer;
                                    AllUtils.GetBattleResult(class2.roomModel_0, out num2, out num3, out buffer);
                                    byte[] completeBytes = protocol_battle_mission_round_end_ack2.GetCompleteBytes("PROTOCOL_BATTLE_TIMERSYNC_REQ");
                                    foreach (Account account in allPlayers)
                                    {
                                        if (class2.roomModel_0.Slots[account.SlotId].State == SlotState.BATTLE)
                                        {
                                            account.SendCompletePacket(completeBytes, protocol_battle_mission_round_end_ack2.GetType().Name);
                                        }
                                        account.SendPacket(new PROTOCOL_BATTLE_ENDBATTLE_ACK(account, winnerTeam, num3, num2, bool_0, buffer));
                                        AllUtils.UpdateSeasonPass(account);
                                    }
                                }
                            }
                        }
                        else
                        {
                            SlotModel[] modelArray2 = new SlotModel[] { class2.roomModel_0.GetSlot(0), class2.roomModel_0.GetSlot(1) };
                            if ((modelArray2[0] == null) || ((modelArray2[0].State != SlotState.BATTLE) || ((modelArray2[1] == null) || (modelArray2[1].State != SlotState.BATTLE))))
                            {
                                AllUtils.EndBattleNoPoints(class2.roomModel_0);
                            }
                            return;
                        }
                    }
                    else
                    {
                        TeamEnum winner = TeamEnum.TEAM_DRAW;
                        if (class2.roomModel_0.RoomType != RoomCondition.Destroy)
                        {
                            if (class2.roomModel_0.SwapRound)
                            {
                                winner = TeamEnum.FR_TEAM;
                                class2.roomModel_0.FRRounds++;
                            }
                            else
                            {
                                winner = TeamEnum.CT_TEAM;
                                class2.roomModel_0.CTRounds++;
                            }
                        }
                        else if (class2.roomModel_0.Bar1 > class2.roomModel_0.Bar2)
                        {
                            if (class2.roomModel_0.SwapRound)
                            {
                                winner = TeamEnum.CT_TEAM;
                                class2.roomModel_0.CTRounds++;
                            }
                            else
                            {
                                winner = TeamEnum.FR_TEAM;
                                class2.roomModel_0.FRRounds++;
                            }
                        }
                        else if (class2.roomModel_0.Bar1 >= class2.roomModel_0.Bar2)
                        {
                            winner = TeamEnum.TEAM_DRAW;
                        }
                        else if (class2.roomModel_0.SwapRound)
                        {
                            winner = TeamEnum.FR_TEAM;
                            class2.roomModel_0.FRRounds++;
                        }
                        else
                        {
                            winner = TeamEnum.CT_TEAM;
                            class2.roomModel_0.CTRounds++;
                        }
                        AllUtils.BattleEndRound(class2.roomModel_0, winner, RoundEndType.TimeOut);
                        return;
                    }
                }
                else
                {
                    if (class2.roomModel_0.Rounds != 1)
                    {
                        if (class2.roomModel_0.Rounds == 2)
                        {
                            AllUtils.EndBattle(class2.roomModel_0, bool_0);
                        }
                        return;
                    }
                    else
                    {
                        class2.roomModel_0.Rounds = 2;
                        SlotModel[] slots = class2.roomModel_0.Slots;
                        int index = 0;
                        while (true)
                        {
                            if (index >= slots.Length)
                            {
                                List<int> list = AllUtils.GetDinossaurs(class2.roomModel_0, true, -2);
                                using (PROTOCOL_BATTLE_MISSION_ROUND_END_ACK protocol_battle_mission_round_end_ack = new PROTOCOL_BATTLE_MISSION_ROUND_END_ACK(class2.roomModel_0, 2, RoundEndType.TimeOut))
                                {
                                    using (PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK protocol_battle_mission_round_pre_start_ack = new PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK(class2.roomModel_0, list))
                                    {
                                        class2.roomModel_0.SendPacketToPlayers(protocol_battle_mission_round_end_ack, protocol_battle_mission_round_pre_start_ack, SlotState.BATTLE, 0);
                                    }
                                }
                                break;
                            }
                            SlotModel model = slots[index];
                            if (model.State == SlotState.BATTLE)
                            {
                                model.KillsOnLife = 0;
                                model.LastKillState = 0;
                                model.RepeatLastState = false;
                            }
                            index++;
                        }
                    }
                    goto TR_0008;
                }
                goto TR_002E;
            TR_0008:
                class2.roomModel_0.RoundTime.StartJob(0x1482, new TimerCallback(class2.method_0));
                return;
            TR_002E:
                AllUtils.ResetBattleInfo(class2.roomModel_0);
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }

        public override void Read()
        {
            this.uint_0 = base.ReadUD();
            this.float_0 = base.ReadT();
            this.int_3 = base.ReadC();
            this.int_0 = base.ReadC();
            this.int_1 = base.ReadC();
            this.int_2 = base.ReadH();
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
                    if ((room != null) && room.GetSlot(player.SlotId, out model2))
                    {
                        bool flag = room.IsBotMode();
                        if ((model2 != null) && (model2.State == SlotState.BATTLE))
                        {
                            if ((this.float_0 != 0f) || (this.int_1 != 0))
                            {
                                AllUtils.ValidateBanPlayer(player, $"Using an illegal program! ({this.float_0}/{this.int_1})");
                            }
                            this.method_0(player, room, model2, flag);
                            room.TimeRoom = this.uint_0;
                            if ((((this.uint_0 == 0) || (this.uint_0 > 0x80000000)) && (room.Rounds == this.int_3)) && (room.State == RoomState.BATTLE))
                            {
                                this.method_1(room, flag);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_BATTLE_TIMERSYNC_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }

        [CompilerGenerated]
        private sealed class Class4
        {
            public RoomModel roomModel_0;

            internal void method_0(object object_0)
            {
                if (this.roomModel_0.State == RoomState.BATTLE)
                {
                    this.roomModel_0.BattleStart = DateTimeUtil.Now().AddSeconds(5.0);
                    using (PROTOCOL_BATTLE_MISSION_ROUND_START_ACK protocol_battle_mission_round_start_ack = new PROTOCOL_BATTLE_MISSION_ROUND_START_ACK(this.roomModel_0))
                    {
                        this.roomModel_0.SendPacketToPlayers(protocol_battle_mission_round_start_ack, SlotState.BATTLE, 0);
                    }
                }
                object obj2 = object_0;
                lock (obj2)
                {
                    this.roomModel_0.RoundTime.StopJob();
                }
            }
        }
    }
}

