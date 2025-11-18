// Decompiled with JetBrains decompiler
// Type: Plugin.Core.XML.RedeemCodeXML
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

#nullable disable
namespace Plugin.Core.XML;

public class RedeemCodeXML
{
  public static List<TicketModel> Tickets;

  private static void smethod_4(string string_0)
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
          for (XmlNode xmlNode1 = xmlDocument.FirstChild; xmlNode1 != null; xmlNode1 = xmlNode1.NextSibling)
          {
            if ("List".Equals(xmlNode1.Name))
            {
              for (XmlNode xmlNode2 = xmlNode1.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
              {
                if ("Permission".Equals(xmlNode2.Name))
                {
                  XmlAttributeCollection attributes = xmlNode2.Attributes;
                  int key = int.Parse(attributes.GetNamedItem("Key").Value);
                  string str1 = attributes.GetNamedItem("Name").Value;
                  string str2 = attributes.GetNamedItem("Description").Value;
                  int num = int.Parse(attributes.GetNamedItem("FakeRank").Value);
                  PermissionXML.sortedList_2.Add(key, num);
                  PermissionXML.sortedList_1.Add((AccessLevel) key, new List<string>());
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

  private static void smethod_5(string Level)
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
              for (XmlNode Permission = xmlNode.FirstChild; Permission != null; Permission = Permission.NextSibling)
              {
                if ("Access".Equals(Permission.Name))
                {
                  AccessLevel Level1 = ComDiv.ParseEnum<AccessLevel>(Permission.Attributes.GetNamedItem(nameof (Level)).Value);
                  RedeemCodeXML.smethod_6(Permission, Level1);
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

  private static void smethod_6(XmlNode Permission, AccessLevel Level)
  {
    for (XmlNode xmlNode1 = Permission.FirstChild; xmlNode1 != null; xmlNode1 = xmlNode1.NextSibling)
    {
      if (nameof (Permission).Equals(xmlNode1.Name))
      {
        for (XmlNode xmlNode2 = xmlNode1.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
        {
          if ("Right".Equals(xmlNode2.Name))
          {
            int key = int.Parse(xmlNode2.Attributes.GetNamedItem("LevelKey").Value);
            if (PermissionXML.sortedList_0.ContainsKey(key))
              PermissionXML.sortedList_1[Level].Add(PermissionXML.sortedList_0[key]);
          }
        }
      }
    }
  }

  static RedeemCodeXML()
  {
    PermissionXML.sortedList_0 = new SortedList<int, string>();
    PermissionXML.sortedList_1 = new SortedList<AccessLevel, List<string>>();
    PermissionXML.sortedList_2 = new SortedList<int, int>();
  }

  public static void Load()
  {
    string str = "Data/RedeemCodes.xml";
    if (File.Exists(str))
    {
      SeasonChallengeXML.smethod_0(str);
    }
    else
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("File not found: " + str, LoggerType.Warning, (Exception) null);
    }
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Plugin Loaded: {RedeemCodeXML.Tickets.Count} Redeem Codes", LoggerType.Info, (Exception) null);
  }

  public static void Reload()
  {
    RedeemCodeXML.Tickets.Clear();
    RedeemCodeXML.Load();
  }
}
