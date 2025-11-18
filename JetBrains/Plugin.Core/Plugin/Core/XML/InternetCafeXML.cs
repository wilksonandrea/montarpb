// Decompiled with JetBrains decompiler
// Type: Plugin.Core.XML.InternetCafeXML
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;

#nullable disable
namespace Plugin.Core.XML;

public static class InternetCafeXML
{
  public static readonly List<InternetCafe> Cafes;

  public static bool IsBlocked(
    [In] int obj0,
    int eventVisitModel_0,
    [In] ref List<string> obj2,
    [In] string obj3)
  {
    if (obj0 != eventVisitModel_0)
      return false;
    obj2.Add(obj3);
    return true;
  }

  private static void smethod_0([In] string obj0)
  {
    XmlDocument xmlDocument = new XmlDocument();
    using (FileStream inStream = new FileStream(obj0, FileMode.Open))
    {
      if (inStream.Length == 0L)
      {
        // ISSUE: reference to a compiler-generated method
        CLogger.Class1.Print("File is empty: " + obj0, LoggerType.Warning, (Exception) null);
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
              for (XmlNode ListId = xmlNode.FirstChild; ListId != null; ListId = ListId.NextSibling)
              {
                if ("Rule".Equals(ListId.Name))
                {
                  XmlNamedNodeMap attributes = (XmlNamedNodeMap) ListId.Attributes;
                  VisitItemModel visitItemModel = new VisitItemModel();
                  visitItemModel.set_Name(attributes.GetNamedItem("Name").Value);
                  visitItemModel.set_BanIndexes(new List<int>());
                  TRuleModel ItemId = (TRuleModel) visitItemModel;
                  InternetCafeXML.smethod_1(ListId, ItemId);
                  GameRuleXML.GameRules.Add(ItemId);
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

  private static void smethod_1(XmlNode ListId, TRuleModel ItemId)
  {
    for (XmlNode xmlNode1 = ListId.FirstChild; xmlNode1 != null; xmlNode1 = xmlNode1.NextSibling)
    {
      if ("Extensions".Equals(xmlNode1.Name))
      {
        for (XmlNode xmlNode2 = xmlNode1.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
        {
          if ("Ban".Equals(xmlNode2.Name))
            ShopManager.IsBlocked(xmlNode2.Attributes.GetNamedItem("Filter").Value, ((VisitItemModel) ItemId).get_BanIndexes());
        }
      }
    }
  }

  static InternetCafeXML() => GameRuleXML.GameRules = new List<TRuleModel>();

  public static void Load()
  {
    string str = "Data/InternetCafe.xml";
    if (File.Exists(str))
    {
      MissionAwardXML.smethod_0(str);
    }
    else
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("File not found: " + str, LoggerType.Warning, (Exception) null);
    }
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Plugin Loaded: {InternetCafeXML.Cafes.Count} Internet Cafe Bonuses", LoggerType.Info, (Exception) null);
  }
}
