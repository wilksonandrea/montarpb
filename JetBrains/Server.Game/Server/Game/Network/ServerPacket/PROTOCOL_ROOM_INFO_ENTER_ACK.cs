// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_ROOM_INFO_ENTER_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_INFO_ENTER_ACK : GameServerPacket
{
  public PROTOCOL_ROOM_INFO_ENTER_ACK([In] Account obj0)
  {
    ((PROTOCOL_ROOM_GET_USER_ITEM_ACK) this).account_0 = obj0;
    if (obj0 == null)
      return;
    ((PROTOCOL_ROOM_GET_USER_ITEM_ACK) this).playerInventory_0 = obj0.Inventory;
    ((PROTOCOL_ROOM_GET_USER_ITEM_ACK) this).playerEquipment_0 = obj0.Equipment;
  }

  public virtual void Write()
  {
    this.WriteH((short) 3646);
    this.WriteH((short) 0);
    this.WriteB(((PROTOCOL_ROOM_GET_USER_ITEM_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_ROOM_GET_USER_ITEM_ACK) this).playerEquipment_0.AccessoryId));
    this.WriteB(((PROTOCOL_ROOM_GET_LOBBY_USER_LIST_ACK) this).method_2());
    this.WriteB(((PROTOCOL_ROOM_INFO_LEAVE_ACK) this).method_1(((PROTOCOL_ROOM_GET_USER_ITEM_ACK) this).playerEquipment_0));
    this.WriteB(((PROTOCOL_ROOM_GET_USER_ITEM_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_ROOM_GET_USER_ITEM_ACK) this).playerEquipment_0.WeaponPrimary));
    this.WriteB(((PROTOCOL_ROOM_GET_USER_ITEM_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_ROOM_GET_USER_ITEM_ACK) this).playerEquipment_0.WeaponSecondary));
    this.WriteB(((PROTOCOL_ROOM_GET_USER_ITEM_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_ROOM_GET_USER_ITEM_ACK) this).playerEquipment_0.WeaponMelee));
    this.WriteB(((PROTOCOL_ROOM_GET_USER_ITEM_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_ROOM_GET_USER_ITEM_ACK) this).playerEquipment_0.WeaponExplosive));
    this.WriteB(((PROTOCOL_ROOM_GET_USER_ITEM_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_ROOM_GET_USER_ITEM_ACK) this).playerEquipment_0.WeaponSpecial));
    this.WriteB(((PROTOCOL_ROOM_INFO_LEAVE_ACK) this).method_0(((PROTOCOL_ROOM_GET_USER_ITEM_ACK) this).account_0, ((PROTOCOL_ROOM_GET_USER_ITEM_ACK) this).playerEquipment_0));
    this.WriteB(((PROTOCOL_ROOM_GET_USER_ITEM_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_ROOM_GET_USER_ITEM_ACK) this).playerEquipment_0.PartHead));
    this.WriteB(((PROTOCOL_ROOM_GET_USER_ITEM_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_ROOM_GET_USER_ITEM_ACK) this).playerEquipment_0.PartFace));
    this.WriteB(((PROTOCOL_ROOM_GET_USER_ITEM_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_ROOM_GET_USER_ITEM_ACK) this).playerEquipment_0.PartJacket));
    this.WriteB(((PROTOCOL_ROOM_GET_USER_ITEM_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_ROOM_GET_USER_ITEM_ACK) this).playerEquipment_0.PartPocket));
    this.WriteB(((PROTOCOL_ROOM_GET_USER_ITEM_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_ROOM_GET_USER_ITEM_ACK) this).playerEquipment_0.PartGlove));
    this.WriteB(((PROTOCOL_ROOM_GET_USER_ITEM_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_ROOM_GET_USER_ITEM_ACK) this).playerEquipment_0.PartBelt));
    this.WriteB(((PROTOCOL_ROOM_GET_USER_ITEM_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_ROOM_GET_USER_ITEM_ACK) this).playerEquipment_0.PartHolster));
    this.WriteB(((PROTOCOL_ROOM_GET_USER_ITEM_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_ROOM_GET_USER_ITEM_ACK) this).playerEquipment_0.PartSkin));
    this.WriteB(((PROTOCOL_ROOM_GET_USER_ITEM_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_ROOM_GET_USER_ITEM_ACK) this).playerEquipment_0.BeretItem));
  }
}
