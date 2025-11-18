// Decompiled with JetBrains decompiler
// Type: Plugin.Core.XML.EventXmasXML
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

public class EventXmasXML
{
  private static List<EventXmasModel> list_0;

  public static void ResetPlayerEvent(long EventId, [In] PlayerEvent obj1)
  {
    if (EventId == 0L)
      return;
    ComDiv.UpdateDB("player_events", "owner_id", (object) EventId, new string[3]
    {
      "last_visit_check_day",
      "last_visit_seq_type",
      "last_visit_date"
    }, new object[3]
    {
      (object) obj1.LastVisitCheckDay,
      (object) obj1.LastVisitSeqType,
      (object) (long) obj1.LastVisitDate
    });
  }

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
          for (XmlNode xmlNode1 = xmlDocument.FirstChild; xmlNode1 != null; xmlNode1 = xmlNode1.NextSibling)
          {
            if ("List".Equals(xmlNode1.Name))
            {
              for (XmlNode xmlNode2 = xmlNode1.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
              {
                if ("Event".Equals(xmlNode2.Name))
                {
                  XmlNamedNodeMap attributes = (XmlNamedNodeMap) xmlNode2.Attributes;
                  EventXmasModel eventXmasModel = new EventXmasModel();
                  ((EventVisitModel) eventXmasModel).Id = int.Parse(attributes.GetNamedItem("Id").Value);
                  ((EventVisitModel) eventXmasModel).BeginDate = uint.Parse(attributes.GetNamedItem("Begin").Value);
                  ((EventVisitModel) eventXmasModel).EndedDate = uint.Parse(attributes.GetNamedItem("Ended").Value);
                  ((EventVisitModel) eventXmasModel).Title = attributes.GetNamedItem("Title").Value;
                  ((EventVisitModel) eventXmasModel).Checks = int.Parse(attributes.GetNamedItem("Days").Value);
                  eventXmasModel.set_Boxes(new List<VisitBoxModel>());
                  EventVisitModel eventRankUpModel_0 = (EventVisitModel) eventXmasModel;
                  for (int index = 0; index < 31 /*0x1F*/; ++index)
                    eventRankUpModel_0.Boxes.Add((VisitBoxModel) new TicketModel());
                  EventXmasXML.smethod_1(xmlNode2, eventRankUpModel_0);
                  ((EventXmasModel) eventRankUpModel_0).SetBoxCounts();
                  EventVisitXML.Events.Add(eventRankUpModel_0);
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

  private static void smethod_1([In] XmlNode obj0, EventVisitModel eventRankUpModel_0)
  {
    for (XmlNode xmlNode1 = obj0.FirstChild; xmlNode1 != null; xmlNode1 = xmlNode1.NextSibling)
    {
      if ("Rewards".Equals(xmlNode1.Name))
      {
        for (XmlNode xmlNode2 = xmlNode1.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
        {
          if ("Box".Equals(xmlNode2.Name))
          {
            XmlNamedNodeMap attributes = (XmlNamedNodeMap) xmlNode2.Attributes;
            int index = int.Parse(attributes.GetNamedItem("Day").Value) - 1;
            ((BanHistory) eventRankUpModel_0.Boxes[index].Reward1).SetGoodId(int.Parse(attributes.GetNamedItem("GoodId1").Value));
            ((BanHistory) eventRankUpModel_0.Boxes[index].Reward2).SetGoodId(int.Parse(attributes.GetNamedItem("GoodId2").Value));
            ((TicketModel) eventRankUpModel_0.Boxes[index]).set_IsBothReward(bool.Parse(attributes.GetNamedItem("Both").Value));
          }
        }
      }
    }
  }

  static EventXmasXML() => EventVisitXML.Events = new List<EventVisitModel>();

  public static void Load()
  {
    string str = "Data/Events/Xmas.xml";
    if (File.Exists(str))
    {
      GameRuleXML.smethod_0(str);
    }
    else
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("File not found: " + str, LoggerType.Warning, (Exception) null);
    }
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Plugin Loaded: {EventXmasXML.list_0.Count} Event X-Mas", LoggerType.Info, (Exception) null);
  }
}
