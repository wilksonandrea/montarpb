// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CS_REQUEST_LIST_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.SQL;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_REQUEST_LIST_REQ : GameClientPacket
{
  private int int_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_REQUEST_LIST_ACK(player.ClanId));
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read() => ((PROTOCOL_CS_REQUEST_INFO_REQ) this).long_0 = this.ReadQ();

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_ROOM_INVITED_ACK(((PROTOCOL_CS_REQUEST_INFO_REQ) this).long_0, DaoManagerSQL.GetRequestClanInviteText(player.ClanId, ((PROTOCOL_CS_REQUEST_INFO_REQ) this).long_0)));
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }
}
