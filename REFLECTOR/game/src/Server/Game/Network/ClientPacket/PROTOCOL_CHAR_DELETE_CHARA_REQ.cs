namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.SQL;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_CHAR_DELETE_CHARA_REQ : GameClientPacket
    {
        private int int_0;

        public override void Read()
        {
            this.int_0 = base.ReadC();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    CharacterModel characterSlot = player.Character.GetCharacterSlot(this.int_0);
                    if (characterSlot == null)
                    {
                        base.Client.SendPacket(new PROTOCOL_CHAR_DELETE_CHARA_ACK(0x800010a7, -1, null, null));
                    }
                    else
                    {
                        ItemsModel model2 = player.Inventory.GetItem(characterSlot.Id);
                        if (model2 == null)
                        {
                            base.Client.SendPacket(new PROTOCOL_CHAR_DELETE_CHARA_ACK(0x800010a7, -1, null, null));
                        }
                        else
                        {
                            int slot = 0;
                            foreach (CharacterModel model3 in player.Character.Characters)
                            {
                                if (model3.Slot != characterSlot.Slot)
                                {
                                    model3.Slot = slot;
                                    DaoManagerSQL.UpdatePlayerCharacter(slot, model3.ObjectId, player.PlayerId);
                                    slot++;
                                }
                            }
                            base.Client.SendPacket(new PROTOCOL_CHAR_DELETE_CHARA_ACK(0, this.int_0, player, model2));
                            if (DaoManagerSQL.DeletePlayerCharacter(characterSlot.ObjectId, player.PlayerId))
                            {
                                player.Character.RemoveCharacter(characterSlot);
                            }
                            if (DaoManagerSQL.DeletePlayerInventoryItem(model2.ObjectId, player.PlayerId))
                            {
                                player.Inventory.RemoveItem(model2);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_CHAR_DELETE_CHARA_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

