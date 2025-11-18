// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BATTLE_NOTIFY_CURRENT_KICKVOTE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_NOTIFY_CURRENT_KICKVOTE_ACK : GameServerPacket
{
  private readonly VoteKickModel voteKickModel_0;

  private byte[] method_0(RoomModel roomModel_1)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      if (roomModel_1.IsDinoMode("DE"))
      {
        syncServerPacket.WriteD(roomModel_1.FRDino);
        syncServerPacket.WriteD(roomModel_1.CTDino);
      }
      else if (roomModel_1.RoomType == RoomCondition.DeathMatch && !roomModel_1.IsBotMode())
      {
        syncServerPacket.WriteD(roomModel_1.FRKills);
        syncServerPacket.WriteD(roomModel_1.CTKills);
      }
      else if (roomModel_1.RoomType == RoomCondition.FreeForAll)
      {
        syncServerPacket.WriteD(((PROTOCOL_BATTLE_NOTIFY_HACK_USER_ACK) this).GetSlotKill());
        syncServerPacket.WriteD(0);
      }
      else if (roomModel_1.IsBotMode())
      {
        syncServerPacket.WriteD((int) roomModel_1.IngameAiLevel);
        syncServerPacket.WriteD(0);
      }
      else
      {
        syncServerPacket.WriteD(roomModel_1.FRRounds);
        syncServerPacket.WriteD(roomModel_1.CTRounds);
      }
      return syncServerPacket.ToArray();
    }
  }

  private int method_1()
  {
    if (((PROTOCOL_BATTLE_NEW_JOIN_ROOM_SCORE_ACK) this).roomModel_0.IsBotMode())
      return 3;
    if (((PROTOCOL_BATTLE_NEW_JOIN_ROOM_SCORE_ACK) this).roomModel_0.RoomType == RoomCondition.DeathMatch && !((PROTOCOL_BATTLE_NEW_JOIN_ROOM_SCORE_ACK) this).roomModel_0.IsBotMode())
      return 1;
    if (((PROTOCOL_BATTLE_NEW_JOIN_ROOM_SCORE_ACK) this).roomModel_0.IsDinoMode(""))
      return 4;
    return ((PROTOCOL_BATTLE_NEW_JOIN_ROOM_SCORE_ACK) this).roomModel_0.RoomType == RoomCondition.FreeForAll ? 5 : 2;
  }
}
