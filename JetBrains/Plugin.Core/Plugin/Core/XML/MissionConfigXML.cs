// Decompiled with JetBrains decompiler
// Type: Plugin.Core.XML.MissionConfigXML
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

public class MissionConfigXML
{
  public static uint MissionPage1;
  public static uint MissionPage2;
  private static List<MissionStore> list_0;

  public static void Reload()
  {
    MissionAwardXML.list_0.Clear();
    MissionAwardXML.Load();
  }

  public static MissionAwards GetAward([In] int obj0)
  {
    lock (MissionAwardXML.list_0)
    {
      foreach (MissionAwards award in MissionAwardXML.list_0)
      {
        if (award.Id == obj0)
          return award;
      }
      return (MissionAwards) null;
    }
  }

  private static void smethod_0(string ConfigId)
  {
    XmlDocument xmlDocument = new XmlDocument();
    using (FileStream inStream = new FileStream(ConfigId, FileMode.Open))
    {
      if (inStream.Length == 0L)
      {
        // ISSUE: reference to a compiler-generated method
        CLogger.Class1.Print("File is empty: " + ConfigId, LoggerType.Warning, (Exception) null);
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
                if ("Mission".Equals(xmlNode2.Name))
                {
                  XmlAttributeCollection attributes = xmlNode2.Attributes;
                  int num1 = int.Parse(attributes.GetNamedItem("Id").Value);
                  int num2 = int.Parse(attributes.GetNamedItem("MasterMedal").Value);
                  int num3 = int.Parse(attributes.GetNamedItem("Exp").Value);
                  int num4 = int.Parse(attributes.GetNamedItem("Point").Value);
                  MissionAwardXML.list_0.Add((MissionAwards) new MissionCardAwards(num1, num2, num3, num4));
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

  static MissionConfigXML() => MissionAwardXML.list_0 = new List<MissionAwards>();

  public static void Load()
  {
    string path = "Data/MissionConfig.xml";
    if (File.Exists(path))
    {
      PermissionXML.smethod_0(path);
    }
    else
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("File not found: " + path, LoggerType.Warning, (Exception) null);
    }
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Plugin Loaded: {MissionConfigXML.list_0.Count} Mission Stores", LoggerType.Info, (Exception) null);
  }
}
