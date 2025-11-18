// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ServerPacket.PROTOCOL_BASE_USER_EVENT_SYNC_ACK
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

#nullable disable
namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_BASE_USER_EVENT_SYNC_ACK : AuthServerPacket
{
  public virtual void Write()
  {
    this.WriteH((short) 2455);
    this.WriteH((short) 0);
    this.WriteD(((PROTOCOL_BASE_NOTICE_ACK) this).serverConfig_0.ChatAnnounceColor);
    this.WriteD(((PROTOCOL_BASE_NOTICE_ACK) this).serverConfig_0.ChannelAnnounceColor);
    this.WriteH((short) 0);
    this.WriteH((ushort) ((PROTOCOL_BASE_NOTICE_ACK) this).string_1.Length);
    this.WriteN(((PROTOCOL_BASE_NOTICE_ACK) this).string_1, ((PROTOCOL_BASE_NOTICE_ACK) this).string_1.Length, "UTF-16LE");
    this.WriteH((ushort) ((PROTOCOL_BASE_NOTICE_ACK) this).string_0.Length);
    this.WriteN(((PROTOCOL_BASE_NOTICE_ACK) this).string_0, ((PROTOCOL_BASE_NOTICE_ACK) this).string_0.Length, "UTF-16LE");
  }
}
