// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Network;
using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK : GameServerPacket
{
  private readonly int int_0;
  private readonly float float_0;
  private readonly float float_1;
  private readonly float float_2;
  private readonly byte byte_0;
  private readonly ushort ushort_0;

  private byte[] method_5([In] RoomModel obj0)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      foreach (byte Disposing in obj0.SlotRewards.Item1)
        syncServerPacket.WriteC(Disposing);
      foreach (int num in obj0.SlotRewards.Item2)
        syncServerPacket.WriteD(num);
      return syncServerPacket.ToArray();
    }
  }

  public PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK(Account roomModel_1, [In] int obj1)
  {
    ((PROTOCOL_BATTLE_GIVEUPBATTLE_ACK) this).account_0 = roomModel_1;
    ((PROTOCOL_BATTLE_GIVEUPBATTLE_ACK) this).int_0 = obj1;
  }
}
