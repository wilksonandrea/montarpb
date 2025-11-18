// Decompiled with JetBrains decompiler
// Type: Plugin.Core.XML.EventQuestXML
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

public class EventQuestXML
{
  private static List<EventQuestModel> list_0;

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
                if ("Event".Equals(string_0.Name))
                {
                  XmlNamedNodeMap attributes = (XmlNamedNodeMap) string_0.Attributes;
                  EventQuestModel eventQuestModel = new EventQuestModel();
                  ((EventPlaytimeModel) eventQuestModel).Id = int.Parse(attributes.GetNamedItem("Id").Value);
                  ((EventPlaytimeModel) eventQuestModel).BeginDate = uint.Parse(attributes.GetNamedItem("Begin").Value);
                  ((EventPlaytimeModel) eventQuestModel).EndedDate = uint.Parse(attributes.GetNamedItem("Ended").Value);
                  ((EventPlaytimeModel) eventQuestModel).Name = attributes.GetNamedItem("Name").Value;
                  ((EventPlaytimeModel) eventQuestModel).Description = attributes.GetNamedItem("Description").Value;
                  ((EventPlaytimeModel) eventQuestModel).Period = bool.Parse(attributes.GetNamedItem("Period").Value);
                  ((EventPlaytimeModel) eventQuestModel).Priority = bool.Parse(attributes.GetNamedItem("Priority").Value);
                  EventPlaytimeModel eventPlaytimeModel = (EventPlaytimeModel) eventQuestModel;
                  EventQuestXML.smethod_1(string_0, eventPlaytimeModel);
                  EventPlaytimeXML.Events.Add(eventPlaytimeModel);
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

  private static void smethod_1(XmlNode string_0, [In] EventPlaytimeModel obj1)
  {
    for (XmlNode xmlNode = string_0.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
    {
      if ("Minutes".Equals(xmlNode.Name))
      {
        for (XmlNode PlayerId = xmlNode.FirstChild; PlayerId != null; PlayerId = PlayerId.NextSibling)
        {
          if ("Time".Equals(PlayerId.Name))
          {
            XmlNamedNodeMap attributes = (XmlNamedNodeMap) PlayerId.Attributes;
            switch (int.Parse(attributes.GetNamedItem("Index").Value))
            {
              case 1:
                obj1.Minutes1 = int.Parse(attributes.GetNamedItem("Play").Value);
                obj1.Goods1 = EventQuestXML.smethod_2(PlayerId);
                continue;
              case 2:
                obj1.Minutes2 = int.Parse(attributes.GetNamedItem("Play").Value);
                ((EventQuestModel) obj1).set_Goods2(EventQuestXML.smethod_2(PlayerId));
                continue;
              case 3:
                obj1.Minutes3 = int.Parse(attributes.GetNamedItem("Play").Value);
                ((EventQuestModel) obj1).set_Goods3(EventQuestXML.smethod_2(PlayerId));
                continue;
              default:
                continue;
            }
          }
        }
      }
    }
  }

  private static List<int> smethod_2(XmlNode PlayerId)
  {
    List<int> intList = new List<int>();
    for (XmlNode xmlNode1 = PlayerId.FirstChild; xmlNode1 != null; xmlNode1 = xmlNode1.NextSibling)
    {
      if ("Reward".Equals(xmlNode1.Name))
      {
        for (XmlNode xmlNode2 = xmlNode1.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
        {
          if ("Goods".Equals(xmlNode2.Name))
          {
            XmlNamedNodeMap attributes = (XmlNamedNodeMap) xmlNode2.Attributes;
            intList.Add(int.Parse(attributes.GetNamedItem("Id").Value));
          }
        }
      }
    }
    return intList;
  }

  static EventQuestXML() => EventPlaytimeXML.Events = new List<EventPlaytimeModel>();

  public static void Load()
  {
    string str = "Data/Events/Quest.xml";
    if (File.Exists(str))
    {
      EventRankUpXML.smethod_0(str);
    }
    else
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("File not found: " + str, LoggerType.Warning, (Exception) null);
    }
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Plugin Loaded: {EventQuestXML.list_0.Count} Event Quest", LoggerType.Info, (Exception) null);
  }

  public static void Reload()
  {
    EventQuestXML.list_0.Clear();
    EventQuestXML.Load();
  }
}
