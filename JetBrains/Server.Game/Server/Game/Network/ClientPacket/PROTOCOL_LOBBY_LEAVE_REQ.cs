// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_LOBBY_LEAVE_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Network;
using Server.Game.Data.Models;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_LOBBY_LEAVE_REQ : GameClientPacket
{
  private uint uint_0;

  private byte[] method_0(int clanModel_0, ref int account_0, [In] List<RoomModel> obj2)
  {
    int num = clanModel_0 == 0 ? 10 : 11;
    using (SyncServerPacket uint_2 = new SyncServerPacket())
    {
      for (int index = clanModel_0 * num; index < obj2.Count; ++index)
      {
        this.method_1(obj2[index], uint_2);
        if (++account_0 == 10)
          break;
      }
      return uint_2.ToArray();
    }
  }

  private void method_1([In] RoomModel obj0, SyncServerPacket uint_2)
  {
    uint_2.WriteD(obj0.RoomId);
    uint_2.WriteU(obj0.Name, 46);
    uint_2.WriteC((byte) obj0.MapId);
    uint_2.WriteC((byte) obj0.Rule);
    uint_2.WriteC((byte) obj0.Stage);
    uint_2.WriteC((byte) obj0.RoomType);
    uint_2.WriteC((byte) obj0.State);
    uint_2.WriteC((byte) obj0.GetCountPlayers());
    uint_2.WriteC((byte) obj0.GetSlotCount());
    uint_2.WriteC((byte) obj0.Ping);
    uint_2.WriteH((ushort) obj0.WeaponsFlag);
    uint_2.WriteD(obj0.GetFlag());
    uint_2.WriteH((short) 0);
    uint_2.WriteB(obj0.LeaderAddr);
    uint_2.WriteC((byte) 0);
  }

  private byte[] method_2(int int_0, ref int int_1, List<Account> list_0)
  {
    int num = int_0 == 0 ? 8 : 9;
    using (SyncServerPacket syncServerPacket_0 = new SyncServerPacket())
    {
      for (int index = int_0 * num; index < list_0.Count; ++index)
      {
        ((PROTOCOL_LOBBY_NEW_MYINFO_REQ) this).method_3(list_0[index], syncServerPacket_0);
        if (++int_1 == 8)
          break;
      }
      return syncServerPacket_0.ToArray();
    }
  }
}
