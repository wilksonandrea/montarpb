// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BATTLE_READYBATTLE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_READYBATTLE_ACK : GameServerPacket
{
  private readonly uint uint_0;

  public virtual void Write()
  {
    this.WriteH((short) 3403);
    this.WriteC((byte) ((PROTOCOL_BATTLE_NOTIFY_KICKVOTE_RESULT_ACK) this).voteKickModel_0.VictimIdx);
    this.WriteC((byte) ((PROTOCOL_BATTLE_NOTIFY_KICKVOTE_RESULT_ACK) this).voteKickModel_0.Accept);
    this.WriteC((byte) ((PROTOCOL_BATTLE_NOTIFY_KICKVOTE_RESULT_ACK) this).voteKickModel_0.Denie);
    this.WriteD(((PROTOCOL_BATTLE_NOTIFY_KICKVOTE_RESULT_ACK) this).uint_0);
  }

  public PROTOCOL_BATTLE_READYBATTLE_ACK(Account roomModel_1, [In] bool obj1)
  {
    ((PROTOCOL_BATTLE_PRESTARTBATTLE_ACK) this).account_0 = roomModel_1;
    ((PROTOCOL_BATTLE_PRESTARTBATTLE_ACK) this).bool_1 = obj1;
    ((PROTOCOL_BATTLE_PRESTARTBATTLE_ACK) this).roomModel_0 = roomModel_1.Room;
    if (((PROTOCOL_BATTLE_PRESTARTBATTLE_ACK) this).roomModel_0 == null)
      return;
    ((PROTOCOL_BATTLE_PRESTARTBATTLE_ACK) this).bool_0 = ((PROTOCOL_BATTLE_PRESTARTBATTLE_ACK) this).roomModel_0.IsPreparing();
    ((PROTOCOL_BATTLE_PRESTARTBATTLE_ACK) this).uint_0 = ((PROTOCOL_BATTLE_PRESTARTBATTLE_ACK) this).roomModel_0.UniqueRoomId;
    ((PROTOCOL_BATTLE_PRESTARTBATTLE_ACK) this).uint_1 = ((PROTOCOL_BATTLE_PRESTARTBATTLE_ACK) this).roomModel_0.Seed;
  }
}
