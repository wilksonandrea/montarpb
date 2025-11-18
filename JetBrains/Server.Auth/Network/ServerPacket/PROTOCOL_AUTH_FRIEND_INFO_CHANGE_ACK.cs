// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ServerPacket.PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;

#nullable disable
namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK : AuthServerPacket
{
  private readonly FriendModel friendModel_0;
  private readonly int int_0;
  private readonly FriendState friendState_0;
  private readonly FriendChangeState friendChangeState_0;

  public virtual void Write() => this.WriteH((short) 3086);

  public PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(int byte_2)
  {
    ((PROTOCOL_AUTH_ACCOUNT_KICK_ACK) this).int_0 = byte_2;
  }
}
