// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BATTLE_MISSION_GENERATOR_INFO_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_MISSION_GENERATOR_INFO_ACK : GameServerPacket
{
  private readonly RoomModel roomModel_0;

  public virtual void Write()
  {
    this.WriteH((short) 5157);
    this.WriteD(((PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK) this).int_0);
    this.WriteC(((PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK) this).byte_0);
    this.WriteH(((PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK) this).ushort_0);
    this.WriteT(((PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK) this).float_0);
    this.WriteT(((PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK) this).float_1);
    this.WriteT(((PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK) this).float_2);
  }

  public PROTOCOL_BATTLE_MISSION_GENERATOR_INFO_ACK([In] int obj0)
  {
    ((PROTOCOL_BATTLE_MISSION_BOMB_UNINSTALL_ACK) this).int_0 = obj0;
  }
}
