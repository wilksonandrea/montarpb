// Decompiled with JetBrains decompiler
// Type: Server.Match.Data.XML.MapStructureXML
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.SharpDX;
using Server.Match.Data.Models;
using Server.Match.Data.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;

#nullable disable
namespace Server.Match.Data.XML;

public class MapStructureXML
{
  public static List<MapModel> Maps;

  public static void Reload()
  {
    ItemStatisticXML.Stats.Clear();
    ItemStatisticXML.Load();
  }

  public static ItemsStatistic GetItemStats([In] int obj0)
  {
    lock (ItemStatisticXML.Stats)
    {
      foreach (ItemsStatistic stat in ItemStatisticXML.Stats)
      {
        if (stat.Id == obj0)
          return stat;
      }
      return (ItemsStatistic) null;
    }
  }

  private static void smethod_0(string S)
  {
    XmlDocument xmlDocument = new XmlDocument();
    using (FileStream inStream = new FileStream(S, FileMode.Open))
    {
      if (inStream.Length == 0L)
      {
        CLogger.Print("File is empty: " + S, LoggerType.Warning, (Exception) null);
      }
      else
      {
        try
        {
          xmlDocument.Load((Stream) inStream);
          for (XmlNode xmlNode1 = xmlDocument.FirstChild; xmlNode1 != null; xmlNode1 = xmlNode1.NextSibling)
          {
            if ("List".Equals(xmlNode1.Name))
            {
              for (XmlNode xmlNode2 = xmlNode1.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
              {
                if ("Statistic".Equals(xmlNode2.Name))
                {
                  XmlNamedNodeMap attributes = (XmlNamedNodeMap) xmlNode2.Attributes;
                  ActionModel actionModel = new ActionModel();
                  ((ItemsStatistic) actionModel).Id = int.Parse(attributes.GetNamedItem("Id").Value);
                  ((ItemsStatistic) actionModel).Name = attributes.GetNamedItem("Name").Value;
                  ((ItemsStatistic) actionModel).BulletLoaded = int.Parse(attributes.GetNamedItem("LoadedBullet").Value);
                  ((ItemsStatistic) actionModel).BulletTotal = int.Parse(attributes.GetNamedItem("TotalBullet").Value);
                  ((ItemsStatistic) actionModel).Damage = int.Parse(attributes.GetNamedItem("Damage").Value);
                  actionModel.set_FireDelay(float.Parse(attributes.GetNamedItem("FireDelay").Value));
                  ((ItemsStatistic) actionModel).HelmetPenetrate = int.Parse(attributes.GetNamedItem("HelmetPenetrate").Value);
                  actionModel.set_Range(float.Parse(attributes.GetNamedItem("Range").Value));
                  ItemsStatistic itemsStatistic = (ItemsStatistic) actionModel;
                  ItemStatisticXML.Stats.Add(itemsStatistic);
                }
              }
            }
          }
        }
        catch (XmlException ex)
        {
          CLogger.Print(ex.Message, LoggerType.Error, (Exception) ex);
        }
      }
      inStream.Dispose();
      inStream.Close();
    }
  }

  static MapStructureXML() => ItemStatisticXML.Stats = new List<ItemsStatistic>();

  public static void Load()
  {
    string str = "Data/Match/MapStructure.xml";
    if (File.Exists(str))
      MapStructureXML.smethod_0(str);
    else
      CLogger.Print("File not found: " + str, LoggerType.Warning, (Exception) null);
  }

  public static void Reload()
  {
    MapStructureXML.Maps.Clear();
    MapStructureXML.Load();
  }

  public static MapModel GetMapId([In] int obj0)
  {
    lock (MapStructureXML.Maps)
    {
      foreach (MapModel map in MapStructureXML.Maps)
      {
        if (map.Id == obj0)
          return map;
      }
      return (MapModel) null;
    }
  }

  public static void SetObjectives(ObjectModel CharaId, [In] RoomModel obj1)
  {
    if (CharaId.UltraSync == 0)
      return;
    if (CharaId.UltraSync != 1 && CharaId.UltraSync != 3)
    {
      if (CharaId.UltraSync != 2 && CharaId.UltraSync != 4)
        return;
      obj1.Bar2 = CharaId.Life;
      obj1.Default2 = obj1.Bar2;
    }
    else
    {
      obj1.Bar1 = CharaId.Life;
      obj1.Default1 = obj1.Bar1;
    }
  }

  private static void smethod_0(string ItemId)
  {
    XmlDocument xmlDocument = new XmlDocument();
    using (FileStream inStream = new FileStream(ItemId, FileMode.Open))
    {
      if (inStream.Length == 0L)
      {
        CLogger.Print("File is empty: " + ItemId, LoggerType.Warning, (Exception) null);
      }
      else
      {
        try
        {
          xmlDocument.Load((Stream) inStream);
          for (XmlNode xmlNode = xmlDocument.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
          {
            if ("List".Equals(xmlNode.Name))
            {
              for (XmlNode string_0 = xmlNode.FirstChild; string_0 != null; string_0 = string_0.NextSibling)
              {
                if ("Map".Equals(string_0.Name))
                {
                  XmlNamedNodeMap attributes = (XmlNamedNodeMap) string_0.Attributes;
                  ObjectHitInfo objectHitInfo = new ObjectHitInfo();
                  ((MapModel) objectHitInfo).Id = int.Parse(attributes.GetNamedItem("Id").Value);
                  objectHitInfo.set_Objects(new List<ObjectModel>());
                  objectHitInfo.set_Bombs(new List<BombPosition>());
                  MapModel room = (MapModel) objectHitInfo;
                  MapStructureXML.smethod_1(string_0, room);
                  AllUtils.smethod_2(string_0, room);
                  MapStructureXML.Maps.Add(room);
                }
              }
            }
          }
        }
        catch (XmlException ex)
        {
          CLogger.Print(ex.Message, LoggerType.Error, (Exception) ex);
        }
      }
      inStream.Dispose();
      inStream.Close();
    }
  }

  private static void smethod_1(XmlNode string_0, [In] MapModel obj1)
  {
    for (XmlNode xmlNode1 = string_0.FirstChild; xmlNode1 != null; xmlNode1 = xmlNode1.NextSibling)
    {
      if ("BombPositions".Equals(xmlNode1.Name))
      {
        for (XmlNode xmlNode2 = xmlNode1.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
        {
          if ("Bomb".Equals(xmlNode2.Name))
          {
            XmlNamedNodeMap attributes = (XmlNamedNodeMap) xmlNode2.Attributes;
            CharaModel charaModel = new CharaModel();
            ((BombPosition) charaModel).X = float.Parse(attributes.GetNamedItem("X").Value);
            ((BombPosition) charaModel).Y = float.Parse(attributes.GetNamedItem("Y").Value);
            ((BombPosition) charaModel).Z = float.Parse(attributes.GetNamedItem("Z").Value);
            BombPosition bombPosition = (BombPosition) charaModel;
            ((CharaModel) bombPosition).set_Position(new Half3(bombPosition.X, bombPosition.Y, bombPosition.Z));
            if ((double) bombPosition.X == 0.0 && (double) bombPosition.Y == 0.0 && (double) bombPosition.Z == 0.0)
              ((CharaModel) bombPosition).set_EveryWhere(true);
            ((ObjectHitInfo) obj1).get_Bombs().Add(bombPosition);
          }
        }
      }
    }
  }
}
