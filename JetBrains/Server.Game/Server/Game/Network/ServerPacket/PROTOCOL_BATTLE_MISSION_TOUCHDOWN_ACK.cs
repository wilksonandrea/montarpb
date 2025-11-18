// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BATTLE_MISSION_TOUCHDOWN_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_MISSION_TOUCHDOWN_ACK : GameServerPacket
{
  private readonly RoomModel roomModel_0;
  private readonly SlotModel slotModel_0;

  private byte[] method_0(RoomModel roomModel_1, List<int> teamEnum_0)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      if (roomModel_1.IsBotMode())
        syncServerPacket.WriteB(Bitwise.HexStringToByteArray("FF FF FF FF FF FF FF FF FF FF"));
      else if (roomModel_1.IsDinoMode(""))
      {
        int Disposing1 = teamEnum_0.Count == 1 || roomModel_1.IsDinoMode("CC") ? (int) byte.MaxValue : roomModel_1.TRex;
        syncServerPacket.WriteC((byte) Disposing1);
        syncServerPacket.WriteC((byte) 10);
        for (int index = 0; index < teamEnum_0.Count; ++index)
        {
          int Disposing2 = teamEnum_0[index];
          if (Disposing2 != roomModel_1.TRex && roomModel_1.IsDinoMode("DE") || roomModel_1.IsDinoMode("CC"))
            syncServerPacket.WriteC((byte) Disposing2);
        }
        int num = 8 - teamEnum_0.Count - (Disposing1 == (int) byte.MaxValue ? 1 : 0);
        for (int index = 0; index < num; ++index)
          syncServerPacket.WriteC(byte.MaxValue);
        syncServerPacket.WriteC(byte.MaxValue);
      }
      else
        syncServerPacket.WriteB(new byte[10]);
      return syncServerPacket.ToArray();
    }
  }

  private byte[] method_1([In] RoomModel obj0)
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
}
