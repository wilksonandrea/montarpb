// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK : GameServerPacket
{
  private readonly MessageModel messageModel_0;

  public virtual void Write()
  {
    this.WriteH((short) 7682);
    this.WriteH((short) 0);
  }

  public PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK([In] List<int> obj0)
  {
    ((PROTOCOL_MESSENGER_NOTE_CHECK_READED_ACK) this).list_0 = obj0;
  }

  public virtual void Write()
  {
    this.WriteH((short) 1927);
    this.WriteC((byte) ((PROTOCOL_MESSENGER_NOTE_CHECK_READED_ACK) this).list_0.Count);
    for (int index = 0; index < ((PROTOCOL_MESSENGER_NOTE_CHECK_READED_ACK) this).list_0.Count; ++index)
      this.WriteD(((PROTOCOL_MESSENGER_NOTE_CHECK_READED_ACK) this).list_0[index]);
  }
}
