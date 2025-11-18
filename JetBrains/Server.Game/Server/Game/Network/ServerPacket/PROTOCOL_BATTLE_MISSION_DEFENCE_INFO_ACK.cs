// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_ACK : GameServerPacket
{
  private readonly RoomModel roomModel_0;

  public virtual void Write()
  {
    this.WriteH((short) 5149);
    this.WriteD(((PROTOCOL_BATTLE_LEAVEP2PSERVER_ACK) this).roomModel_0.LeaderSlot);
  }

  public PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_ACK(
    int account_1,
    byte int_1,
    [In] ushort obj2,
    [In] float obj3,
    [In] float obj4,
    [In] float obj5)
  {
    ((PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK) this).byte_0 = int_1;
    ((PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK) this).int_0 = account_1;
    ((PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK) this).ushort_0 = obj2;
    ((PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK) this).float_0 = obj3;
    ((PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK) this).float_1 = obj4;
    ((PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK) this).float_2 = obj5;
  }
}
