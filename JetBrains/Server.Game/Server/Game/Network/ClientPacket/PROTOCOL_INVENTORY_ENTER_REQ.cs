// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_INVENTORY_ENTER_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_INVENTORY_ENTER_REQ : GameClientPacket
{
  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null || !player.IsGM())
        return;
      long playerId = player.GetChannel().GetPlayer(((PROTOCOL_GM_LOG_LOBBY_REQ) this).int_0).PlayerId;
      if (playerId <= 0L)
        return;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_MEDAL_INFO_ACK(0U, playerId));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_GM_LOG_LOBBY_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read() => ((PROTOCOL_GM_LOG_ROOM_REQ) this).int_0 = (int) this.ReadC();
}
