// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_COMMUNITY_USER_REPORT_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Plugin.Core.Utility;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_COMMUNITY_USER_REPORT_ACK : GameServerPacket
{
  private readonly uint uint_0;

  public virtual void Write()
  {
    this.WriteH((short) 1803);
    this.WriteD(94767);
    this.WriteD(100950);
    this.WriteD(0);
    this.WriteD(983299);
    this.WriteC((byte) 0);
    this.WriteC((byte) 0);
    this.WriteC((byte) 0);
    this.WriteC((byte) 3);
    this.WriteC((byte) 8);
    this.WriteB(Bitwise.HexStringToByteArray("47 00 4D 00 00 00 45 00 56 00 45 00 4E 00 54 00 5F 00 38 00 00 00"));
    this.WriteD(56);
    this.WriteC((byte) 1);
    this.WriteD(180214952);
    this.WriteB(Bitwise.HexStringToByteArray("81 E0 D0 03 09 04 15 00 80 22"));
  }

  public PROTOCOL_COMMUNITY_USER_REPORT_ACK(ServerConfig uint_1)
  {
    ((PROTOCOL_BASE_URL_LIST_ACK) this).serverConfig_0 = uint_1;
  }
}
