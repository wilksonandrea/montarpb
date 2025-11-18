// Decompiled with JetBrains decompiler
// Type: Plugin.Core.XML.TitleAwardXML
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

public class TitleAwardXML
{
  public static List<TitleAward> Awards;

  public static SChannelModel GetServer(int RankId)
  {
    lock (SChannelXML.Servers)
    {
      foreach (SChannelModel server in SChannelXML.Servers)
      {
        if (server.Id == RankId)
          return server;
      }
      return (SChannelModel) null;
    }
  }

  private static void smethod_0(string xmlNode_0, bool rankModel_0)
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
                if ("Server".Equals(xmlNode2.Name))
                {
                  XmlNamedNodeMap attributes = (XmlNamedNodeMap) xmlNode2.Attributes;
                  QuickstartModel quickstartModel = new QuickstartModel(attributes.GetNamedItem("Host").Value, ushort.Parse(attributes.GetNamedItem("Port").Value));
                  ((SChannelModel) quickstartModel).Id = int.Parse(attributes.GetNamedItem("Id").Value);
                  ((SChannelModel) quickstartModel).State = bool.Parse(attributes.GetNamedItem("State").Value);
                  ((SChannelModel) quickstartModel).Type = ComDiv.ParseEnum<SChannelType>(attributes.GetNamedItem("Type").Value);
                  quickstartModel.set_IsMobile(bool.Parse(attributes.GetNamedItem("Mobile").Value));
                  ((SChannelModel) quickstartModel).MaxPlayers = int.Parse(attributes.GetNamedItem("MaxPlayers").Value);
                  ((SChannelModel) quickstartModel).ChannelPlayers = int.Parse(attributes.GetNamedItem("MaxPlayers").Value);
                  SChannelModel schannelModel = (SChannelModel) quickstartModel;
                  if (rankModel_0)
                  {
                    SChannelModel server = TitleAwardXML.GetServer(schannelModel.Id);
                    if (server != null)
                    {
                      lock (SChannelXML.Servers)
                      {
                        server.State = bool.Parse(attributes.GetNamedItem("State").Value);
                        server.Host = attributes.GetNamedItem("Host").Value;
                        ((QuickstartModel) server).set_Port(ushort.Parse(attributes.GetNamedItem("Port").Value));
                        server.Type = ComDiv.ParseEnum<SChannelType>(attributes.GetNamedItem("Type").Value);
                        ((QuickstartModel) server).set_IsMobile(bool.Parse(attributes.GetNamedItem("Mobile").Value));
                        server.MaxPlayers = int.Parse(attributes.GetNamedItem("MaxPlayers").Value);
                        server.ChannelPlayers = int.Parse(attributes.GetNamedItem("ChannelPlayers").Value);
                      }
                    }
                    else
                      SChannelXML.Servers.Add(schannelModel);
                  }
                  else
                    SChannelXML.Servers.Add(schannelModel);
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

  private static void smethod_1(string Update = false, [In] int obj1)
  {
    XmlDocument xmlDocument = new XmlDocument();
    using (FileStream inStream = new FileStream(Update, FileMode.Open))
    {
      if (inStream.Length == 0L)
      {
        // ISSUE: reference to a compiler-generated method
        CLogger.Class1.Print("File is empty: " + Update, LoggerType.Warning, (Exception) null);
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
                if ("Server".Equals(xmlNode2.Name))
                {
                  XmlNamedNodeMap attributes = (XmlNamedNodeMap) xmlNode2.Attributes;
                  SChannelModel server = TitleAwardXML.GetServer(obj1);
                  if (server != null)
                  {
                    server.State = bool.Parse(attributes.GetNamedItem("State").Value);
                    server.Host = attributes.GetNamedItem("Host").Value;
                    ((QuickstartModel) server).set_Port(ushort.Parse(attributes.GetNamedItem("Port").Value));
                    server.Type = ComDiv.ParseEnum<SChannelType>(attributes.GetNamedItem("Type").Value);
                    ((QuickstartModel) server).set_IsMobile(bool.Parse(attributes.GetNamedItem("Mobile").Value));
                    server.MaxPlayers = int.Parse(attributes.GetNamedItem("MaxPlayers").Value);
                    server.ChannelPlayers = int.Parse(attributes.GetNamedItem("ChannelPlayers").Value);
                  }
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

  static TitleAwardXML() => SChannelXML.Servers = new List<SChannelModel>();

  public static void Load()
  {
    string str = "Data/Titles/Rewards.xml";
    if (File.Exists(str))
    {
      TitleSystemXML.smethod_0(str);
    }
    else
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("File not found: " + str, LoggerType.Warning, (Exception) null);
    }
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Plugin Loaded: {TitleAwardXML.Awards.Count} Title Awards", LoggerType.Info, (Exception) null);
  }

  public static void Reload()
  {
    TitleAwardXML.Awards.Clear();
    TitleAwardXML.Load();
  }

  public static List<ItemsModel> GetAwards(int id)
  {
    List<ItemsModel> awards = new List<ItemsModel>();
    lock (TitleAwardXML.Awards)
    {
      foreach (TitleAward award in TitleAwardXML.Awards)
      {
        if (((TitleModel) award).get_Id() == id)
          awards.Add(((TitleModel) award).get_Item());
      }
    }
    return awards;
  }
}
