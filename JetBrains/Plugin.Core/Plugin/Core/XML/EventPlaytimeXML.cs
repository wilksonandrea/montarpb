// Decompiled with JetBrains decompiler
// Type: Plugin.Core.XML.EventPlaytimeXML
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

public class EventPlaytimeXML
{
  public static readonly List<EventPlaytimeModel> Events;

  public static EventBoostModel GetEvent(int string_0)
  {
    lock (EventBoostXML.Events)
    {
      foreach (EventBoostModel eventBoostModel in EventBoostXML.Events)
      {
        if (eventBoostModel.Id == string_0)
          return eventBoostModel;
      }
      return (EventBoostModel) null;
    }
  }

  public static bool EventIsValid(EventBoostModel EventId, [In] PortalBoostEvent obj1, [In] int obj2)
  {
    if (EventId == null)
      return false;
    return EventId.BoostType == obj1 || ((EventPlaytimeModel) EventId).get_BoostValue() == obj2;
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
          for (XmlNode xmlNode1 = xmlDocument.FirstChild; xmlNode1 != null; xmlNode1 = xmlNode1.NextSibling)
          {
            if ("List".Equals(xmlNode1.Name))
            {
              for (XmlNode xmlNode2 = xmlNode1.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
              {
                if ("Event".Equals(xmlNode2.Name))
                {
                  XmlNamedNodeMap attributes = (XmlNamedNodeMap) xmlNode2.Attributes;
                  EventPlaytimeModel eventPlaytimeModel = new EventPlaytimeModel();
                  ((EventBoostModel) eventPlaytimeModel).Id = int.Parse(attributes.GetNamedItem("Id").Value);
                  ((EventBoostModel) eventPlaytimeModel).BeginDate = uint.Parse(attributes.GetNamedItem("Begin").Value);
                  ((EventBoostModel) eventPlaytimeModel).EndedDate = uint.Parse(attributes.GetNamedItem("Ended").Value);
                  eventPlaytimeModel.set_BoostType(ComDiv.ParseEnum<PortalBoostEvent>(attributes.GetNamedItem("BoostType").Value));
                  eventPlaytimeModel.set_BoostValue(int.Parse(attributes.GetNamedItem("BoostValue").Value));
                  ((EventBoostModel) eventPlaytimeModel).BonusExp = int.Parse(attributes.GetNamedItem("BonusExp").Value);
                  ((EventBoostModel) eventPlaytimeModel).BonusGold = int.Parse(attributes.GetNamedItem("BonusGold").Value);
                  ((EventBoostModel) eventPlaytimeModel).Percent = int.Parse(attributes.GetNamedItem("Percent").Value);
                  ((EventBoostModel) eventPlaytimeModel).Name = attributes.GetNamedItem("Name").Value;
                  ((EventBoostModel) eventPlaytimeModel).Description = attributes.GetNamedItem("Description").Value;
                  ((EventBoostModel) eventPlaytimeModel).Period = bool.Parse(attributes.GetNamedItem("Period").Value);
                  ((EventBoostModel) eventPlaytimeModel).Priority = bool.Parse(attributes.GetNamedItem("Priority").Value);
                  EventBoostModel eventBoostModel = (EventBoostModel) eventPlaytimeModel;
                  EventBoostXML.Events.Add(eventBoostModel);
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

  static EventPlaytimeXML() => EventBoostXML.Events = new List<EventBoostModel>();

  public static void Load()
  {
    string path = "Data/Events/Play.xml";
    if (File.Exists(path))
    {
      EventQuestXML.smethod_0(path);
    }
    else
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("File not found: " + path, LoggerType.Warning, (Exception) null);
    }
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Plugin Loaded: {EventPlaytimeXML.Events.Count} Event Playtime", LoggerType.Info, (Exception) null);
  }

  public static void Reload()
  {
    EventPlaytimeXML.Events.Clear();
    EventPlaytimeXML.Load();
  }

  public static EventPlaytimeModel GetRunningEvent()
  {
    lock (EventPlaytimeXML.Events)
    {
      uint num = uint.Parse(DBQuery.Now("yyMMddHHmm"));
      foreach (EventPlaytimeModel runningEvent in EventPlaytimeXML.Events)
      {
        if (runningEvent.BeginDate <= num && num < runningEvent.EndedDate)
          return runningEvent;
      }
      return (EventPlaytimeModel) null;
    }
  }

  public static EventPlaytimeModel GetEvent(int EventId)
  {
    lock (EventPlaytimeXML.Events)
    {
      foreach (EventPlaytimeModel eventPlaytimeModel in EventPlaytimeXML.Events)
      {
        if (eventPlaytimeModel.Id == EventId)
          return eventPlaytimeModel;
      }
      return (EventPlaytimeModel) null;
    }
  }

  public static void ResetPlayerEvent(long Event, PlayerEvent BoostType)
  {
    if (Event == 0L)
      return;
    ComDiv.UpdateDB("player_events", "owner_id", (object) Event, new string[3]
    {
      "last_playtime_value",
      "last_playtime_finish",
      "last_playtime_date"
    }, new object[3]
    {
      (object) BoostType.LastPlaytimeValue,
      (object) BoostType.LastPlaytimeFinish,
      (object) (long) BoostType.LastPlaytimeDate
    });
  }
}
