namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Managers;
    using Plugin.Core.Models;
    using Plugin.Core.SQL;
    using Plugin.Core.Utility;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_CHAR_CREATE_CHARA_REQ : GameClientPacket
    {
        private string string_0;
        private List<CartGoods> list_0 = new List<CartGoods>();

        private CharacterModel method_0(Account account_0, List<GoodsItem> list_1)
        {
            CharacterModel model;
            using (List<GoodsItem>.Enumerator enumerator = list_1.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        GoodsItem current = enumerator.Current;
                        if ((current == null) || (current.Item.Id == 0))
                        {
                            continue;
                        }
                        CharacterModel model1 = new CharacterModel();
                        model1.Id = current.Item.Id;
                        model1.Slot = account_0.Character.GenSlotId(current.Item.Id);
                        model1.Name = this.string_0;
                        model1.CreateDate = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
                        model1.PlayTime = 0;
                        model = model1;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return model;
        }

        public override void Read()
        {
            base.ReadC();
            this.string_0 = base.ReadU(base.ReadC() * 2);
            base.ReadC();
            CartGoods goods1 = new CartGoods();
            goods1.GoodId = base.ReadD();
            goods1.BuyType = base.ReadC();
            CartGoods item = goods1;
            this.list_0.Add(item);
            base.ReadC();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    if ((player.Inventory.Items.Count >= 500) || (player.Character.Characters.Count >= 0x40))
                    {
                        base.Client.SendPacket(new PROTOCOL_CHAR_CREATE_CHARA_ACK(0x800010b9, 0xff, null, null));
                    }
                    else
                    {
                        int num;
                        int num2;
                        int num3;
                        List<GoodsItem> list = ShopManager.GetGoods(this.list_0, out num, out num2, out num3);
                        if (list.Count == 0)
                        {
                            base.Client.SendPacket(new PROTOCOL_CHAR_CREATE_CHARA_ACK(0x80001017, 0xff, null, null));
                        }
                        else if ((0 > (player.Gold - num)) || ((0 > (player.Cash - num2)) || (0 > (player.Tags - num3))))
                        {
                            base.Client.SendPacket(new PROTOCOL_CHAR_CREATE_CHARA_ACK(0x80001018, 0xff, null, null));
                        }
                        else if (!DaoManagerSQL.UpdateAccountValuable(player.PlayerId, player.Gold - num, player.Cash - num2, player.Tags - num3))
                        {
                            base.Client.SendPacket(new PROTOCOL_CHAR_CREATE_CHARA_ACK(0x80001019, 0xff, null, null));
                        }
                        else
                        {
                            player.Gold -= num;
                            player.Cash -= num2;
                            player.Tags -= num3;
                            CharacterModel character = this.method_0(player, list);
                            if (character != null)
                            {
                                player.Character.AddCharacter(character);
                                if (player.Character.GetCharacter(character.Id) != null)
                                {
                                    DaoManagerSQL.CreatePlayerCharacter(character, player.PlayerId);
                                }
                            }
                            base.Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, player, list));
                            base.Client.SendPacket(new PROTOCOL_CHAR_CREATE_CHARA_ACK(0, 1, character, player));
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

