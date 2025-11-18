// Decompiled with JetBrains decompiler
// Type: Plugin.Core.XML.TitleSystemXML
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

public class TitleSystemXML
{
  private static List<TitleModel> list_0;

  public static bool Contains(int string_0, int bool_0)
  {
    if (bool_0 == 0)
      return false;
    foreach (TitleAward award in TitleAwardXML.Awards)
    {
      if (((TitleModel) award).get_Id() == string_0 && ((TitleModel) award).get_Item().Id == bool_0)
        return true;
    }
    return false;
  }

  private static void smethod_0(string string_0)
  {
    XmlDocument xmlDocument = new XmlDocument();
    using (FileStream inStream = new FileStream(string_0, FileMode.Open))
    {
      if (inStream.Length > 0L)
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
                if ("Title".Equals(xmlNode2.Name))
                {
                  int int_0 = int.Parse(xmlNode2.Attributes.GetNamedItem("Id").Value);
                  TitleSystemXML.smethod_1(xmlNode2, int_0);
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

  private static void smethod_1([In] XmlNode obj0, int int_0)
  {
    for (XmlNode xmlNode1 = obj0.FirstChild; xmlNode1 != null; xmlNode1 = xmlNode1.NextSibling)
    {
      if ("Rewards".Equals(xmlNode1.Name))
      {
        for (XmlNode xmlNode2 = xmlNode1.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
        {
          if ("Item".Equals(xmlNode2.Name))
          {
            XmlNamedNodeMap attributes = (XmlNamedNodeMap) xmlNode2.Attributes;
            TitleModel titleModel = new TitleModel();
            titleModel.set_Id(int_0);
            TitleAward titleAward = (TitleAward) titleModel;
            if (titleAward != null)
            {
              int num = int.Parse(attributes.GetNamedItem("Id").Value);
              PlayerBonus playerBonus = new PlayerBonus(num);
              ((ItemsModel) playerBonus).Name = attributes.GetNamedItem("Name").Value;
              ((ItemsModel) playerBonus).Count = uint.Parse(attributes.GetNamedItem("Count").Value);
              ((ItemsModel) playerBonus).Equip = (ItemEquipType) int.Parse(attributes.GetNamedItem("Equip").Value);
              ItemsModel itemsModel = (ItemsModel) playerBonus;
              if (itemsModel.Equip == ItemEquipType.Permanent)
                itemsModel.ObjectId = (long) ComDiv.ValidateStockId(num);
              ((TitleModel) titleAward).set_Item(itemsModel);
              TitleAwardXML.Awards.Add(titleAward);
            }
          }
        }
      }
    }
  }

  static TitleSystemXML() => TitleAwardXML.Awards = new List<TitleAward>();

  public static void Load()
  {
    string path = "Data/Titles/System.xml";
    if (File.Exists(path))
    {
      Bitwise.smethod_0(path);
    }
    else
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("File not found: " + path, LoggerType.Warning, (Exception) null);
    }
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Plugin Loaded: {TitleSystemXML.list_0.Count} Title System", LoggerType.Info, (Exception) null);
  }

  public static void Reload()
  {
    TitleSystemXML.list_0.Clear();
    TitleSystemXML.Load();
  }

  public static TitleModel GetTitle(int TitleId, bool ItemId)
  {
    if (TitleId == 0)
      return !ItemId ? (TitleModel) new TRuleModel() : (TitleModel) null;
    foreach (TitleModel title in TitleSystemXML.list_0)
    {
      if (title.Id == TitleId)
        return title;
    }
    return (TitleModel) null;
  }
}
