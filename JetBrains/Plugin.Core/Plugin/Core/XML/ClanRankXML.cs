// Decompiled with JetBrains decompiler
// Type: Plugin.Core.XML.ClanRankXML
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;

#nullable disable
namespace Plugin.Core.XML;

public class ClanRankXML
{
  private static List<RankModel> list_0;

  private static void smethod_5(XmlNode string_0, [In] PCCafeModel obj1)
  {
    for (XmlNode xmlNode1 = string_0.FirstChild; xmlNode1 != null; xmlNode1 = xmlNode1.NextSibling)
    {
      if ("Rewards".Equals(xmlNode1.Name))
      {
        for (XmlNode xmlNode2 = xmlNode1.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
        {
          if ("Item".Equals(xmlNode2.Name))
          {
            XmlNamedNodeMap attributes = (XmlNamedNodeMap) xmlNode2.Attributes;
            int num = int.Parse(attributes.GetNamedItem("Id").Value);
            PlayerBonus playerBonus = new PlayerBonus(num);
            ((ItemsModel) playerBonus).ObjectId = (long) ComDiv.ValidateStockId(num);
            ((ItemsModel) playerBonus).Name = attributes.GetNamedItem("Name").Value;
            ((ItemsModel) playerBonus).Count = 1U;
            ((ItemsModel) playerBonus).Equip = ItemEquipType.CafePC;
            ItemsModel itemsModel = (ItemsModel) playerBonus;
            ClanRankXML.smethod_6(obj1, itemsModel);
          }
        }
      }
    }
  }

  private static void smethod_6(PCCafeModel Type, [In] ItemsModel obj1)
  {
    lock (((PlayerBattlepass) Type).get_Rewards())
    {
      if (((PlayerBattlepass) Type).get_Rewards().ContainsKey(Type.Type))
        ((PlayerBattlepass) Type).get_Rewards()[Type.Type].Add(obj1);
      else
        ((PlayerBattlepass) Type).get_Rewards().Add(Type.Type, new List<ItemsModel>()
        {
          obj1
        });
    }
  }

  private static void smethod_7(string string_0)
  {
    XmlDocument xmlDocument = new XmlDocument();
    using (FileStream inStream = new FileStream(string_0, FileMode.Open))
    {
      if (inStream.Length == 0L)
      {
        // ISSUE: reference to a compiler-generated method
        CLogger.Class1.Print("File is empty: " + string_0, LoggerType.Warning, (Exception) null);
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
                if ("Item".Equals(xmlNode2.Name))
                {
                  XmlNamedNodeMap attributes = (XmlNamedNodeMap) xmlNode2.Attributes;
                  PlayerBonus playerBonus = new PlayerBonus(int.Parse(attributes.GetNamedItem("Id").Value));
                  ((ItemsModel) playerBonus).Name = attributes.GetNamedItem("Name").Value;
                  ((ItemsModel) playerBonus).Count = uint.Parse(attributes.GetNamedItem("Count").Value);
                  ((ItemsModel) playerBonus).Equip = ItemEquipType.Durable;
                  ItemsModel itemsModel = (ItemsModel) playerBonus;
                  TemplatePackXML.Awards.Add(itemsModel);
                }
              }
            }
          }
        }
        catch (XmlException ex)
        {
          // ISSUE: reference to a compiler-generated method
          CLogger.Class1.Print(ex.Message, LoggerType.Error, (Exception) ex);
        }
      }
      inStream.Dispose();
      inStream.Close();
    }
  }

  static ClanRankXML()
  {
    TemplatePackXML.Basics = new List<ItemsModel>();
    TemplatePackXML.Awards = new List<ItemsModel>();
    TemplatePackXML.Cafes = new List<PCCafeModel>();
  }

  public static void Load()
  {
    string path = "Data/Ranks/Clan.xml";
    if (File.Exists(path))
    {
      DirectLibraryXML.smethod_0(path);
    }
    else
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("File not found: " + path, LoggerType.Warning, (Exception) null);
    }
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Plugin Loaded: {ClanRankXML.list_0.Count} Clan Ranks", LoggerType.Info, (Exception) null);
  }
}
