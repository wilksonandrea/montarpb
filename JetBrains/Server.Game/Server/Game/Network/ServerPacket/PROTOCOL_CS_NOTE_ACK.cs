// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CS_NOTE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_NOTE_ACK : GameServerPacket
{
  private readonly int int_0;

  public virtual void Write()
  {
    this.WriteH((short) 849);
    this.WriteQ(((PROTOCOL_CS_MEMBER_INFO_DELETE_ACK) this).long_0);
  }

  public PROTOCOL_CS_NOTE_ACK(Account list_1)
  {
    ((PROTOCOL_CS_MEMBER_INFO_INSERT_ACK) this).account_0 = list_1;
  }
}
