// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ClientPacket.PROTOCOL_BASE_GAME_SERVER_STATE_REQ
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Auth.Data.Models;
using Server.Auth.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Auth.Network.ClientPacket;

public class PROTOCOL_BASE_GAME_SERVER_STATE_REQ : AuthClientPacket
{
  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      byte account_1 = 0;
      uint num = 0;
      PlayerEvent playerEvent = player.Event;
      if (playerEvent != null)
      {
        account_1 = (byte) playerEvent.LastPlaytimeFinish;
        num = (uint) playerEvent.LastPlaytimeValue;
      }
      StatisticDaily daily = player.Statistic.Daily;
      if (daily == null)
        return;
      this.Client.SendPacket((AuthServerPacket) new PROTOCOL_BASE_GAME_SERVER_STATE_ACK(daily, account_1, num));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_BASE_DAILY_RECORD_REQ: ", LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    this.ReadB(48 /*0x30*/);
    ((PROTOCOL_BASE_GAMEGUARD_REQ) this).byte_0 = this.ReadB(3);
  }
}
