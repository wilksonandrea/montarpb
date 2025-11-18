// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ServerPacket.PROTOCOL_AUTH_GET_POINT_CASH_ACK
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core.Models;
using Server.Auth.Data.Models;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_AUTH_GET_POINT_CASH_ACK : AuthServerPacket
{
  private readonly uint uint_0;
  private readonly Account account_0;

  public virtual void Write()
  {
    this.WriteH((short) 1989);
    this.WriteC((byte) ((PROTOCOL_AUTH_ACCOUNT_KICK_ACK) this).int_0);
  }

  public PROTOCOL_AUTH_GET_POINT_CASH_ACK([In] List<FriendModel> obj0)
  {
    ((PROTOCOL_AUTH_FRIEND_INFO_ACK) this).list_0 = obj0;
  }
}
