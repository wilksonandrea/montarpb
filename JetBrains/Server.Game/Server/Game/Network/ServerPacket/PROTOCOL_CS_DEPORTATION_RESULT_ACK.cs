// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CS_DEPORTATION_RESULT_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_DEPORTATION_RESULT_ACK : GameServerPacket
{
  public virtual void Write()
  {
    this.WriteH((short) 915);
    this.WriteC((byte) ConfigLoader.MinCreateRank);
    this.WriteD(ConfigLoader.MinCreateGold);
  }

  public PROTOCOL_CS_DEPORTATION_RESULT_ACK(int uint_1)
  {
    ((PROTOCOL_CS_DENIAL_REQUEST_ACK) this).int_0 = uint_1;
  }
}
