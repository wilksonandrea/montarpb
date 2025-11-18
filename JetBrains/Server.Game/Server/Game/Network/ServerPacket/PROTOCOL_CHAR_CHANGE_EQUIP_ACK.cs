// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CHAR_CHANGE_EQUIP_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CHAR_CHANGE_EQUIP_ACK : GameServerPacket
{
  private readonly uint uint_0;

  public virtual void Write()
  {
    this.WriteH((short) 5144);
    this.WriteH((short) 0);
  }

  public PROTOCOL_CHAR_CHANGE_EQUIP_ACK(Account roomModel_1)
  {
    ((PROTOCOL_BATTLE_USER_SOPETYPE_ACK) this).account_0 = roomModel_1;
  }
}
