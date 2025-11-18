// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_ACK : GameServerPacket
{
  private readonly RoomModel roomModel_0;

  public virtual void Write()
  {
    this.WriteH((short) 2331);
    this.WriteD(((PROTOCOL_BASE_USER_ENTER_ACK) this).uint_0);
  }

  public PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_ACK([In] int obj0)
  {
    ((PROTOCOL_BASE_USER_LEAVE_ACK) this).int_0 = obj0;
  }
}
