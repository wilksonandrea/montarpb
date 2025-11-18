// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BATTLE_PRESTARTBATTLE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_PRESTARTBATTLE_ACK : GameServerPacket
{
  private readonly Account account_0;
  private readonly RoomModel roomModel_0;
  private readonly bool bool_0;
  private readonly bool bool_1;
  private readonly uint uint_0;
  private readonly uint uint_1;

  public virtual void Write()
  {
    this.WriteH((short) 3413);
    this.WriteC((byte) ((PROTOCOL_BATTLE_NOTIFY_HACK_USER_ACK) this).int_0);
    this.WriteC((byte) 1);
    this.WriteD(1);
  }

  public PROTOCOL_BATTLE_PRESTARTBATTLE_ACK()
  {
  }

  public virtual void Write() => this.WriteH((short) 3405);

  public PROTOCOL_BATTLE_PRESTARTBATTLE_ACK(uint roomModel_1, [In] VoteKickModel obj1)
  {
    ((PROTOCOL_BATTLE_NOTIFY_KICKVOTE_RESULT_ACK) this).uint_0 = roomModel_1;
    ((PROTOCOL_BATTLE_NOTIFY_KICKVOTE_RESULT_ACK) this).voteKickModel_0 = obj1;
  }
}
