// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BASE_GET_MYINFO_RECORD_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Server.Game.Data.Models;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_GET_MYINFO_RECORD_ACK : GameServerPacket
{
  private readonly PlayerStatistic playerStatistic_0;

  public virtual void Write()
  {
    this.WriteH((short) 2312);
    this.WriteB(((PROTOCOL_BASE_GAMEGUARD_ACK) this).byte_0);
    this.WriteD(((PROTOCOL_BASE_GAMEGUARD_ACK) this).int_0);
  }

  public PROTOCOL_BASE_GET_MYINFO_RECORD_ACK(
    SChannelModel statisticDaily_1,
    List<ChannelModel> byte_1)
  {
    ((PROTOCOL_BASE_GET_CHANNELLIST_ACK) this).schannelModel_0 = statisticDaily_1;
    ((PROTOCOL_BASE_GET_CHANNELLIST_ACK) this).list_0 = byte_1;
  }
}
