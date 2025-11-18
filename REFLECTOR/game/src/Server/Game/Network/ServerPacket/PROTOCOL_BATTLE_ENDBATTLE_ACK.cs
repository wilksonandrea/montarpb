namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Network;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Data.Utils;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_BATTLE_ENDBATTLE_ACK : GameServerPacket
    {
        private readonly RoomModel roomModel_0;
        private readonly Account account_0;
        private readonly ClanModel clanModel_0;
        private readonly int int_0;
        private readonly int int_1;
        private readonly int int_2;
        private readonly bool bool_0;
        private readonly byte[] byte_0;

        public PROTOCOL_BATTLE_ENDBATTLE_ACK(Account account_1)
        {
            this.int_0 = 2;
            this.account_0 = account_1;
            if (account_1 != null)
            {
                this.roomModel_0 = account_1.Room;
                this.int_0 = (this.roomModel_0.RoomType == RoomCondition.Tutorial) ? 0 : ((int) AllUtils.GetWinnerTeam(this.roomModel_0));
                this.clanModel_0 = ClanManager.GetClan(account_1.ClanId);
                this.bool_0 = this.roomModel_0.IsBotMode();
                AllUtils.GetBattleResult(this.roomModel_0, out this.int_2, out this.int_1, out this.byte_0);
            }
        }

        public PROTOCOL_BATTLE_ENDBATTLE_ACK(Account account_1, TeamEnum teamEnum_0, int int_3, int int_4, bool bool_1, byte[] byte_1)
        {
            this.int_0 = 2;
            this.account_0 = account_1;
            this.int_0 = (int) teamEnum_0;
            this.int_1 = int_3;
            this.int_2 = int_4;
            this.bool_0 = bool_1;
            this.byte_0 = byte_1;
            if (account_1 != null)
            {
                this.roomModel_0 = account_1.Room;
                this.clanModel_0 = ClanManager.GetClan(account_1.ClanId);
            }
        }

        public PROTOCOL_BATTLE_ENDBATTLE_ACK(Account account_1, int int_3, int int_4, int int_5, bool bool_1, byte[] byte_1)
        {
            this.int_0 = 2;
            this.account_0 = account_1;
            this.int_0 = int_3;
            this.int_1 = int_4;
            this.int_2 = int_5;
            this.bool_0 = bool_1;
            this.byte_0 = byte_1;
            if (account_1 != null)
            {
                this.roomModel_0 = account_1.Room;
                this.clanModel_0 = ClanManager.GetClan(account_1.ClanId);
            }
        }

        private byte[] method_0(RoomModel roomModel_1, bool bool_1)
        {
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                SlotModel[] slots;
                int num;
                if (bool_1)
                {
                    slots = roomModel_1.Slots;
                    num = 0;
                    while (num < slots.Length)
                    {
                        SlotModel model = slots[num];
                        packet.WriteH((ushort) model.Score);
                        num++;
                    }
                }
                else if (roomModel_1.ThisModeHaveRounds() || roomModel_1.IsDinoMode(""))
                {
                    packet.WriteH(roomModel_1.IsDinoMode("DE") ? ((ushort) roomModel_1.FRDino) : (roomModel_1.IsDinoMode("CC") ? ((ushort) roomModel_1.FRKills) : ((ushort) roomModel_1.FRRounds)));
                    packet.WriteH(roomModel_1.IsDinoMode("DE") ? ((ushort) roomModel_1.CTDino) : (roomModel_1.IsDinoMode("CC") ? ((ushort) roomModel_1.CTKills) : ((ushort) roomModel_1.CTRounds)));
                    slots = roomModel_1.Slots;
                    num = 0;
                    while (true)
                    {
                        if (num >= slots.Length)
                        {
                            packet.WriteH((short) 0);
                            packet.WriteH((short) 0);
                            break;
                        }
                        SlotModel model2 = slots[num];
                        packet.WriteC((byte) model2.Objects);
                        num++;
                    }
                }
                return packet.ToArray();
            }
        }

        private byte[] method_1(RoomModel roomModel_1)
        {
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                packet.WriteH(roomModel_1.ThisModeHaveRounds() ? ((ushort) roomModel_1.FRRounds) : ((ushort) 0));
                packet.WriteH(roomModel_1.ThisModeHaveRounds() ? ((ushort) roomModel_1.CTRounds) : ((ushort) 0));
                return packet.ToArray();
            }
        }

        private byte[] method_2(RoomModel roomModel_1)
        {
            byte[] buffer;
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                SlotModel[] slots = roomModel_1.Slots;
                int index = 0;
                while (true)
                {
                    Account account;
                    if (index >= slots.Length)
                    {
                        buffer = packet.ToArray();
                        break;
                    }
                    SlotModel slot = slots[index];
                    if (roomModel_1.GetPlayerBySlot(slot, out account))
                    {
                        packet.WriteC((byte) account.Rank);
                    }
                    else
                    {
                        packet.WriteC((byte) AllUtils.InitBotRank(roomModel_1.IsStartingMatch() ? roomModel_1.IngameAiLevel : roomModel_1.AiLevel));
                    }
                    packet.WriteH((short) 0);
                    packet.WriteD(1);
                    index++;
                }
            }
            return buffer;
        }

        private byte[] method_3(Account account_1)
        {
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                PlayerEvent event2 = account_1.Event;
                if (event2 == null)
                {
                    packet.WriteB(new byte[5]);
                }
                else
                {
                    packet.WriteC((byte) event2.LastPlaytimeFinish);
                    packet.WriteD((uint) event2.LastPlaytimeValue);
                }
                return packet.ToArray();
            }
        }

        private byte[] method_4(Account account_1)
        {
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                SlotModel slot = this.roomModel_0.GetSlot(account_1.SlotId);
                if (slot != null)
                {
                    packet.WriteB(new byte[0x2c]);
                    packet.WriteD(0);
                    packet.WriteB(new byte[0x10]);
                    packet.WriteH((ushort) slot.SeasonPoint);
                    packet.WriteH((ushort) slot.BonusBattlePass);
                    packet.WriteC(0);
                    packet.WriteB(new byte[20]);
                    packet.WriteD(0);
                    packet.WriteH((ushort) ((600 + account_1.InventoryPlus) + 8));
                }
                return packet.ToArray();
            }
        }

        private byte[] method_5(RoomModel roomModel_1)
        {
            byte[] buffer;
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                buffer = roomModel_1.SlotRewards.Item1;
                int index = 0;
                while (true)
                {
                    if (index >= buffer.Length)
                    {
                        int[] numArray = roomModel_1.SlotRewards.Item2;
                        index = 0;
                        while (true)
                        {
                            if (index >= numArray.Length)
                            {
                                buffer = packet.ToArray();
                                break;
                            }
                            int num3 = numArray[index];
                            packet.WriteD(num3);
                            index++;
                        }
                        break;
                    }
                    byte num2 = buffer[index];
                    packet.WriteC(num2);
                    index++;
                }
            }
            return buffer;
        }

        public override void Write()
        {
            base.WriteH((short) 0x1414);
            base.WriteD(this.int_1);
            base.WriteC((byte) this.int_0);
            base.WriteB(this.byte_0);
            base.WriteD(this.int_2);
            base.WriteB(this.method_0(this.roomModel_0, this.bool_0));
            base.WriteC(0);
            base.WriteC(0);
            base.WriteC(0);
            base.WriteB(new byte[5]);
            base.WriteD(0);
            base.WriteD(0);
            base.WriteD(0);
            base.WriteD(0);
            base.WriteD(0);
            base.WriteH((short) 0);
            base.WriteH((short) 0);
            base.WriteB(new byte[14]);
            base.WriteB(this.method_1(this.roomModel_0));
            base.WriteB(new byte[0x1b]);
            base.WriteB(this.method_5(this.roomModel_0));
            base.WriteB(this.method_2(this.roomModel_0));
            base.WriteC((byte) (this.account_0.Nickname.Length * 2));
            base.WriteU(this.account_0.Nickname, this.account_0.Nickname.Length * 2);
            base.WriteD(this.account_0.GetRank());
            base.WriteD(this.account_0.Rank);
            base.WriteD(this.account_0.Gold);
            base.WriteD(this.account_0.Exp);
            base.WriteD(0);
            base.WriteC(0);
            base.WriteQ((long) 0L);
            base.WriteC((byte) this.account_0.AuthLevel());
            base.WriteC(0);
            base.WriteD(this.account_0.Tags);
            base.WriteD(this.account_0.Cash);
            base.WriteD(this.clanModel_0.Id);
            base.WriteD(this.account_0.ClanAccess);
            base.WriteQ(this.account_0.StatusId());
            base.WriteC((byte) this.account_0.CafePC);
            base.WriteC((byte) this.account_0.Country);
            base.WriteC((byte) (this.clanModel_0.Name.Length * 2));
            base.WriteU(this.clanModel_0.Name, this.clanModel_0.Name.Length * 2);
            base.WriteC((byte) this.clanModel_0.Rank);
            base.WriteC((byte) this.clanModel_0.GetClanUnit());
            base.WriteD(this.clanModel_0.Logo);
            base.WriteC((byte) this.clanModel_0.NameColor);
            base.WriteC((byte) this.clanModel_0.Effect);
            base.WriteD(this.account_0.Statistic.Season.Matches);
            base.WriteD(this.account_0.Statistic.Season.MatchWins);
            base.WriteD(this.account_0.Statistic.Season.MatchLoses);
            base.WriteD(this.account_0.Statistic.Season.MatchDraws);
            base.WriteD(this.account_0.Statistic.Season.KillsCount);
            base.WriteD(this.account_0.Statistic.Season.HeadshotsCount);
            base.WriteD(this.account_0.Statistic.Season.DeathsCount);
            base.WriteD(this.account_0.Statistic.Season.TotalMatchesCount);
            base.WriteD(this.account_0.Statistic.Season.TotalKillsCount);
            base.WriteD(this.account_0.Statistic.Season.EscapesCount);
            base.WriteD(this.account_0.Statistic.Season.AssistsCount);
            base.WriteD(this.account_0.Statistic.Season.MvpCount);
            base.WriteD(this.account_0.Statistic.Basic.Matches);
            base.WriteD(this.account_0.Statistic.Basic.MatchWins);
            base.WriteD(this.account_0.Statistic.Basic.MatchLoses);
            base.WriteD(this.account_0.Statistic.Basic.MatchDraws);
            base.WriteD(this.account_0.Statistic.Basic.KillsCount);
            base.WriteD(this.account_0.Statistic.Basic.HeadshotsCount);
            base.WriteD(this.account_0.Statistic.Basic.DeathsCount);
            base.WriteD(this.account_0.Statistic.Basic.TotalMatchesCount);
            base.WriteD(this.account_0.Statistic.Basic.TotalKillsCount);
            base.WriteD(this.account_0.Statistic.Basic.EscapesCount);
            base.WriteD(this.account_0.Statistic.Basic.AssistsCount);
            base.WriteD(this.account_0.Statistic.Basic.MvpCount);
            base.WriteH((ushort) this.account_0.Statistic.Daily.Matches);
            base.WriteH((ushort) this.account_0.Statistic.Daily.MatchWins);
            base.WriteH((ushort) this.account_0.Statistic.Daily.MatchLoses);
            base.WriteH((ushort) this.account_0.Statistic.Daily.MatchDraws);
            base.WriteH((ushort) this.account_0.Statistic.Daily.KillsCount);
            base.WriteH((ushort) this.account_0.Statistic.Daily.HeadshotsCount);
            base.WriteH((ushort) this.account_0.Statistic.Daily.DeathsCount);
            base.WriteD(this.account_0.Statistic.Daily.ExpGained);
            base.WriteD(this.account_0.Statistic.Daily.PointGained);
            base.WriteD(this.account_0.Statistic.Daily.Playtime);
            base.WriteB(this.method_3(this.account_0));
            base.WriteD(0);
            base.WriteC(0);
            base.WriteD(0);
            base.WriteC(0);
            base.WriteD(0);
            base.WriteH((short) 0);
            base.WriteC(0);
            base.WriteB(this.method_4(this.account_0));
        }
    }
}

