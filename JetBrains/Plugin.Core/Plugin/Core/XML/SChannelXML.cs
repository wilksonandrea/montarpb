// Decompiled with JetBrains decompiler
// Type: Plugin.Core.XML.SChannelXML
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

public class SChannelXML
{
  public static List<SChannelModel> Servers;

  private static void smethod_0(string xmlNode_0)
  {
    XmlDocument xmlDocument = new XmlDocument();
    using (FileStream inStream = new FileStream(xmlNode_0, FileMode.Open))
    {
      if (inStream.Length == 0L)
      {
        // ISSUE: reference to a compiler-generated method
        CLogger.Class1.Print("File is empty: " + xmlNode_0, LoggerType.Warning, (Exception) null);
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
              for (XmlNode Id = xmlNode.FirstChild; Id != null; Id = Id.NextSibling)
              {
                if ("Rank".Equals(Id.Name))
                {
                  XmlNamedNodeMap attributes = (XmlNamedNodeMap) Id.Attributes;
                  VisitBoxModel visitBoxModel = new VisitBoxModel(int.Parse(attributes.GetNamedItem("Id").Value));
                  ((RankModel) visitBoxModel).Title = attributes.GetNamedItem("Title").Value;
                  ((RankModel) visitBoxModel).OnNextLevel = int.Parse(attributes.GetNamedItem("OnNextLevel").Value);
                  ((RankModel) visitBoxModel).OnGoldUp = int.Parse(attributes.GetNamedItem("OnGoldUp").Value);
                  ((RankModel) visitBoxModel).OnAllExp = int.Parse(attributes.GetNamedItem("OnAllExp").Value);
                  ((RankModel) visitBoxModel).Rewards = new SortedList<int, List<int>>();
                  RankModel rankModel = (RankModel) visitBoxModel;
                  SChannelXML.smethod_1(Id, rankModel);
                  PlayerRankXML.Ranks.Add(rankModel);
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

  public static List<int> GetRewards([In] int obj0)
  {
    RankModel rank = PlayerRankXML.GetRank(obj0);
    if (rank != null)
    {
      lock (rank.Rewards)
      {
        List<int> rewards;
        if (rank.Rewards.TryGetValue(obj0, out rewards))
          return rewards;
      }
    }
    return new List<int>();
  }

  private static void smethod_1(XmlNode Id, [In] RankModel obj1)
  {
    for (XmlNode xmlNode1 = Id.FirstChild; xmlNode1 != null; xmlNode1 = xmlNode1.NextSibling)
    {
      if ("Rewards".Equals(xmlNode1.Name))
      {
        for (XmlNode xmlNode2 = xmlNode1.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
        {
          if ("Good".Equals(xmlNode2.Name))
          {
            int num = int.Parse(xmlNode2.Attributes.GetNamedItem(nameof (Id)).Value);
            lock (obj1.Rewards)
            {
              if (obj1.Rewards.ContainsKey(obj1.Id))
                obj1.Rewards[obj1.Id].Add(num);
              else
                obj1.Rewards.Add(obj1.Id, new List<int>()
                {
                  num
                });
            }
          }
        }
      }
    }
  }

  static SChannelXML() => PlayerRankXML.Ranks = new List<RankModel>();

  public static void Load(bool Id)
  {
    string str = "Data/Server/SChannels.xml";
    if (File.Exists(str))
    {
      TitleAwardXML.smethod_0(str, Id);
    }
    else
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("File not found: " + str, LoggerType.Warning, (Exception) null);
    }
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Plugin Loaded: {SChannelXML.Servers.Count} Server Channel", LoggerType.Info, (Exception) null);
  }

  public static void UpdateServer(int string_0)
  {
    string str = "Data/Server/SChannels.xml";
    if (File.Exists(str))
    {
      TitleAwardXML.smethod_1(str, string_0);
    }
    else
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("File not found: " + str, LoggerType.Warning, (Exception) null);
    }
  }

  public static void Reload()
  {
    SChannelXML.Servers.Clear();
    SChannelXML.Load(true);
  }
}
