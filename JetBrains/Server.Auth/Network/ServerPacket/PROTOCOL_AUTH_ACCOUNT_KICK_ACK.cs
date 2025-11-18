// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ServerPacket.PROTOCOL_AUTH_ACCOUNT_KICK_ACK
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core.Utility;

#nullable disable
namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_AUTH_ACCOUNT_KICK_ACK : AuthServerPacket
{
  private readonly int int_0;

  public virtual void Write()
  {
    this.WriteH((short) 7700);
    this.WriteH((short) 0);
    this.WriteD(2);
    this.WriteB(ComDiv.AddressBytes("127.0.0.1"));
    this.WriteB(ComDiv.AddressBytes("255.255.255.255"));
    this.WriteB(new byte[109]);
    this.WriteB(ComDiv.AddressBytes("127.0.0.1"));
    this.WriteB(ComDiv.AddressBytes("127.0.0.1"));
    this.WriteB(ComDiv.AddressBytes("255.255.255.255"));
    this.WriteB(new byte[103]);
  }

  public PROTOCOL_AUTH_ACCOUNT_KICK_ACK(short disposing)
  {
    ((PROTOCOL_MATCH_SERVER_IDX_ACK) this).short_0 = disposing;
  }
}
