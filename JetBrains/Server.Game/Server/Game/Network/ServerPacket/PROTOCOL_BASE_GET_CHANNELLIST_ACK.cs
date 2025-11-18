// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BASE_GET_CHANNELLIST_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Server.Game.Data.Models;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_GET_CHANNELLIST_ACK : GameServerPacket
{
  private readonly SChannelModel schannelModel_0;
  private readonly List<ChannelModel> list_0;

  public virtual void Write()
  {
    this.WriteH((short) 2327);
    this.WriteH((short) 0);
    this.WriteD(((PROTOCOL_BASE_CREATE_NICK_ACK) this).uint_0);
    if (((PROTOCOL_BASE_CREATE_NICK_ACK) this).uint_0 != 0U)
      return;
    this.WriteC((byte) 1);
    this.WriteB(((PROTOCOL_BASE_CREATE_NICK_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_BASE_CREATE_NICK_ACK) this).playerEquipment_0.DinoItem));
    this.WriteC((byte) (((PROTOCOL_BASE_CREATE_NICK_ACK) this).account_0.Nickname.Length * 2));
    this.WriteU(((PROTOCOL_BASE_CREATE_NICK_ACK) this).account_0.Nickname, ((PROTOCOL_BASE_CREATE_NICK_ACK) this).account_0.Nickname.Length * 2);
  }

  public PROTOCOL_BASE_GET_CHANNELLIST_ACK([In] StatisticDaily obj0, [In] byte obj1, uint playerEvent_1)
  {
    ((PROTOCOL_BASE_DAILY_RECORD_ACK) this).statisticDaily_0 = obj0;
    ((PROTOCOL_BASE_DAILY_RECORD_ACK) this).byte_0 = obj1;
    ((PROTOCOL_BASE_DAILY_RECORD_ACK) this).uint_0 = playerEvent_1;
  }
}
