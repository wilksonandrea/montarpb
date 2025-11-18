// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Models.RoomModel
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Managers;
using Server.Game.Data.Sync.Server;
using Server.Game.Data.Utils;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

#nullable disable
namespace Server.Game.Data.Models;

public class RoomModel
{
  public SlotModel[] Slots = new SlotModel[18];
  public int Rounds;
  public int TRex;
  public int CTRounds;
  public int CTDino;
  public int FRRounds;
  public int FRDino;
  public int Bar1;
  public int Bar2;
  public int Ping;
  public int FRKills;
  public int FRDeaths;
  public int FRAssists;
  public int CTKills;
  public int CTDeaths;
  public int CTAssists;
  public int SpawnsCount;
  public int KillTime;
  public int RoomId;
  public int ChannelId;
  public int ServerId;
  public int LeaderSlot;
  public int CountPlayers;
  public int CountMaxSlots;
  public int NewInt;
  public byte Limit;
  public byte WatchRuleFlag;
  public byte AiCount;
  public byte IngameAiLevel;
  public byte AiLevel;
  public byte AiType;
  public byte CountdownIG;
  public byte KillCam;
  public uint TimeRoom;
  public uint StartDate;
  public uint UniqueRoomId;
  public uint Seed;
  public long StartTick;
  public string Name;
  public string Password;
  public string MapName;
  public string LeaderName;
  public bool ActiveC4;
  public bool ChangingSlots;
  public bool BlockedClan;
  public bool PreMatchCD;
  public bool SwapRound;
  public bool Competitive;
  public readonly int[] TIMES = new int[11]
  {
    3,
    3,
    3,
    5,
    7,
    5,
    10,
    15,
    20,
    25,
    30
  };
  public readonly int[] KILLS = new int[9]
  {
    15,
    30,
    50,
    60,
    80 /*0x50*/,
    100,
    120,
    140,
    160 /*0xA0*/
  };
  public readonly int[] ROUNDS = new int[6]
  {
    1,
    2,
    3,
    5,
    7,
    9
  };
  public readonly int[] FR_TEAM = new int[9]
  {
    0,
    2,
    4,
    6,
    8,
    10,
    12,
    14,
    16 /*0x10*/
  };
  public readonly int[] CT_TEAM = new int[9]
  {
    1,
    3,
    5,
    7,
    9,
    11,
    13,
    15,
    17
  };
  public readonly int[] ALL_TEAM = new int[18]
  {
    0,
    1,
    2,
    3,
    4,
    5,
    6,
    7,
    8,
    9,
    10,
    11,
    12,
    13,
    14,
    15,
    16 /*0x10*/,
    17
  };
  public readonly int[] INVERT_FR_TEAM = new int[9]
  {
    16 /*0x10*/,
    14,
    12,
    10,
    8,
    6,
    4,
    2,
    0
  };
  public readonly int[] INVERT_CT_TEAM = new int[9]
  {
    17,
    15,
    13,
    11,
    9,
    7,
    5,
    3,
    1
  };
  public byte[] RandomMaps;
  public byte[] LeaderAddr = new byte[4];
  public byte[] HitParts = new byte[35]
  {
    (byte) 0,
    (byte) 1,
    (byte) 2,
    (byte) 3,
    (byte) 4,
    (byte) 5,
    (byte) 6,
    (byte) 7,
    (byte) 8,
    (byte) 9,
    (byte) 10,
    (byte) 11,
    (byte) 12,
    (byte) 13,
    (byte) 14,
    (byte) 15,
    (byte) 16 /*0x10*/,
    (byte) 17,
    (byte) 18,
    (byte) 19,
    (byte) 20,
    (byte) 21,
    (byte) 22,
    (byte) 23,
    (byte) 24,
    (byte) 25,
    (byte) 26,
    (byte) 27,
    (byte) 28,
    (byte) 29,
    (byte) 30,
    (byte) 31 /*0x1F*/,
    (byte) 32 /*0x20*/,
    (byte) 33,
    (byte) 34
  };
  public (byte[], int[]) SlotRewards;
  public ChannelType ChannelType;
  public MapIdEnum MapId;
  public RoomCondition RoomType;
  public RoomState State;
  public MapRules Rule;
  public StageOptions Stage;
  public TeamBalance BalanceType;
  public RoomStageFlag Flag;
  public RoomWeaponsFlag WeaponsFlag;
  public VoteKickModel VoteKick;
  public Synchronize UdpServer;
  public DateTime BattleStart;
  public DateTime LastPingSync;
  public DateTime LastChangeTeam;
  public TimerState BombTime = new TimerState();
  public TimerState CountdownTime = new TimerState();
  public TimerState RoundTime = new TimerState();
  public TimerState RoundTeamSwap = new TimerState();
  public TimerState VoteTime = new TimerState();
  public TimerState PreMatchTime = new TimerState();
  public TimerState MatchEndTime = new TimerState();
  public SafeList<long> KickedPlayersVote = new SafeList<long>();
  public SafeList<long> RequestRoomMaster = new SafeList<long>();
  public SortedList<long, DateTime> KickedPlayersHost = new SortedList<long, DateTime>();

  public Account GetLeader()
  {
    try
    {
      return ClanManager.GetAccount(((MatchModel) this).Slots[((MatchModel) this).Leader].PlayerId, true);
    }
    catch
    {
      return (Account) null;
    }
  }

  public int GetServerInfo() => ((MatchModel) this).ChannelId + ((MatchModel) this).ServerId * 10;

  public int GetCountPlayers()
  {
    lock (((MatchModel) this).Slots)
    {
      int countPlayers = 0;
      foreach (SlotMatch slot in ((MatchModel) this).Slots)
      {
        if (slot.PlayerId > 0L)
          ++countPlayers;
      }
      return countPlayers;
    }
  }

  private void method_0(Account SlotId)
  {
    lock (((MatchModel) this).Slots)
    {
      SlotMatch slotMatch;
      if (!((MatchModel) this).GetSlot(SlotId.MatchSlot, ref slotMatch) || slotMatch.PlayerId != SlotId.PlayerId)
        return;
      slotMatch.PlayerId = 0L;
      slotMatch.State = SlotMatchState.Empty;
    }
  }

  public bool RemovePlayer(Account Exception)
  {
    ChannelModel channel = AllUtils.GetChannel(((MatchModel) this).ServerId, ((MatchModel) this).ChannelId);
    if (channel == null)
      return false;
    this.method_0(Exception);
    if (this.GetCountPlayers() == 0)
    {
      channel.RemoveMatch(((MatchModel) this).MatchId);
    }
    else
    {
      if (Exception.MatchSlot == ((MatchModel) this).Leader)
        ((MatchModel) this).SetNewLeader(-1, -1);
      using (PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK registMercenaryAck = (PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK) new PROTOCOL_CLAN_WAR_TEAM_CHATTING_ACK((MatchModel) this))
        ((MatchModel) this).SendPacketToPlayers((GameServerPacket) registMercenaryAck);
    }
    Exception.MatchSlot = -1;
    Exception.Match = (MatchModel) null;
    return true;
  }

  public RoomModel(int Packet, [In] ChannelModel obj1)
  {
    this.RoomId = Packet;
    for (int index = 0; index < this.Slots.Length; ++index)
      this.Slots[index] = new SlotModel(index);
    this.ChannelId = obj1.Id;
    this.ChannelType = obj1.Type;
    this.ServerId = obj1.ServerId;
    this.Rounds = 1;
    this.AiCount = (byte) 1;
    this.AiLevel = (byte) 1;
    this.TRex = -1;
    this.Ping = 5;
    this.Name = "";
    this.Password = "";
    this.MapName = "";
    this.LeaderName = "";
    this.method_0();
    DateTime dateTime = DateTimeUtil.Now();
    this.LastChangeTeam = dateTime;
    this.LastPingSync = dateTime;
  }

  private void method_0()
  {
    this.UniqueRoomId = (uint) ((this.ServerId & (int) byte.MaxValue) << 20 | (this.ChannelId & (int) byte.MaxValue) << 12 | this.RoomId & 4095 /*0x0FFF*/);
  }

  public void GenerateSeed()
  {
    this.Seed = (uint) ((RoomCondition) ((int) (this.MapId & (MapIdEnum) 255 /*0xFF*/) << 20 | (int) (this.Rule & ~MapRules.None) << 12) | this.RoomType & (RoomCondition) 4095 /*0x0FFF*/);
  }

  public bool ThisModeHaveCD()
  {
    return this.RoomType == RoomCondition.Bomb || this.RoomType == RoomCondition.Annihilation || this.RoomType == RoomCondition.Boss || this.RoomType == RoomCondition.CrossCounter || this.RoomType == RoomCondition.Destroy || this.RoomType == RoomCondition.Ace || this.RoomType == RoomCondition.Escape || this.RoomType == RoomCondition.Glass;
  }

  public bool ThisModeHaveRounds()
  {
    return this.RoomType == RoomCondition.Bomb || this.RoomType == RoomCondition.Destroy || this.RoomType == RoomCondition.Annihilation || this.RoomType == RoomCondition.Defense || this.RoomType == RoomCondition.Destroy || this.RoomType == RoomCondition.Ace || this.RoomType == RoomCondition.Escape || this.RoomType == RoomCondition.Glass;
  }

  public int GetFlag()
  {
    int flag = 0;
    if (this.Flag.HasFlag((Enum) RoomStageFlag.TEAM_SWAP))
      ++flag;
    if (this.Flag.HasFlag((Enum) RoomStageFlag.RANDOM_MAP))
      flag += 2;
    if (this.Flag.HasFlag((Enum) RoomStageFlag.PASSWORD) || this.Password.Length > 0)
      flag += 4;
    if (this.Flag.HasFlag((Enum) RoomStageFlag.OBSERVER_MODE))
      flag += 8;
    if (this.Flag.HasFlag((Enum) RoomStageFlag.REAL_IP))
      flag += 16 /*0x10*/;
    if (this.Flag.HasFlag((Enum) RoomStageFlag.TEAM_BALANCE) || this.BalanceType == TeamBalance.Count)
      flag += 32 /*0x20*/;
    if (this.Flag.HasFlag((Enum) RoomStageFlag.OBSERVER))
      flag += 64 /*0x40*/;
    if (this.Flag.HasFlag((Enum) RoomStageFlag.INTER_ENTER) || this.Limit > (byte) 0 && this.IsStartingMatch())
      flag += 128 /*0x80*/;
    this.Flag = (RoomStageFlag) flag;
    return flag;
  }

