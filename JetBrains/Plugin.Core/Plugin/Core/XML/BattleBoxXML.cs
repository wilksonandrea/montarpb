// Decompiled with JetBrains decompiler
// Type: Plugin.Core.XML.BattleBoxXML
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Npgsql;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;

#nullable disable
namespace Plugin.Core.XML;

public class BattleBoxXML
{
  public static List<BattleBoxModel> BBoxes;
  public static List<ShopData> ShopDataBattleBoxes;
  public static int TotalBoxes;

  public static bool CreatePlayerCompetitiveDB([In] long obj0)
  {
    if (obj0 == 0L)
      return false;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@owner", (object) obj0);
        ((DbCommand) command).CommandText = "INSERT INTO player_competitive VALUES(@owner);";
        ((DbCommand) command).CommandType = CommandType.Text;
        ((DbCommand) command).ExecuteNonQuery();
        ((Component) command).Dispose();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
      return true;
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  public BattleBoxXML()
  {
    ((ConnectionSQL) this).ConnBuilder = new NpgsqlConnectionStringBuilder()
    {
      Database = ConfigLoader.DatabaseName,
      Host = ConfigLoader.DatabaseHost,
      Username = ConfigLoader.DatabaseUsername,
      Password = ConfigLoader.DatabasePassword,
      Port = ConfigLoader.DatabasePort
    };
  }

  public static ConnectionSQL GetInstance() => ConnectionSQL.connectionSQL_0;

  public NpgsqlConnection Conn()
  {
    return new NpgsqlConnection(((DbConnectionStringBuilder) ((ConnectionSQL) this).ConnBuilder).ConnectionString);
  }

  static BattleBoxXML() => ConnectionSQL.connectionSQL_0 = (ConnectionSQL) new BattleBoxXML();

  public static void Load()
  {
    DirectoryInfo directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory() + "\\Data\\BBoxes");
    if (!directoryInfo.Exists)
      return;
    foreach (FileInfo file in directoryInfo.GetFiles())
    {
      try
      {
        BattleBoxXML.smethod_0(int.Parse(file.Name.Substring(0, file.Name.Length - 4)));
      }
      catch (Exception ex)
      {
        // ISSUE: reference to a compiler-generated method
        CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      }
    }
    BattleRewardXML.smethod_4();
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Plugin Loaded: {BattleBoxXML.BBoxes.Count} Battle Boxes", LoggerType.Info, (Exception) null);
  }

  public static void Reload()
  {
    BattleBoxXML.BBoxes.Clear();
    BattleBoxXML.ShopDataBattleBoxes.Clear();
    BattleBoxXML.TotalBoxes = 0;
  }

  private static void smethod_0([In] int obj0)
  {
    string str = $"Data/BBoxes/{obj0}.xml";
    if (File.Exists(str))
    {
      BattleBoxXML.smethod_1(str, obj0);
    }
    else
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("File not found: " + str, LoggerType.Warning, (Exception) null);
    }
  }

  private static void smethod_1(string OwnerId, [In] int obj1)
  {
    XmlDocument xmlDocument = new XmlDocument();
    using (FileStream inStream = new FileStream(OwnerId, FileMode.Open))
    {
      if (inStream.Length == 0L)
      {
        // ISSUE: reference to a compiler-generated method
        CLogger.Class1.Print("File is empty: " + OwnerId, LoggerType.Warning, (Exception) null);
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
              for (XmlNode OwnerId1 = xmlNode.FirstChild; OwnerId1 != null; OwnerId1 = OwnerId1.NextSibling)
              {
                if ("BattleBox".Equals(OwnerId1.Name))
                {
                  XmlNamedNodeMap attributes = (XmlNamedNodeMap) OwnerId1.Attributes;
                  BattleRewardItem battleRewardItem = new BattleRewardItem();
                  ((BattleBoxModel) battleRewardItem).CouponId = obj1;
                  ((BattleBoxModel) battleRewardItem).RequireTags = int.Parse(attributes.GetNamedItem("RequireTags").Value);
                  ((BattleBoxModel) battleRewardItem).Items = new List<BattleBoxItem>();
                  BattleBoxModel battleBoxModel = (BattleBoxModel) battleRewardItem;
                  BattleBoxXML.smethod_2(OwnerId1, battleBoxModel);
                  ((BattleRewardItem) battleBoxModel).InitItemPercentages();
                  BattleBoxXML.BBoxes.Add(battleBoxModel);
                }
              }
            }
          }
        }
        catch (Exception ex)
        {
          // ISSUE: reference to a compiler-generated method
          CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
        }
      }
      inStream.Dispose();
      inStream.Close();
    }
  }

  private static void smethod_2(XmlNode OwnerId, [In] BattleBoxModel obj1)
  {
    for (XmlNode xmlNode1 = OwnerId.FirstChild; xmlNode1 != null; xmlNode1 = xmlNode1.NextSibling)
    {
      if ("Rewards".Equals(xmlNode1.Name))
      {
        for (XmlNode xmlNode2 = xmlNode1.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
        {
          if ("Good".Equals(xmlNode2.Name))
          {
            XmlNamedNodeMap attributes = (XmlNamedNodeMap) xmlNode2.Attributes;
            BattleBoxModel battleBoxModel = new BattleBoxModel();
            battleBoxModel.set_GoodsId(int.Parse(attributes.GetNamedItem("Id").Value));
            battleBoxModel.set_Percent(int.Parse(attributes.GetNamedItem("Percent").Value));
            BattleBoxItem battleBoxItem = (BattleBoxItem) battleBoxModel;
            obj1.Items.Add(battleBoxItem);
          }
        }
      }
    }
  }

  private static byte[] smethod_3(
    int int_0_1,
    int int_0_2,
    [In] ref int obj2,
    [In] List<BattleBoxModel> obj3)
  {
    obj2 = 0;
    using (SyncServerPacket battleBoxModel_0 = new SyncServerPacket())
    {
      for (int index = int_0_2 * int_0_1; index < obj3.Count; ++index)
      {
        BattleRewardXML.smethod_5(obj3[index], battleBoxModel_0);
        if (++obj2 == int_0_1)
          break;
      }
      return battleBoxModel_0.ToArray();
    }
  }
}
