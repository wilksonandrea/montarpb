// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BATTLE_TIMEOUTCLIENT_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Plugin.Core.Utility;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_TIMEOUTCLIENT_ACK : GameServerPacket
{
  public virtual void Write()
  {
    this.WriteH((short) 5128);
    this.WriteH((short) 0);
    this.WriteD((uint) ((PROTOCOL_BATTLE_START_GAME_TRANS_ACK) this).slotModel_0.PlayerId);
    this.WriteC((byte) 2);
    this.WriteC((byte) ((PROTOCOL_BATTLE_START_GAME_TRANS_ACK) this).slotModel_0.Id);
    this.WriteD(((PROTOCOL_BATTLE_START_GAME_TRANS_ACK) this).int_0);
    this.WriteD(((PROTOCOL_BATTLE_START_GAME_TRANS_ACK) this).playerEquipment_0.WeaponPrimary);
    this.WriteD(((PROTOCOL_BATTLE_START_GAME_TRANS_ACK) this).playerEquipment_0.WeaponSecondary);
    this.WriteD(((PROTOCOL_BATTLE_START_GAME_TRANS_ACK) this).playerEquipment_0.WeaponMelee);
    this.WriteD(((PROTOCOL_BATTLE_START_GAME_TRANS_ACK) this).playerEquipment_0.WeaponExplosive);
    this.WriteD(((PROTOCOL_BATTLE_START_GAME_TRANS_ACK) this).playerEquipment_0.WeaponSpecial);
    this.WriteD(((PROTOCOL_BATTLE_START_GAME_TRANS_ACK) this).int_0);
    this.WriteD(((PROTOCOL_BATTLE_START_GAME_TRANS_ACK) this).playerEquipment_0.PartHead);
    this.WriteD(((PROTOCOL_BATTLE_START_GAME_TRANS_ACK) this).playerEquipment_0.PartFace);
    this.WriteD(((PROTOCOL_BATTLE_START_GAME_TRANS_ACK) this).playerEquipment_0.PartJacket);
    this.WriteD(((PROTOCOL_BATTLE_START_GAME_TRANS_ACK) this).playerEquipment_0.PartPocket);
    this.WriteD(((PROTOCOL_BATTLE_START_GAME_TRANS_ACK) this).playerEquipment_0.PartGlove);
    this.WriteD(((PROTOCOL_BATTLE_START_GAME_TRANS_ACK) this).playerEquipment_0.PartBelt);
    this.WriteD(((PROTOCOL_BATTLE_START_GAME_TRANS_ACK) this).playerEquipment_0.PartHolster);
    this.WriteD(((PROTOCOL_BATTLE_START_GAME_TRANS_ACK) this).playerEquipment_0.PartSkin);
    this.WriteD(((PROTOCOL_BATTLE_START_GAME_TRANS_ACK) this).playerEquipment_0.BeretItem);
    this.WriteB(Bitwise.HexStringToByteArray("64 64 64 64 64"));
    this.WriteC((byte) ((PROTOCOL_BATTLE_START_GAME_TRANS_ACK) this).playerTitles_0.Equiped1);
    this.WriteC((byte) ((PROTOCOL_BATTLE_START_GAME_TRANS_ACK) this).playerTitles_0.Equiped2);
    this.WriteC((byte) ((PROTOCOL_BATTLE_START_GAME_TRANS_ACK) this).playerTitles_0.Equiped3);
    this.WriteD(((PROTOCOL_BATTLE_START_GAME_TRANS_ACK) this).playerEquipment_0.AccessoryId);
    this.WriteD(((PROTOCOL_BATTLE_START_GAME_TRANS_ACK) this).playerEquipment_0.SprayId);
    this.WriteD(((PROTOCOL_BATTLE_START_GAME_TRANS_ACK) this).playerEquipment_0.NameCardId);
  }

  public PROTOCOL_BATTLE_TIMEOUTCLIENT_ACK(VoteKickModel roomModel_1)
  {
    ((PROTOCOL_BATTLE_START_KICKVOTE_ACK) this).voteKickModel_0 = roomModel_1;
  }
}
