// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ServerPacket.PROTOCOL_BASE_GET_CHANNELLIST_ACK
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core.Models;
using Server.Auth.Data.Models;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_BASE_GET_CHANNELLIST_ACK : AuthServerPacket
{
  private readonly SChannelModel schannelModel_0;
  private readonly List<ChannelModel> list_0;

  public PROTOCOL_BASE_GET_CHANNELLIST_ACK(int list_0, [In] byte[] obj1)
  {
    ((PROTOCOL_BASE_GAMEGUARD_ACK) this).int_0 = list_0;
    ((PROTOCOL_BASE_GAMEGUARD_ACK) this).byte_0 = obj1;
  }

  public virtual void Write()
  {
    this.WriteH((short) 2312);
    this.WriteB(((PROTOCOL_BASE_GAMEGUARD_ACK) this).byte_0);
    this.WriteD(((PROTOCOL_BASE_GAMEGUARD_ACK) this).int_0);
  }
}
