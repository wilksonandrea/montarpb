// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_ROOM_GET_SLOTINFO_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Utility;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_GET_SLOTINFO_ACK : GameServerPacket
{
  private readonly RoomModel roomModel_0;

  public virtual void Write()
  {
    this.WriteH((short) 3599);
    this.WriteD(((PROTOCOL_ROOM_GET_NICKNAME_ACK) this).int_0);
    this.WriteU(((PROTOCOL_ROOM_GET_NICKNAME_ACK) this).string_0, 66);
  }

  public PROTOCOL_ROOM_GET_SLOTINFO_ACK(uint channelModel_0, Account int_1, [In] int[] obj2)
  {
    ((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).uint_0 = channelModel_0;
    ((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).account_0 = int_1;
    if (int_1 != null)
    {
      ((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).playerInventory_0 = int_1.Inventory;
      ((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).playerEquipment_0 = int_1.Equipment;
      ((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).statisticTotal_0 = int_1.Statistic.Basic;
      ((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).statisticSeason_0 = int_1.Statistic.Season;
      ((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).clanModel_0 = ClanManager.GetClan(int_1.ClanId);
    }
    ((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).int_0 = obj2;
  }

  public virtual void Write()
  {
    this.WriteH((short) 3597);
    this.WriteH((short) 0);
    this.WriteD(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).uint_0);
    if (((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).uint_0 != 0U)
      return;
    this.WriteD(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).int_0[1]);
    this.WriteD(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).int_0[0]);
    this.WriteD(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).statisticSeason_0.Matches);
    this.WriteD(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).statisticSeason_0.MatchWins);
    this.WriteD(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).statisticSeason_0.MatchLoses);
    this.WriteD(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).statisticSeason_0.MatchDraws);
    this.WriteD(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).statisticSeason_0.KillsCount);
    this.WriteD(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).statisticSeason_0.HeadshotsCount);
    this.WriteD(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).statisticSeason_0.DeathsCount);
    this.WriteD(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).statisticSeason_0.TotalMatchesCount);
    this.WriteD(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).statisticSeason_0.TotalKillsCount);
    this.WriteD(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).statisticSeason_0.EscapesCount);
    this.WriteD(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).statisticSeason_0.AssistsCount);
    this.WriteD(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).statisticSeason_0.MvpCount);
    this.WriteD(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).statisticTotal_0.Matches);
    this.WriteD(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).statisticTotal_0.MatchWins);
    this.WriteD(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).statisticTotal_0.MatchLoses);
    this.WriteD(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).statisticTotal_0.MatchDraws);
    this.WriteD(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).statisticTotal_0.KillsCount);
    this.WriteD(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).statisticTotal_0.HeadshotsCount);
    this.WriteD(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).statisticTotal_0.DeathsCount);
    this.WriteD(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).statisticTotal_0.get_TotalMatchesCount());
    this.WriteD(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).statisticTotal_0.get_TotalKillsCount());
    this.WriteD(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).statisticTotal_0.EscapesCount);
    this.WriteD(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).statisticTotal_0.AssistsCount);
    this.WriteD(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).statisticTotal_0.MvpCount);
    this.WriteC((byte) 3);
    this.WriteB(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).playerEquipment_0.DinoItem));
    this.WriteB(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).playerEquipment_0.SprayId));
    this.WriteB(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).playerEquipment_0.NameCardId));
    this.WriteB(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).playerEquipment_0.WeaponPrimary));
    this.WriteB(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).playerEquipment_0.WeaponSecondary));
    this.WriteB(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).playerEquipment_0.WeaponMelee));
    this.WriteB(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).playerEquipment_0.WeaponExplosive));
    this.WriteB(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).playerEquipment_0.WeaponSpecial));
    this.WriteB(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).int_0[0]));
    this.WriteB(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).playerEquipment_0.PartHead));
    this.WriteB(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).playerEquipment_0.PartFace));
    this.WriteB(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).playerEquipment_0.PartJacket));
    this.WriteB(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).playerEquipment_0.PartPocket));
    this.WriteB(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).playerEquipment_0.PartGlove));
    this.WriteB(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).playerEquipment_0.PartBelt));
    this.WriteB(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).playerEquipment_0.PartHolster));
    this.WriteB(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).playerEquipment_0.PartSkin));
    this.WriteB(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).playerEquipment_0.BeretItem));
    this.WriteD(590851);
    this.WriteU(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).account_0.Nickname, 66);
    this.WriteD(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).account_0.GetRank());
    this.WriteD(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).account_0.Rank);
    this.WriteD(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).account_0.Gold);
    this.WriteD(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).account_0.Exp);
    this.WriteD(0);
    this.WriteC((byte) 0);
    this.WriteQ(0L);
    this.WriteC((byte) ((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).account_0.AuthLevel());
    this.WriteC((byte) 0);
    this.WriteD(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).account_0.Tags);
    this.WriteH((short) 0);
    this.WriteD(uint.Parse(DateTimeUtil.Now("yyMMddHHmm")));
    this.WriteH((ushort) ((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).account_0.InventoryPlus);
    this.WriteD(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).account_0.Cash);
    this.WriteD(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).clanModel_0.Id);
    this.WriteD(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).account_0.ClanAccess);
    this.WriteQ(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).account_0.StatusId());
    this.WriteC((byte) ((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).account_0.CafePC);
    this.WriteC((byte) ((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).account_0.Country);
    this.WriteU(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).clanModel_0.Name, 34);
    this.WriteC((byte) ((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).clanModel_0.Rank);
    this.WriteC((byte) ((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).clanModel_0.GetClanUnit());
    this.WriteD(((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).clanModel_0.Logo);
    this.WriteC((byte) ((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).clanModel_0.NameColor);
    this.WriteC((byte) ((PROTOCOL_ROOM_GET_PLAYERINFO_ACK) this).clanModel_0.Effect);
  }

  public PROTOCOL_ROOM_GET_SLOTINFO_ACK(int int_1, int string_1)
  {
    ((PROTOCOL_ROOM_GET_RANK_ACK) this).int_0 = int_1;
    ((PROTOCOL_ROOM_GET_RANK_ACK) this).int_1 = string_1;
  }
}
