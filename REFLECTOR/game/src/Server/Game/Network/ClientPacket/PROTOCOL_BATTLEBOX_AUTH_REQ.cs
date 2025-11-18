namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Managers;
    using Plugin.Core.Models;
    using Plugin.Core.SQL;
    using Plugin.Core.Utility;
    using Plugin.Core.XML;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_BATTLEBOX_AUTH_REQ : GameClientPacket
    {
        private long long_0;
        private int int_0;

        public MessageModel CreateMessage(string SenderName, long OwnerId, string Text)
        {
            MessageModel model1 = new MessageModel(15.0);
            model1.SenderName = SenderName;
            model1.Text = Text;
            model1.Type = NoteMessageType.Gift;
            model1.State = NoteMessageState.Unreaded;
            MessageModel message = model1;
            return (DaoManagerSQL.CreateMessage(OwnerId, message) ? message : null);
        }

        public override void Read()
        {
            this.long_0 = base.ReadUD();
            this.int_0 = base.ReadD();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    ItemsModel model = player.Inventory.GetItem(this.long_0);
                    if (model == null)
                    {
                        base.Client.SendPacket(new PROTOCOL_BATTLEBOX_AUTH_ACK(0x80000000, null, 0));
                    }
                    else
                    {
                        BattleBoxModel battleBox = BattleBoxXML.GetBattleBox(model.Id);
                        if ((battleBox == null) || (battleBox.RequireTags != this.int_0))
                        {
                            base.Client.SendPacket(new PROTOCOL_BATTLEBOX_AUTH_ACK(0x80000000, null, 0));
                        }
                        else if (this.int_0 > player.Tags)
                        {
                            base.Client.SendPacket(new PROTOCOL_BATTLEBOX_AUTH_ACK(0x80000000, null, 0));
                        }
                        else if (!DaoManagerSQL.UpdateAccountTags(player.PlayerId, player.Tags - this.int_0))
                        {
                            base.Client.SendPacket(new PROTOCOL_BATTLEBOX_AUTH_ACK(0x80000000, null, 0));
                        }
                        else
                        {
                            GoodsItem good = ShopManager.GetGood(battleBox.GetItemWithProbabilities<int>(battleBox.Goods, battleBox.Probabilities));
                            if (good == null)
                            {
                                base.Client.SendPacket(new PROTOCOL_BATTLEBOX_AUTH_ACK(0x80000000, null, 0));
                            }
                            else
                            {
                                player.Tags -= this.int_0;
                                if (ComDiv.UpdateDB("accounts", "tags", player.Tags, "player_id", player.PlayerId))
                                {
                                    base.Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, player, good.Item));
                                }
                                model.Count--;
                                if (model.Count > 0)
                                {
                                    ComDiv.UpdateDB("player_items", "count", (long) model.Count, "owner_id", player.PlayerId, "id", model.Id);
                                    base.Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(1, player, model));
                                }
                                else
                                {
                                    if (DaoManagerSQL.DeletePlayerInventoryItem(model.ObjectId, player.PlayerId))
                                    {
                                        player.Inventory.RemoveItem(model);
                                    }
                                    base.Client.SendPacket(new PROTOCOL_AUTH_SHOP_DELETE_ITEM_ACK(1, this.long_0));
                                }
                                base.Client.SendPacket(new PROTOCOL_BATTLEBOX_AUTH_ACK(0, player, good.Item.Id));
                            }
                        }
                    }
                }
            }
            catch (Exception exception1)
            {
                CLogger.Print(exception1.ToString(), LoggerType.Error, null);
            }
        }

        public void SendGiftMessage(Account Player, ItemsModel Item)
        {
            MessageModel model = this.CreateMessage("GM", Player.PlayerId, Translation.GetLabel("BattleBoxGift") + "\n\n" + Item.Name);
            if (model != null)
            {
                Player.Connection.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(model));
            }
        }
    }
}

