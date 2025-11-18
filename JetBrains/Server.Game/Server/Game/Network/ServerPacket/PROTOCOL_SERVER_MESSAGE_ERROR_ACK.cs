// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_SERVER_MESSAGE_ERROR_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_SERVER_MESSAGE_ERROR_ACK : GameServerPacket
{
  private readonly uint uint_0;

  public virtual void Write()
  {
    this.WriteH((short) 3079);
    this.WriteH((short) 0);
    this.WriteD(0);
    this.WriteH((ushort) ((PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK) this).string_0.Length);
    this.WriteN(((PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK) this).string_0, ((PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK) this).string_0.Length, "UTF-16LE");
    this.WriteD(2);
  }

  public PROTOCOL_SERVER_MESSAGE_ERROR_ACK(Account account_0, [In] SlotModel obj1)
  {
    ((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).list_2 = new List<int>();
    ((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).list_1 = new List<int>();
    if (account_0 == null)
      return;
    ((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerEquipment_1 = account_0.Equipment;
    ((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerInventory_0 = account_0.Inventory;
    ((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerCharacters_0 = account_0.Character;
    ((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).list_0 = account_0.Inventory.GetItemsByType(ItemCategory.Coupon);
    RoomModel room = account_0.Room;
    if (room == null || obj1 == null)
      return;
    ((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerEquipment_0 = obj1.Equipment;
    if (((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerEquipment_0.CharaRedId != ((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerEquipment_1.CharaRedId)
      ((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).list_2.Add(((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerCharacters_0.GetCharacter(((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerEquipment_1.CharaRedId).Slot);
    if (((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerEquipment_0.CharaBlueId != ((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerEquipment_1.CharaBlueId)
      ((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).list_2.Add(((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerCharacters_0.GetCharacter(((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerEquipment_1.CharaBlueId).Slot);
    ((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).int_0 = room.ValidateTeam(obj1.Team, obj1.CostumeTeam) == TeamEnum.FR_TEAM ? ((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerEquipment_1.CharaRedId : ((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerEquipment_1.CharaBlueId;
    if (((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerEquipment_0.DinoItem != ((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerEquipment_1.DinoItem)
      ((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).list_1.Add(((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerEquipment_1.DinoItem);
    if (((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerEquipment_0.SprayId != ((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerEquipment_1.SprayId)
      ((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).list_1.Add(((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerEquipment_1.SprayId);
    if (((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerEquipment_0.NameCardId == ((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerEquipment_1.NameCardId)
      return;
    ((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).list_1.Add(((PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK) this).playerEquipment_1.NameCardId);
  }
}
