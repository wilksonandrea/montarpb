// Decompiled with JetBrains decompiler
// Type: Plugin.Core.XML.SystemMapXML
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Models.Map;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml;

#nullable disable
namespace Plugin.Core.XML;

public static class SystemMapXML
{
  public static List<MapRule> Rules;
  public static List<MapMatch> Matches;

  public static void Reload()
  {
    DirectLibraryXML.HashFiles.Clear();
    DirectLibraryXML.Load();
  }

  public static bool IsValid(string pccafeModel_0)
  {
    if (string.IsNullOrEmpty(pccafeModel_0))
      return true;
    for (int index = 0; index < DirectLibraryXML.HashFiles.Count; ++index)
    {
      if (DirectLibraryXML.HashFiles[index] == pccafeModel_0)
        return true;
    }
    return false;
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
                if ("D3D9".Equals(xmlNode2.Name))
                {
                  XmlNamedNodeMap attributes = (XmlNamedNodeMap) xmlNode2.Attributes;
                  DirectLibraryXML.HashFiles.Add(attributes.GetNamedItem("MD5").Value);
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

  static SystemMapXML() => DirectLibraryXML.HashFiles = new List<string>();

  public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> string_0, [In] int obj1)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: reference to a compiler-generated method
    return string_0.Select<T, Class0<T, int>>((Func<T, int, Class0<T, int>>) delegate); // Unable to render the statement
  }

  public static void Load()
  {
    SystemMapXML.smethod_0();
    SystemMapXML.smethod_1();
  }

  public static void Reload()
  {
    SystemMapXML.Rules.Clear();
    SystemMapXML.Matches.Clear();
    SystemMapXML.Load();
  }

  private static void smethod_0()
  {
    string str = "Data/Maps/Rules.xml";
    if (File.Exists(str))
    {
      // ISSUE: reference to a compiler-generated method
      SystemMapXML.Class2<>.smethod_2(str);
    }
    else
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("File not found: " + str, LoggerType.Warning, (Exception) null);
    }
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Plugin Loaded: {SystemMapXML.Rules.Count} Map Rules", LoggerType.Info, (Exception) null);
  }

  private static void smethod_1()
  {
    string path = "Data/Maps/Matches.xml";
    if (File.Exists(path))
    {
      // ISSUE: reference to a compiler-generated method
      SystemMapXML.Class2<>.smethod_3(path);
    }
    else
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("File not found: " + path, LoggerType.Warning, (Exception) null);
    }
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Plugin Loaded: {SystemMapXML.Matches.Count} Map Matches", LoggerType.Info, (Exception) null);
  }

  public static MapRule GetMapRule(int string_0)
  {
    lock (SystemMapXML.Rules)
    {
      foreach (MapRule rule in SystemMapXML.Rules)
      {
        if (rule.Id == string_0)
          return rule;
      }
      return (MapRule) null;
    }
  }
}
