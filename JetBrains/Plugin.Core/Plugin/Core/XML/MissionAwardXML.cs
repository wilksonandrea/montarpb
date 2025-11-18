// Decompiled with JetBrains decompiler
// Type: Plugin.Core.XML.MissionAwardXML
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

public class MissionAwardXML
{
  private static List<MissionAwards> list_0;

  public static void Reload()
  {
    InternetCafeXML.Cafes.Clear();
    InternetCafeXML.Load();
  }

  public static InternetCafe GetICafe([In] int obj0)
  {
    lock (InternetCafeXML.Cafes)
    {
      foreach (InternetCafe cafe in InternetCafeXML.Cafes)
      {
        if (cafe.ConfigId == obj0)
          return cafe;
      }
      return (InternetCafe) null;
    }
  }

  public static bool IsValidAddress([In] string obj0, [In] string obj1) => obj0.Equals(obj1);

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
                if ("Bonus".Equals(xmlNode2.Name))
                {
                  XmlNamedNodeMap attributes = (XmlNamedNodeMap) xmlNode2.Attributes;
                  MapMatch mapMatch = new MapMatch(int.Parse(attributes.GetNamedItem("Id").Value));
                  ((InternetCafe) mapMatch).BasicExp = int.Parse(attributes.GetNamedItem("BasicExp").Value);
                  ((InternetCafe) mapMatch).BasicGold = int.Parse(attributes.GetNamedItem("BasicGold").Value);
                  mapMatch.set_PremiumExp(int.Parse(attributes.GetNamedItem("PremiumExp").Value));
                  mapMatch.set_PremiumGold(int.Parse(attributes.GetNamedItem("PremiumGold").Value));
                  InternetCafe internetCafe = (InternetCafe) mapMatch;
                  InternetCafeXML.Cafes.Add(internetCafe);
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

  static MissionAwardXML() => InternetCafeXML.Cafes = new List<InternetCafe>();

  public static void Load()
  {
    string str = "Data/Cards/MissionAwards.xml";
    if (File.Exists(str))
    {
      MissionConfigXML.smethod_0(str);
    }
    else
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("File not found: " + str, LoggerType.Warning, (Exception) null);
    }
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Plugin Loaded: {MissionAwardXML.list_0.Count} Mission Awards", LoggerType.Info, (Exception) null);
  }
}
