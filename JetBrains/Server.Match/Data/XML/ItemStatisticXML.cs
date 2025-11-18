// Decompiled with JetBrains decompiler
// Type: Server.Match.Data.XML.ItemStatisticXML
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Server.Match.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;

#nullable disable
namespace Server.Match.Data.XML;

public class ItemStatisticXML
{
  public static List<ItemsStatistic> Stats;

  public static void Reload()
  {
    CharaStructureXML.Charas.Clear();
    CharaStructureXML.Load();
  }

  public static int GetCharaHP([In] int obj0)
  {
    foreach (CharaModel chara in CharaStructureXML.Charas)
    {
      if (((DeathServerData) chara).get_Id() == obj0)
        return ((DeathServerData) chara).get_HP();
    }
    return 100;
  }

  private static void smethod_0([In] string obj0)
  {
    XmlDocument xmlDocument = new XmlDocument();
    using (FileStream inStream = new FileStream(obj0, FileMode.Open))
    {
      if (inStream.Length == 0L)
      {
        CLogger.Print("File is empty: " + obj0, LoggerType.Warning, (Exception) null);
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
                if ("Chara".Equals(xmlNode2.Name))
                {
                  XmlNamedNodeMap attributes = (XmlNamedNodeMap) xmlNode2.Attributes;
                  DeathServerData deathServerData = new DeathServerData();
                  deathServerData.set_Id(int.Parse(attributes.GetNamedItem("Id").Value));
                  deathServerData.set_HP(int.Parse(attributes.GetNamedItem("HP").Value));
                  CharaModel charaModel = (CharaModel) deathServerData;
                  CharaStructureXML.Charas.Add(charaModel);
                }
              }
            }
          }
        }
        catch (XmlException ex)
        {
          CLogger.Print(ex.Message, LoggerType.Error, (Exception) ex);
        }
      }
      inStream.Dispose();
      inStream.Close();
    }
  }

  static ItemStatisticXML() => CharaStructureXML.Charas = new List<CharaModel>();

  public static void Load()
  {
    string str = "Data/Match/ItemStatistics.xml";
    if (File.Exists(str))
      MapStructureXML.smethod_0(str);
    else
      CLogger.Print("File not found: " + str, LoggerType.Warning, (Exception) null);
  }
}
