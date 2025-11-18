// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK : GameServerPacket
{
  private readonly uint uint_0;
  private readonly Account account_0;
  private readonly PlayerInventory playerInventory_0;
  private readonly PlayerEquipment playerEquipment_0;
  private readonly StatisticSeason statisticSeason_0;
  private readonly StatisticWeapon statisticWeapon_0;
  private readonly StatisticAcemode statisticAcemode_0;
  private readonly StatisticBattlecup statisticBattlecup_0;
  private readonly int int_0;

  public virtual void Write()
  {
    this.WriteH((short) 2562);
    this.WriteD(((PROTOCOL_LOBBY_LEAVE_ACK) this).uint_0);
  }

  public PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK([In] Account obj0)
  {
    ((PROTOCOL_LOBBY_NEW_MYINFO_ACK) this).account_0 = obj0;
    if (obj0 == null)
      return;
    ((PROTOCOL_LOBBY_NEW_MYINFO_ACK) this).clanModel_0 = ClanManager.GetClan(obj0.ClanId);
    ((PROTOCOL_LOBBY_NEW_MYINFO_ACK) this).statisticClan_0 = obj0.Statistic.Clan;
  }

  public virtual void Write()
  {
    this.WriteH((short) 977);
    this.WriteD(0);
    this.WriteQ(((PROTOCOL_LOBBY_NEW_MYINFO_ACK) this).account_0.PlayerId);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(10101);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(20202);
    this.WriteD(0);
    this.WriteD(30303);
    this.WriteD(0);
    this.WriteD(40404);
    this.WriteD(50505);
    this.WriteD(60606);
    this.WriteD(0);
    this.WriteD(70707);
    this.WriteD(80808);
    this.WriteD(90909);
    this.WriteD(101010);
    this.WriteD(111111);
    this.WriteD(121212);
    this.WriteD(131313);
    this.WriteD(141414);
    this.WriteD(151515);
    this.WriteD(161616);
    this.WriteD(171717);
    this.WriteD(181818);
    this.WriteD(191919);
    this.WriteD(0);
  }

  public PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK(
    [In] uint obj0,
    [In] List<QuickstartModel> obj1,
    [In] RoomModel obj2,
    [In] QuickstartModel obj3)
  {
    ((PROTOCOL_LOBBY_QUICKJOIN_ROOM_ACK) this).uint_0 = obj0;
    ((PROTOCOL_LOBBY_QUICKJOIN_ROOM_ACK) this).list_0 = obj1;
    ((PROTOCOL_LOBBY_QUICKJOIN_ROOM_ACK) this).quickstartModel_0 = obj3;
    ((PROTOCOL_LOBBY_QUICKJOIN_ROOM_ACK) this).roomModel_0 = obj2;
  }
}
