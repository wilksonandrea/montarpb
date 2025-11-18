// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ServerPacket.PROTOCOL_BASE_USER_LEAVE_ACK
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

#nullable disable
namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_BASE_USER_LEAVE_ACK : AuthServerPacket
{
  private readonly int int_0;

  public virtual void Write()
  {
    this.WriteH((short) 2466);
    this.WriteH((short) 514);
    this.WriteH((ushort) ((PROTOCOL_BASE_URL_LIST_ACK) this).serverConfig_0.OfficialBanner.Length);
    this.WriteD(0);
    this.WriteC((byte) 2);
    this.WriteN(((PROTOCOL_BASE_URL_LIST_ACK) this).serverConfig_0.OfficialBanner, ((PROTOCOL_BASE_URL_LIST_ACK) this).serverConfig_0.OfficialBanner.Length, "UTF-16LE");
    this.WriteQ(0L);
  }
}
