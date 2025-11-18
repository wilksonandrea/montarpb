// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CHAR_CHANGE_EQUIP_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Utility;
using Server.Game.Network.ServerPacket;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CHAR_CHANGE_EQUIP_REQ : GameClientPacket
{
  private int int_0;
  private int int_1;
  private bool bool_0;
  private bool bool_1;
  private bool bool_2;
  private bool bool_3;
  private bool bool_4;
  private readonly int[] int_2;
  private readonly int[] int_3;
  private readonly SortedList<int, int> sortedList_0;
  private readonly SortedList<int, int> sortedList_1;
  private readonly SortedList<int, int> sortedList_2;

  internal void method_0([In] object obj0)
  {
    // ISSUE: reference to a compiler-generated field
    if (((PROTOCOL_BATTLE_TIMERSYNC_REQ.Class4) this).roomModel_0.State == RoomState.BATTLE)
    {
      // ISSUE: reference to a compiler-generated field
      ((PROTOCOL_BATTLE_TIMERSYNC_REQ.Class4) this).roomModel_0.BattleStart = DateTimeUtil.Now().AddSeconds(5.0);
      // ISSUE: reference to a compiler-generated field
      using (PROTOCOL_BATTLE_MISSION_ROUND_START_ACK missionRoundStartAck = (PROTOCOL_BATTLE_MISSION_ROUND_START_ACK) new PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_ACK(((PROTOCOL_BATTLE_TIMERSYNC_REQ.Class4) this).roomModel_0))
      {
        // ISSUE: reference to a compiler-generated field
        ((PROTOCOL_BATTLE_TIMERSYNC_REQ.Class4) this).roomModel_0.SendPacketToPlayers((GameServerPacket) missionRoundStartAck, SlotState.BATTLE, 0);
      }
    }
    lock (obj0)
    {
      // ISSUE: reference to a compiler-generated field
      ((PROTOCOL_BATTLE_TIMERSYNC_REQ.Class4) this).roomModel_0.RoundTime.StopJob();
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_BATTLE_USER_SOPETYPE_REQ) this).int_0 = (int) this.ReadC();
  }
}
