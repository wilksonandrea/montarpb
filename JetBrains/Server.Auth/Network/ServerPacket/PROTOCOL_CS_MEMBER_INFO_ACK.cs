// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ServerPacket.PROTOCOL_CS_MEMBER_INFO_ACK
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core.Models;
using Server.Auth.Data.Models;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_CS_MEMBER_INFO_ACK : AuthServerPacket
{
  private readonly List<Account> list_0;

  public virtual void Write()
  {
    this.WriteH((short) 2473);
    this.WriteH((short) 0);
  }

  public PROTOCOL_CS_MEMBER_INFO_ACK(int int_1, [In] List<MessageModel> obj1)
  {
    ((PROTOCOL_BASE_USER_GIFTLIST_ACK) this).int_0 = int_1;
    ((PROTOCOL_BASE_USER_GIFTLIST_ACK) this).list_0 = obj1;
  }
}
