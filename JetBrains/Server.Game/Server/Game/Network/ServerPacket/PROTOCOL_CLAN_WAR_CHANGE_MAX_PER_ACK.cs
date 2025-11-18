// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CLAN_WAR_CHANGE_MAX_PER_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CLAN_WAR_CHANGE_MAX_PER_ACK : GameServerPacket
{
  public readonly MatchModel match;
  public readonly Account Player;

  public virtual void Write()
  {
    this.WriteH((short) 6146);
    this.WriteH((short) 0);
    this.WriteC((byte) 0);
    this.WriteD(((PROTOCOL_CHAR_CREATE_CHARA_ACK) this).uint_0);
    if (((PROTOCOL_CHAR_CREATE_CHARA_ACK) this).uint_0 != 0U)
      return;
    this.WriteB(((PROTOCOL_CHAR_CREATE_CHARA_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_CHAR_CREATE_CHARA_ACK) this).playerEquipment_0.WeaponPrimary));
    this.WriteB(((PROTOCOL_CHAR_CREATE_CHARA_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_CHAR_CREATE_CHARA_ACK) this).playerEquipment_0.WeaponSecondary));
    this.WriteB(((PROTOCOL_CHAR_CREATE_CHARA_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_CHAR_CREATE_CHARA_ACK) this).playerEquipment_0.WeaponMelee));
    this.WriteB(((PROTOCOL_CHAR_CREATE_CHARA_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_CHAR_CREATE_CHARA_ACK) this).playerEquipment_0.WeaponExplosive));
    this.WriteB(((PROTOCOL_CHAR_CREATE_CHARA_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_CHAR_CREATE_CHARA_ACK) this).playerEquipment_0.WeaponSpecial));
    this.WriteB(((PROTOCOL_CHAR_CREATE_CHARA_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_CHAR_CREATE_CHARA_ACK) this).characterModel_0.Id));
    this.WriteB(((PROTOCOL_CHAR_CREATE_CHARA_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_CHAR_CREATE_CHARA_ACK) this).playerEquipment_0.PartHead));
    this.WriteB(((PROTOCOL_CHAR_CREATE_CHARA_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_CHAR_CREATE_CHARA_ACK) this).playerEquipment_0.PartFace));
    this.WriteB(((PROTOCOL_CHAR_CREATE_CHARA_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_CHAR_CREATE_CHARA_ACK) this).playerEquipment_0.PartJacket));
    this.WriteB(((PROTOCOL_CHAR_CREATE_CHARA_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_CHAR_CREATE_CHARA_ACK) this).playerEquipment_0.PartPocket));
    this.WriteB(((PROTOCOL_CHAR_CREATE_CHARA_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_CHAR_CREATE_CHARA_ACK) this).playerEquipment_0.PartGlove));
    this.WriteB(((PROTOCOL_CHAR_CREATE_CHARA_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_CHAR_CREATE_CHARA_ACK) this).playerEquipment_0.PartBelt));
    this.WriteB(((PROTOCOL_CHAR_CREATE_CHARA_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_CHAR_CREATE_CHARA_ACK) this).playerEquipment_0.PartHolster));
    this.WriteB(((PROTOCOL_CHAR_CREATE_CHARA_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_CHAR_CREATE_CHARA_ACK) this).playerEquipment_0.PartSkin));
    this.WriteB(((PROTOCOL_CHAR_CREATE_CHARA_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_CHAR_CREATE_CHARA_ACK) this).playerEquipment_0.BeretItem));
    this.WriteD(((PROTOCOL_CHAR_CREATE_CHARA_ACK) this).account_0.Cash);
    this.WriteD(((PROTOCOL_CHAR_CREATE_CHARA_ACK) this).account_0.Gold);
    this.WriteC(((PROTOCOL_CHAR_CREATE_CHARA_ACK) this).byte_0);
    this.WriteC((byte) 20);
    this.WriteC((byte) ((PROTOCOL_CHAR_CREATE_CHARA_ACK) this).characterModel_0.Slot);
    this.WriteC((byte) 1);
  }

  public PROTOCOL_CLAN_WAR_CHANGE_MAX_PER_ACK(uint characterModel_1)
  {
    ((PROTOCOL_CLAN_WAR_ACCEPT_BATTLE_ACK) this).uint_0 = characterModel_1;
  }
}
