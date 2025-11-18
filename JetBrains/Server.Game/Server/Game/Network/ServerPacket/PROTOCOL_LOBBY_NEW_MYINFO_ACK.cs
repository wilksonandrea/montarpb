// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_LOBBY_NEW_MYINFO_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_LOBBY_NEW_MYINFO_ACK : GameServerPacket
{
  private readonly Account account_0;
  private readonly ClanModel clanModel_0;
  private readonly StatisticClan statisticClan_0;

  public virtual void Write()
  {
    this.WriteH((short) 2568);
    this.WriteC((byte) 0);
    this.WriteU(((PROTOCOL_LOBBY_GET_ROOMINFOADD_ACK) this).roomModel_0.LeaderName, 66);
    this.WriteC((byte) ((PROTOCOL_LOBBY_GET_ROOMINFOADD_ACK) this).roomModel_0.KillTime);
    this.WriteC((byte) (((PROTOCOL_LOBBY_GET_ROOMINFOADD_ACK) this).roomModel_0.Rounds - 1));
    this.WriteH((ushort) ((PROTOCOL_LOBBY_GET_ROOMINFOADD_ACK) this).roomModel_0.GetInBattleTime());
    this.WriteC(((PROTOCOL_LOBBY_GET_ROOMINFOADD_ACK) this).roomModel_0.Limit);
    this.WriteC(((PROTOCOL_LOBBY_GET_ROOMINFOADD_ACK) this).roomModel_0.WatchRuleFlag);
    this.WriteH((ushort) ((PROTOCOL_LOBBY_GET_ROOMINFOADD_ACK) this).roomModel_0.BalanceType);
    this.WriteB(((PROTOCOL_LOBBY_GET_ROOMINFOADD_ACK) this).roomModel_0.RandomMaps);
    this.WriteC(((PROTOCOL_LOBBY_GET_ROOMINFOADD_ACK) this).roomModel_0.CountdownIG);
    this.WriteB(((PROTOCOL_LOBBY_GET_ROOMINFOADD_ACK) this).roomModel_0.LeaderAddr);
    this.WriteC(((PROTOCOL_LOBBY_GET_ROOMINFOADD_ACK) this).roomModel_0.KillCam);
    this.WriteH((short) 0);
  }

  public PROTOCOL_LOBBY_NEW_MYINFO_ACK(
    [In] int obj0,
    [In] int obj1,
    int int_3,
    int bool_1,
    int string_3,
    [In] int obj5,
    [In] byte[] obj6,
    [In] byte[] obj7)
  {
    ((PROTOCOL_LOBBY_GET_ROOMLIST_ACK) this).int_3 = obj0;
    ((PROTOCOL_LOBBY_GET_ROOMLIST_ACK) this).int_2 = obj1;
    ((PROTOCOL_LOBBY_GET_ROOMLIST_ACK) this).int_0 = int_3;
    ((PROTOCOL_LOBBY_GET_ROOMLIST_ACK) this).int_1 = bool_1;
    ((PROTOCOL_LOBBY_GET_ROOMLIST_ACK) this).byte_0 = obj6;
    ((PROTOCOL_LOBBY_GET_ROOMLIST_ACK) this).byte_1 = obj7;
    ((PROTOCOL_LOBBY_GET_ROOMLIST_ACK) this).int_4 = string_3;
    ((PROTOCOL_LOBBY_GET_ROOMLIST_ACK) this).int_5 = obj5;
  }
}