  public bool IsBotMode()
  {
    return this.Stage == StageOptions.AI || this.Stage == StageOptions.DieHard || this.Stage == StageOptions.Infection;
  }

  public void SetBotLevel()
  {
    if (!this.IsBotMode())
      return;
    this.IngameAiLevel = this.AiLevel;
    for (int index = 0; index < 18; ++index)
      this.Slots[index].AiLevel = (int) this.IngameAiLevel;
  }

  private void method_1()
  {
    if (this.RoomType == RoomCondition.Defense)
    {
      if (this.MapId != MapIdEnum.BlackPanther)
        return;
      this.Bar1 = 6000;
      this.Bar2 = 9000;
    }
    else
    {
      if (this.RoomType != RoomCondition.Destroy)
        return;
      if (this.MapId == MapIdEnum.Helispot)
      {
        this.Bar1 = 12000;
        this.Bar2 = 12000;
      }
      else
      {
        if (this.MapId != MapIdEnum.BreakDown)
          return;
        this.Bar1 = 6000;
        this.Bar2 = 6000;
      }
    }
  }

  public void ChangeRounds()
  {
    ++this.Rounds;
    if (!this.method_2() || this.SwapRound)
      return;
    this.SwapRound = true;
  }

  private bool method_2()
  {
    if (!this.Flag.HasFlag((Enum) RoomStageFlag.TEAM_SWAP))
      return false;
    if (this.IsDinoMode(""))
      return this.Rounds == 2;
    int num = this.GetRoundsByMask() - 1;
    return this.FRRounds == num && this.CTRounds == 0 || this.CTRounds == num && this.FRRounds == 0;
  }

  public int GetInBattleTime()
  {
    int inBattleTime = 0;
    if (this.BattleStart != new DateTime() && (this.State == RoomState.BATTLE || this.State == RoomState.PRE_BATTLE))
    {
      inBattleTime = (int) ComDiv.GetDuration(this.BattleStart);
      if (inBattleTime < 0)
        inBattleTime = 0;
    }
    return inBattleTime;
  }

  public int GetInBattleTimeLeft() => this.GetTimeByMask() * 60 - this.GetInBattleTime();

  public ChannelModel GetChannel() => AllUtils.GetChannel(this.ServerId, this.ChannelId);

  public bool GetChannel([In] ref ChannelModel obj0)
  {
    obj0 = AllUtils.GetChannel(this.ServerId, this.ChannelId);
    return obj0 != null;
  }

  public bool GetSlot(int account_0, [In] ref SlotModel obj1)
  {
    obj1 = (SlotModel) null;
    lock (this.Slots)
    {
      if (account_0 >= 0 && account_0 <= 17)
        obj1 = this.Slots[account_0];
      return obj1 != null;
    }
  }

  public SlotModel GetSlot(int int_0)
  {
    lock (this.Slots)
      return int_0 >= 0 && int_0 <= 17 ? this.Slots[int_0] : (SlotModel) null;
  }

  public void StartCounter([In] int obj0, Account channelModel_0, [In] SlotModel obj2)
  {
    // ISSUE: variable of a compiler-generated type
    RoomModel.Class12 class12 = (RoomModel.Class12) new AccountManager();
    // ISSUE: reference to a compiler-generated field
    class12.slotModel_0 = obj2;
    // ISSUE: reference to a compiler-generated field
    class12.roomModel_0 = this;
    // ISSUE: reference to a compiler-generated field
    class12.account_0 = channelModel_0;
    int num = 0;
    // ISSUE: reference to a compiler-generated field
    class12.eventErrorEnum_0 = EventErrorEnum.SUCCESS;
    switch (obj0)
    {
      case 0:
        // ISSUE: reference to a compiler-generated field
        class12.eventErrorEnum_0 = EventErrorEnum.BATTLE_FIRST_MAINLOAD;
        num = 90000;
        break;
      case 1:
        // ISSUE: reference to a compiler-generated field
        class12.eventErrorEnum_0 = EventErrorEnum.BATTLE_FIRST_HOLE;
        num = 30000;
        break;
    }
    if (num > 0)
    {
      // ISSUE: reference to a compiler-generated field
      class12.slotModel_0.FirstInactivityOff = true;
    }
    // ISSUE: reference to a compiler-generated field
    class12.slotModel_0.Timing.StartJob(num, new TimerCallback(((AccountManager) class12).method_0));
  }

