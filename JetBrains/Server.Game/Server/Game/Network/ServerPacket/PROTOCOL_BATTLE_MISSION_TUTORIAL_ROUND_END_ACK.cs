// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BATTLE_MISSION_TUTORIAL_ROUND_END_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_MISSION_TUTORIAL_ROUND_END_ACK : GameServerPacket
{
  private readonly RoomModel roomModel_0;

  private byte[] method_0([In] RoomModel obj0)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      foreach (SlotModel slot in obj0.Slots)
      {
        PlayerEquipment equipment = slot.Equipment;
        if (equipment != null)
        {
          if (slot.Team == TeamEnum.FR_TEAM)
            syncServerPacket.WriteD(!obj0.SwapRound ? equipment.CharaRedId : equipment.CharaBlueId);
          else if (slot.Team == TeamEnum.CT_TEAM)
            syncServerPacket.WriteD(!obj0.SwapRound ? equipment.CharaBlueId : equipment.CharaRedId);
        }
        else if (slot.Team == TeamEnum.FR_TEAM)
          syncServerPacket.WriteD(!obj0.SwapRound ? 601001 : 602002);
        else if (slot.Team == TeamEnum.CT_TEAM)
          syncServerPacket.WriteD(!obj0.SwapRound ? 602002 : 601001);
      }
      return syncServerPacket.ToArray();
    }
  }

  public PROTOCOL_BATTLE_MISSION_TUTORIAL_ROUND_END_ACK(RoomModel roomModel_1, SlotModel list_1)
  {
    ((PROTOCOL_BATTLE_MISSION_TOUCHDOWN_ACK) this).roomModel_0 = roomModel_1;
    ((PROTOCOL_BATTLE_MISSION_TOUCHDOWN_ACK) this).slotModel_0 = list_1;
  }
}
