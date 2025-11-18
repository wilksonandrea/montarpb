// Decompiled with JetBrains decompiler
// Type: Plugin.Core.XML.PermissionXML
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

public class PermissionXML
{
  private static readonly SortedList<int, string> sortedList_0;
  private static readonly SortedList<AccessLevel, List<string>> sortedList_1;
  private static readonly SortedList<int, int> sortedList_2;

  public static void Reload()
  {
    MissionConfigXML.MissionPage1 = 0U;
    MissionConfigXML.MissionPage2 = 0U;
    MissionConfigXML.list_0.Clear();
    MissionConfigXML.Load();
  }

  public static MissionStore GetMission(int PlayerAddress)
  {
    lock (MissionConfigXML.list_0)
    {
      foreach (MissionStore mission in MissionConfigXML.list_0)
      {
        if (mission.Id == PlayerAddress)
          return mission;
      }
      return (MissionStore) null;
    }
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
                if ("Mission".Equals(xmlNode2.Name))
                {
                  XmlNamedNodeMap attributes = (XmlNamedNodeMap) xmlNode2.Attributes;
                  NHistoryModel nhistoryModel = new NHistoryModel();
                  ((MissionStore) nhistoryModel).Id = int.Parse(attributes.GetNamedItem("Id").Value);
                  nhistoryModel.set_ItemId(int.Parse(attributes.GetNamedItem("ItemId").Value));
                  nhistoryModel.set_Enable(bool.Parse(attributes.GetNamedItem("Enable").Value));
                  MissionStore missionStore = (MissionStore) nhistoryModel;
                  uint num1 = (uint) (1 << missionStore.Id);
                  int num2 = (int) Math.Ceiling((double) missionStore.Id / 32.0);
                  if (((NHistoryModel) missionStore).get_Enable())
                  {
                    switch (num2)
                    {
                      case 1:
                        MissionConfigXML.MissionPage1 += num1;
                        break;
                      case 2:
                        MissionConfigXML.MissionPage2 += num1;
                        break;
                    }
                  }
                  MissionConfigXML.list_0.Add(missionStore);
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

  static PermissionXML() => MissionConfigXML.list_0 = new List<MissionStore>();

  public static void Load()
  {
    PermissionXML.smethod_0();
    PermissionXML.smethod_1();
    PermissionXML.smethod_2();
  }

  public static void Reload()
  {
    PermissionXML.sortedList_0.Clear();
    PermissionXML.sortedList_1.Clear();
    PermissionXML.sortedList_2.Clear();
    PermissionXML.Load();
  }

  private static void smethod_0()
  {
    string str = "Data/Access/Permission.xml";
    if (File.Exists(str))
    {
      PermissionXML.smethod_3(str);
    }
    else
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("File not found: " + str, LoggerType.Warning, (Exception) null);
    }
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Plugin Loaded: {PermissionXML.sortedList_0.Count} Permissions", LoggerType.Info, (Exception) null);
  }

  private static void smethod_1()
  {
    string str = "Data/Access/PermissionLevel.xml";
    if (File.Exists(str))
    {
      RedeemCodeXML.smethod_4(str);
    }
    else
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("File not found: " + str, LoggerType.Warning, (Exception) null);
    }
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Plugin Loaded: {PermissionXML.sortedList_2.Count} Permission Ranks", LoggerType.Info, (Exception) null);
  }

  private static void smethod_2()
  {
    string str = "Data/Access/PermissionRight.xml";
    if (File.Exists(str))
    {
      RedeemCodeXML.smethod_5(str);
    }
    else
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("File not found: " + str, LoggerType.Warning, (Exception) null);
    }
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Plugin Loaded: {PermissionXML.sortedList_1.Count} Level Permission", LoggerType.Info, (Exception) null);
  }

  public static int GetFakeRank(int string_0)
  {
    lock (PermissionXML.sortedList_2)
      return PermissionXML.sortedList_2.ContainsKey(string_0) ? PermissionXML.sortedList_2[string_0] : -1;
  }

  public static bool HavePermission(string MissionId, [In] AccessLevel obj1)
  {
    return PermissionXML.sortedList_1.ContainsKey(obj1) && PermissionXML.sortedList_1[obj1].Contains(MissionId);
  }

  private static void smethod_3(string MissionId)
  {
    XmlDocument xmlDocument = new XmlDocument();
    using (FileStream inStream = new FileStream(MissionId, FileMode.Open))
    {
      if (inStream.Length == 0L)
      {
        // ISSUE: reference to a compiler-generated method
        CLogger.Class1.Print("File is empty: " + MissionId, LoggerType.Warning, (Exception) null);
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
                  if (!PermissionXML.sortedList_0.ContainsKey(key))
                    PermissionXML.sortedList_0.Add(key, str1);
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
}
