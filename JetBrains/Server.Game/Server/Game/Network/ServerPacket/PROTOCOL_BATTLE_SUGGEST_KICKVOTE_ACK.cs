// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BATTLE_SUGGEST_KICKVOTE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_SUGGEST_KICKVOTE_ACK : GameServerPacket
{
  private readonly uint uint_0;

  private byte[] method_2([In] RoomModel obj0)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      syncServerPacket.WriteC((byte) obj0.GetReadyPlayers());
      foreach (SlotModel slot in obj0.Slots)
      {
        if (slot.State >= SlotState.READY && slot.Equipment != null)
        {
          Account playerBySlot = obj0.GetPlayerBySlot(slot);
          if (playerBySlot != null && playerBySlot.SlotId == slot.Id)
          {
            syncServerPacket.WriteC((byte) slot.Id);
            PlayerEquipment equipment = playerBySlot.Equipment;
            PlayerTitles title = playerBySlot.Title;
            int num = 0;
            if (equipment != null && title != null)
            {
              switch (obj0.ValidateTeam(slot.Team, slot.CostumeTeam))
              {
                case TeamEnum.FR_TEAM:
                  num = equipment.CharaRedId;
                  break;
                case TeamEnum.CT_TEAM:
                  num = equipment.CharaBlueId;
                  break;
              }
              syncServerPacket.WriteD(num);
              syncServerPacket.WriteD(equipment.WeaponPrimary);
              syncServerPacket.WriteD(equipment.WeaponSecondary);
              syncServerPacket.WriteD(equipment.WeaponMelee);
              syncServerPacket.WriteD(equipment.WeaponExplosive);
              syncServerPacket.WriteD(equipment.WeaponSpecial);
              syncServerPacket.WriteD(num);
              syncServerPacket.WriteD(equipment.PartHead);
              syncServerPacket.WriteD(equipment.PartFace);
              syncServerPacket.WriteD(equipment.PartJacket);
              syncServerPacket.WriteD(equipment.PartPocket);
              syncServerPacket.WriteD(equipment.PartGlove);
              syncServerPacket.WriteD(equipment.PartBelt);
              syncServerPacket.WriteD(equipment.PartHolster);
              syncServerPacket.WriteD(equipment.PartSkin);
              syncServerPacket.WriteD(equipment.BeretItem);
              syncServerPacket.WriteB(Bitwise.HexStringToByteArray("64 64 64 64 64"));
              syncServerPacket.WriteC((byte) title.Equiped1);
              syncServerPacket.WriteC((byte) title.Equiped2);
              syncServerPacket.WriteC((byte) title.Equiped3);
              syncServerPacket.WriteD(equipment.AccessoryId);
              syncServerPacket.WriteD(equipment.SprayId);
              syncServerPacket.WriteD(equipment.NameCardId);
            }
          }
        }
      }
      return syncServerPacket.ToArray();
    }
  }

  public PROTOCOL_BATTLE_SUGGEST_KICKVOTE_ACK(
    RoomModel countDownEnum_1,
    [In] SlotModel obj1,
    [In] PlayerTitles obj2)
  {
    ((PROTOCOL_BATTLE_START_GAME_TRANS_ACK) this).roomModel_0 = countDownEnum_1;
    ((PROTOCOL_BATTLE_START_GAME_TRANS_ACK) this).slotModel_0 = obj1;
    ((PROTOCOL_BATTLE_START_GAME_TRANS_ACK) this).playerTitles_0 = obj2;
    if (countDownEnum_1 == null || obj1 == null || obj2 == null)
      return;
    ((PROTOCOL_BATTLE_START_GAME_TRANS_ACK) this).playerEquipment_0 = obj1.Equipment;
    if (((PROTOCOL_BATTLE_START_GAME_TRANS_ACK) this).playerEquipment_0 == null)
      return;
    switch (countDownEnum_1.ValidateTeam(obj1.Team, obj1.CostumeTeam))
    {
      case TeamEnum.FR_TEAM:
        ((PROTOCOL_BATTLE_START_GAME_TRANS_ACK) this).int_0 = ((PROTOCOL_BATTLE_START_GAME_TRANS_ACK) this).playerEquipment_0.CharaRedId;
        break;
      case TeamEnum.CT_TEAM:
        ((PROTOCOL_BATTLE_START_GAME_TRANS_ACK) this).int_0 = ((PROTOCOL_BATTLE_START_GAME_TRANS_ACK) this).playerEquipment_0.CharaBlueId;
        break;
    }
  }
}
