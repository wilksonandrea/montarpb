// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BASE_USER_TITLE_EQUIP_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_USER_TITLE_EQUIP_REQ : GameClientPacket
{
  private byte byte_0;
  private byte byte_1;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      PlayerReport report = player.Report;
      if (report == null)
        return;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_FRIEND_INSERT_REQ(report.TicketCount));
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_BASE_USER_TITLE_CHANGE_REQ) this).int_0 = (int) this.ReadC();
  }
}