  private void method_3([In] EventErrorEnum obj0, [Out] Account Slot, [In] SlotModel obj2)
  {
    Slot.SendPacket((GameServerPacket) new PROTOCOL_SERVER_MESSAGE_USER_EVENT_START_ACK(obj0));
    Slot.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK(Slot, 0));
    this.ChangeSlotState(obj2.Id, SlotState.NORMAL, true);
    AllUtils.BattleEndPlayersCount(this, this.IsBotMode());
  }

  public void StartBomb()
  {
    this.BombTime.StartJob(42000, (TimerCallback) (SlotIdx =>
    {
      if (this != null && this.ActiveC4)
      {
        if (this.SwapRound)
          ++this.CTRounds;
        else
          ++this.FRRounds;
        this.ActiveC4 = false;
        AllUtils.BattleEndRound(this, TeamEnum.FR_TEAM, RoundEndType.BombFire);
      }
      lock (SlotIdx)
        this.BombTime.StopJob();
    }));
  }

  public void StartVote()
  {
    if (this.VoteKick == null)
      return;
    this.VoteTime.StartJob(20000, (TimerCallback) (Team =>
    {
      AllUtils.VotekickResult(this);
      lock (Team)
        this.VoteTime.StopJob();
    }));
  }

  public void RoundRestart()
  {
    this.StopBomb();
    foreach (SlotModel slot in this.Slots)
    {
      if (slot.PlayerId > 0L && slot.State == SlotState.BATTLE)
      {
        if (!slot.DeathState.HasFlag((Enum) DeadEnum.UseChat))
          slot.DeathState |= DeadEnum.UseChat;
        if (slot.Spectator)
          slot.Spectator = false;
        if (slot.KillsOnLife >= 3 && (this.RoomType == RoomCondition.Annihilation || this.RoomType == RoomCondition.Ace))
          ++slot.Objects;
        slot.KillsOnLife = 0;
        slot.LastKillState = 0;
        slot.RepeatLastState = false;
        slot.DamageBar1 = (ushort) 0;
        slot.DamageBar2 = (ushort) 0;
      }
    }
    // ISSUE: reference to a compiler-generated method
    this.RoundTime.StartJob(8250, new TimerCallback(((RoomModel.Class11) this).method_21));
  }

  private void method_4()
  {
    foreach (SlotModel slot in this.Slots)
    {
      if (slot.PlayerId > 0L)
      {
        if (!slot.DeathState.HasFlag((Enum) DeadEnum.UseChat))
          slot.DeathState |= DeadEnum.UseChat;
        if (slot.Spectator)
          slot.Spectator = false;
      }
    }
    this.StopBomb();
    DateTime dateTime = DateTimeUtil.Now();
    if (this.State == RoomState.BATTLE)
      this.BattleStart = this.IsDinoMode("") ? dateTime.AddSeconds(5.0) : dateTime;
    using (PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK roundPreStartAck = (PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK) new PROTOCOL_BATTLE_MISSION_ROUND_START_ACK(this, AllUtils.GetDinossaurs(this, false, -1)))
      this.SendPacketToPlayers((GameServerPacket) roundPreStartAck, SlotState.BATTLE, 0);
    if (this.method_2())
    {
      // ISSUE: reference to a compiler-generated method
      this.RoundTeamSwap.StartJob(5250, new TimerCallback(((RoomModel.Class11) this).method_22));
    }
    else
      this.method_5();
    this.StopBomb();
  }

  private void method_5()
  {
    using (PROTOCOL_BATTLE_MISSION_ROUND_START_ACK missionRoundStartAck = (PROTOCOL_BATTLE_MISSION_ROUND_START_ACK) new PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_ACK(this))
      this.SendPacketToPlayers((GameServerPacket) missionRoundStartAck, SlotState.BATTLE, 0);
  }

  public void StopBomb()
  {
    if (!this.ActiveC4)
      return;
    this.ActiveC4 = false;
    if (this.BombTime == null)
      return;
    this.BombTime.StopJob();
  }

  public void StartBattle([In] bool obj0)
  {
    lock (this.Slots)
    {
      this.State = RoomState.LOADING;
      this.RequestRoomMaster.Clear();
      this.SetBotLevel();
      AllUtils.CheckClanMatchRestrict(this);
      this.StartTick = DateTimeUtil.Now().Ticks;
      this.StartDate = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
      using (PROTOCOL_BATTLE_START_GAME_ACK battleStartGameAck = (PROTOCOL_BATTLE_START_GAME_ACK) new PROTOCOL_BATTLE_START_GAME_TRANS_ACK(this))
      {
        byte[] completeBytes = ((PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK) battleStartGameAck).GetCompleteBytes("Room.StartBattle");
        foreach (Account allPlayer in this.GetAllPlayers(SlotState.READY, 0))
        {
          SlotModel slot = this.GetSlot(allPlayer.SlotId);
          if (slot != null)
          {
            slot.WithHost = true;
            slot.State = SlotState.LOAD;
            slot.SetMissionsClone(allPlayer.Mission);
            allPlayer.SendCompletePacket(completeBytes, battleStartGameAck.GetType().Name);
          }
        }
      }
      if (obj0)
        this.UpdateSlotsInfo();
      this.UpdateRoomInfo();
    }
  }

  public void StartCountDown()
  {
    using (PROTOCOL_BATTLE_START_COUNTDOWN_ACK Player = (PROTOCOL_BATTLE_START_COUNTDOWN_ACK) new PROTOCOL_BATTLE_START_GAME_ACK(CountDownEnum.Start))
      this.SendPacketToPlayers((GameServerPacket) Player);
    // ISSUE: reference to a compiler-generated method
    this.CountdownTime.StartJob(5250, new TimerCallback(((RoomModel.Class11) this).method_23));
  }

  private void method_6()
  {
    if (this.Slots[this.LeaderSlot].State == SlotState.READY && this.State == RoomState.COUNTDOWN)
    {
      this.StartBattle(true);
    }
    else
    {
      using (PROTOCOL_BATTLE_READYBATTLE_ACK battleReadybattleAck = (PROTOCOL_BATTLE_READYBATTLE_ACK) new PROTOCOL_BATTLE_RESPAWN_ACK(2147487752U /*0x80001008*/))
      {
        byte[] completeBytes = ((PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK) battleReadybattleAck).GetCompleteBytes("Room.ReadyBattle");
        foreach (Account allPlayer in this.GetAllPlayers(SlotState.READY, 0))
        {
          SlotModel slot = this.GetSlot(allPlayer.SlotId);
          if (slot != null && slot.State == SlotState.READY)
            allPlayer.SendCompletePacket(completeBytes, battleReadybattleAck.GetType().Name);
        }
      }
    }
  }

  public void StopCountDown([In] CountDownEnum obj0, [In] bool obj1)
  {
    this.State = RoomState.READY;
    if (obj1)
      this.UpdateRoomInfo();
    this.CountdownTime.StopJob();
    using (PROTOCOL_BATTLE_START_COUNTDOWN_ACK Player = (PROTOCOL_BATTLE_START_COUNTDOWN_ACK) new PROTOCOL_BATTLE_START_GAME_ACK(obj0))
      this.SendPacketToPlayers((GameServerPacket) Player);
  }

  public void StopCountDown([In] int obj0)
  {
    if (this.State != RoomState.COUNTDOWN)
      return;
    if (obj0 == this.LeaderSlot)
    {
      this.StopCountDown(CountDownEnum.StopByHost, true);
    }
    else
    {
      if (this.GetPlayingPlayers((TeamEnum) (this.LeaderSlot % 2 == 0), SlotState.READY, 0) != 0)
        return;
      this.ChangeSlotState(this.LeaderSlot, SlotState.NORMAL, false);
      this.StopCountDown(CountDownEnum.StopByPlayer, true);
    }
  }

  public void CalculateResult()
  {
    lock (this.Slots)
      this.method_7(AllUtils.GetWinnerTeam(this), this.IsBotMode());
  }

  public void CalculateResult([In] TeamEnum obj0)
  {
    lock (this.Slots)
      this.method_7(obj0, this.IsBotMode());
  }

  public void CalculateResult(TeamEnum UpdateInfo, [In] bool obj1)
  {
    lock (this.Slots)
      this.method_7(UpdateInfo, obj1);
  }

  public void CalculateResultFreeForAll([In] int obj0)
  {
    lock (this.Slots)
      this.method_7((TeamEnum) obj0, false);
  }

  private void method_7(TeamEnum SlotId, [In] bool obj1)
  {
    ServerConfig config = GameXender.Client.Config;
    EventRankUpModel runningEvent1 = EventRankUpXML.GetRunningEvent();
    EventBoostModel runningEvent2 = EventBoostXML.GetRunningEvent();
    EventPlaytimeModel runningEvent3 = EventPlaytimeXML.GetRunningEvent();
    BattlePassModel activeSeasonPass = SeasonChallengeXML.GetActiveSeasonPass();
    DateTime dateTime = DateTimeUtil.Now();
    int[] numArray = new int[18];
    int dbquery_0_1 = 0;
    if (config == null)
    {
      CLogger.Print("Server Config Null. RoomResult canceled.", LoggerType.Warning, (Exception) null);
    }
    else
    {
      List<SlotModel> resultType = new List<SlotModel>();
      for (int index = 0; index < 18; ++index)
      {
        SlotModel slot = this.Slots[index];
        numArray[index] = slot.PlayerId == 0L ? 0 : slot.AllKills;
        if (numArray[index] > numArray[dbquery_0_1])
          dbquery_0_1 = index;
        Account account;
        if (!slot.Check && slot.State == SlotState.BATTLE && this.GetPlayerBySlot(slot, ref account))
        {
          StatisticTotal basic = account.Statistic.Basic;
          StatisticSeason season = account.Statistic.Season;
          StatisticDaily daily = account.Statistic.Daily;
          StatisticWeapon weapon = account.Statistic.Weapon;
          DBQuery dbQuery1 = new DBQuery();
          DBQuery dbQuery2 = new DBQuery();
          DBQuery dbQuery3 = new DBQuery();
          DBQuery dbQuery4 = new DBQuery();
          DBQuery dbquery_0_2 = new DBQuery();
          slot.Check = true;
          double num1 = slot.InBattleTime(dateTime);
          int gold = account.Gold;
          int exp = account.Exp;
          int cash = account.Cash;
          if (!obj1)
          {
            if (config.Missions)
            {
              AllUtils.EndMatchMission(this, account, slot, SlotId);
              if (slot.MissionsCompleted)
              {
                account.Mission = slot.Missions;
                DaoManagerSQL.UpdateCurrentPlayerMissionList(account.PlayerId, account.Mission);
              }
              AllUtils.GenerateMissionAwards(account, dbQuery1);
            }
            int num2 = slot.AllKills != 0 || slot.AllDeaths != 0 ? (int) num1 : (int) (num1 / 3.0);
            if (this.RoomType != RoomCondition.Bomb && this.RoomType != RoomCondition.Annihilation && this.RoomType != RoomCondition.Ace)
            {
              slot.Exp = (int) ((double) slot.Score + (double) num2 / 2.5 + (double) slot.AllDeaths * 1.8 + (double) (slot.Objects * 20));
              slot.Gold = (int) ((double) slot.Score + (double) num2 / 3.0 + (double) slot.AllDeaths * 1.8 + (double) (slot.Objects * 20));
              slot.Cash = (int) ((double) slot.Score / 1.5 + (double) num2 / 4.5 + (double) slot.AllDeaths * 1.1 + (double) (slot.Objects * 20));
            }
            else
            {
              slot.Exp = (int) ((double) slot.Score + (double) num2 / 2.5 + (double) slot.AllDeaths * 2.2 + (double) (slot.Objects * 20));
              slot.Gold = (int) ((double) slot.Score + (double) num2 / 3.0 + (double) slot.AllDeaths * 2.2 + (double) (slot.Objects * 20));
              slot.Cash = (int) ((double) (slot.Score / 2) + (double) num2 / 6.5 + (double) slot.AllDeaths * 1.5 + (double) (slot.Objects * 10));
            }
            bool dbquery_1 = slot.Team == SlotId;
            if (this.Rule != MapRules.Chaos && this.Rule != MapRules.HeadHunter)
            {
              if (basic != null && season != null)
                this.method_9(account, basic, season, slot, dbQuery2, dbQuery3, dbquery_0_1, dbquery_1, (int) SlotId);
              if (daily != null)
                this.method_10(account, daily, slot, dbQuery4, dbquery_0_1, dbquery_1, (int) SlotId);
              if (weapon != null)
                this.method_11(account, weapon, slot, dbquery_0_2);
            }
            if (dbquery_1 || this.RoomType == RoomCondition.FreeForAll && SlotId == (TeamEnum) dbquery_0_1)
            {
              slot.Gold += ComDiv.Percentage(slot.Gold, 15);
              slot.Exp += ComDiv.Percentage(slot.Exp, 20);
            }
            if (slot.EarnedEXP > 0)
              slot.Exp += slot.EarnedEXP * 5;
          }
          else
          {
            int num3 = (int) this.IngameAiLevel * (150 + slot.AllDeaths);
            if (num3 == 0)
              ++num3;
            int num4 = slot.Score / num3;
            slot.Gold += num4;
            slot.Exp += num4;
          }
          slot.Exp = slot.Exp > ConfigLoader.MaxExpReward ? ConfigLoader.MaxExpReward : slot.Exp;
          slot.Gold = slot.Gold > ConfigLoader.MaxGoldReward ? ConfigLoader.MaxGoldReward : slot.Gold;
          slot.Cash = slot.Cash > ConfigLoader.MaxCashReward ? ConfigLoader.MaxCashReward : slot.Cash;
          if (this.RoomType == RoomCondition.Ace)
          {
            if (account.SlotId < 0 || account.SlotId > 1)
            {
              slot.Exp = 0;
              slot.Gold = 0;
              slot.Cash = 0;
            }
          }
          else if (slot.Exp < 0 || slot.Gold < 0 || slot.Cash < 0)
          {
            slot.Exp = 2;
            slot.Gold = 2;
            slot.Cash = 2;
          }
          int num5 = 0;
          int num6 = 0;
          int num7 = 0;
          int num8 = 0;
          int num9 = 0;
          int num10 = 0;
          int num11 = 0;
          if (runningEvent1 != null || runningEvent2 != null)
          {
            int[] bonuses = runningEvent1.GetBonuses(account.Rank);
            if (runningEvent1 != null && bonuses != null)
            {
              num9 += ComDiv.Percentage(bonuses[0], bonuses[2]);
              num10 += ComDiv.Percentage(bonuses[1], bonuses[2]);
            }
            if (runningEvent2 != null && runningEvent2.BoostType == PortalBoostEvent.Mode)
            {
              num9 += runningEvent2.BonusExp;
              num10 += runningEvent2.BonusGold;
            }
            if (!slot.BonusFlags.HasFlag((Enum) ResultIcon.Event))
              slot.BonusFlags |= ResultIcon.Event;
            slot.BonusEventExp += num9;
            slot.BonusEventPoint += num10;
          }
          PlayerBonus bonus = account.Bonus;
          if (bonus != null && bonus.Bonuses > 0)
          {
            if ((bonus.Bonuses & 8) == 8)
              num5 += 100;
            if ((bonus.Bonuses & 128 /*0x80*/) == 128 /*0x80*/)
              num6 += 100;
            if ((bonus.Bonuses & 4) == 4)
              num5 += 50;
            if ((bonus.Bonuses & 64 /*0x40*/) == 64 /*0x40*/)
              num6 += 50;
            if ((bonus.Bonuses & 2) == 2)
              num5 += 30;
            if ((bonus.Bonuses & 32 /*0x20*/) == 32 /*0x20*/)
              num6 += 30;
            if ((bonus.Bonuses & 1) == 1)
              num5 += 10;
            if ((bonus.Bonuses & 16 /*0x10*/) == 16 /*0x10*/)
              num6 += 10;
            if ((bonus.Bonuses & 512 /*0x0200*/) == 512 /*0x0200*/)
              num11 += 20;
            if ((bonus.Bonuses & 1024 /*0x0400*/) == 1024 /*0x0400*/)
              num11 += 30;
            if ((bonus.Bonuses & 2048 /*0x0800*/) == 2048 /*0x0800*/)
              num11 += 50;
            if ((bonus.Bonuses & 4096 /*0x1000*/) == 4096 /*0x1000*/)
              num11 += 100;
            if (!slot.BonusFlags.HasFlag((Enum) ResultIcon.Item))
              slot.BonusFlags |= ResultIcon.Item;
            slot.BonusItemExp += num5;
            slot.BonusItemPoint += num6;
            slot.BonusBattlePass += num11;
          }
          PCCafeModel pcCafe = TemplatePackXML.GetPCCafe(account.CafePC);
          if (pcCafe != null)
          {
            PlayerVip playerVip = DaoManagerSQL.GetPlayerVIP(account.PlayerId);
            if (playerVip != null && InternetCafeXML.IsValidAddress(DaoManagerSQL.GetPlayerIP4Address(account.PlayerId), playerVip.Address))
            {
              InternetCafe icafe = InternetCafeXML.GetICafe(ConfigLoader.InternetCafeId);
              if (icafe != null && (account.CafePC == CafeEnum.Gold || account.CafePC == CafeEnum.Silver))
              {
                num7 += account.CafePC == CafeEnum.Gold ? icafe.PremiumExp : (account.CafePC == CafeEnum.Silver ? icafe.BasicExp : 0);
                num8 += account.CafePC == CafeEnum.Gold ? icafe.PremiumGold : (account.CafePC == CafeEnum.Silver ? icafe.BasicGold : 0);
              }
            }
            int num12 = num7 + pcCafe.ExpUp;
            int num13 = num8 + pcCafe.PointUp;
            if (account.CafePC == CafeEnum.Silver && !slot.BonusFlags.HasFlag((Enum) ResultIcon.Pc))
              slot.BonusFlags |= ResultIcon.Pc;
            else if (account.CafePC == CafeEnum.Gold && !slot.BonusFlags.HasFlag((Enum) ResultIcon.PcPlus))
              slot.BonusFlags |= ResultIcon.PcPlus;
            slot.BonusCafeExp += num12;
            slot.BonusCafePoint += num13;
          }
          if (obj1)
          {
            if (slot.BonusItemExp > 300)
              slot.BonusItemExp = 300;
            if (slot.BonusItemPoint > 300)
              slot.BonusItemPoint = 300;
            if (slot.BonusCafeExp > 300)
              slot.BonusCafeExp = 300;
            if (slot.BonusCafePoint > 300)
              slot.BonusCafePoint = 300;
            if (slot.BonusEventExp > 300)
              slot.BonusEventExp = 300;
            if (slot.BonusEventPoint > 300)
              slot.BonusEventPoint = 300;
          }
          int num14 = slot.BonusEventExp + slot.BonusCafeExp + slot.BonusItemExp;
          int num15 = slot.BonusEventPoint + slot.BonusCafePoint + slot.BonusItemPoint;
          account.Exp += slot.Exp + ComDiv.Percentage(slot.Exp, num14);
          account.Gold += slot.Gold + ComDiv.Percentage(slot.Gold, num15);
          if (daily != null)
          {
            daily.ExpGained += slot.Exp + ComDiv.Percentage(slot.Exp, num14);
            daily.PointGained += slot.Gold + ComDiv.Percentage(slot.Gold, num15);
            daily.Playtime += (uint) num1;
            dbQuery4.AddQuery("exp_gained", (object) daily.ExpGained);
            dbQuery4.AddQuery("point_gained", (object) daily.PointGained);
            dbQuery4.AddQuery("playtime", (object) (long) daily.Playtime);
          }
          if (ConfigLoader.WinCashPerBattle)
            account.Cash += slot.Cash;
          RankModel rank = PlayerRankXML.GetRank(account.Rank);
          if (rank != null && account.Exp >= rank.OnNextLevel + rank.OnAllExp && account.Rank <= 50)
          {
            List<int> rewards = PlayerRankXML.GetRewards(account.Rank);
            if (rewards.Count > 0)
            {
              foreach (int num16 in rewards)
              {
                GoodsItem good = ShopManager.GetGood(num16);
                if (good != null)
                {
                  if (ComDiv.GetIdStatics(good.Item.Id, 1) == 6 && account.Character.GetCharacter(good.Item.Id) == null)
                    AllUtils.CreateCharacter(account, good.Item);
                  else
                    account.SendPacket((GameServerPacket) new PROTOCOL_LOBBY_CHATTING_ACK(0, account, good.Item));
                }
              }
            }
            account.Gold += rank.OnGoldUp;
            account.LastRankUpDate = uint.Parse(dateTime.ToString("yyMMddHHmm"));
            account.SendPacket((GameServerPacket) new PROTOCOL_BASE_USER_ENTER_ACK(++account.Rank, rank.OnNextLevel));
            dbQuery1.AddQuery("last_rank_update", (object) (long) account.LastRankUpDate);
            dbQuery1.AddQuery("rank", (object) account.Rank);
          }
          if (runningEvent3 != null)
            AllUtils.PlayTimeEvent(account, runningEvent3, obj1, slot, (long) num1);
          if (activeSeasonPass != null)
          {
            account.UpdateSeasonpass = true;
            AllUtils.CalculateBattlePass(account, slot, activeSeasonPass);
          }
          if (this.Competitive)
            AllUtils.CalculateCompetitive(this, account, slot, SlotId == slot.Team);
          AllUtils.DiscountPlayerItems(slot, account);
          if (exp != account.Exp)
            dbQuery1.AddQuery("experience", (object) account.Exp);
          if (gold != account.Gold)
            dbQuery1.AddQuery("gold", (object) account.Gold);
          if (cash != account.Cash)
            dbQuery1.AddQuery("cash", (object) account.Cash);
          ComDiv.UpdateDB("accounts", "player_id", (object) account.PlayerId, dbQuery1.GetTables(), dbQuery1.GetValues());
          ComDiv.UpdateDB("player_stat_basics", "owner_id", (object) account.PlayerId, dbQuery2.GetTables(), dbQuery2.GetValues());
          ComDiv.UpdateDB("player_stat_seasons", "owner_id", (object) account.PlayerId, dbQuery3.GetTables(), dbQuery3.GetValues());
          ComDiv.UpdateDB("player_stat_dailies", "owner_id", (object) account.PlayerId, dbQuery4.GetTables(), dbQuery4.GetValues());
          ComDiv.UpdateDB("player_stat_weapons", "owner_id", (object) account.PlayerId, dbquery_0_2.GetTables(), dbquery_0_2.GetValues());
          if (ConfigLoader.WinCashPerBattle && ConfigLoader.ShowCashReceiveWarn)
            account.SendPacket((GameServerPacket) new PROTOCOL_SERVER_MESSAGE_DISCONNECTED_HACK(Translation.GetLabel("CashReceived", new object[1]
            {
              (object) slot.Cash
            })));
          resultType.Add(slot);
        }
      }
      if (resultType.Count > 0)
      {
        // ISSUE: reference to a compiler-generated method
        this.SlotRewards = AllUtils.Class5.GetRewardData(this, resultType);
        this.method_8(resultType, obj1);
      }
      this.UpdateSlotsInfo();
      if (this.RoomType == RoomCondition.FreeForAll)
        return;
      this.method_14(SlotId);
    }
  }

  private void method_8(List<SlotModel> resultType, bool isBotMode)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated method
    SlotModel slotModel = resultType.OrderByDescending<SlotModel, int>(RoomModel.Class11.\u003C\u003E9__119_0 ?? (RoomModel.Class11.\u003C\u003E9__119_0 = new Func<SlotModel, int>(((RoomModel.Class13) RoomModel.Class11.\u003C\u003E9).method_0))).FirstOrDefault<SlotModel>();
    Account account;
    if (slotModel == null || !slotModel.Check || slotModel.State != SlotState.BATTLE || !this.GetPlayerBySlot(slotModel, ref account))
      return;
    StatisticTotal basic = account.Statistic.Basic;
    StatisticSeason season = account.Statistic.Season;
    if (isBotMode || basic == null || season == null)
      return;
    ++basic.MvpCount;
    ++season.MvpCount;
    ComDiv.UpdateDB("player_stat_basics", "mvp_count", (object) basic.MvpCount, "owner_id", (object) account.PlayerId);
    ComDiv.UpdateDB("player_stat_seasons", "mvp_count", (object) season.MvpCount, "owner_id", (object) account.PlayerId);
  }

  private void method_9(
    Account SlotWin,
    StatisticTotal bool_0,
    StatisticSeason statisticSeason_0,
    SlotModel slotModel_0,
    [In] DBQuery obj4,
    [In] DBQuery obj5,
    [In] int obj6,
    [In] bool obj7,
    [In] int obj8)
  {
    bool_0.HeadshotsCount += slotModel_0.AllHeadshots;
    bool_0.KillsCount += slotModel_0.AllKills;
    StatisticTotal statisticTotal = bool_0;
    statisticTotal.set_TotalKillsCount(statisticTotal.get_TotalKillsCount() + slotModel_0.AllKills);
    bool_0.DeathsCount += slotModel_0.AllDeaths;
    bool_0.AssistsCount += slotModel_0.AllAssists;
    statisticSeason_0.HeadshotsCount += slotModel_0.AllHeadshots;
    statisticSeason_0.KillsCount += slotModel_0.AllKills;
    statisticSeason_0.TotalKillsCount += slotModel_0.AllKills;
    statisticSeason_0.DeathsCount += slotModel_0.AllDeaths;
    statisticSeason_0.AssistsCount += slotModel_0.AllAssists;
    this.method_12(slotModel_0, SlotWin.Statistic, obj4, obj5);
    if (this.RoomType == RoomCondition.FreeForAll)
      AllUtils.UpdateMatchCountFFA(this, SlotWin, obj6, obj4, obj5);
    else
      AllUtils.UpdateMatchCount(obj7, SlotWin, obj8, obj4, obj5);
  }

  private void method_10(
    [In] Account obj0,
    [In] StatisticDaily obj1,
    [In] SlotModel obj2,
    [In] DBQuery obj3,
    int dbquery_0,
    bool dbquery_1,
    int int_0)
  {
    obj1.KillsCount += obj2.AllKills;
    obj1.DeathsCount += obj2.AllDeaths;
    obj1.HeadshotsCount += obj2.AllHeadshots;
    this.method_13(obj2, obj0.Statistic, obj3);
    if (this.RoomType == RoomCondition.FreeForAll)
      AllUtils.UpdateMatchDailyRecordFFA(this, obj0, dbquery_0, obj3);
    else
      AllUtils.UpdateDailyRecord(dbquery_1, obj0, int_0, obj3);
  }

  private void method_11(
    [In] Account obj0,
    [In] StatisticWeapon obj1,
    SlotModel slotModel_0,
    DBQuery dbquery_0)
  {
    obj1.AssaultKills += slotModel_0.AR[0];
    obj1.AssaultDeaths += slotModel_0.AR[1];
    obj1.SmgKills += slotModel_0.SMG[0];
    obj1.SmgDeaths += slotModel_0.SMG[1];
    obj1.SniperKills += slotModel_0.SR[0];
    obj1.SniperDeaths += slotModel_0.SR[1];
    obj1.ShotgunKills += slotModel_0.SG[0];
    obj1.ShotgunDeaths += slotModel_0.SG[1];
    obj1.MachinegunKills += slotModel_0.MG[0];
    obj1.MachinegunDeaths += slotModel_0.MG[1];
    obj1.ShieldKills += slotModel_0.SHD[0];
    obj1.ShieldDeaths += slotModel_0.SHD[1];
    AllUtils.UpdateWeaponRecord(obj0, slotModel_0, dbquery_0);
  }

  private void method_12([In] SlotModel obj0, [In] PlayerStatistic obj1, [In] DBQuery obj2, [In] DBQuery obj3)
  {
    if (obj1 == null)
      return;
    if (obj0.AllKills > 0)
    {
      obj2.AddQuery("kills_count", (object) obj1.Basic.KillsCount);
      obj2.AddQuery("total_kills", (object) obj1.Basic.get_TotalKillsCount());
      obj3.AddQuery("kills_count", (object) obj1.Season.KillsCount);
      obj3.AddQuery("total_kills", (object) obj1.Season.TotalKillsCount);
    }
    if (obj0.AllAssists > 0)
    {
      obj2.AddQuery("assists_count", (object) obj1.Basic.AssistsCount);
      obj3.AddQuery("assists_count", (object) obj1.Season.AssistsCount);
    }
    if (obj0.AllDeaths > 0)
    {
      obj2.AddQuery("deaths_count", (object) obj1.Basic.DeathsCount);
      obj3.AddQuery("deaths_count", (object) obj1.Season.DeathsCount);
    }
    if (obj0.AllHeadshots <= 0)
      return;
    obj2.AddQuery("headshots_count", (object) obj1.Basic.HeadshotsCount);
    obj3.AddQuery("headshots_count", (object) obj1.Season.HeadshotsCount);
  }

  private void method_13([In] SlotModel obj0, [In] PlayerStatistic obj1, [In] DBQuery obj2)
  {
    if (obj1 == null)
      return;
    if (obj0.AllKills > 0)
      obj2.AddQuery("kills_count", (object) obj1.Daily.KillsCount);
    if (obj0.AllDeaths > 0)
      obj2.AddQuery("deaths_count", (object) obj1.Daily.DeathsCount);
    if (obj0.AllHeadshots <= 0)
      return;
    obj2.AddQuery("headshots_count", (object) obj1.Daily.HeadshotsCount);
  }

  private void method_14([In] TeamEnum obj0)
  {
    if (this.ChannelType != ChannelType.Clan || this.BlockedClan)
      return;
    SortedList<int, ClanModel> sortedList = new SortedList<int, ClanModel>();
    foreach (SlotModel slot in this.Slots)
    {
      Account account;
      if (slot.State == SlotState.BATTLE && this.GetPlayerBySlot(slot, ref account))
      {
        ClanModel clan = ClanManager.GetClan(account.ClanId);
        if (clan.Id != 0)
        {
          bool flag = slot.Team == obj0;
          clan.Exp += slot.Exp;
          clan.BestPlayers.SetBestExp(slot);
          clan.BestPlayers.SetBestKills(slot);
          clan.BestPlayers.SetBestHeadshot(slot);
          clan.BestPlayers.SetBestWins(account.Statistic, slot, flag);
          clan.BestPlayers.SetBestParticipation(account.Statistic, slot);
          if (!sortedList.ContainsKey(account.ClanId))
          {
            sortedList.Add(account.ClanId, clan);
            if (obj0 != TeamEnum.TEAM_DRAW)
            {
              this.method_15(clan, obj0, slot.Team);
              if (flag)
                ++clan.MatchWins;
              else
                ++clan.MatchLoses;
            }
            ++clan.Matches;
            DaoManagerSQL.UpdateClanBattles(clan.Id, clan.Matches, clan.MatchWins, clan.MatchLoses);
          }
        }
      }
    }
    foreach (ClanModel clanModel in (IEnumerable<ClanModel>) sortedList.Values)
    {
      DaoManagerSQL.UpdateClanExp(clanModel.Id, clanModel.Exp);
      DaoManagerSQL.UpdateClanPoints(clanModel.Id, clanModel.Points);
      DaoManagerSQL.UpdateClanBestPlayers(clanModel);
      RankModel rank = ClanRankXML.GetRank(clanModel.Rank);
      if (rank != null && clanModel.Exp >= rank.OnNextLevel + rank.OnAllExp)
        DaoManagerSQL.UpdateClanRank(clanModel.Id, ++clanModel.Rank);
    }
  }

  private void method_15([In] ClanModel obj0, [In] TeamEnum obj1, [In] TeamEnum obj2)
  {
    if (obj1 == TeamEnum.TEAM_DRAW)
      return;
    if (obj1 == obj2)
    {
      float num = 25f + (this.RoomType != RoomCondition.DeathMatch ? (obj2 == TeamEnum.FR_TEAM ? (float) this.FRRounds : (float) this.CTRounds) : (float) ((obj2 == TeamEnum.FR_TEAM ? this.FRKills : this.CTKills) / 20));
      obj0.Points += num;
    }
    else
    {
      if ((double) obj0.Points == 0.0)
        return;
      float num = 40f - (this.RoomType != RoomCondition.DeathMatch ? (obj2 == TeamEnum.FR_TEAM ? (float) this.FRRounds : (float) this.CTRounds) : (float) ((obj2 == TeamEnum.FR_TEAM ? this.FRKills : this.CTKills) / 20));
      obj0.Points -= num;
    }
  }

  public bool IsStartingMatch() => this.State > RoomState.READY;

  public bool IsPreparing() => this.State >= RoomState.LOADING;

  public void UpdateRoomInfo()
  {
    this.GenerateSeed();
    using (PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK Player = (PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK) new PROTOCOL_ROOM_CHANGE_SLOT_ACK(this))
      this.SendPacketToPlayers((GameServerPacket) Player);
  }

  public void SetSlotCount([In] int obj0, [In] bool obj1, bool dbquery_0)
  {
    MapMatch mapLimit = SystemMapXML.GetMapLimit((int) this.MapId, (int) this.Rule);
    if (mapLimit == null)
      return;
    if (obj0 > mapLimit.Limit)
      obj0 = mapLimit.Limit;
    if (this.RoomType == RoomCondition.Tutorial)
      obj0 = 1;
    if (this.IsBotMode())
      obj0 = 8;
    if (obj0 <= 0)
      obj0 = 1;
    if (obj1)
    {
      lock (this.Slots)
      {
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated method
        foreach (SlotModel slotModel in ((IEnumerable<SlotModel>) this.Slots).Where<SlotModel>(RoomModel.Class11.\u003C\u003E9__130_0 ?? (RoomModel.Class11.\u003C\u003E9__130_0 = new Func<SlotModel, bool>(((RoomModel.Class13) RoomModel.Class11.\u003C\u003E9).method_1))))
        {
          if (slotModel.Id >= obj0)
            slotModel.State = SlotState.CLOSE;
        }
      }
    }
    if (!dbquery_0)
      return;
    this.UpdateSlotsInfo();
  }

  public int GetSlotCount()
  {
    lock (this.Slots)
    {
      int slotCount = 0;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated method
      foreach (SlotModel slotModel in ((IEnumerable<SlotModel>) this.Slots).Where<SlotModel>(RoomModel.Class11.\u003C\u003E9__131_0 ?? (RoomModel.Class11.\u003C\u003E9__131_0 = new Func<SlotModel, bool>(((RoomModel.Class13) RoomModel.Class11.\u003C\u003E9).method_2))))
      {
        if (slotModel.State != SlotState.CLOSE)
          ++slotCount;
      }
      return slotCount;
    }
  }

  public void SwitchSlots(
    [In] List<SlotChange> obj0,
    Account teamEnum_0,
    SlotModel teamEnum_1,
    [In] SlotModel obj3,
    [In] SlotState obj4)
  {
    if (teamEnum_0 == null || teamEnum_1 == null || obj3 == null)
      return;
    obj3.ResetSlot();
    obj3.State = SlotState.NORMAL;
    obj3.PlayerId = teamEnum_0.PlayerId;
    obj3.Equipment = teamEnum_0.Equipment;
    if (obj3.Id == 16 /*0x10*/ || obj3.Id == 17)
    {
      obj3.SpecGM = true;
      obj3.ViewType = ViewerType.SpecGM;
    }
    teamEnum_1.ResetSlot();
    teamEnum_1.State = SlotState.EMPTY;
    teamEnum_1.PlayerId = 0L;
    teamEnum_1.Equipment = (PlayerEquipment) null;
    teamEnum_1.SpecGM = false;
    teamEnum_1.ViewType = ViewerType.Normal;
    if (teamEnum_0.SlotId == this.LeaderSlot)
    {
      this.LeaderName = teamEnum_0.Nickname;
      this.LeaderSlot = obj3.Id;
    }
    teamEnum_0.SlotId = obj3.Id;
    obj0.Add(new SlotChange(teamEnum_1, obj3));
  }

  public void SwitchSlots(List<SlotChange> SlotChanges, int Player, int OldSlot, bool NewSlot)
  {
    SlotModel slot1 = this.Slots[Player];
    SlotModel slot2 = this.Slots[OldSlot];
    if (NewSlot)
    {
      if (slot1.State == SlotState.READY)
        slot1.State = SlotState.NORMAL;
      if (slot2.State == SlotState.READY)
        slot2.State = SlotState.NORMAL;
    }
    slot1.SetSlotId(OldSlot);
    slot2.SetSlotId(Player);
    this.Slots[Player] = slot2;
    this.Slots[OldSlot] = slot1;
    SlotChanges.Add(new SlotChange(slot1, slot2));
  }

  public void ChangeSlotState([In] int obj0, [In] SlotState obj1, [In] bool obj2)
  {
    this.ChangeSlotState(this.GetSlot(obj0), obj1, obj2);
  }

  public void ChangeSlotState([In] SlotModel obj0, [In] SlotState obj1, bool OldSlotId)
  {
    if (obj0 == null || obj0.State == obj1)
      return;
    obj0.State = obj1;
    if (obj1 == SlotState.EMPTY || obj1 == SlotState.CLOSE)
    {
      AllUtils.ResetSlotInfo(this, obj0, false);
      obj0.PlayerId = 0L;
    }
    if (!OldSlotId)
      return;
    this.UpdateSlotsInfo();
  }

  public Account GetPlayerBySlot([In] SlotModel obj0)
  {
    try
    {
      long playerId = obj0.PlayerId;
      return playerId > 0L ? ClanManager.GetAccount(playerId, true) : (Account) null;
    }
    catch
    {
      return (Account) null;
    }
  }

  public Account GetPlayerBySlot([In] int obj0)
  {
    try
    {
      long playerId = this.Slots[obj0].PlayerId;
      return playerId > 0L ? ClanManager.GetAccount(playerId, true) : (Account) null;
    }
    catch
    {
      return (Account) null;
    }
  }

  public bool GetPlayerBySlot(int Slot, ref Account State)
  {
    try
    {
      long playerId = this.Slots[Slot].PlayerId;
      State = playerId > 0L ? ClanManager.GetAccount(playerId, true) : (Account) null;
      return State != null;
    }
    catch
    {
      State = (Account) null;
      return false;
    }
  }

  public bool GetPlayerBySlot([In] SlotModel obj0, [In] ref Account obj1)
  {
    try
    {
      long playerId = obj0.PlayerId;
      obj1 = playerId > 0L ? ClanManager.GetAccount(playerId, true) : (Account) null;
      return obj1 != null;
    }
    catch
    {
      obj1 = (Account) null;
      return false;
    }
  }

  public int GetTimeByMask() => this.TIMES[this.KillTime >> 4];

  public int GetRoundsByMask() => this.ROUNDS[this.KillTime & 15];

  public int GetKillsByMask() => this.KILLS[this.KillTime & 15];

  public void UpdateSlotsInfo()
  {
    using (PROTOCOL_ROOM_GET_SLOTINFO_ACK Player = (PROTOCOL_ROOM_GET_SLOTINFO_ACK) new PROTOCOL_ROOM_GET_SLOTONEINFO_ACK(this))
      this.SendPacketToPlayers((GameServerPacket) Player);
  }

  public bool GetLeader(ref Account SlotId)
  {
    SlotId = (Account) null;
    if (this.GetCountPlayers() <= 0)
      return false;
    if (this.LeaderSlot == -1)
      this.SetNewLeader(-1, SlotState.EMPTY, -1, false);
    if (this.LeaderSlot >= 0)
      SlotId = ClanManager.GetAccount(this.Slots[this.LeaderSlot].PlayerId, true);
    return SlotId != null;
  }

  public Account GetLeader()
  {
    if (this.GetCountPlayers() <= 0)
      return (Account) null;
    if (this.LeaderSlot == -1)
      this.SetNewLeader(-1, SlotState.EMPTY, -1, false);
    return this.LeaderSlot != -1 ? ClanManager.GetAccount(this.Slots[this.LeaderSlot].PlayerId, true) : (Account) null;
  }

  public void SetNewLeader(int SlotId, [Out] SlotState Player, [In] int obj2, [In] bool obj3)
  {
    lock (this.Slots)
    {
      if (SlotId == -1)
      {
        foreach (SlotModel slot in this.Slots)
        {
          if (slot.Id != obj2 && slot.PlayerId > 0L && slot.State > Player)
          {
            this.LeaderSlot = slot.Id;
            break;
          }
        }
      }
      else
        this.LeaderSlot = SlotId;
      if (this.LeaderSlot == -1)
        return;
      SlotModel slot1 = this.Slots[this.LeaderSlot];
      if (slot1.State == SlotState.READY)
        slot1.State = SlotState.NORMAL;
      if (!obj3)
        return;
      this.UpdateSlotsInfo();
    }
  }

  public void SendPacketToPlayers([Out] GameServerPacket Player)
  {
    List<Account> allPlayers = this.GetAllPlayers();
    if (allPlayers.Count == 0)
      return;
    byte[] completeBytes = ((PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK) Player).GetCompleteBytes("Room.SendPacketToPlayers(SendPacket)");
    foreach (Account account in allPlayers)
      account.SendCompletePacket(completeBytes, Player.GetType().Name);
  }

  public void SendPacketToPlayers(GameServerPacket LeaderSlot, long State)
  {
    List<Account> allPlayers = this.GetAllPlayers(State);
    if (allPlayers.Count == 0)
      return;
    byte[] completeBytes = ((PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK) LeaderSlot).GetCompleteBytes("Room.SendPacketToPlayers(SendPacket,long)");
    foreach (Account account in allPlayers)
      account.SendCompletePacket(completeBytes, LeaderSlot.GetType().Name);
  }

  public void SendPacketToPlayers([In] GameServerPacket obj0, [In] SlotState obj1, int OldLeader)
  {
    List<Account> allPlayers = this.GetAllPlayers(obj1, OldLeader);
    if (allPlayers.Count == 0)
      return;
    byte[] completeBytes = ((PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK) obj0).GetCompleteBytes("Room.SendPacketToPlayers(SendPacket,SLOT_STATE,int)");
    foreach (Account account in allPlayers)
      account.SendCompletePacket(completeBytes, obj0.GetType().Name);
  }

  public void SendPacketToPlayers(
    GameServerPacket Packet,
    GameServerPacket PlayerId,
    [In] SlotState obj2,
    [In] int obj3)
  {
    List<Account> allPlayers = this.GetAllPlayers(obj2, obj3);
    if (allPlayers.Count == 0)
      return;
    byte[] completeBytes1 = ((PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK) Packet).GetCompleteBytes("Room.SendPacketToPlayers(SendPacket,SendPacket,SLOT_STATE,int)-1");
    byte[] completeBytes2 = ((PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK) PlayerId).GetCompleteBytes("Room.SendPacketToPlayers(SendPacket,SendPacket,SLOT_STATE,int)-2");
    foreach (Account account in allPlayers)
    {
      account.SendCompletePacket(completeBytes1, Packet.GetType().Name);
      account.SendCompletePacket(completeBytes2, PlayerId.GetType().Name);
    }
  }

  public void SendPacketToPlayers([In] GameServerPacket obj0, [In] SlotState obj1, int Type, [In] int obj3)
  {
    List<Account> allPlayers = this.GetAllPlayers(obj1, Type, obj3);
    if (allPlayers.Count == 0)
      return;
    byte[] completeBytes = ((PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK) obj0).GetCompleteBytes("Room.SendPacketToPlayers(SendPacket,SLOT_STATE,int,int)");
    foreach (Account account in allPlayers)
      account.SendCompletePacket(completeBytes, obj0.GetType().Name);
  }

  public void SendPacketToPlayers(
    [In] GameServerPacket obj0,
    [In] SlotState obj1,
    [In] int obj2,
    int Type,
    [In] int obj4)
  {
    List<Account> allPlayers = this.GetAllPlayers(obj1, obj2, Type, obj4);
    if (allPlayers.Count == 0)
      return;
    byte[] completeBytes = ((PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK) obj0).GetCompleteBytes("Room.SendPacketToPlayers(SendPacket,SLOT_STATE,int,int,int)");
    foreach (Account account in allPlayers)
      account.SendCompletePacket(completeBytes, obj0.GetType().Name);
  }

  public void RemovePlayer(Account Packet, bool State, int Type)
  {
    SlotModel slotModel;
    if (Packet == null || !this.GetSlot(Packet.SlotId, ref slotModel))
      return;
    this.method_16(Packet, slotModel, State, Type);
  }

  public void RemovePlayer([In] Account obj0, [In] SlotModel obj1, [In] bool obj2, int Exception)
  {
    if (obj0 == null || obj1 == null)
      return;
    this.method_16(obj0, obj1, obj2, Exception);
  }

  private void method_16([In] Account obj0, [In] SlotModel obj1, bool QuitMotive = 0, [In] int obj3)
  {
    lock (this.Slots)
    {
      bool flag = false;
      bool InBattle = false;
      if (obj0 != null && obj1 != null)
      {
        if (obj1.State >= SlotState.LOAD)
        {
          if (this.LeaderSlot == obj1.Id)
          {
            int leaderSlot = this.LeaderSlot;
            SlotState Player = SlotState.CLOSE;
            if (this.State == RoomState.BATTLE)
              Player = SlotState.BATTLE_READY;
            else if (this.State >= RoomState.LOADING)
              Player = SlotState.READY;
            if (this.GetAllPlayers(obj1.Id).Count >= 1)
              this.SetNewLeader(-1, Player, leaderSlot, false);
            if (this.GetPlayingPlayers(TeamEnum.TEAM_DRAW, SlotState.READY, 1) >= 2)
            {
              using (PROTOCOL_BATTLE_LEAVEP2PSERVER_ACK leaveP2PserverAck = (PROTOCOL_BATTLE_LEAVEP2PSERVER_ACK) new PROTOCOL_BATTLE_MISSION_BOMB_UNINSTALL_ACK(this))
                this.SendPacketToPlayers((GameServerPacket) leaveP2PserverAck, SlotState.RENDEZVOUS, 1, obj1.Id);
            }
            InBattle = true;
          }
          using (PROTOCOL_BATTLE_GIVEUPBATTLE_ACK battleGiveupbattleAck = (PROTOCOL_BATTLE_GIVEUPBATTLE_ACK) new PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK(obj0, obj3))
            this.SendPacketToPlayers((GameServerPacket) battleGiveupbattleAck, SlotState.READY, 1, !QuitMotive ? obj1.Id : -1);
          RoundSync.SendUDPPlayerLeave(this, obj1.Id);
          obj1.ResetSlot();
          if (this.VoteKick != null)
            this.VoteKick.TotalArray[obj1.Id] = false;
        }
        obj1.PlayerId = 0L;
        obj1.Equipment = (PlayerEquipment) null;
        obj1.State = SlotState.EMPTY;
        if (this.State == RoomState.COUNTDOWN)
        {
          if (obj1.Id == this.LeaderSlot)
          {
            this.State = RoomState.READY;
            flag = true;
            this.CountdownTime.StopJob();
            using (PROTOCOL_BATTLE_START_COUNTDOWN_ACK Player = (PROTOCOL_BATTLE_START_COUNTDOWN_ACK) new PROTOCOL_BATTLE_START_GAME_ACK(CountDownEnum.StopByHost))
              this.SendPacketToPlayers((GameServerPacket) Player);
          }
          else if (this.GetPlayingPlayers(obj1.Team, SlotState.READY, 0) == 0)
          {
            if (obj1.Id != this.LeaderSlot)
              this.ChangeSlotState(this.LeaderSlot, SlotState.NORMAL, false);
            this.StopCountDown(CountDownEnum.StopByPlayer, false);
            flag = true;
          }
        }
        else if (this.IsPreparing())
        {
          AllUtils.BattleEndPlayersCount(this, this.IsBotMode());
          if (this.State == RoomState.BATTLE)
            AllUtils.BattleEndRoundPlayersCount(this);
        }
        this.CheckToEndWaitingBattle(InBattle);
        this.RequestRoomMaster.Remove(obj0.PlayerId);
        if (this.VoteTime.IsTimer() && this.VoteKick != null && this.VoteKick.VictimIdx == obj0.SlotId && obj3 != 2)
        {
          this.VoteTime.StopJob();
          this.VoteKick = (VoteKickModel) null;
          using (PROTOCOL_BATTLE_NOTIFY_KICKVOTE_CANCEL_ACK kickvoteCancelAck = (PROTOCOL_BATTLE_NOTIFY_KICKVOTE_CANCEL_ACK) new PROTOCOL_BATTLE_PRESTARTBATTLE_ACK())
            this.SendPacketToPlayers((GameServerPacket) kickvoteCancelAck, SlotState.BATTLE, 0);
        }
        MatchModel match = obj0.Match;
        if (match != null && obj0.MatchSlot >= 0)
        {
          match.Slots[obj0.MatchSlot].State = SlotMatchState.Normal;
          using (PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK registMercenaryAck = (PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK) new PROTOCOL_CLAN_WAR_TEAM_CHATTING_ACK(match))
            match.SendPacketToPlayers((GameServerPacket) registMercenaryAck);
        }
        obj0.Room = (RoomModel) null;
        obj0.SlotId = -1;
        obj0.Status.UpdateRoom(byte.MaxValue);
        AllUtils.SyncPlayerToClanMembers(obj0);
        AllUtils.SyncPlayerToFriends(obj0, false);
        obj0.UpdateCacheInfo();
      }
      this.UpdateSlotsInfo();
      if (!flag)
        return;
      this.UpdateRoomInfo();
    }
  }

  public int AddPlayer([In] Account obj0)
  {
    lock (this.Slots)
    {
      foreach (SlotModel slot in this.Slots)
      {
        if (slot.PlayerId == 0L && slot.State == SlotState.EMPTY)
        {
          if ((slot.Id == 16 /*0x10*/ || slot.Id == 17) && !obj0.IsGM())
            return -1;
          slot.PlayerId = obj0.PlayerId;
          slot.State = SlotState.NORMAL;
          obj0.Room = this;
          obj0.SlotId = slot.Id;
          slot.Equipment = obj0.Equipment;
          obj0.Status.UpdateRoom((byte) this.RoomId);
          AllUtils.SyncPlayerToClanMembers(obj0);
          AllUtils.SyncPlayerToFriends(obj0, false);
          obj0.UpdateCacheInfo();
          return slot.Id;
        }
      }
    }
    return -1;
  }

  public int AddPlayer(Account account_0, TeamEnum slotModel_0)
  {
    lock (this.Slots)
    {
      foreach (int team in this.GetTeamArray(slotModel_0))
      {
        SlotModel slot = this.Slots[team];
        if (slot.PlayerId == 0L && slot.State == SlotState.EMPTY)
        {
          if ((slot.Id == 16 /*0x10*/ || slot.Id == 17) && !account_0.IsGM())
            return -1;
          slot.PlayerId = account_0.PlayerId;
          slot.State = SlotState.NORMAL;
          account_0.Room = this;
          account_0.SlotId = slot.Id;
          slot.Equipment = account_0.Equipment;
          account_0.Status.UpdateRoom((byte) this.RoomId);
          AllUtils.SyncPlayerToClanMembers(account_0);
          AllUtils.SyncPlayerToFriends(account_0, false);
          account_0.UpdateCacheInfo();
          return slot.Id;
        }
      }
    }
    return -1;
  }

  public int[] GetTeamArray([In] TeamEnum obj0)
  {
    if (obj0 == TeamEnum.FR_TEAM)
      return this.FR_TEAM;
    return obj0 != TeamEnum.CT_TEAM ? this.ALL_TEAM : this.CT_TEAM;
  }

  public List<Account> GetAllPlayers([In] SlotState obj0, [In] int obj1)
  {
    List<Account> allPlayers = new List<Account>();
    lock (this.Slots)
    {
      foreach (SlotModel slot in this.Slots)
      {
        if (slot.PlayerId > 0L && (obj1 == 0 && slot.State == obj0 || obj1 == 1 && slot.State > obj0))
        {
          Account account = ClanManager.GetAccount(slot.PlayerId, true);
          if (account != null && account.SlotId != -1)
            allPlayers.Add(account);
        }
      }
    }
    return allPlayers;
  }

  public List<Account> GetAllPlayers(SlotState Player, int TeamIdx, [In] int obj2)
  {
    List<Account> allPlayers = new List<Account>();
    lock (this.Slots)
    {
      foreach (SlotModel slot in this.Slots)
      {
        if (slot.PlayerId > 0L && slot.Id != obj2 && (TeamIdx == 0 && slot.State == Player || TeamIdx == 1 && slot.State > Player))
        {
          Account account = ClanManager.GetAccount(slot.PlayerId, true);
          if (account != null && account.SlotId != -1)
            allPlayers.Add(account);
        }
      }
    }
    return allPlayers;
  }

  public List<Account> GetAllPlayers(SlotState State, int Type, [In] int obj2, [In] int obj3)
  {
    List<Account> allPlayers = new List<Account>();
    lock (this.Slots)
    {
      foreach (SlotModel slot in this.Slots)
      {
        if (slot.PlayerId > 0L && slot.Id != obj2 && slot.Id != obj3 && (Type == 0 && slot.State == State || Type == 1 && slot.State > State))
        {
          Account account = ClanManager.GetAccount(slot.PlayerId, true);
          if (account != null && account.SlotId != -1)
            allPlayers.Add(account);
        }
      }
    }
    return allPlayers;
  }

  public List<Account> GetAllPlayers([In] int obj0)
  {
    List<Account> allPlayers = new List<Account>();
    lock (this.Slots)
    {
      foreach (SlotModel slot in this.Slots)
      {
        long playerId = slot.PlayerId;
        if (playerId > 0L && slot.Id != obj0)
        {
          Account account = ClanManager.GetAccount(playerId, true);
          if (account != null && account.SlotId != -1)
            allPlayers.Add(account);
        }
      }
    }
    return allPlayers;
  }

  public List<Account> GetAllPlayers(long State)
  {
    List<Account> allPlayers = new List<Account>();
    lock (this.Slots)
    {
      foreach (SlotModel slot in this.Slots)
      {
        if (slot.PlayerId > 0L && slot.PlayerId != State)
        {
          Account account = ClanManager.GetAccount(slot.PlayerId, true);
          if (account != null && account.SlotId != -1)
            allPlayers.Add(account);
        }
      }
    }
    return allPlayers;
  }

  public List<Account> GetAllPlayers()
  {
    List<Account> allPlayers = new List<Account>();
    lock (this.Slots)
    {
      foreach (SlotModel slot in this.Slots)
      {
        if (slot.PlayerId > 0L)
        {
          Account account = ClanManager.GetAccount(slot.PlayerId, true);
          if (account != null && account.SlotId != -1)
            allPlayers.Add(account);
        }
      }
    }
    return allPlayers;
  }

  public int GetCountPlayers()
  {
    int countPlayers = 0;
    lock (this.Slots)
    {
      foreach (SlotModel slot in this.Slots)
      {
        if (slot.PlayerId > 0L)
        {
          Account account = ClanManager.GetAccount(slot.PlayerId, true);
          if (account != null && account.SlotId != -1)
            ++countPlayers;
        }
      }
    }
    return countPlayers;
  }

  public int GetPlayingPlayers([In] TeamEnum obj0, bool Type)
  {
    int playingPlayers = 0;
    lock (this.Slots)
    {
      foreach (SlotModel slot in this.Slots)
      {
        if (slot.PlayerId > 0L && (slot.Team == obj0 || obj0 == TeamEnum.TEAM_DRAW) && (Type && slot.State == SlotState.BATTLE_LOAD && !slot.Spectator || !Type && slot.State >= SlotState.LOAD))
          ++playingPlayers;
      }
    }
    return playingPlayers;
  }

  public int GetPlayingPlayers([In] TeamEnum obj0, [In] SlotState obj1, [In] int obj2)
  {
    int playingPlayers = 0;
    lock (this.Slots)
    {
      foreach (SlotModel slot in this.Slots)
      {
        if (slot.PlayerId > 0L && (obj2 == 0 && slot.State == obj1 || obj2 == 1 && slot.State > obj1) && (obj0 == TeamEnum.TEAM_DRAW || slot.Team == obj0))
          ++playingPlayers;
      }
    }
    return playingPlayers;
  }

  public int GetPlayingPlayers(TeamEnum Team, SlotState InBattle, [In] int obj2, [In] int obj3)
  {
    int playingPlayers = 0;
    lock (this.Slots)
    {
      foreach (SlotModel slot in this.Slots)
      {
        if (slot.Id != obj3 && slot.PlayerId > 0L && (obj2 == 0 && slot.State == InBattle || obj2 == 1 && slot.State > InBattle) && (Team == TeamEnum.TEAM_DRAW || slot.Team == Team))
          ++playingPlayers;
      }
    }
    return playingPlayers;
  }

  public void GetPlayingPlayers([In] bool obj0, [In] ref int obj1, ref int Type)
  {
    obj1 = 0;
    Type = 0;
    lock (this.Slots)
    {
      foreach (SlotModel slot in this.Slots)
      {
        if (slot.PlayerId > 0L && (obj0 && slot.State == SlotState.BATTLE && !slot.Spectator || !obj0 && slot.State >= SlotState.RENDEZVOUS))
        {
          if (slot.Team == TeamEnum.FR_TEAM)
            ++obj1;
          else
            ++Type;
        }
      }
    }
  }

  public void GetPlayingPlayers(
    [In] bool obj0,
    [In] ref int obj1,
    ref int Type,
    ref int Exception,
    [In] ref int obj4)
  {
    obj1 = 0;
    Type = 0;
    Exception = 0;
    obj4 = 0;
    lock (this.Slots)
    {
      foreach (SlotModel slot in this.Slots)
      {
        if (slot.DeathState.HasFlag((Enum) DeadEnum.Dead))
        {
          if (slot.Team == TeamEnum.FR_TEAM)
            ++Exception;
          else
            ++obj4;
        }
        if (slot.PlayerId > 0L && (obj0 && slot.State == SlotState.BATTLE && !slot.Spectator || !obj0 && slot.State >= SlotState.RENDEZVOUS))
        {
          if (slot.Team == TeamEnum.FR_TEAM)
            ++obj1;
          else
            ++Type;
        }
      }
    }
  }

  public void CheckToEndWaitingBattle(bool InBattle)
  {
    if (this.State != RoomState.COUNTDOWN && this.State != RoomState.LOADING && this.State != RoomState.RENDEZVOUS || !InBattle && this.Slots[this.LeaderSlot].State != SlotState.BATTLE_READY)
      return;
    AllUtils.EndBattleNoPoints(this);
  }

  public void SpawnReadyPlayers()
  {
    lock (this.Slots)
    {
      bool flag = this.ThisModeHaveRounds() && (this.CountdownIG == (byte) 3 || this.CountdownIG == (byte) 5 || this.CountdownIG == (byte) 7 || this.CountdownIG == (byte) 9);
      if (((this.State != RoomState.PRE_BATTLE ? 0 : (!this.PreMatchCD ? 1 : 0)) & (flag ? 1 : 0)) != 0 && !this.IsBotMode())
      {
        this.PreMatchCD = true;
        using (PROTOCOL_BATTLE_COUNT_DOWN_ACK Player = (PROTOCOL_BATTLE_COUNT_DOWN_ACK) new PROTOCOL_BATTLE_ENDBATTLE_ACK((int) this.CountdownIG))
          this.SendPacketToPlayers((GameServerPacket) Player);
      }
      // ISSUE: reference to a compiler-generated method
      this.PreMatchTime.StartJob(this.CountdownIG == (byte) 0 ? 0 : (int) this.CountdownIG * 1000 + 250, new TimerCallback(((RoomModel.Class11) this).method_24));
    }
  }

  private void method_17()
  {
    DateTime dateTime = DateTimeUtil.Now();
    foreach (SlotModel slot in this.Slots)
    {
      if (slot.State == SlotState.BATTLE_READY && slot.IsPlaying == 0 && slot.PlayerId > 0L)
      {
        slot.IsPlaying = 1;
        slot.StartTime = dateTime;
        slot.State = SlotState.BATTLE;
        if (this.State == RoomState.BATTLE && (this.RoomType == RoomCondition.Bomb || this.RoomType == RoomCondition.Annihilation || this.RoomType == RoomCondition.Destroy || this.RoomType == RoomCondition.Ace || this.RoomType == RoomCondition.Glass))
          slot.Spectator = true;
      }
    }
    this.UpdateSlotsInfo();
    List<int> dinossaurs = AllUtils.GetDinossaurs(this, false, -1);
    if (this.State == RoomState.PRE_BATTLE)
    {
      this.BattleStart = this.IsDinoMode("") ? dateTime.AddMinutes(5.0) : dateTime;
      this.method_1();
    }
    bool flag = false;
    using (PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK roundPreStartAck = (PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK) new PROTOCOL_BATTLE_MISSION_ROUND_START_ACK(this, dinossaurs))
    {
      using (PROTOCOL_BATTLE_MISSION_ROUND_START_ACK missionRoundStartAck = (PROTOCOL_BATTLE_MISSION_ROUND_START_ACK) new PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_ACK(this))
      {
        using (PROTOCOL_BATTLE_RECORD_ACK protocolBattleRecordAck = (PROTOCOL_BATTLE_RECORD_ACK) new PROTOCOL_BATTLE_RESPAWN_FOR_AI_ACK(this))
        {
          byte[] completeBytes1 = ((PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK) roundPreStartAck).GetCompleteBytes("Room.BaseSpawnReadyPlayers-1");
          byte[] completeBytes2 = ((PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK) missionRoundStartAck).GetCompleteBytes("Room.BaseSpawnReadyPlayers-2");
          byte[] completeBytes3 = ((PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK) protocolBattleRecordAck).GetCompleteBytes("Room.BaseSpawnReadyPlayers-3");
          foreach (SlotModel slot in this.Slots)
          {
            Account slotModel_1;
            if (slot.State == SlotState.BATTLE && slot.IsPlaying == 1 && this.GetPlayerBySlot(slot, ref slotModel_1))
            {
              slot.FirstInactivityOff = true;
              slot.IsPlaying = 2;
              if (this.State == RoomState.PRE_BATTLE)
              {
                using (PROTOCOL_BATTLE_STARTBATTLE_ACK battleStartbattleAck = (PROTOCOL_BATTLE_STARTBATTLE_ACK) new PROTOCOL_BATTLE_START_COUNTDOWN_ACK(slot, slotModel_1, dinossaurs, true))
                  this.SendPacketToPlayers((GameServerPacket) battleStartbattleAck, SlotState.READY, 1);
                slotModel_1.SendCompletePacket(completeBytes1, roundPreStartAck.GetType().Name);
                if (this.IsDinoMode(""))
                  flag = true;
                else
                  slotModel_1.SendCompletePacket(completeBytes2, missionRoundStartAck.GetType().Name);
              }
              else if (this.State == RoomState.BATTLE)
              {
                using (PROTOCOL_BATTLE_STARTBATTLE_ACK battleStartbattleAck = (PROTOCOL_BATTLE_STARTBATTLE_ACK) new PROTOCOL_BATTLE_START_COUNTDOWN_ACK(slot, slotModel_1, dinossaurs, false))
                  this.SendPacketToPlayers((GameServerPacket) battleStartbattleAck, SlotState.READY, 1);
                if (this.RoomType != RoomCondition.Bomb && this.RoomType != RoomCondition.Annihilation && this.RoomType != RoomCondition.Destroy && this.RoomType != RoomCondition.Ace && this.RoomType != RoomCondition.Glass)
                  slotModel_1.SendCompletePacket(completeBytes1, roundPreStartAck.GetType().Name);
                else
                  SendClanInfo.SendUDPPlayerSync(this, slot, (CouponEffects) 0, 1);
                slotModel_1.SendCompletePacket(completeBytes2, missionRoundStartAck.GetType().Name);
                slotModel_1.SendCompletePacket(completeBytes3, protocolBattleRecordAck.GetType().Name);
              }
            }
          }
        }
      }
    }
    if (this.State == RoomState.PRE_BATTLE)
    {
      this.State = RoomState.BATTLE;
      this.UpdateRoomInfo();
    }
    if (!flag)
      return;
    this.method_18();
  }

  private void method_18()
  {
    // ISSUE: reference to a compiler-generated method
    this.RoundTime.StartJob(5250, new TimerCallback(((RoomModel.Class11) this).method_25));
  }

  public bool IsDinoMode([In] string obj0)
  {
    switch (obj0)
    {
      case "DE":
        return this.RoomType == RoomCondition.Boss;
      case "CC":
        return this.RoomType == RoomCondition.CrossCounter;
      default:
        return this.RoomType == RoomCondition.Boss || this.RoomType == RoomCondition.CrossCounter;
    }
  }

  public int GetReadyPlayers()
  {
    int readyPlayers = 0;
    foreach (SlotModel slot in this.Slots)
    {
      if (slot != null && slot.State >= SlotState.READY && slot.Equipment != null)
      {
        Account playerBySlot = this.GetPlayerBySlot(slot);
        if (playerBySlot != null && playerBySlot.SlotId == slot.Id)
          ++readyPlayers;
      }
    }
    return readyPlayers;
  }

  public int ResetReadyPlayers()
  {
    int num = 0;
    foreach (SlotModel slot in this.Slots)
    {
      if (slot.State == SlotState.READY)
      {
        slot.State = SlotState.NORMAL;
        ++num;
      }
    }
    return num;
  }

  public TeamEnum CheckTeam([In] int obj0)
  {
    // ISSUE: variable of a compiler-generated type
    RoomModel.Class13 class13 = (RoomModel.Class13) new AccountManager();
    // ISSUE: reference to a compiler-generated field
    class13.int_0 = obj0;
    if (Array.Exists<int>(this.FR_TEAM, new Predicate<int>(((AccountManager) class13).method_0)))
      return TeamEnum.FR_TEAM;
    return Array.Exists<int>(this.CT_TEAM, new Predicate<int>(((AccountManager) class13).method_1)) ? TeamEnum.CT_TEAM : TeamEnum.ALL_TEAM;
  }

  public TeamEnum ValidateTeam([In] TeamEnum obj0, [In] TeamEnum obj1)
  {
    if (this.RoomType == RoomCondition.FreeForAll)
      return obj1;
    if (!this.SwapRound)
      return obj0;
    return obj0 != TeamEnum.FR_TEAM ? TeamEnum.FR_TEAM : TeamEnum.CT_TEAM;
  }

  public string RandomName(int host)
  {
    switch (host)
    {
      case 1:
        return "Feel the Headshots!!";
      case 2:
        return "Land of Dead!";
      case 3:
        return "Kill! or be Killed!!";
      case 4:
        return "Show Me Your Skills!!";
      default:
        return "Point Blank!!";
    }
  }

  public void CheckGhostSlot(SlotModel Dino = "")
  {
    if (Dino.State == SlotState.EMPTY || Dino.State == SlotState.CLOSE || Dino.PlayerId != 0L || this.IsBotMode())
      return;
    Dino.ResetSlot();
    Dino.State = SlotState.EMPTY;
  }
}
