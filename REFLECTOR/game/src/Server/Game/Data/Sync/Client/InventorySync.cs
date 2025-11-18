namespace Server.Game.Data.Sync.Client
{
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Network;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using System;

    public class InventorySync
    {
        public static void Load(SyncClientPacket C)
        {
            long objectId = C.ReadQ();
            int num2 = C.ReadD();
            ItemEquipType type = (ItemEquipType) C.ReadC();
            ItemCategory category = (ItemCategory) C.ReadC();
            uint num3 = C.ReadUD();
            byte length = C.ReadC();
            string str = C.ReadS(length);
            Account account = AccountManager.GetAccount(C.ReadQ(), true);
            if (account != null)
            {
                ItemsModel model = account.Inventory.GetItem(objectId);
                if (model != null)
                {
                    model.Count = num3;
                }
                else
                {
                    ItemsModel model1 = new ItemsModel();
                    model1.ObjectId = objectId;
                    model1.Id = num2;
                    model1.Equip = type;
                    model1.Count = num3;
                    model1.Category = category;
                    model1.Name = str;
                    ItemsModel model2 = model1;
                    account.Inventory.AddItem(model2);
                }
            }
        }
    }
}

