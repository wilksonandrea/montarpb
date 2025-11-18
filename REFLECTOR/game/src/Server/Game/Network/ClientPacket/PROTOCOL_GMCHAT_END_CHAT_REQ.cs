namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_GMCHAT_END_CHAT_REQ : GameClientPacket
    {
        private long long_0;

        public override void Read()
        {
            this.long_0 = base.ReadQ();
        }

        public override void Run()
        {
            try
            {
                if (base.Client.Player != null)
                {
                    Account account = AccountManager.GetAccount(this.long_0, 0x1f);
                    if (account != null)
                    {
                        base.Client.SendPacket(new PROTOCOL_GMCHAT_END_CHAT_ACK(0, account));
                    }
                    else
                    {
                        base.Client.SendPacket(new PROTOCOL_GMCHAT_END_CHAT_ACK(0x80000000, null));
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

