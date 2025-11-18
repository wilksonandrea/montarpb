// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ServerPacket.PROTOCOL_BASE_GAMEGUARD_ACK
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core.Utility;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_BASE_GAMEGUARD_ACK : AuthServerPacket
{
  private readonly int int_0;
  private readonly byte[] byte_0;

  public virtual void Write()
  {
    this.WriteH((short) 2486);
    this.WriteB(new byte[888]);
  }

  public PROTOCOL_BASE_GAMEGUARD_ACK([In] AuthClient obj0)
  {
    ((PROTOCOL_BASE_CONNECT_ACK) this).int_0 = obj0.SessionId;
    ((PROTOCOL_BASE_CONNECT_ACK) this).ushort_0 = obj0.SessionSeed;
    ((PROTOCOL_BASE_CONNECT_ACK) this).list_0 = Bitwise.GenerateRSAKeyPair(((PROTOCOL_BASE_CONNECT_ACK) this).int_0, this.SECURITY_KEY, this.SEED_LENGTH);
  }
}
