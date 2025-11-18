// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ServerPacket.PROTOCOL_BASE_NOTICE_ACK
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core.Models;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_BASE_NOTICE_ACK : AuthServerPacket
{
  private readonly ServerConfig serverConfig_0;
  private readonly string string_0;
  private readonly string string_1;

  public virtual void Write()
  {
    this.WriteH((short) 2308);
    this.WriteH((short) 0);
  }

  public PROTOCOL_BASE_NOTICE_ACK([In] List<MapMatch> obj0, int account_1)
  {
    ((PROTOCOL_BASE_MAP_MATCHINGLIST_ACK) this).list_0 = obj0;
    ((PROTOCOL_BASE_MAP_MATCHINGLIST_ACK) this).int_0 = account_1;
  }
}
