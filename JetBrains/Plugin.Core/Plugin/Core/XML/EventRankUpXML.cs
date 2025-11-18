// Decompiled with JetBrains decompiler
// Type: Plugin.Core.XML.EventRankUpXML
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

public class EventRankUpXML
{
  public static List<EventRankUpModel> Events;

  public static EventQuestModel GetRunningEvent()
  {
    uint num = uint.Parse(DBQuery.Now("yyMMddHHmm"));
    foreach (EventQuestModel runningEvent in EventQuestXML.list_0)
    {
      if (((EventRankUpModel) runningEvent).get_BeginDate() <= num && num < ((EventRankUpModel) runningEvent).get_EndedDate())
        return runningEvent;
    }
    return (EventQuestModel) null;
  }

  public static void ResetPlayerEvent([In] long obj0, PlayerEvent Event)
  {
    if (obj0 == 0L)
      return;
    ComDiv.UpdateDB("player_events", "owner_id", (object) obj0, new string[2]
    {
      "last_quest_date",
      "last_quest_finish"
    }, new object[2]
    {
      (object) (long) ((PlayerInfo) Event).get_LastQuestDate(),
      (object) Event.LastQuestFinish
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
                  EventRankUpModel eventRankUpModel = new EventRankUpModel();
                  eventRankUpModel.set_BeginDate(uint.Parse(attributes.GetNamedItem("Begin").Value));
                  eventRankUpModel.set_EndedDate(uint.Parse(attributes.GetNamedItem("Ended").Value));
                  EventQuestModel eventQuestModel = (EventQuestModel) eventRankUpModel;
                  EventQuestXML.list_0.Add(eventQuestModel);
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

  static EventRankUpXML() => EventQuestXML.list_0 = new List<EventQuestModel>();

  public static void Load()
  {
    string str = "Data/Events/Rank.xml";
    if (File.Exists(str))
    {
      EventVisitXML.smethod_0(str);
    }
    else
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("File not found: " + str, LoggerType.Warning, (Exception) null);
    }
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Plugin Loaded: {EventRankUpXML.Events.Count} Event Rank Up", LoggerType.Info, (Exception) null);
  }

  public static void Reload()
  {
    EventRankUpXML.Events.Clear();
    EventRankUpXML.Load();
  }

  public static EventRankUpModel GetRunningEvent()
  {
    lock (EventRankUpXML.Events)
    {
      uint num = uint.Parse(DBQuery.Now("yyMMddHHmm"));
      foreach (EventRankUpModel runningEvent in EventRankUpXML.Events)
      {
        if (runningEvent.BeginDate <= num && num < runningEvent.EndedDate)
          return runningEvent;
      }
      return (EventRankUpModel) null;
    }
  }
}
