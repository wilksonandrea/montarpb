// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Utility.ComDiv
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Npgsql;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.RAW;
using Plugin.Core.SQL;
using Plugin.Core.XML;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

#nullable disable
namespace Plugin.Core.Utility;

public static class ComDiv
{
  public static void MD5File(string ushort_1)
  {
    try
    {
      using (MD5 md5 = MD5.Create())
      {
        using (FileStream inputStream = System.IO.File.OpenRead(ushort_1))
          Console.WriteLine("MD5: " + BitConverter.ToString(md5.ComputeHash((Stream) inputStream)).Replace("-", "").ToLower());
      }
    }
    catch (Exception ex)
    {
    }
  }

  public static char[] HexCodes(byte[] ipAddress)
  {
    char[] destination = new char[ipAddress.Length * 2];
    for (int index = 0; index < ipAddress.Length; ++index)
      ipAddress[index].ToString("x2").CopyTo(0, destination, index * 2, 2);
    return destination;
  }

  public static string SHA1File(string byte_2)
  {
    try
    {
      using (SHA1 shA1 = SHA1.Create())
      {
        byte[] hash = shA1.ComputeHash(Encoding.UTF8.GetBytes(byte_2));
        StringBuilder stringBuilder = new StringBuilder();
        for (int index = 0; index < hash.Length; ++index)
          stringBuilder.Append(hash[index].ToString("x2"));
        return stringBuilder.ToString();
      }
    }
    catch (Exception ex)
    {
      return "";
    }
  }

  public static int SHA1_Int32(string Plain)
  {
    try
    {
      return int.Parse(ComDiv.SHA1File(Plain), NumberStyles.HexNumber);
    }
    catch (Exception ex)
    {
      return 0;
    }
  }

