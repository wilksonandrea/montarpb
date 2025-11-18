// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_SEASON_CHALLENGE_INFO_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_SEASON_CHALLENGE_INFO_ACK : GameServerPacket
{
  private readonly BattlePassModel battlePassModel_0;
  private readonly PlayerBattlepass playerBattlepass_0;
  private readonly (int, int, int, int) valueTuple_0;
  private readonly int int_0;
  private readonly int int_1;

  public virtual void Write()
  {
    this.WriteH((short) 3622);
    this.WriteC((byte) ((PROTOCOL_ROOM_TEAM_BALANCE_ACK) this).int_0);
    this.WriteC((byte) ((PROTOCOL_ROOM_TEAM_BALANCE_ACK) this).int_1);
    this.WriteC((byte) ((PROTOCOL_ROOM_TEAM_BALANCE_ACK) this).list_0.Count);
    foreach (SlotChange slotChange in ((PROTOCOL_ROOM_TEAM_BALANCE_ACK) this).list_0)
    {
      this.WriteC((byte) slotChange.OldSlot.Id);
      this.WriteC((byte) slotChange.NewSlot.Id);
      this.WriteC((byte) slotChange.OldSlot.State);
      this.WriteC((byte) slotChange.NewSlot.State);
    }
  }
}
