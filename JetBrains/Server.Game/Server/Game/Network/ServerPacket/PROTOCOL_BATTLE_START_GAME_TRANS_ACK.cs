// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BATTLE_START_GAME_TRANS_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_START_GAME_TRANS_ACK : GameServerPacket
{
  private readonly RoomModel roomModel_0;
  private readonly SlotModel slotModel_0;
  private readonly PlayerEquipment playerEquipment_0;
  private readonly PlayerTitles playerTitles_0;
  private readonly int int_0;

  public PROTOCOL_BATTLE_START_GAME_TRANS_ACK([In] RoomModel obj0)
  {
    ((PROTOCOL_BATTLE_START_GAME_ACK) this).roomModel_0 = obj0;
  }

  public virtual void Write()
  {
    this.WriteH((short) 5127);
    this.WriteH((short) 0);
    this.WriteB(((PROTOCOL_BATTLE_START_KICKVOTE_ACK) this).method_0(((PROTOCOL_BATTLE_START_GAME_ACK) this).roomModel_0));
    this.WriteB(((PROTOCOL_BATTLE_START_KICKVOTE_ACK) this).method_1(((PROTOCOL_BATTLE_START_GAME_ACK) this).roomModel_0));
    this.WriteB(((PROTOCOL_BATTLE_SUGGEST_KICKVOTE_ACK) this).method_2(((PROTOCOL_BATTLE_START_GAME_ACK) this).roomModel_0));
    this.WriteC((byte) ((PROTOCOL_BATTLE_START_GAME_ACK) this).roomModel_0.MapId);
    this.WriteC((byte) ((PROTOCOL_BATTLE_START_GAME_ACK) this).roomModel_0.Rule);
    this.WriteC((byte) ((PROTOCOL_BATTLE_START_GAME_ACK) this).roomModel_0.Stage);
    this.WriteC((byte) ((PROTOCOL_BATTLE_START_GAME_ACK) this).roomModel_0.RoomType);
  }
}
