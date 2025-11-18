// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ServerPacket.PROTOCOL_SERVER_MESSAGE_DISCONNECTIONSUCCESS_ACK
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using System.Runtime.InteropServices;

#nullable disable
namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_SERVER_MESSAGE_DISCONNECTIONSUCCESS_ACK : AuthServerPacket
{
  private readonly uint uint_0;
  private readonly bool bool_0;

  public virtual void Write()
  {
    this.WriteH((short) 851);
    this.WriteQ(((PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK) this).account_0.PlayerId);
    this.WriteQ(((PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK) this).ulong_0);
  }

  public PROTOCOL_SERVER_MESSAGE_DISCONNECTIONSUCCESS_ACK(
    int int_1,
    [In] int obj1,
    [In] byte[] obj2,
    [In] byte[] obj3)
  {
    ((PROTOCOL_MESSENGER_NOTE_LIST_ACK) this).int_1 = int_1;
    ((PROTOCOL_MESSENGER_NOTE_LIST_ACK) this).int_0 = obj1;
    ((PROTOCOL_MESSENGER_NOTE_LIST_ACK) this).byte_0 = obj2;
    ((PROTOCOL_MESSENGER_NOTE_LIST_ACK) this).byte_1 = obj3;
  }
}
