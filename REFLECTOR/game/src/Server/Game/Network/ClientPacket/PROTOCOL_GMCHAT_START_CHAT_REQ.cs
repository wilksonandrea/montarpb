namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_GMCHAT_START_CHAT_REQ : GameClientPacket
    {
        private string string_0;
        private int int_0;
        private byte byte_0;

        public override void Read()
        {
            this.string_0 = base.ReadU(base.ReadC() * 2);
            this.int_0 = base.ReadD();
            this.byte_0 = base.ReadC();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    Account account2 = AccountManager.GetAccount(this.string_0, 1, 0x1f);
                    if ((account2 != null) && (player.Nickname != account2.Nickname))
                    {
                        base.Client.SendPacket(new PROTOCOL_GMCHAT_START_CHAT_ACK(0, account2));
                    }
                    else
                    {
                        base.Client.SendPacket(new PROTOCOL_GMCHAT_START_CHAT_ACK(0x80000000, null));
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_GMCHAT_START_CHAT_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

