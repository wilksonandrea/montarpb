// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ServerPacket.PROTOCOL_BASE_USER_GIFTLIST_ACK
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core.Models;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_BASE_USER_GIFTLIST_ACK : AuthServerPacket
{
  private readonly int int_0;
  private readonly List<MessageModel> list_0;

  public virtual void Write()
  {
    this.WriteH((short) 2323);
    this.WriteD(0);
  }

  public PROTOCOL_BASE_USER_GIFTLIST_ACK([In] ServerConfig obj0)
  {
    ((PROTOCOL_BASE_URL_LIST_ACK) this).serverConfig_0 = obj0;
  }
}
