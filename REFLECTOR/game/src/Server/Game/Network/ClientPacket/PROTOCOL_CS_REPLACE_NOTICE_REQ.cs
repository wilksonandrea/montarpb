namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Utility;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_CS_REPLACE_NOTICE_REQ : GameClientPacket
    {
        private string string_0;
        private EventErrorEnum eventErrorEnum_0;

        public override void Read()
        {
            this.string_0 = base.ReadU(base.ReadC() * 2);
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    ClanModel clan = ClanManager.GetClan(player.ClanId);
                    if ((clan.Id <= 0) || ((clan.News == this.string_0) || ((clan.OwnerId != base.Client.PlayerId) && ((player.ClanAccess < 1) || (player.ClanAccess > 2)))))
                    {
                        this.eventErrorEnum_0 = (EventErrorEnum) (-2147479461);
                    }
                    else if (ComDiv.UpdateDB("system_clan", "news", this.string_0, "id", clan.Id))
                    {
                        clan.News = this.string_0;
                    }
                    else
                    {
                        this.eventErrorEnum_0 = (EventErrorEnum) (-2147479437);
                    }
                    base.Client.SendPacket(new PROTOCOL_CS_REPLACE_NOTICE_ACK(this.eventErrorEnum_0));
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

