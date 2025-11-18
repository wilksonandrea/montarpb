// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ServerPacket.PROTOCOL_BASE_GET_SYSTEM_INFO_ACK
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core.Models;
using Plugin.Core.Utility;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_BASE_GET_SYSTEM_INFO_ACK : AuthServerPacket
{
  private readonly ServerConfig serverConfig_0;
  private readonly List<SChannelModel> list_0;
  private readonly List<RankModel> list_1;
  private readonly EventPlaytimeModel eventPlaytimeModel_0;
  private readonly string[] string_0;

  public virtual void Write()
  {
    this.WriteH((short) 2453);
    this.WriteH((short) 0);
    this.WriteC((byte) ((PROTOCOL_BASE_GET_CHARA_INFO_ACK) this).list_0.Count);
    foreach (CharacterModel characterModel in ((PROTOCOL_BASE_GET_CHARA_INFO_ACK) this).list_0)
    {
      this.WriteC((byte) characterModel.Slot);
      this.WriteC((byte) 20);
      this.WriteB(((PROTOCOL_BASE_GET_CHARA_INFO_ACK) this).playerInventory_0.EquipmentData(characterModel.Id));
    }
    foreach (CharacterModel characterModel in ((PROTOCOL_BASE_GET_CHARA_INFO_ACK) this).list_0)
    {
      this.WriteB(((PROTOCOL_BASE_GET_CHARA_INFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_BASE_GET_CHARA_INFO_ACK) this).playerEquipment_0.WeaponPrimary));
      this.WriteB(((PROTOCOL_BASE_GET_CHARA_INFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_BASE_GET_CHARA_INFO_ACK) this).playerEquipment_0.WeaponSecondary));
      this.WriteB(((PROTOCOL_BASE_GET_CHARA_INFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_BASE_GET_CHARA_INFO_ACK) this).playerEquipment_0.WeaponMelee));
      this.WriteB(((PROTOCOL_BASE_GET_CHARA_INFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_BASE_GET_CHARA_INFO_ACK) this).playerEquipment_0.WeaponExplosive));
      this.WriteB(((PROTOCOL_BASE_GET_CHARA_INFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_BASE_GET_CHARA_INFO_ACK) this).playerEquipment_0.WeaponSpecial));
      this.WriteB(((PROTOCOL_BASE_GET_CHARA_INFO_ACK) this).playerInventory_0.EquipmentData(characterModel.Id));
      this.WriteB(((PROTOCOL_BASE_GET_CHARA_INFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_BASE_GET_CHARA_INFO_ACK) this).playerEquipment_0.PartHead));
      this.WriteB(((PROTOCOL_BASE_GET_CHARA_INFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_BASE_GET_CHARA_INFO_ACK) this).playerEquipment_0.PartFace));
      this.WriteB(((PROTOCOL_BASE_GET_CHARA_INFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_BASE_GET_CHARA_INFO_ACK) this).playerEquipment_0.PartJacket));
      this.WriteB(((PROTOCOL_BASE_GET_CHARA_INFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_BASE_GET_CHARA_INFO_ACK) this).playerEquipment_0.PartPocket));
      this.WriteB(((PROTOCOL_BASE_GET_CHARA_INFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_BASE_GET_CHARA_INFO_ACK) this).playerEquipment_0.PartGlove));
      this.WriteB(((PROTOCOL_BASE_GET_CHARA_INFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_BASE_GET_CHARA_INFO_ACK) this).playerEquipment_0.PartBelt));
      this.WriteB(((PROTOCOL_BASE_GET_CHARA_INFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_BASE_GET_CHARA_INFO_ACK) this).playerEquipment_0.PartHolster));
      this.WriteB(((PROTOCOL_BASE_GET_CHARA_INFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_BASE_GET_CHARA_INFO_ACK) this).playerEquipment_0.PartSkin));
      this.WriteB(((PROTOCOL_BASE_GET_CHARA_INFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_BASE_GET_CHARA_INFO_ACK) this).playerEquipment_0.BeretItem));
    }
  }

  public PROTOCOL_BASE_GET_SYSTEM_INFO_ACK([In] uint obj0, List<ItemsModel> byte_1, [In] int obj2)
  {
    ((PROTOCOL_BASE_GET_INVEN_INFO_ACK) this).uint_0 = obj0;
    ((PROTOCOL_BASE_GET_INVEN_INFO_ACK) this).list_0 = byte_1;
    ((PROTOCOL_BASE_GET_INVEN_INFO_ACK) this).int_0 = obj2;
  }

  public virtual void Write()
  {
    this.WriteH((short) 2319);
    this.WriteH((short) 0);
    this.WriteD(((PROTOCOL_BASE_GET_INVEN_INFO_ACK) this).uint_0);
    if (((PROTOCOL_BASE_GET_INVEN_INFO_ACK) this).uint_0 != 0U)
      return;
    this.WriteH((ushort) ((PROTOCOL_BASE_GET_INVEN_INFO_ACK) this).list_0.Count);
    foreach (ItemsModel itemsModel in ((PROTOCOL_BASE_GET_INVEN_INFO_ACK) this).list_0)
    {
      this.WriteD((uint) itemsModel.ObjectId);
      this.WriteD(itemsModel.Id);
      this.WriteC((byte) itemsModel.Equip);
      this.WriteD(itemsModel.Count);
    }
    this.WriteH((ushort) ((PROTOCOL_BASE_GET_INVEN_INFO_ACK) this).int_0);
    this.WriteH((ushort) ((PROTOCOL_BASE_GET_INVEN_INFO_ACK) this).list_0.Count);
    this.WriteH((ushort) ((PROTOCOL_BASE_GET_INVEN_INFO_ACK) this).list_0.Count);
    this.WriteH((ushort) ((PROTOCOL_BASE_GET_INVEN_INFO_ACK) this).list_0.Count);
    this.WriteH((short) 0);
  }

  public PROTOCOL_BASE_GET_SYSTEM_INFO_ACK([In] int obj0, PlayerConfig list_1)
  {
    ((PROTOCOL_BASE_GET_OPTION_ACK) this).int_0 = obj0;
    ((PROTOCOL_BASE_GET_OPTION_ACK) this).playerConfig_0 = list_1;
  }

  public virtual void Write()
  {
    this.WriteH((short) 2321);
    this.WriteH((short) 0);
    this.WriteD(((PROTOCOL_BASE_GET_OPTION_ACK) this).int_0);
    if (((PROTOCOL_BASE_GET_OPTION_ACK) this).int_0 != 0)
      return;
    this.WriteH((ushort) ((PROTOCOL_BASE_GET_OPTION_ACK) this).playerConfig_0.Nations);
    this.WriteH((ushort) ((PROTOCOL_BASE_GET_OPTION_ACK) this).playerConfig_0.Macro5.Length);
    this.WriteN(((PROTOCOL_BASE_GET_OPTION_ACK) this).playerConfig_0.Macro5, ((PROTOCOL_BASE_GET_OPTION_ACK) this).playerConfig_0.Macro5.Length, "UTF-16LE");
    this.WriteH((ushort) ((PROTOCOL_BASE_GET_OPTION_ACK) this).playerConfig_0.Macro4.Length);
    this.WriteN(((PROTOCOL_BASE_GET_OPTION_ACK) this).playerConfig_0.Macro4, ((PROTOCOL_BASE_GET_OPTION_ACK) this).playerConfig_0.Macro4.Length, "UTF-16LE");
    this.WriteH((ushort) ((PROTOCOL_BASE_GET_OPTION_ACK) this).playerConfig_0.Macro3.Length);
    this.WriteN(((PROTOCOL_BASE_GET_OPTION_ACK) this).playerConfig_0.Macro3, ((PROTOCOL_BASE_GET_OPTION_ACK) this).playerConfig_0.Macro3.Length, "UTF-16LE");
    this.WriteH((ushort) ((PROTOCOL_BASE_GET_OPTION_ACK) this).playerConfig_0.Macro2.Length);
    this.WriteN(((PROTOCOL_BASE_GET_OPTION_ACK) this).playerConfig_0.Macro2, ((PROTOCOL_BASE_GET_OPTION_ACK) this).playerConfig_0.Macro2.Length, "UTF-16LE");
    this.WriteH((ushort) ((PROTOCOL_BASE_GET_OPTION_ACK) this).playerConfig_0.Macro1.Length);
    this.WriteN(((PROTOCOL_BASE_GET_OPTION_ACK) this).playerConfig_0.Macro1, ((PROTOCOL_BASE_GET_OPTION_ACK) this).playerConfig_0.Macro1.Length, "UTF-16LE");
    this.WriteH((short) 49);
    this.WriteB(Bitwise.HexStringToByteArray("39 F8 10 00"));
    this.WriteB(((PROTOCOL_BASE_GET_OPTION_ACK) this).playerConfig_0.KeyboardKeys);
    this.WriteH((short) ((PROTOCOL_BASE_GET_OPTION_ACK) this).playerConfig_0.ShowBlood);
    this.WriteC((byte) ((PROTOCOL_BASE_GET_OPTION_ACK) this).playerConfig_0.Crosshair);
    this.WriteC((byte) ((PROTOCOL_BASE_GET_OPTION_ACK) this).playerConfig_0.HandPosition);
    this.WriteD(((PROTOCOL_BASE_GET_OPTION_ACK) this).playerConfig_0.Config);
    this.WriteD(((PROTOCOL_BASE_GET_OPTION_ACK) this).playerConfig_0.AudioEnable);
    this.WriteH((short) 0);
    this.WriteC((byte) ((PROTOCOL_BASE_GET_OPTION_ACK) this).playerConfig_0.AudioSFX);
    this.WriteC((byte) ((PROTOCOL_BASE_GET_OPTION_ACK) this).playerConfig_0.AudioBGM);
    this.WriteC((byte) ((PROTOCOL_BASE_GET_OPTION_ACK) this).playerConfig_0.PointOfView);
    this.WriteC((byte) 0);
    this.WriteC((byte) ((PROTOCOL_BASE_GET_OPTION_ACK) this).playerConfig_0.Sensitivity);
    this.WriteC((byte) ((PROTOCOL_BASE_GET_OPTION_ACK) this).playerConfig_0.InvertMouse);
    this.WriteH((short) 0);
    this.WriteC((byte) ((PROTOCOL_BASE_GET_OPTION_ACK) this).playerConfig_0.EnableInviteMsg);
    this.WriteC((byte) ((PROTOCOL_BASE_GET_OPTION_ACK) this).playerConfig_0.EnableWhisperMsg);
    this.WriteD(((PROTOCOL_BASE_GET_OPTION_ACK) this).playerConfig_0.Macro);
  }
}
