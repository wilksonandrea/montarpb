namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_CS_CLAN_LIST_DETAIL_INFO_REQ : GameClientPacket
    {
        private int int_0;

        public override void Read()
        {
            this.int_0 = base.ReadD();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    player.FindClanId = this.int_0;
                    ClanModel clan = ClanManager.GetClan(player.FindClanId);
                    if (clan.Id > 0)
                    {
                        base.Client.SendPacket(new PROTOCOL_CS_CLAN_LIST_DETAIL_INFO_ACK(0, clan));
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_CS_CLAN_LIST_DETAIL_INFO_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

