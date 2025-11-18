namespace Server.Match.Network.Packets
{
    using Plugin.Core.Network;
    using System;

    public class PROTOCOL_CONNECT
    {
        public static byte[] GET_CODE()
        {
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                packet.WriteC(0x42);
                packet.WriteC(0);
                packet.WriteT(0f);
                packet.WriteC(0);
                packet.WriteH((short) 14);
                packet.WriteD(0);
                packet.WriteC(8);
                return packet.ToArray();
            }
        }
    }
}

