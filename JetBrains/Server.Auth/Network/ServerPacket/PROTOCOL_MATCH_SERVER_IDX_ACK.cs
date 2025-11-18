// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ServerPacket.PROTOCOL_MATCH_SERVER_IDX_ACK
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

#nullable disable
namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_MATCH_SERVER_IDX_ACK : AuthServerPacket
{
  private readonly short short_0;

  public virtual void Write()
  {
    this.WriteH((short) 2517);
    this.WriteH(((PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_ACK) this).short_0);
    this.WriteC(((PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_ACK) this).byte_1);
    this.WriteB(((PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_ACK) this).byte_0);
  }
}
