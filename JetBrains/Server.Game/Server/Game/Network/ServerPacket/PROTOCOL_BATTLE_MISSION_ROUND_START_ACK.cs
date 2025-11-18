// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BATTLE_MISSION_ROUND_START_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_MISSION_ROUND_START_ACK : GameServerPacket
{
  private readonly RoomModel roomModel_0;

  public virtual void Write()
  {
    this.WriteH((short) 5155);
    this.WriteC((byte) ((PROTOCOL_BATTLE_MISSION_ROUND_END_ACK) this).int_0);
    this.WriteC((byte) ((PROTOCOL_BATTLE_MISSION_ROUND_END_ACK) this).roundEndType_0);
    if (((PROTOCOL_BATTLE_MISSION_ROUND_END_ACK) this).roomModel_0.IsDinoMode("DE"))
    {
      this.WriteH((ushort) ((PROTOCOL_BATTLE_MISSION_ROUND_END_ACK) this).roomModel_0.FRDino);
      this.WriteH((ushort) ((PROTOCOL_BATTLE_MISSION_ROUND_END_ACK) this).roomModel_0.CTDino);
    }
    else if (((PROTOCOL_BATTLE_MISSION_ROUND_END_ACK) this).roomModel_0.RoomType != RoomCondition.DeathMatch && ((PROTOCOL_BATTLE_MISSION_ROUND_END_ACK) this).roomModel_0.RoomType != RoomCondition.FreeForAll)
    {
      this.WriteH((ushort) ((PROTOCOL_BATTLE_MISSION_ROUND_END_ACK) this).roomModel_0.FRRounds);
      this.WriteH((ushort) ((PROTOCOL_BATTLE_MISSION_ROUND_END_ACK) this).roomModel_0.CTRounds);
    }
    else
    {
      this.WriteH((ushort) ((PROTOCOL_BATTLE_MISSION_ROUND_END_ACK) this).roomModel_0.FRKills);
      this.WriteH((ushort) ((PROTOCOL_BATTLE_MISSION_ROUND_END_ACK) this).roomModel_0.CTKills);
    }
  }

  public PROTOCOL_BATTLE_MISSION_ROUND_START_ACK([In] RoomModel obj0, List<int> int_1)
  {
    ((PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK) this).roomModel_0 = obj0;
    ((PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK) this).list_0 = int_1;
  }

  public virtual void Write()
  {
    this.WriteH((short) 5151);
    this.WriteD(AllUtils.GetSlotsFlag(((PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK) this).roomModel_0, false, false));
    this.WriteB(((PROTOCOL_BATTLE_MISSION_TOUCHDOWN_ACK) this).method_0(((PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK) this).roomModel_0, ((PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK) this).list_0));
    this.WriteC(((PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK) this).roomModel_0.SwapRound ? (byte) 3 : (byte) 0);
    if (!((PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK) this).roomModel_0.SwapRound)
      return;
    this.WriteB(((PROTOCOL_BATTLE_MISSION_TOUCHDOWN_ACK) this).method_1(((PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK) this).roomModel_0));
  }
}
