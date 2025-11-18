namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Filters;
    using Plugin.Core.Models;
    using Plugin.Core.SQL;
    using Plugin.Core.Utility;
    using Plugin.Core.XML;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Data.Utils;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_BASE_CREATE_NICK_REQ : GameClientPacket
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
                Account player = base.Client.Player;
                if (player != null)
                {
                    if ((player.Nickname.Length != 0) || (string.IsNullOrEmpty(this.string_0) || ((this.string_0.Length < ConfigLoader.MinNickSize) || (this.string_0.Length > ConfigLoader.MaxNickSize))))
                    {
                        base.Client.SendPacket(new PROTOCOL_BASE_CREATE_NICK_ACK(0x80000113, null));
                    }
                    else
                    {
                        foreach (string str in NickFilter.Filters)
                        {
                            if (this.string_0.Contains(str))
                            {
                                base.Client.SendPacket(new PROTOCOL_BASE_CREATE_NICK_ACK(0x80001013, null));
                                break;
                            }
                        }
                        if (DaoManagerSQL.IsPlayerNameExist(this.string_0))
                        {
                            base.Client.SendPacket(new PROTOCOL_BASE_CREATE_NICK_ACK(0x80000113, null));
                        }
                        else if (!AccountManager.UpdatePlayerName(this.string_0, player.PlayerId))
                        {
                            base.Client.SendPacket(new PROTOCOL_BASE_CREATE_NICK_ACK(0x80001013, null));
                        }
                        else
                        {
                            DaoManagerSQL.CreatePlayerNickHistory(player.PlayerId, player.Nickname, this.string_0, "First nick choosed");
                            player.Nickname = this.string_0;
                            List<ItemsModel> basics = TemplatePackXML.Basics;
                            if (basics.Count > 0)
                            {
                                foreach (ItemsModel model in basics)
                                {
                                    if ((ComDiv.GetIdStatics(model.Id, 1) == 6) && (player.Character.GetCharacter(model.Id) == null))
                                    {
                                        AllUtils.CreateCharacter(player, model);
                                    }
                                }
                            }
                            List<ItemsModel> awards = TemplatePackXML.Awards;
                            if (awards.Count > 0)
                            {
                                foreach (ItemsModel model2 in awards)
                                {
                                    if ((ComDiv.GetIdStatics(model2.Id, 1) == 6) && (player.Character.GetCharacter(model2.Id) == null))
                                    {
                                        AllUtils.CreateCharacter(player, model2);
                                        continue;
                                    }
                                    base.Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, player, model2));
                                }
                            }
                            base.Client.SendPacket(new PROTOCOL_BASE_CREATE_NICK_ACK(0, player));
                            base.Client.SendPacket(new PROTOCOL_SHOP_PLUS_POINT_ACK(player.Gold, player.Gold, 4));
                            base.Client.SendPacket(new PROTOCOL_BASE_QUEST_GET_INFO_ACK(player));
                            base.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_ACK(player.Friend.Friends));
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_LOBBY_CREATE_NICK_NAME_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

