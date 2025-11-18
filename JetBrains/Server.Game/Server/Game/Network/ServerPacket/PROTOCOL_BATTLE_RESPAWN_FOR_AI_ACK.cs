// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BATTLE_RESPAWN_FOR_AI_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_RESPAWN_FOR_AI_ACK : GameServerPacket
{
  private readonly int int_0;

  public PROTOCOL_BATTLE_RESPAWN_FOR_AI_ACK([In] RoomModel obj0)
  {
    ((PROTOCOL_BATTLE_RECORD_ACK) this).roomModel_0 = obj0;
  }

  public virtual void Write()
  {
    this.WriteH((short) 5163);
    this.WriteH((ushort) ((PROTOCOL_BATTLE_RECORD_ACK) this).roomModel_0.FRKills);
    this.WriteH((ushort) ((PROTOCOL_BATTLE_RECORD_ACK) this).roomModel_0.FRDeaths);
    this.WriteH((ushort) ((PROTOCOL_BATTLE_RECORD_ACK) this).roomModel_0.FRAssists);
    this.WriteH((ushort) ((PROTOCOL_BATTLE_RECORD_ACK) this).roomModel_0.CTKills);
    this.WriteH((ushort) ((PROTOCOL_BATTLE_RECORD_ACK) this).roomModel_0.CTDeaths);
    this.WriteH((ushort) ((PROTOCOL_BATTLE_RECORD_ACK) this).roomModel_0.CTAssists);
    foreach (SlotModel slot in ((PROTOCOL_BATTLE_RECORD_ACK) this).roomModel_0.Slots)
    {
      this.WriteH((ushort) slot.AllKills);
      this.WriteH((ushort) slot.AllDeaths);
      this.WriteH((ushort) slot.AllAssists);
    }
  }
}
