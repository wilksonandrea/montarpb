// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BASE_GAMEGUARD_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_GAMEGUARD_REQ : GameClientPacket
{
  private byte[] byte_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null || player.ChannelId >= 0)
        return;
      ChannelModel channel = AllUtils.GetChannel(this.Client.ServerId, ((PROTOCOL_BASE_ENTER_PASS_REQ) this).int_0);
      if (channel == null)
        return;
      if (!((PROTOCOL_BASE_ENTER_PASS_REQ) this).string_0.Equals(channel.Password))
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_GAME_SERVER_STATE_ACK(2147483648U /*0x80000000*/));
      else
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_GAME_SERVER_STATE_ACK(0U));
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_BASE_EVENT_PORTAL_REQ) this).string_0 = this.ReadS(32 /*0x20*/);
  }
}
