// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ServerPacket.PROTOCOL_BASE_MAP_RULELIST_ACK
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

#nullable disable
namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_BASE_MAP_RULELIST_ACK : AuthServerPacket
{
  public virtual void Write()
  {
    this.WriteH((short) 2313);
    this.WriteC((byte) 3);
    this.WriteH((short) 68);
    this.WriteD(((PROTOCOL_BASE_LOGIN_WAIT_ACK) this).int_0);
  }
}
