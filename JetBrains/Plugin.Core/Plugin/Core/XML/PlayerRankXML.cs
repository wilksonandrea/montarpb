// Decompiled with JetBrains decompiler
// Type: Plugin.Core.XML.PlayerRankXML
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;

#nullable disable
namespace Plugin.Core.XML;

public class PlayerRankXML
{
  public static readonly List<RankModel> Ranks;

  private static void smethod_2(XmlNode class0_0, [In] RandomBoxModel obj1)
  {
    for (XmlNode xmlNode1 = class0_0.FirstChild; xmlNode1 != null; xmlNode1 = xmlNode1.NextSibling)
    {
      if ("Rewards".Equals(xmlNode1.Name))
      {
        for (XmlNode xmlNode2 = xmlNode1.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
        {
          if ("Good".Equals(xmlNode2.Name))
          {
            XmlNamedNodeMap attributes = (XmlNamedNodeMap) xmlNode2.Attributes;
            RandomBoxModel randomBoxModel = new RandomBoxModel();
            ((RandomBoxItem) randomBoxModel).Index = int.Parse(attributes.GetNamedItem("Index").Value);
            ((RandomBoxItem) randomBoxModel).GoodsId = int.Parse(attributes.GetNamedItem("Id").Value);
            randomBoxModel.set_Percent(int.Parse(attributes.GetNamedItem("Percent").Value));
            randomBoxModel.set_Special(bool.Parse(attributes.GetNamedItem("Special").Value));
            RandomBoxItem randomBoxItem = (RandomBoxItem) randomBoxModel;
            obj1.Items.Add(randomBoxItem);
          }
        }
      }
    }
  }

  public static bool ContainsBox(int int_0) => RandomBoxXML.RBoxes.ContainsKey(int_0);

  public static RandomBoxModel GetBox(int string_0)
  {
    try
    {
      return RandomBoxXML.RBoxes[string_0];
    }
    catch
    {
      return (RandomBoxModel) null;
    }
  }

  static PlayerRankXML() => RandomBoxXML.RBoxes = new SortedList<int, RandomBoxModel>();

  public static void Load()
  {
    string str = "Data/Ranks/Player.xml";
    if (File.Exists(str))
    {
      SChannelXML.smethod_0(str);
    }
    else
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("File not found: " + str, LoggerType.Warning, (Exception) null);
    }
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Plugin Loaded: {PlayerRankXML.Ranks.Count} Player Ranks", LoggerType.Info, (Exception) null);
  }

  public static void Reload()
  {
    PlayerRankXML.Ranks.Clear();
    PlayerRankXML.Load();
  }

  public static RankModel GetRank([In] int obj0)
  {
    lock (PlayerRankXML.Ranks)
    {
      foreach (RankModel rank in PlayerRankXML.Ranks)
      {
        if (rank.Id == obj0)
          return rank;
      }
      return (RankModel) null;
    }
  }
}
