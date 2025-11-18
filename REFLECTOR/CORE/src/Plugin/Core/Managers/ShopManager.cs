namespace Plugin.Core.Managers
{
    using Npgsql;
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Network;
    using Plugin.Core.SQL;
    using Plugin.Core.Utility;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class ShopManager
    {
        public static List<ItemsRepair> ItemRepairs = new List<ItemsRepair>();
        public static List<GoodsItem> ShopAllList = new List<GoodsItem>();
        public static List<GoodsItem> ShopBuyableList = new List<GoodsItem>();
        public static SortedList<int, GoodsItem> ShopUniqueList = new SortedList<int, GoodsItem>();
        public static List<ShopData> ShopDataMt1 = new List<ShopData>();
        public static List<ShopData> ShopDataMt2 = new List<ShopData>();
        public static List<ShopData> ShopDataGoods = new List<ShopData>();
        public static List<ShopData> ShopDataItems = new List<ShopData>();
        public static List<ShopData> ShopDataItemRepairs = new List<ShopData>();
        public static byte[] ShopTagData;
        public static int TotalGoods;
        public static int TotalItems;
        public static int TotalMatching1;
        public static int TotalMatching2;
        public static int TotalRepairs;
        public static int Set4p;

        public static GoodsItem GetGood(int GoodId)
        {
            GoodsItem item2;
            if (GoodId == 0)
            {
                return null;
            }
            List<GoodsItem> shopAllList = ShopAllList;
            lock (shopAllList)
            {
                using (List<GoodsItem>.Enumerator enumerator = ShopAllList.GetEnumerator())
                {
                    while (true)
                    {
                        if (enumerator.MoveNext())
                        {
                            GoodsItem current = enumerator.Current;
                            if (current.Id != GoodId)
                            {
                                continue;
                            }
                            item2 = current;
                        }
                        else
                        {
                            return null;
                        }
                        break;
                    }
                }
            }
            return item2;
        }

        public static List<GoodsItem> GetGoods(List<CartGoods> ShopCart, out int GoldPrice, out int CashPrice, out int TagsPrice)
        {
            GoldPrice = 0;
            CashPrice = 0;
            TagsPrice = 0;
            List<GoodsItem> list = new List<GoodsItem>();
            if (ShopCart.Count != 0)
            {
                List<GoodsItem> shopBuyableList = ShopBuyableList;
                lock (shopBuyableList)
                {
                    foreach (GoodsItem item in ShopBuyableList)
                    {
                        foreach (CartGoods goods in ShopCart)
                        {
                            if (goods.GoodId == item.Id)
                            {
                                list.Add(item);
                                if (goods.BuyType == 1)
                                {
                                    GoldPrice += item.PriceGold;
                                    continue;
                                }
                                if (goods.BuyType == 2)
                                {
                                    CashPrice += item.PriceCash;
                                }
                            }
                        }
                    }
                }
            }
            return list;
        }

        public static GoodsItem GetItemId(int ItemId)
        {
            GoodsItem item2;
            if (ItemId == 0)
            {
                return null;
            }
            List<GoodsItem> shopAllList = ShopAllList;
            lock (shopAllList)
            {
                using (List<GoodsItem>.Enumerator enumerator = ShopAllList.GetEnumerator())
                {
                    while (true)
                    {
                        if (enumerator.MoveNext())
                        {
                            GoodsItem current = enumerator.Current;
                            if (current.Item.Id != ItemId)
                            {
                                continue;
                            }
                            item2 = current;
                        }
                        else
                        {
                            return null;
                        }
                        break;
                    }
                }
            }
            return item2;
        }

        public static ItemsRepair GetRepairItem(int ItemId)
        {
            ItemsRepair repair2;
            if (ItemId == 0)
            {
                return null;
            }
            List<ItemsRepair> list = ItemRepairs;
            lock (list)
            {
                using (List<ItemsRepair>.Enumerator enumerator = ItemRepairs.GetEnumerator())
                {
                    while (true)
                    {
                        if (enumerator.MoveNext())
                        {
                            ItemsRepair current = enumerator.Current;
                            if (current.Id != ItemId)
                            {
                                continue;
                            }
                            repair2 = current;
                        }
                        else
                        {
                            return null;
                        }
                        break;
                    }
                }
            }
            return repair2;
        }

        public static bool IsBlocked(string Text, List<int> Items)
        {
            SortedList<int, GoodsItem> shopUniqueList = ShopUniqueList;
            lock (shopUniqueList)
            {
                foreach (GoodsItem item in ShopUniqueList.Values)
                {
                    if (!Items.Contains(item.Item.Id) && item.Item.Name.Contains(Text))
                    {
                        Items.Add(item.Item.Id);
                    }
                }
            }
            return false;
        }

        public static bool IsRepairableItem(int ItemId) => 
            GetRepairItem(ItemId) != null;

        public static void Load(int Type)
        {
            smethod_4(Type);
            smethod_0(Type);
            smethod_1(Type);
            smethod_2(Type);
            if (Type == 1)
            {
                try
                {
                    smethod_5(0);
                    smethod_6(1);
                    smethod_7();
                    smethod_8();
                    smethod_17();
                }
                catch (Exception exception)
                {
                    CLogger.Print(exception.Message, LoggerType.Error, exception);
                }
                CLogger.Print($"Plugin Loaded: {ShopBuyableList.Count} Buyable Items", LoggerType.Info, null);
                CLogger.Print($"Plugin Loaded: {ItemRepairs.Count} Repairable Items", LoggerType.Info, null);
            }
        }

        public static void Reset()
        {
            Set4p = 0;
            ShopAllList.Clear();
            ShopBuyableList.Clear();
            ShopUniqueList.Clear();
            ShopDataMt1.Clear();
            ShopDataMt2.Clear();
            ShopDataGoods.Clear();
            ShopDataItems.Clear();
            ShopDataItemRepairs.Clear();
            ItemRepairs.Clear();
            TotalGoods = 0;
            TotalItems = 0;
            TotalMatching1 = 0;
            TotalMatching2 = 0;
            TotalRepairs = 0;
        }

        private static void smethod_0(int int_0)
        {
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM system_shop";
                    command.CommandType = CommandType.Text;
                    NpgsqlDataReader reader = command.ExecuteReader(CommandBehavior.Default);
                    while (true)
                    {
                        string[] textArray4;
                        string[] textArray5;
                        string[] textArray6;
                        if (!reader.Read())
                        {
                            command.Dispose();
                            reader.Close();
                            connection.Dispose();
                            connection.Close();
                            break;
                        }
                        int num = int.Parse($"{reader["item_id"]}");
                        if (!$"{reader["item_count_list"]}".Contains(","))
                        {
                            textArray4 = new string[] { $"{reader["item_count_list"]}" };
                        }
                        else
                        {
                            char[] separator = new char[] { ',' };
                            textArray4 = $"{reader["item_count_list"]}".Split(separator);
                        }
                        string[] strArray = textArray4;
                        if (!$"{reader["price_cash_list"]}".Contains(","))
                        {
                            textArray5 = new string[] { $"{reader["price_cash_list"]}" };
                        }
                        else
                        {
                            char[] separator = new char[] { ',' };
                            textArray5 = $"{reader["price_cash_list"]}".Split(separator);
                        }
                        string[] strArray2 = textArray5;
                        if (!$"{reader["price_gold_list"]}".Contains(","))
                        {
                            textArray6 = new string[] { $"{reader["price_gold_list"]}" };
                        }
                        else
                        {
                            char[] separator = new char[] { ',' };
                            textArray6 = $"{reader["price_gold_list"]}".Split(separator);
                        }
                        string[] strArray3 = textArray6;
                        if ((strArray.Length != strArray2.Length) || (strArray2.Length != strArray3.Length))
                        {
                            CLogger.Print($"Loading goods with invalid counts / moneys / points sizes. ({num})", LoggerType.Warning, null);
                        }
                        else
                        {
                            int num2 = 0;
                            string[] strArray4 = strArray;
                            for (int i = 0; i < strArray4.Length; i++)
                            {
                                uint num4;
                                num2++;
                                if (!uint.TryParse(strArray4[i], out num4))
                                {
                                    CLogger.Print($"Loading goods with count != UInt ({num})", LoggerType.Warning, null);
                                }
                                else
                                {
                                    int num5;
                                    if (!int.TryParse(strArray2[num2 - 1], out num5))
                                    {
                                        CLogger.Print($"Loading goods with cash != Int ({num})", LoggerType.Warning, null);
                                    }
                                    else
                                    {
                                        int num6;
                                        if (!int.TryParse(strArray3[num2 - 1], out num6))
                                        {
                                            CLogger.Print($"Loading goods with gold != Int ({num})", LoggerType.Warning, null);
                                        }
                                        else
                                        {
                                            int num7 = ComDiv.GetIdStatics(num, 1);
                                            string str = $"{reader["item_name"]}";
                                            GoodsItem item1 = new GoodsItem();
                                            GoodsItem item2 = new GoodsItem();
                                            item2.Id = int.Parse($"{num}{((num7 == 0x16) || ((num7 == 0x1a) || ((num7 == 0x24) || ((num7 == 0x25) || (num7 == 40))))) ? "00" : $"{num2:D2}"}");
                                            GoodsItem local1 = item2;
                                            local1.PriceGold = num6;
                                            local1.PriceCash = num5;
                                            GoodsItem item = local1;
                                            int percent = int.Parse($"{reader["discount_percent"]}");
                                            if ((percent > 0) && (item.PriceCash > 0))
                                            {
                                                item.StarCash = item.PriceCash * 0xff;
                                                item.PriceCash = ComDiv.Percentage(item.PriceCash, percent);
                                            }
                                            if ((percent > 0) && (item.PriceGold > 0))
                                            {
                                                item.StarGold = item.PriceGold * 0xff;
                                                item.PriceGold = ComDiv.Percentage(item.PriceGold, percent);
                                            }
                                            item.Tag = (percent > 0) ? ItemTag.Sale : ((ItemTag) int.Parse($"{reader["shop_tag"]}"));
                                            item.Title = int.Parse($"{reader["title_requi"]}");
                                            item.AuthType = int.Parse($"{reader["item_consume"]}");
                                            item.BuyType2 = (item.AuthType == 2) ? 1 : (IsRepairableItem(num) ? 2 : 1);
                                            item.BuyType3 = (item.AuthType == 1) ? 2 : 1;
                                            item.Visibility = bool.Parse($"{reader["item_visible"]}") ? 0 : 4;
                                            item.Item.SetItemId(num);
                                            item.Item.Name = (item.AuthType == 1) ? $"{str} ({num4} qty)" : ((item.AuthType == 2) ? $"{str} ({(num4 / 0xe10)} hours)" : str);
                                            item.Item.Count = num4;
                                            int num9 = ComDiv.GetIdStatics(item.Item.Id, 1);
                                            if ((int_0 == 1) || ((int_0 == 2) && (num9 == 0x10)))
                                            {
                                                ShopAllList.Add(item);
                                                if ((item.Visibility != 2) && (item.Visibility != 4))
                                                {
                                                    ShopBuyableList.Add(item);
                                                }
                                                if (!ShopUniqueList.ContainsKey(item.Item.Id) && (item.AuthType > 0))
                                                {
                                                    ShopUniqueList.Add(item.Item.Id, item);
                                                    if (item.Visibility == 4)
                                                    {
                                                        Set4p++;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }

        private static void smethod_1(int int_0)
        {
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM system_shop_effects";
                    command.CommandType = CommandType.Text;
                    NpgsqlDataReader reader = command.ExecuteReader(CommandBehavior.Default);
                    while (true)
                    {
                        string[] textArray4;
                        string[] textArray5;
                        string[] textArray6;
                        if (!reader.Read())
                        {
                            command.Dispose();
                            reader.Close();
                            connection.Dispose();
                            connection.Close();
                            break;
                        }
                        int num = int.Parse($"{reader["coupon_id"]}");
                        if (!$"{reader["coupon_count_day_list"]}".Contains(","))
                        {
                            textArray4 = new string[] { $"{reader["coupon_count_day_list"]}" };
                        }
                        else
                        {
                            char[] separator = new char[] { ',' };
                            textArray4 = $"{reader["coupon_count_day_list"]}".Split(separator);
                        }
                        string[] strArray = textArray4;
                        if (!$"{reader["price_cash_list"]}".Contains(","))
                        {
                            textArray5 = new string[] { $"{reader["price_cash_list"]}" };
                        }
                        else
                        {
                            char[] separator = new char[] { ',' };
                            textArray5 = $"{reader["price_cash_list"]}".Split(separator);
                        }
                        string[] strArray2 = textArray5;
                        if (!$"{reader["price_gold_list"]}".Contains(","))
                        {
                            textArray6 = new string[] { $"{reader["price_gold_list"]}" };
                        }
                        else
                        {
                            char[] separator = new char[] { ',' };
                            textArray6 = $"{reader["price_gold_list"]}".Split(separator);
                        }
                        string[] strArray3 = textArray6;
                        if ((strArray.Length != strArray2.Length) || (strArray2.Length != strArray3.Length))
                        {
                            CLogger.Print($"Loading goods with invalid counts / moneys / points sizes. ({num})", LoggerType.Warning, null);
                        }
                        else
                        {
                            int num2 = 0;
                            string[] strArray4 = strArray;
                            for (int i = 0; i < strArray4.Length; i++)
                            {
                                int num4;
                                num2++;
                                if (!int.TryParse(strArray4[i], out num4))
                                {
                                    CLogger.Print($"Loading effects with count != Int ({num})", LoggerType.Warning, null);
                                }
                                else
                                {
                                    int num5;
                                    if (!int.TryParse(strArray2[num2 - 1], out num5))
                                    {
                                        CLogger.Print($"Loading effects with cash != Int ({num})", LoggerType.Warning, null);
                                    }
                                    else
                                    {
                                        int num6;
                                        if (!int.TryParse(strArray3[num2 - 1], out num6))
                                        {
                                            CLogger.Print($"Loading effects with gold != Int ({num})", LoggerType.Warning, null);
                                        }
                                        else
                                        {
                                            if (num4 >= 100)
                                            {
                                                num4 = 100;
                                            }
                                            int num7 = int.Parse($"{$"{num}".Substring(0, 2)}{num4:D2}{$"{num}".Substring(4, 3)}");
                                            GoodsItem item1 = new GoodsItem();
                                            item1.Id = int.Parse($"{num}{num2:D2}");
                                            item1.PriceGold = num6;
                                            item1.PriceCash = num5;
                                            GoodsItem item = item1;
                                            int percent = int.Parse($"{reader["discount_percent"]}");
                                            if ((percent > 0) && (item.PriceCash > 0))
                                            {
                                                item.StarCash = item.PriceCash * 0xff;
                                                item.PriceCash = ComDiv.Percentage(item.PriceCash, percent);
                                            }
                                            if ((percent > 0) && (item.PriceGold > 0))
                                            {
                                                item.PriceGold *= 0xff;
                                                item.PriceGold = ComDiv.Percentage(item.PriceGold, percent);
                                            }
                                            item.Tag = (percent > 0) ? ItemTag.Sale : ((ItemTag) int.Parse($"{reader["shop_tag"]}"));
                                            item.Title = 0;
                                            item.AuthType = 1;
                                            item.BuyType2 = 1;
                                            item.BuyType3 = 2;
                                            item.Visibility = bool.Parse($"{reader["coupon_visible"]}") ? 0 : 4;
                                            item.Item.SetItemId(num7);
                                            item.Item.Name = $"{reader["coupon_name"]} ({num4} days)";
                                            item.Item.Count = 1;
                                            int num9 = ComDiv.GetIdStatics(item.Item.Id, 1);
                                            if ((int_0 == 1) || ((int_0 == 2) && (num9 == 0x10)))
                                            {
                                                ShopAllList.Add(item);
                                                if ((item.Visibility != 2) && (item.Visibility != 4))
                                                {
                                                    ShopBuyableList.Add(item);
                                                }
                                                if (!ShopUniqueList.ContainsKey(item.Item.Id) && (item.AuthType > 0))
                                                {
                                                    ShopUniqueList.Add(item.Item.Id, item);
                                                    if (item.Visibility == 4)
                                                    {
                                                        Set4p++;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }

        private static byte[] smethod_10(int int_0, int int_1, ref int int_2, List<GoodsItem> list_0)
        {
            byte[] buffer;
            int_2 = 0;
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                int num = int_1 * int_0;
                while (true)
                {
                    if (num < list_0.Count)
                    {
                        smethod_14(list_0[num], packet);
                        int num2 = int_2 + 1;
                        int_2 = num2;
                        if (num2 != int_0)
                        {
                            num++;
                            continue;
                        }
                    }
                    buffer = packet.ToArray();
                    break;
                }
            }
            return buffer;
        }

        private static byte[] smethod_11(int int_0, int int_1, ref int int_2, List<ItemsRepair> list_0)
        {
            byte[] buffer;
            int_2 = 0;
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                int num = int_1 * int_0;
                while (true)
                {
                    if (num < list_0.Count)
                    {
                        smethod_15(list_0[num], packet);
                        int num2 = int_2 + 1;
                        int_2 = num2;
                        if (num2 != int_0)
                        {
                            num++;
                            continue;
                        }
                    }
                    buffer = packet.ToArray();
                    break;
                }
            }
            return buffer;
        }

        private static byte[] smethod_12(int int_0, int int_1, ref int int_2, List<GoodsItem> list_0)
        {
            byte[] buffer;
            int_2 = 0;
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                int num = int_1 * int_0;
                while (true)
                {
                    if (num < list_0.Count)
                    {
                        smethod_16(list_0[num], packet);
                        int num2 = int_2 + 1;
                        int_2 = num2;
                        if (num2 != int_0)
                        {
                            num++;
                            continue;
                        }
                    }
                    buffer = packet.ToArray();
                    break;
                }
            }
            return buffer;
        }

        private static void smethod_13(GoodsItem goodsItem_0, SyncServerPacket syncServerPacket_0)
        {
            syncServerPacket_0.WriteD(goodsItem_0.Item.Id);
            syncServerPacket_0.WriteC((byte) goodsItem_0.AuthType);
            syncServerPacket_0.WriteC((byte) goodsItem_0.BuyType2);
            syncServerPacket_0.WriteC((byte) goodsItem_0.BuyType3);
            syncServerPacket_0.WriteC((byte) goodsItem_0.Title);
            syncServerPacket_0.WriteC((goodsItem_0.Title != 0) ? ((byte) 2) : ((byte) 0));
            syncServerPacket_0.WriteH((short) 0);
        }

        private static void smethod_14(GoodsItem goodsItem_0, SyncServerPacket syncServerPacket_0)
        {
            syncServerPacket_0.WriteD(goodsItem_0.Id);
            syncServerPacket_0.WriteC(1);
            syncServerPacket_0.WriteC((goodsItem_0.Visibility == 4) ? ((byte) 4) : ((byte) 1));
            syncServerPacket_0.WriteD(goodsItem_0.PriceGold);
            syncServerPacket_0.WriteD(goodsItem_0.PriceCash);
            syncServerPacket_0.WriteD(0);
            syncServerPacket_0.WriteC((byte) goodsItem_0.Tag);
            syncServerPacket_0.WriteC(0);
            syncServerPacket_0.WriteC(0);
            syncServerPacket_0.WriteC(0);
            syncServerPacket_0.WriteD((goodsItem_0.StarCash > 0) ? goodsItem_0.StarCash : ((goodsItem_0.StarGold > 0) ? goodsItem_0.StarGold : 0));
            syncServerPacket_0.WriteD(0);
            syncServerPacket_0.WriteD(0);
            syncServerPacket_0.WriteD(0);
            syncServerPacket_0.WriteB(new byte[0x62]);
        }

        private static void smethod_15(ItemsRepair itemsRepair_0, SyncServerPacket syncServerPacket_0)
        {
            syncServerPacket_0.WriteD(itemsRepair_0.Id);
            syncServerPacket_0.WriteD((int) (((long) itemsRepair_0.Point) / ((ulong) itemsRepair_0.Quantity)));
            syncServerPacket_0.WriteD((int) (((long) itemsRepair_0.Cash) / ((ulong) itemsRepair_0.Quantity)));
            syncServerPacket_0.WriteD(itemsRepair_0.Quantity);
        }

        private static void smethod_16(GoodsItem goodsItem_0, SyncServerPacket syncServerPacket_0)
        {
            syncServerPacket_0.WriteD(goodsItem_0.Id);
            syncServerPacket_0.WriteD(goodsItem_0.Item.Id);
            syncServerPacket_0.WriteD(goodsItem_0.Item.Count);
            syncServerPacket_0.WriteD(0);
        }

        private static void smethod_17()
        {
            string text = "zOne";
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                packet.WriteS(text, text.Length + 1);
                ShopTagData = packet.ToArray();
            }
        }

        private static void smethod_2(int int_0)
        {
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command = connection.CreateCommand();
                    command.CommandText = $"SELECT * FROM system_shop_sets WHERE visible = '{true}';";
                    command.CommandType = CommandType.Text;
                    NpgsqlDataReader reader = command.ExecuteReader(CommandBehavior.Default);
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            command.Dispose();
                            reader.Close();
                            connection.Dispose();
                            connection.Close();
                            break;
                        }
                        string str = $"{reader["name"]}";
                        smethod_3(int.Parse($"{reader["id"]}"), str, int_0);
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }

        private static void smethod_3(int int_0, string string_0, int int_1)
        {
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command = connection.CreateCommand();
                    command.CommandText = $"SELECT * FROM system_shop_sets_items WHERE set_id = '{int_0}' AND set_name = '{string_0}';";
                    command.CommandType = CommandType.Text;
                    NpgsqlDataReader reader = command.ExecuteReader(CommandBehavior.Default);
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            command.Dispose();
                            reader.Close();
                            connection.Dispose();
                            connection.Close();
                            break;
                        }
                        int num = int.Parse($"{reader["id"]}");
                        string str = $"{reader["name"]}";
                        int num2 = int.Parse($"{reader["consume"]}");
                        uint num3 = uint.Parse($"{reader["count"]}");
                        int num4 = int.Parse($"{reader["price_gold"]}");
                        int num5 = int.Parse($"{reader["price_cash"]}");
                        GoodsItem item1 = new GoodsItem();
                        item1.Id = int_0;
                        item1.PriceGold = num4;
                        item1.PriceCash = num5;
                        item1.Tag = ItemTag.Hot;
                        item1.Title = 0;
                        item1.AuthType = 0;
                        item1.BuyType2 = 1;
                        item1.BuyType3 = (num2 == 1) ? 2 : 1;
                        GoodsItem local1 = item1;
                        local1.Visibility = 4;
                        GoodsItem item = local1;
                        item.Item.SetItemId(num);
                        item.Item.Name = str;
                        item.Item.Count = num3;
                        int num6 = ComDiv.GetIdStatics(item.Item.Id, 1);
                        if ((int_1 == 1) || ((int_1 == 2) && (num6 == 0x10)))
                        {
                            ShopAllList.Add(item);
                            if ((item.Visibility != 2) && (item.Visibility != 4))
                            {
                                ShopBuyableList.Add(item);
                            }
                            if (!ShopUniqueList.ContainsKey(item.Item.Id) && (item.AuthType > 0))
                            {
                                ShopUniqueList.Add(item.Item.Id, item);
                                if (item.Visibility == 4)
                                {
                                    Set4p++;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }

        private static void smethod_4(int int_0)
        {
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM system_shop_repair";
                    command.CommandType = CommandType.Text;
                    NpgsqlDataReader reader = command.ExecuteReader(CommandBehavior.Default);
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            command.Dispose();
                            reader.Close();
                            connection.Dispose();
                            connection.Close();
                            break;
                        }
                        ItemsRepair repair1 = new ItemsRepair();
                        repair1.Id = int.Parse($"{reader["item_id"]}");
                        repair1.Point = int.Parse($"{reader["price_gold"]}");
                        repair1.Cash = int.Parse($"{reader["price_cash"]}");
                        repair1.Quantity = uint.Parse($"{reader["quantity"]}");
                        repair1.Enable = bool.Parse($"{reader["repairable"]}");
                        ItemsRepair item = repair1;
                        if ((int_0 == 1) && (item.Enable && (item.Quantity <= 100)))
                        {
                            ItemRepairs.Add(item);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }

        private static void smethod_5(int int_0)
        {
            List<GoodsItem> list = new List<GoodsItem>();
            List<GoodsItem> list2 = new List<GoodsItem>();
            List<GoodsItem> shopAllList = ShopAllList;
            lock (shopAllList)
            {
                foreach (GoodsItem item in ShopAllList)
                {
                    if (item.Item.Count != 0)
                    {
                        if (((item.Tag != ItemTag.PcCafe) || (int_0 != 0)) && (((item.Tag == ItemTag.PcCafe) && (int_0 > 0)) || (item.Visibility != 2)))
                        {
                            list.Add(item);
                        }
                        if ((item.Visibility < 2) || (item.Visibility == 4))
                        {
                            list2.Add(item);
                        }
                    }
                }
            }
            TotalMatching1 = list.Count;
            TotalGoods = list2.Count;
            int num = (int) Math.Ceiling((double) (((double) list.Count) / 500.0));
            int num2 = 0;
            for (int i = 0; i < num; i++)
            {
                byte[] buffer = smethod_12(500, i, ref num2, list);
                ShopData data1 = new ShopData();
                data1.Buffer = buffer;
                data1.ItemsCount = num2;
                data1.Offset = i * 500;
                ShopData data = data1;
                ShopDataMt1.Add(data);
            }
            num = (int) Math.Ceiling((double) (((double) list2.Count) / 50.0));
            for (int j = 0; j < num; j++)
            {
                byte[] buffer2 = smethod_10(50, j, ref num2, list2);
                ShopData data3 = new ShopData();
                data3.Buffer = buffer2;
                data3.ItemsCount = num2;
                data3.Offset = j * 50;
                ShopData data2 = data3;
                ShopDataGoods.Add(data2);
            }
        }

        private static void smethod_6(int int_0)
        {
            List<GoodsItem> list = new List<GoodsItem>();
            List<GoodsItem> shopAllList = ShopAllList;
            lock (shopAllList)
            {
                foreach (GoodsItem item in ShopAllList)
                {
                    if (((item.Item.Count != 0) && ((item.Tag != ItemTag.PcCafe) || (int_0 != 0))) && (((item.Tag == ItemTag.PcCafe) && (int_0 > 0)) || (item.Visibility != 2)))
                    {
                        list.Add(item);
                    }
                }
            }
            TotalMatching2 = list.Count;
            int num = (int) Math.Ceiling((double) (((double) list.Count) / 500.0));
            int num2 = 0;
            for (int i = 0; i < num; i++)
            {
                byte[] buffer = smethod_12(500, i, ref num2, list);
                ShopData data1 = new ShopData();
                data1.Buffer = buffer;
                data1.ItemsCount = num2;
                data1.Offset = i * 500;
                ShopData data = data1;
                ShopDataMt2.Add(data);
            }
        }

        private static void smethod_7()
        {
            List<GoodsItem> list = new List<GoodsItem>();
            SortedList<int, GoodsItem> shopUniqueList = ShopUniqueList;
            lock (shopUniqueList)
            {
                foreach (GoodsItem item in ShopUniqueList.Values)
                {
                    if ((item.Visibility != 1) && (item.Visibility != 3))
                    {
                        list.Add(item);
                    }
                }
            }
            TotalItems = list.Count;
            int num = (int) Math.Ceiling((double) (((double) list.Count) / 800.0));
            int num2 = 0;
            for (int i = 0; i < num; i++)
            {
                byte[] buffer = smethod_9(800, i, ref num2, list);
                ShopData data1 = new ShopData();
                data1.Buffer = buffer;
                data1.ItemsCount = num2;
                data1.Offset = i * 800;
                ShopData data = data1;
                ShopDataItems.Add(data);
            }
        }

        private static void smethod_8()
        {
            List<ItemsRepair> list = new List<ItemsRepair>();
            List<ItemsRepair> list2 = ItemRepairs;
            lock (list2)
            {
                foreach (ItemsRepair repair in ItemRepairs)
                {
                    list.Add(repair);
                }
            }
            TotalRepairs = list.Count;
            int num = (int) Math.Ceiling((double) (((double) list.Count) / 100.0));
            int num2 = 0;
            for (int i = 0; i < num; i++)
            {
                byte[] buffer = smethod_11(100, i, ref num2, list);
                ShopData data1 = new ShopData();
                data1.Buffer = buffer;
                data1.ItemsCount = num2;
                data1.Offset = i * 100;
                ShopData item = data1;
                ShopDataItemRepairs.Add(item);
            }
        }

        private static byte[] smethod_9(int int_0, int int_1, ref int int_2, List<GoodsItem> list_0)
        {
            byte[] buffer;
            int_2 = 0;
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                int num = int_1 * int_0;
                while (true)
                {
                    if (num < list_0.Count)
                    {
                        smethod_13(list_0[num], packet);
                        int num2 = int_2 + 1;
                        int_2 = num2;
                        if (num2 != int_0)
                        {
                            num++;
                            continue;
                        }
                    }
                    buffer = packet.ToArray();
                    break;
                }
            }
            return buffer;
        }

        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> list, int limit)
        {
            Class14<T> class2 = new Class14<T> {
                int_0 = limit
            };
            System.Func<T, int, Class0<T, int>> selector = Class13<T>.<>9__16_0;
            if (Class13<T>.<>9__16_0 == null)
            {
                System.Func<T, int, Class0<T, int>> local1 = Class13<T>.<>9__16_0;
                selector = Class13<T>.<>9__16_0 = new System.Func<T, int, Class0<T, int>>(Class13<T>.<>9.method_0);
            }
            Func<IGrouping<int, Class0<T, int>>, IEnumerable<T>> func2 = Class13<T>.<>9__16_2;
            if (Class13<T>.<>9__16_2 == null)
            {
                Func<IGrouping<int, Class0<T, int>>, IEnumerable<T>> local2 = Class13<T>.<>9__16_2;
                func2 = Class13<T>.<>9__16_2 = new Func<IGrouping<int, Class0<T, int>>, IEnumerable<T>>(Class13<T>.<>9.method_1);
            }
            return list.Select<T, Class0<T, int>>(selector).GroupBy<Class0<T, int>, int>(new Func<Class0<T, int>, int>(class2.method_0)).Select<IGrouping<int, Class0<T, int>>, IEnumerable<T>>(func2);
        }

        [Serializable, CompilerGenerated]
        private sealed class Class13<T>
        {
            public static readonly ShopManager.Class13<T> <>9;
            public static System.Func<T, int, Class0<T, int>> <>9__16_0;
            public static Func<Class0<T, int>, T> <>9__16_3;
            public static Func<IGrouping<int, Class0<T, int>>, IEnumerable<T>> <>9__16_2;

            static Class13()
            {
                ShopManager.Class13<T>.<>9 = new ShopManager.Class13<T>();
            }

            internal Class0<T, int> method_0(T gparam_0, int int_0) => 
                new Class0<T, int>(gparam_0, int_0);

            internal IEnumerable<T> method_1(IGrouping<int, Class0<T, int>> igrouping_0)
            {
                Func<Class0<T, int>, T> selector = ShopManager.Class13<T>.<>9__16_3;
                if (ShopManager.Class13<T>.<>9__16_3 == null)
                {
                    Func<Class0<T, int>, T> local1 = ShopManager.Class13<T>.<>9__16_3;
                    selector = ShopManager.Class13<T>.<>9__16_3 = new Func<Class0<T, int>, T>(ShopManager.Class13<T>.<>9.method_2);
                }
                return igrouping_0.Select<Class0<T, int>, T>(selector);
            }

            internal T method_2(Class0<T, int> class0_0) => 
                class0_0.item;
        }

        [CompilerGenerated]
        private sealed class Class14<T>
        {
            public int int_0;

            internal int method_0(Class0<T, int> class0_0) => 
                class0_0.inx / this.int_0;
        }
    }
}

