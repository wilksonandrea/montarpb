// Decompiled with JetBrains decompiler
// Type: Plugin.Core.XML.EventVisitXML
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

public class EventVisitXML
{
  public static readonly List<EventVisitModel> Events;

  public static EventRankUpModel GetEvent([In] int obj0)
  {
    lock (EventRankUpXML.Events)
    {
      foreach (EventRankUpModel eventRankUpModel in EventRankUpXML.Events)
      {
        if (eventRankUpModel.Id == obj0)
          return eventRankUpModel;
      }
      return (EventRankUpModel) null;
    }
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
          for (XmlNode xmlNode = xmlDocument.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
          {
            if ("List".Equals(xmlNode.Name))
            {
              for (XmlNode pId = xmlNode.FirstChild; pId != null; pId = pId.NextSibling)
              {
                if ("Event".Equals(pId.Name))
                {
                  XmlNamedNodeMap attributes = (XmlNamedNodeMap) pId.Attributes;
                  EventVisitModel eventVisitModel = new EventVisitModel();
                  ((EventRankUpModel) eventVisitModel).Id = int.Parse(attributes.GetNamedItem("Id").Value);
                  ((EventRankUpModel) eventVisitModel).BeginDate = uint.Parse(attributes.GetNamedItem("Begin").Value);
                  ((EventRankUpModel) eventVisitModel).EndedDate = uint.Parse(attributes.GetNamedItem("Ended").Value);
                  ((EventRankUpModel) eventVisitModel).Name = attributes.GetNamedItem("Name").Value;
                  ((EventRankUpModel) eventVisitModel).Description = attributes.GetNamedItem("Description").Value;
                  ((EventRankUpModel) eventVisitModel).Period = bool.Parse(attributes.GetNamedItem("Period").Value);
                  ((EventRankUpModel) eventVisitModel).Priority = bool.Parse(attributes.GetNamedItem("Priority").Value);
                  eventVisitModel.set_Ranks(new List<int[]>());
                  EventRankUpModel pE = (EventRankUpModel) eventVisitModel;
                  EventVisitXML.smethod_1(pId, pE);
                  EventRankUpXML.Events.Add(pE);
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

  private static void smethod_1(XmlNode pId, EventRankUpModel pE)
  {
    for (XmlNode xmlNode1 = pId.FirstChild; xmlNode1 != null; xmlNode1 = xmlNode1.NextSibling)
    {
      if ("Rewards".Equals(xmlNode1.Name))
      {
        for (XmlNode xmlNode2 = xmlNode1.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
        {
          if ("Rank".Equals(xmlNode2.Name))
          {
            XmlNamedNodeMap attributes = (XmlNamedNodeMap) xmlNode2.Attributes;
            int[] numArray = new int[4]
            {
              int.Parse(attributes.GetNamedItem("UpId").Value),
              int.Parse(attributes.GetNamedItem("BonusExp").Value),
              int.Parse(attributes.GetNamedItem("BonusPoint").Value),
              int.Parse(attributes.GetNamedItem("Percent").Value)
            };
            ((EventVisitModel) pE).get_Ranks().Add(numArray);
          }
        }
      }
    }
  }

  static EventVisitXML() => EventRankUpXML.Events = new List<EventRankUpModel>();

  public static void Load()
  {
    string str = "Data/Events/Visit.xml";
    if (File.Exists(str))
    {
      EventXmasXML.smethod_0(str);
    }
    else
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("File not found: " + str, LoggerType.Warning, (Exception) null);
    }
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Plugin Loaded: {EventVisitXML.Events.Count} Event Visit", LoggerType.Info, (Exception) null);
  }

  public static void Reload()
  {
    EventVisitXML.Events.Clear();
    EventVisitXML.Load();
  }

  public static EventVisitModel GetEvent(int string_0)
  {
    lock (EventVisitXML.Events)
    {
      foreach (EventVisitModel eventVisitModel in EventVisitXML.Events)
      {
        if (eventVisitModel.Id == string_0)
          return eventVisitModel;
      }
      return (EventVisitModel) null;
    }
  }

  public static EventVisitModel GetRunningEvent()
  {
    lock (EventVisitXML.Events)
    {
      uint num = uint.Parse(DBQuery.Now("yyMMddHHmm"));
      foreach (EventVisitModel runningEvent in EventVisitXML.Events)
      {
        if (runningEvent.BeginDate <= num && num < runningEvent.EndedDate)
          return runningEvent;
      }
      return (EventVisitModel) null;
    }
  }
}
