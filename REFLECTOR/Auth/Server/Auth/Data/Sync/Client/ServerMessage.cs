namespace Server.Auth.Data.Sync.Client
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Server.Auth;
    using Server.Auth.Network.ServerPacket;
    using System;

    public static class ServerMessage
    {
        public static void Load(SyncClientPacket C)
        {
            byte length = C.ReadC();
            string str = C.ReadS(length);
            if (!string.IsNullOrEmpty(str) && (length <= 60))
            {
                int num2 = 0;
                using (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK protocol_server_message_announce_ack = new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(str))
                {
                    num2 = AuthXender.Client.SendPacketToAllClients(protocol_server_message_announce_ack);
                }
                CLogger.Print($"Message sent to '{num2}' Players", LoggerType.Command, null);
            }
        }
    }
}

