// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CS_REQUEST_INFO_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_REQUEST_INFO_ACK : GameServerPacket
{
  private readonly string string_0;
  private readonly uint uint_0;
  private readonly Account account_0;

  public virtual void Write()
  {
    this.WriteH((short) 859);
    this.WriteD((uint) ((PROTOCOL_CS_REPLACE_NOTICE_ACK) this).eventErrorEnum_0);
  }

  public PROTOCOL_CS_REQUEST_INFO_ACK(int int_1)
  {
    ((PROTOCOL_CS_REPLACE_PERSONMAX_ACK) this).int_0 = int_1;
  }
}
