// Decompiled with JetBrains decompiler
// Type: Plugin.Core.XML.SeasonPassXML
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

public class SeasonPassXML
{
  public static readonly List<BattlePassModel> Seasons;

  public static BattlePassModel GetActiveSeasonPass()
  {
    lock (SeasonChallengeXML.Seasons)
    {
      uint num = uint.Parse(DateTime.Now.ToString("yyMMddHHmm"));
      foreach (BattlePassModel season in SeasonChallengeXML.Seasons)
      {
        if (season.BeginDate <= num && num < season.EndedDate)
          return season;
      }
      return (BattlePassModel) null;
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
              for (XmlNode string_0 = xmlNode.FirstChild; string_0 != null; string_0 = string_0.NextSibling)
              {
                if ("Season".Equals(string_0.Name))
                {
                  XmlNamedNodeMap attributes = (XmlNamedNodeMap) string_0.Attributes;
                  BattlePassModel battlePassModel = new BattlePassModel()
                  {
                    Id = int.Parse(attributes.GetNamedItem("Id").Value),
                    MaxDailyPoints = int.Parse(attributes.GetNamedItem("MaxDailyPoints").Value),
                    Name = attributes.GetNamedItem("Name").Value,
                    BeginDate = uint.Parse(attributes.GetNamedItem("Begin").Value),
                    EndedDate = uint.Parse(attributes.GetNamedItem("Ended").Value),
                    Enable = bool.Parse(attributes.GetNamedItem("Enable").Value),
                    Cards = new List<PassBoxModel>()
                  };
                  for (int index = 0; index < 99; ++index)
                    battlePassModel.Cards.Add((PassBoxModel) new PassItemModel());
                  SeasonPassXML.smethod_1(string_0, battlePassModel);
                  // ISSUE: reference to a compiler-generated method
                  ((BattlePassModel.Class11) battlePassModel).SetBoxCounts();
                  SeasonChallengeXML.Seasons.Add(battlePassModel);
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

  private static void smethod_1(XmlNode string_0, [In] BattlePassModel obj1)
  {
    for (XmlNode xmlNode1 = string_0.FirstChild; xmlNode1 != null; xmlNode1 = xmlNode1.NextSibling)
    {
      if ("Rewards".Equals(xmlNode1.Name))
      {
        for (XmlNode xmlNode2 = xmlNode1.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
        {
          if ("Card".Equals(xmlNode2.Name))
          {
            XmlNamedNodeMap attributes = (XmlNamedNodeMap) xmlNode2.Attributes;
            int index = int.Parse(attributes.GetNamedItem("Level").Value) - 1;
            ((PassItemModel) obj1.Cards[index]).set_Card(index + 1);
            ((PCCafeModel) obj1.Cards[index].Normal).SetGoodId(int.Parse(attributes.GetNamedItem("Normal").Value));
            ((PCCafeModel) obj1.Cards[index].PremiumA).SetGoodId(int.Parse(attributes.GetNamedItem("PremiumA").Value));
            ((PCCafeModel) obj1.Cards[index].PremiumB).SetGoodId(int.Parse(attributes.GetNamedItem("PremiumB").Value));
            obj1.Cards[index].RequiredPoints = int.Parse(attributes.GetNamedItem("ReqPoints").Value);
          }
        }
      }
    }
  }

  static SeasonPassXML() => SeasonChallengeXML.Seasons = new List<BattlePassModel>();
}
