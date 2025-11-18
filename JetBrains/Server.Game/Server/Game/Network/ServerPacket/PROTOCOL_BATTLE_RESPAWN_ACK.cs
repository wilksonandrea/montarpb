// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BATTLE_RESPAWN_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_RESPAWN_ACK : GameServerPacket
{
  private readonly RoomModel roomModel_0;
  private readonly SlotModel slotModel_0;
  private readonly PlayerEquipment playerEquipment_0;
  private readonly List<int> list_0;
  private readonly int int_0;

  private byte[] method_0(bool int_1)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      if (int_1)
      {
        string str = "02 14 03 15 04 16 05 17 06 18 07 19 08 1A 09 1B0A 1C 0B 1D 0C 1E 0D 1F 0E 20 0F 21 10 22 11 0012 01 13";
        syncServerPacket.WriteB(Bitwise.HexStringToByteArray(str));
      }
      return syncServerPacket.ToArray();
    }
  }

  public PROTOCOL_BATTLE_RESPAWN_ACK(uint uint_1)
  {
    ((PROTOCOL_BATTLE_READYBATTLE_ACK) this).uint_0 = uint_1;
  }

  public virtual void Write()
  {
    this.WriteH((short) 5124);
    this.WriteC((byte) 0);
    this.WriteH((short) 0);
    this.WriteD(((PROTOCOL_BATTLE_READYBATTLE_ACK) this).uint_0);
  }
}
