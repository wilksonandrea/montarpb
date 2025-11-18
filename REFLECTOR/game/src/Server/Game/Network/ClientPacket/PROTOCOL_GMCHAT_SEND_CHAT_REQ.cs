namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_GMCHAT_SEND_CHAT_REQ : GameClientPacket
    {
        private long long_0;
        private string string_0;
        private string string_1;
        private string string_2;

        public override void Read()
        {
            this.string_0 = base.ReadU(base.ReadC() * 2);
            this.string_2 = base.ReadU(base.ReadH() * 2);
            this.string_1 = base.ReadU(base.ReadC() * 2);
            this.long_0 = base.ReadQ();
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
                        account2.SendPacket(new PROTOCOL_GMCHAT_SEND_CHAT_ACK(this.string_0, this.string_2, this.string_1, account2));
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(base.GetType().Name + ": " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

