// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BATTLE_GIVEUPBATTLE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_GIVEUPBATTLE_ACK : GameServerPacket
{
  private readonly Account account_0;
  private readonly int int_0;

  private byte[] method_1([In] RoomModel obj0)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      syncServerPacket.WriteH(obj0.ThisModeHaveRounds() ? (ushort) obj0.FRRounds : (ushort) 0);
      syncServerPacket.WriteH(obj0.ThisModeHaveRounds() ? (ushort) obj0.CTRounds : (ushort) 0);
      return syncServerPacket.ToArray();
    }
  }

  private byte[] method_2([In] RoomModel obj0)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      foreach (SlotModel slot in obj0.Slots)
      {
        Account account;
        if (obj0.GetPlayerBySlot(slot, ref account))
        {
          syncServerPacket.WriteC((byte) account.Rank);
        }
        else
        {
          // ISSUE: reference to a compiler-generated method
          syncServerPacket.WriteC((byte) AllUtils.Class5.InitBotRank(obj0.IsStartingMatch() ? (int) obj0.IngameAiLevel : (int) obj0.AiLevel));
        }
        syncServerPacket.WriteH((short) 0);
        syncServerPacket.WriteD(1);
      }
      return syncServerPacket.ToArray();
    }
  }
}
