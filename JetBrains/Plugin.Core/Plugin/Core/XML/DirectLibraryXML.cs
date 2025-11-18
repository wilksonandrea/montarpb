// Decompiled with JetBrains decompiler
// Type: Plugin.Core.XML.DirectLibraryXML
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

public class DirectLibraryXML
{
  public static List<string> HashFiles;

  public static void Reload()
  {
    ClanRankXML.list_0.Clear();
    ClanRankXML.Load();
  }

  public static RankModel GetRank(int xmlNode_0)
  {
    lock (ClanRankXML.list_0)
    {
      foreach (RankModel rank in ClanRankXML.list_0)
      {
        if (rank.Id == xmlNode_0)
          return rank;
      }
      return (RankModel) null;
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
                if ("Rank".Equals(xmlNode2.Name))
                {
                  XmlNamedNodeMap attributes = (XmlNamedNodeMap) xmlNode2.Attributes;
                  VisitBoxModel visitBoxModel = new VisitBoxModel((int) byte.Parse(attributes.GetNamedItem("Id").Value));
                  ((RankModel) visitBoxModel).Title = attributes.GetNamedItem("Title").Value;
                  ((RankModel) visitBoxModel).OnNextLevel = int.Parse(attributes.GetNamedItem("OnNextLevel").Value);
                  ((RankModel) visitBoxModel).OnGoldUp = 0;
                  ((RankModel) visitBoxModel).OnAllExp = int.Parse(attributes.GetNamedItem("OnAllExp").Value);
                  RankModel rankModel = (RankModel) visitBoxModel;
                  ClanRankXML.list_0.Add(rankModel);
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

  static DirectLibraryXML() => ClanRankXML.list_0 = new List<RankModel>();

  public static void Load()
  {
    string path = "Data/DirectLibrary.xml";
    if (File.Exists(path))
    {
      SystemMapXML.smethod_0(path);
    }
    else
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("File not found: " + path, LoggerType.Warning, (Exception) null);
    }
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Plugin Loaded: {DirectLibraryXML.HashFiles.Count} Lib Hases", LoggerType.Info, (Exception) null);
  }
}
