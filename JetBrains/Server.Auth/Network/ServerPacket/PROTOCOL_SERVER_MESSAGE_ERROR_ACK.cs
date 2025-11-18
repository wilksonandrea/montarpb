// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ServerPacket.PROTOCOL_SERVER_MESSAGE_ERROR_ACK
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using System.Runtime.InteropServices;

#nullable disable
namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_SERVER_MESSAGE_ERROR_ACK : AuthServerPacket
{
  private readonly uint uint_0;

  public virtual void Write()
  {
    this.WriteH((short) 1925);
    this.WriteC((byte) ((PROTOCOL_MESSENGER_NOTE_LIST_ACK) this).int_0);
    this.WriteC((byte) ((PROTOCOL_MESSENGER_NOTE_LIST_ACK) this).int_1);
    this.WriteB(((PROTOCOL_MESSENGER_NOTE_LIST_ACK) this).byte_0);
    this.WriteB(((PROTOCOL_MESSENGER_NOTE_LIST_ACK) this).byte_1);
  }

  public PROTOCOL_SERVER_MESSAGE_ERROR_ACK([In] string obj0)
  {
    ((PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK) this).string_0 = obj0;
  }
}
