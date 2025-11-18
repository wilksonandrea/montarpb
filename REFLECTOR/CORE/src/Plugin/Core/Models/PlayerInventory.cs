namespace Plugin.Core.Models
{
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Plugin.Core.XML;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PlayerInventory
    {
        public PlayerInventory()
        {
            this.Items = new List<ItemsModel>();
        }

        public void AddItem(ItemsModel Item)
        {
            List<ItemsModel> list = this.Items;
            lock (list)
            {
                this.Items.Add(Item);
            }
        }

        public byte[] EquipmentData(int ItemId)
        {
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                ItemsModel model = this.GetItem(ItemId);
                if (model != null)
                {
                    packet.WriteD(model.Id);
                    packet.WriteD((uint) model.ObjectId);
                }
                else
                {
                    packet.WriteD(ItemId);
                    packet.WriteD(0);
                }
                return packet.ToArray();
            }
        }

        public ItemsModel GetItem(int Id)
        {
            ItemsModel model2;
            List<ItemsModel> list = this.Items;
            lock (list)
            {
                using (List<ItemsModel>.Enumerator enumerator = this.Items.GetEnumerator())
                {
                    while (true)
                    {
                        if (enumerator.MoveNext())
                        {
                            ItemsModel current = enumerator.Current;
                            if (current.Id != Id)
                            {
                                continue;
                            }
                            model2 = current;
                        }
                        else
                        {
                            return null;
                        }
                        break;
                    }
                }
            }
            return model2;
        }

        public ItemsModel GetItem(long ObjectId)
        {
            ItemsModel model2;
            List<ItemsModel> list = this.Items;
            lock (list)
            {
                using (List<ItemsModel>.Enumerator enumerator = this.Items.GetEnumerator())
                {
                    while (true)
                    {
                        if (enumerator.MoveNext())
                        {
                            ItemsModel current = enumerator.Current;
                            if (current.ObjectId != ObjectId)
                            {
                                continue;
                            }
                            model2 = current;
                        }
                        else
                        {
                            return null;
                        }
                        break;
                    }
                }
            }
            return model2;
        }

        public List<ItemsModel> GetItemsByType(ItemCategory Type)
        {
            List<ItemsModel> list = new List<ItemsModel>();
            List<ItemsModel> list2 = this.Items;
            lock (list2)
            {
                foreach (ItemsModel model in this.Items)
                {
                    if ((model.Category == Type) || ((model.Id > 0x186a00) && ((model.Id < 0x19f0a0) && (Type == ItemCategory.NewItem))))
                    {
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        public void LoadBasicItems()
        {
            List<ItemsModel> list = this.Items;
            lock (list)
            {
                this.Items.AddRange(TemplatePackXML.Basics);
            }
        }

        public void LoadGeneralBeret()
        {
            List<ItemsModel> list = this.Items;
            lock (list)
            {
                this.Items.Add(new ItemsModel(0x2932e8, "Beret S. General", ItemEquipType.Permanent, 1));
            }
        }

        public void LoadHatForGM()
        {
            List<ItemsModel> list = this.Items;
            lock (list)
            {
                this.Items.Add(new ItemsModel(0xaaf00, "MOD Hat", ItemEquipType.Permanent, 1));
            }
        }

        public bool RemoveItem(ItemsModel Item)
        {
            List<ItemsModel> list = this.Items;
            lock (list)
            {
                return this.Items.Remove(Item);
            }
        }

        public List<ItemsModel> Items { get; set; }
    }
}

