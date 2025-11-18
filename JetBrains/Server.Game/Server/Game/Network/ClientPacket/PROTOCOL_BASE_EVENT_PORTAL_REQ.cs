// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BASE_EVENT_PORTAL_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_EVENT_PORTAL_REQ : GameClientPacket
{
  private string string_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      byte num = 0;
      uint playerEvent_1 = 0;
      PlayerEvent playerEvent = player.Event;
      if (playerEvent != null)
      {
        num = (byte) playerEvent.LastPlaytimeFinish;
        playerEvent_1 = (uint) playerEvent.LastPlaytimeValue;
      }
      StatisticDaily daily = player.Statistic.Daily;
      if (daily == null)
        return;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_GET_CHANNELLIST_ACK(daily, num, playerEvent_1));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_BASE_DAILY_RECORD_REQ: ", LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_BASE_ENTER_PASS_REQ) this).int_0 = (int) this.ReadH();
    ((PROTOCOL_BASE_ENTER_PASS_REQ) this).string_0 = this.ReadS(16 /*0x10*/);
  }
}
