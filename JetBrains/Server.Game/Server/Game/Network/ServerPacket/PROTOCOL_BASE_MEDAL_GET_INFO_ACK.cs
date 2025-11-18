// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BASE_MEDAL_GET_INFO_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_MEDAL_GET_INFO_ACK : GameServerPacket
{
  private readonly Account account_0;

  public virtual void Write()
  {
    this.WriteH((short) 6660);
    this.WriteH((short) 0);
    this.WriteD(0);
    this.WriteH((short) (byte) ((PROTOCOL_GMCHAT_SEND_CHAT_ACK) this).string_2.Length);
    this.WriteU(((PROTOCOL_GMCHAT_SEND_CHAT_ACK) this).string_2, ((PROTOCOL_GMCHAT_SEND_CHAT_ACK) this).string_2.Length * 2);
    this.WriteC((byte) ((PROTOCOL_GMCHAT_SEND_CHAT_ACK) this).string_1.Length);
    this.WriteU(((PROTOCOL_GMCHAT_SEND_CHAT_ACK) this).string_1, ((PROTOCOL_GMCHAT_SEND_CHAT_ACK) this).string_1.Length * 2);
  }

  public PROTOCOL_BASE_MEDAL_GET_INFO_ACK([In] int obj0)
  {
    ((PROTOCOL_ROOM_CHANGE_OBSERVER_SLOT_ACK) this).int_0 = obj0;
  }
}
