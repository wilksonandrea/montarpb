namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Models;
    using Plugin.Core.Network;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_ROOM_JOIN_ACK : GameServerPacket
    {
        private readonly uint uint_0;
        private readonly RoomModel roomModel_0;
        private readonly int int_0;

        public PROTOCOL_ROOM_JOIN_ACK(uint uint_1, Account account_0)
        {
            this.uint_0 = uint_1;
            if (account_0 != null)
            {
                this.int_0 = account_0.SlotId;
                this.roomModel_0 = account_0.Room;
            }
        }

        private byte[] method_0(RoomModel roomModel_1)
        {
            byte[] buffer;
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                packet.WriteC((byte) roomModel_1.Slots.Length);
                SlotModel[] slots = roomModel_1.Slots;
                int index = 0;
                while (true)
                {
                    if (index >= slots.Length)
                    {
                        buffer = packet.ToArray();
                        break;
                    }
                    SlotModel model = slots[index];
                    packet.WriteC((byte) model.Team);
                    index++;
                }
            }
            return buffer;
        }

        private byte[] method_1(RoomModel roomModel_1)
        {
            byte[] buffer;
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                packet.WriteC((byte) roomModel_1.Slots.Length);
                SlotModel[] slots = roomModel_1.Slots;
                int index = 0;
                while (true)
                {
                    if (index >= slots.Length)
                    {
                        buffer = packet.ToArray();
                        break;
                    }
                    SlotModel slot = slots[index];
                    packet.WriteC((byte) slot.State);
                    Account playerBySlot = roomModel_1.GetPlayerBySlot(slot);
                    if (playerBySlot == null)
                    {
                        packet.WriteB(new byte[10]);
                        packet.WriteD(uint.MaxValue);
                        packet.WriteB(new byte[0x36]);
                        packet.WriteC((byte) slot.Id);
                        packet.WriteB(new byte[0x44]);
                        packet.WriteC(0);
                        packet.WriteC(0xff);
                        packet.WriteC(0xff);
                    }
                    else
                    {
                        ClanModel clan = ClanManager.GetClan(playerBySlot.ClanId);
                        packet.WriteC((byte) playerBySlot.GetRank());
                        packet.WriteD(clan.Id);
                        packet.WriteD(playerBySlot.ClanAccess);
                        packet.WriteC((byte) clan.Rank);
                        packet.WriteD(clan.Logo);
                        packet.WriteC((byte) playerBySlot.CafePC);
                        packet.WriteC((byte) playerBySlot.Country);
                        packet.WriteQ((long) playerBySlot.Effects);
                        packet.WriteC((byte) clan.Effect);
                        packet.WriteC(0);
                        packet.WriteC((byte) base.NATIONS);
                        packet.WriteC(0);
                        packet.WriteD(playerBySlot.Equipment.NameCardId);
                        packet.WriteC((byte) playerBySlot.Bonus.NickBorderColor);
                        packet.WriteC((byte) playerBySlot.AuthLevel());
                        packet.WriteU(clan.Name, 0x22);
                        packet.WriteC((byte) playerBySlot.SlotId);
                        packet.WriteU(playerBySlot.Nickname, 0x42);
                        packet.WriteC((byte) playerBySlot.NickColor);
                        packet.WriteC((byte) playerBySlot.Bonus.MuzzleColor);
                        packet.WriteC(0);
                        packet.WriteC(0xff);
                        packet.WriteC(0xff);
                    }
                    index++;
                }
            }
            return buffer;
        }

        public override void Write()
        {
            base.WriteH((short) 0xe02);
            base.WriteH((short) 0);
            base.WriteD(this.uint_0);
            if (this.uint_0 == 0)
            {
                SlotModel[] slots = this.roomModel_0.Slots;
                lock (slots)
                {
                    base.WriteB(this.method_0(this.roomModel_0));
                    base.WriteB(this.method_1(this.roomModel_0));
                    base.WriteC(this.roomModel_0.AiType);
                    this.WriteC(this.roomModel_0.IsStartingMatch() ? this.roomModel_0.IngameAiLevel : this.roomModel_0.AiLevel);
                    base.WriteC(this.roomModel_0.AiCount);
                    base.WriteC((byte) this.roomModel_0.GetAllPlayers().Count);
                    base.WriteC((byte) this.roomModel_0.LeaderSlot);
                    base.WriteC((byte) this.roomModel_0.CountdownTime.GetTimeLeft());
                    base.WriteC((byte) this.roomModel_0.Password.Length);
                    base.WriteS(this.roomModel_0.Password, this.roomModel_0.Password.Length);
                    base.WriteB(new byte[0x11]);
                    base.WriteU(this.roomModel_0.LeaderName, 0x42);
                    base.WriteD(this.roomModel_0.KillTime);
                    base.WriteC(this.roomModel_0.Limit);
                    base.WriteC(this.roomModel_0.WatchRuleFlag);
                    base.WriteH((ushort) this.roomModel_0.BalanceType);
                    base.WriteB(this.roomModel_0.RandomMaps);
                    base.WriteC(this.roomModel_0.CountdownIG);
                    base.WriteB(this.roomModel_0.LeaderAddr);
                    base.WriteC(this.roomModel_0.KillCam);
                    base.WriteH((short) 0);
                    base.WriteD(this.roomModel_0.RoomId);
                    base.WriteU(this.roomModel_0.Name, 0x2e);
                    base.WriteC((byte) this.roomModel_0.MapId);
                    base.WriteC((byte) this.roomModel_0.Rule);
                    base.WriteC((byte) this.roomModel_0.Stage);
                    base.WriteC((byte) this.roomModel_0.RoomType);
                    base.WriteC((byte) this.roomModel_0.State);
                    base.WriteC((byte) this.roomModel_0.GetCountPlayers());
                    base.WriteC((byte) this.roomModel_0.GetSlotCount());
                    base.WriteC((byte) this.roomModel_0.Ping);
                    base.WriteH((ushort) this.roomModel_0.WeaponsFlag);
                    base.WriteD(this.roomModel_0.GetFlag());
                    base.WriteH((short) 0);
                    base.WriteB(new byte[4]);
                    base.WriteC(0);
                    base.WriteC((byte) this.int_0);
                }
            }
        }
    }
}

