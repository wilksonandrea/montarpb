// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BASE_UNKNOWN_PACKET_REQ
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

public class PROTOCOL_BASE_UNKNOWN_PACKET_REQ : GameClientPacket
{
  public virtual void Run()
  {
    try
    {
      Account player1 = this.Client.Player;
      if (player1 == null)
        return;
      PlayerSession player2 = player1.GetChannel().GetPlayer(((PROTOCOL_BASE_GET_USER_SUBTASK_REQ) this).int_0);
      if (player2 == null)
        return;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_SUPPLAY_BOX_ANNOUNCE_ACK(player2));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_BASE_GET_USER_SUBTASK_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_BASE_RANDOMBOX_LIST_REQ) this).string_0 = this.ReadS(32 /*0x20*/);
  }
}
