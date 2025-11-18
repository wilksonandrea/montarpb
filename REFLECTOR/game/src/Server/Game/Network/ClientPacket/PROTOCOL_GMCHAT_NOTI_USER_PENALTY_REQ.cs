namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_GMCHAT_NOTI_USER_PENALTY_REQ : GameClientPacket
    {
        private long long_0;
        private int int_0;
        private byte byte_0;

        public override void Read()
        {
            this.int_0 = base.ReadD();
            this.byte_0 = base.ReadC();
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
                        base.Client.SendPacket(new PROTOCOL_GMCHAT_NOTI_USER_PENALTY_ACK(0, account));
                    }
                    else
                    {
                        base.Client.SendPacket(new PROTOCOL_GMCHAT_NOTI_USER_PENALTY_ACK(0x80000000, account));
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

