// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Utils.AllUtils
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Npgsql;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.RAW;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Sync;
using Server.Game.Data.Sync.Server;
using Server.Game.Data.XML;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Xml;

#nullable disable
namespace Server.Game.Data.Utils;

public static class AllUtils
{
  public static ChannelModel GetChannel(int roomModel_0, int list_0)
  {
    lock (ChannelsXML.Channels)
    {
      foreach (ChannelModel channel in ChannelsXML.Channels)
      {
        if (channel.ServerId == roomModel_0 && channel.Id == list_0)
          return channel;
      }
      return (ChannelModel) null;
    }
  }

  public static List<ChannelModel> GetChannels([In] int obj0)
  {
    List<ChannelModel> channels = new List<ChannelModel>(11);
    for (int index = 0; index < ChannelsXML.Channels.Count; ++index)
    {
      ChannelModel channel = ChannelsXML.Channels[index];
      if (channel.ServerId == obj0)
        channels.Add(channel);
    }
    return channels;
  }

  private static void smethod_0(string gameServerPacket_0)
  {
    XmlDocument xmlDocument = new XmlDocument();
    using (FileStream inStream = new FileStream(gameServerPacket_0, FileMode.Open))
    {
      if (inStream.Length == 0L)
      {
        CLogger.Print("File is empty: " + gameServerPacket_0, LoggerType.Warning, (Exception) null);
      }
      else
      {
        try
        {
          xmlDocument.Load((Stream) inStream);
          for (XmlNode xmlNode1 = xmlDocument.FirstChild; xmlNode1 != null; xmlNode1 = xmlNode1.NextSibling)
          {
            if ("List".Equals(xmlNode1.Name))
            {
              for (XmlNode xmlNode2 = xmlNode1.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
              {
                if ("Channel".Equals(xmlNode2.Name))
                {
                  int list_0 = int.Parse(xmlNode2.Attributes.GetNamedItem("ServerId").Value);
                  AllUtils.smethod_1(xmlNode2, list_0);
                }
              }
            }
          }
        }
        catch (XmlException ex)
        {
          CLogger.Print(ex.Message, LoggerType.Error, (Exception) ex);
        }
      }
      inStream.Dispose();
      inStream.Close();
    }
  }

  private static void smethod_1([In] XmlNode obj0, int list_0)
  {
    for (XmlNode xmlNode1 = obj0.FirstChild; xmlNode1 != null; xmlNode1 = xmlNode1.NextSibling)
    {
      if ("Count".Equals(xmlNode1.Name))
      {
        for (XmlNode xmlNode2 = xmlNode1.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
        {
          if ("Setting".Equals(xmlNode2.Name))
          {
            XmlNamedNodeMap attributes = (XmlNamedNodeMap) xmlNode2.Attributes;
            ChannelModel channelModel = new ChannelModel(list_0)
            {
              Id = int.Parse(attributes.GetNamedItem("Id").Value),
              Type = ComDiv.ParseEnum<ChannelType>(attributes.GetNamedItem("Type").Value),
              MaxRooms = int.Parse(attributes.GetNamedItem("MaxRooms").Value),
              ExpBonus = int.Parse(attributes.GetNamedItem("ExpBonus").Value),
              GoldBonus = int.Parse(attributes.GetNamedItem("GoldBonus").Value),
              CashBonus = int.Parse(attributes.GetNamedItem("CashBonus").Value)
            };
            try
            {
              if (channelModel.Type == ChannelType.CH_PW)
                channelModel.Password = attributes.GetNamedItem("Password").Value;
            }
            catch (XmlException ex)
            {
              CLogger.Print(ex.Message, LoggerType.Error, (Exception) ex);
            }
            ChannelModel channel = AllUtils.GetChannel(channelModel.ServerId, channelModel.Id);
            if (channel != null)
            {
              lock (ChannelsXML.Channels)
              {
                channel.Type = channelModel.Type;
                channel.MaxRooms = channelModel.MaxRooms;
                channel.ExpBonus = channelModel.ExpBonus;
                channel.GoldBonus = channelModel.GoldBonus;
                channel.CashBonus = channelModel.CashBonus;
              }
            }
            else
              ChannelsXML.Channels.Add(channelModel);
          }
        }
      }
    }
  }

  static AllUtils() => ChannelsXML.Channels = new List<ChannelModel>();

  public static void ValidateAuthLevel([In] Account obj0)
  {
    if (Enum.IsDefined(typeof (AccessLevel), (object) obj0.Access))
      return;
    AccessLevel ValueReq1 = obj0.AuthLevel();
    if (!ComDiv.UpdateDB("accounts", "access_level", (object) (int) ValueReq1, "player_id", (object) obj0.PlayerId))
      return;
    obj0.Access = ValueReq1;
  }

  public static void LoadPlayerInventory(Account ServerId)
  {
    lock (ServerId.Inventory.Items)
      ServerId.Inventory.Items.AddRange((IEnumerable<ItemsModel>) DaoManagerSQL.GetPlayerInventoryItems(ServerId.PlayerId));
  }

  public static void LoadPlayerMissions(Account string_0)
  {
    PlayerMissions playerMissionsDb = DaoManagerSQL.GetPlayerMissionsDB(string_0.PlayerId, string_0.Mission.Mission1, string_0.Mission.Mission2, string_0.Mission.Mission3, string_0.Mission.Mission4);
    if (playerMissionsDb != null)
    {
      string_0.Mission = playerMissionsDb;
    }
    else
    {
      if (DaoManagerSQL.CreatePlayerMissionsDB(string_0.PlayerId))
        return;
      CLogger.Print("There was an error when creating Player Missions!", LoggerType.Warning, (Exception) null);
    }
  }

  public static void ValidatePlayerInventoryStatus(Account xmlNode_0)
  {
    xmlNode_0.Inventory.LoadBasicItems();
    if (xmlNode_0.Rank >= 46)
      xmlNode_0.Inventory.LoadGeneralBeret();
    if (xmlNode_0.IsGM())
      xmlNode_0.Inventory.LoadHatForGM();
    if (!string.IsNullOrEmpty(xmlNode_0.Nickname))
      AllUtils.smethod_21(xmlNode_0);
    string int_0;
    if (AllUtils.smethod_0(xmlNode_0, ref int_0))
    {
      List<ItemsModel> pcCafeRewards = TemplatePackXML.GetPCCafeRewards(xmlNode_0.CafePC);
      lock (xmlNode_0.Inventory.Items)
        xmlNode_0.Inventory.Items.AddRange((IEnumerable<ItemsModel>) pcCafeRewards);
      foreach (ItemsModel Opening in pcCafeRewards)
      {
        if (ComDiv.GetIdStatics(Opening.Id, 1) == 6 && xmlNode_0.Character.GetCharacter(Opening.Id) == null)
          AllUtils.CreateCharacter(xmlNode_0, Opening);
        if (ComDiv.GetIdStatics(Opening.Id, 1) == 16 /*0x10*/)
        {
          CouponFlag couponEffect = CouponEffectXML.GetCouponEffect(Opening.Id);
          if (couponEffect != null && couponEffect.EffectFlag > (CouponEffects) 0 && !xmlNode_0.Effects.HasFlag((Enum) couponEffect.EffectFlag))
          {
            xmlNode_0.Effects |= couponEffect.EffectFlag;
            DaoManagerSQL.UpdateCouponEffect(xmlNode_0.PlayerId, xmlNode_0.Effects);
          }
        }
      }
    }
    else
    {
      foreach (ItemsModel pcCafeReward in TemplatePackXML.GetPCCafeRewards(xmlNode_0.CafePC))
      {
        if (ComDiv.GetIdStatics(pcCafeReward.Id, 1) == 6 && xmlNode_0.Character.GetCharacter(pcCafeReward.Id) != null)
          AllUtils.smethod_3(xmlNode_0, pcCafeReward.Id);
        if (ComDiv.GetIdStatics(pcCafeReward.Id, 1) == 16 /*0x10*/)
        {
          CouponFlag couponEffect = CouponEffectXML.GetCouponEffect(pcCafeReward.Id);
          if (couponEffect != null && couponEffect.EffectFlag > (CouponEffects) 0 && xmlNode_0.Effects.HasFlag((Enum) couponEffect.EffectFlag))
          {
            xmlNode_0.Effects -= couponEffect.EffectFlag;
            DaoManagerSQL.UpdateCouponEffect(xmlNode_0.PlayerId, xmlNode_0.Effects);
          }
        }
      }
      if (xmlNode_0.CafePC <= CafeEnum.None || !ComDiv.UpdateDB("accounts", "pc_cafe", (object) 0, "player_id", (object) xmlNode_0.PlayerId))
        return;
      xmlNode_0.CafePC = CafeEnum.None;
      if (!string.IsNullOrEmpty(int_0) && ComDiv.DeleteDB("player_vip", "owner_id", (object) xmlNode_0.PlayerId))
        CLogger.Print($"VIP for UID: {xmlNode_0.PlayerId} Nick: {xmlNode_0.Nickname} Deleted Due To {int_0}", LoggerType.Info, (Exception) null);
      CLogger.Print($"Player PC Cafe was resetted by default into '{xmlNode_0.CafePC}'; (UID: {xmlNode_0.PlayerId} Nick: {xmlNode_0.Nickname})", LoggerType.Info, (Exception) null);
    }
  }

  private static bool smethod_0([In] Account obj0, ref string int_0)
  {
    if (obj0.IsGM())
    {
      int_0 = "GM Special Access";
      return true;
    }
    PlayerVip playerVip = DaoManagerSQL.GetPlayerVIP(obj0.PlayerId);
    if (playerVip != null)
    {
      if (playerVip.Expirate < uint.Parse(DateTimeUtil.Now("yyMMddHHmm")))
      {
        int_0 = "The Time Has Expired!";
        return false;
      }
      if (!InternetCafeXML.IsValidAddress(DaoManagerSQL.GetPlayerIP4Address(obj0.PlayerId), playerVip.Address) && ConfigLoader.ICafeSystem)
      {
        int_0 = "Invalid Configuration!";
        return false;
      }
      string ValueReq1 = $"{obj0.CafePC}";
      if (!playerVip.Benefit.Equals(ValueReq1) && ComDiv.UpdateDB("player_vip", "last_benefit", (object) ValueReq1, "owner_id", (object) obj0.PlayerId))
        playerVip.Benefit = ValueReq1;
      int_0 = "Valid Access";
      return true;
    }
    int_0 = "Database Not Found!";
    return false;
  }

  public static void LoadPlayerEquipments(Account Player)
  {
    PlayerEquipment playerEquipmentsDb = DaoManagerSQL.GetPlayerEquipmentsDB(Player.PlayerId);
    if (playerEquipmentsDb != null)
    {
      Player.Equipment = playerEquipmentsDb;
    }
    else
    {
      if (DaoManagerSQL.CreatePlayerEquipmentsDB(Player.PlayerId))
        return;
      CLogger.Print("There was an error when creating Player Equipment!", LoggerType.Warning, (Exception) null);
    }
  }

  public static void LoadPlayerCharacters(Account Player)
  {
    List<CharacterModel> playerCharactersDb = DaoManagerSQL.GetPlayerCharactersDB(Player.PlayerId);
    if (playerCharactersDb.Count <= 0)
      return;
    Player.Character.Characters = playerCharactersDb;
  }

  public static void LoadPlayerStatistic(Account Player)
  {
    StatisticTotal playerStatBasicDb = DaoManagerSQL.GetPlayerStatBasicDB(Player.PlayerId);
    if (playerStatBasicDb != null)
      Player.Statistic.Basic = playerStatBasicDb;
    else if (!DaoManagerSQL.CreatePlayerStatBasicDB(Player.PlayerId))
      CLogger.Print("There was an error when creating Player Basic Statistic!", LoggerType.Warning, (Exception) null);
    StatisticSeason playerStatSeasonDb = DaoManagerSQL.GetPlayerStatSeasonDB(Player.PlayerId);
    if (playerStatSeasonDb != null)
      Player.Statistic.Season = playerStatSeasonDb;
    else if (!DaoManagerSQL.CreatePlayerStatSeasonDB(Player.PlayerId))
      CLogger.Print("There was an error when creating Player Season Statistic!", LoggerType.Warning, (Exception) null);
    StatisticClan playerStatClanDb = DaoManagerSQL.GetPlayerStatClanDB(Player.PlayerId);
    if (playerStatClanDb != null)
      Player.Statistic.Clan = playerStatClanDb;
    else if (!DaoManagerSQL.CreatePlayerStatClanDB(Player.PlayerId))
      CLogger.Print("There was an error when creating Player Clan Statistic!", LoggerType.Warning, (Exception) null);
    StatisticDaily playerStatDailiesDb = DaoManagerSQL.GetPlayerStatDailiesDB(Player.PlayerId);
    if (playerStatDailiesDb != null)
      Player.Statistic.Daily = playerStatDailiesDb;
    else if (!DaoManagerSQL.CreatePlayerStatDailiesDB(Player.PlayerId))
      CLogger.Print("There was an error when creating Player Daily Statistic!", LoggerType.Warning, (Exception) null);
    StatisticWeapon playerStatWeaponsDb = DaoManagerSQL.GetPlayerStatWeaponsDB(Player.PlayerId);
    if (playerStatWeaponsDb != null)
      Player.Statistic.Weapon = playerStatWeaponsDb;
    else if (!DaoManagerSQL.CreatePlayerStatWeaponsDB(Player.PlayerId))
      CLogger.Print("There was an error when creating Player Weapon Statistic!", LoggerType.Warning, (Exception) null);
    StatisticAcemode playerStatAcemodesDb = DaoManagerSQL.GetPlayerStatAcemodesDB(Player.PlayerId);
    if (playerStatAcemodesDb != null)
      Player.Statistic.Acemode = playerStatAcemodesDb;
    else if (!DaoManagerSQL.CreatePlayerStatAcemodesDB(Player.PlayerId))
      CLogger.Print("There was an error when creating Player Acemode Statistic!", LoggerType.Warning, (Exception) null);
    StatisticBattlecup playerStatBattlecupDb = DaoManagerSQL.GetPlayerStatBattlecupDB(Player.PlayerId);
    if (playerStatBattlecupDb != null)
    {
      Player.Statistic.Battlecup = playerStatBattlecupDb;
    }
    else
    {
      if (DaoManagerSQL.CreatePlayerStatBattlecupsDB(Player.PlayerId))
        return;
      CLogger.Print("There was an error when creating Player Battlecup Statistic!", LoggerType.Warning, (Exception) null);
    }
  }

  public static void LoadPlayerTitles(Account account_0)
  {
    PlayerTitles playerTitlesDb = DaoManagerSQL.GetPlayerTitlesDB(account_0.PlayerId);
    if (playerTitlesDb != null)
    {
      account_0.Title = playerTitlesDb;
    }
    else
    {
      if (DaoManagerSQL.CreatePlayerTitlesDB(account_0.PlayerId))
        return;
      CLogger.Print("There was an error when creating Player Title!", LoggerType.Warning, (Exception) null);
    }
  }

  public static void LoadPlayerBattlepass([In] Account obj0)
  {
    PlayerBattlepass playerBattlepassDb = DaoManagerSQL.GetPlayerBattlepassDB(obj0.PlayerId);
    if (playerBattlepassDb != null)
    {
      obj0.Battlepass = playerBattlepassDb;
    }
    else
    {
      if (DaoManagerSQL.CreatePlayerBattlepassDB(obj0.PlayerId))
        return;
      CLogger.Print("There was an error when creating Player Battlepass!", LoggerType.Warning, (Exception) null);
    }
  }

  public static void LoadPlayerCompetitive(Account Player)
  {
    PlayerCompetitive playerCompetitiveDb = DaoManagerSQL.GetPlayerCompetitiveDB(Player.PlayerId);
    if (playerCompetitiveDb != null)
    {
      Player.Competitive = playerCompetitiveDb;
    }
    else
    {
      if (DaoManagerSQL.CreatePlayerCompetitiveDB(Player.PlayerId))
        return;
      CLogger.Print("There was an error when creating Player Competitive!", LoggerType.Warning, (Exception) null);
    }
  }

  public static void LoadPlayerBonus(Account Player)
  {
    PlayerBonus playerBonusDb = DaoManagerSQL.GetPlayerBonusDB(Player.PlayerId);
    if (playerBonusDb != null)
    {
      Player.Bonus = playerBonusDb;
    }
    else
    {
      if (DaoManagerSQL.CreatePlayerBonusDB(Player.PlayerId))
        return;
      CLogger.Print("There was an error when creating Player Bonus!", LoggerType.Warning, (Exception) null);
    }
  }

  public static void LoadPlayerFriend(Account Player, [In] bool obj1)
  {
    List<FriendModel> playerFriendsDb = DaoManagerSQL.GetPlayerFriendsDB(Player.PlayerId);
    if (playerFriendsDb.Count <= 0)
      return;
    Player.Friend.Friends = playerFriendsDb;
    if (!obj1)
      return;
    AccountManager.GetFriendlyAccounts(Player.Friend);
  }

  public static void LoadPlayerEvent(Account Player)
  {
    PlayerEvent playerEventDb = DaoManagerSQL.GetPlayerEventDB(Player.PlayerId);
    if (playerEventDb != null)
    {
      Player.Event = playerEventDb;
    }
    else
    {
      if (DaoManagerSQL.CreatePlayerEventDB(Player.PlayerId))
        return;
      CLogger.Print("There was an error when creating Player Event!", LoggerType.Warning, (Exception) null);
    }
  }

  public static void LoadPlayerConfig(Account Player)
  {
    PlayerConfig playerConfigDb = DaoManagerSQL.GetPlayerConfigDB(Player.PlayerId);
    if (playerConfigDb != null)
    {
      Player.Config = playerConfigDb;
    }
    else
    {
      if (DaoManagerSQL.CreatePlayerConfigDB(Player.PlayerId))
        return;
      CLogger.Print("There was an error when creating Player Config!", LoggerType.Warning, (Exception) null);
    }
  }

  public static void LoadPlayerQuickstarts(Account Player)
  {
    List<QuickstartModel> playerQuickstartsDb = DaoManagerSQL.GetPlayerQuickstartsDB(Player.PlayerId);
    if (playerQuickstartsDb.Count > 0)
    {
      Player.Quickstart.Quickjoins = playerQuickstartsDb;
    }
    else
    {
      if (DaoManagerSQL.CreatePlayerQuickstartsDB(Player.PlayerId))
        return;
      CLogger.Print("There was an error when creating Player Quickstarts!", LoggerType.Warning, (Exception) null);
    }
  }

  public static void LoadPlayerReport(Account Player)
  {
    PlayerReport playerReportDb = DaoManagerSQL.GetPlayerReportDB(Player.PlayerId);
    if (playerReportDb != null)
    {
      Player.Report = playerReportDb;
    }
    else
    {
      if (DaoManagerSQL.CreatePlayerReportDB(Player.PlayerId))
        return;
      CLogger.Print("There was an error when creating Player Report!", LoggerType.Warning, (Exception) null);
    }
  }

  public static int GetKillScore([In] KillingMessage obj0)
  {
    int killScore = 0;
    switch (obj0)
    {
      case KillingMessage.PiercingShot:
      case KillingMessage.MassKill:
        killScore += 6;
        goto case KillingMessage.Suicide;
      case KillingMessage.ChainStopper:
        killScore += 8;
        goto case KillingMessage.Suicide;
      case KillingMessage.Headshot:
        killScore += 10;
        goto case KillingMessage.Suicide;
      case KillingMessage.ChainHeadshot:
        killScore += 14;
        goto case KillingMessage.Suicide;
      case KillingMessage.ChainSlugger:
        killScore += 6;
        goto case KillingMessage.Suicide;
      case KillingMessage.Suicide:
        return killScore;
      case KillingMessage.ObjectDefense:
        killScore += 7;
        goto case KillingMessage.Suicide;
      default:
        killScore += 5;
        goto case KillingMessage.Suicide;
    }
  }

  private static ClassType smethod_1(ClassType Player)
  {
    switch (Player)
    {
      case ClassType.DualHandGun:
        return ClassType.HandGun;
      case ClassType.DualKnife:
      case ClassType.Knuckle:
        return ClassType.Knife;
      case ClassType.DualSMG:
        return ClassType.SMG;
      case ClassType.DualShotgun:
        return ClassType.Shotgun;
      default:
        return Player;
    }
  }

  public static TeamEnum GetWinnerTeam(RoomModel Player)
  {
    if (Player == null)
      return TeamEnum.TEAM_DRAW;
    TeamEnum winnerTeam = TeamEnum.TEAM_DRAW;
    if (Player.RoomType != RoomCondition.Bomb && Player.RoomType != RoomCondition.Destroy && Player.RoomType != RoomCondition.Annihilation && Player.RoomType != RoomCondition.Defense && Player.RoomType != RoomCondition.Destroy)
    {
      if (Player.IsDinoMode("DE"))
      {
        if (Player.CTDino == Player.FRDino)
          winnerTeam = TeamEnum.TEAM_DRAW;
        else if (Player.CTDino > Player.FRDino)
          winnerTeam = TeamEnum.CT_TEAM;
        else if (Player.CTDino < Player.FRDino)
          winnerTeam = TeamEnum.FR_TEAM;
      }
      else if (Player.CTKills == Player.FRKills)
        winnerTeam = TeamEnum.TEAM_DRAW;
      else if (Player.CTKills > Player.FRKills)
        winnerTeam = TeamEnum.CT_TEAM;
      else if (Player.CTKills < Player.FRKills)
        winnerTeam = TeamEnum.FR_TEAM;
    }
    else if (Player.CTRounds == Player.FRRounds)
      winnerTeam = TeamEnum.TEAM_DRAW;
    else if (Player.CTRounds > Player.FRRounds)
      winnerTeam = TeamEnum.CT_TEAM;
    else if (Player.CTRounds < Player.FRRounds)
      winnerTeam = TeamEnum.FR_TEAM;
    return winnerTeam;
  }

  public static TeamEnum GetWinnerTeam(RoomModel Player, [In] int obj1, [In] int obj2)
  {
    if (Player == null)
      return TeamEnum.TEAM_DRAW;
    TeamEnum winnerTeam = TeamEnum.TEAM_DRAW;
    if (obj1 == 0)
      winnerTeam = TeamEnum.CT_TEAM;
    else if (obj2 == 0)
      winnerTeam = TeamEnum.FR_TEAM;
    return winnerTeam;
  }

  public static void UpdateMatchCount(
    bool classType_0,
    Account RedPlayers,
    int BluePlayers,
    [In] DBQuery obj3,
    [In] DBQuery obj4)
  {
    if (BluePlayers == 2)
    {
      obj3.AddQuery("match_draws", (object) ++RedPlayers.Statistic.Basic.MatchDraws);
      obj4.AddQuery("match_draws", (object) ++RedPlayers.Statistic.Season.MatchDraws);
    }
    else if (classType_0)
    {
      obj3.AddQuery("match_wins", (object) ++RedPlayers.Statistic.Basic.MatchWins);
      obj4.AddQuery("match_wins", (object) ++RedPlayers.Statistic.Season.MatchWins);
    }
    else
    {
      obj3.AddQuery("match_loses", (object) ++RedPlayers.Statistic.Basic.MatchLoses);
      obj4.AddQuery("match_loses", (object) ++RedPlayers.Statistic.Season.MatchLoses);
    }
    obj3.AddQuery("matches", (object) ++RedPlayers.Statistic.Basic.Matches);
    DBQuery dbQuery = obj3;
    StatisticTotal basic = RedPlayers.Statistic.Basic;
    int num = basic.get_TotalMatchesCount() + 1;
    basic.set_TotalMatchesCount(num);
    // ISSUE: variable of a boxed type
    __Boxed<int> local = (System.ValueType) num;
    dbQuery.AddQuery("total_matches", (object) local);
    obj4.AddQuery("matches", (object) ++RedPlayers.Statistic.Season.Matches);
    obj4.AddQuery("total_matches", (object) ++RedPlayers.Statistic.Season.TotalMatchesCount);
  }

  public static void UpdateDailyRecord(
    bool WonTheMatch,
    Account Player,
    int WinnerTeam,
    DBQuery TotalQuery)
  {
    if (WinnerTeam == 2)
      TotalQuery.AddQuery("match_draws", (object) ++Player.Statistic.Daily.MatchDraws);
    else if (WonTheMatch)
    {
      DBQuery dbQuery = TotalQuery;
      StatisticDaily daily = Player.Statistic.Daily;
      int num = daily.get_MatchWins() + 1;
      daily.set_MatchWins(num);
      // ISSUE: variable of a boxed type
      __Boxed<int> local = (System.ValueType) num;
      dbQuery.AddQuery("match_wins", (object) local);
    }
    else
    {
      DBQuery dbQuery = TotalQuery;
      StatisticDaily daily = Player.Statistic.Daily;
      int num = daily.get_MatchLoses() + 1;
      daily.set_MatchLoses(num);
      // ISSUE: variable of a boxed type
      __Boxed<int> local = (System.ValueType) num;
      dbQuery.AddQuery("match_loses", (object) local);
    }
    TotalQuery.AddQuery("matches", (object) ++Player.Statistic.Daily.Matches);
  }

  public static void UpdateMatchCountFFA(
    [In] RoomModel obj0,
    [In] Account obj1,
    [In] int obj2,
    [In] DBQuery obj3,
    DBQuery SeasonQuery)
  {
    int[] numArray = new int[18];
    for (int index = 0; index < numArray.Length; ++index)
    {
      SlotModel slot = obj0.Slots[index];
      numArray[index] = slot.PlayerId == 0L ? 0 : slot.AllKills;
    }
    int index1 = 0;
    for (int index2 = 0; index2 < numArray.Length; ++index2)
    {
      if (numArray[index2] > numArray[index1])
        index1 = index2;
    }
    if (numArray[index1] == obj2)
    {
      obj3.AddQuery("match_wins", (object) ++obj1.Statistic.Basic.MatchWins);
      SeasonQuery.AddQuery("match_wins", (object) ++obj1.Statistic.Season.MatchWins);
    }
    else
    {
      obj3.AddQuery("match_loses", (object) ++obj1.Statistic.Basic.MatchLoses);
      SeasonQuery.AddQuery("match_loses", (object) ++obj1.Statistic.Season.MatchLoses);
    }
    obj3.AddQuery("matches", (object) ++obj1.Statistic.Basic.Matches);
    DBQuery dbQuery = obj3;
    StatisticTotal basic = obj1.Statistic.Basic;
    int num = basic.get_TotalMatchesCount() + 1;
    basic.set_TotalMatchesCount(num);
    // ISSUE: variable of a boxed type
    __Boxed<int> local = (System.ValueType) num;
    dbQuery.AddQuery("total_matches", (object) local);
    SeasonQuery.AddQuery("matches", (object) ++obj1.Statistic.Season.Matches);
    SeasonQuery.AddQuery("total_matches", (object) ++obj1.Statistic.Season.TotalMatchesCount);
  }

  public static void UpdateMatchDailyRecordFFA(
    RoomModel Room,
    Account Player,
    int SlotWin,
    DBQuery TotalQuery)
  {
    int[] numArray = new int[18];
    for (int index = 0; index < numArray.Length; ++index)
    {
      SlotModel slot = Room.Slots[index];
      numArray[index] = slot.PlayerId == 0L ? 0 : slot.AllKills;
    }
    int index1 = 0;
    for (int index2 = 0; index2 < numArray.Length; ++index2)
    {
      if (numArray[index2] > numArray[index1])
        index1 = index2;
    }
    if (numArray[index1] == SlotWin)
    {
      DBQuery dbQuery = TotalQuery;
      StatisticDaily daily = Player.Statistic.Daily;
      int num = daily.get_MatchWins() + 1;
      daily.set_MatchWins(num);
      // ISSUE: variable of a boxed type
      __Boxed<int> local = (System.ValueType) num;
      dbQuery.AddQuery("match_wins", (object) local);
    }
    else
    {
      DBQuery dbQuery = TotalQuery;
      StatisticDaily daily = Player.Statistic.Daily;
      int num = daily.get_MatchLoses() + 1;
      daily.set_MatchLoses(num);
      // ISSUE: variable of a boxed type
      __Boxed<int> local = (System.ValueType) num;
      dbQuery.AddQuery("match_loses", (object) local);
    }
    TotalQuery.AddQuery("matches", (object) ++Player.Statistic.Daily.Matches);
  }

  public static void UpdateWeaponRecord([In] Account obj0, [In] SlotModel obj1, [In] DBQuery obj2)
  {
    StatisticWeapon weapon = obj0.Statistic.Weapon;
    if (obj1.AR[0] > 0)
      obj2.AddQuery("assault_rifle_kills", (object) ++weapon.AssaultKills);
    if (obj1.AR[1] > 0)
      obj2.AddQuery("assault_rifle_deaths", (object) ++weapon.AssaultDeaths);
    if (obj1.SMG[0] > 0)
      obj2.AddQuery("sub_machine_gun_kills", (object) ++weapon.SmgKills);
    if (obj1.SMG[1] > 0)
      obj2.AddQuery("sub_machine_gun_deaths", (object) ++weapon.SmgDeaths);
    if (obj1.SR[0] > 0)
      obj2.AddQuery("sniper_rifle_kills", (object) ++weapon.SniperKills);
    if (obj1.SR[1] > 0)
      obj2.AddQuery("sniper_rifle_deaths", (object) ++weapon.SniperDeaths);
    if (obj1.SG[0] > 0)
      obj2.AddQuery("shot_gun_kills", (object) ++weapon.ShotgunKills);
    if (obj1.SG[1] > 0)
      obj2.AddQuery("shot_gun_deaths", (object) ++weapon.ShotgunDeaths);
    if (obj1.MG[0] > 0)
      obj2.AddQuery("machine_gun_kills", (object) ++weapon.MachinegunKills);
    if (obj1.MG[1] > 0)
      obj2.AddQuery("machine_gun_deaths", (object) ++weapon.MachinegunDeaths);
    if (obj1.SHD[0] > 0)
      obj2.AddQuery("shield_kills", (object) ++weapon.ShieldKills);
    if (obj1.SHD[1] <= 0)
      return;
    obj2.AddQuery("shield_deaths", (object) ++weapon.ShieldDeaths);
  }

  public static void GenerateMissionAwards([In] Account obj0, [In] DBQuery obj1)
  {
    try
    {
      PlayerMissions mission = obj0.Mission;
      int actualMission = mission.ActualMission;
      int currentMissionId = mission.GetCurrentMissionId();
      int currentCard = mission.GetCurrentCard();
      if (currentMissionId <= 0 || mission.SelectedCard)
        return;
      int num1 = 0;
      int num2 = 0;
      byte[] currentMissionList = mission.GetCurrentMissionList();
      foreach (MissionCardModel card in MissionCardRAW.GetCards(currentMissionId, -1))
      {
        if ((int) currentMissionList[card.ArrayIdx] >= card.MissionLimit)
        {
          ++num2;
          if (card.CardBasicId == currentCard)
            ++num1;
        }
      }
      if (num2 >= 40)
      {
        int masterMedal = obj0.MasterMedal;
        int ribbon = obj0.Ribbon;
        int medal = obj0.Medal;
        int ensign = obj0.Ensign;
        MissionCardAwards award1 = MissionCardRAW.GetAward(currentMissionId, currentCard);
        if (award1 != null)
        {
          obj0.Ribbon += award1.Ribbon;
          obj0.Medal += award1.Medal;
          obj0.Ensign += award1.Ensign;
          obj0.Gold += award1.get_Gold();
          obj0.Exp += award1.get_Exp();
        }
        MissionAwards award2 = MissionAwardXML.GetAward(currentMissionId);
        if (award2 != null)
        {
          obj0.MasterMedal += award2.MasterMedal;
          obj0.Exp += award2.Exp;
          obj0.Gold += award2.Gold;
        }
        List<ItemsModel> missionAwards = MissionCardRAW.GetMissionAwards(currentMissionId);
        if (missionAwards.Count > 0)
          obj0.SendPacket((GameServerPacket) new PROTOCOL_LOBBY_CHATTING_ACK(0, obj0, missionAwards));
        obj0.SendPacket((GameServerPacket) new PROTOCOL_BASE_QUEST_CHANGE_ACK(273U, 4, obj0));
        if (obj0.Ribbon != ribbon)
          obj1.AddQuery("ribbon", (object) obj0.Ribbon);
        if (obj0.Ensign != ensign)
          obj1.AddQuery("ensign", (object) obj0.Ensign);
        if (obj0.Medal != medal)
          obj1.AddQuery("medal", (object) obj0.Medal);
        if (obj0.MasterMedal != masterMedal)
          obj1.AddQuery("master_medal", (object) obj0.MasterMedal);
        obj1.AddQuery($"mission_id{actualMission + 1}", (object) 0);
        ComDiv.UpdateDB("player_missions", "owner_id", (object) obj0.PlayerId, new string[2]
        {
          $"card{actualMission + 1}",
          $"mission{actualMission + 1}_raw"
        }, new object[2]{ (object) 0, (object) new byte[0] });
        switch (actualMission)
        {
          case 0:
            mission.Mission1 = 0;
            mission.Card1 = 0;
            mission.List1 = new byte[40];
            break;
          case 1:
            mission.Mission2 = 0;
            mission.Card2 = 0;
            mission.List2 = new byte[40];
            break;
          case 2:
            mission.Mission3 = 0;
            mission.Card3 = 0;
            mission.List3 = new byte[40];
            break;
          case 3:
            mission.Mission4 = 0;
            mission.Card3 = 0;
            mission.List4 = new byte[40];
            if (obj0.Event == null)
              break;
            obj0.Event.LastQuestFinish = 1;
            ComDiv.UpdateDB("player_events", "last_quest_finish", (object) 1, "owner_id", (object) obj0.PlayerId);
            break;
        }
      }
      else
      {
        if (num1 != 4 || mission.SelectedCard)
          return;
        MissionCardAwards award = MissionCardRAW.GetAward(currentMissionId, currentCard);
        if (award != null)
        {
          int ribbon = obj0.Ribbon;
          int medal = obj0.Medal;
          int ensign = obj0.Ensign;
          obj0.Ribbon += award.Ribbon;
          obj0.Medal += award.Medal;
          obj0.Ensign += award.Ensign;
          obj0.Gold += award.get_Gold();
          obj0.Exp += award.get_Exp();
          if (obj0.Ribbon != ribbon)
            obj1.AddQuery("ribbon", (object) obj0.Ribbon);
          if (obj0.Ensign != ensign)
            obj1.AddQuery("ensign", (object) obj0.Ensign);
          if (obj0.Medal != medal)
            obj1.AddQuery("medal", (object) obj0.Medal);
        }
        mission.SelectedCard = true;
        obj0.SendPacket((GameServerPacket) new PROTOCOL_BASE_QUEST_CHANGE_ACK(1U, 1, obj0));
      }
    }
    catch (Exception ex)
    {
      CLogger.Print("AllUtils.GenerateMissionAwards: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public static void ResetSlotInfo(RoomModel Player, SlotModel Slot, bool Query)
  {
    if (Slot.State < SlotState.LOAD)
      return;
    Player.ChangeSlotState(Slot, SlotState.NORMAL, Query);
    Slot.ResetSlot();
  }

  public static void EndMatchMission(
    RoomModel Player,
    Account query,
    [In] SlotModel obj2,
    [In] TeamEnum obj3)
  {
    if (obj3 == TeamEnum.TEAM_DRAW)
      return;
    AllUtils.CompleteMission(Player, query, obj2, obj2.Team == obj3 ? MissionType.WIN : MissionType.DEFEAT, 0);
  }

  public static void VotekickResult([In] RoomModel obj0)
  {
    VoteKickModel voteKick = obj0.VoteKick;
    if (voteKick != null)
    {
      int inGamePlayers = voteKick.GetInGamePlayers();
      if (voteKick.Accept > voteKick.Denie && voteKick.Enemies > 0 && voteKick.Allies > 0 && voteKick.Votes.Count >= inGamePlayers / 2)
      {
        Account playerBySlot = obj0.GetPlayerBySlot(voteKick.VictimIdx);
        if (playerBySlot != null)
        {
          playerBySlot.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_NOTIFY_HACK_USER_ACK());
          obj0.KickedPlayersVote.Add(playerBySlot.PlayerId);
          obj0.RemovePlayer(playerBySlot, true, 2);
        }
      }
      uint roomModel_1 = 0;
      if (voteKick.Allies == 0)
        roomModel_1 = 2147488001U;
      else if (voteKick.Enemies == 0)
        roomModel_1 = 2147488002U;
      else if (voteKick.Denie < voteKick.Accept || voteKick.Votes.Count < inGamePlayers / 2)
        roomModel_1 = 2147488000U /*0x80001100*/;
      using (PROTOCOL_BATTLE_NOTIFY_KICKVOTE_RESULT_ACK kickvoteResultAck = (PROTOCOL_BATTLE_NOTIFY_KICKVOTE_RESULT_ACK) new PROTOCOL_BATTLE_PRESTARTBATTLE_ACK(roomModel_1, voteKick))
        obj0.SendPacketToPlayers((GameServerPacket) kickvoteResultAck, SlotState.BATTLE, 0);
    }
    obj0.VoteKick = (VoteKickModel) null;
  }

  public static void ResetBattleInfo(RoomModel Room)
  {
    foreach (SlotModel slot in Room.Slots)
    {
      if (slot.PlayerId > 0L && slot.State >= SlotState.LOAD)
      {
        slot.State = SlotState.NORMAL;
        slot.ResetSlot();
      }
      Room.CheckGhostSlot(slot);
    }
    Room.PreMatchCD = false;
    Room.BlockedClan = false;
    Room.SwapRound = false;
    Room.Rounds = 1;
    Room.SpawnsCount = 0;
    Room.FRKills = 0;
    Room.FRAssists = 0;
    Room.FRDeaths = 0;
    Room.CTKills = 0;
    Room.CTAssists = 0;
    Room.CTDeaths = 0;
    Room.FRDino = 0;
    Room.CTDino = 0;
    Room.FRRounds = 0;
    Room.CTRounds = 0;
    Room.BattleStart = new DateTime();
    Room.TimeRoom = 0U;
    Room.Bar1 = 0;
    Room.Bar2 = 0;
    Room.IngameAiLevel = (byte) 0;
    Room.State = RoomState.READY;
    Room.UpdateRoomInfo();
    Room.VoteKick = (VoteKickModel) null;
    Room.UdpServer = (Synchronize) null;
    if (Room.RoundTime.IsTimer())
      Room.RoundTime.StopJob();
    if (Room.VoteTime.IsTimer())
      Room.VoteTime.StopJob();
    if (Room.BombTime.IsTimer())
      Room.BombTime.StopJob();
    Room.UpdateSlotsInfo();
  }

  public static List<int> GetDinossaurs([In] RoomModel obj0, bool Player, int Slot)
  {
    List<int> dinossaurs = new List<int>();
    if (obj0.IsDinoMode(""))
    {
      TeamEnum teamEnum = obj0.Rounds == 1 ? TeamEnum.FR_TEAM : TeamEnum.CT_TEAM;
      foreach (int team in obj0.GetTeamArray(teamEnum))
      {
        SlotModel slot = obj0.Slots[team];
        if (slot.State == SlotState.BATTLE && !slot.SpecGM)
          dinossaurs.Add(team);
      }
      if (((obj0.TRex == -1 ? 1 : (obj0.Slots[obj0.TRex].State <= SlotState.BATTLE_LOAD ? 1 : 0)) | (Player ? 1 : 0)) != 0 && dinossaurs.Count > 1 && obj0.IsDinoMode("DE"))
      {
        if (Slot >= 0 && dinossaurs.Contains(Slot))
          obj0.TRex = Slot;
        else if (Slot == -2)
          obj0.TRex = dinossaurs[new Random().Next(0, dinossaurs.Count)];
      }
    }
    return dinossaurs;
  }

  public static void BattleEndPlayersCount(RoomModel Room, [In] bool obj1)
  {
    if (Room == null | obj1 || !Room.IsPreparing())
      return;
    int num1 = 0;
    int num2 = 0;
    int num3 = 0;
    int num4 = 0;
    foreach (SlotModel slot in Room.Slots)
    {
      if (slot.State == SlotState.BATTLE)
      {
        if (slot.Team == TeamEnum.FR_TEAM)
          ++num2;
        else
          ++num1;
      }
      else if (slot.State >= SlotState.LOAD)
      {
        if (slot.Team == TeamEnum.FR_TEAM)
          ++num4;
        else
          ++num3;
      }
    }
    if ((num2 != 0 && num1 != 0 || Room.State != RoomState.BATTLE) && (num4 != 0 && num3 != 0 || Room.State > RoomState.PRE_BATTLE))
      return;
    AllUtils.EndBattle(Room, obj1);
  }

  public static void EndBattle(RoomModel Room) => AllUtils.EndBattle(Room, Room.IsBotMode());

  public static void EndBattle([In] RoomModel obj0, bool ForceNewTRex)
  {
    AllUtils.EndBattle(obj0, ForceNewTRex, AllUtils.GetWinnerTeam(obj0));
  }

  public static void EndBattleNoPoints(RoomModel Room)
  {
    List<Account> allPlayers = Room.GetAllPlayers(SlotState.READY, 1);
    if (allPlayers.Count > 0)
    {
      int Room1;
      int int_4;
      byte[] byte_1;
      AllUtils.GetBattleResult(Room, ref Room1, ref int_4, ref byte_1);
      bool bool_1 = Room.IsBotMode();
      foreach (Account Room2 in allPlayers)
      {
        Room2.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_ENDBATTLE_ACK(Room2, TeamEnum.TEAM_DRAW, int_4, Room1, bool_1, byte_1));
        AllUtils.UpdateSeasonPass(Room2);
      }
    }
    AllUtils.ResetBattleInfo(Room);
  }

  public static void EndBattle([In] RoomModel obj0, bool IsBotMode, [In] TeamEnum obj2)
  {
    List<Account> allPlayers = obj0.GetAllPlayers(SlotState.READY, 1);
    if (allPlayers.Count > 0)
    {
      obj0.CalculateResult(obj2, IsBotMode);
      int Room1;
      int int_4;
      byte[] byte_1;
      AllUtils.GetBattleResult(obj0, ref Room1, ref int_4, ref byte_1);
      foreach (Account Room2 in allPlayers)
      {
        Room2.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_ENDBATTLE_ACK(Room2, obj2, int_4, Room1, IsBotMode, byte_1));
        AllUtils.UpdateSeasonPass(Room2);
      }
    }
    AllUtils.ResetBattleInfo(obj0);
  }

  public static void BattleEndRound(
    [In] RoomModel obj0,
    TeamEnum isBotMode,
    bool WinnerTeam,
    [In] FragInfos obj3,
    [In] SlotModel obj4)
  {
    // ISSUE: variable of a compiler-generated type
    AllUtils.Class9 class9 = (AllUtils.Class9) new GameSync();
    // ISSUE: reference to a compiler-generated field
    class9.roomModel_0 = obj0;
    // ISSUE: reference to a compiler-generated field
    class9.teamEnum_0 = isBotMode;
    // ISSUE: reference to a compiler-generated field
    class9.bool_0 = WinnerTeam;
    // ISSUE: reference to a compiler-generated field
    class9.fragInfos_0 = obj3;
    // ISSUE: reference to a compiler-generated field
    class9.slotModel_0 = obj4;
    // ISSUE: reference to a compiler-generated field
    class9.roomModel_0.MatchEndTime.StartJob(1250, new TimerCallback(((GameSync) class9).method_0));
  }

  private static void smethod_2(
    RoomModel Room,
    TeamEnum Winner,
    bool ForceRestart,
    FragInfos Kills,
    SlotModel Killer)
  {
    int roundsByMask = Room.GetRoundsByMask();
    if (Room.FRRounds < roundsByMask && Room.CTRounds < roundsByMask)
    {
      if (!(!Room.ActiveC4 | ForceRestart))
        return;
      Room.StopBomb();
      Room.ChangeRounds();
      SendClanInfo.SendUDPRoundSync(Room);
      using (PROTOCOL_BATTLE_WINNING_CAM_ACK Packet = (PROTOCOL_BATTLE_WINNING_CAM_ACK) new PROTOCOL_GMCHAT_SEND_CHAT_ACK(Kills))
      {
        using (PROTOCOL_BATTLE_MISSION_ROUND_END_ACK PlayerId = (PROTOCOL_BATTLE_MISSION_ROUND_END_ACK) new PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK(Room, Winner, RoundEndType.AllDeath))
          Room.SendPacketToPlayers((GameServerPacket) Packet, (GameServerPacket) PlayerId, SlotState.BATTLE, 0);
      }
      Room.RoundRestart();
    }
    else
    {
      Room.StopBomb();
      using (PROTOCOL_BATTLE_WINNING_CAM_ACK Packet = (PROTOCOL_BATTLE_WINNING_CAM_ACK) new PROTOCOL_GMCHAT_SEND_CHAT_ACK(Kills))
      {
        using (PROTOCOL_BATTLE_MISSION_ROUND_END_ACK PlayerId = (PROTOCOL_BATTLE_MISSION_ROUND_END_ACK) new PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK(Room, Winner, RoundEndType.AllDeath))
          Room.SendPacketToPlayers((GameServerPacket) Packet, (GameServerPacket) PlayerId, SlotState.BATTLE, 0);
      }
      AllUtils.EndBattle(Room, Room.IsBotMode(), Winner);
    }
  }

  public static void BattleEndRound(
    RoomModel roomModel_0,
    TeamEnum teamEnum_0,
    RoundEndType bool_0)
  {
    using (PROTOCOL_BATTLE_MISSION_ROUND_END_ACK missionRoundEndAck = (PROTOCOL_BATTLE_MISSION_ROUND_END_ACK) new PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK(roomModel_0, teamEnum_0, bool_0))
      roomModel_0.SendPacketToPlayers((GameServerPacket) missionRoundEndAck, SlotState.BATTLE, 0);
    roomModel_0.StopBomb();
    int roundsByMask = roomModel_0.GetRoundsByMask();
    if (roomModel_0.FRRounds < roundsByMask && roomModel_0.CTRounds < roundsByMask)
    {
      roomModel_0.ChangeRounds();
      SendClanInfo.SendUDPRoundSync(roomModel_0);
      roomModel_0.RoundRestart();
    }
    else
      AllUtils.EndBattle(roomModel_0, roomModel_0.IsBotMode(), teamEnum_0);
  }

  public static int AddFriend([In] Account obj0, [In] Account obj1, [In] int obj2)
  {
    if (obj0 != null)
    {
      if (obj1 != null)
      {
        try
        {
          FriendModel friend = obj0.Friend.GetFriend(obj1.PlayerId);
          if (friend == null)
          {
            using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
            {
              NpgsqlCommand command = npgsqlConnection.CreateCommand();
              ((DbConnection) npgsqlConnection).Open();
              ((DbCommand) command).CommandType = CommandType.Text;
              command.Parameters.AddWithValue("@friend", (object) obj1.PlayerId);
              command.Parameters.AddWithValue("@owner", (object) obj0.PlayerId);
              command.Parameters.AddWithValue("@state", (object) obj2);
              ((DbCommand) command).CommandText = "INSERT INTO player_friends (id, owner_id, state) VALUES (@friend, @owner, @state)";
              ((DbCommand) command).ExecuteNonQuery();
              ((Component) command).Dispose();
              ((Component) npgsqlConnection).Dispose();
              ((DbConnection) npgsqlConnection).Close();
            }
            lock (obj0.Friend.Friends)
            {
              FriendModel friendModel = new FriendModel(obj1.PlayerId, obj1.Rank, obj1.NickColor, obj1.Nickname, obj1.IsOnline, obj1.Status)
              {
                State = obj2,
                Removed = false
              };
              obj0.Friend.Friends.Add(friendModel);
              UpdateChannel.Load(obj0, friendModel, 0);
            }
            return 0;
          }
          if (friend.Removed)
          {
            friend.Removed = false;
            DaoManagerSQL.UpdatePlayerFriendBlock(obj0.PlayerId, friend);
            UpdateChannel.Load(obj0, friend, 1);
          }
          return 1;
        }
        catch (Exception ex)
        {
          CLogger.Print("AllUtils.AddFriend: " + ex.Message, LoggerType.Error, ex);
          return -1;
        }
      }
    }
    return -1;
  }

  public static void SyncPlayerToFriends([In] Account obj0, bool Winner)
  {
    if (obj0 == null || obj0.Friend.Friends.Count == 0)
      return;
    PlayerInfo playerInfo = new PlayerInfo(obj0.PlayerId, obj0.Rank, obj0.NickColor, obj0.Nickname, obj0.IsOnline, obj0.Status);
    for (int index = 0; index < obj0.Friend.Friends.Count; ++index)
    {
      FriendModel friend1 = obj0.Friend.Friends[index];
      if (Winner || friend1.State == 0 && !friend1.Removed)
      {
        Account account = ClanManager.GetAccount(friend1.get_PlayerId(), 287);
        if (account != null)
        {
          int num = -1;
          FriendModel friend2 = account.Friend.GetFriend(obj0.PlayerId, ref num);
          if (friend2 != null)
          {
            friend2.Info = playerInfo;
            account.SendPacket((GameServerPacket) new PROTOCOL_AUTH_FRIEND_INSERT_ACK(FriendChangeState.Update, friend2, num), false);
          }
        }
      }
    }
  }

  public static void SyncPlayerToClanMembers(Account Owner)
  {
    if (Owner == null || Owner.ClanId <= 0)
      return;
    using (PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK Packet = (PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK) new PROTOCOL_CS_MEMBER_INFO_INSERT_ACK(Owner))
      ClanManager.SendPacket((GameServerPacket) Packet, Owner.ClanId, Owner.PlayerId, true, true);
  }

  public static void UpdateSlotEquips([In] Account obj0)
  {
    RoomModel room = obj0.Room;
    if (room == null)
      return;
    AllUtils.UpdateSlotEquips(obj0, room);
  }

  public static void UpdateSlotEquips([In] Account obj0, [In] RoomModel obj1)
  {
    SlotModel slotModel;
    if (obj1.GetSlot(obj0.SlotId, ref slotModel))
      slotModel.Equipment = obj0.Equipment;
    obj1.UpdateSlotsInfo();
  }

  public static int GetSlotsFlag([In] RoomModel obj0, bool all, [In] bool obj2)
  {
    if (obj0 == null)
      return 0;
    int slotsFlag = 0;
    foreach (SlotModel slot in obj0.Slots)
    {
      if (slot.State >= SlotState.LOAD && (obj2 && slot.MissionsCompleted || !obj2 && (!all || !slot.Spectator)))
        slotsFlag += slot.Flag;
    }
    return slotsFlag;
  }

  public static void GetBattleResult(
    RoomModel Player,
    ref int Room,
    [In] ref int obj2,
    [In] ref byte[] obj3)
  {
    Room = 0;
    obj2 = 0;
    obj3 = new byte[306];
    if (Player == null)
      return;
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      foreach (SlotModel slot in Player.Slots)
      {
        if (slot.State >= SlotState.LOAD)
        {
          int flag = slot.Flag;
          if (slot.MissionsCompleted)
            Room += flag;
          obj2 += flag;
        }
      }
      foreach (SlotModel slot in Player.Slots)
        syncServerPacket.WriteH((ushort) slot.Exp);
      foreach (SlotModel slot in Player.Slots)
        syncServerPacket.WriteH((ushort) slot.Gold);
      foreach (SlotModel slot in Player.Slots)
        syncServerPacket.WriteC((byte) slot.BonusFlags);
      foreach (SlotModel slot in Player.Slots)
      {
        syncServerPacket.WriteH((ushort) slot.BonusCafeExp);
        syncServerPacket.WriteH((ushort) slot.BonusItemExp);
        syncServerPacket.WriteH((ushort) slot.BonusEventExp);
      }
      foreach (SlotModel slot in Player.Slots)
      {
        syncServerPacket.WriteH((ushort) slot.BonusCafePoint);
        syncServerPacket.WriteH((ushort) slot.BonusItemPoint);
        syncServerPacket.WriteH((ushort) slot.BonusEventPoint);
      }
      obj3 = syncServerPacket.ToArray();
    }
  }

  public static bool DiscountPlayerItems([In] SlotModel obj0, [In] Account obj1)
  {
    try
    {
      bool flag1 = false;
      bool flag2 = false;
      uint uint32 = Convert.ToUInt32(DateTimeUtil.Now("yyMMddHHmm"));
      List<ItemsModel> itemsModelList = new List<ItemsModel>();
      List<object> objectList = new List<object>();
      int bonuses = obj1.Bonus != null ? obj1.Bonus.Bonuses : 0;
      int freePass = obj1.Bonus != null ? obj1.Bonus.FreePass : 0;
      lock (obj1.Inventory.Items)
      {
        for (int index = 0; index < obj1.Inventory.Items.Count; ++index)
        {
          ItemsModel itemsModel = obj1.Inventory.Items[index];
          if (itemsModel.Equip == ItemEquipType.Durable && obj0.ItemUsages.Contains(itemsModel.Id) && !obj0.SpecGM)
          {
            if (itemsModel.Count <= uint32 && (itemsModel.Id == 800216 || itemsModel.Id == 2700013 || itemsModel.Id == 800169))
              DaoManagerSQL.DeletePlayerInventoryItem(itemsModel.ObjectId, obj1.PlayerId);
            if (--itemsModel.Count < 1U)
            {
              objectList.Add((object) itemsModel.ObjectId);
              obj1.Inventory.Items.RemoveAt(index--);
            }
            else
            {
              itemsModelList.Add(itemsModel);
              ComDiv.UpdateDB("player_items", "count", (object) (long) itemsModel.Count, "object_id", (object) itemsModel.ObjectId, "owner_id", (object) obj1.PlayerId);
            }
          }
          else if (itemsModel.Count <= uint32 && itemsModel.Equip == ItemEquipType.Temporary)
          {
            if (itemsModel.Category == ItemCategory.Coupon)
            {
              if (obj1.Bonus != null)
              {
                if (!obj1.Bonus.RemoveBonuses(itemsModel.Id))
                {
                  if (itemsModel.Id == 1600014)
                  {
                    ComDiv.UpdateDB("player_bonus", "crosshair_color", (object) 4, "owner_id", (object) obj1.PlayerId);
                    obj1.Bonus.CrosshairColor = 4;
                    flag1 = true;
                  }
                  else if (itemsModel.Id == 1600006)
                  {
                    ComDiv.UpdateDB("accounts", "nick_color", (object) 0, "player_id", (object) obj1.PlayerId);
                    obj1.NickColor = 0;
                    if (obj1.Room != null)
                    {
                      using (PROTOCOL_ROOM_GET_COLOR_NICK_ACK Player = (PROTOCOL_ROOM_GET_COLOR_NICK_ACK) new PROTOCOL_ROOM_GET_NICK_OUTLINE_COLOR_ACK(obj1.SlotId, obj1.NickColor))
                        obj1.Room.SendPacketToPlayers((GameServerPacket) Player);
                      obj1.Room.UpdateSlotsInfo();
                    }
                    flag1 = true;
                  }
                  else if (itemsModel.Id == 1600009)
                  {
                    ComDiv.UpdateDB("player_bonus", "fake_rank", (object) 55, "owner_id", (object) obj1.PlayerId);
                    obj1.Bonus.FakeRank = 55;
                    if (obj1.Room != null)
                    {
                      using (PROTOCOL_ROOM_GET_RANK_ACK Player = (PROTOCOL_ROOM_GET_RANK_ACK) new PROTOCOL_ROOM_GET_SLOTINFO_ACK(obj1.SlotId, obj1.Rank))
                        obj1.Room.SendPacketToPlayers((GameServerPacket) Player);
                      obj1.Room.UpdateSlotsInfo();
                    }
                    flag1 = true;
                  }
                  else if (itemsModel.Id == 1600010)
                  {
                    ComDiv.UpdateDB("player_bonus", "fake_nick", (object) "", "owner_id", (object) obj1.PlayerId);
                    ComDiv.UpdateDB("accounts", "nickname", (object) obj1.Bonus.FakeNick, "player_id", (object) obj1.PlayerId);
                    obj1.Nickname = obj1.Bonus.FakeNick;
                    obj1.Bonus.FakeNick = "";
                    if (obj1.Room != null)
                    {
                      using (PROTOCOL_ROOM_GET_NICKNAME_ACK Player = (PROTOCOL_ROOM_GET_NICKNAME_ACK) new PROTOCOL_ROOM_GET_RANK_ACK(obj1.SlotId, obj1.Nickname))
                        obj1.Room.SendPacketToPlayers((GameServerPacket) Player);
                      obj1.Room.UpdateSlotsInfo();
                    }
                    flag1 = true;
                  }
                  else if (itemsModel.Id == 1600187)
                  {
                    ComDiv.UpdateDB("player_bonus", "muzzle_color", (object) 0, "owner_id", (object) obj1.PlayerId);
                    obj1.Bonus.MuzzleColor = 0;
                    if (obj1.Room != null)
                    {
                      using (PROTOCOL_ROOM_GET_COLOR_MUZZLE_FLASH_ACK Player = (PROTOCOL_ROOM_GET_COLOR_MUZZLE_FLASH_ACK) new PROTOCOL_ROOM_GET_NAMECARD_ACK(obj1.SlotId, obj1.Bonus.MuzzleColor))
                        obj1.Room.SendPacketToPlayers((GameServerPacket) Player);
                    }
                    flag1 = true;
                  }
                  else if (itemsModel.Id == 1600205)
                  {
                    ComDiv.UpdateDB("player_bonus", "nick_border_color", (object) 0, "owner_id", (object) obj1.PlayerId);
                    obj1.Bonus.NickBorderColor = 0;
                    if (obj1.Room != null)
                    {
                      using (PROTOCOL_ROOM_GET_NICK_OUTLINE_COLOR_ACK Player = (PROTOCOL_ROOM_GET_NICK_OUTLINE_COLOR_ACK) new PROTOCOL_ROOM_GET_NICKNAME_ACK(obj1.SlotId, obj1.Bonus.NickBorderColor))
                        obj1.Room.SendPacketToPlayers((GameServerPacket) Player);
                    }
                    flag1 = true;
                  }
                }
                CouponFlag couponEffect = CouponEffectXML.GetCouponEffect(itemsModel.Id);
                if (couponEffect != null && couponEffect.EffectFlag > (CouponEffects) 0 && obj1.Effects.HasFlag((Enum) couponEffect.EffectFlag))
                {
                  obj1.Effects -= couponEffect.EffectFlag;
                  flag2 = true;
                }
              }
              else
                continue;
            }
            objectList.Add((object) itemsModel.ObjectId);
            obj1.Inventory.Items.RemoveAt(index--);
          }
          else if (itemsModel.Count == 0U)
          {
            objectList.Add((object) itemsModel.ObjectId);
            obj1.Inventory.Items.RemoveAt(index--);
          }
        }
      }
      if (objectList.Count > 0)
      {
        for (int index = 0; index < objectList.Count; ++index)
        {
          ItemsModel itemsModel = obj1.Inventory.GetItem((long) objectList[index]);
          if (itemsModel != null && itemsModel.Category == ItemCategory.Character && ComDiv.GetIdStatics(itemsModel.Id, 1) == 6)
            AllUtils.smethod_3(obj1, itemsModel.Id);
          obj1.SendPacket((GameServerPacket) new PROTOCOL_AUTH_SHOP_GOODSLIST_ACK(1U, (long) objectList[index]));
        }
        ComDiv.DeleteDB("player_items", "object_id", objectList.ToArray(), "owner_id", (object) obj1.PlayerId);
      }
      objectList.Clear();
      if (obj1.Bonus != null && (obj1.Bonus.Bonuses != bonuses || obj1.Bonus.FreePass != freePass))
        DaoManagerSQL.UpdatePlayerBonus(obj1.PlayerId, obj1.Bonus.Bonuses, obj1.Bonus.FreePass);
      if (obj1.Effects < (CouponEffects) 0)
        obj1.Effects = (CouponEffects) 0;
      if (itemsModelList.Count > 0)
        obj1.SendPacket((GameServerPacket) new PROTOCOL_LOBBY_CHATTING_ACK(1, obj1, itemsModelList));
      itemsModelList.Clear();
      if (flag2)
        ComDiv.UpdateDB("accounts", "coupon_effect", (object) (long) obj1.Effects, "player_id", (object) obj1.PlayerId);
      if (flag1)
        obj1.SendPacket((GameServerPacket) new PROTOCOL_BASE_NEW_REWARD_POPUP_ACK(0, obj1));
      int num = ComDiv.CheckEquipedItems(obj1.Equipment, obj1.Inventory.Items, false);
      if (num > 0)
      {
        DBQuery valueTuple_0 = new DBQuery();
        if ((num & 2) == 2)
          ComDiv.UpdateWeapons(obj1.Equipment, valueTuple_0);
        if ((num & 1) == 1)
          ComDiv.UpdateChars(obj1.Equipment, valueTuple_0);
        if ((num & 3) == 3)
          ComDiv.UpdateItems(obj1.Equipment, valueTuple_0);
        ComDiv.UpdateDB("player_equipments", "owner_id", (object) obj1.PlayerId, valueTuple_0.GetTables(), valueTuple_0.GetValues());
        obj1.SendPacket((GameServerPacket) new PROTOCOL_SERVER_MESSAGE_ERROR_ACK(obj1, obj0));
        obj0.Equipment = obj1.Equipment;
      }
      return true;
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  private static void smethod_3([In] Account obj0, [Out] int MissionFlag)
  {
    CharacterModel character1 = obj0.Character.GetCharacter(MissionFlag);
    if (character1 == null)
      return;
    int OwnerId = 0;
    foreach (CharacterModel character2 in obj0.Character.Characters)
    {
      if (character2.Slot != character1.Slot)
      {
        character2.Slot = OwnerId;
        DaoManagerSQL.UpdatePlayerCharacter(OwnerId, character2.ObjectId, obj0.PlayerId);
        ++OwnerId;
      }
    }
    if (!DaoManagerSQL.DeletePlayerCharacter(character1.ObjectId, obj0.PlayerId))
      return;
    obj0.Character.RemoveCharacter(character1);
  }

  public static void TryBalancePlayer([In] RoomModel obj0, [In] Account obj1, [In] bool obj2, out SlotModel Data)
  {
    SlotModel slot1 = obj0.GetSlot(obj1.SlotId);
    if (slot1 == null)
      return;
    TeamEnum team = slot1.Team;
    TeamEnum balanceTeamIdx = AllUtils.GetBalanceTeamIdx(obj0, obj2, team);
    if (team == balanceTeamIdx || balanceTeamIdx == TeamEnum.ALL_TEAM)
      return;
    SlotModel slotModel = (SlotModel) null;
    foreach (int index in team == TeamEnum.CT_TEAM ? obj0.FR_TEAM : obj0.CT_TEAM)
    {
      SlotModel slot2 = obj0.Slots[index];
      if (slot2.State != SlotState.CLOSE && slot2.PlayerId == 0L)
      {
        slotModel = slot2;
        break;
      }
    }
    if (slotModel == null)
      return;
    List<SlotChange> slotChangeList = new List<SlotChange>();
    lock (obj0.Slots)
      obj0.SwitchSlots(slotChangeList, slotModel.Id, slot1.Id, false);
    if (slotChangeList.Count <= 0)
      return;
    obj1.SlotId = slot1.Id;
    Data = slot1;
    using (PROTOCOL_ROOM_TEAM_BALANCE_ACK Player = (PROTOCOL_ROOM_TEAM_BALANCE_ACK) new PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_ACK(slotChangeList, obj0.LeaderSlot, 1))
      obj0.SendPacketToPlayers((GameServerPacket) Player);
    obj0.UpdateSlotsInfo();
  }

  public static TeamEnum GetBalanceTeamIdx([In] RoomModel obj0, bool int_0, [In] TeamEnum obj2)
  {
    int num1 = !int_0 || obj2 != TeamEnum.FR_TEAM ? 0 : 1;
    int num2 = !int_0 || obj2 != TeamEnum.CT_TEAM ? 0 : 1;
    foreach (SlotModel slot in obj0.Slots)
    {
      if (slot.State == SlotState.NORMAL && !int_0 || slot.State >= SlotState.LOAD & int_0)
      {
        if (slot.Team == TeamEnum.FR_TEAM)
          ++num1;
        else
          ++num2;
      }
    }
    if (num1 + 1 < num2)
      return TeamEnum.FR_TEAM;
    return num2 + 1 >= num1 + 1 ? TeamEnum.ALL_TEAM : TeamEnum.CT_TEAM;
  }

  public static int GetNewSlotId([In] int obj0) => obj0 % 2 != 0 ? obj0 - 1 : obj0 + 1;

  public static void GetXmasReward([In] Account obj0)
  {
    EventXmasModel runningEvent = EventXmasXML.GetRunningEvent();
    if (runningEvent == null)
      return;
    PlayerEvent playerEvent = obj0.Event;
    uint ValueReq1 = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
    if (playerEvent == null || playerEvent.LastXmasDate > runningEvent.BeginDate && playerEvent.LastXmasDate <= runningEvent.EndedDate || !ComDiv.UpdateDB("player_events", "last_xmas_date", (object) (long) ValueReq1, "owner_id", (object) obj0.PlayerId))
      return;
    playerEvent.LastXmasDate = ValueReq1;
    GoodsItem good = ShopManager.GetGood(runningEvent.GoodId);
    if (good == null)
      return;
    if (ComDiv.GetIdStatics(good.Item.Id, 1) == 6 && obj0.Character.GetCharacter(good.Item.Id) == null)
      AllUtils.CreateCharacter(obj0, good.Item);
    else
      obj0.SendPacket((GameServerPacket) new PROTOCOL_LOBBY_CHATTING_ACK(0, obj0, good.Item));
    obj0.SendPacket((GameServerPacket) new PROTOCOL_BASE_RANK_UP_ACK(obj0, good.Item));
  }

  private static void smethod_4(
    RoomModel Room,
    ref int InBattle,
    ref int PlayerTeamIdx,
    [In] ref int obj3,
    [In] ref int obj4)
  {
    if (!Room.SwapRound)
      return;
    int num1 = InBattle;
    int num2 = PlayerTeamIdx;
    PlayerTeamIdx = num1;
    InBattle = num2;
    int num3 = obj3;
    int num4 = obj4;
    obj4 = num3;
    obj3 = num4;
  }

  public static void BattleEndRoundPlayersCount(RoomModel roomModel_0)
  {
    if (roomModel_0.RoundTime.IsTimer() || roomModel_0.RoomType != RoomCondition.Bomb && roomModel_0.RoomType != RoomCondition.Annihilation && roomModel_0.RoomType != RoomCondition.Destroy && roomModel_0.RoomType != RoomCondition.Ace)
      return;
    int InBattle;
    int num1;
    int Exception;
    int num2;
    roomModel_0.GetPlayingPlayers(true, ref InBattle, ref num1, ref Exception, ref num2);
    AllUtils.smethod_4(roomModel_0, ref InBattle, ref num1, ref Exception, ref num2);
    if (Exception == InBattle)
    {
      if (!roomModel_0.ActiveC4)
      {
        if (roomModel_0.SwapRound)
          ++roomModel_0.FRRounds;
        else
          ++roomModel_0.CTRounds;
      }
      AllUtils.BattleEndRound(roomModel_0, roomModel_0.SwapRound ? TeamEnum.FR_TEAM : TeamEnum.CT_TEAM, false, (FragInfos) null, (SlotModel) null);
    }
    else
    {
      if (num2 != num1)
        return;
      if (roomModel_0.SwapRound)
        ++roomModel_0.CTRounds;
      else
        ++roomModel_0.FRRounds;
      AllUtils.BattleEndRound(roomModel_0, roomModel_0.SwapRound ? TeamEnum.CT_TEAM : TeamEnum.FR_TEAM, true, (FragInfos) null, (SlotModel) null);
    }
  }

  public static void BattleEndKills([In] RoomModel obj0)
  {
    AllUtils.smethod_5(obj0, obj0.IsBotMode());
  }

  public static void BattleEndKills([In] RoomModel obj0, [In] bool obj1)
  {
    AllUtils.smethod_5(obj0, obj1);
  }

  private static void smethod_5([In] RoomModel obj0, [In] bool obj1)
  {
    int killsByMask = obj0.GetKillsByMask();
    if (obj0.FRKills < killsByMask && obj0.CTKills < killsByMask)
      return;
    List<Account> allPlayers = obj0.GetAllPlayers(SlotState.READY, 1);
    if (allPlayers.Count > 0)
    {
      TeamEnum winnerTeam = AllUtils.GetWinnerTeam(obj0);
      obj0.CalculateResult(winnerTeam, obj1);
      int Room1;
      int int_4;
      byte[] byte_1;
      AllUtils.GetBattleResult(obj0, ref Room1, ref int_4, ref byte_1);
      using (PROTOCOL_BATTLE_MISSION_ROUND_END_ACK missionRoundEndAck = (PROTOCOL_BATTLE_MISSION_ROUND_END_ACK) new PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK(obj0, winnerTeam, RoundEndType.TimeOut))
      {
        byte[] completeBytes = ((PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK) missionRoundEndAck).GetCompleteBytes("AllUtils.BaseEndByKills");
        foreach (Account Room2 in allPlayers)
        {
          SlotModel slot = obj0.GetSlot(Room2.SlotId);
          if (slot != null)
          {
            if (slot.State == SlotState.BATTLE)
              Room2.SendCompletePacket(completeBytes, missionRoundEndAck.GetType().Name);
            Room2.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_ENDBATTLE_ACK(Room2, winnerTeam, int_4, Room1, obj1, byte_1));
            AllUtils.UpdateSeasonPass(Room2);
          }
        }
      }
    }
    AllUtils.ResetBattleInfo(obj0);
  }

  public static void BattleEndKillsFreeForAll(RoomModel room) => AllUtils.smethod_6(room);

  private static void smethod_6(RoomModel room)
  {
    int killsByMask = room.GetKillsByMask();
    int[] numArray1 = new int[18];
    for (int index = 0; index < numArray1.Length; ++index)
    {
      SlotModel slot = room.Slots[index];
      numArray1[index] = slot.PlayerId == 0L ? 0 : slot.AllKills;
    }
    int fragInfos_1 = 0;
    for (int index = 0; index < numArray1.Length; ++index)
    {
      if (numArray1[index] > numArray1[fragInfos_1])
        fragInfos_1 = index;
    }
    if (numArray1[fragInfos_1] < killsByMask)
      return;
    List<Account> allPlayers = room.GetAllPlayers(SlotState.READY, 1);
    if (allPlayers.Count > 0)
    {
      room.CalculateResultFreeForAll(fragInfos_1);
      int Room;
      int slotModel_1;
      byte[] numArray2;
      AllUtils.GetBattleResult(room, ref Room, ref slotModel_1, ref numArray2);
      using (PROTOCOL_BATTLE_MISSION_ROUND_END_ACK missionRoundEndAck = (PROTOCOL_BATTLE_MISSION_ROUND_END_ACK) new PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK(room, fragInfos_1, RoundEndType.FreeForAll))
      {
        byte[] completeBytes = ((PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK) missionRoundEndAck).GetCompleteBytes("AllUtils.BaseEndByKills");
        foreach (Account account in allPlayers)
        {
          SlotModel slot = room.GetSlot(account.SlotId);
          if (slot != null)
          {
            if (slot.State == SlotState.BATTLE)
              account.SendCompletePacket(completeBytes, missionRoundEndAck.GetType().Name);
            account.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_ENDBATTLE_ACK(account, fragInfos_1, slotModel_1, Room, false, numArray2));
            AllUtils.UpdateSeasonPass(account);
          }
        }
      }
    }
    AllUtils.ResetBattleInfo(room);
  }

  public static bool CheckClanMatchRestrict([In] RoomModel obj0)
  {
    if (obj0.ChannelType == ChannelType.Clan)
    {
      foreach (ClanTeam clanTeam in (IEnumerable<ClanTeam>) AllUtils.smethod_7(obj0).Values)
      {
        if (clanTeam.PlayersFR >= 1 && clanTeam.PlayersCT >= 1)
        {
          obj0.BlockedClan = true;
          return true;
        }
      }
    }
    return false;
  }

  public static bool Have2ClansToClanMatch(RoomModel roomModel_0)
  {
    return AllUtils.smethod_7(roomModel_0).Count == 2;
  }

  public static bool HavePlayersToClanMatch([In] RoomModel obj0)
  {
    SortedList<int, ClanTeam> sortedList = AllUtils.smethod_7(obj0);
    bool flag1 = false;
    bool flag2 = false;
    foreach (ClanTeam clanTeam in (IEnumerable<ClanTeam>) sortedList.Values)
    {
      if (clanTeam.PlayersFR >= 4)
        flag1 = true;
      else if (clanTeam.PlayersCT >= 4)
        flag2 = true;
    }
    return flag1 & flag2;
  }

  private static SortedList<int, ClanTeam> smethod_7(RoomModel room)
  {
    SortedList<int, ClanTeam> sortedList = new SortedList<int, ClanTeam>();
    for (int index = 0; index < room.GetAllPlayers().Count; ++index)
    {
      Account allPlayer = room.GetAllPlayers()[index];
      if (allPlayer.ClanId != 0)
      {
        ClanTeam clanTeam;
        if (sortedList.TryGetValue(allPlayer.ClanId, out clanTeam) && clanTeam != null)
        {
          if (allPlayer.SlotId % 2 == 0)
            ++clanTeam.PlayersFR;
          else
            ++clanTeam.PlayersCT;
        }
        else
        {
          clanTeam = new ClanTeam()
          {
            ClanId = allPlayer.ClanId
          };
          if (allPlayer.SlotId % 2 == 0)
            ++clanTeam.PlayersFR;
          else
            ++clanTeam.PlayersCT;
          sortedList.Add(allPlayer.ClanId, clanTeam);
        }
      }
    }
    return sortedList;
  }

  public static void PlayTimeEvent(
    Account roomModel_0,
    [In] EventPlaytimeModel obj1,
    [In] bool obj2,
    [In] SlotModel obj3,
    [In] long obj4)
  {
    try
    {
      RoomModel room = roomModel_0.Room;
      PlayerEvent BoostType = roomModel_0.Event;
      if (room == null || BoostType == null)
        return;
      int minutes1 = obj1.Minutes1;
      int minutes2 = obj1.Minutes2;
      int minutes3 = obj1.Minutes3;
      if (minutes1 == 0 && minutes2 == 0 && minutes3 == 0)
      {
        CLogger.Print($"Event Playtime Disabled Due To: 0 Value! (Minutes1: {minutes1}; Minutes2: {minutes2}; Minutes3: {minutes3}", LoggerType.Warning, (Exception) null);
      }
      else
      {
        long lastPlaytimeValue = BoostType.LastPlaytimeValue;
        long lastPlaytimeFinish = (long) BoostType.LastPlaytimeFinish;
        long lastPlaytimeDate = (long) BoostType.LastPlaytimeDate;
        if (BoostType.LastPlaytimeFinish >= 0 && BoostType.LastPlaytimeFinish <= 2)
        {
          BoostType.LastPlaytimeValue += obj4;
          int num1 = BoostType.LastPlaytimeFinish == 0 ? obj1.Minutes1 : (BoostType.LastPlaytimeFinish == 1 ? obj1.Minutes2 : (BoostType.LastPlaytimeFinish == 2 ? obj1.Minutes3 : 0));
          if (num1 == 0)
            return;
          int num2 = num1 * 60;
          if (BoostType.LastPlaytimeValue >= (long) num2)
          {
            Random random = new Random();
            List<int> intList = BoostType.LastPlaytimeFinish == 0 ? obj1.Goods1 : (BoostType.LastPlaytimeFinish == 1 ? obj1.Goods2 : (BoostType.LastPlaytimeFinish == 2 ? obj1.Goods3 : new List<int>()));
            if (intList.Count > 0)
            {
              GoodsItem good = ShopManager.GetGood(intList[random.Next(0, intList.Count)]);
              if (good != null)
              {
                if (ComDiv.GetIdStatics(good.Item.Id, 1) == 6 && roomModel_0.Character.GetCharacter(good.Item.Id) == null)
                  AllUtils.CreateCharacter(roomModel_0, good.Item);
                else
                  roomModel_0.SendPacket((GameServerPacket) new PROTOCOL_LOBBY_CHATTING_ACK(0, roomModel_0, good.Item));
                roomModel_0.SendPacket((GameServerPacket) new PROTOCOL_BASE_RANK_UP_ACK(roomModel_0, good.Item));
              }
            }
            ++BoostType.LastPlaytimeFinish;
            BoostType.LastPlaytimeValue = 0L;
          }
          BoostType.LastPlaytimeDate = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
        }
        if (BoostType.LastPlaytimeValue == lastPlaytimeValue && (long) BoostType.LastPlaytimeFinish == lastPlaytimeFinish && (long) BoostType.LastPlaytimeDate == lastPlaytimeDate)
          return;
        EventPlaytimeXML.ResetPlayerEvent(roomModel_0.PlayerId, BoostType);
      }
    }
    catch (Exception ex)
    {
      CLogger.Print("[AllUtils.PlayTimeEvent] " + ex.Message, LoggerType.Error, ex);
    }
  }

  public static void CompleteMission(
    RoomModel Player,
    SlotModel EvPlaytime,
    FragInfos IsBotMode,
    MissionType Slot,
    int PlayedTime)
  {
    try
    {
      Account playerBySlot = Player.GetPlayerBySlot(EvPlaytime);
      if (playerBySlot == null)
        return;
      AllUtils.smethod_8(Player, playerBySlot, EvPlaytime, IsBotMode, Slot, PlayedTime);
    }
    catch (Exception ex)
    {
      CLogger.Print("[AllUtils.CompleteMission1] " + ex.Message, LoggerType.Error, ex);
    }
  }

  public static void CompleteMission(
    RoomModel Room,
    SlotModel Slot,
    MissionType Kills,
    int AutoComplete)
  {
    try
    {
      Account playerBySlot = Room.GetPlayerBySlot(Slot);
      if (playerBySlot == null)
        return;
      AllUtils.smethod_9(Room, playerBySlot, Slot, Kills, AutoComplete);
    }
    catch (Exception ex)
    {
      CLogger.Print("[AllUtils.CompleteMission2] " + ex.Message, LoggerType.Error, ex);
    }
  }

  public static void CompleteMission(
    [In] RoomModel obj0,
    [In] Account obj1,
    [In] SlotModel obj2,
    [In] FragInfos obj3,
    MissionType MoreInfo,
    [In] int obj5)
  {
    AllUtils.smethod_8(obj0, obj1, obj2, obj3, MoreInfo, obj5);
  }

  public static void CompleteMission(
    [In] RoomModel obj0,
    Account player,
    SlotModel slot,
    MissionType kills,
    int autoComplete)
  {
    AllUtils.smethod_9(obj0, player, slot, kills, autoComplete);
  }

  private static void smethod_8(
    RoomModel room,
    Account player,
    SlotModel slot,
    FragInfos autoComplete,
    MissionType moreInfo,
    [In] int obj5)
  {
    try
    {
      PlayerMissions missions = slot.Missions;
      if (missions == null)
        return;
      int currentMissionId = missions.GetCurrentMissionId();
      int currentCard = missions.GetCurrentCard();
      if (currentMissionId <= 0 || missions.SelectedCard)
        return;
      List<MissionCardModel> cards = MissionCardRAW.GetCards(currentMissionId, currentCard);
      if (cards.Count == 0)
        return;
      KillingMessage allKillFlags = autoComplete.GetAllKillFlags();
      byte[] currentMissionList = missions.GetCurrentMissionList();
      ClassType idStatics1 = (ClassType) ComDiv.GetIdStatics(autoComplete.WeaponId, 2);
      ClassType slotModel_0 = AllUtils.smethod_1(idStatics1);
      int idStatics2 = ComDiv.GetIdStatics(autoComplete.WeaponId, 3);
      ClassType classType = obj5 > 0 ? (ClassType) ComDiv.GetIdStatics(autoComplete.WeaponId, 2) : ClassType.Unknown;
      ClassType classType_1 = obj5 > 0 ? AllUtils.smethod_1(classType) : ClassType.Unknown;
      int idStatics3 = obj5 > 0 ? ComDiv.GetIdStatics(obj5, 3) : 0;
      foreach (MissionCardModel missionCardModel in cards)
      {
        int num = 0;
        if (missionCardModel.MapId == 0 || (MapIdEnum) missionCardModel.MapId == room.MapId)
        {
          if (autoComplete.Frags.Count > 0)
          {
            if (missionCardModel.MissionType != MissionType.KILL && (missionCardModel.MissionType != MissionType.CHAINSTOPPER || !allKillFlags.HasFlag((Enum) KillingMessage.ChainStopper)) && (missionCardModel.MissionType != MissionType.CHAINSLUGGER || !allKillFlags.HasFlag((Enum) KillingMessage.ChainSlugger)) && (missionCardModel.MissionType != MissionType.CHAINKILLER || slot.KillsOnLife < 4) && (missionCardModel.MissionType != MissionType.TRIPLE_KILL || slot.KillsOnLife != 3) && (missionCardModel.MissionType != MissionType.DOUBLE_KILL || slot.KillsOnLife != 2) && (missionCardModel.MissionType != MissionType.HEADSHOT || !allKillFlags.HasFlag((Enum) KillingMessage.Headshot) && !allKillFlags.HasFlag((Enum) KillingMessage.ChainHeadshot)) && (missionCardModel.MissionType != MissionType.CHAINHEADSHOT || !allKillFlags.HasFlag((Enum) KillingMessage.ChainHeadshot)) && (missionCardModel.MissionType != MissionType.PIERCING || !allKillFlags.HasFlag((Enum) KillingMessage.PiercingShot)) && (missionCardModel.MissionType != MissionType.MASS_KILL || !allKillFlags.HasFlag((Enum) KillingMessage.MassKill)) && (missionCardModel.MissionType != MissionType.KILL_MAN || !room.IsDinoMode("") || (slot.Team != TeamEnum.CT_TEAM || room.Rounds != 2) && (slot.Team != TeamEnum.FR_TEAM || room.Rounds != 1)))
            {
              if (missionCardModel.MissionType == MissionType.KILL_WEAPONCLASS || missionCardModel.MissionType == MissionType.DOUBLE_KILL_WEAPONCLASS && slot.KillsOnLife == 2 || missionCardModel.MissionType == MissionType.TRIPLE_KILL_WEAPONCLASS && slot.KillsOnLife == 3)
                num = AllUtils.smethod_11(missionCardModel, autoComplete);
            }
            else
              num = AllUtils.smethod_10(missionCardModel, idStatics1, slotModel_0, idStatics2, autoComplete);
          }
          else if (missionCardModel.MissionType == MissionType.DEATHBLOW && moreInfo == MissionType.DEATHBLOW)
            num = AllUtils.smethod_13(missionCardModel, classType, classType_1, idStatics3);
          else if (missionCardModel.MissionType == moreInfo)
            num = 1;
        }
        if (num != 0)
        {
          int arrayIdx = missionCardModel.ArrayIdx;
          if ((int) currentMissionList[arrayIdx] + 1 <= missionCardModel.MissionLimit)
          {
            slot.MissionsCompleted = true;
            currentMissionList[arrayIdx] += (byte) num;
            if ((int) currentMissionList[arrayIdx] > missionCardModel.MissionLimit)
              currentMissionList[arrayIdx] = (byte) missionCardModel.MissionLimit;
            int uint_1 = (int) currentMissionList[arrayIdx];
            player.SendPacket((GameServerPacket) new PROTOCOL_BASE_QUEST_GET_INFO_ACK(uint_1, missionCardModel));
          }
        }
      }
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  private static void smethod_9(
    [In] RoomModel obj0,
    Account account_0,
    SlotModel slotModel_0,
    MissionType fragInfos_0,
    int missionType_0)
  {
    try
    {
      PlayerMissions missions = slotModel_0.Missions;
      if (missions == null)
        return;
      int currentMissionId = missions.GetCurrentMissionId();
      int currentCard = missions.GetCurrentCard();
      if (currentMissionId <= 0 || missions.SelectedCard)
        return;
      List<MissionCardModel> cards = MissionCardRAW.GetCards(currentMissionId, currentCard);
      if (cards.Count == 0)
        return;
      byte[] currentMissionList = missions.GetCurrentMissionList();
      ClassType classType = missionType_0 > 0 ? (ClassType) ComDiv.GetIdStatics(missionType_0, 2) : ClassType.Unknown;
      ClassType classType_1 = missionType_0 > 0 ? AllUtils.smethod_1(classType) : ClassType.Unknown;
      int idStatics = missionType_0 > 0 ? ComDiv.GetIdStatics(missionType_0, 3) : 0;
      foreach (MissionCardModel int_1 in cards)
      {
        int num = 0;
        if (int_1.MapId == 0 || (MapIdEnum) int_1.MapId == obj0.MapId)
        {
          if (int_1.MissionType == MissionType.DEATHBLOW && fragInfos_0 == MissionType.DEATHBLOW)
            num = AllUtils.smethod_13(int_1, classType, classType_1, idStatics);
          else if (int_1.MissionType == fragInfos_0)
            num = 1;
        }
        if (num != 0)
        {
          int arrayIdx = int_1.ArrayIdx;
          if ((int) currentMissionList[arrayIdx] + 1 <= int_1.MissionLimit)
          {
            slotModel_0.MissionsCompleted = true;
            currentMissionList[arrayIdx] += (byte) num;
            if ((int) currentMissionList[arrayIdx] > int_1.MissionLimit)
              currentMissionList[arrayIdx] = (byte) int_1.MissionLimit;
            int uint_1 = (int) currentMissionList[arrayIdx];
            account_0.SendPacket((GameServerPacket) new PROTOCOL_BASE_QUEST_GET_INFO_ACK(uint_1, int_1));
          }
        }
      }
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  private static int smethod_10(
    MissionCardModel roomModel_0,
    ClassType account_0,
    ClassType slotModel_0,
    int missionType_0,
    FragInfos int_0)
  {
    int num = 0;
    if ((roomModel_0.WeaponReqId == 0 || roomModel_0.WeaponReqId == missionType_0) && (roomModel_0.WeaponReq == ClassType.Unknown || roomModel_0.WeaponReq == account_0 || roomModel_0.WeaponReq == slotModel_0))
    {
      foreach (FragModel frag in int_0.Frags)
      {
        if ((int) frag.VictimSlot % 2 != (int) int_0.KillerSlot % 2)
          ++num;
      }
    }
    return num;
  }

  private static int smethod_11(MissionCardModel missionCardModel_0, FragInfos classType_0)
  {
    int num = 0;
    foreach (FragModel frag in classType_0.Frags)
    {
      if ((int) frag.VictimSlot % 2 != (int) classType_0.KillerSlot % 2 && (missionCardModel_0.WeaponReq == ClassType.Unknown || missionCardModel_0.WeaponReq == (ClassType) frag.WeaponClass || missionCardModel_0.WeaponReq == AllUtils.smethod_1((ClassType) frag.WeaponClass)))
        ++num;
    }
    return num;
  }

  private static int smethod_12(
    [In] MissionCardModel obj0,
    [In] ClassType obj1,
    ClassType classType_1,
    int int_0,
    int fragInfos_0,
    [In] FragModel obj5)
  {
    return (obj0.WeaponReqId == 0 || obj0.WeaponReqId == int_0) && (obj0.WeaponReq == ClassType.Unknown || obj0.WeaponReq == obj1 || obj0.WeaponReq == classType_1) && (int) obj5.VictimSlot % 2 != fragInfos_0 % 2 ? 1 : 0;
  }

  private static int smethod_13(
    [In] MissionCardModel obj0,
    ClassType classType_0,
    ClassType classType_1,
    int int_0)
  {
    return obj0.WeaponReqId != 0 && obj0.WeaponReqId != int_0 || obj0.WeaponReq != ClassType.Unknown && obj0.WeaponReq != classType_0 && obj0.WeaponReq != classType_1 ? 0 : 1;
  }

  public static void EnableQuestMission([In] Account obj0)
  {
    PlayerEvent playerEvent = obj0.Event;
    if (playerEvent == null || playerEvent.LastQuestFinish != 0 || EventQuestXML.GetRunningEvent() == null)
      return;
    obj0.Mission.Mission4 = 13;
  }

  public static void GetReadyPlayers(
    RoomModel missionCardModel_0,
    ref int classType_0,
    ref int classType_1,
    ref int int_0)
  {
    int num = 0;
    for (int index = 0; index < missionCardModel_0.Slots.Length; ++index)
    {
      SlotModel slot = missionCardModel_0.Slots[index];
      if (slot.State == SlotState.READY)
      {
        if (missionCardModel_0.RoomType == RoomCondition.FreeForAll && index > 0)
          ++num;
        else if (slot.Team == TeamEnum.FR_TEAM)
          ++classType_0;
        else
          ++classType_1;
      }
    }
    if (missionCardModel_0.RoomType == RoomCondition.FreeForAll)
      int_0 = num;
    else if (missionCardModel_0.LeaderSlot % 2 == 0)
      int_0 = classType_1;
    else
      int_0 = classType_0;
  }

  public static bool CompetitiveMatchCheck(Account Player, RoomModel FRPlayers, [In] ref uint obj2)
  {
    if (FRPlayers.Competitive)
    {
      foreach (SlotModel slot in FRPlayers.Slots)
      {
        if (slot != null && slot.State != SlotState.CLOSE && slot.State < SlotState.READY)
        {
          Player.SendPacket((GameServerPacket) new PROTOCOL_LOBBY_GET_ROOMINFOADD_ACK(Translation.GetLabel("Competitive"), Player.Session.SessionId, Player.NickColor, true, Translation.GetLabel("CompetitiveFullSlot")));
          obj2 = 2147487858U;
          return true;
        }
      }
    }
    obj2 = 0U;
    return false;
  }

  public static bool ClanMatchCheck(
    [In] RoomModel obj0,
    [In] ChannelType obj1,
    int CTPlayers,
    ref uint TotalEnemys)
  {
    if (!ConfigLoader.IsTestMode && obj1 == ChannelType.Clan)
    {
      if (!AllUtils.Have2ClansToClanMatch(obj0))
      {
        TotalEnemys = 2147487857U;
        return true;
      }
      if (CTPlayers > 0 && !AllUtils.HavePlayersToClanMatch(obj0))
      {
        TotalEnemys = 2147487858U;
        return true;
      }
      TotalEnemys = 0U;
      return false;
    }
    TotalEnemys = 0U;
    return false;
  }

  public static void TryBalanceTeams([In] RoomModel obj0)
  {
    if (obj0.BalanceType != TeamBalance.Count || obj0.IsBotMode())
      return;
    int[] numArray1;
    switch (AllUtils.GetBalanceTeamIdx(obj0, false, TeamEnum.ALL_TEAM))
    {
      case TeamEnum.ALL_TEAM:
        return;
      case TeamEnum.CT_TEAM:
        numArray1 = obj0.FR_TEAM;
        break;
      default:
        numArray1 = obj0.CT_TEAM;
        break;
    }
    int[] numArray2 = numArray1;
    SlotModel Data = (SlotModel) null;
    for (int index = numArray2.Length - 1; index >= 0; --index)
    {
      SlotModel slot = obj0.Slots[numArray2[index]];
      if (slot.State == SlotState.READY && obj0.LeaderSlot != slot.Id)
      {
        Data = slot;
        break;
      }
    }
    Account account;
    if (Data == null || !obj0.GetPlayerBySlot(Data, ref account))
      return;
    AllUtils.TryBalancePlayer(obj0, account, false, out Data);
  }

  public static void FreepassEffect(
    Account Room,
    SlotModel Type,
    RoomModel TotalEnemys,
    [Out] bool Error)
  {
    DBQuery dbQuery = new DBQuery();
    if (Room.Bonus.FreePass != 0 && (Room.Bonus.FreePass != 1 || TotalEnemys.ChannelType != ChannelType.Clan))
    {
      if (TotalEnemys.State != RoomState.BATTLE)
        return;
      int num1 = 0;
      int num2 = 0;
      int num3;
      int num4;
      if (Error)
      {
        int num5 = (int) TotalEnemys.IngameAiLevel * (150 + Type.AllDeaths);
        if (num5 == 0)
          ++num5;
        int num6 = Type.Score / num5;
        num3 = num2 + num6;
        num4 = num1 + num6;
      }
      else
      {
        int num7 = Type.AllKills != 0 || Type.AllDeaths != 0 ? (int) Type.InBattleTime(DateTimeUtil.Now()) : 0;
        if (TotalEnemys.RoomType != RoomCondition.Bomb && TotalEnemys.RoomType != RoomCondition.FreeForAll && TotalEnemys.RoomType != RoomCondition.Destroy)
        {
          num4 = (int) ((double) Type.Score + (double) num7 / 2.5 + (double) Type.AllDeaths * 1.8 + (double) (Type.Objects * 20));
          num3 = (int) ((double) Type.Score + (double) num7 / 3.0 + (double) Type.AllDeaths * 1.8 + (double) (Type.Objects * 20));
        }
        else
        {
          num4 = (int) ((double) Type.Score + (double) num7 / 2.5 + (double) Type.AllDeaths * 2.2 + (double) (Type.Objects * 20));
          num3 = (int) ((double) Type.Score + (double) num7 / 3.0 + (double) Type.AllDeaths * 2.2 + (double) (Type.Objects * 20));
        }
      }
      Room.Exp += ConfigLoader.MaxExpReward < num4 ? ConfigLoader.MaxExpReward : num4;
      Room.Gold += ConfigLoader.MaxGoldReward < num3 ? ConfigLoader.MaxGoldReward : num3;
      if (num3 > 0)
        dbQuery.AddQuery("gold", (object) Room.Gold);
      if (num4 > 0)
        dbQuery.AddQuery("experience", (object) Room.Exp);
    }
    else
    {
      if (Error || Type.State < SlotState.BATTLE_READY)
        return;
      if (Room.Gold > 0)
      {
        Room.Gold -= 200;
        if (Room.Gold < 0)
          Room.Gold = 0;
        dbQuery.AddQuery("gold", (object) Room.Gold);
      }
      ComDiv.UpdateDB("player_stat_basics", "owner_id", (object) Room.PlayerId, "escapes_count", (object) ++Room.Statistic.Basic.EscapesCount);
      ComDiv.UpdateDB("player_stat_seasons", "owner_id", (object) Room.PlayerId, "escapes_count", (object) ++Room.Statistic.Season.EscapesCount);
    }
    ComDiv.UpdateDB("accounts", "player_id", (object) Room.PlayerId, dbQuery.GetTables(), dbQuery.GetValues());
  }

  public static void LeaveHostGiveBattlePVE(RoomModel Room, [In] Account obj1)
  {
    List<Account> allPlayers = Room.GetAllPlayers(SlotState.READY, 1);
    if (allPlayers.Count == 0)
      return;
    int leaderSlot = Room.LeaderSlot;
    Room.SetNewLeader(-1, SlotState.BATTLE_READY, leaderSlot, true);
    using (PROTOCOL_BATTLE_GIVEUPBATTLE_ACK battleGiveupbattleAck = (PROTOCOL_BATTLE_GIVEUPBATTLE_ACK) new PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK(obj1, 0))
    {
      using (PROTOCOL_BATTLE_LEAVEP2PSERVER_ACK leaveP2PserverAck = (PROTOCOL_BATTLE_LEAVEP2PSERVER_ACK) new PROTOCOL_BATTLE_MISSION_BOMB_UNINSTALL_ACK(Room))
      {
        byte[] completeBytes1 = ((PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK) battleGiveupbattleAck).GetCompleteBytes("PROTOCOL_BATTLE_GIVEUPBATTLE_REQ-1");
        byte[] completeBytes2 = ((PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK) leaveP2PserverAck).GetCompleteBytes("PROTOCOL_BATTLE_GIVEUPBATTLE_REQ-2");
        foreach (Account account in allPlayers)
        {
          SlotModel slot = Room.GetSlot(account.SlotId);
          if (slot != null)
          {
            if (slot.State >= SlotState.PRESTART)
              account.SendCompletePacket(completeBytes2, leaveP2PserverAck.GetType().Name);
            account.SendCompletePacket(completeBytes1, battleGiveupbattleAck.GetType().Name);
          }
        }
      }
    }
  }

  public static void LeaveHostEndBattlePVE([In] RoomModel obj0, Account Slot)
  {
    List<Account> allPlayers = obj0.GetAllPlayers(SlotState.READY, 1);
    if (allPlayers.Count > 0)
    {
      using (PROTOCOL_BATTLE_GIVEUPBATTLE_ACK battleGiveupbattleAck = (PROTOCOL_BATTLE_GIVEUPBATTLE_ACK) new PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK(Slot, 0))
      {
        byte[] completeBytes = ((PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK) battleGiveupbattleAck).GetCompleteBytes("PROTOCOL_BATTLE_GIVEUPBATTLE_REQ-3");
        TeamEnum winnerTeam = AllUtils.GetWinnerTeam(obj0);
        int Room1;
        int int_4;
        byte[] byte_1;
        AllUtils.GetBattleResult(obj0, ref Room1, ref int_4, ref byte_1);
        foreach (Account Room2 in allPlayers)
        {
          Room2.SendCompletePacket(completeBytes, battleGiveupbattleAck.GetType().Name);
          Room2.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_ENDBATTLE_ACK(Room2, winnerTeam, int_4, Room1, true, byte_1));
          AllUtils.UpdateSeasonPass(Room2);
        }
      }
    }
    AllUtils.ResetBattleInfo(obj0);
  }

  public static void LeaveHostEndBattlePVP(
    [In] RoomModel obj0,
    [In] Account obj1,
    [In] int obj2,
    int IsBotMode,
    [In] ref bool obj4)
  {
    obj4 = true;
    List<Account> allPlayers = obj0.GetAllPlayers(SlotState.READY, 1);
    if (allPlayers.Count > 0)
    {
      TeamEnum winnerTeam = AllUtils.GetWinnerTeam(obj0, obj2, IsBotMode);
      if (obj0.State == RoomState.BATTLE)
        obj0.CalculateResult(winnerTeam, false);
      using (PROTOCOL_BATTLE_GIVEUPBATTLE_ACK battleGiveupbattleAck = (PROTOCOL_BATTLE_GIVEUPBATTLE_ACK) new PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK(obj1, 0))
      {
        byte[] completeBytes = ((PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK) battleGiveupbattleAck).GetCompleteBytes("PROTOCOL_BATTLE_GIVEUPBATTLE_REQ-4");
        int Room1;
        int int_4;
        byte[] byte_1;
        AllUtils.GetBattleResult(obj0, ref Room1, ref int_4, ref byte_1);
        foreach (Account Room2 in allPlayers)
        {
          Room2.SendCompletePacket(completeBytes, battleGiveupbattleAck.GetType().Name);
          Room2.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_ENDBATTLE_ACK(Room2, winnerTeam, int_4, Room1, false, byte_1));
          AllUtils.UpdateSeasonPass(Room2);
        }
      }
    }
    AllUtils.ResetBattleInfo(obj0);
  }

  public static void LeaveHostGiveBattlePVP(RoomModel Room, Account Player)
  {
    List<Account> allPlayers = Room.GetAllPlayers(SlotState.READY, 1);
    if (allPlayers.Count == 0)
      return;
    int leaderSlot = Room.LeaderSlot;
    SlotState Player1 = Room.State == RoomState.BATTLE ? SlotState.BATTLE_READY : SlotState.READY;
    Room.SetNewLeader(-1, Player1, leaderSlot, true);
    using (PROTOCOL_BATTLE_LEAVEP2PSERVER_ACK leaveP2PserverAck = (PROTOCOL_BATTLE_LEAVEP2PSERVER_ACK) new PROTOCOL_BATTLE_MISSION_BOMB_UNINSTALL_ACK(Room))
    {
      using (PROTOCOL_BATTLE_GIVEUPBATTLE_ACK battleGiveupbattleAck = (PROTOCOL_BATTLE_GIVEUPBATTLE_ACK) new PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK(Player, 0))
      {
        byte[] completeBytes1 = ((PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK) leaveP2PserverAck).GetCompleteBytes("PROTOCOL_BATTLE_GIVEUPBATTLE_REQ-6");
        byte[] completeBytes2 = ((PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK) battleGiveupbattleAck).GetCompleteBytes("PROTOCOL_BATTLE_GIVEUPBATTLE_REQ-7");
        foreach (Account account in allPlayers)
        {
          if (Room.Slots[account.SlotId].State >= SlotState.PRESTART)
            account.SendCompletePacket(completeBytes1, leaveP2PserverAck.GetType().Name);
          account.SendCompletePacket(completeBytes2, battleGiveupbattleAck.GetType().Name);
        }
      }
    }
  }

  public static void LeavePlayerEndBattlePVP(
    [In] RoomModel obj0,
    [In] Account obj1,
    int TeamFR,
    int TeamCT,
    out bool IsFinished)
  {
    IsFinished = true;
    TeamEnum winnerTeam = AllUtils.GetWinnerTeam(obj0, TeamFR, TeamCT);
    List<Account> allPlayers = obj0.GetAllPlayers(SlotState.READY, 1);
    if (allPlayers.Count > 0)
    {
      if (obj0.State == RoomState.BATTLE)
        obj0.CalculateResult(winnerTeam, false);
      using (PROTOCOL_BATTLE_GIVEUPBATTLE_ACK battleGiveupbattleAck = (PROTOCOL_BATTLE_GIVEUPBATTLE_ACK) new PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK(obj1, 0))
      {
        byte[] completeBytes = ((PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK) battleGiveupbattleAck).GetCompleteBytes("PROTOCOL_BATTLE_GIVEUPBATTLE_REQ-8");
        int Room1;
        int int_4;
        byte[] byte_1;
        AllUtils.GetBattleResult(obj0, ref Room1, ref int_4, ref byte_1);
        foreach (Account Room2 in allPlayers)
        {
          Room2.SendCompletePacket(completeBytes, battleGiveupbattleAck.GetType().Name);
          Room2.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_ENDBATTLE_ACK(Room2, winnerTeam, int_4, Room1, false, byte_1));
          AllUtils.UpdateSeasonPass(Room2);
        }
      }
    }
    AllUtils.ResetBattleInfo(obj0);
  }

  public static void LeavePlayerQuitBattle(RoomModel Room, Account Player)
  {
    using (PROTOCOL_BATTLE_GIVEUPBATTLE_ACK battleGiveupbattleAck = (PROTOCOL_BATTLE_GIVEUPBATTLE_ACK) new PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK(Player, 0))
      Room.SendPacketToPlayers((GameServerPacket) battleGiveupbattleAck, SlotState.READY, 1);
  }

  private static int smethod_14([In] int obj0, [In] SortedList<int, int> obj1)
  {
    int num;
    return obj1.TryGetValue(obj0, out num) ? num : 0;
  }

  private static int smethod_15([In] int obj0, [In] SortedList<int, int> obj1)
  {
    int num;
    return obj1.TryGetValue(obj0, out num) ? num : 0;
  }

  private static int smethod_16([In] Account obj0, int Player)
  {
    ItemsModel itemsModel = obj0.Inventory.GetItem(Player);
    return itemsModel != null ? itemsModel.Id : 0;
  }

  public static void ValidateAccesoryEquipment([In] Account obj0, int sortedList_0)
  {
    if (obj0.Equipment.AccessoryId == sortedList_0)
      return;
    obj0.Equipment.AccessoryId = AllUtils.smethod_16(obj0, sortedList_0);
    ComDiv.UpdateDB("player_equipments", "accesory_id", (object) obj0.Equipment.AccessoryId, "owner_id", (object) obj0.PlayerId);
  }

  public static void ValidateDisabledCoupon([In] Account obj0, SortedList<int, int> sortedList_0)
  {
    for (int index = 0; index < sortedList_0.Keys.Count; ++index)
    {
      ItemsModel itemsModel = obj0.Inventory.GetItem(AllUtils.smethod_14(index, sortedList_0));
      if (itemsModel != null)
      {
        CouponFlag couponEffect = CouponEffectXML.GetCouponEffect(itemsModel.Id);
        if (couponEffect != null && couponEffect.EffectFlag > (CouponEffects) 0 && obj0.Effects.HasFlag((Enum) couponEffect.EffectFlag))
        {
          obj0.Effects -= couponEffect.EffectFlag;
          DaoManagerSQL.UpdateCouponEffect(obj0.PlayerId, obj0.Effects);
        }
      }
    }
  }

  public static void ValidateEnabledCoupon([In] Account obj0, SortedList<int, int> int_0)
  {
    for (int index = 0; index < int_0.Keys.Count; ++index)
    {
      ItemsModel itemsModel = obj0.Inventory.GetItem(AllUtils.smethod_14(index, int_0));
      if (itemsModel != null)
      {
        int num = obj0.Bonus.AddBonuses(itemsModel.Id) ? 1 : 0;
        CouponFlag couponEffect = CouponEffectXML.GetCouponEffect(itemsModel.Id);
        if (couponEffect != null && couponEffect.EffectFlag > (CouponEffects) 0 && !obj0.Effects.HasFlag((Enum) couponEffect.EffectFlag))
        {
          obj0.Effects |= couponEffect.EffectFlag;
          DaoManagerSQL.UpdateCouponEffect(obj0.PlayerId, obj0.Effects);
        }
        if (num != 0)
          DaoManagerSQL.UpdatePlayerBonus(obj0.PlayerId, obj0.Bonus.Bonuses, obj0.Bonus.FreePass);
      }
    }
  }

  private static bool smethod_17(
    [In] int obj0,
    CouponEffects AccessoryId,
    [In] (int, CouponEffects, bool) obj2)
  {
    if (obj0 != obj2.Item1)
      return false;
    return obj2.Item3 ? (AccessoryId & obj2.Item2) > (CouponEffects) 0 : AccessoryId.HasFlag((Enum) obj2.Item2);
  }

  public static bool CheckDuplicateCouponEffects(Account Player, int Coupons)
  {
    bool flag = false;
    foreach ((int, CouponEffects, bool) valueTuple in new List<(int, CouponEffects, bool)>()
    {
      (1600065, CouponEffects.Defense20 | CouponEffects.Defense10 | CouponEffects.Defense5, true),
      (1600079, CouponEffects.Defense90 | CouponEffects.Defense10 | CouponEffects.Defense5, true),
      (1600044, CouponEffects.Defense90 | CouponEffects.Defense20 | CouponEffects.Defense5, true),
      (1600030, CouponEffects.Defense90 | CouponEffects.Defense20 | CouponEffects.Defense10, true),
      (1600078, CouponEffects.JackHollowPoint | CouponEffects.HollowPoint | CouponEffects.FullMetalJack, true),
      (1600032, CouponEffects.HollowPointPlus | CouponEffects.JackHollowPoint | CouponEffects.FullMetalJack, true),
      (1600031, CouponEffects.HollowPointPlus | CouponEffects.JackHollowPoint | CouponEffects.HollowPoint, true),
      (1600036, CouponEffects.HollowPointPlus | CouponEffects.HollowPoint | CouponEffects.FullMetalJack, true),
      (1600028, CouponEffects.HP5, false),
      (1600040, CouponEffects.HP10, false),
      (1600209, CouponEffects.Camoflage50, false),
      (1600208, CouponEffects.Camoflage99, false)
    })
    {
      if (AllUtils.smethod_17(Coupons, Player.Effects, valueTuple))
      {
        flag = true;
        break;
      }
    }
    return flag;
  }

  public static void ValidateCharacterEquipment(
    Account int_0,
    PlayerEquipment couponEffects_0,
    int[] valueTuple_0,
    [In] int obj3,
    [In] int[] obj4)
  {
    DBQuery dbQuery = new DBQuery();
    CharacterModel character = int_0.Character.GetCharacter(obj3);
    if (character != null)
    {
      int idStatics1 = ComDiv.GetIdStatics(character.Id, 1);
      int idStatics2 = ComDiv.GetIdStatics(character.Id, 2);
      int idStatics3 = ComDiv.GetIdStatics(character.Id, 5);
      if (idStatics1 == 6 && (idStatics2 == 1 || idStatics3 == 632) && obj4[0] == character.Slot)
      {
        if (couponEffects_0.CharaRedId != character.Id)
        {
          couponEffects_0.CharaRedId = character.Id;
          dbQuery.AddQuery("chara_red_side", (object) couponEffects_0.CharaRedId);
        }
      }
      else if (idStatics1 == 6 && (idStatics2 == 2 || idStatics3 == 664) && obj4[1] == character.Slot && couponEffects_0.CharaBlueId != character.Id)
      {
        couponEffects_0.CharaBlueId = character.Id;
        dbQuery.AddQuery("chara_blue_side", (object) couponEffects_0.CharaBlueId);
      }
    }
    for (int index = 0; index < valueTuple_0.Length; ++index)
    {
      int num = AllUtils.smethod_16(int_0, valueTuple_0[index]);
      switch (index)
      {
        case 0:
          if (num != 0 && couponEffects_0.WeaponPrimary != num)
          {
            couponEffects_0.WeaponPrimary = num;
            dbQuery.AddQuery("weapon_primary", (object) couponEffects_0.WeaponPrimary);
            break;
          }
          break;
        case 1:
          if (num != 0 && couponEffects_0.WeaponSecondary != num)
          {
            couponEffects_0.WeaponSecondary = num;
            dbQuery.AddQuery("weapon_secondary", (object) couponEffects_0.WeaponSecondary);
            break;
          }
          break;
        case 2:
          if (num != 0 && couponEffects_0.WeaponMelee != num)
          {
            couponEffects_0.WeaponMelee = num;
            dbQuery.AddQuery("weapon_melee", (object) couponEffects_0.WeaponMelee);
            break;
          }
          break;
        case 3:
          if (num != 0 && couponEffects_0.WeaponExplosive != num)
          {
            couponEffects_0.WeaponExplosive = num;
            dbQuery.AddQuery("weapon_explosive", (object) couponEffects_0.WeaponExplosive);
            break;
          }
          break;
        case 4:
          if (num != 0 && couponEffects_0.WeaponSpecial != num)
          {
            couponEffects_0.WeaponSpecial = num;
            dbQuery.AddQuery("weapon_special", (object) couponEffects_0.WeaponSpecial);
            break;
          }
          break;
        case 5:
          if (couponEffects_0.PartHead != num)
          {
            couponEffects_0.PartHead = num;
            dbQuery.AddQuery("part_head", (object) couponEffects_0.PartHead);
            break;
          }
          break;
        case 6:
          if (num != 0 && couponEffects_0.PartFace != num)
          {
            couponEffects_0.PartFace = num;
            dbQuery.AddQuery("part_face", (object) couponEffects_0.PartFace);
            break;
          }
          break;
        case 7:
          if (num != 0 && couponEffects_0.PartJacket != num)
          {
            couponEffects_0.PartJacket = num;
            dbQuery.AddQuery("part_jacket", (object) couponEffects_0.PartJacket);
            break;
          }
          break;
        case 8:
          if (num != 0 && couponEffects_0.PartPocket != num)
          {
            couponEffects_0.PartPocket = num;
            dbQuery.AddQuery("part_pocket", (object) couponEffects_0.PartPocket);
            break;
          }
          break;
        case 9:
          if (num != 0 && couponEffects_0.PartGlove != num)
          {
            couponEffects_0.PartGlove = num;
            dbQuery.AddQuery("part_glove", (object) couponEffects_0.PartGlove);
            break;
          }
          break;
        case 10:
          if (num != 0 && couponEffects_0.PartBelt != num)
          {
            couponEffects_0.PartBelt = num;
            dbQuery.AddQuery("part_belt", (object) couponEffects_0.PartBelt);
            break;
          }
          break;
        case 11:
          if (num != 0 && couponEffects_0.PartHolster != num)
          {
            couponEffects_0.PartHolster = num;
            dbQuery.AddQuery("part_holster", (object) couponEffects_0.PartHolster);
            break;
          }
          break;
        case 12:
          if (num != 0 && couponEffects_0.PartSkin != num)
          {
            couponEffects_0.PartSkin = num;
            dbQuery.AddQuery("part_skin", (object) couponEffects_0.PartSkin);
            break;
          }
          break;
        case 13:
          if (couponEffects_0.BeretItem != num)
          {
            couponEffects_0.BeretItem = num;
            dbQuery.AddQuery("beret_item_part", (object) couponEffects_0.BeretItem);
            break;
          }
          break;
      }
    }
    ComDiv.UpdateDB("player_equipments", "owner_id", (object) int_0.PlayerId, dbQuery.GetTables(), dbQuery.GetValues());
  }

  public static void ValidateItemEquipment(Account Player, SortedList<int, int> Equip)
  {
    for (int index = 0; index < Equip.Keys.Count; ++index)
    {
      int Player1 = AllUtils.smethod_15(index, Equip);
      switch (index)
      {
        case 0:
          if (Player1 != 0 && Player.Equipment.DinoItem != Player1)
          {
            Player.Equipment.DinoItem = AllUtils.smethod_16(Player, Player1);
            ComDiv.UpdateDB("player_equipments", "dino_item_chara", (object) Player.Equipment.DinoItem, "owner_id", (object) Player.PlayerId);
            break;
          }
          break;
        case 1:
          if (Player.Equipment.SprayId != Player1)
          {
            Player.Equipment.SprayId = AllUtils.smethod_16(Player, Player1);
            ComDiv.UpdateDB("player_equipments", "spray_id", (object) Player.Equipment.SprayId, "owner_id", (object) Player.PlayerId);
            break;
          }
          break;
        case 2:
          if (Player.Equipment.NameCardId != Player1)
          {
            Player.Equipment.NameCardId = AllUtils.smethod_16(Player, Player1);
            ComDiv.UpdateDB("player_equipments", "namecard_id", (object) Player.Equipment.NameCardId, "owner_id", (object) Player.PlayerId);
            break;
          }
          break;
      }
    }
  }

  public static void ValidateCharacterSlot([In] Account obj0, [In] PlayerEquipment obj1, int[] EquipmentList)
  {
    DBQuery dbQuery = new DBQuery();
    CharacterModel characterSlot1 = obj0.Character.GetCharacterSlot(EquipmentList[0]);
    if (characterSlot1 != null && obj1.CharaRedId != characterSlot1.Id)
    {
      obj1.CharaRedId = AllUtils.smethod_16(obj0, characterSlot1.Id);
      dbQuery.AddQuery("chara_red_side", (object) obj1.CharaRedId);
    }
    CharacterModel characterSlot2 = obj0.Character.GetCharacterSlot(EquipmentList[1]);
    if (characterSlot2 != null && obj1.CharaBlueId != characterSlot2.Id)
    {
      obj1.CharaBlueId = AllUtils.smethod_16(obj0, characterSlot2.Id);
      dbQuery.AddQuery("chara_blue_side", (object) obj1.CharaBlueId);
    }
    ComDiv.UpdateDB("player_equipments", "owner_id", (object) obj0.PlayerId, dbQuery.GetTables(), dbQuery.GetValues());
  }

  public static PlayerEquipment ValidateRespawnEQ(SlotModel Player, int[] Items)
  {
    PlayerEquipment playerEquipment = new PlayerEquipment()
    {
      WeaponPrimary = Items[0],
      WeaponSecondary = Items[1],
      WeaponMelee = Items[2],
      WeaponExplosive = Items[3],
      WeaponSpecial = Items[4],
      PartHead = Items[6],
      PartFace = Items[7],
      PartJacket = Items[8],
      PartPocket = Items[9],
      PartGlove = Items[10],
      PartBelt = Items[11],
      PartHolster = Items[12],
      PartSkin = Items[13],
      BeretItem = Items[14],
      AccessoryId = Items[15],
      CharaRedId = Player.Equipment.CharaRedId,
      CharaBlueId = Player.Equipment.CharaBlueId,
      DinoItem = Player.Equipment.DinoItem
    };
    int idStatics1 = ComDiv.GetIdStatics(Items[5], 1);
    int idStatics2 = ComDiv.GetIdStatics(Items[5], 2);
    int idStatics3 = ComDiv.GetIdStatics(Items[5], 5);
    switch (idStatics1)
    {
      case 6:
        if (idStatics2 != 1 && idStatics3 != 632)
        {
          if (idStatics2 == 2 || idStatics3 == 664)
          {
            playerEquipment.CharaBlueId = Items[5];
            break;
          }
          break;
        }
        playerEquipment.CharaRedId = Items[5];
        break;
      case 15:
        playerEquipment.DinoItem = Items[5];
        break;
    }
    return playerEquipment;
  }

  public static void InsertItem(int Player, SlotModel Equip)
  {
    lock (Equip.ItemUsages)
    {
      if (Equip.ItemUsages.Contains(Player))
        return;
      Equip.ItemUsages.Add(Player);
    }
  }

  public static void ValidateBanPlayer([In] Account obj0, [In] string obj1)
  {
    if (ConfigLoader.AutoBan && DaoManagerSQL.SaveAutoBan(obj0.PlayerId, obj0.Username, obj0.Nickname, $"Cheat {obj1})", DateTimeUtil.Now("dd -MM-yyyy HH:mm:ss"), obj0.PublicIP.ToString(), "Illegal Program"))
    {
      using (PROTOCOL_LOBBY_CHATTING_ACK iasyncResult_0 = (PROTOCOL_LOBBY_CHATTING_ACK) new PROTOCOL_LOBBY_GET_ROOMINFOADD_ACK("Server", 0, 1, false, $"Permanently ban player [{obj0.Nickname}], {obj1}"))
        GameXender.Client.SendPacketToAllClients((GameServerPacket) iasyncResult_0);
      obj0.SendPacket((GameServerPacket) new PROTOCOL_AUTH_FIND_USER_ACK(2), false);
      obj0.Close(1000, true);
    }
    CLogger.Print($"Player: {obj0.Nickname}; Id: {obj0.PlayerId}; User: {obj0.Username}; Reason: {obj1}", LoggerType.Hack, (Exception) null);
  }

  public static bool ServerCommands([In] Account obj0, string ItemIds)
  {
    try
    {
      // ISSUE: reference to a compiler-generated method
      int num = CommandManager.Class14.TryParse(ItemIds, obj0) ? 1 : 0;
      if (num != 0)
        CLogger.Print($"Player '{obj0.Nickname}' (UID: {obj0.PlayerId}) Running Command '{ItemIds}'", LoggerType.Command, (Exception) null);
      return num != 0;
    }
    catch
    {
      obj0.SendPacket((GameServerPacket) new PROTOCOL_LOBBY_GET_ROOMINFOADD_ACK("Server", 0, 5, false, Translation.GetLabel("CommandsExceptionError")));
      return true;
    }
  }

  public static bool SlotValidMessage([In] SlotModel obj0, SlotModel Slot)
  {
    if ((obj0.State == SlotState.NORMAL || obj0.State == SlotState.READY) && (Slot.State == SlotState.NORMAL || Slot.State == SlotState.READY))
      return true;
    if (obj0.State < SlotState.LOAD || Slot.State < SlotState.LOAD)
      return false;
    if (Slot.SpecGM || obj0.SpecGM || obj0.DeathState.HasFlag((Enum) DeadEnum.UseChat) || obj0.DeathState.HasFlag((Enum) DeadEnum.Dead) && Slot.DeathState.HasFlag((Enum) DeadEnum.Dead) || obj0.Spectator && Slot.Spectator)
      return true;
    if (!obj0.DeathState.HasFlag((Enum) DeadEnum.Alive) || !Slot.DeathState.HasFlag((Enum) DeadEnum.Alive))
      return false;
    if (obj0.Spectator && Slot.Spectator)
      return true;
    return !obj0.Spectator && !Slot.Spectator;
  }

  public static bool PlayerIsBattle([In] Account obj0)
  {
    RoomModel room = obj0.Room;
    SlotModel slotModel;
    return room != null && room.GetSlot(obj0.SlotId, ref slotModel) && slotModel.State >= SlotState.READY;
  }

  public static void RoomPingSync(RoomModel Player)
  {
    if (ComDiv.GetDuration(Player.LastPingSync) < (double) ConfigLoader.PingUpdateTimeSeconds)
      return;
    byte[] roomModel_1 = new byte[18];
    for (int index = 0; index < 18; ++index)
      roomModel_1[index] = (byte) Player.Slots[index].Ping;
    using (PROTOCOL_BATTLE_SENDPING_ACK battleSendpingAck = (PROTOCOL_BATTLE_SENDPING_ACK) new PROTOCOL_BATTLE_STARTBATTLE_ACK(roomModel_1))
      Player.SendPacketToPlayers((GameServerPacket) battleSendpingAck, SlotState.BATTLE, 0);
    Player.LastPingSync = DateTimeUtil.Now();
  }

  public static List<ItemsModel> RepairableItems(
    [In] Account obj0,
    List<long> Text,
    [In] ref int obj2,
    [In] ref int obj3,
    [In] ref uint obj4)
  {
    obj2 = 0;
    obj3 = 0;
    obj4 = 0U;
    List<ItemsModel> itemsModelList = new List<ItemsModel>();
    if (Text.Count > 0)
    {
      foreach (long num in Text)
      {
        ItemsModel ObjectIds = obj0.Inventory.GetItem(num);
        if (ObjectIds != null)
        {
          uint[] numArray = AllUtils.smethod_18(obj0, ObjectIds);
          obj2 += (int) numArray[0];
          obj3 += (int) numArray[1];
          obj4 = numArray[2];
          itemsModelList.Add(ObjectIds);
        }
        else
          obj4 = 2147483920U /*0x80000110*/;
      }
    }
    return itemsModelList;
  }

  private static uint[] smethod_18(Account Player, ItemsModel ObjectIds)
  {
    uint[] numArray = new uint[3];
    ItemsRepair repairItem = ShopManager.GetRepairItem(ObjectIds.Id);
    if (repairItem != null)
    {
      uint num1 = repairItem.Quantity - ObjectIds.Count;
      if (repairItem.Point > repairItem.Cash)
      {
        uint num2 = (uint) ComDiv.Percentage(repairItem.Point, (int) num1);
        if ((long) Player.Gold < (long) num2)
        {
          numArray[2] = 2147483920U /*0x80000110*/;
          return numArray;
        }
        numArray[0] = num2;
      }
      else if (repairItem.Cash > repairItem.Point)
      {
        uint num3 = (uint) ComDiv.Percentage(repairItem.Cash, (int) num1);
        if ((long) Player.Cash < (long) num3)
        {
          numArray[2] = 2147483920U /*0x80000110*/;
          return numArray;
        }
        numArray[1] = num3;
      }
      else
      {
        numArray[2] = 2147483920U /*0x80000110*/;
        return numArray;
      }
      ObjectIds.Count = repairItem.Quantity;
      ComDiv.UpdateDB("player_items", "count", (object) (long) ObjectIds.Count, "owner_id", (object) Player.PlayerId, "id", (object) ObjectIds.Id);
      numArray[2] = 1U;
      return numArray;
    }
    numArray[2] = 2147483920U /*0x80000110*/;
    return numArray;
  }

  public static bool ChannelRequirementCheck([In] Account obj0, [In] ChannelModel obj1)
  {
    return !obj0.IsGM() && (obj1.Type == ChannelType.Clan && obj0.ClanId == 0 || obj1.Type == ChannelType.Novice && obj0.Statistic.GetKDRatio() > 40 && obj0.Statistic.GetSeasonKDRatio() > 40 || obj1.Type == ChannelType.Training && obj0.Rank >= 4 || obj1.Type == ChannelType.Special && obj0.Rank <= 25 || obj1.Type == ChannelType.Blocked);
  }

  public static bool ChangeCostume([In] SlotModel obj0, [In] TeamEnum obj1)
  {
    if (obj0.CostumeTeam != obj1)
      obj0.CostumeTeam = obj1;
    return obj0.CostumeTeam == obj1;
  }

  public static void ClassicModeCheck([In] RoomModel obj0, PlayerEquipment itemsModel_0)
  {
    if (!ConfigLoader.TournamentRule)
      return;
    TRuleModel truleModel = GameRuleXML.CheckTRuleByRoomName(obj0.Name);
    if (truleModel == null || truleModel.BanIndexes.Count <= 0)
      return;
    foreach (int banIndex in truleModel.BanIndexes)
    {
      if (GameRuleXML.IsBlocked(banIndex, itemsModel_0.WeaponPrimary))
        itemsModel_0.WeaponPrimary = 103004;
      else if (GameRuleXML.IsBlocked(banIndex, itemsModel_0.WeaponSecondary))
        itemsModel_0.WeaponSecondary = 202003;
      else if (GameRuleXML.IsBlocked(banIndex, itemsModel_0.WeaponMelee))
        itemsModel_0.WeaponMelee = 301001;
      else if (GameRuleXML.IsBlocked(banIndex, itemsModel_0.WeaponExplosive))
        itemsModel_0.WeaponExplosive = 407001;
      else if (GameRuleXML.IsBlocked(banIndex, itemsModel_0.WeaponSpecial))
        itemsModel_0.WeaponSpecial = 508001;
      else if (GameRuleXML.IsBlocked(banIndex, itemsModel_0.PartHead))
        itemsModel_0.PartHead = 1000700000;
      else if (GameRuleXML.IsBlocked(banIndex, itemsModel_0.PartFace))
        itemsModel_0.PartFace = 1000800000;
      else if (GameRuleXML.IsBlocked(banIndex, itemsModel_0.PartJacket))
        itemsModel_0.PartJacket = 1000900000;
      else if (GameRuleXML.IsBlocked(banIndex, itemsModel_0.PartPocket))
        itemsModel_0.PartPocket = 1001000000;
      else if (GameRuleXML.IsBlocked(banIndex, itemsModel_0.PartGlove))
        itemsModel_0.PartGlove = 1001100000;
      else if (GameRuleXML.IsBlocked(banIndex, itemsModel_0.PartBelt))
        itemsModel_0.PartBelt = 1001200000;
      else if (GameRuleXML.IsBlocked(banIndex, itemsModel_0.PartHolster))
        itemsModel_0.PartHolster = 1001300000;
      else if (GameRuleXML.IsBlocked(banIndex, itemsModel_0.PartSkin))
        itemsModel_0.PartSkin = 1001400000;
      else if (GameRuleXML.IsBlocked(banIndex, itemsModel_0.BeretItem))
        itemsModel_0.BeretItem = 0;
      else if (GameRuleXML.IsBlocked(banIndex, itemsModel_0.DinoItem))
        itemsModel_0.DinoItem = 1500511;
      else if (GameRuleXML.IsBlocked(banIndex, itemsModel_0.AccessoryId))
        itemsModel_0.AccessoryId = 0;
      else if (GameRuleXML.IsBlocked(banIndex, itemsModel_0.SprayId))
        itemsModel_0.SprayId = 0;
      else if (GameRuleXML.IsBlocked(banIndex, itemsModel_0.NameCardId))
        itemsModel_0.NameCardId = 0;
    }
  }

  public static bool ClassicModeCheck([In] Account obj0, RoomModel Channel)
  {
    TRuleModel truleModel = GameRuleXML.CheckTRuleByRoomName(Channel.Name);
    if (truleModel == null)
      return false;
    PlayerEquipment equipment = obj0.Equipment;
    if (equipment == null)
    {
      CLogger.Print($"Player '{obj0.Nickname}' has invalid equipment (Error) on {(ConfigLoader.TournamentRule ? "Enabled" : "Disabled")} Tournament Rules!", LoggerType.Warning, (Exception) null);
      return false;
    }
    List<string> stringList = new List<string>();
    if (truleModel.BanIndexes.Count > 0)
    {
      foreach (int banIndex in truleModel.BanIndexes)
      {
        if (!GameRuleXML.IsBlocked(banIndex, equipment.WeaponPrimary, ref stringList, Translation.GetLabel("Primary")) && !GameRuleXML.IsBlocked(banIndex, equipment.WeaponSecondary, ref stringList, Translation.GetLabel("Secondary")) && !GameRuleXML.IsBlocked(banIndex, equipment.WeaponMelee, ref stringList, Translation.GetLabel("Melee")) && !GameRuleXML.IsBlocked(banIndex, equipment.WeaponExplosive, ref stringList, Translation.GetLabel("Explosive")) && !GameRuleXML.IsBlocked(banIndex, equipment.WeaponSpecial, ref stringList, Translation.GetLabel("Special")) && !GameRuleXML.IsBlocked(banIndex, equipment.CharaRedId, ref stringList, Translation.GetLabel("Character")) && !GameRuleXML.IsBlocked(banIndex, equipment.CharaBlueId, ref stringList, Translation.GetLabel("Character")) && !GameRuleXML.IsBlocked(banIndex, equipment.PartHead, ref stringList, Translation.GetLabel("PartHead")) && !GameRuleXML.IsBlocked(banIndex, equipment.PartFace, ref stringList, Translation.GetLabel("PartFace")) && !GameRuleXML.IsBlocked(banIndex, equipment.BeretItem, ref stringList, Translation.GetLabel("BeretItem")))
          GameRuleXML.IsBlocked(banIndex, equipment.AccessoryId, ref stringList, Translation.GetLabel("Accessory"));
      }
    }
    if (stringList.Count <= 0)
      return false;
    obj0.SendPacket((GameServerPacket) new PROTOCOL_SERVER_MESSAGE_DISCONNECTED_HACK(Translation.GetLabel("ClassicModeWarn", new object[1]
    {
      (object) string.Join(", ", stringList.ToArray())
    })));
    return true;
  }

  public static bool Check4vs4(
    [In] RoomModel obj0,
    bool CostumeTeam,
    [In] ref int obj2,
    [In] ref int obj3,
    [In] ref int obj4)
  {
    if (!CostumeTeam)
      return obj2 + obj3 >= 8;
    int num1 = obj2 + obj3 + 1;
    if (num1 > 8)
    {
      int num2 = num1 - 8;
      if (num2 > 0)
      {
        for (int int_0 = 15; int_0 >= 0; --int_0)
        {
          if (int_0 != obj0.LeaderSlot)
          {
            SlotModel slot = obj0.GetSlot(int_0);
            if (slot != null && slot.State == SlotState.READY)
            {
              obj0.ChangeSlotState(int_0, SlotState.NORMAL, false);
              if (int_0 % 2 == 0)
                --obj2;
              else
                --obj3;
              if (--num2 == 0)
                break;
            }
          }
        }
        obj0.UpdateSlotsInfo();
        obj4 = obj0.LeaderSlot % 2 != 0 ? obj2 : obj3;
        return true;
      }
    }
    return false;
  }

  public static void UpdateSeasonPass(Account Room)
  {
    if (SeasonChallengeXML.GetActiveSeasonPass() == null || !Room.UpdateSeasonpass)
      return;
    Room.UpdateSeasonpass = false;
    Room.SendPacket((GameServerPacket) new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK());
    Room.SendPacket((GameServerPacket) new PROTOCOL_SEASON_CHALLENGE_SEASON_CHANGE(Room));
  }

  public static void CalculateBattlePass(
    [In] Account obj0,
    SlotModel IsLeader,
    BattlePassModel PlayerFR)
  {
    PlayerBattlepass battlepass = obj0.Battlepass;
    if (PlayerFR == null || battlepass == null)
      return;
    if (battlepass.Id == PlayerFR.Id)
    {
      if (battlepass.Level >= PlayerFR.Cards.Count)
      {
        obj0.UpdateSeasonpass = true;
      }
      else
      {
        IsLeader.SeasonPoint += ComDiv.Percentage(IsLeader.Exp, 35);
        int num1 = IsLeader.SeasonPoint + ComDiv.Percentage(IsLeader.SeasonPoint, IsLeader.BonusBattlePass);
        battlepass.TotalPoints += num1;
        battlepass.DailyPoints += num1;
        uint num2 = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
        if (ComDiv.UpdateDB("player_battlepass", "owner_id", (object) obj0.PlayerId, new string[3]
        {
          "total_points",
          "daily_points",
          "last_record"
        }, new object[3]
        {
          (object) battlepass.TotalPoints,
          (object) battlepass.DailyPoints,
          (object) (long) num2
        }))
          battlepass.LastRecord = num2;
        obj0.UpdateSeasonpass = true;
      }
    }
    AllUtils.smethod_19(obj0, battlepass, PlayerFR);
  }

  private static void smethod_19([In] Account obj0, [In] PlayerBattlepass obj1, [In] BattlePassModel obj2)
  {
    PassBoxModel card = obj2.Cards[obj1.Level];
    if (!obj2.SeasonIsEnabled() || card == null || obj1.TotalPoints < card.RequiredPoints)
      return;
    int ValueReq1 = obj1.Level + 1;
    if (ComDiv.UpdateDB("player_battlepass", "level", (object) ValueReq1, "owner_id", (object) obj0.PlayerId))
      obj1.Level = ValueReq1;
    int[] numArray1 = new int[3];
    PassItemModel normal = card.Normal;
    numArray1[0] = normal != null ? normal.GoodId : 0;
    int[] Closing = numArray1;
    if (obj1.IsPremium)
    {
      int[] numArray2 = Closing;
      PassItemModel premiumA = card.PremiumA;
      int goodId1 = premiumA != null ? premiumA.GoodId : 0;
      numArray2[1] = goodId1;
      int[] numArray3 = Closing;
      PassItemModel premiumB = card.PremiumB;
      int goodId2 = premiumB != null ? premiumB.GoodId : 0;
      numArray3[2] = goodId2;
    }
    // ISSUE: reference to a compiler-generated method
    AllUtils.Class5.smethod_22(obj0, Closing);
  }

  public static void ProcessBattlepassPremiumBuy([In] Account obj0)
  {
    PlayerBattlepass battlepass = obj0.Battlepass;
    if (battlepass == null)
      return;
    BattlePassModel seasonPass = SeasonChallengeXML.GetSeasonPass(battlepass.Id);
    if (seasonPass == null)
      return;
    battlepass.IsPremium = true;
    for (int index = 0; index < battlepass.Level; ++index)
    {
      PassBoxModel card = seasonPass.Cards[index];
      int[] numArray = new int[3];
      PassItemModel premiumA = card.PremiumA;
      numArray[1] = premiumA != null ? premiumA.GoodId : 0;
      PassItemModel premiumB = card.PremiumB;
      numArray[2] = premiumB != null ? premiumB.GoodId : 0;
      int[] Closing = numArray;
      // ISSUE: reference to a compiler-generated method
      AllUtils.Class5.smethod_22(obj0, Closing);
    }
    ComDiv.UpdateDB("player_battlepass", "premium", (object) battlepass.IsPremium, "owner_id", (object) obj0.PlayerId);
  }

  public static void SendCompetitiveInfo([In] Account obj0)
  {
    try
    {
      obj0.SendPacket((GameServerPacket) new PROTOCOL_LOBBY_GET_ROOMINFOADD_ACK(Translation.GetLabel("Competitive"), obj0.Session.SessionId, obj0.NickColor, true, Translation.GetLabel("CompetitiveRank", new object[3]
      {
        (object) obj0.Competitive.Rank().Name,
        (object) obj0.Competitive.Points,
        (object) obj0.Competitive.Rank().Points
      })));
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.ToString(), LoggerType.Error, (Exception) null);
    }
  }

  public static void CalculateCompetitive(
    RoomModel account_0,
    Account playerBattlepass_0,
    SlotModel battlePassModel_0,
    [In] bool obj3)
  {
    if (!account_0.Competitive)
      return;
    int num = (obj3 ? 50 : -30) + 2 * battlePassModel_0.AllKills + battlePassModel_0.AllAssists - battlePassModel_0.AllDeaths;
    playerBattlepass_0.Competitive.Points += num;
    if (playerBattlepass_0.Competitive.Points < 0)
      playerBattlepass_0.Competitive.Points = 0;
    AllUtils.smethod_20(playerBattlepass_0.Competitive);
    string label1 = Translation.GetLabel("CompetitivePointsEarned", new object[1]
    {
      (object) num
    });
    string label2 = Translation.GetLabel("CompetitiveRank", new object[3]
    {
      (object) playerBattlepass_0.Competitive.Rank().Name,
      (object) playerBattlepass_0.Competitive.Points,
      (object) playerBattlepass_0.Competitive.Rank().Points
    });
    playerBattlepass_0.SendPacket((GameServerPacket) new PROTOCOL_LOBBY_GET_ROOMINFOADD_ACK(Translation.GetLabel("Competitive"), playerBattlepass_0.Session.SessionId, playerBattlepass_0.NickColor, true, $"{label1}\n\r{label2}"));
  }

  private static void smethod_20(PlayerCompetitive Player)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    AllUtils.Class6 class6 = (AllUtils.Class6) new AllUtils.Class8();
    // ISSUE: reference to a compiler-generated field
    class6.playerCompetitive_0 = Player;
    // ISSUE: reference to a compiler-generated method
    CompetitiveRank competitiveRank = CompetitiveXML.Ranks.FirstOrDefault<CompetitiveRank>(new System.Func<CompetitiveRank, bool>(((AllUtils.Class9) class6).method_0));
    // ISSUE: reference to a compiler-generated field
    int ValueReq1 = competitiveRank == null ? class6.playerCompetitive_0.Level : competitiveRank.Id;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    ComDiv.UpdateDB("player_competitive", "points", (object) class6.playerCompetitive_0.Points, "owner_id", (object) class6.playerCompetitive_0.OwnerId);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    if (ValueReq1 == class6.playerCompetitive_0.Level || !ComDiv.UpdateDB("player_competitive", "level", (object) ValueReq1, "owner_id", (object) class6.playerCompetitive_0.OwnerId))
      return;
    // ISSUE: reference to a compiler-generated field
    class6.playerCompetitive_0.Level = ValueReq1;
  }

