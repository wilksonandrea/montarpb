namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.SQL;
    using Plugin.Core.Utility;
    using Plugin.Core.XML;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_BASE_USER_TITLE_CHANGE_REQ : GameClientPacket
    {
        private int int_0;
        private uint uint_0;

        public override void Read()
        {
            this.int_0 = base.ReadC();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if ((player != null) && (this.int_0 < 0x2d))
                {
                    if (player.Title.OwnerId == 0)
                    {
                        DaoManagerSQL.CreatePlayerTitlesDB(player.PlayerId);
                        PlayerTitles titles1 = new PlayerTitles();
                        titles1.OwnerId = player.PlayerId;
                        player.Title = titles1;
                    }
                    TitleModel title = TitleSystemXML.GetTitle(this.int_0, true);
                    if (title == null)
                    {
                        this.uint_0 = 0x80001083;
                    }
                    else
                    {
                        TitleModel model2;
                        TitleModel model3;
                        TitleSystemXML.Get2Titles(title.Req1, title.Req2, out model2, out model3, false);
                        if ((((title.Req1 != 0) && (model2 == null)) || (((title.Req2 != 0) && (model3 == null)) || (player.Rank < title.Rank))) || ((player.Ribbon < title.Ribbon) || ((player.Medal < title.Medal) || ((player.MasterMedal < title.MasterMedal) || ((player.Ensign < title.Ensign) || (player.Title.Contains(title.Flag) || (!player.Title.Contains(model2.Flag) || !player.Title.Contains(model3.Flag))))))))
                        {
                            this.uint_0 = 0x80001083;
                        }
                        else
                        {
                            player.Ribbon -= title.Ribbon;
                            player.Medal -= title.Medal;
                            player.MasterMedal -= title.MasterMedal;
                            player.Ensign -= title.Ensign;
                            long flags = player.Title.Add(title.Flag);
                            DaoManagerSQL.UpdatePlayerTitlesFlags(player.PlayerId, flags);
                            List<ItemsModel> awards = TitleAwardXML.GetAwards(this.int_0);
                            if (awards.Count > 0)
                            {
                                base.Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, player, awards));
                            }
                            base.Client.SendPacket(new PROTOCOL_BASE_MEDAL_GET_INFO_ACK(player));
                            DaoManagerSQL.UpdatePlayerTitleRequi(player.PlayerId, player.Medal, player.Ensign, player.MasterMedal, player.Ribbon);
                            if (player.Title.Slots < title.Slot)
                            {
                                player.Title.Slots = title.Slot;
                                ComDiv.UpdateDB("player_titles", "slots", player.Title.Slots, "owner_id", player.PlayerId);
                            }
                        }
                    }
                    base.Client.SendPacket(new PROTOCOL_BASE_USER_TITLE_CHANGE_ACK(this.uint_0, player.Title.Slots));
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_BASE_USER_TITLE_CHANGE_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

