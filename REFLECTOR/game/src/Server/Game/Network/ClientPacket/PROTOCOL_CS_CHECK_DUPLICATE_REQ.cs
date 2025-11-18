namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Managers;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_CS_CHECK_DUPLICATE_REQ : GameClientPacket
    {
        private string string_0;

        public override void Read()
        {
            this.string_0 = base.ReadU(base.ReadC() * 2);
        }

        public override void Run()
        {
            try
            {
                base.Client.SendPacket(new PROTOCOL_CS_CHECK_DUPLICATE_ACK(!ClanManager.IsClanNameExist(this.string_0) ? 0 : 0x80000000));
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

