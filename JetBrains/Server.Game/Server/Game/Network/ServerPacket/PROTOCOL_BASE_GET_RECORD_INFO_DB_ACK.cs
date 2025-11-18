// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BASE_GET_RECORD_INFO_DB_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_GET_RECORD_INFO_DB_ACK : GameServerPacket
{
  private readonly PlayerStatistic playerStatistic_0;

  public virtual void Write()
  {
    this.WriteH((short) 2333);
    this.WriteD(0);
    this.WriteD(1);
    this.WriteD(0);
    this.WriteH((short) 0);
    this.WriteC((byte) 0);
    this.WriteC((byte) ((PROTOCOL_BASE_GET_CHANNELLIST_ACK) this).list_0.Count);
    foreach (ChannelModel channelModel in ((PROTOCOL_BASE_GET_CHANNELLIST_ACK) this).list_0)
      this.WriteH((ushort) channelModel.Players.Count);
    this.WriteH((short) 310);
    this.WriteH((ushort) ((PROTOCOL_BASE_GET_CHANNELLIST_ACK) this).schannelModel_0.ChannelPlayers);
    this.WriteC((byte) ((PROTOCOL_BASE_GET_CHANNELLIST_ACK) this).list_0.Count);
    this.WriteC((byte) 0);
  }

  public PROTOCOL_BASE_GET_RECORD_INFO_DB_ACK([In] Account obj0)
  {
    ((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).account_0 = obj0;
    if (obj0 == null)
      return;
    ((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).clanModel_0 = ClanManager.GetClan(obj0.ClanId);
  }
}
