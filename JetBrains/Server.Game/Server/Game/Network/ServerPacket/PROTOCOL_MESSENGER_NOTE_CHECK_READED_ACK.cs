// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_MESSENGER_NOTE_CHECK_READED_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_MESSENGER_NOTE_CHECK_READED_ACK : GameServerPacket
{
  private readonly List<int> list_0;

  private byte[] method_1([In] Account obj0)
  {
    if (obj0?.Status == null)
      return new byte[4]
      {
        (byte) 3,
        (byte) 0,
        (byte) 0,
        (byte) 0
      };
    FriendState Disposing1;
    int Disposing2;
    int Disposing3;
    int Disposing4;
    ComDiv.GetPlayerLocation(obj0.Status, obj0.IsOnline, ref Disposing1, ref Disposing2, ref Disposing3, ref Disposing4);
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      syncServerPacket.WriteC((byte) Disposing1);
      syncServerPacket.WriteC((byte) Disposing4);
      syncServerPacket.WriteC((byte) Disposing3);
      syncServerPacket.WriteC((byte) Disposing2);
      return syncServerPacket.ToArray();
    }
  }

  public PROTOCOL_MESSENGER_NOTE_CHECK_READED_ACK(bool uint_1)
  {
    ((PROTOCOL_MATCH_CLAN_SEASON_ACK) this).bool_0 = uint_1;
  }
}
