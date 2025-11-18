namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.SQL;
    using Plugin.Core.Utility;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Data.Sync.Server;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_CS_CREATE_CLAN_REQ : GameClientPacket
    {
        private uint uint_0;
        private string string_0;
        private string string_1;

        public override void Read()
        {
            base.ReadD();
            this.string_0 = base.ReadU(0x22);
            this.string_1 = base.ReadU(510);
            base.ReadD();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    ClanModel model1 = new ClanModel();
                    model1.Name = this.string_0;
                    model1.Info = this.string_1;
                    model1.Logo = 0;
                    model1.OwnerId = player.PlayerId;
                    model1.CreationDate = uint.Parse(DateTimeUtil.Now("yyyyMMdd"));
                    ClanModel clan = model1;
                    if ((player.ClanId > 0) || (DaoManagerSQL.GetRequestClanId(player.PlayerId) > 0))
                    {
                        this.uint_0 = 0x8000105c;
                    }
                    else if ((0 > (player.Gold - ConfigLoader.MinCreateGold)) || (ConfigLoader.MinCreateRank > player.Rank))
                    {
                        this.uint_0 = 0x8000104a;
                    }
                    else if (!ClanManager.IsClanNameExist(clan.Name))
                    {
                        if (ClanManager.Clans.Count > ConfigLoader.MaxActiveClans)
                        {
                            this.uint_0 = 0x80001055;
                        }
                        else if (!DaoManagerSQL.CreateClan(out clan.Id, clan.Name, clan.OwnerId, clan.Info, clan.CreationDate) || !DaoManagerSQL.UpdateAccountGold(player.PlayerId, player.Gold - ConfigLoader.MinCreateGold))
                        {
                            this.uint_0 = 0x80001048;
                        }
                        else
                        {
                            clan.BestPlayers.SetDefault();
                            player.ClanDate = clan.CreationDate;
                            string[] cOLUMNS = new string[] { "clan_access", "clan_date", "clan_id" };
                            object[] vALUES = new object[] { 1, clan.CreationDate, clan.Id };
                            if (!ComDiv.UpdateDB("accounts", "player_id", player.PlayerId, cOLUMNS, vALUES))
                            {
                                this.uint_0 = 0x80001048;
                            }
                            else if (clan.Id <= 0)
                            {
                                this.uint_0 = 0x8000104b;
                            }
                            else
                            {
                                player.ClanId = clan.Id;
                                player.ClanAccess = 1;
                                ClanManager.AddClan(clan);
                                SendClanInfo.Load(clan, 0);
                                player.Gold -= ConfigLoader.MinCreateGold;
                            }
                        }
                    }
                    else
                    {
                        this.uint_0 = 0x8000105a;
                        return;
                    }
                    base.Client.SendPacket(new PROTOCOL_CS_DETAIL_INFO_ACK((int) this.uint_0, clan));
                    base.Client.SendPacket(new PROTOCOL_CS_CREATE_CLAN_ACK(this.uint_0, clan, player));
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_CS_CREATE_CLAN_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

