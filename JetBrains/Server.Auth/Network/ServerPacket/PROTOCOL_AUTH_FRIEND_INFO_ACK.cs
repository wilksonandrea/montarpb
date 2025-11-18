// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ServerPacket.PROTOCOL_AUTH_FRIEND_INFO_ACK
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core.Models;
using System.Collections.Generic;

#nullable disable
namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_AUTH_FRIEND_INFO_ACK : AuthServerPacket
{
  private readonly List<FriendModel> list_0;

  public virtual void Write()
  {
    this.WriteH((short) 7682);
    this.WriteH((short) 0);
  }
}
