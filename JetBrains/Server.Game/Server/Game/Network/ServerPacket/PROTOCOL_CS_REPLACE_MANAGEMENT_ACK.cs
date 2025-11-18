// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CS_REPLACE_MANAGEMENT_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_REPLACE_MANAGEMENT_ACK : GameServerPacket
{
  private readonly uint uint_0;

  public virtual void Write() => this.WriteH((short) 907);

  public PROTOCOL_CS_REPLACE_MANAGEMENT_ACK(int int_1)
  {
    ((PROTOCOL_CS_REPLACE_COLOR_NAME_RESULT_ACK) this).int_0 = int_1;
  }
}
