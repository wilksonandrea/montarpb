// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BATTLE_LEAVEP2PSERVER_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_LEAVEP2PSERVER_ACK : GameServerPacket
{
  private readonly RoomModel roomModel_0;

  private byte[] method_3([In] Account obj0)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      PlayerEvent playerEvent = obj0.Event;
      if (playerEvent != null)
      {
        syncServerPacket.WriteC((byte) playerEvent.LastPlaytimeFinish);
        syncServerPacket.WriteD((uint) playerEvent.LastPlaytimeValue);
      }
      else
        syncServerPacket.WriteB(new byte[5]);
      return syncServerPacket.ToArray();
    }
  }

  private byte[] method_4(Account roomModel_1)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      SlotModel slot = ((PROTOCOL_BATTLE_ENDBATTLE_ACK) this).roomModel_0.GetSlot(roomModel_1.SlotId);
      if (slot != null)
      {
        syncServerPacket.WriteB(new byte[44]);
        syncServerPacket.WriteD(0);
        syncServerPacket.WriteB(new byte[16 /*0x10*/]);
        syncServerPacket.WriteH((ushort) slot.SeasonPoint);
        syncServerPacket.WriteH((ushort) slot.BonusBattlePass);
        syncServerPacket.WriteC((byte) 0);
        syncServerPacket.WriteB(new byte[20]);
        syncServerPacket.WriteD(0);
        syncServerPacket.WriteH((ushort) (600 + roomModel_1.InventoryPlus + 8));
      }
      return syncServerPacket.ToArray();
    }
  }
}
