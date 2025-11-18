// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_MATCH_CLAN_SEASON_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_MATCH_CLAN_SEASON_ACK : GameServerPacket
{
  private readonly bool bool_0;

  public virtual void Write()
  {
    this.WriteH((short) 5378);
    this.WriteD(((PROTOCOL_LOBBY_QUICKJOIN_ROOM_ACK) this).uint_0);
    foreach (QuickstartModel quickstartModel in ((PROTOCOL_LOBBY_QUICKJOIN_ROOM_ACK) this).list_0)
    {
      this.WriteC((byte) quickstartModel.MapId);
      this.WriteC((byte) quickstartModel.Rule);
      this.WriteC((byte) quickstartModel.StageOptions);
      this.WriteC((byte) quickstartModel.Type);
    }
    if (((PROTOCOL_LOBBY_QUICKJOIN_ROOM_ACK) this).uint_0 != 0U)
      return;
    this.WriteC((byte) ((PROTOCOL_LOBBY_QUICKJOIN_ROOM_ACK) this).roomModel_0.ChannelId);
    this.WriteD(((PROTOCOL_LOBBY_QUICKJOIN_ROOM_ACK) this).roomModel_0.RoomId);
    this.WriteH((short) 1);
    if (((PROTOCOL_LOBBY_QUICKJOIN_ROOM_ACK) this).uint_0 != 0U)
    {
      this.WriteC((byte) ((PROTOCOL_LOBBY_QUICKJOIN_ROOM_ACK) this).quickstartModel_0.MapId);
      this.WriteC((byte) ((PROTOCOL_LOBBY_QUICKJOIN_ROOM_ACK) this).quickstartModel_0.Rule);
      this.WriteC((byte) ((PROTOCOL_LOBBY_QUICKJOIN_ROOM_ACK) this).quickstartModel_0.StageOptions);
      this.WriteC((byte) ((PROTOCOL_LOBBY_QUICKJOIN_ROOM_ACK) this).quickstartModel_0.Type);
    }
    else
    {
      this.WriteC((byte) 0);
      this.WriteC((byte) 0);
      this.WriteC((byte) 0);
      this.WriteC((byte) 0);
    }
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
  }

  public PROTOCOL_MATCH_CLAN_SEASON_ACK(uint account_1, Account list_1, [In] int obj2)
  {
    ((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).uint_0 = account_1;
    ((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).account_0 = list_1;
    if (list_1 == null)
      return;
    ((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).playerInventory_0 = list_1.Inventory;
    ((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).playerEquipment_0 = list_1.Equipment;
    ((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticSeason_0 = list_1.Statistic.Season;
    ((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticWeapon_0 = list_1.Statistic.Weapon;
    ((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticAcemode_0 = list_1.Statistic.Acemode;
    ((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticBattlecup_0 = list_1.Statistic.Battlecup;
    ((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).int_0 = obj2 == int.MaxValue ? ((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).playerEquipment_0.CharaRedId : obj2;
  }
}
