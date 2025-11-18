// Decompiled with JetBrains decompiler
// Type: Plugin.Core.XML.TemplatePackXML
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

public class TemplatePackXML
{
  public static List<ItemsModel> Basics;
  public static List<ItemsModel> Awards;
  public static List<PCCafeModel> Cafes;

  public static void Reload()
  {
    SynchronizeXML.Servers.Clear();
    SynchronizeXML.Load();
  }

  public static Synchronize GetServer(int SeasonId)
  {
    if (SynchronizeXML.Servers.Count == 0)
      return (Synchronize) null;
    try
    {
      lock (SynchronizeXML.Servers)
      {
        foreach (Synchronize server in SynchronizeXML.Servers)
        {
          if (((TitleAward) server).get_RemotePort() == SeasonId)
            return server;
        }
        return (Synchronize) null;
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      return (Synchronize) null;
    }
  }

  private static void smethod_0(string string_0)
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
              XmlAttributeCollection attributes1 = xmlNode1.Attributes;
              for (XmlNode xmlNode2 = xmlNode1.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
              {
                if ("Sync".Equals(xmlNode2.Name))
                {
                  XmlNamedNodeMap attributes2 = (XmlNamedNodeMap) xmlNode2.Attributes;
                  TitleAward titleAward = new TitleAward(attributes2.GetNamedItem("Host").Value, int.Parse(attributes2.GetNamedItem("Port").Value));
                  titleAward.set_RemotePort(int.Parse(attributes2.GetNamedItem("RemotePort").Value));
                  Synchronize synchronize = (Synchronize) titleAward;
                  SynchronizeXML.Servers.Add(synchronize);
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

  static TemplatePackXML() => SynchronizeXML.Servers = new List<Synchronize>();

  public static void Load()
  {
    TemplatePackXML.smethod_0();
    TemplatePackXML.smethod_1();
    TemplatePackXML.smethod_2();
  }

  public static void Reload()
  {
    TemplatePackXML.Basics.Clear();
    TemplatePackXML.Awards.Clear();
    TemplatePackXML.Cafes.Clear();
    TemplatePackXML.Load();
  }

  private static void smethod_0()
  {
    string str = "Data/Temps/Basic.xml";
    if (File.Exists(str))
    {
      TemplatePackXML.smethod_3(str);
    }
    else
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("File not found: " + str, LoggerType.Warning, (Exception) null);
    }
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Plugin Loaded: {TemplatePackXML.Basics.Count} Basic Templates", LoggerType.Info, (Exception) null);
  }

  private static void smethod_1()
  {
    string str = "Data/Temps/CafePC.xml";
    if (File.Exists(str))
    {
      TemplatePackXML.smethod_4(str);
    }
    else
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("File not found: " + str, LoggerType.Warning, (Exception) null);
    }
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Plugin Loaded: {TemplatePackXML.Cafes.Count} PC Cafes", LoggerType.Info, (Exception) null);
  }

  private static void smethod_2()
  {
    string str = "Data/Temps/Award.xml";
    if (File.Exists(str))
    {
      ClanRankXML.smethod_7(str);
    }
    else
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("File not found: " + str, LoggerType.Warning, (Exception) null);
    }
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Plugin Loaded: {TemplatePackXML.Awards.Count} Award Templates", LoggerType.Info, (Exception) null);
  }

  public static PCCafeModel GetPCCafe(CafeEnum xmlNode_0)
  {
    lock (TemplatePackXML.Cafes)
    {
      foreach (PCCafeModel cafe in TemplatePackXML.Cafes)
      {
        if (cafe.Type == xmlNode_0)
          return cafe;
      }
      return (PCCafeModel) null;
    }
  }

  public static List<ItemsModel> GetPCCafeRewards([In] CafeEnum obj0)
  {
    PCCafeModel pcCafe = TemplatePackXML.GetPCCafe(obj0);
    if (pcCafe != null)
    {
      lock (((PlayerBattlepass) pcCafe).get_Rewards())
      {
        List<ItemsModel> pcCafeRewards;
        if (((PlayerBattlepass) pcCafe).get_Rewards().TryGetValue(obj0, out pcCafeRewards))
          return pcCafeRewards;
      }
    }
    return new List<ItemsModel>();
  }

  private static void smethod_3(string int_0)
  {
    XmlDocument xmlDocument = new XmlDocument();
    using (FileStream inStream = new FileStream(int_0, FileMode.Open))
    {
      if (inStream.Length == 0L)
      {
        // ISSUE: reference to a compiler-generated method
        CLogger.Class1.Print("File is empty: " + int_0, LoggerType.Warning, (Exception) null);
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
                  int num = int.Parse(attributes.GetNamedItem("Id").Value);
                  PlayerBonus playerBonus = new PlayerBonus(num);
                  ((ItemsModel) playerBonus).ObjectId = (long) ComDiv.ValidateStockId(num);
                  ((ItemsModel) playerBonus).Name = attributes.GetNamedItem("Name").Value;
                  ((ItemsModel) playerBonus).Count = 1U;
                  ((ItemsModel) playerBonus).Equip = ItemEquipType.Permanent;
                  ItemsModel itemsModel = (ItemsModel) playerBonus;
                  TemplatePackXML.Basics.Add(itemsModel);
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

  private static void smethod_4(string Port)
  {
    XmlDocument xmlDocument = new XmlDocument();
    using (FileStream inStream = new FileStream(Port, FileMode.Open))
    {
      if (inStream.Length == 0L)
      {
        // ISSUE: reference to a compiler-generated method
        CLogger.Class1.Print("File is empty: " + Port, LoggerType.Warning, (Exception) null);
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
                if ("Cafe".Equals(string_0.Name))
                {
                  XmlNamedNodeMap attributes = (XmlNamedNodeMap) string_0.Attributes;
                  PlayerBattlepass playerBattlepass = new PlayerBattlepass(ComDiv.ParseEnum<CafeEnum>(attributes.GetNamedItem("Type").Value));
                  playerBattlepass.set_ExpUp(int.Parse(attributes.GetNamedItem("ExpUp").Value));
                  ((PCCafeModel) playerBattlepass).PointUp = int.Parse(attributes.GetNamedItem("PointUp").Value);
                  playerBattlepass.set_Rewards(new SortedList<CafeEnum, List<ItemsModel>>());
                  PCCafeModel pcCafeModel = (PCCafeModel) playerBattlepass;
                  ClanRankXML.smethod_5(string_0, pcCafeModel);
                  TemplatePackXML.Cafes.Add(pcCafeModel);
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
}
