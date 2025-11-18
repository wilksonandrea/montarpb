// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CS_REPLACE_PERSONMAX_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_REPLACE_PERSONMAX_ACK : GameServerPacket
{
  private readonly int int_0;

  public virtual void Write()
  {
    this.WriteH((short) 876);
    this.WriteQ(((PROTOCOL_CS_REPLACE_MEMBER_BASIC_RESULT_ACK) this).account_0.PlayerId);
    this.WriteU(((PROTOCOL_CS_REPLACE_MEMBER_BASIC_RESULT_ACK) this).account_0.Nickname, 66);
    this.WriteC((byte) ((PROTOCOL_CS_REPLACE_MEMBER_BASIC_RESULT_ACK) this).account_0.Rank);
    this.WriteC((byte) ((PROTOCOL_CS_REPLACE_MEMBER_BASIC_RESULT_ACK) this).account_0.ClanAccess);
    this.WriteQ(((PROTOCOL_CS_REPLACE_MEMBER_BASIC_RESULT_ACK) this).ulong_0);
    this.WriteD(((PROTOCOL_CS_REPLACE_MEMBER_BASIC_RESULT_ACK) this).account_0.ClanDate);
    this.WriteC((byte) ((PROTOCOL_CS_REPLACE_MEMBER_BASIC_RESULT_ACK) this).account_0.NickColor);
    this.WriteD(0);
    this.WriteD(0);
  }

  public PROTOCOL_CS_REPLACE_PERSONMAX_ACK(string eventErrorEnum_1)
  {
    ((PROTOCOL_CS_REPLACE_NAME_RESULT_ACK) this).string_0 = eventErrorEnum_1;
  }
}
