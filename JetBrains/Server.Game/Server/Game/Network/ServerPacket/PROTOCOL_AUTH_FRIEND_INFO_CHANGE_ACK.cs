// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK : GameServerPacket
{
  private readonly FriendModel friendModel_0;
  private readonly int int_0;
  private readonly FriendState friendState_0;
  private readonly FriendChangeState friendChangeState_0;

  public virtual void Write()
  {
    this.WriteH((short) 1817);
    this.WriteD(((PROTOCOL_AUTH_FRIEND_ACCEPT_ACK) this).uint_0);
  }

  public PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK([In] uint obj0)
  {
    ((PROTOCOL_AUTH_FRIEND_DELETE_ACK) this).uint_0 = obj0;
  }

  public virtual void Write()
  {
    this.WriteH((short) 1821);
    this.WriteD(((PROTOCOL_AUTH_FRIEND_DELETE_ACK) this).uint_0);
  }

  public PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(List<FriendModel> int_1)
  {
    ((PROTOCOL_AUTH_FRIEND_INFO_ACK) this).list_0 = int_1;
  }
}
