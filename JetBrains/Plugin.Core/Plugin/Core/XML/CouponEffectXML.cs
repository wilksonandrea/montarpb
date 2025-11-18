// Decompiled with JetBrains decompiler
// Type: Plugin.Core.XML.CouponEffectXML
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

#nullable disable
namespace Plugin.Core.XML;

public static class CouponEffectXML
{
  private static List<CouponFlag> list_0;

  public static void Reload()
  {
    CompetitiveXML.Ranks.Clear();
    CompetitiveXML.Load();
  }

  public static CompetitiveRank GetRank(int BattleBoxId)
  {
    lock (CompetitiveXML.Ranks)
    {
      foreach (CompetitiveRank rank in CompetitiveXML.Ranks)
      {
        if (rank.Id == BattleBoxId)
          return rank;
      }
      return (CompetitiveRank) null;
    }
  }

  private static void smethod_0(string RewardType)
  {
    XmlDocument xmlDocument = new XmlDocument();
    using (FileStream inStream = new FileStream(RewardType, FileMode.Open))
    {
      if (inStream.Length == 0L)
      {
        // ISSUE: reference to a compiler-generated method
        CLogger.Class1.Print("File is empty: " + RewardType, LoggerType.Warning, (Exception) null);
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
                if ("Competitive".Equals(xmlNode2.Name))
                {
                  XmlNamedNodeMap attributes = (XmlNamedNodeMap) xmlNode2.Attributes;
                  EventLoginModel eventLoginModel = new EventLoginModel();
                  ((CompetitiveRank) eventLoginModel).Id = int.Parse(attributes.GetNamedItem("Id").Value);
                  ((CompetitiveRank) eventLoginModel).TourneyLevel = int.Parse(attributes.GetNamedItem("TourneyLevel").Value);
                  eventLoginModel.set_Points(int.Parse(attributes.GetNamedItem("Points").Value));
                  eventLoginModel.set_Name(attributes.GetNamedItem("Name").Value);
                  CompetitiveRank competitiveRank = (CompetitiveRank) eventLoginModel;
                  CompetitiveXML.Ranks.Add(competitiveRank);
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

  static CouponEffectXML() => CompetitiveXML.Ranks = new List<CompetitiveRank>();
}
