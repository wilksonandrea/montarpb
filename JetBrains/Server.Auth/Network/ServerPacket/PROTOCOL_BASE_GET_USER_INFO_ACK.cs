// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ServerPacket.PROTOCOL_BASE_GET_USER_INFO_ACK
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using dummy_ptr;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Auth.Data.Models;
using Server.Auth.Data.Utils;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_BASE_GET_USER_INFO_ACK : AuthServerPacket
{
  private readonly Account account_0;
  private readonly ClanModel clanModel_0;
  private readonly PlayerInventory playerInventory_0;
  private readonly PlayerEquipment playerEquipment_0;
  private readonly PlayerStatistic playerStatistic_0;
  private readonly EventVisitModel eventVisitModel_0;
  private readonly List<QuickstartModel> list_0;
  private readonly List<CharacterModel> list_1;
  private readonly uint uint_0;
  private readonly uint uint_1;

  public PROTOCOL_BASE_GET_USER_INFO_ACK(ServerConfig uint_1)
  {
    ((PROTOCOL_BASE_GET_SYSTEM_INFO_ACK) this).serverConfig_0 = uint_1;
    if (uint_1 != null)
    {
      ((PROTOCOL_BASE_GET_SYSTEM_INFO_ACK) this).list_0 = SChannelXML.Servers;
      ((PROTOCOL_BASE_GET_SYSTEM_INFO_ACK) this).list_1 = PlayerRankXML.Ranks;
      ((PROTOCOL_BASE_GET_SYSTEM_INFO_ACK) this).eventPlaytimeModel_0 = EventPlaytimeXML.GetRunningEvent();
    }
    ((PROTOCOL_BASE_GET_SYSTEM_INFO_ACK) this).string_0 = new string[2]
    {
      "ded9a5bc68c44c6b885ac376be4f08c6",
      "5c67549f9ea01f1c7429d2a6bb121844"
    };
  }

  public virtual void Write()
  {
    this.WriteH((short) 2315);
    this.WriteH((short) 0);
    this.WriteC((byte) ((PROTOCOL_BASE_GET_SYSTEM_INFO_ACK) this).string_0[0].Length);
    this.WriteS(((PROTOCOL_BASE_GET_SYSTEM_INFO_ACK) this).string_0[0], ((PROTOCOL_BASE_GET_SYSTEM_INFO_ACK) this).string_0[0].Length);
    this.WriteC((byte) ((PROTOCOL_BASE_GET_SYSTEM_INFO_ACK) this).string_0[1].Length);
    this.WriteS(((PROTOCOL_BASE_GET_SYSTEM_INFO_ACK) this).string_0[1], ((PROTOCOL_BASE_GET_SYSTEM_INFO_ACK) this).string_0[1].Length);
    this.WriteD(0);
    this.WriteD(16 /*0x10*/);
    this.WriteB(new byte[61]);
    this.WriteH((short) 5);
    this.WriteH((short) 120);
    this.WriteH((short) 1026);
    this.WriteC((byte) 0);
    this.WriteH((short) 770);
    this.WriteC((byte) 1);
    this.WriteH((short) 200);
    this.WriteH((short) 0);
    this.WriteD(50);
    this.WriteD(50);
    this.WriteC((byte) 1);
    this.WriteH((short) 1000);
    this.WriteC((byte) 0);
    this.WriteD(153699);
    this.WriteC((byte) 0);
    this.WriteC((byte) 1);
    this.WriteB(new byte[373]);
    this.WriteC((byte) ((PROTOCOL_BASE_GET_SYSTEM_INFO_ACK) this).serverConfig_0.Showroom);
    this.WriteC((byte) 5);
    this.WriteC((byte) 4);
    this.WriteH((short) 3500);
    this.WriteH((short) 0);
    this.WriteH((short) 1450);
    this.WriteH((short) 0);
    this.WriteD(49);
    this.WriteD(1);
    this.WriteH((short) 1793);
    this.WriteC((byte) 1);
    this.WriteH((short) 8483);
    this.WriteH((short) 0);
    this.WriteB(new byte[52]);
    this.WriteH((short) 2565);
    this.WriteB(new byte[229]);
    this.WriteB(this.method_2(((PROTOCOL_BASE_GET_SYSTEM_INFO_ACK) this).eventPlaytimeModel_0));
    this.WriteC((byte) ((PROTOCOL_BASE_GET_SYSTEM_INFO_ACK) this).serverConfig_0.Missions);
    this.WriteH((ushort) MissionConfigXML.MissionPage1);
    this.WriteH((ushort) MissionConfigXML.MissionPage2);
    this.WriteH((ushort) this.SECURITY_KEY);
    this.WriteB(this.method_0(((PROTOCOL_BASE_GET_SYSTEM_INFO_ACK) this).list_0));
    this.WriteC((byte) 1);
    this.WriteC((byte) this.NATIONS);
    this.WriteC((byte) 0);
    this.WriteH((short) ((PROTOCOL_BASE_GET_SYSTEM_INFO_ACK) this).serverConfig_0.ShopURL.Length);
    this.WriteS(((PROTOCOL_BASE_GET_SYSTEM_INFO_ACK) this).serverConfig_0.ShopURL, ((PROTOCOL_BASE_GET_SYSTEM_INFO_ACK) this).serverConfig_0.ShopURL.Length);
    this.WriteB(this.method_1(((PROTOCOL_BASE_GET_SYSTEM_INFO_ACK) this).list_1));
    this.WriteC((byte) 0);
    this.WriteC((byte) 6);
  }

  private byte[] method_0([In] List<SChannelModel> obj0)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      syncServerPacket.WriteC((byte) obj0.Count);
      foreach (SChannelModel schannelModel in obj0)
      {
        syncServerPacket.WriteD(schannelModel.State ? 1 : 0);
        syncServerPacket.WriteB(ComDiv.AddressBytes(schannelModel.Host));
        syncServerPacket.WriteB(ComDiv.AddressBytes(schannelModel.Host));
        syncServerPacket.WriteH(schannelModel.Port);
        syncServerPacket.WriteC((byte) schannelModel.Type);
        syncServerPacket.WriteH((ushort) schannelModel.MaxPlayers);
        syncServerPacket.WriteD(schannelModel.LastPlayers);
        if (schannelModel.Id == 0)
        {
          syncServerPacket.WriteB(Bitwise.HexStringToByteArray("01 01 01 01 01 01 01 01 01 01 0E 00 00 00 00"));
        }
        else
        {
          foreach (ChannelModel channel in AllUtils.GetChannels(schannelModel.Id))
            syncServerPacket.WriteC((byte) channel.Type);
          syncServerPacket.WriteC((byte) schannelModel.Type);
          syncServerPacket.WriteC((byte) 0);
          syncServerPacket.WriteH((short) 0);
        }
      }
      return syncServerPacket.ToArray();
    }
  }

  private byte[] method_1([In] List<RankModel> obj0)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      syncServerPacket.WriteC((byte) obj0.Count);
      foreach (RankModel rankModel in obj0)
      {
        syncServerPacket.WriteC((byte) rankModel.Id);
        List<int> rewards = PlayerRankXML.GetRewards(rankModel.Id);
        foreach (int num in rewards)
        {
          GoodsItem good = ShopManager.GetGood(num);
          syncServerPacket.WriteD(good == null ? 0 : good.Id);
        }
        for (int count = rewards.Count; 4 - count > 0; ++count)
          syncServerPacket.WriteD(0);
      }
      return syncServerPacket.ToArray();
    }
  }

  private byte[] method_2(EventPlaytimeModel int_1)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      syncServerPacket.WriteC((byte) 3);
      if (int_1 != null && int_1.EventIsEnabled())
      {
        syncServerPacket.WriteD(int_1.Minutes1 * 60);
        syncServerPacket.WriteD(int_1.Minutes2 * 60);
        syncServerPacket.WriteD(int_1.Minutes3 * 60);
      }
      else
        syncServerPacket.WriteB(new byte[12]);
      return syncServerPacket.ToArray();
    }
  }

  public PROTOCOL_BASE_GET_USER_INFO_ACK([In] Account obj0)
  {
    this.account_0 = obj0;
    if (obj0 != null)
    {
      this.playerInventory_0 = obj0.Inventory;
      this.playerEquipment_0 = obj0.Equipment;
      this.playerStatistic_0 = obj0.Statistic;
      this.uint_1 = uint.Parse(obj0.LastLoginDate.ToString("yyMMddHHmm"));
      this.clanModel_0 = \u007B1a8343ff\u002Da99e\u002D485e\u002Dad67\u002D094168725faa\u007D.GetClanDB((object) obj0.ClanId, 1);
      this.list_0 = obj0.Quickstart.Quickjoins;
      this.list_1 = obj0.Character.Characters;
      this.eventVisitModel_0 = EventVisitXML.GetRunningEvent();
    }
    else
      this.uint_0 = 2147483648U /*0x80000000*/;
  }

  public virtual void Write()
  {
    this.WriteH((short) 2317);
    this.WriteH((short) 0);
    this.WriteD(this.uint_0);
    if (this.uint_0 != 0U)
      return;
    this.WriteB(new byte[21]);
    this.WriteD(1);
    this.WriteC((byte) 1);
    this.WriteC((byte) 6);
    this.WriteC((byte) 1);
    this.WriteB(new byte[160 /*0xA0*/]);
    this.WriteD(this.playerStatistic_0.Battlecup.Matches);
    this.WriteD(this.playerStatistic_0.GetBCWinRatio());
    this.WriteD(this.playerStatistic_0.Battlecup.MatchLoses);
    this.WriteD(this.playerStatistic_0.Battlecup.KillsCount);
    this.WriteD(this.playerStatistic_0.Battlecup.DeathsCount);
    this.WriteD(this.playerStatistic_0.Battlecup.HeadshotsCount);
    this.WriteD(this.playerStatistic_0.Battlecup.AssistsCount);
    this.WriteD(this.playerStatistic_0.Battlecup.EscapesCount);
    this.WriteD(this.playerStatistic_0.GetBCKDRatio());
    this.WriteD(this.playerStatistic_0.Battlecup.MatchWins);
    this.WriteD(this.playerStatistic_0.Battlecup.AverageDamage);
    this.WriteD(this.playerStatistic_0.Battlecup.PlayTime);
    this.WriteD(this.playerStatistic_0.Acemode.Matches);
    this.WriteD(this.playerStatistic_0.Acemode.MatchWins);
    this.WriteD(this.playerStatistic_0.Acemode.MatchLoses);
    this.WriteD(this.playerStatistic_0.Acemode.Kills);
    this.WriteD(this.playerStatistic_0.Acemode.Deaths);
    this.WriteD(this.playerStatistic_0.Acemode.Headshots);
    this.WriteD(this.playerStatistic_0.Acemode.Assists);
    this.WriteD(this.playerStatistic_0.Acemode.Escapes);
    this.WriteD(this.playerStatistic_0.Acemode.Winstreaks);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.AccessoryId));
    this.WriteB(((PROTOCOL_BASE_LOGIN_WAIT_ACK) this).method_4(3));
    this.WriteB(((PROTOCOL_BASE_LOGIN_WAIT_ACK) this).method_3(3));
    this.WriteD(this.playerStatistic_0.Weapon.AssaultKills);
    this.WriteD(this.playerStatistic_0.Weapon.AssaultDeaths);
    this.WriteD(this.playerStatistic_0.Weapon.SmgKills);
    this.WriteD(this.playerStatistic_0.Weapon.SmgDeaths);
    this.WriteD(this.playerStatistic_0.Weapon.SniperKills);
    this.WriteD(this.playerStatistic_0.Weapon.SniperDeaths);
    this.WriteD(this.playerStatistic_0.Weapon.MachinegunKills);
    this.WriteD(this.playerStatistic_0.Weapon.MachinegunDeaths);
    this.WriteD(this.playerStatistic_0.Weapon.ShotgunKills);
    this.WriteD(this.playerStatistic_0.Weapon.ShotgunDeaths);
    this.WriteD(this.playerStatistic_0.Weapon.ShieldKills);
    this.WriteD(this.playerStatistic_0.Weapon.ShieldDeaths);
    this.WriteC((byte) this.list_1.Count);
    this.WriteC((byte) this.NATIONS);
    this.WriteC((byte) 0);
    this.WriteB(((PROTOCOL_BASE_LOGIN_ACK) this).method_2(this.list_0));
    this.WriteB(new byte[33]);
    this.WriteC((byte) 4);
    this.WriteB(new byte[20]);
    this.WriteD(this.account_0.Title.Slots);
    this.WriteC((byte) 3);
    this.WriteC((byte) this.account_0.Title.Equiped1);
    this.WriteC((byte) this.account_0.Title.Equiped2);
    this.WriteC((byte) this.account_0.Title.Equiped3);
    this.WriteQ(this.account_0.Title.Flags);
    this.WriteC((byte) 160 /*0xA0*/);
    this.WriteB(this.account_0.Mission.List1);
    this.WriteB(this.account_0.Mission.List2);
    this.WriteB(this.account_0.Mission.List3);
    this.WriteB(this.account_0.Mission.List4);
    this.WriteC((byte) this.account_0.Mission.ActualMission);
    this.WriteC((byte) this.account_0.Mission.Card1);
    this.WriteC((byte) this.account_0.Mission.Card2);
    this.WriteC((byte) this.account_0.Mission.Card3);
    this.WriteC((byte) this.account_0.Mission.Card4);
    this.WriteB(ComDiv.GetMissionCardFlags(this.account_0.Mission.Mission1, this.account_0.Mission.List1));
    this.WriteB(ComDiv.GetMissionCardFlags(this.account_0.Mission.Mission2, this.account_0.Mission.List2));
    this.WriteB(ComDiv.GetMissionCardFlags(this.account_0.Mission.Mission3, this.account_0.Mission.List3));
    this.WriteB(ComDiv.GetMissionCardFlags(this.account_0.Mission.Mission4, this.account_0.Mission.List4));
    this.WriteC((byte) this.account_0.Mission.Mission1);
    this.WriteC((byte) this.account_0.Mission.Mission2);
    this.WriteC((byte) this.account_0.Mission.Mission3);
    this.WriteC((byte) this.account_0.Mission.Mission4);
    this.WriteD(this.account_0.MasterMedal);
    this.WriteD(this.account_0.Medal);
    this.WriteD(this.account_0.Ensign);
    this.WriteD(this.account_0.Ribbon);
    this.WriteD(0);
    this.WriteC((byte) 0);
    this.WriteD(0);
    this.WriteC((byte) 2);
    this.WriteB(new byte[406]);
    this.WriteB(((PROTOCOL_BASE_LOGIN_ACK) this).method_1(this.account_0, this.eventVisitModel_0));
    this.WriteC((byte) 2);
    this.WriteD(0);
    this.WriteC((byte) 0);
    this.WriteD(0);
    this.WriteB(((PROTOCOL_BASE_LOGIN_ACK) this).method_0(this.account_0, this.eventVisitModel_0, this.uint_1));
    this.WriteB(ComDiv.AddressBytes("127.0.0.1"));
    this.WriteD(this.uint_1);
    this.WriteC(this.list_1.Count == 0 ? (byte) 0 : (byte) this.account_0.Character.GetCharacter(this.playerEquipment_0.CharaRedId).Slot);
    this.WriteC(this.list_1.Count == 0 ? (byte) 1 : (byte) this.account_0.Character.GetCharacter(this.playerEquipment_0.CharaBlueId).Slot);
    this.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.DinoItem));
    this.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.SprayId));
    this.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.NameCardId));
    this.WriteQ(AllUtils.LoadCouponEffects(this.account_0));
    this.WriteD(0);
    this.WriteC((byte) 0);
    this.WriteT(((ChannelModel) this.account_0).PointUp());
    this.WriteT(((ChannelModel) this.account_0).ExpUp());
    this.WriteC((byte) 0);
    this.WriteC((byte) this.account_0.NickColor);
    this.WriteD(this.account_0.Bonus.FakeRank);
    this.WriteD(this.account_0.Bonus.FakeRank);
    this.WriteU(this.account_0.Bonus.FakeNick, 66);
    this.WriteH((short) this.account_0.Bonus.CrosshairColor);
    this.WriteH((short) this.account_0.Bonus.MuzzleColor);
    this.WriteC((byte) this.account_0.Bonus.NickBorderColor);
    this.WriteD(this.playerStatistic_0.Season.Matches);
    this.WriteD(this.playerStatistic_0.Season.MatchWins);
    this.WriteD(this.playerStatistic_0.Season.MatchLoses);
    this.WriteD(this.playerStatistic_0.Season.MatchDraws);
    this.WriteD(this.playerStatistic_0.Season.KillsCount);
    this.WriteD(this.playerStatistic_0.Season.HeadshotsCount);
    this.WriteD(this.playerStatistic_0.Season.DeathsCount);
    this.WriteD(this.playerStatistic_0.Season.TotalMatchesCount);
    this.WriteD(this.playerStatistic_0.Season.TotalKillsCount);
    this.WriteD(this.playerStatistic_0.Season.EscapesCount);
    this.WriteD(this.playerStatistic_0.Season.AssistsCount);
    this.WriteD(this.playerStatistic_0.Season.MvpCount);
    this.WriteD(this.playerStatistic_0.Basic.Matches);
    this.WriteD(this.playerStatistic_0.Basic.MatchWins);
    this.WriteD(this.playerStatistic_0.Basic.MatchLoses);
    this.WriteD(this.playerStatistic_0.Basic.MatchDraws);
    this.WriteD(this.playerStatistic_0.Basic.KillsCount);
    this.WriteD(this.playerStatistic_0.Basic.HeadshotsCount);
    this.WriteD(this.playerStatistic_0.Basic.DeathsCount);
    this.WriteD(this.playerStatistic_0.Basic.get_TotalMatchesCount());
    this.WriteD(this.playerStatistic_0.Basic.get_TotalKillsCount());
    this.WriteD(this.playerStatistic_0.Basic.EscapesCount);
    this.WriteD(this.playerStatistic_0.Basic.AssistsCount);
    this.WriteD(this.playerStatistic_0.Basic.MvpCount);
    this.WriteU(this.account_0.Nickname, 66);
    this.WriteD(this.account_0.Rank);
    this.WriteD(this.account_0.GetRank());
    this.WriteD(this.account_0.Gold);
    this.WriteD(this.account_0.Exp);
    this.WriteD(0);
    this.WriteC((byte) 0);
    this.WriteQ(0L);
    this.WriteC((byte) this.account_0.AuthLevel());
    this.WriteC((byte) 0);
    this.WriteD(this.account_0.Tags);
    this.WriteH((short) 0);
    this.WriteD(this.uint_1);
    this.WriteH((ushort) this.account_0.InventoryPlus);
    this.WriteD(this.account_0.Cash);
    this.WriteD(this.clanModel_0.Id);
    this.WriteD(this.account_0.ClanAccess);
    this.WriteQ(this.account_0.StatusId());
    this.WriteC((byte) this.account_0.CafePC);
    this.WriteC((byte) this.account_0.Country);
    this.WriteU(this.clanModel_0.Name, 34);
    this.WriteC((byte) this.clanModel_0.Rank);
    this.WriteC((byte) this.clanModel_0.GetClanUnit());
    this.WriteD(this.clanModel_0.Logo);
    this.WriteC((byte) this.clanModel_0.NameColor);
    this.WriteC((byte) this.clanModel_0.Effect);
    this.WriteC(AuthXender.Client.Config.EnableBlood ? (byte) this.account_0.Age : (byte) 42);
  }
}
