// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_ACK : GameServerPacket
{
  private readonly List<Account> list_0;
  private readonly List<int> list_1;

  public virtual void Write()
  {
    this.WriteH((short) 3628);
    this.WriteD(((PROTOCOL_ROOM_GET_COLOR_NICK_ACK) this).int_0);
    this.WriteC((byte) ((PROTOCOL_ROOM_GET_COLOR_NICK_ACK) this).int_1);
  }

  public virtual void Write() => this.WriteH((short) 3637);
}
