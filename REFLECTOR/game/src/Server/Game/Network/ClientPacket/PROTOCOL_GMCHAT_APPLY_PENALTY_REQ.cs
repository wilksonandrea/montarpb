namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Managers;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_GMCHAT_APPLY_PENALTY_REQ : GameClientPacket
    {
        private long long_0;
        private string string_0;
        private string string_1;
        private int int_0;
        private byte byte_0;

        public override void Read()
        {
            this.string_0 = base.ReadU(base.ReadC() * 2);
            this.string_1 = base.ReadU(base.ReadC() * 2);
            this.byte_0 = base.ReadC();
            this.int_0 = base.ReadD();
            base.ReadC();
            this.long_0 = base.ReadQ();
        }

        public override void Run()
        {
            try
            {
                if (base.Client.Player != null)
                {
                    if (AccountManager.GetAccount(this.long_0, 0x1f) != null)
                    {
                        base.Client.SendPacket(new PROTOCOL_GMCHAT_APPLY_PENALTY_ACK(0));
                    }
                    else
                    {
                        base.Client.SendPacket(new PROTOCOL_GMCHAT_APPLY_PENALTY_ACK(0x80000000));
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_GMCHAT_APPLY_PENALTY_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

