// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BATTLE_DEATH_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_DEATH_ACK : GameServerPacket
{
  private readonly RoomModel roomModel_0;
  private readonly FragInfos fragInfos_0;
  private readonly SlotModel slotModel_0;

  public virtual void Write()
  {
    this.WriteH((short) 7426);
    this.WriteD(((PROTOCOL_BATTLEBOX_GET_LIST_ACK) this).int_0);
    this.WriteD(((PROTOCOL_BATTLEBOX_GET_LIST_ACK) this).shopData_0.ItemsCount);
    this.WriteD(((PROTOCOL_BATTLEBOX_GET_LIST_ACK) this).shopData_0.Offset);
    this.WriteB(((PROTOCOL_BATTLEBOX_GET_LIST_ACK) this).shopData_0.Buffer);
    this.WriteD(585);
  }

  public PROTOCOL_BATTLE_DEATH_ACK([In] RoomModel obj0)
  {
    ((PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_ACK) this).roomModel_0 = obj0;
  }
}
