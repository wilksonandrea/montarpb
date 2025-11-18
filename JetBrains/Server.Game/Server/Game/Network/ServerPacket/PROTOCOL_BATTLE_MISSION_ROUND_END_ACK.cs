// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BATTLE_MISSION_ROUND_END_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_MISSION_ROUND_END_ACK : GameServerPacket
{
  private readonly RoomModel roomModel_0;
  private readonly int int_0;
  private readonly RoundEndType roundEndType_0;

  public virtual void Write()
  {
    this.WriteH((short) 5159);
    this.WriteD(((PROTOCOL_BATTLE_MISSION_BOMB_UNINSTALL_ACK) this).int_0);
  }

  public PROTOCOL_BATTLE_MISSION_ROUND_END_ACK([In] RoomModel obj0)
  {
    ((PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_ACK) this).roomModel_0 = obj0;
  }

  public virtual void Write()
  {
    this.WriteH((short) 5181);
    this.WriteH((ushort) ((PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_ACK) this).roomModel_0.Bar1);
    this.WriteH((ushort) ((PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_ACK) this).roomModel_0.Bar2);
    for (int index = 0; index < 18; ++index)
      this.WriteH(((PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_ACK) this).roomModel_0.Slots[index].DamageBar1);
    for (int index = 0; index < 18; ++index)
      this.WriteH(((PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_ACK) this).roomModel_0.Slots[index].DamageBar2);
  }
}
