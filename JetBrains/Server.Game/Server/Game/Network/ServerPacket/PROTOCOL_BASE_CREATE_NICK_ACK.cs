// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BASE_CREATE_NICK_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_CREATE_NICK_ACK : GameServerPacket
{
  private readonly uint uint_0;
  private readonly Account account_0;
  private readonly PlayerInventory playerInventory_0;
  private readonly PlayerEquipment playerEquipment_0;

  public virtual void Write()
  {
    this.WriteH((short) 2337);
    this.WriteD((uint) ((PROTOCOL_BASE_ATTENDANCE_ACK) this).eventErrorEnum_0);
    if (((PROTOCOL_BASE_ATTENDANCE_ACK) this).eventErrorEnum_0 != EventErrorEnum.VISIT_EVENT_SUCCESS)
      return;
    this.WriteD(((PROTOCOL_BASE_ATTENDANCE_ACK) this).eventVisitModel_0.Id);
    this.WriteC((byte) ((PROTOCOL_BASE_ATTENDANCE_ACK) this).playerEvent_0.LastVisitCheckDay);
    this.WriteC((byte) (((PROTOCOL_BASE_ATTENDANCE_ACK) this).playerEvent_0.LastVisitCheckDay - 1));
    this.WriteC(((PROTOCOL_BASE_ATTENDANCE_ACK) this).uint_1 < ((PROTOCOL_BASE_ATTENDANCE_ACK) this).uint_0 ? (byte) 1 : (byte) 2);
    this.WriteC((byte) ((PROTOCOL_BASE_ATTENDANCE_ACK) this).playerEvent_0.LastVisitSeqType);
    this.WriteC((byte) 1);
  }

  public PROTOCOL_BASE_CREATE_NICK_ACK(EventErrorEnum uint_1)
  {
    ((PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_ACK) this).eventErrorEnum_0 = uint_1;
  }
}
