using Npgsql;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Managers
{
    /// <summary>
    /// Gestor de la tienda del juego - maneja items, precios, reparaciones y efectos
    /// </summary>
    public static class ShopManager
    {
        #region Public Static Collections

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

        #endregion

        #region Public Static Counters

        public static int TotalGoods;
        public static int TotalItems;
        public static int TotalMatching1;
        public static int TotalMatching2;
        public static int TotalRepairs;
        public static int Set4p;

        #endregion

        #region Utility Methods

        /// <summary>
        /// Divide una lista en sublistas de tamaño específico
        /// </summary>
        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> list, int limit)
        {
            return list
                .Select((item, index) => new { item, index })
                .GroupBy(x => x.index / limit)
                .Select(group => group.Select(x => x.item));
        }

        #endregion

        #region Main Load Method

        /// <summary>
        /// Carga todos los datos de la tienda desde la base de datos
        /// </summary>
        /// <param name="Type">1 = carga completa, 2 = solo items tipo 16</param>

        public static void Load(int Type)
        {
            LoadRepairableItems(Type);
            LoadShopItems(Type);
            LoadShopEffects(Type);
            LoadShopSets(Type);

            if (Type != 1)
                return;

            try
            {
                BuildMatchingAndGoodsData(0);
                BuildMatchingData2(1);
                BuildUniqueItemsData();
                BuildRepairItemsData();
                BuildShopTagData();
            }
            catch (Exception ex)
            {
                CLogger.Print(ex.Message, LoggerType.Error, ex);
            }

            CLogger.Print($"Plugin Loaded: {ShopBuyableList.Count} Buyable Items", LoggerType.Info);
            CLogger.Print($"Plugin Loaded: {ItemRepairs.Count} Repairable Items", LoggerType.Info);
        }

        #endregion

        #region Database Loading Methods

        /// <summary>
        /// Carga items de la tienda desde system_shop
        /// </summary>

        private static void LoadShopItems(int loadType)
        {
            try
            {
                using (NpgsqlConnection conn = ConnectionSQL.GetInstance().Conn())
                {
                    conn.Open();
                    NpgsqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "SELECT * FROM system_shop";
                    cmd.CommandType = CommandType.Text;

                    NpgsqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                    while (reader.Read())
                    {
                        int itemId = int.Parse($"{reader["item_id"]}");

                        // Parsear listas de cantidades, precios en cash y gold
                        string[] countList = ParseCommaSeparatedValue($"{reader["item_count_list"]}");
                        string[] cashPriceList = ParseCommaSeparatedValue($"{reader["price_cash_list"]}");
                        string[] goldPriceList = ParseCommaSeparatedValue($"{reader["price_gold_list"]}");

                        if (countList.Length != cashPriceList.Length || cashPriceList.Length != goldPriceList.Length)
                        {
                            CLogger.Print($"Loading goods with invalid counts / moneys / points sizes. ({itemId})", LoggerType.Warning);
                            continue;
                        }

                        int variantIndex = 0;
                        foreach (string countStr in countList)
                        {
                            variantIndex++;

                            if (!uint.TryParse(countStr, out uint itemCount))
                            {
                                CLogger.Print($"Loading goods with count != UInt ({itemId})", LoggerType.Warning);
                                continue;
                            }

                            if (!int.TryParse(cashPriceList[variantIndex - 1], out int cashPrice))
                            {
                                CLogger.Print($"Loading goods with cash != Int ({itemId})", LoggerType.Warning);
                                continue;
                            }

                            if (!int.TryParse(goldPriceList[variantIndex - 1], out int goldPrice))
                            {
                                CLogger.Print($"Loading goods with gold != Int ({itemId})", LoggerType.Warning);
                                continue;
                            }

                            int itemCategory = ComDiv.GetIdStatics(itemId, 1);
                            string itemName = $"{reader["item_name"]}";

                            // Generar ID único del good (item + variante)
                            bool isSpecialCategory = itemCategory == 22 || itemCategory == 26 ||
                                                     itemCategory == 36 || itemCategory == 37 || itemCategory == 40;
                            int goodId = int.Parse($"{itemId}{(isSpecialCategory ? "00" : $"{variantIndex:D2}")}");

                            GoodsItem good = new GoodsItem()
                            {
                                Id = goodId,
                                PriceGold = goldPrice,
                                PriceCash = cashPrice
                            };

                            // Aplicar descuento si existe
                            int discountPercent = int.Parse($"{reader["discount_percent"]}");
                            if (discountPercent > 0 && good.PriceCash > 0)
                            {
                                good.StarCash = good.PriceCash * 255; // Precio original
                                good.PriceCash = ComDiv.Percentage(good.PriceCash, discountPercent);
                            }
                            if (discountPercent > 0 && good.PriceGold > 0)
                            {
                                good.StarGold = good.PriceGold * 255;
                                good.PriceGold = ComDiv.Percentage(good.PriceGold, discountPercent);
                            }

                            good.Tag = discountPercent > 0 ? ItemTag.Sale : (ItemTag)int.Parse($"{reader["shop_tag"]}");
                            good.Title = int.Parse($"{reader["title_requi"]}");
                            good.AuthType = int.Parse($"{reader["item_consume"]}");
                            good.BuyType2 = good.AuthType == 2 ? 1 : (IsRepairableItem(itemId) ? 2 : 1);
                            good.BuyType3 = good.AuthType == 1 ? 2 : 1;
                            good.Visibility = bool.Parse($"{reader["item_visible"]}") ? 0 : 4;

                            good.Item.SetItemId(itemId);
                            good.Item.Name = good.AuthType == 1
                                ? $"{itemName} ({itemCount} qty)"
                                : (good.AuthType == 2 ? $"{itemName} ({itemCount / 3600U} hours)" : itemName);
                            good.Item.Count = itemCount;

                            int itemType = ComDiv.GetIdStatics(good.Item.Id, 1);

                            switch (loadType)
                            {
                                case 1:
                                    ShopAllList.Add(good);
                                    if (good.Visibility != 2 && good.Visibility != 4)
                                        ShopBuyableList.Add(good);
                                    if (!ShopUniqueList.ContainsKey(good.Item.Id) && good.AuthType > 0)
                                    {
                                        ShopUniqueList.Add(good.Item.Id, good);
                                        if (good.Visibility == 4)
                                            Set4p++;
                                    }
                                    break;
                                case 2:
                                    if (itemType == 16)
                                        goto case 1;
                                    break;
                            }
                        }
                    }

                    cmd.Dispose();
                    reader.Close();
                    conn.Dispose();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                CLogger.Print(ex.Message, LoggerType.Error, ex);
            }
        }

        /// <summary>
        /// Carga efectos/cupones de la tienda desde system_shop_effects
        /// </summary>

        private static void LoadShopEffects(int loadType)
        {
            try
            {
                using (NpgsqlConnection conn = ConnectionSQL.GetInstance().Conn())
                {
                    conn.Open();
                    NpgsqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "SELECT * FROM system_shop_effects";
                    cmd.CommandType = CommandType.Text;

                    NpgsqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                    while (reader.Read())
                    {
                        int couponId = int.Parse($"{reader["coupon_id"]}");

                        string[] dayCountList = ParseCommaSeparatedValue($"{reader["coupon_count_day_list"]}");
                        string[] cashPriceList = ParseCommaSeparatedValue($"{reader["price_cash_list"]}");
                        string[] goldPriceList = ParseCommaSeparatedValue($"{reader["price_gold_list"]}");

                        if (dayCountList.Length != cashPriceList.Length || cashPriceList.Length != goldPriceList.Length)
                        {
                            CLogger.Print($"Loading goods with invalid counts / moneys / points sizes. ({couponId})", LoggerType.Warning);
                            continue;
                        }

                        int variantIndex = 0;
                        foreach (string dayStr in dayCountList)
                        {
                            variantIndex++;

                            if (!int.TryParse(dayStr, out int dayCount))
                            {
                                CLogger.Print($"Loading effects with count != Int ({couponId})", LoggerType.Warning);
                                continue;
                            }

                            if (!int.TryParse(cashPriceList[variantIndex - 1], out int cashPrice))
                            {
                                CLogger.Print($"Loading effects with cash != Int ({couponId})", LoggerType.Warning);
                                continue;
                            }

                            if (!int.TryParse(goldPriceList[variantIndex - 1], out int goldPrice))
                            {
                                CLogger.Print($"Loading effects with gold != Int ({couponId})", LoggerType.Warning);
                                continue;
                            }

                            // Limitar días a máximo 100
                            if (dayCount >= 100)
                                dayCount = 100;

                            // Construir ID del item basado en el coupon ID y días
                            string couponIdStr = $"{couponId}";
                            int itemId = int.Parse($"{couponIdStr.Substring(0, 2)}{dayCount:D2}{couponIdStr.Substring(4, 3)}");

                            GoodsItem good = new GoodsItem()
                            {
                                Id = int.Parse($"{couponId}{variantIndex:D2}"),
                                PriceGold = goldPrice,
                                PriceCash = cashPrice
                            };

                            int discountPercent = int.Parse($"{reader["discount_percent"]}");
                            if (discountPercent > 0 && good.PriceCash > 0)
                            {
                                good.StarCash = good.PriceCash * 255;
                                good.PriceCash = ComDiv.Percentage(good.PriceCash, discountPercent);
                            }
                            if (discountPercent > 0 && good.PriceGold > 0)
                            {
                                good.PriceGold *= 255;
                                good.PriceGold = ComDiv.Percentage(good.PriceGold, discountPercent);
                            }

                            good.Tag = discountPercent > 0 ? ItemTag.Sale : (ItemTag)int.Parse($"{reader["shop_tag"]}");
                            good.Title = 0;
                            good.AuthType = 1;
                            good.BuyType2 = 1;
                            good.BuyType3 = 2;
                            good.Visibility = bool.Parse($"{reader["coupon_visible"]}") ? 0 : 4;

                            good.Item.SetItemId(itemId);
                            good.Item.Name = $"{reader["coupon_name"]} ({dayCount} days)";
                            good.Item.Count = 1U;

                            int itemCategory = ComDiv.GetIdStatics(good.Item.Id, 1);

                            switch (loadType)
                            {
                                case 1:
                                    ShopAllList.Add(good);
                                    if (good.Visibility != 2 && good.Visibility != 4)
                                        ShopBuyableList.Add(good);
                                    if (!ShopUniqueList.ContainsKey(good.Item.Id) && good.AuthType > 0)
                                    {
                                        ShopUniqueList.Add(good.Item.Id, good);
                                        if (good.Visibility == 4)
                                            Set4p++;
                                    }
                                    break;
                                case 2:
                                    if (itemCategory == 16)
                                        goto case 1;
                                    break;
                            }
                        }
                    }

                    cmd.Dispose();
                    reader.Close();
                    conn.Dispose();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                CLogger.Print(ex.Message, LoggerType.Error, ex);
            }
        }

        /// <summary>
        /// Carga sets/paquetes de items desde system_shop_sets
        /// </summary>

        private static void LoadShopSets(int loadType)
        {
            try
            {
                using (NpgsqlConnection conn = ConnectionSQL.GetInstance().Conn())
                {
                    conn.Open();
                    NpgsqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = $"SELECT * FROM system_shop_sets WHERE visible = '{true}';";
                    cmd.CommandType = CommandType.Text;

                    NpgsqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                    while (reader.Read())
                    {
                        LoadShopSetItems(
                            int.Parse($"{reader["id"]}"),
                            $"{reader["name"]}",
                            loadType
                        );
                    }

                    cmd.Dispose();
                    reader.Close();
                    conn.Dispose();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                CLogger.Print(ex.Message, LoggerType.Error, ex);
            }
        }

        /// <summary>
        /// Carga items individuales de un set
        /// </summary>

        private static void LoadShopSetItems(int setId, string setName, int loadType)
        {
            try
            {
                using (NpgsqlConnection conn = ConnectionSQL.GetInstance().Conn())
                {
                    conn.Open();
                    NpgsqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = $"SELECT * FROM system_shop_sets_items WHERE set_id = '{setId}' AND set_name = '{setName}';";
                    cmd.CommandType = CommandType.Text;

                    NpgsqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                    while (reader.Read())
                    {
                        int itemId = int.Parse($"{reader["id"]}");
                        string itemName = $"{reader["name"]}";
                        int consumeType = int.Parse($"{reader["consume"]}");
                        uint itemCount = uint.Parse($"{reader["count"]}");
                        int goldPrice = int.Parse($"{reader["price_gold"]}");
                        int cashPrice = int.Parse($"{reader["price_cash"]}");

                        GoodsItem good = new GoodsItem()
                        {
                            Id = setId,
                            PriceGold = goldPrice,
                            PriceCash = cashPrice,
                            Tag = ItemTag.Hot,
                            Title = 0,
                            AuthType = 0,
                            BuyType2 = 1,
                            BuyType3 = consumeType == 1 ? 2 : 1,
                            Visibility = 4
                        };

                        good.Item.SetItemId(itemId);
                        good.Item.Name = itemName;
                        good.Item.Count = itemCount;

                        int itemCategory = ComDiv.GetIdStatics(good.Item.Id, 1);

                        switch (loadType)
                        {
                            case 1:
                                ShopAllList.Add(good);
                                if (good.Visibility != 2 && good.Visibility != 4)
                                    ShopBuyableList.Add(good);
                                if (!ShopUniqueList.ContainsKey(good.Item.Id) && good.AuthType > 0)
                                {
                                    ShopUniqueList.Add(good.Item.Id, good);
                                    if (good.Visibility == 4)
                                        Set4p++;
                                }
                                break;
                            case 2:
                                if (itemCategory == 16)
                                    goto case 1;
                                break;
                        }
                    }

                    cmd.Dispose();
                    reader.Close();
                    conn.Dispose();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                CLogger.Print(ex.Message, LoggerType.Error, ex);
            }
        }

        /// <summary>
        /// Carga items reparables desde system_shop_repair
        /// </summary>

        private static void LoadRepairableItems(int loadType)
        {
            try
            {
                using (NpgsqlConnection conn = ConnectionSQL.GetInstance().Conn())
                {
                    conn.Open();
                    NpgsqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "SELECT * FROM system_shop_repair";
                    cmd.CommandType = CommandType.Text;

                    NpgsqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                    while (reader.Read())
                    {
                        ItemsRepair repairItem = new ItemsRepair()
                        {
                            Id = int.Parse($"{reader["item_id"]}"),
                            Point = int.Parse($"{reader["price_gold"]}"),
                            Cash = int.Parse($"{reader["price_cash"]}"),
                            Quantity = uint.Parse($"{reader["quantity"]}"),
                            Enable = bool.Parse($"{reader["repairable"]}")
                        };

                        if (loadType == 1 && repairItem.Enable && repairItem.Quantity <= 100U)
                            ItemRepairs.Add(repairItem);
                    }

                    cmd.Dispose();
                    reader.Close();
                    conn.Dispose();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                CLogger.Print(ex.Message, LoggerType.Error, ex);
            }
        }

        #endregion

        #region Data Building Methods

        /// <summary>
        /// Construye datos de matching y goods para enviar al cliente
        /// </summary>
        private static void BuildMatchingAndGoodsData(int pcCafeFilter)
        {
            List<GoodsItem> matchingList = new List<GoodsItem>();
            List<GoodsItem> goodsList = new List<GoodsItem>();

            lock (ShopAllList)
            {
                foreach (GoodsItem item in ShopAllList)
                {
                    if (item.Item.Count == 0U)
                        continue;

                    // Filtro de matching
                    if ((item.Tag != ItemTag.PcCafe || pcCafeFilter != 0) &&
                        (item.Tag == ItemTag.PcCafe && pcCafeFilter > 0 || item.Visibility != 2))
                        matchingList.Add(item);

                    // Filtro de goods
                    if (item.Visibility < 2 || item.Visibility == 4)
                        goodsList.Add(item);
                }
            }

            TotalMatching1 = matchingList.Count;
            TotalGoods = goodsList.Count;

            // Serializar matching en paquetes de 500 items
            int matchingPages = (int)Math.Ceiling((double)matchingList.Count / 500.0);
            for (int page = 0; page < matchingPages; page++)
            {
                int itemsWritten = 0;
                byte[] buffer = SerializeMatchingItems(500, page, ref itemsWritten, matchingList);

                ShopDataMt1.Add(new ShopData()
                {
                    Buffer = buffer,
                    ItemsCount = itemsWritten,
                    Offset = page * 500
                });
            }

            // Serializar goods en paquetes de 50 items
            int goodsPages = (int)Math.Ceiling((double)goodsList.Count / 50.0);
            for (int page = 0; page < goodsPages; page++)
            {
                int itemsWritten = 0;
                byte[] buffer = SerializeGoodsItems(50, page, ref itemsWritten, goodsList);

                ShopDataGoods.Add(new ShopData()
                {
                    Buffer = buffer,
                    ItemsCount = itemsWritten,
                    Offset = page * 50
                });
            }
        }

        /// <summary>
        /// Construye datos de matching 2
        /// </summary>
        private static void BuildMatchingData2(int pcCafeFilter)
        {
            List<GoodsItem> matchingList = new List<GoodsItem>();

            lock (ShopAllList)
            {
                foreach (GoodsItem item in ShopAllList)
                {
                    if (item.Item.Count != 0U &&
                        (item.Tag != ItemTag.PcCafe || pcCafeFilter != 0) &&
                        (item.Tag == ItemTag.PcCafe && pcCafeFilter > 0 || item.Visibility != 2))
                        matchingList.Add(item);
                }
            }

            TotalMatching2 = matchingList.Count;

            int pages = (int)Math.Ceiling((double)matchingList.Count / 500.0);
            for (int page = 0; page < pages; page++)
            {
                int itemsWritten = 0;
                byte[] buffer = SerializeMatchingItems(500, page, ref itemsWritten, matchingList);

                ShopDataMt2.Add(new ShopData()
                {
                    Buffer = buffer,
                    ItemsCount = itemsWritten,
                    Offset = page * 500
                });
            }
        }

        /// <summary>
        /// Construye datos de items únicos
        /// </summary>
        private static void BuildUniqueItemsData()
        {
            List<GoodsItem> uniqueList = new List<GoodsItem>();

            lock (ShopUniqueList)
            {
                foreach (GoodsItem item in ShopUniqueList.Values)
                {
                    if (item.Visibility != 1 && item.Visibility != 3)
                        uniqueList.Add(item);
                }
            }

            TotalItems = uniqueList.Count;

            int pages = (int)Math.Ceiling((double)uniqueList.Count / 800.0);
            for (int page = 0; page < pages; page++)
            {
                int itemsWritten = 0;
                byte[] buffer = SerializeUniqueItems(800, page, ref itemsWritten, uniqueList);

                ShopDataItems.Add(new ShopData()
                {
                    Buffer = buffer,
                    ItemsCount = itemsWritten,
                    Offset = page * 800
                });
            }
        }

        /// <summary>
        /// Construye datos de items reparables
        /// </summary>
        private static void BuildRepairItemsData()
        {
            List<ItemsRepair> repairList = new List<ItemsRepair>();

            lock (ItemRepairs)
            {
                foreach (ItemsRepair item in ItemRepairs)
                    repairList.Add(item);
            }

            TotalRepairs = repairList.Count;

            int pages = (int)Math.Ceiling((double)repairList.Count / 100.0);
            for (int page = 0; page < pages; page++)
            {
                int itemsWritten = 0;
                byte[] buffer = SerializeRepairItems(100, page, ref itemsWritten, repairList);

                ShopDataItemRepairs.Add(new ShopData()
                {
                    Buffer = buffer,
                    ItemsCount = itemsWritten,
                    Offset = page * 100
                });
            }
        }

        /// <summary>
        /// Construye datos de tag de tienda
        /// </summary>

        private static void BuildShopTagData()
        {
            string tagText = "zOne";
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                packet.WriteS(tagText, tagText.Length + 1);
                ShopTagData = packet.ToArray();
            }
        }

        #endregion

        #region Serialization Methods

        /// <summary>
        /// Serializa items únicos para enviar al cliente
        /// </summary>
        private static byte[] SerializeUniqueItems(int itemsPerPage, int pageIndex, ref int itemsWritten, List<GoodsItem> itemList)
        {
            itemsWritten = 0;
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                for (int i = pageIndex * itemsPerPage; i < itemList.Count; i++)
                {
                    WriteItemDataShort(itemList[i], packet);
                    if (++itemsWritten == itemsPerPage)
                        break;
                }
                return packet.ToArray();
            }
        }

        /// <summary>
        /// Serializa goods items
        /// </summary>
        private static byte[] SerializeGoodsItems(int itemsPerPage, int pageIndex, ref int itemsWritten, List<GoodsItem> itemList)
        {
            itemsWritten = 0;
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                for (int i = pageIndex * itemsPerPage; i < itemList.Count; i++)
                {
                    WriteGoodsItemData(itemList[i], packet);
                    if (++itemsWritten == itemsPerPage)
                        break;
                }
                return packet.ToArray();
            }
        }

        /// <summary>
        /// Serializa items de reparación
        /// </summary>
        private static byte[] SerializeRepairItems(int itemsPerPage, int pageIndex, ref int itemsWritten, List<ItemsRepair> repairList)
        {
            itemsWritten = 0;
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                for (int i = pageIndex * itemsPerPage; i < repairList.Count; i++)
                {
                    WriteRepairItemData(repairList[i], packet);
                    if (++itemsWritten == itemsPerPage)
                        break;
                }
                return packet.ToArray();
            }
        }

        /// <summary>
        /// Serializa matching items
        /// </summary>
        private static byte[] SerializeMatchingItems(int itemsPerPage, int pageIndex, ref int itemsWritten, List<GoodsItem> itemList)
        {
            itemsWritten = 0;
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                for (int i = pageIndex * itemsPerPage; i < itemList.Count; i++)
                {
                    WriteMatchingItemData(itemList[i], packet);
                    if (++itemsWritten == itemsPerPage)
                        break;
                }
                return packet.ToArray();
            }
        }

        #endregion

        #region Packet Writing Methods

        /// <summary>
        /// Escribe datos cortos de item (para lista única)
        /// </summary>
        private static void WriteItemDataShort(GoodsItem item, SyncServerPacket packet)
        {
            packet.WriteD(item.Item.Id);
            packet.WriteC((byte)item.AuthType);
            packet.WriteC((byte)item.BuyType2);
            packet.WriteC((byte)item.BuyType3);
            packet.WriteC((byte)item.Title);
            packet.WriteC(item.Title != 0 ? (byte)2 : (byte)0);
            packet.WriteH((short)0);
        }

        /// <summary>
        /// Escribe datos completos de good
        /// </summary>
        private static void WriteGoodsItemData(GoodsItem item, SyncServerPacket packet)
        {
            packet.WriteD(item.Id);
            packet.WriteC((byte)1);
            packet.WriteC(item.Visibility == 4 ? (byte)4 : (byte)1);
            packet.WriteD(item.PriceGold);
            packet.WriteD(item.PriceCash);
            packet.WriteD(0);
            packet.WriteC((byte)item.Tag);
            packet.WriteC((byte)0);
            packet.WriteC((byte)0);
            packet.WriteC((byte)0);
            packet.WriteD(item.StarCash > 0 ? item.StarCash : (item.StarGold > 0 ? item.StarGold : 0));
            packet.WriteD(0);
            packet.WriteD(0);
            packet.WriteD(0);
            packet.WriteB(new byte[98]); // Padding
        }

        /// <summary>
        /// Escribe datos de item reparable
        /// </summary>
        private static void WriteRepairItemData(ItemsRepair item, SyncServerPacket packet)
        {
            packet.WriteD(item.Id);
            packet.WriteD((int)((double)item.Point / (double)item.Quantity));
            packet.WriteD((int)((double)item.Cash / (double)item.Quantity));
            packet.WriteD(item.Quantity);
        }

        /// <summary>
        /// Escribe datos de matching item
        /// </summary>
        private static void WriteMatchingItemData(GoodsItem item, SyncServerPacket packet)
        {
            packet.WriteD(item.Id);
            packet.WriteD(item.Item.Id);
            packet.WriteD(item.Item.Count);
            packet.WriteD(0);
        }

        #endregion

        #region Public Utility Methods

        /// <summary>
        /// Reinicia todas las listas y contadores
        /// </summary>
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

        /// <summary>
        /// Verifica si un item es reparable
        /// </summary>
        public static bool IsRepairableItem(int ItemId) => GetRepairItem(ItemId) != null;

        /// <summary>
        /// Obtiene un item reparable por ID
        /// </summary>
        public static ItemsRepair GetRepairItem(int ItemId)
        {
            if (ItemId == 0)
                return null;

            lock (ItemRepairs)
            {
                foreach (ItemsRepair repair in ItemRepairs)
                {
                    if (repair.Id == ItemId)
                        return repair;
                }
            }
            return null;
        }

        /// <summary>
        /// Busca items bloqueados por texto
        /// </summary>
        public static bool IsBlocked(string Text, List<int> Items)
        {
            lock (ShopUniqueList)
            {
                foreach (GoodsItem item in ShopUniqueList.Values)
                {
                    if (!Items.Contains(item.Item.Id) && item.Item.Name.Contains(Text))
                        Items.Add(item.Item.Id);
                }
            }
            return false;
        }

        /// <summary>
        /// Obtiene un good por ID
        /// </summary>
        public static GoodsItem GetGood(int GoodId)
        {
            if (GoodId == 0)
                return null;

            lock (ShopAllList)
            {
                foreach (GoodsItem item in ShopAllList)
                {
                    if (item.Id == GoodId)
                        return item;
                }
            }
            return null;
        }

        /// <summary>
        /// Obtiene un good por Item ID
        /// </summary>
        public static GoodsItem GetItemId(int ItemId)
        {
            if (ItemId == 0)
                return null;

            lock (ShopAllList)
            {
                foreach (GoodsItem item in ShopAllList)
                {
                    if (item.Item.Id == ItemId)
                        return item;
                }
            }
            return null;
        }

        /// <summary>
        /// Obtiene goods del carrito de compras y calcula precios totales
        /// </summary>
        public static List<GoodsItem> GetGoods(
            List<CartGoods> ShopCart,
            out int GoldPrice,
            out int CashPrice,
            out int TagsPrice)
        {
            GoldPrice = 0;
            CashPrice = 0;
            TagsPrice = 0;

            List<GoodsItem> purchasedGoods = new List<GoodsItem>();

            if (ShopCart.Count == 0)
                return purchasedGoods;

            lock (ShopBuyableList)
            {
                foreach (GoodsItem shopItem in ShopBuyableList)
                {
                    foreach (CartGoods cartItem in ShopCart)
                    {
                        if (cartItem.GoodId == shopItem.Id)
                        {
                            purchasedGoods.Add(shopItem);

                            if (cartItem.BuyType == 1)
                                GoldPrice += shopItem.PriceGold;
                            else if (cartItem.BuyType == 2)
                                CashPrice += shopItem.PriceCash;
                        }
                    }
                }
            }

            return purchasedGoods;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Parsea valores separados por comas
        /// </summary>
        private static string[] ParseCommaSeparatedValue(string value)
        {
            if (!value.Contains(","))
                return new string[] { value };
            else
                return value.Split(',');
        }

        #endregion
    }
}