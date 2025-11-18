// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ClientPacket.PROTOCOL_BASE_KEEP_ALIVE_REQ
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Auth.Network.ServerPacket;

#nullable disable
namespace Server.Auth.Network.ClientPacket;

public class PROTOCOL_BASE_KEEP_ALIVE_REQ : AuthClientPacket
{
  public override void Write()
  {
    ((BaseServerPacket) this).WriteH((short) 3079);
    ((BaseServerPacket) this).WriteH((short) 0);
    ((BaseServerPacket) this).WriteD(0);
    ((BaseServerPacket) this).WriteH((ushort) ((PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK) this).string_0.Length);
    ((BaseServerPacket) this).WriteN(((PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK) this).string_0, ((PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK) this).string_0.Length, "UTF-16LE");
    ((BaseServerPacket) this).WriteD(2);
  }

  public PROTOCOL_BASE_KEEP_ALIVE_REQ(uint int_2, bool int_3)
    : this()
  {
    ((PROTOCOL_SERVER_MESSAGE_DISCONNECTIONSUCCESS_ACK) this).uint_0 = int_2;
    ((PROTOCOL_SERVER_MESSAGE_DISCONNECTIONSUCCESS_ACK) this).bool_0 = int_3;
  }

  public override void Write()
  {
    ((BaseServerPacket) this).WriteH((short) 3074);
    ((BaseServerPacket) this).WriteD(uint.Parse(DateTimeUtil.Now("yyMMddHHmm")));
    ((BaseServerPacket) this).WriteD(((PROTOCOL_SERVER_MESSAGE_DISCONNECTIONSUCCESS_ACK) this).uint_0);
    ((BaseServerPacket) this).WriteD(((PROTOCOL_SERVER_MESSAGE_DISCONNECTIONSUCCESS_ACK) this).bool_0 ? 1 : 0);
    if (!((PROTOCOL_SERVER_MESSAGE_DISCONNECTIONSUCCESS_ACK) this).bool_0)
      return;
    ((BaseServerPacket) this).WriteD(0);
  }
}
