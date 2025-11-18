// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BATTLEBOX_AUTH_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLEBOX_AUTH_ACK : GameServerPacket
{
  private readonly uint uint_0;
  private readonly Account account_0;
  private readonly int int_0;

  public virtual void Write()
  {
    this.WriteH((short) 2379);
    this.WriteD(((PROTOCOL_BASE_USER_TITLE_EQUIP_ACK) this).uint_0);
    this.WriteC((byte) ((PROTOCOL_BASE_USER_TITLE_EQUIP_ACK) this).int_0);
    this.WriteC((byte) ((PROTOCOL_BASE_USER_TITLE_EQUIP_ACK) this).int_1);
  }

  public PROTOCOL_BATTLEBOX_AUTH_ACK(Account uint_1)
  {
    ((PROTOCOL_BASE_USER_TITLE_INFO_ACK) this).account_0 = uint_1;
  }
}
