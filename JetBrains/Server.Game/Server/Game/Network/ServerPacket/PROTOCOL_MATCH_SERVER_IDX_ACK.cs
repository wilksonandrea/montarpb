// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_MATCH_SERVER_IDX_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_MATCH_SERVER_IDX_ACK : GameServerPacket
{
  private readonly short short_0;

  public virtual void Write()
  {
    this.WriteH((short) 2438);
    this.WriteH((short) 0);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).uint_0);
    if (((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).uint_0 != 0U)
      return;
    this.WriteQ(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).account_0.PlayerId);
    this.WriteU(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).account_0.Nickname, 66);
    this.WriteB(this.method_0(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).account_0));
    if (((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).account_0.GetRank() != 53 && ((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).account_0.GetRank() != 54)
      this.WriteC((byte) ((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).account_0.GetRank());
    else
      this.WriteC((byte) 55);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).account_0.Exp);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticSeason_0.Matches);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticSeason_0.MatchWins);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticSeason_0.MatchDraws);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticSeason_0.MatchLoses);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticSeason_0.EscapesCount);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticSeason_0.KillsCount);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticSeason_0.DeathsCount);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticSeason_0.HeadshotsCount);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticSeason_0.AssistsCount);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticSeason_0.MvpCount);
    this.WriteB(new byte[45]);
    this.WriteD(1000);
    this.WriteB(new byte[82]);
    this.WriteD(0);
    this.WriteB(new byte[128 /*0x80*/]);
    this.WriteC((byte) 5);
    this.WriteC((byte) 1);
    this.WriteC((byte) 0);
    this.WriteD(ComDiv.GetPlayerStatus(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).account_0.Status, ((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).account_0.IsOnline));
    this.WriteB(((PROTOCOL_MESSENGER_NOTE_CHECK_READED_ACK) this).method_1(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).account_0));
    this.WriteC((byte) 8);
    this.WriteC((byte) 0);
    this.WriteC((byte) 7);
    this.WriteC((byte) 0);
    this.WriteD(52);
    this.WriteC((byte) 1);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteC((byte) 0);
    this.WriteC((byte) ((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).account_0.NickColor);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).account_0.Bonus.FakeRank);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).account_0.Bonus.FakeRank);
    this.WriteU(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).account_0.Bonus.FakeNick, 66);
    this.WriteH((short) ((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).account_0.Bonus.CrosshairColor);
    this.WriteH((short) ((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).account_0.Bonus.MuzzleColor);
    this.WriteC((byte) ((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).account_0.Bonus.NickBorderColor);
    this.WriteC((byte) 2);
    this.WriteC((byte) 0);
    this.WriteB(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).playerEquipment_0.WeaponPrimary));
    this.WriteB(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).playerEquipment_0.WeaponSecondary));
    this.WriteB(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).playerEquipment_0.WeaponMelee));
    this.WriteB(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).playerEquipment_0.WeaponExplosive));
    this.WriteB(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).playerEquipment_0.WeaponSpecial));
    this.WriteB(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).int_0));
    this.WriteB(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).playerEquipment_0.PartHead));
    this.WriteB(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).playerEquipment_0.PartFace));
    this.WriteB(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).playerEquipment_0.PartJacket));
    this.WriteB(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).playerEquipment_0.PartPocket));
    this.WriteB(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).playerEquipment_0.PartGlove));
    this.WriteB(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).playerEquipment_0.PartBelt));
    this.WriteB(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).playerEquipment_0.PartHolster));
    this.WriteB(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).playerEquipment_0.PartSkin));
    this.WriteB(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).playerEquipment_0.BeretItem));
    this.WriteC((byte) 0);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).playerEquipment_0.CharaRedId);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).playerEquipment_0.CharaBlueId);
    this.WriteB(new byte[631]);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticWeapon_0.AssaultKills);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticWeapon_0.AssaultDeaths);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticWeapon_0.SmgKills);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticWeapon_0.SmgDeaths);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticWeapon_0.SniperKills);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticWeapon_0.SniperDeaths);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticWeapon_0.MachinegunKills);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticWeapon_0.MachinegunDeaths);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticWeapon_0.ShotgunKills);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticWeapon_0.ShotgunDeaths);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticWeapon_0.ShieldKills);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticWeapon_0.ShieldDeaths);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).playerEquipment_0.CharaRedId);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).playerEquipment_0.CharaBlueId);
    this.WriteC((byte) 0);
    this.WriteD(16 /*0x10*/);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).playerEquipment_0.NameCardId);
    this.WriteC((byte) 0);
    this.WriteB(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).playerInventory_0.EquipmentData(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).playerEquipment_0.SprayId));
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticAcemode_0.Matches);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticAcemode_0.MatchWins);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticAcemode_0.MatchLoses);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticAcemode_0.Kills);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticAcemode_0.Deaths);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticAcemode_0.Headshots);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticAcemode_0.Assists);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticAcemode_0.Escapes);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticAcemode_0.Winstreaks);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticBattlecup_0.Matches);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).account_0.Statistic.GetBCWinRatio());
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticBattlecup_0.MatchLoses);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticBattlecup_0.KillsCount);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticBattlecup_0.DeathsCount);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticBattlecup_0.HeadshotsCount);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticBattlecup_0.AssistsCount);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticBattlecup_0.EscapesCount);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).account_0.Statistic.GetBCKDRatio());
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticBattlecup_0.MatchWins);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticBattlecup_0.AverageDamage);
    this.WriteD(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK) this).statisticBattlecup_0.PlayTime);
  }

  private byte[] method_0([In] Account obj0)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      ClanModel clan = ClanManager.GetClan(obj0.ClanId);
      syncServerPacket.WriteU(clan.Name, 34);
      syncServerPacket.WriteD(clan.Logo);
      syncServerPacket.WriteC((byte) clan.Effect);
      return syncServerPacket.ToArray();
    }
  }
}
