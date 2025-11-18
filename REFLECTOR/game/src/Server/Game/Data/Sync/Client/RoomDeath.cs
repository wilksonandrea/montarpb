namespace Server.Game.Data.Sync.Client
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Network;
    using Plugin.Core.Utility;
    using Server.Game.Data.Models;
    using Server.Game.Data.Sync.Update;
    using Server.Game.Data.Utils;
    using Server.Game.Data.XML;
    using System;
    using System.Runtime.InteropServices;

    public class RoomDeath
    {
        public static void EndBattleByDeath(RoomModel Room, SlotModel Killer, bool IsBotMode, bool IsSuicide, FragInfos Kills)
        {
            if ((Room.RoomType == RoomCondition.DeathMatch) && !IsBotMode)
            {
                AllUtils.BattleEndKills(Room, IsBotMode);
            }
            else if (Room.RoomType == RoomCondition.FreeForAll)
            {
                AllUtils.BattleEndKillsFreeForAll(Room);
            }
            else if (!Killer.SpecGM && ((Room.RoomType == RoomCondition.Bomb) || ((Room.RoomType == RoomCondition.Annihilation) || ((Room.RoomType == RoomCondition.Destroy) || (Room.RoomType == RoomCondition.Ace)))))
            {
                if ((Room.RoomType != RoomCondition.Bomb) && ((Room.RoomType != RoomCondition.Annihilation) && (Room.RoomType != RoomCondition.Destroy)))
                {
                    if (Room.RoomType == RoomCondition.Ace)
                    {
                        SlotModel[] modelArray = new SlotModel[] { Room.GetSlot(0), Room.GetSlot(1) };
                        if (modelArray[0].DeathState == DeadEnum.Dead)
                        {
                            Room.CTRounds++;
                            AllUtils.BattleEndRound(Room, TeamEnum.CT_TEAM, true, Kills, Killer);
                        }
                        else if (modelArray[1].DeathState == DeadEnum.Dead)
                        {
                            Room.FRRounds++;
                            AllUtils.BattleEndRound(Room, TeamEnum.FR_TEAM, true, Kills, Killer);
                        }
                    }
                }
                else
                {
                    int num;
                    int num2;
                    int num3;
                    int num4;
                    TeamEnum enum3;
                    TeamEnum enum2 = TeamEnum.TEAM_DRAW;
                    Room.GetPlayingPlayers(true, out num, out num2, out num3, out num4);
                    smethod_0(Room, Killer, ref num, ref num2, ref num3, ref num4, out enum3);
                    if ((((num3 == num) && (enum3 == TeamEnum.FR_TEAM)) & IsSuicide) && !Room.ActiveC4)
                    {
                        smethod_1(Room, ref enum2, 1);
                        AllUtils.BattleEndRound(Room, enum2, true, Kills, Killer);
                    }
                    else if ((num4 == num2) && (enum3 == TeamEnum.CT_TEAM))
                    {
                        smethod_1(Room, ref enum2, 2);
                        AllUtils.BattleEndRound(Room, enum2, true, Kills, Killer);
                    }
                    else if ((num3 == num) && (enum3 == TeamEnum.CT_TEAM))
                    {
                        if (!Room.ActiveC4)
                        {
                            smethod_1(Room, ref enum2, 1);
                        }
                        else if (IsSuicide)
                        {
                            smethod_1(Room, ref enum2, 2);
                        }
                        AllUtils.BattleEndRound(Room, enum2, false, Kills, Killer);
                    }
                    else if ((num4 == num2) && (enum3 == TeamEnum.FR_TEAM))
                    {
                        if (IsSuicide && Room.ActiveC4)
                        {
                            smethod_1(Room, ref enum2, 1);
                        }
                        else
                        {
                            smethod_1(Room, ref enum2, 2);
                        }
                        AllUtils.BattleEndRound(Room, enum2, true, Kills, Killer);
                    }
                }
            }
        }

        public static void Load(SyncClientPacket C)
        {
            int id = C.ReadH();
            int num2 = C.ReadH();
            byte num3 = C.ReadC();
            byte slotIdx = C.ReadC();
            int num5 = C.ReadD();
            float num6 = C.ReadT();
            float num7 = C.ReadT();
            float num8 = C.ReadT();
            byte num9 = C.ReadC();
            byte num10 = C.ReadC();
            int num11 = num3 * 0x19;
            if (C.ToArray().Length > (0x1c + num11))
            {
                CLogger.Print($"Invalid Death (Length > 53): {C.ToArray().Length}", LoggerType.Warning, null);
            }
            ChannelModel channel = ChannelsXML.GetChannel(C.ReadH(), num2);
            if (channel != null)
            {
                RoomModel room = channel.GetRoom(id);
                if ((room != null) && (!room.RoundTime.IsTimer() && (room.State == RoomState.BATTLE)))
                {
                    SlotModel slot = room.GetSlot(slotIdx);
                    if ((slot != null) && (slot.State == SlotState.BATTLE))
                    {
                        FragInfos infos1 = new FragInfos();
                        infos1.KillsCount = num3;
                        infos1.KillerSlot = slotIdx;
                        infos1.WeaponId = num5;
                        infos1.X = num6;
                        infos1.Y = num7;
                        infos1.Z = num8;
                        infos1.Flag = num9;
                        infos1.Unk = num10;
                        FragInfos kill = infos1;
                        bool flag = false;
                        int num12 = 0;
                        while (true)
                        {
                            if (num12 >= num3)
                            {
                                kill.KillsCount = (byte) kill.Frags.Count;
                                KillFragInfo.GenDeath(room, slot, kill, flag);
                                break;
                            }
                            byte num13 = C.ReadC();
                            byte num14 = C.ReadC();
                            byte num15 = C.ReadC();
                            float num16 = C.ReadT();
                            float num17 = C.ReadT();
                            float num18 = C.ReadT();
                            byte num19 = C.ReadC();
                            byte num20 = C.ReadC();
                            byte[] buffer = C.ReadB(8);
                            SlotModel model4 = room.GetSlot(num13);
                            if ((model4 != null) && (model4.State == SlotState.BATTLE))
                            {
                                FragModel model1 = new FragModel();
                                model1.VictimSlot = num13;
                                model1.WeaponClass = num14;
                                model1.HitspotInfo = num15;
                                model1.X = num16;
                                model1.Y = num17;
                                model1.Z = num18;
                                model1.AssistSlot = num19;
                                model1.Unk = num20;
                                model1.Unks = buffer;
                                FragModel item = model1;
                                if (kill.KillerSlot == num13)
                                {
                                    flag = true;
                                }
                                kill.Frags.Add(item);
                            }
                            num12++;
                        }
                    }
                }
            }
        }

        public static unsafe void RegistryFragInfos(RoomModel Room, SlotModel Killer, out int Score, bool IsBotMode, bool IsSuicide, FragInfos Kills)
        {
            Score = 0;
            ItemClass class2 = (ItemClass) ComDiv.GetIdStatics(Kills.WeaponId, 1);
            ClassType type = (ClassType) ComDiv.GetIdStatics(Kills.WeaponId, 2);
            foreach (FragModel model in Kills.Frags)
            {
                CharaDeath death = (CharaDeath) (model.HitspotInfo >> 4);
                if ((Kills.KillsCount - IsSuicide) > 1)
                {
                    model.KillFlag |= ((death == CharaDeath.BOOM) || ((death == CharaDeath.OBJECT_EXPLOSION) || ((death == CharaDeath.POISON) || ((death == CharaDeath.HOWL) || ((death == CharaDeath.TRAMPLED) || (type == ClassType.Shotgun)))))) ? KillingMessage.MassKill : KillingMessage.PiercingShot;
                }
                else
                {
                    int num3 = 0;
                    if (death == CharaDeath.HEADSHOT)
                    {
                        num3 = 4;
                    }
                    else if ((death == CharaDeath.DEFAULT) && (class2 == ItemClass.Melee))
                    {
                        num3 = 6;
                    }
                    if (num3 <= 0)
                    {
                        Killer.LastKillState = 0;
                        Killer.RepeatLastState = false;
                    }
                    else
                    {
                        int num4 = Killer.LastKillState >> 12;
                        if (num3 == 4)
                        {
                            if (num4 != 4)
                            {
                                Killer.RepeatLastState = false;
                            }
                            Killer.LastKillState = (num3 << 12) | (Killer.KillsOnLife + 1);
                            if (Killer.RepeatLastState)
                            {
                                model.KillFlag |= ((Killer.LastKillState & 0x3fff) <= 1) ? KillingMessage.Headshot : KillingMessage.ChainHeadshot;
                            }
                            else
                            {
                                model.KillFlag |= KillingMessage.Headshot;
                                Killer.RepeatLastState = true;
                            }
                        }
                        else if (num3 == 6)
                        {
                            if (num4 != 6)
                            {
                                Killer.RepeatLastState = false;
                            }
                            Killer.LastKillState = (num3 << 12) | (Killer.KillsOnLife + 1);
                            if (Killer.RepeatLastState && ((Killer.LastKillState & 0x3fff) > 1))
                            {
                                model.KillFlag |= KillingMessage.ChainSlugger;
                            }
                            else
                            {
                                Killer.RepeatLastState = true;
                            }
                        }
                    }
                }
                byte victimSlot = model.VictimSlot;
                byte assistSlot = model.AssistSlot;
                SlotModel slot = Room.Slots[victimSlot];
                SlotModel model3 = Room.Slots[assistSlot];
                if (slot.KillsOnLife > 3)
                {
                    model.KillFlag |= KillingMessage.ChainStopper;
                }
                if ((((Kills.WeaponId != 0x4a48) && (Kills.WeaponId != 0x4a4e)) || (Kills.KillerSlot != victimSlot)) || !slot.SpecGM)
                {
                    slot.AllDeaths++;
                }
                if (Kills.KillerSlot != assistSlot)
                {
                    model3.AllAssists++;
                }
                if (Room.RoomType == RoomCondition.FreeForAll)
                {
                    Killer.AllKills++;
                    if (Killer.DeathState == DeadEnum.Alive)
                    {
                        Killer.KillsOnLife++;
                    }
                }
                else if (Killer.Team != slot.Team)
                {
                    Score += AllUtils.GetKillScore(model.KillFlag);
                    Killer.AllKills++;
                    if (Killer.DeathState == DeadEnum.Alive)
                    {
                        Killer.KillsOnLife++;
                    }
                    if (slot.Team == TeamEnum.FR_TEAM)
                    {
                        Room.FRDeaths++;
                        Room.CTKills++;
                    }
                    else
                    {
                        Room.CTDeaths++;
                        Room.FRKills++;
                    }
                    if (Room.IsDinoMode("DE"))
                    {
                        if (Killer.Team == TeamEnum.FR_TEAM)
                        {
                            Room.FRDino += 4;
                        }
                        else
                        {
                            Room.CTDino += 4;
                        }
                    }
                }
                slot.LastKillState = 0;
                slot.KillsOnLife = 0;
                slot.RepeatLastState = false;
                slot.PassSequence = 0;
                slot.DeathState = DeadEnum.Dead;
                if (!IsBotMode)
                {
                    switch (type)
                    {
                        case ClassType.Assault:
                        {
                            int* aR = Killer.AR;
                            aR[0]++;
                            int* numPtr4 = &(slot.AR[1]);
                            numPtr4[0]++;
                            break;
                        }
                        case ClassType.SMG:
                        {
                            int* sMG = Killer.SMG;
                            sMG[0]++;
                            int* numPtr6 = &(slot.SMG[1]);
                            numPtr6[0]++;
                            break;
                        }
                        case ClassType.Sniper:
                        {
                            int* sR = Killer.SR;
                            sR[0]++;
                            int* numPtr8 = &(slot.SR[1]);
                            numPtr8[0]++;
                            break;
                        }
                        case ClassType.Shotgun:
                        {
                            int* sG = Killer.SG;
                            sG[0]++;
                            int* numPtr10 = &(slot.SG[1]);
                            numPtr10[0]++;
                            break;
                        }
                        case ClassType.ThrowingGrenade:
                        case ClassType.ThrowingSpecial:
                        case ClassType.Mission:
                            break;

                        case ClassType.Machinegun:
                        {
                            int* mG = Killer.MG;
                            mG[0]++;
                            int* numPtr12 = &(slot.MG[1]);
                            numPtr12[0]++;
                            break;
                        }
                        default:
                            if (type == ClassType.Shield)
                            {
                                int* sHD = Killer.SHD;
                                sHD[0]++;
                                int* numPtr2 = &(slot.SHD[1]);
                                numPtr2[0]++;
                            }
                            break;
                    }
                    AllUtils.CompleteMission(Room, slot, MissionType.DEATH, 0);
                }
                if (death == CharaDeath.HEADSHOT)
                {
                    Killer.AllHeadshots++;
                }
            }
        }

        private static void smethod_0(RoomModel roomModel_0, SlotModel slotModel_0, ref int int_0, ref int int_1, ref int int_2, ref int int_3, out TeamEnum teamEnum_0)
        {
            teamEnum_0 = slotModel_0.Team;
            if (roomModel_0.SwapRound)
            {
                teamEnum_0 = (teamEnum_0 == TeamEnum.FR_TEAM) ? TeamEnum.CT_TEAM : TeamEnum.FR_TEAM;
                int num = int_0;
                int num2 = int_1;
                int_1 = num;
                int_0 = num2;
                num2 = int_2;
                num = int_3;
                int_3 = num2;
                int_2 = num;
            }
        }

        private static void smethod_1(RoomModel roomModel_0, ref TeamEnum teamEnum_0, int int_0)
        {
            if (int_0 == 1)
            {
                if (roomModel_0.SwapRound)
                {
                    teamEnum_0 = TeamEnum.FR_TEAM;
                    roomModel_0.FRRounds++;
                }
                else
                {
                    teamEnum_0 = TeamEnum.CT_TEAM;
                    roomModel_0.CTRounds++;
                }
            }
            else if (int_0 == 2)
            {
                if (roomModel_0.SwapRound)
                {
                    teamEnum_0 = TeamEnum.CT_TEAM;
                    roomModel_0.CTRounds++;
                }
                else
                {
                    teamEnum_0 = TeamEnum.FR_TEAM;
                    roomModel_0.FRRounds++;
                }
            }
        }
    }
}

