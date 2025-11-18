// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BATTLE_MISSION_BOMB_UNINSTALL_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_MISSION_BOMB_UNINSTALL_ACK : GameServerPacket
{
  private readonly int int_0;

  public virtual void Write()
  {
    this.WriteH((short) 5134);
    this.WriteD(((PROTOCOL_BATTLE_GIVEUPBATTLE_ACK) this).account_0.SlotId);
    this.WriteC((byte) ((PROTOCOL_BATTLE_GIVEUPBATTLE_ACK) this).int_0);
    this.WriteD(((PROTOCOL_BATTLE_GIVEUPBATTLE_ACK) this).account_0.Exp);
    this.WriteD(((PROTOCOL_BATTLE_GIVEUPBATTLE_ACK) this).account_0.Rank);
    this.WriteD(((PROTOCOL_BATTLE_GIVEUPBATTLE_ACK) this).account_0.Gold);
    this.WriteD(((PROTOCOL_BATTLE_GIVEUPBATTLE_ACK) this).account_0.Statistic.Season.EscapesCount);
    this.WriteD(((PROTOCOL_BATTLE_GIVEUPBATTLE_ACK) this).account_0.Statistic.Basic.EscapesCount);
    this.WriteD(0);
    this.WriteC((byte) 0);
  }

  public PROTOCOL_BATTLE_MISSION_BOMB_UNINSTALL_ACK(RoomModel account_1)
  {
    ((PROTOCOL_BATTLE_LEAVEP2PSERVER_ACK) this).roomModel_0 = account_1;
  }
}
