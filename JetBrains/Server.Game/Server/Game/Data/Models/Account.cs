// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Models.Account
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Managers;
using Server.Game.Data.Sync;
using Server.Game.Data.Sync.Update;
using Server.Game.Data.Utils;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;

#nullable disable
namespace Server.Game.Data.Models;

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
  public string FindPlayer;
  public uint LastRankUpDate;
  public uint ClanDate;
  public int InventoryPlus;
  public int Sight;
  public int LastRoomPage;
  public int LastPlayerPage;
  public int ServerId;
  public int ChannelId;
  public int ClanAccess;
  public int Exp;
  public int Gold;
  public int Cash;
  public int ClanId;
  public int SlotId;
  public int NickColor;
  public int Rank;
  public int Ribbon;
  public int Ensign;
  public int Medal;
  public int MasterMedal;
  public int MatchSlot;
  public int Age;
  public int Tags;
  public int FindClanId;
  public bool IsOnline;
  public bool HideGMcolor;
  public bool AntiKickGM;
  public bool LoadedShop;
  public bool UpdateSeasonpass;
  public CouponEffects Effects;
  public AccessLevel Access;
  public CafeEnum CafePC;
  public IPAddress PublicIP;
  public GameClient Connection;
  public RoomModel Room;
  public PlayerSession Session;
  public MatchModel Match;
  public PlayerConfig Config;
  public PlayerBonus Bonus;
  public PlayerEvent Event;
  public PlayerTitles Title;
  public PlayerInventory Inventory;
  public PlayerStatistic Statistic;
  public PlayerCharacters Character;
  public PlayerEquipment Equipment;
  public PlayerFriends Friend;
  public PlayerQuickstart Quickstart;
  public PlayerMissions Mission;
  public PlayerReport Report;
  public PlayerBattlepass Battlepass;
  public PlayerCompetitive Competitive;
  public AccountStatus Status;
  public List<PlayerTopup> TopUps;
  public DateTime LastLobbyEnter;
  public DateTime LastPingDebug;

  public static void LoadGMWarning(SyncClientPacket C)
  {
    string slotModel_0 = C.ReadS((int) C.ReadC());
    string str1 = C.ReadS((int) C.ReadC());
    string str2 = C.ReadS((int) C.ReadH());
    Account accountDb = AccountManager.GetAccountDB((object) slotModel_0, 0, 31 /*0x1F*/);
    if (accountDb == null || !(accountDb.Password == str1) || accountDb.Access < AccessLevel.GAMEMASTER)
      return;
    int num = 0;
    using (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK iasyncResult_0 = (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK) new PROTOCOL_SERVER_MESSAGE_DISCONNECTED_HACK(str2))
      num = GameXender.Client.SendPacketToAllClients((GameServerPacket) iasyncResult_0);
    CLogger.Print($"Message sent to '{num}' Players: '{str2}'; by Username: '{slotModel_0}'", LoggerType.Command, (Exception) null);
  }

  public static void LoadShopRestart(SyncClientPacket Room)
  {
    int Playtime = (int) Room.ReadC();
    ShopManager.Reset();
    ShopManager.Load(Playtime);
    CLogger.Print($"Shop restarted. (Type: {Playtime})", LoggerType.Command, (Exception) null);
  }

  public static void LoadServerUpdate([In] SyncClientPacket obj0)
  {
    int string_0 = (int) obj0.ReadC();
    SChannelXML.UpdateServer(string_0);
    CLogger.Print($"Server updated. (Id: {string_0})", LoggerType.Command, (Exception) null);
  }

  public static void LoadShutdown(SyncClientPacket C)
  {
    string slotModel_0 = C.ReadS((int) C.ReadC());
    string str = C.ReadS((int) C.ReadC());
    Account accountDb = AccountManager.GetAccountDB((object) slotModel_0, 0, 31 /*0x1F*/);
    if (accountDb == null || !(accountDb.Password == str) || accountDb.Access < AccessLevel.GAMEMASTER)
      return;
    int num = 0;
    foreach (GameClient gameClient in (IEnumerable<GameClient>) GameXender.SocketSessions.Values)
    {
      gameClient.Client.Shutdown(SocketShutdown.Both);
      gameClient.Client.Close(10000);
      ++num;
    }
    CLogger.Print($"Disconnected Players: {num} (By: {slotModel_0})", LoggerType.Warning, (Exception) null);
    GameXender.Client.ServerIsClosed = true;
    GameXender.Client.MainSocket.Close(5000);
    CLogger.Print("1/2 Step", LoggerType.Warning, (Exception) null);
    Thread.Sleep(5000);
    ((KillFragInfo) GameXender.Sync).Close();
    CLogger.Print("2/2 Step", LoggerType.Warning, (Exception) null);
    foreach (GameClient gameClient in (IEnumerable<GameClient>) GameXender.SocketSessions.Values)
      gameClient.Close(0, true, false);
    CLogger.Print($"{$"Shutdowned Server: {num} players disconnected; by Login: '"}{slotModel_0};", LoggerType.Command, (Exception) null);
  }

  public Account()
  {
  }

  public Account()
  {
    this.LastLobbyEnter = DateTimeUtil.Now();
    this.Nickname = "";
    this.Password = "";
    this.Username = "";
    this.HardwareId = "";
    this.Email = "";
    this.FindPlayer = "";
    this.ServerId = -1;
    this.ChannelId = -1;
    this.SlotId = -1;
    this.MatchSlot = -1;
  }

  public void SimpleClear()
  {
    this.Title = new PlayerTitles();
    this.Equipment = new PlayerEquipment();
    this.Inventory = new PlayerInventory();
    this.Status = new AccountStatus();
    this.Character = new PlayerCharacters();
    this.Statistic = new PlayerStatistic();
    this.Quickstart = new PlayerQuickstart();
    this.Battlepass = new PlayerBattlepass();
    this.Competitive = new PlayerCompetitive();
    this.Report = new PlayerReport();
    this.Mission = new PlayerMissions();
    this.Bonus = new PlayerBonus();
    this.Event = new PlayerEvent();
    this.Config = new PlayerConfig();
    this.TopUps.Clear();
    this.Friend.CleanList();
    this.Session = (PlayerSession) null;
    this.Match = (MatchModel) null;
    this.Room = (RoomModel) null;
    this.Connection = (GameClient) null;
  }

  public void SetPublicIP(IPAddress C)
  {
    if (C == null)
      this.PublicIP = new IPAddress(new byte[4]);
    this.PublicIP = C;
  }

  public void SetPublicIP(string C) => this.PublicIP = IPAddress.Parse(C);

  public ChannelModel GetChannel() => AllUtils.GetChannel(this.ServerId, this.ChannelId);

  public void ResetPages()
  {
    this.LastRoomPage = 0;
    this.LastPlayerPage = 0;
  }

  public bool GetChannel(ref ChannelModel C)
  {
    C = AllUtils.GetChannel(this.ServerId, this.ChannelId);
    return C != null;
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

  public void Close(int C, [In] bool obj1)
  {
    if (this.Connection == null)
      return;
    this.Connection.Close(C, true, obj1);
  }

  public void SendPacket(GameServerPacket address)
  {
    if (this.Connection == null)
      return;
    this.Connection.SendPacket(address);
  }

  public void SendPacket([Out] GameServerPacket Channel, [In] bool obj1)
  {
    if (this.Connection != null)
    {
      this.Connection.SendPacket(Channel);
    }
    else
    {
      if (obj1 || this.Status.ServerId == byte.MaxValue || (int) this.Status.ServerId == this.ServerId)
        return;
      GameXender.Sync.SendBytes(this.PlayerId, Channel, (int) this.Status.ServerId);
    }
  }

  public void SendPacket(byte[] time, string kicked = false)
  {
    if (this.Connection == null)
      return;
    this.Connection.SendPacket(time, kicked);
  }

  public void SendPacket(byte[] Packet, string OnlyInServer, [In] bool obj2)
  {
    if (this.Connection != null)
    {
      this.Connection.SendPacket(Packet, OnlyInServer);
    }
    else
    {
      if (obj2 || this.Status.ServerId == byte.MaxValue || (int) this.Status.ServerId == this.ServerId)
        return;
      GameXender.Sync.SendBytes(this.PlayerId, OnlyInServer, Packet, (int) this.Status.ServerId);
    }
  }

  public void SendCompletePacket(byte[] Data, string PacketName)
  {
    if (this.Connection == null)
      return;
    this.Connection.SendCompletePacket(Data, PacketName);
  }

  public void SendCompletePacket(byte[] Data, string PacketName, bool OnlyInServer)
  {
    if (this.Connection != null)
    {
      this.Connection.SendCompletePacket(Data, PacketName);
    }
    else
    {
      if (OnlyInServer || this.Status.ServerId == byte.MaxValue || (int) this.Status.ServerId == this.ServerId)
        return;
      // ISSUE: reference to a compiler-generated method
      ((GameSync.Class10) GameXender.Sync).SendCompleteBytes(this.PlayerId, PacketName, Data, (int) this.Status.ServerId);
    }
  }

  public long StatusId() => (long) !string.IsNullOrEmpty(this.Nickname);

  public int GetSessionId() => this.Session == null ? 0 : this.Session.SessionId;

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

  public bool UseChatGM()
  {
    if (this.HideGMcolor)
      return false;
    return this.Rank == 53 || this.Rank == 54;
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
}
