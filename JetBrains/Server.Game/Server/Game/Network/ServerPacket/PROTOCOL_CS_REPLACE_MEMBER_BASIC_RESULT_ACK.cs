// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CS_REPLACE_MEMBER_BASIC_RESULT_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_REPLACE_MEMBER_BASIC_RESULT_ACK : GameServerPacket
{
  private readonly Account account_0;
  private readonly ulong ulong_0;

  public virtual void Write()
  {
    this.WriteH((short) 869);
    this.WriteD(((PROTOCOL_CS_REPLACE_MANAGEMENT_ACK) this).uint_0);
  }

  public PROTOCOL_CS_REPLACE_MEMBER_BASIC_RESULT_ACK(int int_3)
  {
    ((PROTOCOL_CS_REPLACE_MARKEFFECT_RESULT_ACK) this).int_0 = int_3;
  }
}
