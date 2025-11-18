namespace Server.Match.Network.Packets
{
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Server.Match.Data.Enums;
    using Server.Match.Data.Models;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_EVENTS_ACTION
    {
        public static byte[] GET_CODE(List<ObjectHitInfo> Objs)
        {
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                foreach (ObjectHitInfo info in Objs)
                {
                    if (info.Type == 1)
                    {
                        if (info.ObjSyncId == 0)
                        {
                            packet.WriteH((short) 10);
                            packet.WriteH((ushort) info.ObjId);
                            packet.WriteC(3);
                            packet.WriteH((short) 6);
                            packet.WriteH((ushort) info.ObjLife);
                            packet.WriteC((byte) info.KillerSlot);
                            continue;
                        }
                        packet.WriteH((short) 13);
                        packet.WriteH((ushort) info.ObjId);
                        packet.WriteC(6);
                        packet.WriteH((ushort) info.ObjLife);
                        packet.WriteC((byte) info.AnimId1);
                        packet.WriteC((byte) info.AnimId2);
                        packet.WriteT(info.SpecialUse);
                        continue;
                    }
                    if (info.Type == 2)
                    {
                        ushort num = 11;
                        UdpGameEvent hpSync = UdpGameEvent.HpSync;
                        if (info.ObjLife == 0)
                        {
                            num = (ushort) (num + 13);
                            hpSync |= UdpGameEvent.GetWeaponForHost;
                        }
                        packet.WriteH(num);
                        packet.WriteH((ushort) info.ObjId);
                        packet.WriteC(0);
                        packet.WriteD((uint) hpSync);
                        packet.WriteH((ushort) info.ObjLife);
                        if (hpSync.HasFlag(UdpGameEvent.GetWeaponForHost))
                        {
                            packet.WriteC((byte) info.DeathType);
                            packet.WriteC((byte) info.KillerSlot);
                            packet.WriteHV(info.Position);
                            packet.WriteD(0);
                            packet.WriteC((byte) info.HitPart);
                        }
                        continue;
                    }
                    if (info.Type == 3)
                    {
                        if (info.ObjSyncId == 0)
                        {
                            packet.WriteH((short) 8);
                            packet.WriteH((ushort) info.ObjId);
                            packet.WriteC(9);
                            packet.WriteH((short) 1);
                            packet.WriteC((byte) (info.ObjLife == 0));
                            continue;
                        }
                        packet.WriteH((short) 14);
                        packet.WriteH((ushort) info.ObjId);
                        packet.WriteC(12);
                        packet.WriteC((byte) info.DestroyState);
                        packet.WriteH((ushort) info.ObjLife);
                        packet.WriteT(info.SpecialUse);
                        packet.WriteC((byte) info.AnimId1);
                        packet.WriteC((byte) info.AnimId2);
                        continue;
                    }
                    if (info.Type == 4)
                    {
                        packet.WriteH((short) 11);
                        packet.WriteH((ushort) info.ObjId);
                        packet.WriteC(8);
                        packet.WriteD((uint) 8);
                        packet.WriteH((ushort) info.ObjLife);
                        continue;
                    }
                    if (info.Type == 5)
                    {
                        packet.WriteH((short) 12);
                        packet.WriteH((ushort) info.ObjId);
                        packet.WriteC(0);
                        packet.WriteD((uint) 0x40000000);
                        packet.WriteC((byte) (info.DeathType * ((CharaDeath) 0x10)));
                        packet.WriteC((byte) info.HitPart);
                        packet.WriteC((byte) info.KillerSlot);
                        continue;
                    }
                    if (info.Type == 6)
                    {
                        packet.WriteH((short) 15);
                        packet.WriteH((ushort) info.ObjId);
                        packet.WriteC(0);
                        packet.WriteD((uint) 0x4000000);
                        packet.WriteD(info.WeaponId);
                        packet.WriteC(info.Accessory);
                        packet.WriteC(info.Extensions);
                    }
                }
                return packet.ToArray();
            }
        }
    }
}

