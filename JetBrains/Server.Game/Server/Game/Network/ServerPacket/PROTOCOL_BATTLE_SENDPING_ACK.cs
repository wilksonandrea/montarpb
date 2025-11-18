// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BATTLE_SENDPING_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_SENDPING_ACK : GameServerPacket
{
  private readonly byte[] byte_0;

  public PROTOCOL_BATTLE_SENDPING_ACK(RoomModel account_1, SlotModel bool_2)
  {
    ((PROTOCOL_BATTLE_RESPAWN_ACK) this).roomModel_0 = account_1;
    ((PROTOCOL_BATTLE_RESPAWN_ACK) this).slotModel_0 = bool_2;
    if (account_1 == null || bool_2 == null)
      return;
    ((PROTOCOL_BATTLE_RESPAWN_ACK) this).playerEquipment_0 = bool_2.Equipment;
    if (((PROTOCOL_BATTLE_RESPAWN_ACK) this).playerEquipment_0 != null)
    {
      switch (account_1.ValidateTeam(bool_2.Team, bool_2.CostumeTeam))
      {
        case TeamEnum.FR_TEAM:
          ((PROTOCOL_BATTLE_RESPAWN_ACK) this).int_0 = ((PROTOCOL_BATTLE_RESPAWN_ACK) this).playerEquipment_0.CharaRedId;
          break;
        case TeamEnum.CT_TEAM:
          ((PROTOCOL_BATTLE_RESPAWN_ACK) this).int_0 = ((PROTOCOL_BATTLE_RESPAWN_ACK) this).playerEquipment_0.CharaBlueId;
          break;
      }
    }
    ((PROTOCOL_BATTLE_RESPAWN_ACK) this).list_0 = AllUtils.GetDinossaurs(account_1, false, bool_2.Id);
  }

  public virtual void Write()
  {
    this.WriteH((short) 5138);
    this.WriteD(((PROTOCOL_BATTLE_RESPAWN_ACK) this).slotModel_0.Id);
    this.WriteD(((PROTOCOL_BATTLE_RESPAWN_ACK) this).roomModel_0.SpawnsCount++);
    this.WriteD(++((PROTOCOL_BATTLE_RESPAWN_ACK) this).slotModel_0.SpawnsCount);
    this.WriteD(((PROTOCOL_BATTLE_RESPAWN_ACK) this).playerEquipment_0.WeaponPrimary);
    this.WriteD(((PROTOCOL_BATTLE_RESPAWN_ACK) this).playerEquipment_0.WeaponSecondary);
    this.WriteD(((PROTOCOL_BATTLE_RESPAWN_ACK) this).playerEquipment_0.WeaponMelee);
    this.WriteD(((PROTOCOL_BATTLE_RESPAWN_ACK) this).playerEquipment_0.WeaponExplosive);
    this.WriteD(((PROTOCOL_BATTLE_RESPAWN_ACK) this).playerEquipment_0.WeaponSpecial);
    this.WriteB(Bitwise.HexStringToByteArray("64 64 64 64 64"));
    this.WriteD(((PROTOCOL_BATTLE_RESPAWN_ACK) this).int_0);
    this.WriteD(((PROTOCOL_BATTLE_RESPAWN_ACK) this).playerEquipment_0.PartHead);
    this.WriteD(((PROTOCOL_BATTLE_RESPAWN_ACK) this).playerEquipment_0.PartFace);
    this.WriteD(((PROTOCOL_BATTLE_RESPAWN_ACK) this).playerEquipment_0.PartJacket);
    this.WriteD(((PROTOCOL_BATTLE_RESPAWN_ACK) this).playerEquipment_0.PartPocket);
    this.WriteD(((PROTOCOL_BATTLE_RESPAWN_ACK) this).playerEquipment_0.PartGlove);
    this.WriteD(((PROTOCOL_BATTLE_RESPAWN_ACK) this).playerEquipment_0.PartBelt);
    this.WriteD(((PROTOCOL_BATTLE_RESPAWN_ACK) this).playerEquipment_0.PartHolster);
    this.WriteD(((PROTOCOL_BATTLE_RESPAWN_ACK) this).playerEquipment_0.PartSkin);
    this.WriteD(((PROTOCOL_BATTLE_RESPAWN_ACK) this).playerEquipment_0.BeretItem);
    this.WriteD(((PROTOCOL_BATTLE_RESPAWN_ACK) this).playerEquipment_0.AccessoryId);
    this.WriteB(((PROTOCOL_BATTLE_STARTBATTLE_ACK) this).method_0(((PROTOCOL_BATTLE_RESPAWN_ACK) this).roomModel_0, ((PROTOCOL_BATTLE_RESPAWN_ACK) this).list_0));
  }
}
