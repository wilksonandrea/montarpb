namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_CS_CHATTING_REQ : GameClientPacket
    {
        private ChattingType chattingType_0;
        private string string_0;

        public override void Read()
        {
            this.chattingType_0 = (ChattingType) base.ReadH();
            this.string_0 = base.ReadU(base.ReadH() * 2);
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    int num = -1;
                    bool flag = true;
                    bool useCache = true;
                    if ((this.string_0.Length <= 60) && (this.chattingType_0 == ChattingType.Clan))
                    {
                        using (PROTOCOL_CS_CHATTING_ACK protocol_cs_chatting_ack = new PROTOCOL_CS_CHATTING_ACK(this.string_0, player))
                        {
                            ClanManager.SendPacket(protocol_cs_chatting_ack, player.ClanId, (long) num, useCache, flag);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

