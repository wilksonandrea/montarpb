// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BATTLE_STARTBATTLE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Models;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_STARTBATTLE_ACK : GameServerPacket
{
  private readonly RoomModel roomModel_0;
  private readonly SlotModel slotModel_0;
  private readonly bool bool_0;
  private readonly List<int> list_0;

  private byte[] method_0(RoomModel bool_2, [In] List<int> obj1)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      if (bool_2.IsDinoMode(""))
      {
        int Disposing1 = obj1.Count == 1 || bool_2.IsDinoMode("CC") ? (int) byte.MaxValue : bool_2.TRex;
        syncServerPacket.WriteC((byte) Disposing1);
        syncServerPacket.WriteC((byte) 10);
        for (int index = 0; index < obj1.Count; ++index)
        {
          int Disposing2 = obj1[index];
          if (Disposing2 != bool_2.TRex && bool_2.IsDinoMode("DE") || bool_2.IsDinoMode("CC"))
            syncServerPacket.WriteC((byte) Disposing2);
        }
        int num = 8 - obj1.Count - (Disposing1 == (int) byte.MaxValue ? 1 : 0);
        for (int index = 0; index < num; ++index)
          syncServerPacket.WriteC(byte.MaxValue);
        syncServerPacket.WriteC(byte.MaxValue);
      }
      else
        syncServerPacket.WriteB(new byte[10]);
      return syncServerPacket.ToArray();
    }
  }

  public PROTOCOL_BATTLE_STARTBATTLE_ACK(int roomModel_1)
  {
    ((PROTOCOL_BATTLE_RESPAWN_FOR_AI_ACK) this).int_0 = roomModel_1;
  }

  public virtual void Write()
  {
    this.WriteH((short) 5175);
    this.WriteD(((PROTOCOL_BATTLE_RESPAWN_FOR_AI_ACK) this).int_0);
  }

  public PROTOCOL_BATTLE_STARTBATTLE_ACK(byte[] roomModel_1)
  {
    ((PROTOCOL_BATTLE_SENDPING_ACK) this).byte_0 = roomModel_1;
  }
}
