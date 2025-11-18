// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Managers.ShopManager
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Npgsql;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Models.Map;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Managers;

public static class ShopManager
{
  public static List<ItemsRepair> ItemRepairs;
  public static List<GoodsItem> ShopAllList;
  public static List<GoodsItem> ShopBuyableList;
  public static SortedList<int, GoodsItem> ShopUniqueList;
  public static List<ShopData> ShopDataMt1;
  public static List<ShopData> ShopDataMt2;
  public static List<ShopData> ShopDataGoods;
  public static List<ShopData> ShopDataItems;
  public static List<ShopData> ShopDataItemRepairs;
  public static byte[] ShopTagData;
  public static int TotalGoods;
  public static int TotalItems;
  public static int TotalMatching1;
  public static int TotalMatching2;
  public static int TotalRepairs;
  public static int Set4p;

  public static byte[] InitRankUpData([In] EventRankUpModel obj0)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      syncServerPacket.WriteC((byte) ((EventVisitModel) obj0).get_Ranks().Count);
      foreach (int[] numArray in ((EventVisitModel) obj0).get_Ranks())
      {
        syncServerPacket.WriteD(numArray[0]);
        syncServerPacket.WriteD(ComDiv.Percentage(numArray[1], numArray[3]));
        syncServerPacket.WriteD(ComDiv.Percentage(numArray[2], numArray[3]));
      }
      return syncServerPacket.ToArray();
    }
  }

  public static byte[] InitPlaytimeData([In] EventPlaytimeModel obj0)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      syncServerPacket.WriteD(obj0.Minutes1 * 60);
      syncServerPacket.WriteD(obj0.Minutes2 * 60);
      syncServerPacket.WriteD(obj0.Minutes3 * 60);
      foreach (int num in obj0.Goods1)
        syncServerPacket.WriteD(num);
      syncServerPacket.WriteB(new byte[(20 - obj0.Goods1.Count) * 4]);
      foreach (int num in obj0.Goods2)
        syncServerPacket.WriteD(num);
      syncServerPacket.WriteB(new byte[(20 - obj0.Goods2.Count) * 4]);
      foreach (int num in ((EventQuestModel) obj0).get_Goods3())
        syncServerPacket.WriteD(num);
      syncServerPacket.WriteB(new byte[(20 - ((EventQuestModel) obj0).get_Goods3().Count) * 4]);
      return syncServerPacket.ToArray();
    }
  }

  public static byte[] InitBoostData([In] EventBoostModel obj0)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      syncServerPacket.WriteH((ushort) obj0.BoostType);
      syncServerPacket.WriteD(((EventPlaytimeModel) obj0).get_BoostValue());
      syncServerPacket.WriteD(ComDiv.Percentage(obj0.BonusExp, obj0.Percent));
      syncServerPacket.WriteD(ComDiv.Percentage(obj0.BonusGold, obj0.Percent));
      return syncServerPacket.ToArray();
    }
  }

  public static byte[] InitLoginData([In] EventLoginModel obj0)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      syncServerPacket.WriteC((byte) 1);
      syncServerPacket.WriteC(((EventBoostModel) obj0).get_Goods().Count > 4 ? (byte) 4 : (byte) ((EventBoostModel) obj0).get_Goods().Count);
      for (int index = 0; index < 4; ++index)
      {
        if (((EventBoostModel) obj0).get_Goods().Count >= index + 1)
          syncServerPacket.WriteD(((EventBoostModel) obj0).get_Goods()[index]);
        else
          syncServerPacket.WriteD(0);
      }
      return syncServerPacket.ToArray();
    }
  }

  static ShopManager() => PortalManager.AllEvents = new SortedList<string, PortalEvents>();

  public static IEnumerable<IEnumerable<T>> Split<T>([In] this IEnumerable<T> obj0, [In] int obj1)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: reference to a compiler-generated method
    return obj0.Select<T, Class0<T, int>>((Func<T, int, Class0<T, int>>) delegate); // Unable to render the statement
  }

  public static void Load(int Playtime)
  {
    ShopManager.smethod_4(Playtime);
    ShopManager.smethod_0(Playtime);
    ShopManager.smethod_1(Playtime);
    ShopManager.smethod_2(Playtime);
    if (Playtime != 1)
      return;
    try
    {
      ShopManager.smethod_5(0);
      ShopManager.smethod_6(1);
      ShopManager.smethod_7();
      ShopManager.smethod_8();
      // ISSUE: reference to a compiler-generated method
      ShopManager.Class13<>.smethod_17();
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
    }
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Plugin Loaded: {ShopManager.ShopBuyableList.Count} Buyable Items", LoggerType.Info, (Exception) null);
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Plugin Loaded: {ShopManager.ItemRepairs.Count} Repairable Items", LoggerType.Info, (Exception) null);
  }

  private static void smethod_0(int Boost)
  {
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        ((DbConnection) npgsqlConnection).Open();
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbCommand) command).CommandText = "SELECT * FROM system_shop";
        ((DbCommand) command).CommandType = CommandType.Text;
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        while (((DbDataReader) npgsqlDataReader).Read())
        {
          int num1 = int.Parse($"{((DbDataReader) npgsqlDataReader)["item_id"]}");
          string[] strArray1;
          if (!$"{((DbDataReader) npgsqlDataReader)["item_count_list"]}".Contains(","))
            strArray1 = new string[1]
            {
              $"{((DbDataReader) npgsqlDataReader)["item_count_list"]}"
            };
          else
            strArray1 = $"{((DbDataReader) npgsqlDataReader)["item_count_list"]}".Split(',');
          string[] strArray2 = strArray1;
          string[] strArray3;
          if (!$"{((DbDataReader) npgsqlDataReader)["price_cash_list"]}".Contains(","))
            strArray3 = new string[1]
            {
              $"{((DbDataReader) npgsqlDataReader)["price_cash_list"]}"
            };
          else
            strArray3 = $"{((DbDataReader) npgsqlDataReader)["price_cash_list"]}".Split(',');
          string[] strArray4 = strArray3;
          string[] strArray5;
          if (!$"{((DbDataReader) npgsqlDataReader)["price_gold_list"]}".Contains(","))
            strArray5 = new string[1]
            {
              $"{((DbDataReader) npgsqlDataReader)["price_gold_list"]}"
            };
          else
            strArray5 = $"{((DbDataReader) npgsqlDataReader)["price_gold_list"]}".Split(',');
          string[] strArray6 = strArray5;
          if (strArray2.Length == strArray4.Length && strArray4.Length == strArray6.Length)
          {
            int num2 = 0;
            foreach (string s in strArray2)
            {
              ++num2;
              uint num3;
              ref uint local = ref num3;
              if (!uint.TryParse(s, out local))
              {
                // ISSUE: reference to a compiler-generated method
                CLogger.Class1.Print($"Loading goods with count != UInt ({num1})", LoggerType.Warning, (Exception) null);
              }
              else
              {
                int result1;
                if (!int.TryParse(strArray4[num2 - 1], out result1))
                {
                  // ISSUE: reference to a compiler-generated method
                  CLogger.Class1.Print($"Loading goods with cash != Int ({num1})", LoggerType.Warning, (Exception) null);
                }
                else
                {
                  int result2;
                  if (!int.TryParse(strArray6[num2 - 1], out result2))
                  {
                    // ISSUE: reference to a compiler-generated method
                    CLogger.Class1.Print($"Loading goods with gold != Int ({num1})", LoggerType.Warning, (Exception) null);
                  }
                  else
                  {
                    int idStatics1 = ComDiv.GetIdStatics(num1, 1);
                    string str = $"{((DbDataReader) npgsqlDataReader)["item_name"]}";
                    ItemsRepair itemsRepair = new ItemsRepair();
                    ((GoodsItem) itemsRepair).Id = int.Parse($"{num1}{(idStatics1 == 22 || idStatics1 == 26 || idStatics1 == 36 || idStatics1 == 37 || idStatics1 == 40 ? (object) "00" : (object) $"{num2:D2}")}");
                    ((GoodsItem) itemsRepair).PriceGold = result2;
                    ((GoodsItem) itemsRepair).PriceCash = result1;
                    GoodsItem goodsItem = (GoodsItem) itemsRepair;
                    int num4 = int.Parse($"{((DbDataReader) npgsqlDataReader)["discount_percent"]}");
                    if (num4 > 0 && goodsItem.PriceCash > 0)
                    {
                      goodsItem.StarCash = goodsItem.PriceCash * (int) byte.MaxValue;
                      goodsItem.PriceCash = ComDiv.Percentage(goodsItem.PriceCash, num4);
                    }
                    if (num4 > 0 && goodsItem.PriceGold > 0)
                    {
                      goodsItem.StarGold = goodsItem.PriceGold * (int) byte.MaxValue;
                      goodsItem.PriceGold = ComDiv.Percentage(goodsItem.PriceGold, num4);
                    }
                    ((ItemsRepair) goodsItem).set_Tag(num4 > 0 ? ItemTag.Sale : (ItemTag) int.Parse($"{((DbDataReader) npgsqlDataReader)["shop_tag"]}"));
                    goodsItem.Title = int.Parse($"{((DbDataReader) npgsqlDataReader)["title_requi"]}");
                    goodsItem.AuthType = int.Parse($"{((DbDataReader) npgsqlDataReader)["item_consume"]}");
                    goodsItem.BuyType2 = goodsItem.AuthType == 2 ? 1 : (ShopManager.IsRepairableItem(num1) ? 2 : 1);
                    goodsItem.BuyType3 = goodsItem.AuthType == 1 ? 2 : 1;
                    goodsItem.Visibility = bool.Parse($"{((DbDataReader) npgsqlDataReader)["item_visible"]}") ? 0 : 4;
                    ((PlayerBonus) ((ItemsRepair) goodsItem).get_Item()).SetItemId(num1);
                    ((ItemsRepair) goodsItem).get_Item().Name = goodsItem.AuthType == 1 ? $"{str} ({num3} qty)" : (goodsItem.AuthType == 2 ? $"{str} ({num3 / 3600U} hours)" : str);
                    ((ItemsRepair) goodsItem).get_Item().Count = num3;
                    int idStatics2 = ComDiv.GetIdStatics(((ItemsRepair) goodsItem).get_Item().Id, 1);
                    switch (Boost)
                    {
                      case 1:
                        ShopManager.ShopAllList.Add(goodsItem);
                        if (goodsItem.Visibility != 2 && goodsItem.Visibility != 4)
                          ShopManager.ShopBuyableList.Add(goodsItem);
                        if (!ShopManager.ShopUniqueList.ContainsKey(((ItemsRepair) goodsItem).get_Item().Id) && goodsItem.AuthType > 0)
                        {
                          ShopManager.ShopUniqueList.Add(((ItemsRepair) goodsItem).get_Item().Id, goodsItem);
                          if (goodsItem.Visibility == 4)
                          {
                            ++ShopManager.Set4p;
                            continue;
                          }
                          continue;
                        }
                        continue;
                      case 2:
                        if (idStatics2 != 16 /*0x10*/)
                          continue;
                        goto case 1;
                      default:
                        continue;
                    }
                  }
                }
              }
            }
          }
          else
          {
            // ISSUE: reference to a compiler-generated method
            CLogger.Class1.Print($"Loading goods with invalid counts / moneys / points sizes. ({num1})", LoggerType.Warning, (Exception) null);
          }
        }
        ((Component) command).Dispose();
        ((DbDataReader) npgsqlDataReader).Close();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  private static void smethod_1(int Login)
  {
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        ((DbConnection) npgsqlConnection).Open();
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbCommand) command).CommandText = "SELECT * FROM system_shop_effects";
        ((DbCommand) command).CommandType = CommandType.Text;
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        while (((DbDataReader) npgsqlDataReader).Read())
        {
          int num1 = int.Parse($"{((DbDataReader) npgsqlDataReader)["coupon_id"]}");
          string[] strArray1;
          if (!$"{((DbDataReader) npgsqlDataReader)["coupon_count_day_list"]}".Contains(","))
            strArray1 = new string[1]
            {
              $"{((DbDataReader) npgsqlDataReader)["coupon_count_day_list"]}"
            };
          else
            strArray1 = $"{((DbDataReader) npgsqlDataReader)["coupon_count_day_list"]}".Split(',');
          string[] strArray2 = strArray1;
          string[] strArray3;
          if (!$"{((DbDataReader) npgsqlDataReader)["price_cash_list"]}".Contains(","))
            strArray3 = new string[1]
            {
              $"{((DbDataReader) npgsqlDataReader)["price_cash_list"]}"
            };
          else
            strArray3 = $"{((DbDataReader) npgsqlDataReader)["price_cash_list"]}".Split(',');
          string[] strArray4 = strArray3;
          string[] strArray5;
          if (!$"{((DbDataReader) npgsqlDataReader)["price_gold_list"]}".Contains(","))
            strArray5 = new string[1]
            {
              $"{((DbDataReader) npgsqlDataReader)["price_gold_list"]}"
            };
          else
            strArray5 = $"{((DbDataReader) npgsqlDataReader)["price_gold_list"]}".Split(',');
          string[] strArray6 = strArray5;
          if (strArray2.Length == strArray4.Length && strArray4.Length == strArray6.Length)
          {
            int num2 = 0;
            foreach (string s in strArray2)
            {
              ++num2;
              int num3;
              ref int local = ref num3;
              if (!int.TryParse(s, out local))
              {
                // ISSUE: reference to a compiler-generated method
                CLogger.Class1.Print($"Loading effects with count != Int ({num1})", LoggerType.Warning, (Exception) null);
              }
              else
              {
                int result1;
                if (!int.TryParse(strArray4[num2 - 1], out result1))
                {
                  // ISSUE: reference to a compiler-generated method
                  CLogger.Class1.Print($"Loading effects with cash != Int ({num1})", LoggerType.Warning, (Exception) null);
                }
                else
                {
                  int result2;
                  if (!int.TryParse(strArray6[num2 - 1], out result2))
                  {
                    // ISSUE: reference to a compiler-generated method
                    CLogger.Class1.Print($"Loading effects with gold != Int ({num1})", LoggerType.Warning, (Exception) null);
                  }
                  else
                  {
                    if (num3 >= 100)
                      num3 = 100;
                    int int_1 = int.Parse($"{$"{num1}".Substring(0, 2)}{num3:D2}{$"{num1}".Substring(4, 3)}");
                    ItemsRepair itemsRepair = new ItemsRepair();
                    ((GoodsItem) itemsRepair).Id = int.Parse($"{num1}{num2:D2}");
                    ((GoodsItem) itemsRepair).PriceGold = result2;
                    ((GoodsItem) itemsRepair).PriceCash = result1;
                    GoodsItem goodsItem = (GoodsItem) itemsRepair;
                    int num4 = int.Parse($"{((DbDataReader) npgsqlDataReader)["discount_percent"]}");
                    if (num4 > 0 && goodsItem.PriceCash > 0)
                    {
                      goodsItem.StarCash = goodsItem.PriceCash * (int) byte.MaxValue;
                      goodsItem.PriceCash = ComDiv.Percentage(goodsItem.PriceCash, num4);
                    }
                    if (num4 > 0 && goodsItem.PriceGold > 0)
                    {
                      goodsItem.PriceGold *= (int) byte.MaxValue;
                      goodsItem.PriceGold = ComDiv.Percentage(goodsItem.PriceGold, num4);
                    }
                    ((ItemsRepair) goodsItem).set_Tag(num4 > 0 ? ItemTag.Sale : (ItemTag) int.Parse($"{((DbDataReader) npgsqlDataReader)["shop_tag"]}"));
                    goodsItem.Title = 0;
                    goodsItem.AuthType = 1;
                    goodsItem.BuyType2 = 1;
                    goodsItem.BuyType3 = 2;
                    goodsItem.Visibility = bool.Parse($"{((DbDataReader) npgsqlDataReader)["coupon_visible"]}") ? 0 : 4;
                    ((PlayerBonus) ((ItemsRepair) goodsItem).get_Item()).SetItemId(int_1);
                    ((ItemsRepair) goodsItem).get_Item().Name = $"{((DbDataReader) npgsqlDataReader)["coupon_name"]} ({num3} days)";
                    ((ItemsRepair) goodsItem).get_Item().Count = 1U;
                    int idStatics = ComDiv.GetIdStatics(((ItemsRepair) goodsItem).get_Item().Id, 1);
                    switch (Login)
                    {
                      case 1:
                        ShopManager.ShopAllList.Add(goodsItem);
                        if (goodsItem.Visibility != 2 && goodsItem.Visibility != 4)
                          ShopManager.ShopBuyableList.Add(goodsItem);
                        if (!ShopManager.ShopUniqueList.ContainsKey(((ItemsRepair) goodsItem).get_Item().Id) && goodsItem.AuthType > 0)
                        {
                          ShopManager.ShopUniqueList.Add(((ItemsRepair) goodsItem).get_Item().Id, goodsItem);
                          if (goodsItem.Visibility == 4)
                          {
                            ++ShopManager.Set4p;
                            continue;
                          }
                          continue;
                        }
                        continue;
                      case 2:
                        if (idStatics != 16 /*0x10*/)
                          continue;
                        goto case 1;
                      default:
                        continue;
                    }
                  }
                }
              }
            }
          }
          else
          {
            // ISSUE: reference to a compiler-generated method
            CLogger.Class1.Print($"Loading goods with invalid counts / moneys / points sizes. ({num1})", LoggerType.Warning, (Exception) null);
          }
        }
        ((Component) command).Dispose();
        ((DbDataReader) npgsqlDataReader).Close();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  private static void smethod_2(int list)
  {
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        ((DbConnection) npgsqlConnection).Open();
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbCommand) command).CommandText = $"SELECT * FROM system_shop_sets WHERE visible = '{(System.ValueType) true}';";
        ((DbCommand) command).CommandType = CommandType.Text;
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        while (((DbDataReader) npgsqlDataReader).Read())
          ShopManager.smethod_3(int.Parse($"{((DbDataReader) npgsqlDataReader)["id"]}"), $"{((DbDataReader) npgsqlDataReader)["name"]}", list);
        ((Component) command).Dispose();
        ((DbDataReader) npgsqlDataReader).Close();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  private static void smethod_3([In] int obj0, string limit, [In] int obj2)
  {
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        ((DbConnection) npgsqlConnection).Open();
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbCommand) command).CommandText = $"SELECT * FROM system_shop_sets_items WHERE set_id = '{obj0}' AND set_name = '{limit}';";
        ((DbCommand) command).CommandType = CommandType.Text;
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        while (((DbDataReader) npgsqlDataReader).Read())
        {
          int int_1 = int.Parse($"{((DbDataReader) npgsqlDataReader)["id"]}");
          string str = $"{((DbDataReader) npgsqlDataReader)["name"]}";
          int num1 = int.Parse($"{((DbDataReader) npgsqlDataReader)["consume"]}");
          uint num2 = uint.Parse($"{((DbDataReader) npgsqlDataReader)["count"]}");
          int num3 = int.Parse($"{((DbDataReader) npgsqlDataReader)["price_gold"]}");
          int num4 = int.Parse($"{((DbDataReader) npgsqlDataReader)["price_cash"]}");
          ItemsRepair itemsRepair = new ItemsRepair();
          ((GoodsItem) itemsRepair).Id = obj0;
          ((GoodsItem) itemsRepair).PriceGold = num3;
          ((GoodsItem) itemsRepair).PriceCash = num4;
          itemsRepair.set_Tag(ItemTag.Hot);
          ((GoodsItem) itemsRepair).Title = 0;
          ((GoodsItem) itemsRepair).AuthType = 0;
          ((GoodsItem) itemsRepair).BuyType2 = 1;
          ((GoodsItem) itemsRepair).BuyType3 = num1 == 1 ? 2 : 1;
          ((GoodsItem) itemsRepair).Visibility = 4;
          GoodsItem goodsItem = (GoodsItem) itemsRepair;
          ((PlayerBonus) ((ItemsRepair) goodsItem).get_Item()).SetItemId(int_1);
          ((ItemsRepair) goodsItem).get_Item().Name = str;
          ((ItemsRepair) goodsItem).get_Item().Count = num2;
          int idStatics = ComDiv.GetIdStatics(((ItemsRepair) goodsItem).get_Item().Id, 1);
          switch (obj2)
          {
            case 1:
              ShopManager.ShopAllList.Add(goodsItem);
              if (goodsItem.Visibility != 2 && goodsItem.Visibility != 4)
                ShopManager.ShopBuyableList.Add(goodsItem);
              if (!ShopManager.ShopUniqueList.ContainsKey(((ItemsRepair) goodsItem).get_Item().Id) && goodsItem.AuthType > 0)
              {
                ShopManager.ShopUniqueList.Add(((ItemsRepair) goodsItem).get_Item().Id, goodsItem);
                if (goodsItem.Visibility == 4)
                {
                  ++ShopManager.Set4p;
                  continue;
                }
                continue;
              }
              continue;
            case 2:
              if (idStatics != 16 /*0x10*/)
                continue;
              goto case 1;
            default:
              continue;
          }
        }
        ((Component) command).Dispose();
        ((DbDataReader) npgsqlDataReader).Close();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  private static void smethod_4(int int_0)
  {
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        ((DbConnection) npgsqlConnection).Open();
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbCommand) command).CommandText = "SELECT * FROM system_shop_repair";
        ((DbCommand) command).CommandType = CommandType.Text;
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        while (((DbDataReader) npgsqlDataReader).Read())
        {
          MapRule mapRule = new MapRule();
          ((ItemsRepair) mapRule).Id = int.Parse($"{((DbDataReader) npgsqlDataReader)["item_id"]}");
          ((ItemsRepair) mapRule).Point = int.Parse($"{((DbDataReader) npgsqlDataReader)["price_gold"]}");
          ((ItemsRepair) mapRule).Cash = int.Parse($"{((DbDataReader) npgsqlDataReader)["price_cash"]}");
          mapRule.set_Quantity(uint.Parse($"{((DbDataReader) npgsqlDataReader)["quantity"]}"));
          mapRule.set_Enable(bool.Parse($"{((DbDataReader) npgsqlDataReader)["repairable"]}"));
          ItemsRepair itemsRepair = (ItemsRepair) mapRule;
          if (int_0 == 1 && ((MapRule) itemsRepair).get_Enable() && ((MapRule) itemsRepair).get_Quantity() <= 100U)
            ShopManager.ItemRepairs.Add(itemsRepair);
        }
        ((Component) command).Dispose();
        ((DbDataReader) npgsqlDataReader).Close();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public static void Reset()
  {
    ShopManager.Set4p = 0;
    ShopManager.ShopAllList.Clear();
    ShopManager.ShopBuyableList.Clear();
    ShopManager.ShopUniqueList.Clear();
    ShopManager.ShopDataMt1.Clear();
    ShopManager.ShopDataMt2.Clear();
    ShopManager.ShopDataGoods.Clear();
    ShopManager.ShopDataItems.Clear();
    ShopManager.ShopDataItemRepairs.Clear();
    ShopManager.ItemRepairs.Clear();
    ShopManager.TotalGoods = 0;
    ShopManager.TotalItems = 0;
    ShopManager.TotalMatching1 = 0;
    ShopManager.TotalMatching2 = 0;
    ShopManager.TotalRepairs = 0;
  }

  private static void smethod_5(int int_0)
  {
    List<GoodsItem> list_0 = new List<GoodsItem>();
    List<GoodsItem> goodsItemList = new List<GoodsItem>();
    lock (ShopManager.ShopAllList)
    {
      foreach (GoodsItem shopAll in ShopManager.ShopAllList)
      {
        if (((ItemsRepair) shopAll).get_Item().Count != 0U)
        {
          if ((((ItemsRepair) shopAll).get_Tag() != ItemTag.PcCafe || int_0 != 0) && (((ItemsRepair) shopAll).get_Tag() == ItemTag.PcCafe && int_0 > 0 || shopAll.Visibility != 2))
            list_0.Add(shopAll);
          if (shopAll.Visibility < 2 || shopAll.Visibility == 4)
            goodsItemList.Add(shopAll);
        }
      }
    }
    ShopManager.TotalMatching1 = list_0.Count;
    ShopManager.TotalGoods = goodsItemList.Count;
    int num1 = (int) Math.Ceiling((double) list_0.Count / 500.0);
    int int_2 = 0;
    for (int index = 0; index < num1; ++index)
    {
      byte[] numArray = ShopManager.smethod_12(500, index, ref int_2, list_0);
      SlotChange slotChange = new SlotChange();
      ((ShopData) slotChange).Buffer = numArray;
      slotChange.set_ItemsCount(int_2);
      slotChange.set_Offset(index * 500);
      ShopData shopData = (ShopData) slotChange;
      ShopManager.ShopDataMt1.Add(shopData);
    }
    int num2 = (int) Math.Ceiling((double) goodsItemList.Count / 50.0);
    for (int int_1 = 0; int_1 < num2; ++int_1)
    {
      byte[] numArray = ShopManager.smethod_10(50, int_1, ref int_2, goodsItemList);
      SlotChange slotChange = new SlotChange();
      ((ShopData) slotChange).Buffer = numArray;
      slotChange.set_ItemsCount(int_2);
      slotChange.set_Offset(int_1 * 50);
      ShopData shopData = (ShopData) slotChange;
      ShopManager.ShopDataGoods.Add(shopData);
    }
  }

  private static void smethod_6(int int_0)
  {
    List<GoodsItem> list_0 = new List<GoodsItem>();
    lock (ShopManager.ShopAllList)
    {
      foreach (GoodsItem shopAll in ShopManager.ShopAllList)
      {
        if (((ItemsRepair) shopAll).get_Item().Count != 0U && (((ItemsRepair) shopAll).get_Tag() != ItemTag.PcCafe || int_0 != 0) && (((ItemsRepair) shopAll).get_Tag() == ItemTag.PcCafe && int_0 > 0 || shopAll.Visibility != 2))
          list_0.Add(shopAll);
      }
    }
    ShopManager.TotalMatching2 = list_0.Count;
    int num1 = (int) Math.Ceiling((double) list_0.Count / 500.0);
    int num2 = 0;
    for (int index = 0; index < num1; ++index)
    {
      byte[] numArray = ShopManager.smethod_12(500, index, ref num2, list_0);
      SlotChange slotChange = new SlotChange();
      ((ShopData) slotChange).Buffer = numArray;
      slotChange.set_ItemsCount(num2);
      slotChange.set_Offset(index * 500);
      ShopData shopData = (ShopData) slotChange;
      ShopManager.ShopDataMt2.Add(shopData);
    }
  }

  private static void smethod_7()
  {
    List<GoodsItem> goodsItemList = new List<GoodsItem>();
    lock (ShopManager.ShopUniqueList)
    {
      foreach (GoodsItem goodsItem in (IEnumerable<GoodsItem>) ShopManager.ShopUniqueList.Values)
      {
        if (goodsItem.Visibility != 1 && goodsItem.Visibility != 3)
          goodsItemList.Add(goodsItem);
      }
    }
    ShopManager.TotalItems = goodsItemList.Count;
    int num = (int) Math.Ceiling((double) goodsItemList.Count / 800.0);
    int int_1 = 0;
    for (int string_0 = 0; string_0 < num; ++string_0)
    {
      byte[] numArray = ShopManager.smethod_9(800, string_0, ref int_1, goodsItemList);
      SlotChange slotChange = new SlotChange();
      ((ShopData) slotChange).Buffer = numArray;
      slotChange.set_ItemsCount(int_1);
      slotChange.set_Offset(string_0 * 800);
      ShopData shopData = (ShopData) slotChange;
      ShopManager.ShopDataItems.Add(shopData);
    }
  }

  private static void smethod_8()
  {
    List<ItemsRepair> list_0 = new List<ItemsRepair>();
    lock (ShopManager.ItemRepairs)
    {
      foreach (ItemsRepair itemRepair in ShopManager.ItemRepairs)
        list_0.Add(itemRepair);
    }
    ShopManager.TotalRepairs = list_0.Count;
    int num1 = (int) Math.Ceiling((double) list_0.Count / 100.0);
    int num2 = 0;
    for (int index = 0; index < num1; ++index)
    {
      byte[] numArray = ShopManager.smethod_11(100, index, ref num2, list_0);
      SlotChange slotChange = new SlotChange();
      ((ShopData) slotChange).Buffer = numArray;
      slotChange.set_ItemsCount(num2);
      slotChange.set_Offset(index * 100);
      ShopData shopData = (ShopData) slotChange;
      ShopManager.ShopDataItemRepairs.Add(shopData);
    }
  }

  private static byte[] smethod_9([In] int obj0, int string_0, ref int int_1, [In] List<GoodsItem> obj3)
  {
    int_1 = 0;
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      for (int index = string_0 * obj0; index < obj3.Count; ++index)
      {
        ShopManager.smethod_13(obj3[index], syncServerPacket);
        if (++int_1 == obj0)
          break;
      }
      return syncServerPacket.ToArray();
    }
  }

  private static byte[] smethod_10(int int_0, int int_1, ref int int_2, [In] List<GoodsItem> obj3)
  {
    int_2 = 0;
    using (SyncServerPacket int_1_1 = new SyncServerPacket())
    {
      for (int index = int_1 * int_0; index < obj3.Count; ++index)
      {
        ShopManager.smethod_14(obj3[index], int_1_1);
        if (++int_2 == int_0)
          break;
      }
      return int_1_1.ToArray();
    }
  }

  private static byte[] smethod_11([In] int obj0, [In] int obj1, [In] ref int obj2, List<ItemsRepair> list_0)
  {
    obj2 = 0;
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      for (int index = obj1 * obj0; index < list_0.Count; ++index)
      {
        ShopManager.smethod_15(list_0[index], syncServerPacket);
        if (++obj2 == obj0)
          break;
      }
      return syncServerPacket.ToArray();
    }
  }

  private static byte[] smethod_12([In] int obj0, [In] int obj1, [In] ref int obj2, List<GoodsItem> list_0)
  {
    obj2 = 0;
    using (SyncServerPacket syncServerPacket_0 = new SyncServerPacket())
    {
      for (int index = obj1 * obj0; index < list_0.Count; ++index)
      {
        ShopManager.smethod_16(list_0[index], syncServerPacket_0);
        if (++obj2 == obj0)
          break;
      }
      return syncServerPacket_0.ToArray();
    }
  }

  private static void smethod_13([In] GoodsItem obj0, [In] SyncServerPacket obj1)
  {
    obj1.WriteD(((ItemsRepair) obj0).get_Item().Id);
    obj1.WriteC((byte) obj0.AuthType);
    obj1.WriteC((byte) obj0.BuyType2);
    obj1.WriteC((byte) obj0.BuyType3);
    obj1.WriteC((byte) obj0.Title);
    obj1.WriteC(obj0.Title != 0 ? (byte) 2 : (byte) 0);
    obj1.WriteH((short) 0);
  }

  private static void smethod_14([In] GoodsItem obj0, SyncServerPacket int_1)
  {
    int_1.WriteD(obj0.Id);
    int_1.WriteC((byte) 1);
    int_1.WriteC(obj0.Visibility == 4 ? (byte) 4 : (byte) 1);
    int_1.WriteD(obj0.PriceGold);
    int_1.WriteD(obj0.PriceCash);
    int_1.WriteD(0);
    int_1.WriteC((byte) ((ItemsRepair) obj0).get_Tag());
    int_1.WriteC((byte) 0);
    int_1.WriteC((byte) 0);
    int_1.WriteC((byte) 0);
    int_1.WriteD(obj0.StarCash > 0 ? obj0.StarCash : (obj0.StarGold > 0 ? obj0.StarGold : 0));
    int_1.WriteD(0);
    int_1.WriteD(0);
    int_1.WriteD(0);
    int_1.WriteB(new byte[98]);
  }

  private static void smethod_15([In] ItemsRepair obj0, [In] SyncServerPacket obj1)
  {
    obj1.WriteD(obj0.Id);
    obj1.WriteD((int) (double) ((long) obj0.Point / (long) ((MapRule) obj0).get_Quantity()));
    obj1.WriteD((int) (double) ((long) obj0.Cash / (long) ((MapRule) obj0).get_Quantity()));
    obj1.WriteD(((MapRule) obj0).get_Quantity());
  }

  private static void smethod_16([In] GoodsItem obj0, SyncServerPacket syncServerPacket_0)
  {
    syncServerPacket_0.WriteD(obj0.Id);
    syncServerPacket_0.WriteD(((ItemsRepair) obj0).get_Item().Id);
    syncServerPacket_0.WriteD(((ItemsRepair) obj0).get_Item().Count);
    syncServerPacket_0.WriteD(0);
  }

  public static bool IsRepairableItem([In] int obj0) => ShopManager.GetRepairItem(obj0) != null;

  public static ItemsRepair GetRepairItem(int itemsRepair_0)
  {
    if (itemsRepair_0 == 0)
      return (ItemsRepair) null;
    lock (ShopManager.ItemRepairs)
    {
      foreach (ItemsRepair itemRepair in ShopManager.ItemRepairs)
      {
        if (itemRepair.Id == itemsRepair_0)
          return itemRepair;
      }
    }
    return (ItemsRepair) null;
  }

  public static bool IsBlocked([In] string obj0, List<int> syncServerPacket_0)
  {
    lock (ShopManager.ShopUniqueList)
    {
      foreach (GoodsItem goodsItem in (IEnumerable<GoodsItem>) ShopManager.ShopUniqueList.Values)
      {
        if (!syncServerPacket_0.Contains(((ItemsRepair) goodsItem).get_Item().Id) && ((ItemsRepair) goodsItem).get_Item().Name.Contains(obj0))
          syncServerPacket_0.Add(((ItemsRepair) goodsItem).get_Item().Id);
      }
    }
    return false;
  }
}
