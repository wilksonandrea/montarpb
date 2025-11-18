// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CS_MEMBER_LIST_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_MEMBER_LIST_ACK : GameServerPacket
{
  private readonly uint uint_0;
  private readonly byte[] byte_0;
  private readonly byte byte_1;
  private readonly byte byte_2;

  public virtual void Write()
  {
    this.WriteH((short) 851);
    this.WriteQ(((PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK) this).account_0.PlayerId);
    this.WriteQ(((PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK) this).ulong_0);
  }

  public PROTOCOL_CS_MEMBER_LIST_ACK(long int_2)
  {
    ((PROTOCOL_CS_MEMBER_INFO_DELETE_ACK) this).long_0 = int_2;
  }
}
