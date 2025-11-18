// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ClientPacket.PROTOCOL_MATCH_CLAN_SEASON_REQ
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core;
using Plugin.Core.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable
namespace Server.Auth.Network.ClientPacket;

public class PROTOCOL_MATCH_CLAN_SEASON_REQ : AuthClientPacket
{
  private static List<Mission> smethod_0(string value)
  {
    List<Mission> source1 = new List<Mission>();
    try
    {
      if (!Directory.Exists(value))
      {
        CLogger.Print("Directory not found: " + value, LoggerType.Warning, (Exception) null);
        return source1;
      }
      foreach (string file in Directory.GetFiles(value, "*.hex"))
      {
        string[] source2 = File.ReadAllLines(file);
        if (source2.Length >= 6)
        {
          Mission mission1 = (Mission) new PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_REQ();
          mission1.Id = int.Parse(source2[0].Split(new char[1]
          {
            '='
          }, 2)[1]);
          mission1.Name = source2[1].Split(new char[1]
          {
            '='
          }, 2)[1];
          ((PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_REQ) mission1).set_Description(source2[2].Split(new char[1]
          {
            '='
          }, 2)[1]);
          mission1.RewardId = int.Parse(source2[3].Split(new char[1]
          {
            '='
          }, 2)[1]);
          mission1.RewardCount = int.Parse(source2[4].Split(new char[1]
          {
            '='
          }, 2)[1]);
          Mission mission2 = mission1;
          string[] strArray = string.Join(" ", ((IEnumerable<string>) source2).Skip<string>(5)).Split(new char[1]
          {
            ' '
          }, StringSplitOptions.RemoveEmptyEntries);
          List<byte> byteList = new List<byte>();
          foreach (string str in strArray)
            byteList.Add(Convert.ToByte(str, 16 /*0x10*/));
          ((PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_REQ) mission2).set_ObjectivesData(byteList.ToArray());
          source1.Add(mission2);
        }
      }
    }
    catch (Exception ex)
    {
      CLogger.Print("Error loading missions: " + ex.Message, LoggerType.Error, ex);
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return source1.OrderBy<Mission, int>(PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_REQ.Class2.\u003C\u003E9__5_0 ?? (PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_REQ.Class2.\u003C\u003E9__5_0 = new Func<Mission, int>(((PROTOCOL_AUTH_GET_POINT_CASH_REQ) PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_REQ.Class2.\u003C\u003E9).method_0))).ToList<Mission>();
  }

  static PROTOCOL_MATCH_CLAN_SEASON_REQ()
  {
    // ISSUE: reference to a compiler-generated field
    PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_REQ.Class2.\u003C\u003E9 = (PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_REQ.Class2) new PROTOCOL_AUTH_GET_POINT_CASH_REQ();
  }
}
