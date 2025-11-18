// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BASE_UNKNOWN_PACKET_1803_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Utility;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_UNKNOWN_PACKET_1803_ACK : GameServerPacket
{
  private readonly string string_0;
  private readonly string string_1;

  public virtual void Write()
  {
    this.WriteH((short) 2499);
    this.WriteC((byte) ((PROTOCOL_BASE_RANDOMBOX_LIST_ACK) this).bool_0);
    this.WriteD(uint.Parse(DateTimeUtil.Now("yyMMddHHmm")));
  }

  public PROTOCOL_BASE_UNKNOWN_PACKET_1803_ACK(string uint_1)
  {
    ((PROTOCOL_BASE_SUPPLAY_BOX_ANNOUNCE_ACK) this).string_0 = uint_1;
  }
}
