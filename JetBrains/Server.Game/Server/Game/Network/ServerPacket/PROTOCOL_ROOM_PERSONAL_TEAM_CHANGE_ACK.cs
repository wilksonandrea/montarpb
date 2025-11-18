// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_ROOM_PERSONAL_TEAM_CHANGE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_PERSONAL_TEAM_CHANGE_ACK : GameServerPacket
{
  public virtual void Write()
  {
    this.WriteH((short) 3586);
    this.WriteH((short) 0);
    this.WriteD(((PROTOCOL_ROOM_JOIN_ACK) this).uint_0);
    if (((PROTOCOL_ROOM_JOIN_ACK) this).uint_0 != 0U)
      return;
    lock (((PROTOCOL_ROOM_JOIN_ACK) this).roomModel_0.Slots)
    {
      this.WriteB(this.method_0(((PROTOCOL_ROOM_JOIN_ACK) this).roomModel_0));
      this.WriteB(((PROTOCOL_ROOM_REQUEST_MAIN_ACK) this).method_1(((PROTOCOL_ROOM_JOIN_ACK) this).roomModel_0));
      this.WriteC(((PROTOCOL_ROOM_JOIN_ACK) this).roomModel_0.AiType);
      this.WriteC(((PROTOCOL_ROOM_JOIN_ACK) this).roomModel_0.IsStartingMatch() ? ((PROTOCOL_ROOM_JOIN_ACK) this).roomModel_0.IngameAiLevel : ((PROTOCOL_ROOM_JOIN_ACK) this).roomModel_0.AiLevel);
      this.WriteC(((PROTOCOL_ROOM_JOIN_ACK) this).roomModel_0.AiCount);
      this.WriteC((byte) ((PROTOCOL_ROOM_JOIN_ACK) this).roomModel_0.GetAllPlayers().Count);
      this.WriteC((byte) ((PROTOCOL_ROOM_JOIN_ACK) this).roomModel_0.LeaderSlot);
      this.WriteC((byte) ((PROTOCOL_ROOM_JOIN_ACK) this).roomModel_0.CountdownTime.GetTimeLeft());
      this.WriteC((byte) ((PROTOCOL_ROOM_JOIN_ACK) this).roomModel_0.Password.Length);
      this.WriteS(((PROTOCOL_ROOM_JOIN_ACK) this).roomModel_0.Password, ((PROTOCOL_ROOM_JOIN_ACK) this).roomModel_0.Password.Length);
      this.WriteB(new byte[17]);
      this.WriteU(((PROTOCOL_ROOM_JOIN_ACK) this).roomModel_0.LeaderName, 66);
      this.WriteD(((PROTOCOL_ROOM_JOIN_ACK) this).roomModel_0.KillTime);
      this.WriteC(((PROTOCOL_ROOM_JOIN_ACK) this).roomModel_0.Limit);
      this.WriteC(((PROTOCOL_ROOM_JOIN_ACK) this).roomModel_0.WatchRuleFlag);
      this.WriteH((ushort) ((PROTOCOL_ROOM_JOIN_ACK) this).roomModel_0.BalanceType);
      this.WriteB(((PROTOCOL_ROOM_JOIN_ACK) this).roomModel_0.RandomMaps);
      this.WriteC(((PROTOCOL_ROOM_JOIN_ACK) this).roomModel_0.CountdownIG);
      this.WriteB(((PROTOCOL_ROOM_JOIN_ACK) this).roomModel_0.LeaderAddr);
      this.WriteC(((PROTOCOL_ROOM_JOIN_ACK) this).roomModel_0.KillCam);
      this.WriteH((short) 0);
      this.WriteD(((PROTOCOL_ROOM_JOIN_ACK) this).roomModel_0.RoomId);
      this.WriteU(((PROTOCOL_ROOM_JOIN_ACK) this).roomModel_0.Name, 46);
      this.WriteC((byte) ((PROTOCOL_ROOM_JOIN_ACK) this).roomModel_0.MapId);
      this.WriteC((byte) ((PROTOCOL_ROOM_JOIN_ACK) this).roomModel_0.Rule);
      this.WriteC((byte) ((PROTOCOL_ROOM_JOIN_ACK) this).roomModel_0.Stage);
      this.WriteC((byte) ((PROTOCOL_ROOM_JOIN_ACK) this).roomModel_0.RoomType);
      this.WriteC((byte) ((PROTOCOL_ROOM_JOIN_ACK) this).roomModel_0.State);
      this.WriteC((byte) ((PROTOCOL_ROOM_JOIN_ACK) this).roomModel_0.GetCountPlayers());
      this.WriteC((byte) ((PROTOCOL_ROOM_JOIN_ACK) this).roomModel_0.GetSlotCount());
      this.WriteC((byte) ((PROTOCOL_ROOM_JOIN_ACK) this).roomModel_0.Ping);
      this.WriteH((ushort) ((PROTOCOL_ROOM_JOIN_ACK) this).roomModel_0.WeaponsFlag);
      this.WriteD(((PROTOCOL_ROOM_JOIN_ACK) this).roomModel_0.GetFlag());
      this.WriteH((short) 0);
      this.WriteB(new byte[4]);
      this.WriteC((byte) 0);
      this.WriteC((byte) ((PROTOCOL_ROOM_JOIN_ACK) this).int_0);
    }
  }

  private byte[] method_0([In] RoomModel obj0)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      syncServerPacket.WriteC((byte) obj0.Slots.Length);
      foreach (SlotModel slot in obj0.Slots)
        syncServerPacket.WriteC((byte) slot.Team);
      return syncServerPacket.ToArray();
    }
  }
}
