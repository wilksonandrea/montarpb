namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Network;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_ROOM_GET_SLOTINFO_ACK : GameServerPacket
    {
        private readonly RoomModel roomModel_0;

        public PROTOCOL_ROOM_GET_SLOTINFO_ACK(RoomModel roomModel_1)
        {
            this.roomModel_0 = roomModel_1;
            if ((roomModel_1 != null) && (roomModel_1.GetLeader() == null))
            {
                roomModel_1.SetNewLeader(-1, SlotState.EMPTY, roomModel_1.LeaderSlot, false);
            }
        }

        private byte[] method_0(RoomModel roomModel_1)
        {
            byte[] buffer;
            using (SyncServerPacket packet = new SyncServerPacket())
            {
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
                    packet.WriteH((short) 0x23);
                    packet.WriteC((byte) slot.State);
                    Account playerBySlot = roomModel_1.GetPlayerBySlot(slot);
                    if (playerBySlot == null)
                    {
                        packet.WriteB(new byte[10]);
                        packet.WriteD(uint.MaxValue);
                        packet.WriteB(new byte[0x15]);
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
                        packet.WriteC((byte) slot.ViewType);
                        packet.WriteC((byte) base.NATIONS);
                        packet.WriteC(0);
                        packet.WriteD(playerBySlot.Equipment.NameCardId);
                        packet.WriteC((byte) playerBySlot.Bonus.NickBorderColor);
                        packet.WriteC((byte) playerBySlot.AuthLevel());
                        packet.WriteC((byte) (clan.Name.Length * 2));
                        packet.WriteU(clan.Name, clan.Name.Length * 2);
                    }
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
                    packet.WriteC((byte) roomModel_1.ValidateTeam(model.Team, model.CostumeTeam));
                    index++;
                }
            }
            return buffer;
        }

        public override void Write()
        {
            base.WriteH((short) 0xe0b);
            base.WriteC((byte) this.roomModel_0.LeaderSlot);
            base.WriteB(this.method_0(this.roomModel_0));
            base.WriteB(this.method_1(this.roomModel_0));
        }
    }
}

