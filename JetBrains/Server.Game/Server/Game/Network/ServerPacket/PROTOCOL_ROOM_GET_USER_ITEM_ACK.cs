// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_ROOM_GET_USER_ITEM_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_GET_USER_ITEM_ACK : GameServerPacket
{
  private readonly Account account_0;
  private readonly PlayerInventory playerInventory_0;
  private readonly PlayerEquipment playerEquipment_0;

  public PROTOCOL_ROOM_GET_USER_ITEM_ACK(Account int_2)
  {
    ((PROTOCOL_ROOM_GET_SLOTONEINFO_ACK) this).account_0 = int_2;
    if (int_2 == null)
      return;
    ((PROTOCOL_ROOM_GET_SLOTONEINFO_ACK) this).roomModel_0 = int_2.Room;
    ((PROTOCOL_ROOM_GET_SLOTONEINFO_ACK) this).clanModel_0 = ClanManager.GetClan(int_2.ClanId);
  }

  public PROTOCOL_ROOM_GET_USER_ITEM_ACK([In] Account obj0, ClanModel int_3)
  {
    ((PROTOCOL_ROOM_GET_SLOTONEINFO_ACK) this).account_0 = obj0;
    if (obj0 != null)
      ((PROTOCOL_ROOM_GET_SLOTONEINFO_ACK) this).roomModel_0 = obj0.Room;
    ((PROTOCOL_ROOM_GET_SLOTONEINFO_ACK) this).clanModel_0 = int_3;
  }

  public virtual void Write()
  {
    this.WriteH((short) 3588);
    this.WriteH((short) 0);
    this.WriteC((byte) ((PROTOCOL_ROOM_GET_SLOTONEINFO_ACK) this).roomModel_0.GetSlot(((PROTOCOL_ROOM_GET_SLOTONEINFO_ACK) this).account_0.SlotId).Team);
    this.WriteC((byte) ((PROTOCOL_ROOM_GET_SLOTONEINFO_ACK) this).roomModel_0.GetSlot(((PROTOCOL_ROOM_GET_SLOTONEINFO_ACK) this).account_0.SlotId).State);
    this.WriteC((byte) ((PROTOCOL_ROOM_GET_SLOTONEINFO_ACK) this).account_0.GetRank());
    this.WriteD(((PROTOCOL_ROOM_GET_SLOTONEINFO_ACK) this).clanModel_0.Id);
    this.WriteD(((PROTOCOL_ROOM_GET_SLOTONEINFO_ACK) this).account_0.ClanAccess);
    this.WriteC((byte) ((PROTOCOL_ROOM_GET_SLOTONEINFO_ACK) this).clanModel_0.Rank);
    this.WriteD(((PROTOCOL_ROOM_GET_SLOTONEINFO_ACK) this).clanModel_0.Logo);
    this.WriteC((byte) ((PROTOCOL_ROOM_GET_SLOTONEINFO_ACK) this).account_0.CafePC);
    this.WriteC((byte) ((PROTOCOL_ROOM_GET_SLOTONEINFO_ACK) this).account_0.Country);
    this.WriteQ((long) ((PROTOCOL_ROOM_GET_SLOTONEINFO_ACK) this).account_0.Effects);
    this.WriteC((byte) ((PROTOCOL_ROOM_GET_SLOTONEINFO_ACK) this).clanModel_0.Effect);
    this.WriteC((byte) ((PROTOCOL_ROOM_GET_SLOTONEINFO_ACK) this).roomModel_0.GetSlot(((PROTOCOL_ROOM_GET_SLOTONEINFO_ACK) this).account_0.SlotId).ViewType);
    this.WriteC((byte) this.NATIONS);
    this.WriteC((byte) 0);
    this.WriteD(((PROTOCOL_ROOM_GET_SLOTONEINFO_ACK) this).account_0.Equipment.NameCardId);
    this.WriteC((byte) ((PROTOCOL_ROOM_GET_SLOTONEINFO_ACK) this).account_0.Bonus.NickBorderColor);
    this.WriteC((byte) ((PROTOCOL_ROOM_GET_SLOTONEINFO_ACK) this).account_0.AuthLevel());
    this.WriteU(((PROTOCOL_ROOM_GET_SLOTONEINFO_ACK) this).clanModel_0.Name, 34);
    this.WriteC((byte) ((PROTOCOL_ROOM_GET_SLOTONEINFO_ACK) this).account_0.SlotId);
    this.WriteU(((PROTOCOL_ROOM_GET_SLOTONEINFO_ACK) this).account_0.Nickname, 66);
    this.WriteC((byte) ((PROTOCOL_ROOM_GET_SLOTONEINFO_ACK) this).account_0.NickColor);
    this.WriteC((byte) ((PROTOCOL_ROOM_GET_SLOTONEINFO_ACK) this).account_0.Bonus.MuzzleColor);
    this.WriteC((byte) 0);
    this.WriteC(byte.MaxValue);
    this.WriteC(byte.MaxValue);
  }

  public PROTOCOL_ROOM_GET_USER_ITEM_ACK(
    uint roomModel_1,
    [In] PlayerEquipment obj1,
    [In] int[] obj2,
    [In] byte obj3)
  {
    ((PROTOCOL_ROOM_GET_USER_EQUIPMENT_ACK) this).uint_0 = roomModel_1;
    ((PROTOCOL_ROOM_GET_USER_EQUIPMENT_ACK) this).playerEquipment_0 = obj1;
    ((PROTOCOL_ROOM_GET_USER_EQUIPMENT_ACK) this).int_0 = obj2;
    ((PROTOCOL_ROOM_GET_USER_EQUIPMENT_ACK) this).byte_0 = obj3;
  }

  public virtual void Write()
  {
    this.WriteH((short) 3666);
    this.WriteH((short) 0);
    this.WriteD(((PROTOCOL_ROOM_GET_USER_EQUIPMENT_ACK) this).uint_0);
    if (((PROTOCOL_ROOM_GET_USER_EQUIPMENT_ACK) this).uint_0 != 0U)
      return;
    this.WriteD(((PROTOCOL_ROOM_GET_USER_EQUIPMENT_ACK) this).int_0[1]);
    this.WriteC((byte) 10);
    this.WriteD(((PROTOCOL_ROOM_GET_USER_EQUIPMENT_ACK) this).int_0[0]);
    this.WriteD(((PROTOCOL_ROOM_GET_USER_EQUIPMENT_ACK) this).playerEquipment_0.PartHead);
    this.WriteD(((PROTOCOL_ROOM_GET_USER_EQUIPMENT_ACK) this).playerEquipment_0.PartFace);
    this.WriteD(((PROTOCOL_ROOM_GET_USER_EQUIPMENT_ACK) this).playerEquipment_0.PartJacket);
    this.WriteD(((PROTOCOL_ROOM_GET_USER_EQUIPMENT_ACK) this).playerEquipment_0.PartPocket);
    this.WriteD(((PROTOCOL_ROOM_GET_USER_EQUIPMENT_ACK) this).playerEquipment_0.PartGlove);
    this.WriteD(((PROTOCOL_ROOM_GET_USER_EQUIPMENT_ACK) this).playerEquipment_0.PartBelt);
    this.WriteD(((PROTOCOL_ROOM_GET_USER_EQUIPMENT_ACK) this).playerEquipment_0.PartHolster);
    this.WriteD(((PROTOCOL_ROOM_GET_USER_EQUIPMENT_ACK) this).playerEquipment_0.PartSkin);
    this.WriteD(((PROTOCOL_ROOM_GET_USER_EQUIPMENT_ACK) this).playerEquipment_0.BeretItem);
    this.WriteC((byte) 5);
    this.WriteD(((PROTOCOL_ROOM_GET_USER_EQUIPMENT_ACK) this).playerEquipment_0.WeaponPrimary);
    this.WriteD(((PROTOCOL_ROOM_GET_USER_EQUIPMENT_ACK) this).playerEquipment_0.WeaponSecondary);
    this.WriteD(((PROTOCOL_ROOM_GET_USER_EQUIPMENT_ACK) this).playerEquipment_0.WeaponMelee);
    this.WriteD(((PROTOCOL_ROOM_GET_USER_EQUIPMENT_ACK) this).playerEquipment_0.WeaponExplosive);
    this.WriteD(((PROTOCOL_ROOM_GET_USER_EQUIPMENT_ACK) this).playerEquipment_0.WeaponSpecial);
    this.WriteC((byte) 2);
    this.WriteD(((PROTOCOL_ROOM_GET_USER_EQUIPMENT_ACK) this).playerEquipment_0.CharaRedId);
    this.WriteD(((PROTOCOL_ROOM_GET_USER_EQUIPMENT_ACK) this).playerEquipment_0.CharaBlueId);
    this.WriteC(((PROTOCOL_ROOM_GET_USER_EQUIPMENT_ACK) this).byte_0);
  }
}
