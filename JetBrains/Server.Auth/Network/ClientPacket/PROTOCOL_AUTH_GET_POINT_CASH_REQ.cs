// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ClientPacket.PROTOCOL_AUTH_GET_POINT_CASH_REQ
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

#nullable disable
namespace Server.Auth.Network.ClientPacket;

public class PROTOCOL_AUTH_GET_POINT_CASH_REQ : AuthClientPacket
{
  internal int method_0(Mission value) => value.Id;

  public virtual void Read()
  {
  }
}
