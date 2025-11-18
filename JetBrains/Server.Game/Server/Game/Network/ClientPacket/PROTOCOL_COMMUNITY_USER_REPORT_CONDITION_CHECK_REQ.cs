// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_COMMUNITY_USER_REPORT_CONDITION_CHECK_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_COMMUNITY_USER_REPORT_CONDITION_CHECK_REQ : GameClientPacket
{
  public virtual void Run()
  {
    try
    {
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_ACK(0));
      this.Client.Close(0, false, false);
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_COMMUNITY_USER_REPORT_REQ) this).string_1 = this.ReadU((int) this.ReadC() * 2);
    ((PROTOCOL_COMMUNITY_USER_REPORT_REQ) this).reportType_0 = (ReportType) this.ReadC();
    ((PROTOCOL_COMMUNITY_USER_REPORT_REQ) this).string_0 = this.ReadU((int) this.ReadC() * 2);
  }
}
