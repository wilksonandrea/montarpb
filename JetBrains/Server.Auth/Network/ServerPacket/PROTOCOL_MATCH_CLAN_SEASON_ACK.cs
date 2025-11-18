// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ServerPacket.PROTOCOL_MATCH_CLAN_SEASON_ACK
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using System.Runtime.InteropServices;

#nullable disable
namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_MATCH_CLAN_SEASON_ACK : AuthServerPacket
{
  private readonly int int_0;

  public virtual void Write()
  {
    this.WriteH((short) 2484);
    this.WriteC((byte) 0);
    this.WriteD(1);
    this.WriteD(1);
    this.WriteD(1);
    this.WriteD(33602800);
  }

  public PROTOCOL_MATCH_CLAN_SEASON_ACK(byte[] Name, [In] short obj1, [In] byte obj2)
  {
    ((PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_ACK) this).byte_0 = Name;
    ((PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_ACK) this).short_0 = obj1;
    ((PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_ACK) this).byte_1 = obj2;
  }
}
