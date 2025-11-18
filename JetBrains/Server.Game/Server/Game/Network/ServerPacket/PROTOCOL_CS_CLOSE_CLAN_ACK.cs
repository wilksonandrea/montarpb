// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CS_CLOSE_CLAN_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_CLOSE_CLAN_ACK : GameServerPacket
{
  private readonly uint uint_0;

  public virtual void Write()
  {
    this.WriteH((short) 998);
    this.WriteH((short) 0);
    this.WriteC((byte) 0);
    this.WriteC(((PROTOCOL_CS_CLAN_LIST_FILTER_ACK) this).byte_0);
    this.WriteH((ushort) ((PROTOCOL_CS_CLAN_LIST_FILTER_ACK) this).int_0);
    this.WriteD(((PROTOCOL_CS_CLAN_LIST_FILTER_ACK) this).int_0);
    this.WriteB(((PROTOCOL_CS_CLAN_LIST_FILTER_ACK) this).byte_1);
  }

  public PROTOCOL_CS_CLOSE_CLAN_ACK(int int_1, int list_1)
  {
    ((PROTOCOL_CS_CLIENT_ENTER_ACK) this).int_1 = int_1;
    ((PROTOCOL_CS_CLIENT_ENTER_ACK) this).int_0 = list_1;
  }
}
