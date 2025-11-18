// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_REQ
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

public class PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_REQ : GameClientPacket
{
  private int int_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_SEASON_CHALLENGE_SEASON_CHANGE(player));
    }
    catch (Exception ex)
    {
      CLogger.Print($"{this.GetType().Name}; {ex.Message}", LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
  }
}
