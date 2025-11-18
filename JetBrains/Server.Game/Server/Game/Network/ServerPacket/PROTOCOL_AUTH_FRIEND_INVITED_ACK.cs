// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_AUTH_FRIEND_INVITED_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_AUTH_FRIEND_INVITED_ACK : GameServerPacket
{
  private readonly uint uint_0;

  public PROTOCOL_AUTH_FRIEND_INVITED_ACK(
    FriendChangeState uint_1,
    FriendModel friendModel_1,
    [In] int obj2,
    [In] int obj3)
  {
    ((PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK) this).friendChangeState_0 = uint_1;
    ((PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK) this).friendState_0 = (FriendState) obj2;
    ((PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK) this).friendModel_0 = friendModel_1;
    ((PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK) this).int_0 = obj3;
  }

  public PROTOCOL_AUTH_FRIEND_INVITED_ACK(
    [In] FriendChangeState obj0,
    [In] FriendModel obj1,
    FriendState int_1,
    [In] int obj3)
  {
    ((PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK) this).friendChangeState_0 = obj0;
    ((PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK) this).friendState_0 = int_1;
    ((PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK) this).friendModel_0 = obj1;
    ((PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK) this).int_0 = obj3;
  }
}
