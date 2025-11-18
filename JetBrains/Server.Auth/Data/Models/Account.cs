// Decompiled with JetBrains decompiler
// Type: Server.Auth.Data.Models.Account
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Auth.Data.Managers;
using Server.Auth.Data.Sync.Update;
using Server.Auth.Data.Utils;
using Server.Auth.Network;
using Server.Auth.Network.ServerPacket;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;

#nullable disable
namespace Server.Auth.Data.Models;

public class Account
{
  public CountryFlags Country;
  public long PlayerId;
  public long BanObjectId;
  public string Nickname;
  public string Password;
  public string Username;
  public string HardwareId;
  public string Email;
  public uint LastRankUpDate;
  public int InventoryPlus;
  public int Exp;
  public int Gold;
  public int ClanId;
  public int ClanAccess;
  public int Cash;
  public int Rank;
  public int Ribbon;
  public int Ensign;
  public int Medal;
  public int MasterMedal;
  public int NickColor;
  public int Age;
  public int Tags;
  public bool MyConfigsLoaded;
  public bool IsOnline;
  public AccessLevel Access;
  public CouponEffects Effects;
  public CafeEnum CafePC;
  public PhysicalAddress MacAddress;
  public AuthClient Connection;
  public PlayerBonus Bonus;
  public PlayerConfig Config;
  public PlayerEvent Event;
  public PlayerTitles Title;
  public PlayerInventory Inventory;
  public AccountStatus Status;
  public PlayerFriends Friend;
  public PlayerStatistic Statistic;
  public PlayerQuickstart Quickstart;
  public PlayerCharacters Character;
  public PlayerEquipment Equipment;
  public PlayerMissions Mission;
  public PlayerReport Report;
  public PlayerBattlepass Battlepass;
  public PlayerCompetitive Competitive;
  public List<Account> ClanPlayers;
  public List<PlayerTopup> TopUps;
  public DateTime LastLoginDate;

  public Account()
  {
  }

  public static void LoadGMWarning(SyncClientPacket C)
  {
    string str1 = C.ReadS((int) C.ReadC());
    string str2 = C.ReadS((int) C.ReadC());
    string str3 = C.ReadS((int) C.ReadH());
    Account accountDb = AccountManager.GetAccountDB((object) str1, (object) str2, 2, 31 /*0x1F*/);
    if (accountDb == null || !(accountDb.Password == str2) || accountDb.Access < AccessLevel.GAMEMASTER)
      return;
    int num = 0;
    using (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK messageAnnounceAck = (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK) new PROTOCOL_SERVER_MESSAGE_ERROR_ACK(str3))
      num = ((AuthXender) AuthXender.Client).SendPacketToAllClients((AuthServerPacket) messageAnnounceAck);
    CLogger.Print($"Message sent to '{num}' Players: '{str3}'; by Username: '{str1}'", LoggerType.Command, (Exception) null);
  }

  public static void LoadShopRestart(SyncClientPacket C)
  {
    int Playtime = (int) C.ReadC();
    ShopManager.Reset();
    ShopManager.Load(Playtime);
    CLogger.Print($"Shop restarted. (Type: {Playtime})", LoggerType.Command, (Exception) null);
  }

  public static void LoadServerUpdate(SyncClientPacket C)
  {
    int string_0 = (int) C.ReadC();
    SChannelXML.UpdateServer(string_0);
    CLogger.Print($"Server updated. (Id: {string_0})", LoggerType.Command, (Exception) null);
  }

  public static void LoadShutdown(SyncClientPacket C)
  {
    string str1 = C.ReadS((int) C.ReadC());
    string str2 = C.ReadS((int) C.ReadC());
    Account accountDb = AccountManager.GetAccountDB((object) str1, (object) str2, 2, 31 /*0x1F*/);
    if (accountDb == null || !(accountDb.Password == str2) || accountDb.Access < AccessLevel.GAMEMASTER)
      return;
    int num = 0;
    foreach (AuthClient authClient in (IEnumerable<AuthClient>) AuthXender.SocketSessions.Values)
    {
      authClient.Client.Shutdown(SocketShutdown.Both);
      authClient.Client.Close(10000);
      ++num;
    }
    CLogger.Print($"Disconnected Players: {num} (By: {str1})", LoggerType.Warning, (Exception) null);
    AuthXender.Client.ServerIsClosed = true;
    AuthXender.Client.MainSocket.Close(5000);
    CLogger.Print("1/2 Step", LoggerType.Warning, (Exception) null);
    Thread.Sleep(5000);
    ((ClanInfo) AuthXender.Sync).Close();
    CLogger.Print("2/2 Step", LoggerType.Warning, (Exception) null);
    foreach (AuthClient authClient in (IEnumerable<AuthClient>) AuthXender.SocketSessions.Values)
      authClient.Close(0, true);
    CLogger.Print($"Shutdowned Server: {num} players disconnected; by Username: '{str1};", LoggerType.Command, (Exception) null);
  }