  public static bool CanOpenSlotCompetitive(RoomModel Room, SlotModel Player)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    AllUtils.Class7 class7 = (AllUtils.Class7) new AllUtils.Class9();
    // ISSUE: reference to a compiler-generated field
    class7.slotModel_0 = Player;
    return ((IEnumerable<SlotModel>) Room.Slots).Where<SlotModel>(new System.Func<SlotModel, bool>(((GameSync) class7).method_0)).Count<SlotModel>() < 5;
  }

  public static bool CanCloseSlotCompetitive([In] RoomModel obj0, [In] SlotModel obj1)
  {
    // ISSUE: variable of a compiler-generated type
    AllUtils.Class8 class8 = (AllUtils.Class8) new GameSync();
    // ISSUE: reference to a compiler-generated field
    class8.slotModel_0 = obj1;
    return ((IEnumerable<SlotModel>) obj0.Slots).Where<SlotModel>(new System.Func<SlotModel, bool>(((GameSync) class8).method_0)).Count<SlotModel>() > 3;
  }

  private static void smethod_21(Account playerCompetitive_0)
  {
    List<ItemsModel> items = playerCompetitive_0.Inventory.Items;
    lock (items)
    {
      foreach (ItemsModel Opening in items)
      {
        if (ComDiv.GetIdStatics(Opening.Id, 1) == 6 && playerCompetitive_0.Character.GetCharacter(Opening.Id) == null)
          AllUtils.CreateCharacter(playerCompetitive_0, Opening);
      }
    }
  }

  public static void CreateCharacter(Account Room, ItemsModel Opening)
  {
    CharacterModel OwnerId = new CharacterModel()
    {
      Id = Opening.Id,
      Name = Opening.Name,
      Slot = Room.Character.GenSlotId(Opening.Id),
      CreateDate = uint.Parse(DateTimeUtil.Now("yyMMddHHmm")),
      PlayTime = 0
    };
    Room.Character.AddCharacter(OwnerId);
    Room.SendPacket((GameServerPacket) new PROTOCOL_LOBBY_CHATTING_ACK(0, Room, Opening));
    if (DaoManagerSQL.CreatePlayerCharacter(OwnerId, Room.PlayerId))
      Room.SendPacket((GameServerPacket) new PROTOCOL_CLAN_WAR_CANCEL_MATCHMAKING_ACK(0U, (byte) 3, OwnerId, Room));
    else
      Room.SendPacket((GameServerPacket) new PROTOCOL_CLAN_WAR_CANCEL_MATCHMAKING_ACK(2147483648U /*0x80000000*/, byte.MaxValue, (CharacterModel) null, (Account) null));
  }
}
