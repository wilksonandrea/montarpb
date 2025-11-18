// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BASE_GAME_SERVER_STATE_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_GAME_SERVER_STATE_REQ : GameClientPacket
{
  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      if (!player.LoadedShop)
        player.LoadedShop = true;
      if (Bitwise.ReadFile(Environment.CurrentDirectory + "/Data/Raws/EventPortal.dat") == ((PROTOCOL_BASE_EVENT_PORTAL_REQ) this).string_0)
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_GET_UID_LOBBY_ACK(false));
      else
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_GET_UID_LOBBY_ACK(true));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_BASE_EVENT_PORTAL_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    this.ReadB(48 /*0x30*/);
    ((PROTOCOL_BASE_GAMEGUARD_REQ) this).byte_0 = this.ReadB(3);
  }
}
