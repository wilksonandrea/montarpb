// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_ACK : GameServerPacket
{
  private readonly uint uint_0;
  private readonly BattlePassModel battlePassModel_0;
  private readonly PlayerBattlepass playerBattlepass_0;
  private readonly (int, int, int, int) valueTuple_0;

  public virtual void Write()
  {
    this.WriteH((short) 3683);
    this.WriteC((byte) 0);
    this.WriteC((byte) 0);
    this.WriteC((byte) 0);
    this.WriteC((byte) 0);
    this.WriteC((byte) 0);
    this.WriteC((byte) 0);
  }

  public PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_ACK(List<SlotChange> int_0, [In] int obj1, [In] int obj2)
  {
    ((PROTOCOL_ROOM_TEAM_BALANCE_ACK) this).list_0 = int_0;
    ((PROTOCOL_ROOM_TEAM_BALANCE_ACK) this).int_1 = obj1;
    ((PROTOCOL_ROOM_TEAM_BALANCE_ACK) this).int_0 = obj2;
  }
}