  public Account()
  {
    this.LastLoginDate = DateTimeUtil.Now();
    this.Nickname = "";
    this.Password = "";
    this.Username = "";
    this.HardwareId = "";
    this.Email = "";
  }

  public void SimpleClear()
  {
    this.Connection = (AuthClient) null;
    this.Config = new PlayerConfig();
    this.Bonus = new PlayerBonus();
    this.Event = new PlayerEvent();
    this.Title = new PlayerTitles();
    this.Inventory = new PlayerInventory();
    this.Statistic = new PlayerStatistic();
    this.Character = new PlayerCharacters();
    this.Equipment = new PlayerEquipment();
    this.Friend = new PlayerFriends();
    this.Status = new AccountStatus();
    this.Mission = new PlayerMissions();
    this.Quickstart = new PlayerQuickstart();
    this.Report = new PlayerReport();
    this.Battlepass = new PlayerBattlepass();
    this.Competitive = new PlayerCompetitive();
    this.ClanPlayers = new List<Account>();
    this.TopUps = new List<PlayerTopup>();
  }

  public void SetOnlineStatus(bool C)
  {
    if (this.IsOnline == C || !ComDiv.UpdateDB("accounts", "online", (object) C, "player_id", (object) this.PlayerId))
      return;
    this.IsOnline = C;
    CLogger.Print($"Account User: {this.Username}, Player UID: {this.PlayerId}, Is {(this.IsOnline ? (object) "Connected" : (object) "Disconnected")}", LoggerType.Info, (Exception) null);
  }

  public void UpdateCacheInfo()
  {
    if (this.PlayerId == 0L)
      return;
    lock (AccountManager.Accounts)
      AccountManager.Accounts[this.PlayerId] = this;
  }

  public void Close(int C)
  {
    if (this.Connection == null)
      return;
    this.Connection.Close(C, true);
  }

  public void SendPacket(AuthServerPacket C)
  {
    if (this.Connection == null)
      return;
    this.Connection.SendPacket(C);
  }

  public void SendPacket(byte[] C, [In] string obj1)
  {
    if (this.Connection == null)
      return;
    this.Connection.SendPacket(C, obj1);
  }

  public void SendCompletePacket(byte[] Online, [In] string obj1)
  {
    if (this.Connection == null)
      return;
    this.Connection.SendCompletePacket(Online, obj1);
  }

  public long StatusId() => (long) !string.IsNullOrEmpty(this.Nickname);

  public bool ComparePassword(string Packet) => ConfigLoader.IsTestMode || this.Password == Packet;

  public void SetPlayerId(long Data, int PacketName)
  {
    this.PlayerId = Data;
    this.GetAccountInfos(PacketName);
  }

  public void GetAccountInfos(int Data)
  {
    if (Data <= 0 || this.PlayerId <= 0L)
      return;
    if ((Data & 1) == 1)
      AllUtils.LoadPlayerEquipments(this);
    if ((Data & 2) == 2)
      AllUtils.LoadPlayerCharacters(this);
    if ((Data & 4) == 4)
      AllUtils.LoadPlayerStatistic(this);
    if ((Data & 8) == 8)
      AllUtils.LoadPlayerTitles(this);
    if ((Data & 16 /*0x10*/) == 16 /*0x10*/)
      AllUtils.LoadPlayerBonus(this);
    if ((Data & 32 /*0x20*/) == 32 /*0x20*/)
      AllUtils.LoadPlayerFriend(this, true);
    if ((Data & 64 /*0x40*/) == 64 /*0x40*/)
      AllUtils.LoadPlayerEvent(this);
    if ((Data & 128 /*0x80*/) == 128 /*0x80*/)
      AllUtils.LoadPlayerConfig(this);
    if ((Data & 256 /*0x0100*/) == 256 /*0x0100*/)
      AllUtils.LoadPlayerFriend(this, false);
    if ((Data & 512 /*0x0200*/) == 512 /*0x0200*/)
      AllUtils.LoadPlayerQuickstarts(this);
    if ((Data & 1024 /*0x0400*/) == 1024 /*0x0400*/)
      AllUtils.LoadPlayerReport(this);
    if ((Data & 2048 /*0x0800*/) == 2048 /*0x0800*/)
      AllUtils.LoadPlayerBattlepass(this);
    if ((Data & 4096 /*0x1000*/) != 4096 /*0x1000*/)
      return;
    AllUtils.LoadPlayerCompetitive(this);
  }

  public int GetRank()
  {
    return this.Bonus != null && this.Bonus.FakeRank != 55 ? this.Bonus.FakeRank : this.Rank;
  }

  public AccessLevel AuthLevel()
  {
    switch (this.Rank)
    {
      case 53:
        return AccessLevel.GAMEMASTER;
      case 54:
        return AccessLevel.MODERATOR;
      default:
        return AccessLevel.NORMAL;
    }
  }

  public bool IsGM() => this.AuthLevel() > AccessLevel.NORMAL;

  public bool HavePermission([In] string obj0) => PermissionXML.HavePermission(obj0, this.Access);
}
