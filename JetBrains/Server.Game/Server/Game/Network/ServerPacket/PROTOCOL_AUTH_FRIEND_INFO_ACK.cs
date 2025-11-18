// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_AUTH_FRIEND_INFO_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_AUTH_FRIEND_INFO_ACK : GameServerPacket
{
  private readonly List<FriendModel> list_0;

  public virtual void Write()
  {
    this.WriteH((short) 1834);
    this.WriteH((short) 0);
    this.WriteD(((PROTOCOL_AUTH_FIND_USER_ACK) this).uint_0);
  }

  public PROTOCOL_AUTH_FRIEND_INFO_ACK([In] uint obj0)
  {
    ((PROTOCOL_AUTH_FRIEND_ACCEPT_ACK) this).uint_0 = obj0;
  }
}