  public static string GetHWID()
  {
    try
    {
      string s = Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER") + Environment.GetEnvironmentVariable("COMPUTERNAME") + Environment.UserName.Trim();
      using (MD5 md5 = MD5.Create())
      {
        byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(s));
        StringBuilder stringBuilder = new StringBuilder();
        for (int index = 0; index < hash.Length; ++index)
        {
          stringBuilder.Append(hash[index].ToString("x3").Substring(0, 3));
          if (index != hash.Length - 1)
            stringBuilder.Append("-");
        }
        return stringBuilder.ToString();
      }
    }
    catch (Exception ex)
    {
      return "";
    }
  }

  public static int CheckEquipedItems(PlayerEquipment Cipher, [In] List<ItemsModel> obj1, [In] bool obj2)
  {
    int num1 = 0;
    (bool, bool, bool, bool, bool) BattleRules = (false, false, false, false, false);
    (bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool) valueTuple1 = (false, false, false, false, false, false, false, false, false, false, false, false);
    (bool, bool, bool) valueTuple2 = (false, false, false);
    if (Cipher.BeretItem == 0)
      valueTuple1.Item11 = true;
    if (Cipher.AccessoryId == 0)
      valueTuple2.Item1 = true;
    if (((PlayerMissions) Cipher).get_SprayId() == 0)
      valueTuple2.Item2 = true;
    if (((PlayerMissions) Cipher).get_NameCardId() == 0)
      valueTuple2.Item3 = true;
    if (Cipher.WeaponPrimary == 103004)
      BattleRules.Item1 = true;
    if (obj2)
    {
      if (!BattleRules.Item1 && (Cipher.WeaponPrimary == 105025 || Cipher.WeaponPrimary == 106007))
        BattleRules.Item1 = true;
      if (!BattleRules.Item3 && Cipher.WeaponMelee == 323001)
        BattleRules.Item3 = true;
    }
    ((bool, bool, bool, bool, bool), (bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool), (bool, bool, bool)) tuple = ComDiv.smethod_0(Cipher, obj1, BattleRules, valueTuple1, valueTuple2);
    (bool, bool, bool, bool, bool) list_0 = tuple.Item1;
    (bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool) valueTuple_0 = tuple.Item2;
    (bool, bool, bool) valueTuple_1 = tuple.Item3;
    bool flag1 = !list_0.Item1 || !list_0.Item2 || !list_0.Item3 || !list_0.Item4 || !list_0.Item5;
    int num2 = !valueTuple_0.Item1 || !valueTuple_0.Item2 || !valueTuple_0.Item3 || !valueTuple_0.Item4 || !valueTuple_0.Item5 || !valueTuple_0.Item6 || !valueTuple_0.Item7 || !valueTuple_0.Item8 || !valueTuple_0.Item9 || !valueTuple_0.Item10 || !valueTuple_0.Item11 ? 1 : (!valueTuple_0.Item12 ? 1 : 0);
    bool flag2 = !valueTuple_1.Item1 || !valueTuple_1.Item2 || !valueTuple_1.Item3;
    if (flag1)
      num1 += 2;
    if (num2 != 0)
      ++num1;
    if (flag2)
      num1 += 3;
    ComDiv.smethod_1(Cipher, ref list_0, ref valueTuple_0, ref valueTuple_1);
    return num1;
  }

  private static ((bool, bool, bool, bool, bool), (bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool), (bool, bool, bool)) smethod_0(
    PlayerEquipment msg,
    List<ItemsModel> Inventory,
    (bool, bool, bool, bool, bool) BattleRules,
    [In] (bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool) obj3,
    [In] (bool, bool, bool) obj4)
  {
    lock (Inventory)
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      HashSet<int> intSet = new HashSet<int>(Inventory.Where<ItemsModel>(ComDiv.Class5.\u003C\u003E9__1_0 ?? (ComDiv.Class5.\u003C\u003E9__1_0 = new System.Func<ItemsModel, bool>(((DateTimeUtil) ComDiv.Class5.\u003C\u003E9).method_0))).Select<ItemsModel, int>(ComDiv.Class5.\u003C\u003E9__1_1 ?? (ComDiv.Class5.\u003C\u003E9__1_1 = new System.Func<ItemsModel, int>(((DBQuery) ComDiv.Class5.\u003C\u003E9).method_1))));
      if (intSet.Contains(msg.WeaponPrimary))
        BattleRules.Item1 = true;
      if (intSet.Contains(msg.WeaponSecondary))
        BattleRules.Item2 = true;
      if (intSet.Contains(msg.WeaponMelee))
        BattleRules.Item3 = true;
      if (intSet.Contains(msg.WeaponExplosive))
        BattleRules.Item4 = true;
      if (intSet.Contains(msg.WeaponSpecial))
        BattleRules.Item5 = true;
      if (intSet.Contains(msg.CharaRedId))
        obj3.Item1 = true;
      if (intSet.Contains(msg.CharaBlueId))
        obj3.Item2 = true;
      if (intSet.Contains(msg.PartHead))
        obj3.Item3 = true;
      if (intSet.Contains(msg.PartFace))
        obj3.Item4 = true;
      if (intSet.Contains(msg.PartJacket))
        obj3.Item5 = true;
      if (intSet.Contains(msg.PartPocket))
        obj3.Item6 = true;
      if (intSet.Contains(msg.PartGlove))
        obj3.Item7 = true;
      if (intSet.Contains(msg.PartBelt))
        obj3.Item8 = true;
      if (intSet.Contains(msg.PartHolster))
        obj3.Item9 = true;
      if (intSet.Contains(msg.PartSkin))
        obj3.Item10 = true;
      if (msg.BeretItem != 0 && intSet.Contains(msg.BeretItem))
        obj3.Item11 = true;
      if (intSet.Contains(msg.DinoItem))
        obj3.Item12 = true;
      if (msg.AccessoryId != 0 && intSet.Contains(msg.AccessoryId))
        obj4.Item1 = true;
      if (((PlayerMissions) msg).get_SprayId() != 0 && intSet.Contains(((PlayerMissions) msg).get_SprayId()))
        obj4.Item2 = true;
      if (((PlayerMissions) msg).get_NameCardId() != 0)
      {
        if (intSet.Contains(((PlayerMissions) msg).get_NameCardId()))
          obj4.Item3 = true;
      }
    }
    return (BattleRules, obj3, obj4);
  }

  private static void smethod_1(
    PlayerEquipment playerEquipment_0,
    ref (bool, bool, bool, bool, bool) list_0,
    ref (bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool) valueTuple_0,
    ref (bool, bool, bool) valueTuple_1)
  {
    if (!list_0.Item1)
      playerEquipment_0.WeaponPrimary = 103004;
    if (!list_0.Item2)
      playerEquipment_0.WeaponSecondary = 202003;
    if (!list_0.Item3)
      playerEquipment_0.WeaponMelee = 301001;
    if (!list_0.Item4)
      playerEquipment_0.WeaponExplosive = 407001;
    if (!list_0.Item5)
      playerEquipment_0.WeaponSpecial = 508001;
    if (!valueTuple_0.Item1)
      playerEquipment_0.CharaRedId = 601001;
    if (!valueTuple_0.Item2)
      playerEquipment_0.CharaBlueId = 602002;
    if (!valueTuple_0.Item3)
      playerEquipment_0.PartHead = 1000700000;
    if (!valueTuple_0.Item4)
      playerEquipment_0.PartFace = 1000800000;
    if (!valueTuple_0.Item5)
      playerEquipment_0.PartJacket = 1000900000;
    if (!valueTuple_0.Item6)
      playerEquipment_0.PartPocket = 1001000000;
    if (!valueTuple_0.Item7)
      playerEquipment_0.PartGlove = 1001100000;
    if (!valueTuple_0.Item8)
      playerEquipment_0.PartBelt = 1001200000;
    if (!valueTuple_0.Item9)
      playerEquipment_0.PartHolster = 1001300000;
    if (!valueTuple_0.Item10)
      playerEquipment_0.PartSkin = 1001400000;
    if (!valueTuple_0.Item11)
      playerEquipment_0.BeretItem = 0;
    if (!valueTuple_0.Item12)
      playerEquipment_0.DinoItem = 1500511;
    if (!valueTuple_1.Item1)
      playerEquipment_0.AccessoryId = 0;
    if (!valueTuple_1.Item2)
      ((PlayerMissions) playerEquipment_0).set_SprayId(0);
    if (!valueTuple_1.Item3)
      ((PlayerMissions) playerEquipment_0).set_NameCardId(0);
    if (playerEquipment_0.PartHead != 1000700000 || playerEquipment_0.PartFace == 1000800000)
      return;
    playerEquipment_0.PartHead = 0;
  }

  public static void UpdateWeapons([In] PlayerEquipment obj0, [In] DBQuery obj1)
  {
    ((ObjectCopier) obj1).AddQuery("weapon_primary", (object) obj0.WeaponPrimary);
    ((ObjectCopier) obj1).AddQuery("weapon_secondary", (object) obj0.WeaponSecondary);
    ((ObjectCopier) obj1).AddQuery("weapon_melee", (object) obj0.WeaponMelee);
    ((ObjectCopier) obj1).AddQuery("weapon_explosive", (object) obj0.WeaponExplosive);
    ((ObjectCopier) obj1).AddQuery("weapon_special", (object) obj0.WeaponSpecial);
  }

  public static void UpdateChars([In] PlayerEquipment obj0, DBQuery valueTuple_0)
  {
    ((ObjectCopier) valueTuple_0).AddQuery("chara_red_side", (object) obj0.CharaRedId);
    ((ObjectCopier) valueTuple_0).AddQuery("chara_blue_side", (object) obj0.CharaBlueId);
    ((ObjectCopier) valueTuple_0).AddQuery("part_head", (object) obj0.PartHead);
    ((ObjectCopier) valueTuple_0).AddQuery("part_face", (object) obj0.PartFace);
    ((ObjectCopier) valueTuple_0).AddQuery("part_jacket", (object) obj0.PartJacket);
    ((ObjectCopier) valueTuple_0).AddQuery("part_pocket", (object) obj0.PartPocket);
    ((ObjectCopier) valueTuple_0).AddQuery("part_glove", (object) obj0.PartGlove);
    ((ObjectCopier) valueTuple_0).AddQuery("part_belt", (object) obj0.PartBelt);
    ((ObjectCopier) valueTuple_0).AddQuery("part_holster", (object) obj0.PartHolster);
    ((ObjectCopier) valueTuple_0).AddQuery("part_skin", (object) obj0.PartSkin);
    ((ObjectCopier) valueTuple_0).AddQuery("beret_item_part", (object) obj0.BeretItem);
    ((ObjectCopier) valueTuple_0).AddQuery("dino_item_chara", (object) obj0.DinoItem);
  }

  public static void UpdateItems([In] PlayerEquipment obj0, [In] DBQuery obj1)
  {
    ((ObjectCopier) obj1).AddQuery("accesory_id", (object) obj0.AccessoryId);
    ((ObjectCopier) obj1).AddQuery("spray_id", (object) ((PlayerMissions) obj0).get_SprayId());
    ((ObjectCopier) obj1).AddQuery("namecard_id", (object) ((PlayerMissions) obj0).get_NameCardId());
  }

  public static void TryCreateItem([In] ItemsModel obj0, PlayerInventory Query, [In] long obj2)
  {
    try
    {
      ItemsModel itemsModel = Query.GetItem(obj0.Id);
      if (itemsModel == null)
      {
        if (!DaoManagerSQL.CreatePlayerInventoryItem(obj0, obj2))
          return;
        ((PlayerTopup) Query).AddItem(obj0);
      }
      else
      {
        obj0.ObjectId = itemsModel.ObjectId;
        if (itemsModel.Equip == ItemEquipType.Durable)
        {
          if (ShopManager.IsRepairableItem(obj0.Id))
          {
            obj0.Count = 100U;
            ComDiv.UpdateDB("player_items", "count", (object) (long) obj0.Count, "owner_id", (object) obj2, "id", (object) obj0.Id);
          }
          else
          {
            obj0.Count += itemsModel.Count;
            ComDiv.UpdateDB("player_items", "count", (object) (long) obj0.Count, "owner_id", (object) obj2, "id", (object) obj0.Id);
          }
        }
        else if (itemsModel.Equip == ItemEquipType.Temporary)
        {
          DateTime exact = DateTime.ParseExact(itemsModel.Count.ToString(), "yyMMddHHmm", (IFormatProvider) CultureInfo.InvariantCulture);
          if (obj0.Category != ItemCategory.Coupon)
          {
            obj0.Equip = ItemEquipType.Temporary;
            obj0.Count = Convert.ToUInt32(exact.AddSeconds((double) obj0.Count).ToString("yyMMddHHmm"));
          }
          else
          {
            TimeSpan timeSpan = DateTime.ParseExact(obj0.Count.ToString(), "yyMMddHHmm", (IFormatProvider) CultureInfo.InvariantCulture) - DBQuery.Now();
            obj0.Equip = ItemEquipType.Temporary;
            obj0.Count = Convert.ToUInt32(exact.AddDays(timeSpan.TotalDays).ToString("yyMMddHHmm"));
          }
          ComDiv.UpdateDB("player_items", "count", (object) (long) obj0.Count, "owner_id", (object) obj2, "id", (object) obj0.Id);
        }
        itemsModel.Equip = obj0.Equip;
        itemsModel.Count = obj0.Count;
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public static ItemCategory GetItemCategory(int Equip)
  {
    int idStatics1 = ComDiv.GetIdStatics(Equip, 1);
    int idStatics2 = ComDiv.GetIdStatics(Equip, 4);
    if (idStatics1 >= 1 && idStatics1 <= 5)
      return ItemCategory.Weapon;
    if (idStatics1 >= 6 && idStatics1 <= 14 || idStatics1 == 27 || idStatics2 >= 7 && idStatics2 <= 14)
      return ItemCategory.Character;
    if (idStatics1 >= 16 /*0x10*/ && idStatics1 <= 20 || idStatics1 == 22 || idStatics1 == 26 || idStatics1 >= 28 && idStatics1 <= 29 || idStatics1 >= 36 && idStatics1 <= 40)
      return ItemCategory.Coupon;
    if (idStatics1 == 15 || idStatics1 >= 30 && idStatics1 <= 35)
      return ItemCategory.NewItem;
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Invalid Category [{idStatics1}]: {Equip}", LoggerType.Warning, (Exception) null);
    return ItemCategory.None;
  }

  public static uint ValidateStockId([In] int obj0)
  {
    int idStatics = ComDiv.GetIdStatics(obj0, 4);
    int num;
    switch (idStatics)
    {
      case 7:
      case 8:
      case 9:
      case 10:
      case 11:
      case 12:
      case 13:
      case 14:
        num = idStatics;
        break;
      default:
        num = obj0;
        break;
    }
    return ComDiv.GenStockId(num);
  }

  public static int GetIdStatics(int Model, int Inventory)
  {
    switch (Inventory)
    {
      case 1:
        return Model / 100000;
      case 2:
        return Model % 100000 / 1000;
      case 3:
        return Model % 1000;
      case 4:
        return Model % 10000000 / 100000;
      case 5:
        return Model / 1000;
      default:
        return 0;
    }
  }

  public static double GetDuration([In] DateTime obj0) => (DBQuery.Now() - obj0).TotalSeconds;

  public static byte[] AddressBytes(string ItemId) => IPAddress.Parse(ItemId).GetAddressBytes();

  public static int CreateItemId(int ItemId, int Type, [In] int obj2)
  {
    return ItemId * 100000 + Type * 1000 + obj2;
  }

  public static int Percentage(int Date, [In] int obj1) => Date * obj1 / 100;

  public static float Percentage(float ItemClass, int ClassType)
  {
    return (float) ((double) ItemClass * (double) ClassType / 100.0);
  }

  public static char[] SubArray([In] this char[] obj0, [In] int obj1, int Number)
  {
    List<char> charList = new List<char>();
    for (int index = obj1; index < Number; ++index)
      charList.Add(obj0[index]);
    return charList.ToArray();
  }

  public static bool UpdateDB(string Total, string[] Percent, [In] object[] obj2)
  {
    if (Percent.Length != 0 && obj2.Length != 0 && Percent.Length != obj2.Length)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print($"[Update Database] Wrong values: {string.Join(",", Percent)}/{string.Join(",", obj2)}", LoggerType.Warning, (Exception) null);
      return false;
    }
    if (Percent.Length != 0)
    {
      if (obj2.Length != 0)
      {
        try
        {
          using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
          {
            using (NpgsqlCommand command = npgsqlConnection.CreateCommand())
            {
              ((DbConnection) npgsqlConnection).Open();
              ((DbCommand) command).CommandType = CommandType.Text;
              List<string> stringList = new List<string>();
              for (int index = 0; index < obj2.Length; ++index)
              {
                object obj = obj2[index];
                string str = Percent[index];
                string parameterName = "@Value" + index.ToString();
                command.Parameters.AddWithValue(parameterName, obj);
                stringList.Add($"{str}={parameterName}");
              }
              string str1 = string.Join(",", stringList.ToArray());
              ((DbCommand) command).CommandText = $"UPDATE {Total} SET {str1}";
              ((DbCommand) command).ExecuteNonQuery();
              ((Component) command).Dispose();
              ((DbConnection) npgsqlConnection).Close();
            }
          }
          return true;
        }
        catch (Exception ex)
        {
          // ISSUE: reference to a compiler-generated method
          CLogger.Class1.Print("[AllUtils.UpdateDB1] " + ex.Message, LoggerType.Error, ex);
          return false;
        }
      }
    }
    return false;
  }

  public static bool UpdateDB(
    [In] string obj0,
    string StartIndex,
    object Length,
    [In] string[] obj3,
    [In] object[] obj4)
  {
    if (obj3.Length != 0 && obj4.Length != 0 && obj3.Length != obj4.Length)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print($"[Update Database] Wrong values: {string.Join(",", obj3)}/{string.Join(",", obj4)}", LoggerType.Warning, (Exception) null);
      return false;
    }
    if (obj3.Length != 0)
    {
      if (obj4.Length != 0)
      {
        try
        {
          using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
          {
            using (NpgsqlCommand command = npgsqlConnection.CreateCommand())
            {
              ((DbConnection) npgsqlConnection).Open();
              ((DbCommand) command).CommandType = CommandType.Text;
              List<string> stringList = new List<string>();
              for (int index = 0; index < obj4.Length; ++index)
              {
                object obj = obj4[index];
                string str = obj3[index];
                string parameterName = "@Value" + index.ToString();
                command.Parameters.AddWithValue(parameterName, obj);
                stringList.Add($"{str}={parameterName}");
              }
              string str1 = string.Join(",", stringList.ToArray());
              command.Parameters.AddWithValue("@Req1", Length);
              ((DbCommand) command).CommandText = $"UPDATE {obj0} SET {str1} WHERE {StartIndex}=@Req1";
              ((DbCommand) command).ExecuteNonQuery();
              ((Component) command).Dispose();
              ((DbConnection) npgsqlConnection).Close();
            }
          }
          return true;
        }
        catch (Exception ex)
        {
          // ISSUE: reference to a compiler-generated method
          CLogger.Class1.Print("[AllUtils.UpdateDB2] " + ex.Message, LoggerType.Error, ex);
          return false;
        }
      }
    }
    return false;
  }

  public static bool UpdateDB(
    string TABEL,
    string Req1,
    object ValueReq1,
    string COLUMNS,
    params object VALUES)
  {
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        using (NpgsqlCommand command = npgsqlConnection.CreateCommand())
        {
          ((DbConnection) npgsqlConnection).Open();
          ((DbCommand) command).CommandType = CommandType.Text;
          command.Parameters.AddWithValue("@Value", ValueReq1);
          command.Parameters.AddWithValue("@Req1", VALUES);
          ((DbCommand) command).CommandText = $"UPDATE {TABEL} SET {Req1}=@Value WHERE {COLUMNS}=@Req1";
          ((DbCommand) command).ExecuteNonQuery();
          ((Component) command).Dispose();
          ((DbConnection) npgsqlConnection).Close();
        }
      }
      return true;
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("[AllUtils.UpdateDB3] " + ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  public static bool UpdateDB(
    string TABEL,
    string COLUMN,
    object VALUE,
    string Req1,
    object ValueReq1,
    [In] string[] obj5,
    [In] object[] obj6)
  {
    if (obj5.Length != 0 && obj6.Length != 0 && obj5.Length != obj6.Length)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print($"[Update Database] Wrong values: {string.Join(",", obj5)}/{string.Join(",", obj6)}", LoggerType.Warning, (Exception) null);
      return false;
    }
    if (obj5.Length != 0)
    {
      if (obj6.Length != 0)
      {
        try
        {
          using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
          {
            using (NpgsqlCommand command = npgsqlConnection.CreateCommand())
            {
              ((DbConnection) npgsqlConnection).Open();
              ((DbCommand) command).CommandType = CommandType.Text;
              List<string> stringList = new List<string>();
              for (int index = 0; index < obj6.Length; ++index)
              {
                object obj = obj6[index];
                string str = obj5[index];
                string parameterName = "@Value" + index.ToString();
                command.Parameters.AddWithValue(parameterName, obj);
                stringList.Add($"{str}={parameterName}");
              }
              string str1 = string.Join(",", stringList.ToArray());
              if (COLUMN != null)
                command.Parameters.AddWithValue("@Req1", VALUE);
              if (Req1 != null)
                command.Parameters.AddWithValue("@Req2", ValueReq1);
              if (COLUMN != null && Req1 == null)
                ((DbCommand) command).CommandText = $"UPDATE {TABEL} SET {str1} WHERE {COLUMN}=@Req1";
              else if (Req1 != null && COLUMN == null)
                ((DbCommand) command).CommandText = $"UPDATE {TABEL} SET {str1} WHERE {Req1}=@Req2";
              else if (Req1 != null && COLUMN != null)
                ((DbCommand) command).CommandText = $"UPDATE {TABEL} SET {str1} WHERE {COLUMN}=@Req1 AND {Req1}=@Req2";
              ((DbCommand) command).ExecuteNonQuery();
              ((Component) command).Dispose();
              ((DbConnection) npgsqlConnection).Close();
            }
          }
          return true;
        }
        catch (Exception ex)
        {
          // ISSUE: reference to a compiler-generated method
          CLogger.Class1.Print("[AllUtils.UpdateDB4] " + ex.Message, LoggerType.Error, ex);
          return false;
        }
      }
    }
    return false;
  }

  public static bool UpdateDB(
    [In] string obj0,
    [In] string obj1,
    int[] ValueReq1,
    string Req2,
    object valueReq2,
    string[] COLUMNS,
    params object[] VALUES)
  {
    if (COLUMNS.Length != 0 && VALUES.Length != 0 && COLUMNS.Length != VALUES.Length)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print($"[updateDB5] Wrong values: {string.Join(",", COLUMNS)}/{string.Join(",", VALUES)}", LoggerType.Warning, (Exception) null);
      return false;
    }
    if (COLUMNS.Length != 0)
    {
      if (VALUES.Length != 0)
      {
        try
        {
          using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
          {
            using (NpgsqlCommand command = npgsqlConnection.CreateCommand())
            {
              ((DbConnection) npgsqlConnection).Open();
              ((DbCommand) command).CommandType = CommandType.Text;
              List<string> stringList = new List<string>();
              for (int index = 0; index < VALUES.Length; ++index)
              {
                object obj = VALUES[index];
                string str = COLUMNS[index];
                string parameterName = "@Value" + index.ToString();
                command.Parameters.AddWithValue(parameterName, obj);
                stringList.Add($"{str}={parameterName}");
              }
              string str1 = string.Join(",", stringList.ToArray());
              if (obj1 != null)
                command.Parameters.AddWithValue("@Req1", (object) ValueReq1);
              if (Req2 != null)
                command.Parameters.AddWithValue("@Req2", valueReq2);
              if (obj1 != null && Req2 == null)
                ((DbCommand) command).CommandText = $"UPDATE {obj0} SET {str1} WHERE {obj1} = ANY (@Req1)";
              else if (Req2 != null && obj1 == null)
                ((DbCommand) command).CommandText = $"UPDATE {obj0} SET {str1} WHERE {Req2}=@Req2";
              else if (Req2 != null && obj1 != null)
                ((DbCommand) command).CommandText = $"UPDATE {obj0} SET {str1} WHERE {obj1} = ANY (@Req1) AND {Req2}=@Req2";
              ((DbCommand) command).ExecuteNonQuery();
              ((Component) command).Dispose();
              ((DbConnection) npgsqlConnection).Close();
            }
          }
          return true;
        }
        catch (Exception ex)
        {
          // ISSUE: reference to a compiler-generated method
          CLogger.Class1.Print("[AllUtils.UpdateDB5] " + ex.Message, LoggerType.Error, ex);
          return false;
        }
      }
    }
    return false;
  }

  public static bool UpdateDB(
    [In] string obj0,
    [In] string obj1,
    object ValueReq1,
    string Req2,
    object ValueReq2,
    string COLUMNS,
    params object VALUES)
  {
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        using (NpgsqlCommand command = npgsqlConnection.CreateCommand())
        {
          ((DbConnection) npgsqlConnection).Open();
          ((DbCommand) command).CommandType = CommandType.Text;
          command.Parameters.AddWithValue("@Value", ValueReq1);
          if (Req2 != null)
            command.Parameters.AddWithValue("@Req1", ValueReq2);
          if (COLUMNS != null)
            command.Parameters.AddWithValue("@Req2", VALUES);
          if (Req2 != null && COLUMNS == null)
            ((DbCommand) command).CommandText = $"UPDATE {obj0} SET {obj1}=@Value WHERE {Req2}=@Req1";
          else if (COLUMNS != null && Req2 == null)
            ((DbCommand) command).CommandText = $"UPDATE {obj0} SET {obj1}=@Value WHERE {COLUMNS}=@Req2";
          else if (COLUMNS != null && Req2 != null)
            ((DbCommand) command).CommandText = $"UPDATE {obj0} SET {obj1}=@Value WHERE {Req2}=@Req1 AND {COLUMNS}=@Req2";
          ((DbCommand) command).ExecuteNonQuery();
          ((Component) command).Dispose();
          ((DbConnection) npgsqlConnection).Close();
        }
      }
      return true;
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("[AllUtils.UpdateDB6] " + ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  public static bool DeleteDB([In] string obj0, [In] string obj1, object VALUE)
  {
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        using (NpgsqlCommand command = npgsqlConnection.CreateCommand())
        {
          ((DbConnection) npgsqlConnection).Open();
          ((DbCommand) command).CommandType = CommandType.Text;
          command.Parameters.AddWithValue("@Req1", VALUE);
          ((DbCommand) command).CommandText = $"DELETE FROM {obj0} WHERE {obj1}=@Req1";
          ((DbCommand) command).ExecuteNonQuery();
          ((Component) command).Dispose();
          ((Component) npgsqlConnection).Dispose();
          ((DbConnection) npgsqlConnection).Close();
        }
      }
      return true;
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  public static bool DeleteDB([In] string obj0, [In] string obj1, [In] object obj2, [In] string obj3, [In] object obj4)
  {
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        using (NpgsqlCommand command = npgsqlConnection.CreateCommand())
        {
          ((DbConnection) npgsqlConnection).Open();
          ((DbCommand) command).CommandType = CommandType.Text;
          if (obj1 != null)
            command.Parameters.AddWithValue("@Req1", obj2);
          if (obj3 != null)
            command.Parameters.AddWithValue("@Req2", obj4);
          if (obj1 != null && obj3 == null)
            ((DbCommand) command).CommandText = $"DELETE FROM {obj0} WHERE {obj1}=@Req1";
          else if (obj3 != null && obj1 == null)
            ((DbCommand) command).CommandText = $"DELETE FROM {obj0} WHERE {obj3}=@Req2";
          else if (obj3 != null && obj1 != null)
            ((DbCommand) command).CommandText = $"DELETE FROM {obj0} WHERE {obj1}=@Req1 AND {obj3}=@Req2";
          ((DbCommand) command).ExecuteNonQuery();
          ((Component) command).Dispose();
          ((Component) npgsqlConnection).Dispose();
          ((DbConnection) npgsqlConnection).Close();
        }
      }
      return true;
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  public static bool DeleteDB(
    string TABEL,
    string Req1,
    object[] ValueReq1,
    string Req2,
    object ValueReq2)
  {
    if (ValueReq1.Length == 0)
      return false;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        using (NpgsqlCommand command = npgsqlConnection.CreateCommand())
        {
          ((DbConnection) npgsqlConnection).Open();
          ((DbCommand) command).CommandType = CommandType.Text;
          List<string> stringList = new List<string>();
          for (int index = 0; index < ValueReq1.Length; ++index)
          {
            object obj = ValueReq1[index];
            string parameterName = "@Value" + index.ToString();
            command.Parameters.AddWithValue(parameterName, obj);
            stringList.Add(parameterName);
          }
          string str = string.Join(",", stringList.ToArray());
          command.Parameters.AddWithValue("@Req2", ValueReq2);
          ((DbCommand) command).CommandText = $"DELETE FROM {TABEL} WHERE {Req1} in ({str}) AND {Req2}=@Req2";
          ((DbCommand) command).ExecuteNonQuery();
          ((Component) command).Dispose();
          ((Component) npgsqlConnection).Dispose();
          ((DbConnection) npgsqlConnection).Close();
        }
      }
      return true;
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  public static uint GetPlayerStatus(AccountStatus TABEL, bool Req1)
  {
    FriendState Req2;
    int num1;
    int num2;
    int ValueReq1;
    ComDiv.GetPlayerLocation(TABEL, Req1, ref Req2, ref num1, ref num2, ref ValueReq1);
    return ComDiv.GetPlayerStatus(num1, num2, ValueReq1, (int) Req2);
  }

  public static uint GetPlayerStatus([In] int obj0, [In] int obj1, int ValueReq1, int Req2)
  {
    int num1 = (Req2 & (int) byte.MaxValue) << 28;
    int num2 = (ValueReq1 & (int) byte.MaxValue) << 20;
    int num3 = (obj1 & (int) byte.MaxValue) << 12;
    int num4 = obj0 & 4095 /*0x0FFF*/;
    int num5 = num2;
    return (uint) (num1 | num5 | num3 | num4);
  }

  public static ulong GetPlayerStatus(
    [In] int obj0,
    int isOnline,
    int serverId,
    int stateId,
    [In] int obj4)
  {
    long num1 = ((long) obj0 & (long) uint.MaxValue) << 32 /*0x20*/;
    long num2 = (long) ((obj4 & (int) byte.MaxValue) << 28);
    long num3 = (long) ((stateId & (int) byte.MaxValue) << 20);
    long num4 = (long) ((serverId & (int) byte.MaxValue) << 12);
    long num5 = (long) (isOnline & 4095 /*0x0FFF*/);
    long num6 = num2;
    return (ulong) (num1 | num6 | num3 | num4 | num5);
  }

  public static ulong GetClanStatus(AccountStatus clanFId, bool roomId)
  {
    FriendState state;
    int roomId1;
    int channelId;
    int serverId;
    int num;
    ComDiv.GetPlayerLocation(clanFId, roomId, out state, out roomId1, out channelId, out serverId, ref num);
    return ComDiv.GetPlayerStatus(num, roomId1, channelId, serverId, (int) state);
  }

  public static ulong GetClanStatus([In] FriendState obj0)
  {
    return ComDiv.GetPlayerStatus(0, 0, 0, 0, (int) obj0);
  }

  public static uint GetFriendStatus([In] FriendModel obj0)
  {
    PlayerInfo playerInfo = ((MessageModel) obj0).get_Info();
    if (playerInfo == null)
      return 0;
    FriendState Req2 = FriendState.None;
    int ValueReq1 = 0;
    int num1 = 0;
    int num2 = 0;
    if (obj0.Removed)
      Req2 = FriendState.Offline;
    else if (obj0.State > 0)
      Req2 = (FriendState) obj0.State;
    else
      ComDiv.GetPlayerLocation(playerInfo.Status, playerInfo.IsOnline, ref Req2, ref num2, ref num1, ref ValueReq1);
    return ComDiv.GetPlayerStatus(num2, num1, ValueReq1, (int) Req2);
  }

  public static uint GetFriendStatus([In] FriendModel obj0, [In] FriendState obj1)
  {
    PlayerInfo playerInfo = ((MessageModel) obj0).get_Info();
    if (playerInfo == null)
      return 0;
    FriendState Req2 = obj1;
    int ValueReq1 = 0;
    int num1 = 0;
    int num2 = 0;
    if (obj0.Removed)
      Req2 = FriendState.Offline;
    else if (obj0.State > 0)
      Req2 = (FriendState) obj0.State;
    else if (obj1 == FriendState.None)
      ComDiv.GetPlayerLocation(playerInfo.Status, playerInfo.IsOnline, ref Req2, ref num2, ref num1, ref ValueReq1);
    return ComDiv.GetPlayerStatus(num2, num1, ValueReq1, (int) Req2);
  }

  public static void GetPlayerLocation(
    [In] AccountStatus obj0,
    bool isOnline,
    [In] ref FriendState obj2,
    [In] ref int obj3,
    [In] ref int obj4,
    [In] ref int obj5)
  {
    obj3 = 0;
    obj4 = 0;
    obj5 = 0;
    if (isOnline)
    {
      if (obj0.RoomId != byte.MaxValue)
      {
        obj3 = (int) obj0.RoomId;
        obj4 = (int) obj0.ChannelId;
        obj2 = FriendState.Room;
      }
      else if (obj0.RoomId == byte.MaxValue && obj0.ChannelId != byte.MaxValue)
      {
        obj4 = (int) obj0.ChannelId;
        obj2 = FriendState.Lobby;
      }
      else
        obj2 = obj0.RoomId != byte.MaxValue || obj0.ChannelId != byte.MaxValue ? FriendState.Offline : FriendState.Online;
      if (obj0.ServerId == byte.MaxValue)
        return;
      obj5 = (int) obj0.ServerId;
    }
    else
      obj2 = FriendState.Offline;
  }

  public static void GetPlayerLocation(
    [In] AccountStatus obj0,
    bool isOnline,
    out FriendState state,
    out int roomId,
    out int channelId,
    out int serverId,
    [In] ref int obj6)
  {
    roomId = 0;
    channelId = 0;
    serverId = 0;
    obj6 = 0;
    if (isOnline)
    {
      if (obj0.RoomId != byte.MaxValue)
      {
        roomId = (int) obj0.RoomId;
        channelId = (int) obj0.ChannelId;
        state = FriendState.Room;
      }
      else if ((obj0.ClanMatchId != byte.MaxValue || obj0.RoomId == byte.MaxValue) && obj0.ChannelId != byte.MaxValue)
      {
        channelId = (int) obj0.ChannelId;
        state = FriendState.Lobby;
      }
      else
        state = obj0.RoomId != byte.MaxValue || obj0.ChannelId != byte.MaxValue ? FriendState.Offline : FriendState.Online;
      if (obj0.ServerId != byte.MaxValue)
        serverId = (int) obj0.ServerId;
      if (obj0.ClanMatchId == byte.MaxValue)
        return;
      obj6 = (int) obj0.ClanMatchId + 1;
    }
    else
      state = FriendState.Offline;
  }

  public static ushort GetMissionCardFlags([In] int obj0, [In] int obj1, [Out] byte[] state)
  {
    if (obj0 == 0)
      return 0;
    int missionCardFlags = 0;
    foreach (MissionCardModel card in MissionCardRAW.GetCards(obj0, obj1))
    {
      if ((int) state[((MissionItemAward) card).get_ArrayIdx()] >= card.MissionLimit)
        missionCardFlags |= ((MissionItemAward) card).get_Flag();
    }
    return (ushort) missionCardFlags;
  }

  public static byte[] GetMissionCardFlags([In] int obj0, [In] byte[] obj1)
  {
    if (obj0 == 0)
      return new byte[20];
    List<MissionCardModel> cards = MissionCardRAW.GetCards(obj0);
    if (cards.Count == 0)
      return new byte[20];
    using (SyncServerPacket syncServerPacket = new SyncServerPacket(20L))
    {
      int num = 0;
      for (int index = 0; index < 10; ++index)
      {
        foreach (MissionCardModel card in MissionCardRAW.GetCards(cards, index))
        {
          if ((int) obj1[((MissionItemAward) card).get_ArrayIdx()] >= card.MissionLimit)
            num |= ((MissionItemAward) card).get_Flag();
        }
        syncServerPacket.WriteH((ushort) num);
        num = 0;
      }
      return syncServerPacket.ToArray();
    }
  }

  public static int CountDB(string missionId)
  {
    int num = 0;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        ((DbCommand) command).CommandText = missionId;
        num = Convert.ToInt32(((DbCommand) command).ExecuteScalar());
        ((Component) command).Dispose();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("[QuerySQL.CountDB] " + ex.Message, LoggerType.Error, ex);
    }
    return num;
  }

  public static bool ValidateAllPlayersAccount()
  {
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        ((DbCommand) command).CommandType = CommandType.Text;
        ((DbCommand) command).CommandText = $"UPDATE accounts SET online = {(System.ValueType) false} WHERE online = {(System.ValueType) true}";
        ((DbCommand) command).ExecuteNonQuery();
        ((Component) command).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
      return true;
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  public static uint GenStockId([In] int obj0) => BitConverter.ToUInt32(ComDiv.smethod_2(obj0), 0);

  private static byte[] smethod_2([In] int obj0)
  {
    byte[] bytes = BitConverter.GetBytes(obj0);
    bytes[3] = (byte) 64 /*0x40*/;
    return bytes;
  }

  public static T NextOf<T>(IList<T> missionId, T arrayList)
  {
    int num = missionId.IndexOf(arrayList);
    return missionId[num == missionId.Count - 1 ? 0 : num + 1];
  }

  public static T ParseEnum<T>(string CommandArgument)
  {
    return (T) System.Enum.Parse(typeof (T), CommandArgument, true);
  }
}
