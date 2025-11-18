// Decompiled with JetBrains decompiler
// Type: Plugin.Core.XML.CompetitiveXML
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

public class CompetitiveXML
{
  public static List<CompetitiveRank> Ranks;

  public static BattleRewardModel GetRewardType([In] BattleRewardType obj0)
  {
    lock (BattleRewardXML.Rewards)
    {
      foreach (BattleRewardModel reward in BattleRewardXML.Rewards)
      {
        if (reward.Type == obj0)
          return reward;
      }
      return (BattleRewardModel) null;
    }
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
              for (XmlNode battleBoxModel_0 = xmlNode.FirstChild; battleBoxModel_0 != null; battleBoxModel_0 = battleBoxModel_0.NextSibling)
              {
                if ("Item".Equals(battleBoxModel_0.Name))
                {
                  XmlNamedNodeMap attributes = (XmlNamedNodeMap) battleBoxModel_0.Attributes;
                  int length = int.Parse(attributes.GetNamedItem("Count").Value);
                  CartGoods cartGoods = new CartGoods();
                  ((BattleRewardModel) cartGoods).Type = ComDiv.ParseEnum<BattleRewardType>(attributes.GetNamedItem("Type").Value);
                  ((BattleRewardModel) cartGoods).Percentage = int.Parse(attributes.GetNamedItem("Percentage").Value);
                  cartGoods.set_Enable(bool.Parse(attributes.GetNamedItem("Enable").Value));
                  cartGoods.set_Rewards(new int[length]);
                  BattleRewardModel syncServerPacket_0 = (BattleRewardModel) cartGoods;
                  CompetitiveXML.smethod_1(battleBoxModel_0, syncServerPacket_0);
                  BattleRewardXML.Rewards.Add(syncServerPacket_0);
                }
              }
            }
          }
        }
        catch (Exception ex)
        {
          // ISSUE: reference to a compiler-generated method
          CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
        }
      }
      inStream.Dispose();
      inStream.Close();
    }
  }

  private static void smethod_1(XmlNode battleBoxModel_0, BattleRewardModel syncServerPacket_0)
  {
    for (XmlNode xmlNode1 = battleBoxModel_0.FirstChild; xmlNode1 != null; xmlNode1 = xmlNode1.NextSibling)
    {
      if ("Rewards".Equals(xmlNode1.Name))
      {
        for (XmlNode xmlNode2 = xmlNode1.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
        {
          if ("Good".Equals(xmlNode2.Name))
          {
            XmlNamedNodeMap attributes = (XmlNamedNodeMap) xmlNode2.Attributes;
            BattleRewardModel battleRewardModel = new BattleRewardModel();
            battleRewardModel.set_Index(int.Parse(attributes.GetNamedItem("Index").Value));
            battleRewardModel.set_GoodId(int.Parse(attributes.GetNamedItem("Id").Value));
            BattleRewardItem battleRewardItem = (BattleRewardItem) battleRewardModel;
            ((CartGoods) syncServerPacket_0).get_Rewards()[((BattleRewardModel) battleRewardItem).get_Index()] = ((BattleRewardModel) battleRewardItem).get_GoodId();
          }
        }
      }
    }
  }

  static CompetitiveXML() => BattleRewardXML.Rewards = new List<BattleRewardModel>();

  public static void Load()
  {
    string str = "Data/Competitions.xml";
    if (File.Exists(str))
    {
      CouponEffectXML.smethod_0(str);
    }
    else
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("File not found: " + str, LoggerType.Warning, (Exception) null);
    }
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Plugin Loaded: {CompetitiveXML.Ranks.Count} Competitive Ranks", LoggerType.Info, (Exception) null);
  }
}
