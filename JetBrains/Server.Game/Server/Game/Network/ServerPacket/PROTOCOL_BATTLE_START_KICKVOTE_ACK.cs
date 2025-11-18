// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BATTLE_START_KICKVOTE_ACK
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

public class PROTOCOL_BATTLE_START_KICKVOTE_ACK : GameServerPacket
{
  private readonly VoteKickModel voteKickModel_0;

  private byte[] method_0([In] RoomModel obj0)
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
            syncServerPacket.WriteD((uint) playerBySlot.PlayerId);
        }
      }
      return syncServerPacket.ToArray();
    }
  }

  private byte[] method_1(RoomModel roomModel_1)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      syncServerPacket.WriteC((byte) roomModel_1.Slots.Length);
      foreach (SlotModel slot in roomModel_1.Slots)
        syncServerPacket.WriteC((byte) slot.Team);
      return syncServerPacket.ToArray();
    }
  }
}
