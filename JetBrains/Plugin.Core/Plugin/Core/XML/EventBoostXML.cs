// Decompiled with JetBrains decompiler
// Type: Plugin.Core.XML.EventBoostXML
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

public class EventBoostXML
{
  public static List<EventBoostModel> Events;

  public static EventLoginModel GetEvent([In] int obj0)
  {
    lock (EventLoginXML.Events)
    {
      foreach (EventLoginModel eventLoginModel in EventLoginXML.Events)
      {
        if (eventLoginModel.Id == obj0)
          return eventLoginModel;
      }
      return (EventLoginModel) null;
    }
  }

  private static void smethod_0(string Level)
  {
    XmlDocument xmlDocument = new XmlDocument();
    using (FileStream inStream = new FileStream(Level, FileMode.Open))
    {
      if (inStream.Length == 0L)
      {
        // ISSUE: reference to a compiler-generated method
        CLogger.Class1.Print("File is empty: " + Level, LoggerType.Warning, (Exception) null);
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
                if ("Event".Equals(string_0.Name))
                {
                  XmlNamedNodeMap attributes = (XmlNamedNodeMap) string_0.Attributes;
                  EventBoostModel eventBoostModel = new EventBoostModel();
                  ((EventLoginModel) eventBoostModel).Id = int.Parse(attributes.GetNamedItem("Id").Value);
                  ((EventLoginModel) eventBoostModel).BeginDate = uint.Parse(attributes.GetNamedItem("Begin").Value);
                  ((EventLoginModel) eventBoostModel).EndedDate = uint.Parse(attributes.GetNamedItem("Ended").Value);
                  ((EventLoginModel) eventBoostModel).Name = attributes.GetNamedItem("Name").Value;
                  ((EventLoginModel) eventBoostModel).Description = attributes.GetNamedItem("Description").Value;
                  ((EventLoginModel) eventBoostModel).Period = bool.Parse(attributes.GetNamedItem("Period").Value);
                  eventBoostModel.set_Priority(bool.Parse(attributes.GetNamedItem("Priority").Value));
                  eventBoostModel.set_Goods(new List<int>());
                  EventLoginModel eventLoginModel = (EventLoginModel) eventBoostModel;
                  EventBoostXML.smethod_1(string_0, eventLoginModel);
                  EventLoginXML.Events.Add(eventLoginModel);
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

  private static void smethod_1(XmlNode string_0, [In] EventLoginModel obj1)
  {
    for (XmlNode xmlNode1 = string_0.FirstChild; xmlNode1 != null; xmlNode1 = xmlNode1.NextSibling)
    {
      if ("Rewards".Equals(xmlNode1.Name))
      {
        for (XmlNode xmlNode2 = xmlNode1.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
        {
          if ("Item".Equals(xmlNode2.Name))
          {
            int num = int.Parse(xmlNode2.Attributes.GetNamedItem("GoodId").Value);
            if (((EventBoostModel) obj1).get_Goods().Count <= 4)
            {
              ((EventBoostModel) obj1).get_Goods().Add(num);
            }
            else
            {
              // ISSUE: reference to a compiler-generated method
              CLogger.Class1.Print("Max that can be listed on Login Event was 4!", LoggerType.Warning, (Exception) null);
              return;
            }
          }
        }
      }
    }
  }

  static EventBoostXML() => EventLoginXML.Events = new List<EventLoginModel>();

  public static void Load()
  {
    string path = "Data/Events/Boost.xml";
    if (File.Exists(path))
    {
      EventPlaytimeXML.smethod_0(path);
    }
    else
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("File not found: " + path, LoggerType.Warning, (Exception) null);
    }
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Plugin Loaded: {EventBoostXML.Events.Count} Event Boost Bonus", LoggerType.Info, (Exception) null);
  }

  public static void Reload()
  {
    EventBoostXML.Events.Clear();
    EventBoostXML.Load();
  }

  public static EventBoostModel GetRunningEvent()
  {
    lock (EventBoostXML.Events)
    {
      uint num = uint.Parse(DBQuery.Now("yyMMddHHmm"));
      foreach (EventBoostModel runningEvent in EventBoostXML.Events)
      {
        if (runningEvent.BeginDate <= num && num < runningEvent.EndedDate)
          return runningEvent;
      }
      return (EventBoostModel) null;
    }
  }
}
