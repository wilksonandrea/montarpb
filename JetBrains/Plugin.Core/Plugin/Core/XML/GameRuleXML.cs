// Decompiled with JetBrains decompiler
// Type: Plugin.Core.XML.GameRuleXML
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

public class GameRuleXML
{
  public static List<TRuleModel> GameRules;

  public static void Reload()
  {
    EventXmasXML.list_0.Clear();
    EventXmasXML.Load();
  }

  public static EventXmasModel GetRunningEvent()
  {
    uint num = uint.Parse(DBQuery.Now("yyMMddHHmm"));
    foreach (EventXmasModel runningEvent in EventXmasXML.list_0)
    {
      if (runningEvent.BeginDate <= num && num < ((MissionAwards) runningEvent).get_EndedDate())
        return runningEvent;
    }
    return (EventXmasModel) null;
  }

  private static void smethod_0(string PlayerId)
  {
    XmlDocument xmlDocument = new XmlDocument();
    using (FileStream inStream = new FileStream(PlayerId, FileMode.Open))
    {
      if (inStream.Length == 0L)
      {
        // ISSUE: reference to a compiler-generated method
        CLogger.Class1.Print("File is empty: " + PlayerId, LoggerType.Warning, (Exception) null);
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
                  MissionAwards missionAwards = new MissionAwards();
                  ((EventXmasModel) missionAwards).BeginDate = uint.Parse(attributes.GetNamedItem("Begin").Value);
                  missionAwards.set_EndedDate(uint.Parse(attributes.GetNamedItem("Ended").Value));
                  missionAwards.set_GoodId(int.Parse(attributes.GetNamedItem("GoodId").Value));
                  EventXmasModel eventXmasModel = (EventXmasModel) missionAwards;
                  EventXmasXML.list_0.Add(eventXmasModel);
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

  static GameRuleXML() => EventXmasXML.list_0 = new List<EventXmasModel>();

  public static void Load()
  {
    string path = "Data/ClassicMode.xml";
    if (File.Exists(path))
    {
      InternetCafeXML.smethod_0(path);
    }
    else
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("File not found: " + path, LoggerType.Warning, (Exception) null);
    }
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Plugin Loaded: {GameRuleXML.GameRules.Count} Game Rules", LoggerType.Info, (Exception) null);
  }

  public static void Reload()
  {
    GameRuleXML.GameRules.Clear();
    GameRuleXML.Load();
  }

  public static TRuleModel CheckTRuleByRoomName([In] string obj0)
  {
    lock (GameRuleXML.GameRules)
    {
      foreach (TRuleModel gameRule in GameRuleXML.GameRules)
      {
        if (obj0.ToLower().Contains(((VisitItemModel) gameRule).get_Name().ToLower()))
          return gameRule;
      }
      return (TRuleModel) null;
    }
  }

  public static bool IsBlocked(int string_0, [In] int obj1) => string_0 == obj1;
}
