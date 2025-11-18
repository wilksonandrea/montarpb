// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_MESSENGER_NOTE_SEND_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_MESSENGER_NOTE_SEND_ACK : GameServerPacket
{
  private readonly uint uint_0;

  public PROTOCOL_MESSENGER_NOTE_SEND_ACK(uint account_1, [In] List<object> obj1)
  {
    ((PROTOCOL_MESSENGER_NOTE_DELETE_ACK) this).uint_0 = account_1;
    ((PROTOCOL_MESSENGER_NOTE_DELETE_ACK) this).list_0 = obj1;
  }

  public virtual void Write()
  {
    this.WriteH((short) 1929);
    this.WriteD(((PROTOCOL_MESSENGER_NOTE_DELETE_ACK) this).uint_0);
    this.WriteC((byte) ((PROTOCOL_MESSENGER_NOTE_DELETE_ACK) this).list_0.Count);
    foreach (long num in ((PROTOCOL_MESSENGER_NOTE_DELETE_ACK) this).list_0)
      this.WriteD((uint) num);
  }
}
