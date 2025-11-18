// Decompiled with JetBrains decompiler
// Type: Server.Match.Data.Utils.AllUtils
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.SharpDX;
using Plugin.Core.Utility;
using Server.Match.Data.Enums;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;
using Server.Match.Data.XML;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml;

#nullable disable
namespace Server.Match.Data.Utils;

public static class AllUtils
{
  private static void smethod_2(XmlNode obj, MapModel room)
  {
    for (XmlNode xmlNode = obj.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
    {
      if ("Objects".Equals(xmlNode.Name))
      {
        for (XmlNode string_0 = xmlNode.FirstChild; string_0 != null; string_0 = string_0.NextSibling)
        {
          if ("Obj".Equals(string_0.Name))
          {
            XmlNamedNodeMap attributes = (XmlNamedNodeMap) string_0.Attributes;
            PacketModel packetModel = new PacketModel(bool.Parse(attributes.GetNamedItem("NeedSync").Value));
            ((ObjectModel) packetModel).Id = int.Parse(attributes.GetNamedItem("Id").Value);
            ((ObjectModel) packetModel).Life = int.Parse(attributes.GetNamedItem("Life").Value);
            ((ObjectModel) packetModel).Animation = int.Parse(attributes.GetNamedItem("Animation").Value);
            ObjectModel mapModel_0 = (ObjectModel) packetModel;
            if (mapModel_0.Life > -1)
              mapModel_0.Destroyable = true;
            if (mapModel_0.Animation > (int) byte.MaxValue)
            {
              if (mapModel_0.Animation == 256 /*0x0100*/)
                mapModel_0.UltraSync = 1;
              else if (mapModel_0.Animation == 257)
                mapModel_0.UltraSync = 2;
              else if (mapModel_0.Animation == 258)
                mapModel_0.UltraSync = 3;
              else if (mapModel_0.Animation == 259)
                mapModel_0.UltraSync = 4;
              mapModel_0.Animation = (int) byte.MaxValue;
            }
            AllUtils.smethod_3(string_0, mapModel_0);
            AllUtils.smethod_4(string_0, mapModel_0);
            room.Objects.Add(mapModel_0);
          }
        }
      }
    }
  }

  private static void smethod_3(XmlNode string_0, [In] ObjectModel obj1)
  {
    for (XmlNode xmlNode1 = string_0.FirstChild; xmlNode1 != null; xmlNode1 = xmlNode1.NextSibling)
    {
      if ("Anims".Equals(xmlNode1.Name))
      {
        for (XmlNode xmlNode2 = xmlNode1.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
        {
          if ("Sync".Equals(xmlNode2.Name))
          {
            XmlNamedNodeMap attributes = (XmlNamedNodeMap) xmlNode2.Attributes;
            AssistServerData assistServerData = new AssistServerData();
            ((AnimModel) assistServerData).Id = int.Parse(attributes.GetNamedItem("Id").Value);
            assistServerData.set_Duration(float.Parse(attributes.GetNamedItem("Date").Value));
            ((AnimModel) assistServerData).NextAnim = int.Parse(attributes.GetNamedItem("Next").Value);
            ((AnimModel) assistServerData).OtherObj = int.Parse(attributes.GetNamedItem("OtherOBJ").Value);
            assistServerData.set_OtherAnim(int.Parse(attributes.GetNamedItem("OtherANIM").Value));
            AnimModel animModel = (AnimModel) assistServerData;
            if (animModel.Id == 0)
              obj1.NoInstaSync = true;
            if (animModel.Id != (int) byte.MaxValue)
              obj1.UpdateId = 3;
            obj1.Animations.Add(animModel);
          }
        }
      }
    }
  }

  private static void smethod_4([In] XmlNode obj0, ObjectModel mapModel_0)
  {
    for (XmlNode xmlNode1 = obj0.FirstChild; xmlNode1 != null; xmlNode1 = xmlNode1.NextSibling)
    {
      if ("DestroyEffects".Equals(xmlNode1.Name))
      {
        for (XmlNode xmlNode2 = xmlNode1.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
        {
          if ("Effect".Equals(xmlNode2.Name))
          {
            XmlNamedNodeMap attributes = (XmlNamedNodeMap) xmlNode2.Attributes;
            MapModel mapModel = new MapModel();
            mapModel.set_Id(int.Parse(attributes.GetNamedItem("Id").Value));
            mapModel.set_Life(int.Parse(attributes.GetNamedItem("Percent").Value));
            DeffectModel deffectModel = (DeffectModel) mapModel;
            mapModel_0.Effects.Add(deffectModel);
          }
        }
      }
    }
  }

  static AllUtils() => MapStructureXML.Maps = new List<MapModel>();

  public static float GetDuration([In] DateTime obj0)
  {
    return (float) (DateTimeUtil.Now() - obj0).TotalSeconds;
  }

  public static ItemClass ItemClassified(ClassType xmlNode_0)
  {
    ItemClass itemClass = ItemClass.Unknown;
    switch (xmlNode_0)
    {
      case ClassType.Knife:
      case ClassType.DualKnife:
      case ClassType.Knuckle:
        itemClass = ItemClass.Melee;
        break;
      case ClassType.HandGun:
      case ClassType.CIC:
      case ClassType.DualHandGun:
        itemClass = ItemClass.Secondary;
        break;
      case ClassType.Assault:
        itemClass = ItemClass.Primary;
        break;
      case ClassType.SMG:
      case ClassType.DualSMG:
        itemClass = ItemClass.Primary;
        break;
      case ClassType.Sniper:
        itemClass = ItemClass.Primary;
        break;
      case ClassType.Shotgun:
      case ClassType.DualShotgun:
        itemClass = ItemClass.Primary;
        break;
      case ClassType.ThrowingGrenade:
        itemClass = ItemClass.Explosive;
        break;
      case ClassType.ThrowingSpecial:
        itemClass = ItemClass.Special;
        break;
      case ClassType.Machinegun:
        itemClass = ItemClass.Primary;
        break;
      case ClassType.Dino:
        itemClass = ItemClass.Unknown;
        break;
    }
    return itemClass;
  }

  public static ObjectType GetHitType([In] uint obj0) => (ObjectType) ((int) obj0 & 3);

  public static int GetHitWho(uint xmlNode_0) => (int) (xmlNode_0 >> 2) & 511 /*0x01FF*/;

  public static CharaHitPart GetHitPart([In] uint obj0)
  {
    return (CharaHitPart) ((int) (obj0 >> 11) & 63 /*0x3F*/);
  }

  public static int GetHitDamageBot(uint Date) => (int) (Date >> 20);

  public static int GetHitDamageNormal(uint ClassWeapon) => (int) (ClassWeapon >> 21);

  public static int GetHitHelmet(uint HitInfo) => (int) (HitInfo >> 17) & 7;

  public static CharaDeath GetCharaDeath(uint HitInfo) => (CharaDeath) ((int) HitInfo & 15);

  public static int GetKillerId(uint HitInfo) => (int) (HitInfo >> 11) & 511 /*0x01FF*/;

  public static int GetObjectType(uint HitInfo) => (int) (HitInfo >> 10) & 1;

  public static int GetRoomInfo(uint HitInfo, [In] int obj1)
  {
    switch (obj1)
    {
      case 0:
        return (int) HitInfo & 4095 /*0x0FFF*/;
      case 1:
        return (int) (HitInfo >> 12) & (int) byte.MaxValue;
      case 2:
        return (int) (HitInfo >> 20) & 4095 /*0x0FFF*/;
      default:
        return 0;
    }
  }

  public static int GetSeedInfo(uint HitInfo, [In] int obj1)
  {
    switch (obj1)
    {
      case 0:
        return (int) HitInfo & 4095 /*0x0FFF*/;
      case 1:
        return (int) (HitInfo >> 12) & (int) byte.MaxValue;
      case 2:
        return (int) (HitInfo >> 20) & 4095 /*0x0FFF*/;
      default:
        return 0;
    }
  }

  public static byte[] BaseWriteCode(
    int HitInfo,
    byte[] Type,
    int SlotId,
    [In] float obj3,
    [In] int obj4,
    [In] int obj5,
    [In] int obj6,
    [In] int obj7)
  {
    int num = (17 + Type.Length) % 6 + 1;
    byte[] MStream = Bitwise.Encrypt(Type, num);
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      syncServerPacket.WriteC((byte) HitInfo);
      syncServerPacket.WriteC((byte) SlotId);
      syncServerPacket.WriteT(obj3);
      syncServerPacket.WriteC((byte) obj4);
      syncServerPacket.WriteH((ushort) (17 + MStream.Length));
      syncServerPacket.WriteC((byte) obj5);
      syncServerPacket.WriteC((byte) obj6);
      syncServerPacket.WriteC((byte) obj7);
      syncServerPacket.WriteC((byte) 0);
      syncServerPacket.WriteD(0);
      syncServerPacket.WriteB(MStream);
      return syncServerPacket.ToArray();
    }
  }

  public static bool ValidateHitData([In] int obj0, [In] HitDataInfo obj1, [In] ref int obj2)
  {
    if (!ConfigLoader.AntiScript)
    {
      obj2 = obj0;
      return true;
    }
    ItemsStatistic itemStats = MapStructureXML.GetItemStats(obj1.WeaponId);
    if (itemStats == null)
    {
      CLogger.Print($"The Item Statistic was not found. Please add: {obj1.WeaponId} to config!", LoggerType.Warning, (Exception) null);
      obj2 = 0;
      return false;
    }
    ItemClass itemClass = AllUtils.ItemClassified(obj1.WeaponClass);
    float num1 = Vector3.Distance((Vector3) obj1.StartBullet, (Vector3) obj1.EndBullet);
    if (itemClass != ItemClass.Melee && (double) num1 > (double) ((ActionModel) itemStats).get_Range())
    {
      obj2 = 0;
      return false;
    }
    if (itemClass == ItemClass.Melee && (double) num1 > (double) ((ActionModel) itemStats).get_Range())
    {
      obj2 = 0;
      return false;
    }
    if (AllUtils.GetHitPart(obj1.HitIndex) != CharaHitPart.HEAD)
    {
      int num2 = itemStats.Damage + itemStats.Damage * 30 / 100;
      if (itemClass != ItemClass.Melee && obj0 > num2)
      {
        obj2 = 0;
        return false;
      }
      if (itemClass == ItemClass.Melee && obj0 > itemStats.Damage)
      {
        obj2 = 0;
        return false;
      }
    }
    obj2 = obj0;
    return true;
  }

  public static bool ValidateGrenadeHit([In] int obj0, [In] GrenadeHitInfo obj1, [In] ref int obj2)
  {
    if (!ConfigLoader.AntiScript)
    {
      obj2 = AllUtils.ItemClassified(obj1.WeaponClass) == ItemClass.Explosive ? obj0 * ConfigLoader.GrenateDamageMultipler : obj0;
      return true;
    }
    ItemsStatistic itemStats = MapStructureXML.GetItemStats(obj1.WeaponId);
    if (itemStats == null)
    {
      CLogger.Print($"The Item Statistic was not found. Please add: {obj1.WeaponId} to config!", LoggerType.Warning, (Exception) null);
      obj2 = 0;
      return false;
    }
    int num1 = (int) AllUtils.ItemClassified(obj1.WeaponClass);
    float num2 = Vector3.Distance((Vector3) obj1.FirePos, (Vector3) obj1.HitPos);
    if (num1 == 4)
    {
      if ((double) num2 > (double) ((ActionModel) itemStats).get_Range())
      {
        obj2 = 0;
        return false;
      }
      if (obj0 > itemStats.Damage)
      {
        obj2 = 0;
        return false;
      }
    }
    obj2 = AllUtils.ItemClassified(obj1.WeaponClass) == ItemClass.Explosive ? obj0 * ConfigLoader.GrenateDamageMultipler : obj0;
    return true;
  }

  public static void GetDecryptedData([In] PacketModel obj0)
  {
    try
    {
      if (obj0.Data.Length >= obj0.Length)
      {
        byte[] destinationArray1 = new byte[obj0.Length - 17];
        Array.Copy((Array) obj0.Data, 17, (Array) destinationArray1, 0, destinationArray1.Length);
        byte[] sourceArray = Bitwise.Decrypt(destinationArray1, obj0.Length % 6 + 1);
        byte[] destinationArray2 = new byte[sourceArray.Length - 9];
        Array.Copy((Array) sourceArray, (Array) destinationArray2, destinationArray2.Length);
        obj0.WithEndData = sourceArray;
        ((PlayerModel) obj0).set_WithoutEndData(destinationArray2);
      }
      else
        CLogger.Print($"Invalid packet size. (Packet.Data.Length >= Packet.Length): [ {obj0.Data.Length} | {obj0.Length} ]", LoggerType.Warning, (Exception) null);
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }
}
