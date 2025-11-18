// Decompiled with JetBrains decompiler
// Type: Server.Auth.Data.Utils.AllUtils
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Auth.Data.Managers;
using Server.Auth.Data.Models;
using Server.Auth.Data.Sync;
using Server.Auth.Data.XML;
using Server.Auth.Network;
using Server.Auth.Network.ServerPacket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;

#nullable disable
namespace Server.Auth.Data.Utils;

public static class AllUtils
{
  public static ChannelModel GetChannel([In] int obj0, int syncServerPacket_0)
  {
    lock (ChannelsXML.Channels)
    {
      foreach (ChannelModel channel in ChannelsXML.Channels)
      {
        if (channel.ServerId == obj0 && channel.Id == syncServerPacket_0)
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

  private static void smethod_0(string dbquery_0)
  {
    XmlDocument xmlDocument = new XmlDocument();
    using (FileStream inStream = new FileStream(dbquery_0, FileMode.Open))
    {
      if (inStream.Length == 0L)
      {
        CLogger.Print("File is empty: " + dbquery_0, LoggerType.Warning, (Exception) null);
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
                  int playerConfig_0 = int.Parse(xmlNode2.Attributes.GetNamedItem("ServerId").Value);
                  AllUtils.smethod_1(xmlNode2, playerConfig_0);
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

  private static void smethod_1([In] XmlNode obj0, int playerConfig_0)
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
            AccountManager accountManager = new AccountManager(playerConfig_0);
            ((ChannelModel) accountManager).Id = int.Parse(attributes.GetNamedItem("Id").Value);
            ((ChannelModel) accountManager).Type = ComDiv.ParseEnum<ChannelType>(attributes.GetNamedItem("Type").Value);
            ((ChannelModel) accountManager).MaxRooms = int.Parse(attributes.GetNamedItem("MaxRooms").Value);
            ((ChannelModel) accountManager).ExpBonus = int.Parse(attributes.GetNamedItem("ExpBonus").Value);
            ((ChannelModel) accountManager).GoldBonus = int.Parse(attributes.GetNamedItem("GoldBonus").Value);
            accountManager.set_CashBonus(int.Parse(attributes.GetNamedItem("CashBonus").Value));
            ChannelModel channelModel = (ChannelModel) accountManager;
            try
            {
              if (channelModel.Type == ChannelType.CH_PW)
                ((AccountManager) channelModel).set_Password(attributes.GetNamedItem("Password").Value);
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
                ((AccountManager) channel).set_CashBonus(((AccountManager) channelModel).get_CashBonus());
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
      AuthSync.smethod_2(xmlNode_0);
    string int_0;
    if (AllUtils.smethod_0(xmlNode_0, ref int_0))
    {
      List<ItemsModel> pcCafeRewards = TemplatePackXML.GetPCCafeRewards(xmlNode_0.CafePC);
      lock (xmlNode_0.Inventory.Items)
        xmlNode_0.Inventory.Items.AddRange((IEnumerable<ItemsModel>) pcCafeRewards);
      foreach (ItemsModel itemsModel in pcCafeRewards)
      {
        if (ComDiv.GetIdStatics(itemsModel.Id, 1) == 6 && xmlNode_0.Character.GetCharacter(itemsModel.Id) == null)
          AuthSync.CreateCharacter(xmlNode_0, itemsModel);
        if (ComDiv.GetIdStatics(itemsModel.Id, 1) == 16 /*0x10*/)
        {
          CouponFlag couponEffect = CouponEffectXML.GetCouponEffect(itemsModel.Id);
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
          AllUtils.smethod_1(xmlNode_0, pcCafeReward.Id);
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

  public static void LoadPlayerBonus([In] Account obj0)
  {
    PlayerBonus playerBonusDb = DaoManagerSQL.GetPlayerBonusDB(obj0.PlayerId);
    if (playerBonusDb != null)
    {
      obj0.Bonus = playerBonusDb;
    }
    else
    {
      if (DaoManagerSQL.CreatePlayerBonusDB(obj0.PlayerId))
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

  public static bool DiscountPlayerItems(Account Player)
  {
    try
    {
      bool flag = false;
      uint uint32 = Convert.ToUInt32(DateTimeUtil.Now("yyMMddHHmm"));
      List<object> objectList = new List<object>();
      int bonuses = Player.Bonus != null ? Player.Bonus.Bonuses : 0;
      int freePass = Player.Bonus != null ? Player.Bonus.FreePass : 0;
      lock (Player.Inventory.Items)
      {
        for (int index = 0; index < Player.Inventory.Items.Count; ++index)
        {
          ItemsModel itemsModel = Player.Inventory.Items[index];
          if (itemsModel.Count <= uint32 && itemsModel.Equip == ItemEquipType.Temporary)
          {
            if (itemsModel.Category == ItemCategory.Coupon)
            {
              if (Player.Bonus != null)
              {
                if (!Player.Bonus.RemoveBonuses(itemsModel.Id))
                {
                  if (itemsModel.Id == 1600014)
                  {
                    ComDiv.UpdateDB("player_bonus", "crosshair_color", (object) 4, "owner_id", (object) Player.PlayerId);
                    Player.Bonus.CrosshairColor = 4;
                  }
                  else if (itemsModel.Id == 1600006)
                  {
                    ComDiv.UpdateDB("accounts", "nick_color", (object) 0, "player_id", (object) Player.PlayerId);
                    Player.NickColor = 0;
                  }
                  else if (itemsModel.Id == 1600009)
                  {
                    ComDiv.UpdateDB("player_bonus", "fake_rank", (object) 55, "owner_id", (object) Player.PlayerId);
                    Player.Bonus.FakeRank = 55;
                  }
                  else if (itemsModel.Id == 1600010)
                  {
                    if (Player.Bonus.FakeNick.Length > 0)
                    {
                      ComDiv.UpdateDB("player_bonus", "fake_nick", (object) "", "owner_id", (object) Player.PlayerId);
                      ComDiv.UpdateDB("accounts", "nickname", (object) Player.Bonus.FakeNick, "player_id", (object) Player.PlayerId);
                      Player.Nickname = Player.Bonus.FakeNick;
                      Player.Bonus.FakeNick = "";
                    }
                  }
                  else if (itemsModel.Id == 1600187)
                  {
                    ComDiv.UpdateDB("player_bonus", "muzzle_color", (object) 0, "owner_id", (object) Player.PlayerId);
                    Player.Bonus.MuzzleColor = 0;
                  }
                  else if (itemsModel.Id == 1600205)
                  {
                    ComDiv.UpdateDB("player_bonus", "nick_border_color", (object) 0, "owner_id", (object) Player.PlayerId);
                    Player.Bonus.NickBorderColor = 0;
                  }
                }
                CouponFlag couponEffect = CouponEffectXML.GetCouponEffect(itemsModel.Id);
                if (couponEffect != null && couponEffect.EffectFlag > (CouponEffects) 0 && Player.Effects.HasFlag((Enum) couponEffect.EffectFlag))
                {
                  Player.Effects -= couponEffect.EffectFlag;
                  flag = true;
                }
              }
              else
                continue;
            }
            objectList.Add((object) itemsModel.ObjectId);
            Player.Inventory.Items.RemoveAt(index--);
          }
          else if (itemsModel.Count == 0U)
          {
            objectList.Add((object) itemsModel.ObjectId);
            Player.Inventory.Items.RemoveAt(index--);
          }
        }
      }
      if (objectList.Count > 0)
      {
        for (int index = 0; index < objectList.Count; ++index)
        {
          ItemsModel itemsModel = Player.Inventory.GetItem((long) objectList[index]);
          if (itemsModel != null && itemsModel.Category == ItemCategory.Character && ComDiv.GetIdStatics(itemsModel.Id, 1) == 6)
            AllUtils.smethod_1(Player, itemsModel.Id);
        }
        ComDiv.DeleteDB("player_items", "object_id", objectList.ToArray(), "owner_id", (object) Player.PlayerId);
      }
      objectList.Clear();
      if (Player.Bonus != null && (Player.Bonus.Bonuses != bonuses || Player.Bonus.FreePass != freePass))
        DaoManagerSQL.UpdatePlayerBonus(Player.PlayerId, Player.Bonus.Bonuses, Player.Bonus.FreePass);
      if (Player.Effects < (CouponEffects) 0)
        Player.Effects = (CouponEffects) 0;
      if (flag)
        ComDiv.UpdateDB("accounts", "coupon_effect", (object) (long) Player.Effects, "player_id", (object) Player.PlayerId);
      int num = ComDiv.CheckEquipedItems(Player.Equipment, Player.Inventory.Items, false);
      if (num > 0)
      {
        DBQuery valueTuple_0 = new DBQuery();
        if ((num & 2) == 2)
          ComDiv.UpdateWeapons(Player.Equipment, valueTuple_0);
        if ((num & 1) == 1)
          ComDiv.UpdateChars(Player.Equipment, valueTuple_0);
        if ((num & 3) == 3)
          ComDiv.UpdateItems(Player.Equipment, valueTuple_0);
        ComDiv.UpdateDB("player_equipments", "owner_id", (object) Player.PlayerId, valueTuple_0.GetTables(), valueTuple_0.GetValues());
      }
      return true;
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  private static void smethod_1(Account Player, [In] int obj1)
  {
    CharacterModel character1 = Player.Character.GetCharacter(obj1);
    if (character1 == null)
      return;
    int OwnerId = 0;
    foreach (CharacterModel character2 in Player.Character.Characters)
    {
      if (character2.Slot != character1.Slot)
      {
        character2.Slot = OwnerId;
        DaoManagerSQL.UpdatePlayerCharacter(OwnerId, character2.ObjectId, Player.PlayerId);
        ++OwnerId;
      }
    }
    if (!DaoManagerSQL.DeletePlayerCharacter(character1.ObjectId, Player.PlayerId))
      return;
    Player.Character.RemoveCharacter(character1);
  }

  public static void CheckGameEvents(Account Player)
  {
    uint[] numArray = new uint[2]
    {
      uint.Parse(DateTimeUtil.Now("yyMMddHHmm")),
      uint.Parse(DateTimeUtil.Now("yyMMdd"))
    };
    PlayerEvent BoostType = Player.Event;
    if (BoostType != null)
    {
      EventQuestModel runningEvent1 = EventQuestXML.GetRunningEvent();
      if (runningEvent1 != null)
      {
        uint lastQuestDate = BoostType.LastQuestDate;
        int lastQuestFinish = BoostType.LastQuestFinish;
        if (BoostType.LastQuestDate < runningEvent1.BeginDate)
        {
          BoostType.LastQuestDate = 0U;
          BoostType.LastQuestFinish = 0;
        }
        if (BoostType.LastQuestFinish == 0)
        {
          Player.Mission.Mission4 = 13;
          if (BoostType.LastQuestDate == 0U)
            BoostType.LastQuestDate = numArray[0];
        }
        if ((int) BoostType.LastQuestDate != (int) lastQuestDate || BoostType.LastQuestFinish != lastQuestFinish)
          EventQuestXML.ResetPlayerEvent(Player.PlayerId, BoostType);
      }
      EventLoginModel runningEvent2 = EventLoginXML.GetRunningEvent();
      if (runningEvent2 != null)
      {
        if (BoostType.LastLoginDate < runningEvent2.BeginDate)
        {
          BoostType.LastLoginDate = 0U;
          ComDiv.UpdateDB("player_events", "last_login_date", (object) 0, "owner_id", (object) Player.PlayerId);
        }
        if (uint.Parse($"{DateTimeUtil.Convert($"{BoostType.LastLoginDate}"):yyMMdd}") < numArray[1])
        {
          foreach (int good1 in runningEvent2.Goods)
          {
            GoodsItem good2 = ShopManager.GetGood(good1);
            if (good2 != null)
              ComDiv.TryCreateItem(new ItemsModel(good2.Item), Player.Inventory, Player.PlayerId);
          }
          ComDiv.UpdateDB("player_events", "last_login_date", (object) (long) numArray[0], "owner_id", (object) Player.PlayerId);
          Player.SendPacket((AuthServerPacket) new PROTOCOL_SERVER_MESSAGE_ERROR_ACK(Translation.GetLabel("LoginGiftMessage")));
        }
      }
      EventVisitModel runningEvent3 = EventVisitXML.GetRunningEvent();
      if (runningEvent3 != null && BoostType.LastVisitDate < runningEvent3.BeginDate)
      {
        BoostType.LastVisitDate = 0U;
        BoostType.LastVisitCheckDay = 0;
        BoostType.LastVisitSeqType = 0;
        EventVisitXML.ResetPlayerEvent(Player.PlayerId, BoostType);
      }
      EventXmasModel runningEvent4 = EventXmasXML.GetRunningEvent();
      if (runningEvent4 != null && BoostType.LastXmasDate < runningEvent4.BeginDate)
      {
        BoostType.LastXmasDate = 0U;
        ComDiv.UpdateDB("player_events", "last_xmas_date", (object) 0, "owner_id", (object) Player.PlayerId);
      }
      EventPlaytimeModel runningEvent5 = EventPlaytimeXML.GetRunningEvent();
      if (runningEvent5 != null)
      {
        if (BoostType.LastPlaytimeDate < runningEvent5.BeginDate)
        {
          BoostType.LastPlaytimeDate = 0U;
          BoostType.LastPlaytimeFinish = 0;
          BoostType.LastPlaytimeValue = 0L;
          EventPlaytimeXML.ResetPlayerEvent(Player.PlayerId, BoostType);
        }
        if (uint.Parse($"{DateTimeUtil.Convert($"{BoostType.LastPlaytimeDate}"):yyMMdd}") < numArray[1])
        {
          BoostType.LastPlaytimeValue = 0L;
          BoostType.LastPlaytimeFinish = 0;
          EventPlaytimeXML.ResetPlayerEvent(Player.PlayerId, BoostType);
        }
      }
    }
    ComDiv.UpdateDB("accounts", "last_login_date", (object) (long) numArray[0], "player_id", (object) Player.PlayerId);
  }

  public static void ProcessBattlepass(Account Player)
  {
    BattlePassModel activeSeasonPass = SeasonChallengeXML.GetActiveSeasonPass();
    if (activeSeasonPass == null)
      return;
    PlayerBattlepass battlepass = Player.Battlepass;
    if (battlepass == null)
      return;
    if (battlepass.Id != activeSeasonPass.Id)
    {
      battlepass.Id = activeSeasonPass.Id;
      battlepass.IsPremium = false;
      battlepass.Level = 0;
      battlepass.TotalPoints = 0;
      battlepass.DailyPoints = 0;
      battlepass.LastRecord = 0U;
      ComDiv.UpdateDB("player_battlepass", "owner_id", (object) Player.PlayerId, new string[6]
      {
        "id",
        "level",
        "premium",
        "total_points",
        "daily_points",
        "last_record"
      }, new object[6]
      {
        (object) battlepass.Id,
        (object) battlepass.Level,
        (object) battlepass.IsPremium,
        (object) battlepass.TotalPoints,
        (object) battlepass.DailyPoints,
        (object) (long) battlepass.LastRecord
      });
    }
    (int, int, int, int) levelProgression = activeSeasonPass.GetLevelProgression(battlepass.TotalPoints);
    if (battlepass.Level != levelProgression.Item1)
    {
      battlepass.Level = levelProgression.Item1;
      ComDiv.UpdateDB("player_battlepass", "level", (object) battlepass.Level, "owner_id", (object) Player.PlayerId);
    }
    if (uint.Parse($"{DateTimeUtil.Convert($"{battlepass.LastRecord}"):yyMMdd}") >= uint.Parse(DateTimeUtil.Now("yyMMdd")))
      return;
    battlepass.DailyPoints = 0;
    battlepass.LastRecord = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
    ComDiv.UpdateDB("player_battlepass", "owner_id", (object) Player.PlayerId, new string[2]
    {
      "daily_points",
      "last_record"
    }, new object[2]
    {
      (object) battlepass.DailyPoints,
      (object) (long) battlepass.LastRecord
    });
  }

  public static long LoadCouponEffects(Account Player)
  {
    long num = 0;
    foreach ((CouponEffects, long) valueTuple in new List<(CouponEffects, long)>()
    {
      (CouponEffects.Ammo40, 1L),
      (CouponEffects.Ammo10, 2L),
      (CouponEffects.GetDroppedWeapon, 4L),
      (CouponEffects.QuickChangeWeapon, 16L /*0x10*/),
      (CouponEffects.QuickChangeReload, 128L /*0x80*/),
      (CouponEffects.Invincible, 512L /*0x0200*/),
      (CouponEffects.FullMetalJack, 2048L /*0x0800*/),
      (CouponEffects.HollowPoint, 8192L /*0x2000*/),
      (CouponEffects.HollowPointPlus, 32768L /*0x8000*/),
      (CouponEffects.C4SpeedKit, 65536L /*0x010000*/),
      (CouponEffects.ExtraGrenade, 131072L /*0x020000*/),
      (CouponEffects.ExtraThrowGrenade, 262144L /*0x040000*/),
      (CouponEffects.JackHollowPoint, 524288L /*0x080000*/),
      (CouponEffects.HP5, 1048576L /*0x100000*/),
      (CouponEffects.HP10, 2097152L /*0x200000*/),
      (CouponEffects.Defense5, 4194304L /*0x400000*/),
      (CouponEffects.Defense10, 8388608L /*0x800000*/),
      (CouponEffects.Defense20, 16777216L /*0x01000000*/),
      (CouponEffects.Defense90, 33554432L /*0x02000000*/),
      (CouponEffects.Respawn20, 67108864L /*0x04000000*/),
      (CouponEffects.Respawn30, 268435456L /*0x10000000*/),
      (CouponEffects.Respawn50, 1073741824L /*0x40000000*/),
      (CouponEffects.Respawn100, 8589934592L /*0x0200000000*/),
      (CouponEffects.Camoflage50, 34359738368L /*0x0800000000*/),
      (CouponEffects.Camoflage99, 68719476736L /*0x1000000000*/)
    })
    {
      if (Player.Effects.HasFlag((Enum) valueTuple.Item1))
        num += valueTuple.Item2;
    }
    return num;
  }
}
