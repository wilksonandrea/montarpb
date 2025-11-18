// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_SERVER_MESSAGE_INVITED_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Server.Game.Data.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_SERVER_MESSAGE_INVITED_ACK : GameServerPacket
{
  private readonly Account account_0;
  private readonly RoomModel roomModel_0;

  public virtual void Write()
  {
    this.WriteH((short) 3082);
    this.WriteH((short) 0);
    this.WriteC((byte) ((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).list_2.Count);
    foreach (byte Half in ((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).list_2)
    {
      this.WriteC(Half);
      this.WriteC((byte) 0);
      this.WriteC((byte) 0);
      this.WriteC((byte) 0);
    }
    this.WriteC((byte) 0);
    this.WriteB(((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerEquipment_1.AccessoryId));
    this.WriteC((byte) ((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).list_0.Count);
    foreach (ItemsModel itemsModel in ((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).list_0)
      this.WriteD(itemsModel.Id);
    this.WriteC((byte) 0);
    this.WriteC((byte) 0);
    this.WriteC((byte) 0);
    this.WriteB(((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerEquipment_1.WeaponPrimary));
    this.WriteB(((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerEquipment_1.WeaponSecondary));
    this.WriteB(((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerEquipment_1.WeaponMelee));
    this.WriteB(((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerEquipment_1.WeaponExplosive));
    this.WriteB(((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerEquipment_1.WeaponSpecial));
    this.WriteB(((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).int_0));
    this.WriteB(((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerEquipment_1.PartHead));
    this.WriteB(((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerEquipment_1.PartFace));
    this.WriteB(((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerEquipment_1.PartJacket));
    this.WriteB(((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerEquipment_1.PartPocket));
    this.WriteB(((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerEquipment_1.PartGlove));
    this.WriteB(((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerEquipment_1.PartBelt));
    this.WriteB(((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerEquipment_1.PartHolster));
    this.WriteB(((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerEquipment_1.PartSkin));
    this.WriteB(((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerEquipment_1.BeretItem));
    this.WriteC((byte) ((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).list_1.Count);
    foreach (int num in ((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).list_1)
      this.WriteB(((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerInventory_0.EquipmentData(num));
  }

  public PROTOCOL_SERVER_MESSAGE_INVITED_ACK(uint uint_1, bool int_1)
  {
    ((PROTOCOL_SERVER_MESSAGE_DISCONNECTED_HACK) this).uint_0 = uint_1;
    ((PROTOCOL_SERVER_MESSAGE_DISCONNECTED_HACK) this).bool_0 = int_1;
  }
}
