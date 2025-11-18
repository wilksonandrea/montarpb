namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Utility;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Data.Sync.Server;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_CS_CLOSE_CLAN_REQ : GameClientPacket
    {
        private uint uint_0;

        public override void Read()
        {
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    ClanModel clan = ClanManager.GetClan(player.ClanId);
                    if ((clan.Id > 0) && ((clan.OwnerId == base.Client.PlayerId) && ComDiv.DeleteDB("system_clan", "id", clan.Id)))
                    {
                        string[] cOLUMNS = new string[] { "clan_id", "clan_access" };
                        object[] vALUES = new object[] { 0, 0 };
                        if (ComDiv.UpdateDB("accounts", "player_id", player.PlayerId, cOLUMNS, vALUES))
                        {
                            string[] textArray2 = new string[] { "clan_matches", "clan_match_wins" };
                            object[] objArray2 = new object[] { 0, 0 };
                            if (ComDiv.UpdateDB("player_stat_clans", "owner_id", player.PlayerId, textArray2, objArray2) && ClanManager.RemoveClan(clan))
                            {
                                player.ClanId = 0;
                                player.ClanAccess = 0;
                                SendClanInfo.Load(clan, 1);
                                goto TR_0002;
                            }
                        }
                    }
                    this.uint_0 = 0x8000106a;
                }
                else
                {
                    return;
                }
            TR_0002:
                base.Client.SendPacket(new PROTOCOL_CS_CLOSE_CLAN_ACK(this.uint_0));
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_CS_CLOSE_CLAN_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

