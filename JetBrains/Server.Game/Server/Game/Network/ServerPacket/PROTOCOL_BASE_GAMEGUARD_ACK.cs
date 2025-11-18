// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BASE_GAMEGUARD_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;
using Server.Game.Data.Utils;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_GAMEGUARD_ACK : GameServerPacket
{
  private readonly int int_0;
  private readonly byte[] byte_0;

  public virtual void Write()
  {
    this.WriteH((short) 2306);
    this.WriteH((short) 0);
    this.WriteC((byte) AllUtils.GetChannels(((PROTOCOL_BASE_CONNECT_ACK) this).int_0).Count);
    foreach (ChannelModel channel in AllUtils.GetChannels(((PROTOCOL_BASE_CONNECT_ACK) this).int_0))
      this.WriteC((byte) channel.Type);
    this.WriteH((ushort) (((PROTOCOL_BASE_CONNECT_ACK) this).list_0[0].Length + ((PROTOCOL_BASE_CONNECT_ACK) this).list_0[1].Length + 2));
    this.WriteH((ushort) ((PROTOCOL_BASE_CONNECT_ACK) this).list_0[0].Length);
    this.WriteB(((PROTOCOL_BASE_CONNECT_ACK) this).list_0[0]);
    this.WriteB(((PROTOCOL_BASE_CONNECT_ACK) this).list_0[1]);
    this.WriteC((byte) 3);
    this.WriteH((short) 80 /*0x50*/);
    this.WriteH(((PROTOCOL_BASE_CONNECT_ACK) this).ushort_0);
    this.WriteD(((PROTOCOL_BASE_CONNECT_ACK) this).int_1);
  }

  public PROTOCOL_BASE_GAMEGUARD_ACK(uint eventErrorEnum_1, Account eventVisitModel_1)
  {
    ((PROTOCOL_BASE_CREATE_NICK_ACK) this).uint_0 = eventErrorEnum_1;
    ((PROTOCOL_BASE_CREATE_NICK_ACK) this).account_0 = eventVisitModel_1;
    if (eventVisitModel_1 == null)
      return;
    ((PROTOCOL_BASE_CREATE_NICK_ACK) this).playerInventory_0 = eventVisitModel_1.Inventory;
    ((PROTOCOL_BASE_CREATE_NICK_ACK) this).playerEquipment_0 = eventVisitModel_1.Equipment;
  }
}
