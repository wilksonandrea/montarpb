// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BASE_GET_USER_DETAIL_INFO_REQ
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

public class PROTOCOL_BASE_GET_USER_DETAIL_INFO_REQ : GameClientPacket
{
  private int int_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK(player));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_LOBBY_NEW_MYINFO_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ) this).int_0 = (int) this.ReadC();
    for (int index = 0; index < 3; ++index)
      ((PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ) this).list_1.Add(new QuickstartModel()
      {
        MapId = (int) this.ReadC(),
        Rule = (int) this.ReadC(),
        StageOptions = (int) this.ReadC(),
        Type = (int) this.ReadC()
      });
  }
}
