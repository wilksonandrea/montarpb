// Decompiled with JetBrains decompiler
// Type: Plugin.Core.XML.SeasonChallengeXML
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

public class SeasonChallengeXML
{
  public static List<BattlePassModel> Seasons;

  public static TicketModel GetTicket(string string_0, [In] TicketType obj1)
  {
    lock (RedeemCodeXML.Tickets)
    {
      foreach (TicketModel ticket in RedeemCodeXML.Tickets)
      {
        if (ticket.Token == string_0 && ticket.Type == obj1)
          return ticket;
      }
      return (TicketModel) null;
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
          for (XmlNode xmlNode = xmlDocument.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
          {
            if ("List".Equals(xmlNode.Name))
            {
              for (XmlNode xmlNode_0 = xmlNode.FirstChild; xmlNode_0 != null; xmlNode_0 = xmlNode_0.NextSibling)
              {
                if ("Ticket".Equals(xmlNode_0.Name))
                {
                  XmlNamedNodeMap attributes = (XmlNamedNodeMap) xmlNode_0.Attributes;
                  InternetCafe internetCafe = new InternetCafe();
                  ((TicketModel) internetCafe).Token = attributes.GetNamedItem("Token").Value;
                  ((TicketModel) internetCafe).Type = ComDiv.ParseEnum<TicketType>(attributes.GetNamedItem("Type").Value);
                  ((TicketModel) internetCafe).TicketCount = uint.Parse(attributes.GetNamedItem("Count").Value);
                  internetCafe.set_PlayerRation(uint.Parse(attributes.GetNamedItem("PlayerRation").Value));
                  internetCafe.set_Rewards(new List<int>());
                  TicketModel accessLevel_0 = (TicketModel) internetCafe;
                  if (accessLevel_0.Type == TicketType.VOUCHER)
                  {
                    accessLevel_0.GoldReward = int.Parse(attributes.GetNamedItem("GoldReward").Value);
                    accessLevel_0.CashReward = int.Parse(attributes.GetNamedItem("CashReward").Value);
                    accessLevel_0.TagsReward = int.Parse(attributes.GetNamedItem("TagsReward").Value);
                  }
                  if (accessLevel_0.Type == TicketType.COUPON)
                    SeasonChallengeXML.smethod_1(xmlNode_0, accessLevel_0);
                  RedeemCodeXML.Tickets.Add(accessLevel_0);
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

  private static void smethod_1(XmlNode xmlNode_0, TicketModel accessLevel_0)
  {
    for (XmlNode xmlNode1 = xmlNode_0.FirstChild; xmlNode1 != null; xmlNode1 = xmlNode1.NextSibling)
    {
      if ("Rewards".Equals(xmlNode1.Name))
      {
        for (XmlNode xmlNode2 = xmlNode1.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
        {
          if ("Goods".Equals(xmlNode2.Name))
          {
            XmlNamedNodeMap attributes = (XmlNamedNodeMap) xmlNode2.Attributes;
            ((InternetCafe) accessLevel_0).get_Rewards().Add(int.Parse(attributes.GetNamedItem("Id").Value));
          }
        }
      }
    }
  }

  static SeasonChallengeXML() => RedeemCodeXML.Tickets = new List<TicketModel>();

  public static void Load()
  {
    string path = "Data/SeasonChallenges.xml";
    if (File.Exists(path))
    {
      SeasonPassXML.smethod_0(path);
    }
    else
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("File not found: " + path, LoggerType.Warning, (Exception) null);
    }
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Plugin Loaded: {SeasonChallengeXML.Seasons.Count} Season Challenge", LoggerType.Info, (Exception) null);
  }

  public static void Reload()
  {
    SeasonChallengeXML.Seasons.Clear();
    SeasonChallengeXML.Load();
  }

  public static BattlePassModel GetSeasonPass(int Token)
  {
    lock (SeasonChallengeXML.Seasons)
    {
      foreach (BattlePassModel season in SeasonChallengeXML.Seasons)
      {
        if (season.Id == Token)
          return season;
      }
      return (BattlePassModel) null;
    }
  }
}
