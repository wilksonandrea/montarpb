namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_CS_CHECK_MARK_REQ : GameClientPacket
    {
        private uint uint_0;
        private uint uint_1;

        public override void Read()
        {
            this.uint_0 = base.ReadUD();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if ((player == null) || ((ClanManager.GetClan(player.ClanId).Logo == this.uint_0) || ClanManager.IsClanLogoExist(this.uint_0)))
                {
                    this.uint_1 = 0x80000000;
                }
                base.Client.SendPacket(new PROTOCOL_CS_CHECK_MARK_ACK(this.uint_1));
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

