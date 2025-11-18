namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_CS_PAGE_CHATTING_REQ : GameClientPacket
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
                if ((player != null) && (this.chattingType_0 == ChattingType.ClanMemberPage))
                {
                    using (PROTOCOL_CS_PAGE_CHATTING_ACK protocol_cs_page_chatting_ack = new PROTOCOL_CS_PAGE_CHATTING_ACK(player, this.string_0))
                    {
                        ClanManager.SendPacket(protocol_cs_page_chatting_ack, player.ClanId, -1L, true, true);
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_CS_PAGE_CHATTING_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

