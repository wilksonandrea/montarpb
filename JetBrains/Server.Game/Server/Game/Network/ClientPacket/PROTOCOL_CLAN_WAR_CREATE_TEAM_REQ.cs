// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CLAN_WAR_CREATE_TEAM_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CLAN_WAR_CREATE_TEAM_REQ : GameClientPacket
{
  private int int_0;
  private List<int> list_0;

  public virtual void Run()
  {
    try
    {
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_CLAN_WAR_CREATE_ROOM_ACK());
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_CLAN_WAR_CANCEL_MATCHMAKING_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ) this).int_3 = (int) this.ReadH();
    this.ReadD();
    this.ReadD();
    int num1 = (int) this.ReadH();
    ((PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ) this).string_0 = this.ReadU(46);
    ((PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ) this).mapIdEnum_0 = (MapIdEnum) this.ReadC();
    ((PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ) this).mapRules_0 = (MapRules) this.ReadC();
    ((PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ) this).stageOptions_0 = (StageOptions) this.ReadC();
    ((PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ) this).roomCondition_0 = (RoomCondition) this.ReadC();
    int num2 = (int) this.ReadC();
    int num3 = (int) this.ReadC();
    ((PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ) this).int_1 = (int) this.ReadC();
    ((PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ) this).int_2 = (int) this.ReadC();
    ((PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ) this).roomWeaponsFlag_0 = (RoomWeaponsFlag) this.ReadH();
    ((PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ) this).roomStageFlag_0 = (RoomStageFlag) this.ReadD();
  }
}
