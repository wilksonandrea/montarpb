// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_AUTH_SHOP_CAPSULE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_AUTH_SHOP_CAPSULE_ACK : GameServerPacket
{
  private readonly List<ItemsModel> list_0;
  private readonly int int_0;
  private readonly int int_1;

  public virtual void Write()
  {
    this.WriteH((short) 1830);
    this.WriteU(((PROTOCOL_AUTH_RECV_WHISPER_ACK) this).string_0, 66);
    this.WriteC((byte) ((PROTOCOL_AUTH_RECV_WHISPER_ACK) this).bool_0);
    this.WriteC((byte) 0);
    this.WriteH((ushort) (((PROTOCOL_AUTH_RECV_WHISPER_ACK) this).string_1.Length + 1));
    this.WriteN(((PROTOCOL_AUTH_RECV_WHISPER_ACK) this).string_1, ((PROTOCOL_AUTH_RECV_WHISPER_ACK) this).string_1.Length + 2, "UTF-16LE");
    this.WriteC((byte) 0);
  }

  public PROTOCOL_AUTH_SHOP_CAPSULE_ACK([In] string obj0, [In] string obj1, uint account_1)
  {
    ((PROTOCOL_AUTH_SEND_WHISPER_ACK) this).string_0 = obj0;
    ((PROTOCOL_AUTH_SEND_WHISPER_ACK) this).string_1 = obj1;
    ((PROTOCOL_AUTH_SEND_WHISPER_ACK) this).uint_0 = account_1;
  }

  public virtual void Write()
  {
    this.WriteH((short) 1827);
    this.WriteD(((PROTOCOL_AUTH_SEND_WHISPER_ACK) this).uint_0);
    this.WriteC((byte) ((PROTOCOL_AUTH_SEND_WHISPER_ACK) this).int_0);
    this.WriteU(((PROTOCOL_AUTH_SEND_WHISPER_ACK) this).string_0, 66);
    if (((PROTOCOL_AUTH_SEND_WHISPER_ACK) this).uint_0 != 0U)
      return;
    this.WriteH((ushort) (((PROTOCOL_AUTH_SEND_WHISPER_ACK) this).string_1.Length + 1));
    this.WriteN(((PROTOCOL_AUTH_SEND_WHISPER_ACK) this).string_1, ((PROTOCOL_AUTH_SEND_WHISPER_ACK) this).string_1.Length + 2, "UTF-16LE");
  }
}
