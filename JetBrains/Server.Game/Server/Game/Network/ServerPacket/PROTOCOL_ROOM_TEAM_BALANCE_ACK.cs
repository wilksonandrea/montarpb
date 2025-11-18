// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_ROOM_TEAM_BALANCE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_TEAM_BALANCE_ACK : GameServerPacket
{
  private readonly int int_0;
  private readonly int int_1;
  private readonly List<SlotChange> list_0;

  public PROTOCOL_ROOM_TEAM_BALANCE_ACK(uint uint_1)
  {
    ((PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK) this).uint_0 = uint_1;
  }

  public PROTOCOL_ROOM_TEAM_BALANCE_ACK(int uint_1)
  {
    ((PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK) this).uint_0 = (uint) uint_1;
  }
}
