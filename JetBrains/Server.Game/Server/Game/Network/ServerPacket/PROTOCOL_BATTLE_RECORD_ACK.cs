// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BATTLE_RECORD_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Utility;
using Server.Game.Data.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_RECORD_ACK : GameServerPacket
{
  private readonly RoomModel roomModel_0;

  public virtual void Write()
  {
    this.WriteH((short) 5130);
    this.WriteD(((PROTOCOL_BATTLE_PRESTARTBATTLE_ACK) this).bool_0 ? 1 : 0);
    if (!((PROTOCOL_BATTLE_PRESTARTBATTLE_ACK) this).bool_0)
      return;
    this.WriteD(((PROTOCOL_BATTLE_PRESTARTBATTLE_ACK) this).account_0.SlotId);
    this.WriteC(((PROTOCOL_BATTLE_PRESTARTBATTLE_ACK) this).roomModel_0.RoomType == RoomCondition.Tutorial ? (byte) 1 : (byte) ConfigLoader.UdpType);
    this.WriteB(ComDiv.AddressBytes(ConfigLoader.HOST[1]));
    this.WriteB(ComDiv.AddressBytes(ConfigLoader.HOST[1]));
    this.WriteH((ushort) ConfigLoader.DEFAULT_PORT[2]);
    this.WriteD(((PROTOCOL_BATTLE_PRESTARTBATTLE_ACK) this).uint_0);
    this.WriteD(((PROTOCOL_BATTLE_PRESTARTBATTLE_ACK) this).uint_1);
    this.WriteB(((PROTOCOL_BATTLE_RESPAWN_ACK) this).method_0(((PROTOCOL_BATTLE_PRESTARTBATTLE_ACK) this).bool_1));
  }
}
