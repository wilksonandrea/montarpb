namespace Server.Game.Data.Models
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Managers;
    using Plugin.Core.Models;
    using Plugin.Core.SQL;
    using Plugin.Core.Utility;
    using Plugin.Core.XML;
    using Server.Game;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Sync.Server;
    using Server.Game.Data.Utils;
    using Server.Game.Data.XML;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;

    public class RoomModel
    {
        public SlotModel[] Slots = new SlotModel[0x12];
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
        public readonly int[] TIMES = new int[] { 3, 3, 3, 5, 7, 5, 10, 15, 20, 0x19, 30 };
        public readonly int[] KILLS = new int[] { 15, 30, 50, 60, 80, 100, 120, 140, 160 };
        public readonly int[] ROUNDS = new int[] { 1, 2, 3, 5, 7, 9 };
        public readonly int[] FR_TEAM = new int[] { 0, 2, 4, 6, 8, 10, 12, 14, 0x10 };
        public readonly int[] CT_TEAM = new int[] { 1, 3, 5, 7, 9, 11, 13, 15, 0x11 };
        public readonly int[] ALL_TEAM = new int[] { 
            0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15,
            0x10, 0x11
        };
        public readonly int[] INVERT_FR_TEAM = new int[] { 0x10, 14, 12, 10, 8, 6, 4, 2, 0 };
        public readonly int[] INVERT_CT_TEAM = new int[] { 0x11, 15, 13, 11, 9, 7, 5, 3, 1 };
        public byte[] RandomMaps;
        public byte[] LeaderAddr = new byte[4];
        public byte[] HitParts = new byte[] { 
            0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15,
            0x10, 0x11, 0x12, 0x13, 20, 0x15, 0x16, 0x17, 0x18, 0x19, 0x1a, 0x1b, 0x1c, 0x1d, 30, 0x1f,
            0x20, 0x21, 0x22
        };
        public (byte[], int[]) SlotRewards;
        public Plugin.Core.Enums.ChannelType ChannelType;
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

        public RoomModel(int int_0, ChannelModel channelModel_0)
        {
            this.RoomId = int_0;
            for (int i = 0; i < this.Slots.Length; i++)
            {
                this.Slots[i] = new SlotModel(i);
            }
            this.ChannelId = channelModel_0.Id;
            this.ChannelType = channelModel_0.Type;
            this.ServerId = channelModel_0.ServerId;
            this.Rounds = 1;
            this.AiCount = 1;
            this.AiLevel = 1;
            this.TRex = -1;
            this.Ping = 5;
            this.Name = "";
            this.Password = "";
            this.MapName = "";
            this.LeaderName = "";
            this.method_0();
            DateTime time = DateTimeUtil.Now();
            this.LastChangeTeam = time;
            this.LastPingSync = time;
        }

        public int AddPlayer(Account Player)
        {
            int num2;
            SlotModel[] slots = this.Slots;
            lock (slots)
            {
                SlotModel[] modelArray2 = this.Slots;
                int index = 0;
                while (true)
                {
                    if (index < modelArray2.Length)
                    {
                        SlotModel model = modelArray2[index];
                        if ((model.PlayerId != 0) || (model.State != SlotState.EMPTY))
                        {
                            index++;
                            continue;
                        }
                        if (((model.Id == 0x10) || (model.Id == 0x11)) && !Player.IsGM())
                        {
                            num2 = -1;
                        }
                        else
                        {
                            model.PlayerId = Player.PlayerId;
                            model.State = SlotState.NORMAL;
                            Player.Room = this;
                            Player.SlotId = model.Id;
                            model.Equipment = Player.Equipment;
                            Player.Status.UpdateRoom((byte) this.RoomId);
                            AllUtils.SyncPlayerToClanMembers(Player);
                            AllUtils.SyncPlayerToFriends(Player, false);
                            Player.UpdateCacheInfo();
                            num2 = model.Id;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                    break;
                }
            }
            return num2;
        }

        public int AddPlayer(Account Player, TeamEnum TeamIdx)
        {
            int num3;
            SlotModel[] slots = this.Slots;
            lock (slots)
            {
                int[] teamArray = this.GetTeamArray(TeamIdx);
                int index = 0;
                while (true)
                {
                    if (index < teamArray.Length)
                    {
                        int num2 = teamArray[index];
                        SlotModel model = this.Slots[num2];
                        if ((model.PlayerId != 0) || (model.State != SlotState.EMPTY))
                        {
                            index++;
                            continue;
                        }
                        if (((model.Id == 0x10) || (model.Id == 0x11)) && !Player.IsGM())
                        {
                            num3 = -1;
                        }
                        else
                        {
                            model.PlayerId = Player.PlayerId;
                            model.State = SlotState.NORMAL;
                            Player.Room = this;
                            Player.SlotId = model.Id;
                            model.Equipment = Player.Equipment;
                            Player.Status.UpdateRoom((byte) this.RoomId);
                            AllUtils.SyncPlayerToClanMembers(Player);
                            AllUtils.SyncPlayerToFriends(Player, false);
                            Player.UpdateCacheInfo();
                            num3 = model.Id;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                    break;
                }
            }
            return num3;
        }

        public void CalculateResult()
        {
            SlotModel[] slots = this.Slots;
            lock (slots)
            {
                this.method_7(AllUtils.GetWinnerTeam(this), this.IsBotMode());
            }
        }

        public void CalculateResult(TeamEnum resultType)
        {
            SlotModel[] slots = this.Slots;
            lock (slots)
            {
                this.method_7(resultType, this.IsBotMode());
            }
        }

        public void CalculateResult(TeamEnum resultType, bool isBotMode)
        {
            SlotModel[] slots = this.Slots;
            lock (slots)
            {
                this.method_7(resultType, isBotMode);
            }
        }

        public void CalculateResultFreeForAll(int SlotWin)
        {
            SlotModel[] slots = this.Slots;
            lock (slots)
            {
                this.method_7((TeamEnum) SlotWin, false);
            }
        }

        public void ChangeRounds()
        {
            this.Rounds++;
            if (this.method_2())
            {
                this.SwapRound ??= true;
            }
        }

        public void ChangeSlotState(SlotModel Slot, SlotState State, bool SendInfo)
        {
            if ((Slot != null) && (Slot.State != State))
            {
                Slot.State = State;
                if ((State == SlotState.EMPTY) || (State == SlotState.CLOSE))
                {
                    AllUtils.ResetSlotInfo(this, Slot, false);
                    Slot.PlayerId = 0L;
                }
                if (SendInfo)
                {
                    this.UpdateSlotsInfo();
                }
            }
        }

        public void ChangeSlotState(int SlotId, SlotState State, bool SendInfo)
        {
            this.ChangeSlotState(this.GetSlot(SlotId), State, SendInfo);
        }

        public void CheckGhostSlot(SlotModel Slot)
        {
            if ((Slot.State != SlotState.EMPTY) && ((Slot.State != SlotState.CLOSE) && ((Slot.PlayerId == 0) && !this.IsBotMode())))
            {
                Slot.ResetSlot();
                Slot.State = SlotState.EMPTY;
            }
        }

        public TeamEnum CheckTeam(int SlotIdx)
        {
            Class13 class2 = new Class13 {
                int_0 = SlotIdx
            };
            return (!Array.Exists<int>(this.FR_TEAM, new Predicate<int>(class2.method_0)) ? (!Array.Exists<int>(this.CT_TEAM, new Predicate<int>(class2.method_1)) ? TeamEnum.ALL_TEAM : TeamEnum.CT_TEAM) : TeamEnum.FR_TEAM);
        }

        public void CheckToEndWaitingBattle(bool host)
        {
            if (((this.State == RoomState.COUNTDOWN) || ((this.State == RoomState.LOADING) || (this.State == RoomState.RENDEZVOUS))) && (host || (this.Slots[this.LeaderSlot].State == SlotState.BATTLE_READY)))
            {
                AllUtils.EndBattleNoPoints(this);
            }
        }

        public void GenerateSeed()
        {
            this.Seed = (uint) (((((int) (this.MapId & 0xff)) << 20) | (((byte) (this.Rule & 0xff)) << 12)) | (this.RoomType & ((RoomCondition) 0xfff)));
        }

        public List<Account> GetAllPlayers()
        {
            List<Account> list = new List<Account>();
            SlotModel[] slots = this.Slots;
            lock (slots)
            {
                foreach (SlotModel model in this.Slots)
                {
                    if (model.PlayerId > 0L)
                    {
                        Account item = AccountManager.GetAccount(model.PlayerId, true);
                        if ((item != null) && (item.SlotId != -1))
                        {
                            list.Add(item);
                        }
                    }
                }
            }
            return list;
        }

        public List<Account> GetAllPlayers(int Exception)
        {
            List<Account> list = new List<Account>();
            SlotModel[] slots = this.Slots;
            lock (slots)
            {
                foreach (SlotModel model in this.Slots)
                {
                    long playerId = model.PlayerId;
                    if ((playerId > 0L) && (model.Id != Exception))
                    {
                        Account item = AccountManager.GetAccount(playerId, true);
                        if ((item != null) && (item.SlotId != -1))
                        {
                            list.Add(item);
                        }
                    }
                }
            }
            return list;
        }

        public List<Account> GetAllPlayers(long Exception)
        {
            List<Account> list = new List<Account>();
            SlotModel[] slots = this.Slots;
            lock (slots)
            {
                foreach (SlotModel model in this.Slots)
                {
                    if ((model.PlayerId > 0L) && (model.PlayerId != Exception))
                    {
                        Account item = AccountManager.GetAccount(model.PlayerId, true);
                        if ((item != null) && (item.SlotId != -1))
                        {
                            list.Add(item);
                        }
                    }
                }
            }
            return list;
        }

        public List<Account> GetAllPlayers(SlotState State, int Type)
        {
            List<Account> list = new List<Account>();
            SlotModel[] slots = this.Slots;
            lock (slots)
            {
                foreach (SlotModel model in this.Slots)
                {
                    if ((model.PlayerId > 0L) && (((Type == 0) && (model.State == State)) || ((Type == 1) && (model.State > State))))
                    {
                        Account item = AccountManager.GetAccount(model.PlayerId, true);
                        if ((item != null) && (item.SlotId != -1))
                        {
                            list.Add(item);
                        }
                    }
                }
            }
            return list;
        }

        public List<Account> GetAllPlayers(SlotState State, int Type, int Exception)
        {
            List<Account> list = new List<Account>();
            SlotModel[] slots = this.Slots;
            lock (slots)
            {
                foreach (SlotModel model in this.Slots)
                {
                    if (((model.PlayerId > 0L) && (model.Id != Exception)) && (((Type == 0) && (model.State == State)) || ((Type == 1) && (model.State > State))))
                    {
                        Account item = AccountManager.GetAccount(model.PlayerId, true);
                        if ((item != null) && (item.SlotId != -1))
                        {
                            list.Add(item);
                        }
                    }
                }
            }
            return list;
        }

        public List<Account> GetAllPlayers(SlotState State, int Type, int Exception, int Exception2)
        {
            List<Account> list = new List<Account>();
            SlotModel[] slots = this.Slots;
            lock (slots)
            {
                foreach (SlotModel model in this.Slots)
                {
                    if (((model.PlayerId > 0L) && ((model.Id != Exception) && (model.Id != Exception2))) && (((Type == 0) && (model.State == State)) || ((Type == 1) && (model.State > State))))
                    {
                        Account item = AccountManager.GetAccount(model.PlayerId, true);
                        if ((item != null) && (item.SlotId != -1))
                        {
                            list.Add(item);
                        }
                    }
                }
            }
            return list;
        }

        public ChannelModel GetChannel() => 
            ChannelsXML.GetChannel(this.ServerId, this.ChannelId);

        public bool GetChannel(out ChannelModel Channel)
        {
            Channel = ChannelsXML.GetChannel(this.ServerId, this.ChannelId);
            return (Channel != null);
        }

        public int GetCountPlayers()
        {
            int num = 0;
            SlotModel[] slots = this.Slots;
            lock (slots)
            {
                foreach (SlotModel model in this.Slots)
                {
                    if (model.PlayerId > 0L)
                    {
                        Account account = AccountManager.GetAccount(model.PlayerId, true);
                        if ((account != null) && (account.SlotId != -1))
                        {
                            num++;
                        }
                    }
                }
            }
            return num;
        }

        public int GetFlag()
        {
            int num = 0;
            if (this.Flag.HasFlag(RoomStageFlag.TEAM_SWAP))
            {
                num++;
            }
            if (this.Flag.HasFlag(RoomStageFlag.RANDOM_MAP))
            {
                num += 2;
            }
            if (this.Flag.HasFlag(RoomStageFlag.PASSWORD) || (this.Password.Length > 0))
            {
                num += 4;
            }
            if (this.Flag.HasFlag(RoomStageFlag.OBSERVER_MODE))
            {
                num += 8;
            }
            if (this.Flag.HasFlag(RoomStageFlag.REAL_IP))
            {
                num += 0x10;
            }
            if (this.Flag.HasFlag(RoomStageFlag.TEAM_BALANCE) || (this.BalanceType == TeamBalance.Count))
            {
                num += 0x20;
            }
            if (this.Flag.HasFlag(RoomStageFlag.OBSERVER))
            {
                num += 0x40;
            }
            if (this.Flag.HasFlag(RoomStageFlag.INTER_ENTER) || ((this.Limit > 0) && this.IsStartingMatch()))
            {
                num += 0x80;
            }
            this.Flag = (RoomStageFlag) num;
            return num;
        }

        public int GetInBattleTime()
        {
            int duration = 0;
            DateTime time = new DateTime();
            if ((this.BattleStart != time) && ((this.State == RoomState.BATTLE) || (this.State == RoomState.PRE_BATTLE)))
            {
                duration = (int) ComDiv.GetDuration(this.BattleStart);
                if (duration < 0)
                {
                    duration = 0;
                }
            }
            return duration;
        }

        public int GetInBattleTimeLeft() => 
            (this.GetTimeByMask() * 60) - this.GetInBattleTime();

        public int GetKillsByMask() => 
            this.KILLS[this.KillTime & 15];

        public Account GetLeader()
        {
            if (this.GetCountPlayers() <= 0)
            {
                return null;
            }
            if (this.LeaderSlot == -1)
            {
                this.SetNewLeader(-1, SlotState.EMPTY, -1, false);
            }
            return ((this.LeaderSlot == -1) ? null : AccountManager.GetAccount(this.Slots[this.LeaderSlot].PlayerId, true));
        }

        public bool GetLeader(out Account Player)
        {
            Player = null;
            if (this.GetCountPlayers() <= 0)
            {
                return false;
            }
            if (this.LeaderSlot == -1)
            {
                this.SetNewLeader(-1, SlotState.EMPTY, -1, false);
            }
            if (this.LeaderSlot >= 0)
            {
                Player = AccountManager.GetAccount(this.Slots[this.LeaderSlot].PlayerId, true);
            }
            return (Player != null);
        }

        public Account GetPlayerBySlot(SlotModel Slot)
        {
            try
            {
                long playerId = Slot.PlayerId;
                return ((playerId > 0L) ? AccountManager.GetAccount(playerId, true) : null);
            }
            catch
            {
                return null;
            }
        }

        public Account GetPlayerBySlot(int SlotId)
        {
            try
            {
                long playerId = this.Slots[SlotId].PlayerId;
                return ((playerId > 0L) ? AccountManager.GetAccount(playerId, true) : null);
            }
            catch
            {
                return null;
            }
        }

        public bool GetPlayerBySlot(SlotModel Slot, out Account Player)
        {
            try
            {
                long playerId = Slot.PlayerId;
                Player = (playerId > 0L) ? AccountManager.GetAccount(playerId, true) : null;
                return (Player != null);
            }
            catch
            {
                Player = null;
                return false;
            }
        }

        public bool GetPlayerBySlot(int SlotId, out Account Player)
        {
            try
            {
                long playerId = this.Slots[SlotId].PlayerId;
                Player = (playerId > 0L) ? AccountManager.GetAccount(playerId, true) : null;
                return (Player != null);
            }
            catch
            {
                Player = null;
                return false;
            }
        }

        public int GetPlayingPlayers(TeamEnum Team, bool InBattle)
        {
            int num = 0;
            SlotModel[] slots = this.Slots;
            lock (slots)
            {
                foreach (SlotModel model in this.Slots)
                {
                    if (((model.PlayerId > 0L) && ((model.Team == Team) || (Team == TeamEnum.TEAM_DRAW))) && ((InBattle && ((model.State == SlotState.BATTLE_LOAD) && !model.Spectator)) || (!InBattle && (model.State >= SlotState.LOAD))))
                    {
                        num++;
                    }
                }
            }
            return num;
        }

        public int GetPlayingPlayers(TeamEnum Team, SlotState State, int Type)
        {
            int num = 0;
            SlotModel[] slots = this.Slots;
            lock (slots)
            {
                foreach (SlotModel model in this.Slots)
                {
                    if (((model.PlayerId > 0L) && (((Type == 0) && (model.State == State)) || ((Type == 1) && (model.State > State)))) && ((Team == TeamEnum.TEAM_DRAW) || (model.Team == Team)))
                    {
                        num++;
                    }
                }
            }
            return num;
        }

        public void GetPlayingPlayers(bool InBattle, out int PlayerFR, out int PlayerCT)
        {
            PlayerFR = 0;
            PlayerCT = 0;
            SlotModel[] slots = this.Slots;
            lock (slots)
            {
                foreach (SlotModel model in this.Slots)
                {
                    if ((model.PlayerId > 0L) && ((InBattle && ((model.State == SlotState.BATTLE) && !model.Spectator)) || (!InBattle && (model.State >= SlotState.RENDEZVOUS))))
                    {
                        if (model.Team == TeamEnum.FR_TEAM)
                        {
                            PlayerFR++;
                        }
                        else
                        {
                            PlayerCT++;
                        }
                    }
                }
            }
        }

        public int GetPlayingPlayers(TeamEnum Team, SlotState State, int Type, int Exception)
        {
            int num = 0;
            SlotModel[] slots = this.Slots;
            lock (slots)
            {
                foreach (SlotModel model in this.Slots)
                {
                    if ((((model.Id != Exception) && (model.PlayerId > 0L)) && (((Type == 0) && (model.State == State)) || ((Type == 1) && (model.State > State)))) && ((Team == TeamEnum.TEAM_DRAW) || (model.Team == Team)))
                    {
                        num++;
                    }
                }
            }
            return num;
        }

        public void GetPlayingPlayers(bool InBattle, out int PlayerFR, out int PlayerCT, out int DeathFR, out int DeathCT)
        {
            PlayerFR = 0;
            PlayerCT = 0;
            DeathFR = 0;
            DeathCT = 0;
            SlotModel[] slots = this.Slots;
            lock (slots)
            {
                foreach (SlotModel model in this.Slots)
                {
                    if (model.DeathState.HasFlag(DeadEnum.Dead))
                    {
                        if (model.Team == TeamEnum.FR_TEAM)
                        {
                            DeathFR++;
                        }
                        else
                        {
                            DeathCT++;
                        }
                    }
                    if ((model.PlayerId > 0L) && ((InBattle && ((model.State == SlotState.BATTLE) && !model.Spectator)) || (!InBattle && (model.State >= SlotState.RENDEZVOUS))))
                    {
                        if (model.Team == TeamEnum.FR_TEAM)
                        {
                            PlayerFR++;
                        }
                        else
                        {
                            PlayerCT++;
                        }
                    }
                }
            }
        }

        public int GetReadyPlayers()
        {
            int num = 0;
            foreach (SlotModel model in this.Slots)
            {
                if ((model != null) && ((model.State >= SlotState.READY) && (model.Equipment != null)))
                {
                    Account playerBySlot = this.GetPlayerBySlot(model);
                    if ((playerBySlot != null) && (playerBySlot.SlotId == model.Id))
                    {
                        num++;
                    }
                }
            }
            return num;
        }

        public int GetRoundsByMask() => 
            this.ROUNDS[this.KillTime & 15];

        public SlotModel GetSlot(int SlotIdx)
        {
            SlotModel[] slots = this.Slots;
            lock (slots)
            {
                return (((SlotIdx < 0) || (SlotIdx > 0x11)) ? null : this.Slots[SlotIdx]);
            }
        }

        public bool GetSlot(int SlotId, out SlotModel Slot)
        {
            Slot = null;
            SlotModel[] slots = this.Slots;
            lock (slots)
            {
                if ((SlotId >= 0) && (SlotId <= 0x11))
                {
                    Slot = this.Slots[SlotId];
                }
                return (Slot != null);
            }
        }

        public int GetSlotCount()
        {
            SlotModel[] slots = this.Slots;
            lock (slots)
            {
                int num = 0;
                Func<SlotModel, bool> predicate = Class11.<>9__131_0;
                if (Class11.<>9__131_0 == null)
                {
                    Func<SlotModel, bool> local1 = Class11.<>9__131_0;
                    predicate = Class11.<>9__131_0 = new Func<SlotModel, bool>(Class11.<>9.method_2);
                }
                using (IEnumerator<SlotModel> enumerator = this.Slots.Where<SlotModel>(predicate).GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        if (enumerator.Current.State == SlotState.CLOSE)
                        {
                            continue;
                        }
                        num++;
                    }
                }
                return num;
            }
        }

        public int[] GetTeamArray(TeamEnum Team) => 
            (Team == TeamEnum.FR_TEAM) ? this.FR_TEAM : ((Team == TeamEnum.CT_TEAM) ? this.CT_TEAM : this.ALL_TEAM);

        public int GetTimeByMask() => 
            this.TIMES[this.KillTime >> 4];

        public bool IsBotMode() => 
            (this.Stage == StageOptions.AI) || ((this.Stage == StageOptions.DieHard) || (this.Stage == StageOptions.Infection));

        public bool IsDinoMode(string Dino = "") => 
            !Dino.Equals("DE") ? (!Dino.Equals("CC") ? ((this.RoomType == RoomCondition.Boss) || (this.RoomType == RoomCondition.CrossCounter)) : (this.RoomType == RoomCondition.CrossCounter)) : (this.RoomType == RoomCondition.Boss);

        public bool IsPreparing() => 
            this.State >= RoomState.LOADING;

        public bool IsStartingMatch() => 
            this.State > RoomState.READY;

        private void method_0()
        {
            this.UniqueRoomId = (uint) ((((this.ServerId & 0xff) << 20) | ((this.ChannelId & 0xff) << 12)) | (this.RoomId & 0xfff));
        }

        private void method_1()
        {
            if (this.RoomType == RoomCondition.Defense)
            {
                if (this.MapId == MapIdEnum.BlackPanther)
                {
                    this.Bar1 = 0x1770;
                    this.Bar2 = 0x2328;
                }
            }
            else if (this.RoomType == RoomCondition.Destroy)
            {
                if (this.MapId == MapIdEnum.Helispot)
                {
                    this.Bar1 = 0x2ee0;
                    this.Bar2 = 0x2ee0;
                }
                else if (this.MapId == MapIdEnum.BreakDown)
                {
                    this.Bar1 = 0x1770;
                    this.Bar2 = 0x1770;
                }
            }
        }

        private void method_10(Account account_0, StatisticDaily statisticDaily_0, SlotModel slotModel_0, DBQuery dbquery_0, int int_0, bool bool_0, int int_1)
        {
            statisticDaily_0.KillsCount += slotModel_0.AllKills;
            statisticDaily_0.DeathsCount += slotModel_0.AllDeaths;
            statisticDaily_0.HeadshotsCount += slotModel_0.AllHeadshots;
            this.method_13(slotModel_0, account_0.Statistic, dbquery_0);
            if (this.RoomType == RoomCondition.FreeForAll)
            {
                AllUtils.UpdateMatchDailyRecordFFA(this, account_0, int_0, dbquery_0);
            }
            else
            {
                AllUtils.UpdateDailyRecord(bool_0, account_0, int_1, dbquery_0);
            }
        }

        private void method_11(Account account_0, StatisticWeapon statisticWeapon_0, SlotModel slotModel_0, DBQuery dbquery_0)
        {
            statisticWeapon_0.AssaultKills += slotModel_0.AR[0];
            statisticWeapon_0.AssaultDeaths += slotModel_0.AR[1];
            statisticWeapon_0.SmgKills += slotModel_0.SMG[0];
            statisticWeapon_0.SmgDeaths += slotModel_0.SMG[1];
            statisticWeapon_0.SniperKills += slotModel_0.SR[0];
            statisticWeapon_0.SniperDeaths += slotModel_0.SR[1];
            statisticWeapon_0.ShotgunKills += slotModel_0.SG[0];
            statisticWeapon_0.ShotgunDeaths += slotModel_0.SG[1];
            statisticWeapon_0.MachinegunKills += slotModel_0.MG[0];
            statisticWeapon_0.MachinegunDeaths += slotModel_0.MG[1];
            statisticWeapon_0.ShieldKills += slotModel_0.SHD[0];
            statisticWeapon_0.ShieldDeaths += slotModel_0.SHD[1];
            AllUtils.UpdateWeaponRecord(account_0, slotModel_0, dbquery_0);
        }

        private void method_12(SlotModel slotModel_0, PlayerStatistic playerStatistic_0, DBQuery dbquery_0, DBQuery dbquery_1)
        {
            if (playerStatistic_0 != null)
            {
                if (slotModel_0.AllKills > 0)
                {
                    dbquery_0.AddQuery("kills_count", playerStatistic_0.Basic.KillsCount);
                    dbquery_0.AddQuery("total_kills", playerStatistic_0.Basic.TotalKillsCount);
                    dbquery_1.AddQuery("kills_count", playerStatistic_0.Season.KillsCount);
                    dbquery_1.AddQuery("total_kills", playerStatistic_0.Season.TotalKillsCount);
                }
                if (slotModel_0.AllAssists > 0)
                {
                    dbquery_0.AddQuery("assists_count", playerStatistic_0.Basic.AssistsCount);
                    dbquery_1.AddQuery("assists_count", playerStatistic_0.Season.AssistsCount);
                }
                if (slotModel_0.AllDeaths > 0)
                {
                    dbquery_0.AddQuery("deaths_count", playerStatistic_0.Basic.DeathsCount);
                    dbquery_1.AddQuery("deaths_count", playerStatistic_0.Season.DeathsCount);
                }
                if (slotModel_0.AllHeadshots > 0)
                {
                    dbquery_0.AddQuery("headshots_count", playerStatistic_0.Basic.HeadshotsCount);
                    dbquery_1.AddQuery("headshots_count", playerStatistic_0.Season.HeadshotsCount);
                }
            }
        }

        private void method_13(SlotModel slotModel_0, PlayerStatistic playerStatistic_0, DBQuery dbquery_0)
        {
            if (playerStatistic_0 != null)
            {
                if (slotModel_0.AllKills > 0)
                {
                    dbquery_0.AddQuery("kills_count", playerStatistic_0.Daily.KillsCount);
                }
                if (slotModel_0.AllDeaths > 0)
                {
                    dbquery_0.AddQuery("deaths_count", playerStatistic_0.Daily.DeathsCount);
                }
                if (slotModel_0.AllHeadshots > 0)
                {
                    dbquery_0.AddQuery("headshots_count", playerStatistic_0.Daily.HeadshotsCount);
                }
            }
        }

        private void method_14(TeamEnum teamEnum_0)
        {
            if ((this.ChannelType == Plugin.Core.Enums.ChannelType.Clan) && !this.BlockedClan)
            {
                SortedList<int, ClanModel> list = new SortedList<int, ClanModel>();
                SlotModel[] slots = this.Slots;
                int index = 0;
                while (index < slots.Length)
                {
                    Account account;
                    SlotModel slot = slots[index];
                    if ((slot.State == SlotState.BATTLE) && this.GetPlayerBySlot(slot, out account))
                    {
                        ClanModel clan = ClanManager.GetClan(account.ClanId);
                        if (clan.Id != 0)
                        {
                            bool wonTheMatch = slot.Team == teamEnum_0;
                            clan.Exp += slot.Exp;
                            clan.BestPlayers.SetBestExp(slot);
                            clan.BestPlayers.SetBestKills(slot);
                            clan.BestPlayers.SetBestHeadshot(slot);
                            clan.BestPlayers.SetBestWins(account.Statistic, slot, wonTheMatch);
                            clan.BestPlayers.SetBestParticipation(account.Statistic, slot);
                            if (!list.ContainsKey(account.ClanId))
                            {
                                list.Add(account.ClanId, clan);
                                if (teamEnum_0 != TeamEnum.TEAM_DRAW)
                                {
                                    this.method_15(clan, teamEnum_0, slot.Team);
                                    if (wonTheMatch)
                                    {
                                        clan.MatchWins++;
                                    }
                                    else
                                    {
                                        clan.MatchLoses++;
                                    }
                                }
                                clan.Matches++;
                                DaoManagerSQL.UpdateClanBattles(clan.Id, clan.Matches, clan.MatchWins, clan.MatchLoses);
                            }
                        }
                    }
                    index++;
                }
                foreach (ClanModel model3 in list.Values)
                {
                    DaoManagerSQL.UpdateClanExp(model3.Id, model3.Exp);
                    DaoManagerSQL.UpdateClanPoints(model3.Id, model3.Points);
                    DaoManagerSQL.UpdateClanBestPlayers(model3);
                    RankModel rank = ClanRankXML.GetRank(model3.Rank);
                    if ((rank != null) && (model3.Exp >= (rank.OnNextLevel + rank.OnAllExp)))
                    {
                        index = model3.Rank + 1;
                        model3.Rank = index;
                        DaoManagerSQL.UpdateClanRank(model3.Id, index);
                    }
                }
            }
        }

        private void method_15(ClanModel clanModel_0, TeamEnum teamEnum_0, TeamEnum teamEnum_1)
        {
            if (teamEnum_0 != TeamEnum.TEAM_DRAW)
            {
                if (teamEnum_0 == teamEnum_1)
                {
                    float num = (this.RoomType != RoomCondition.DeathMatch) ? ((teamEnum_1 == TeamEnum.FR_TEAM) ? ((float) this.FRRounds) : ((float) this.CTRounds)) : ((float) (((teamEnum_1 == TeamEnum.FR_TEAM) ? this.FRKills : this.CTKills) / 20));
                    float num2 = 25f + num;
                    clanModel_0.Points += num2;
                }
                else if (clanModel_0.Points != 0f)
                {
                    float num3 = (this.RoomType != RoomCondition.DeathMatch) ? ((teamEnum_1 == TeamEnum.FR_TEAM) ? ((float) this.FRRounds) : ((float) this.CTRounds)) : ((float) (((teamEnum_1 == TeamEnum.FR_TEAM) ? this.FRKills : this.CTKills) / 20));
                    float num4 = 40f - num3;
                    clanModel_0.Points -= num4;
                }
            }
        }

        private void method_16(Account account_0, SlotModel slotModel_0, bool bool_0, int int_0)
        {
            SlotModel[] slots = this.Slots;
            lock (slots)
            {
                bool flag2 = false;
                bool host = false;
                if ((account_0 != null) && (slotModel_0 != null))
                {
                    if (slotModel_0.State >= SlotState.LOAD)
                    {
                        if (this.LeaderSlot == slotModel_0.Id)
                        {
                            int leaderSlot = this.LeaderSlot;
                            SlotState cLOSE = SlotState.CLOSE;
                            if (this.State == RoomState.BATTLE)
                            {
                                cLOSE = SlotState.BATTLE_READY;
                            }
                            else if (this.State >= RoomState.LOADING)
                            {
                                cLOSE = SlotState.READY;
                            }
                            if (this.GetAllPlayers(slotModel_0.Id).Count >= 1)
                            {
                                this.SetNewLeader(-1, cLOSE, leaderSlot, false);
                            }
                            if (this.GetPlayingPlayers(TeamEnum.TEAM_DRAW, SlotState.READY, 1) >= 2)
                            {
                                using (PROTOCOL_BATTLE_LEAVEP2PSERVER_ACK protocol_battle_leaveppserver_ack = new PROTOCOL_BATTLE_LEAVEP2PSERVER_ACK(this))
                                {
                                    this.SendPacketToPlayers(protocol_battle_leaveppserver_ack, SlotState.RENDEZVOUS, 1, slotModel_0.Id);
                                }
                            }
                            host = true;
                        }
                        using (PROTOCOL_BATTLE_GIVEUPBATTLE_ACK protocol_battle_giveupbattle_ack = new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(account_0, int_0))
                        {
                            this.SendPacketToPlayers(protocol_battle_giveupbattle_ack, SlotState.READY, 1, !bool_0 ? slotModel_0.Id : -1);
                        }
                        BattleLeaveSync.SendUDPPlayerLeave(this, slotModel_0.Id);
                        slotModel_0.ResetSlot();
                        if (this.VoteKick != null)
                        {
                            this.VoteKick.TotalArray[slotModel_0.Id] = false;
                        }
                    }
                    slotModel_0.PlayerId = 0L;
                    slotModel_0.Equipment = null;
                    slotModel_0.State = SlotState.EMPTY;
                    if (this.State != RoomState.COUNTDOWN)
                    {
                        if (this.IsPreparing())
                        {
                            AllUtils.BattleEndPlayersCount(this, this.IsBotMode());
                            if (this.State == RoomState.BATTLE)
                            {
                                AllUtils.BattleEndRoundPlayersCount(this);
                            }
                        }
                    }
                    else if (slotModel_0.Id != this.LeaderSlot)
                    {
                        if (this.GetPlayingPlayers(slotModel_0.Team, SlotState.READY, 0) == 0)
                        {
                            if (slotModel_0.Id != this.LeaderSlot)
                            {
                                this.ChangeSlotState(this.LeaderSlot, SlotState.NORMAL, false);
                            }
                            this.StopCountDown(CountDownEnum.StopByPlayer, false);
                            flag2 = true;
                        }
                    }
                    else
                    {
                        this.State = RoomState.READY;
                        flag2 = true;
                        this.CountdownTime.StopJob();
                        using (PROTOCOL_BATTLE_START_COUNTDOWN_ACK protocol_battle_start_countdown_ack = new PROTOCOL_BATTLE_START_COUNTDOWN_ACK(CountDownEnum.StopByHost))
                        {
                            this.SendPacketToPlayers(protocol_battle_start_countdown_ack);
                        }
                    }
                    this.CheckToEndWaitingBattle(host);
                    this.RequestRoomMaster.Remove(account_0.PlayerId);
                    if (this.VoteTime.IsTimer() && ((this.VoteKick != null) && ((this.VoteKick.VictimIdx == account_0.SlotId) && (int_0 != 2))))
                    {
                        this.VoteTime.StopJob();
                        this.VoteKick = null;
                        using (PROTOCOL_BATTLE_NOTIFY_KICKVOTE_CANCEL_ACK protocol_battle_notify_kickvote_cancel_ack = new PROTOCOL_BATTLE_NOTIFY_KICKVOTE_CANCEL_ACK())
                        {
                            this.SendPacketToPlayers(protocol_battle_notify_kickvote_cancel_ack, SlotState.BATTLE, 0);
                        }
                    }
                    MatchModel match = account_0.Match;
                    if ((match != null) && (account_0.MatchSlot >= 0))
                    {
                        match.Slots[account_0.MatchSlot].State = SlotMatchState.Normal;
                        using (PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK protocol_clan_war_regist_mercenary_ack = new PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK(match))
                        {
                            match.SendPacketToPlayers(protocol_clan_war_regist_mercenary_ack);
                        }
                    }
                    account_0.Room = null;
                    account_0.SlotId = -1;
                    account_0.Status.UpdateRoom(0xff);
                    AllUtils.SyncPlayerToClanMembers(account_0);
                    AllUtils.SyncPlayerToFriends(account_0, false);
                    account_0.UpdateCacheInfo();
                }
                this.UpdateSlotsInfo();
                if (flag2)
                {
                    this.UpdateRoomInfo();
                }
            }
        }

        private void method_17()
        {
            DateTime time = DateTimeUtil.Now();
            foreach (SlotModel model in this.Slots)
            {
                if ((model.State == SlotState.BATTLE_READY) && ((model.IsPlaying == 0) && (model.PlayerId > 0L)))
                {
                    model.IsPlaying = 1;
                    model.StartTime = time;
                    model.State = SlotState.BATTLE;
                    if ((this.State == RoomState.BATTLE) && ((this.RoomType == RoomCondition.Bomb) || ((this.RoomType == RoomCondition.Annihilation) || ((this.RoomType == RoomCondition.Destroy) || ((this.RoomType == RoomCondition.Ace) || (this.RoomType == RoomCondition.Glass))))))
                    {
                        model.Spectator = true;
                    }
                }
            }
            this.UpdateSlotsInfo();
            List<int> list = AllUtils.GetDinossaurs(this, false, -1);
            if (this.State == RoomState.PRE_BATTLE)
            {
                this.BattleStart = this.IsDinoMode("") ? time.AddMinutes(5.0) : time;
                this.method_1();
            }
            bool flag = false;
            using (PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK protocol_battle_mission_round_pre_start_ack = new PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK(this, list))
            {
                using (PROTOCOL_BATTLE_MISSION_ROUND_START_ACK protocol_battle_mission_round_start_ack = new PROTOCOL_BATTLE_MISSION_ROUND_START_ACK(this))
                {
                    using (PROTOCOL_BATTLE_RECORD_ACK protocol_battle_record_ack = new PROTOCOL_BATTLE_RECORD_ACK(this))
                    {
                        byte[] completeBytes = protocol_battle_mission_round_pre_start_ack.GetCompleteBytes("Room.BaseSpawnReadyPlayers-1");
                        byte[] data = protocol_battle_mission_round_start_ack.GetCompleteBytes("Room.BaseSpawnReadyPlayers-2");
                        byte[] buffer3 = protocol_battle_record_ack.GetCompleteBytes("Room.BaseSpawnReadyPlayers-3");
                        foreach (SlotModel model2 in this.Slots)
                        {
                            Account account;
                            if ((model2.State == SlotState.BATTLE) && ((model2.IsPlaying == 1) && this.GetPlayerBySlot(model2, out account)))
                            {
                                model2.FirstInactivityOff = true;
                                model2.IsPlaying = 2;
                                if (this.State == RoomState.PRE_BATTLE)
                                {
                                    using (PROTOCOL_BATTLE_STARTBATTLE_ACK protocol_battle_startbattle_ack = new PROTOCOL_BATTLE_STARTBATTLE_ACK(model2, account, list, true))
                                    {
                                        this.SendPacketToPlayers(protocol_battle_startbattle_ack, SlotState.READY, 1);
                                    }
                                    account.SendCompletePacket(completeBytes, protocol_battle_mission_round_pre_start_ack.GetType().Name);
                                    if (this.IsDinoMode(""))
                                    {
                                        flag = true;
                                    }
                                    else
                                    {
                                        account.SendCompletePacket(data, protocol_battle_mission_round_start_ack.GetType().Name);
                                    }
                                }
                                else if (this.State == RoomState.BATTLE)
                                {
                                    using (PROTOCOL_BATTLE_STARTBATTLE_ACK protocol_battle_startbattle_ack2 = new PROTOCOL_BATTLE_STARTBATTLE_ACK(model2, account, list, false))
                                    {
                                        this.SendPacketToPlayers(protocol_battle_startbattle_ack2, SlotState.READY, 1);
                                    }
                                    if ((this.RoomType != RoomCondition.Bomb) && ((this.RoomType != RoomCondition.Annihilation) && ((this.RoomType != RoomCondition.Destroy) && ((this.RoomType != RoomCondition.Ace) && (this.RoomType != RoomCondition.Glass)))))
                                    {
                                        account.SendCompletePacket(completeBytes, protocol_battle_mission_round_pre_start_ack.GetType().Name);
                                    }
                                    else
                                    {
                                        EquipmentSync.SendUDPPlayerSync(this, model2, 0L, 1);
                                    }
                                    account.SendCompletePacket(data, protocol_battle_mission_round_start_ack.GetType().Name);
                                    account.SendCompletePacket(buffer3, protocol_battle_record_ack.GetType().Name);
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
            if (flag)
            {
                this.method_18();
            }
        }

        private void method_18()
        {
            this.RoundTime.StartJob(0x1482, new TimerCallback(this.method_25));
        }

        [CompilerGenerated]
        private void method_19(object object_0)
        {
            if ((this != null) && this.ActiveC4)
            {
                if (this.SwapRound)
                {
                    this.CTRounds++;
                }
                else
                {
                    this.FRRounds++;
                }
                this.ActiveC4 = false;
                AllUtils.BattleEndRound(this, TeamEnum.FR_TEAM, RoundEndType.BombFire);
            }
            object obj2 = object_0;
            lock (obj2)
            {
                this.BombTime.StopJob();
            }
        }

        private bool method_2()
        {
            if (!this.Flag.HasFlag(RoomStageFlag.TEAM_SWAP))
            {
                return false;
            }
            if (this.IsDinoMode(""))
            {
                return (this.Rounds == 2);
            }
            int num = this.GetRoundsByMask() - 1;
            return (((this.FRRounds != num) || (this.CTRounds != 0)) ? ((this.CTRounds == num) && (this.FRRounds == 0)) : true);
        }

        [CompilerGenerated]
        private void method_20(object object_0)
        {
            AllUtils.VotekickResult(this);
            object obj2 = object_0;
            lock (obj2)
            {
                this.VoteTime.StopJob();
            }
        }

        [CompilerGenerated]
        private void method_21(object object_0)
        {
            this.method_4();
            object obj2 = object_0;
            lock (obj2)
            {
                this.RoundTime.StopJob();
            }
        }

        [CompilerGenerated]
        private void method_22(object object_0)
        {
            this.method_5();
            object obj2 = object_0;
            lock (obj2)
            {
                this.RoundTeamSwap.StopJob();
            }
        }

        [CompilerGenerated]
        private void method_23(object object_0)
        {
            this.method_6();
            object obj2 = object_0;
            lock (obj2)
            {
                this.CountdownTime.StopJob();
            }
        }

        [CompilerGenerated]
        private void method_24(object object_0)
        {
            this.method_17();
            object obj2 = object_0;
            lock (obj2)
            {
                this.PreMatchTime.StopJob();
            }
        }

        [CompilerGenerated]
        private void method_25(object object_0)
        {
            if (this.State == RoomState.BATTLE)
            {
                using (PROTOCOL_BATTLE_MISSION_ROUND_START_ACK protocol_battle_mission_round_start_ack = new PROTOCOL_BATTLE_MISSION_ROUND_START_ACK(this))
                {
                    this.SendPacketToPlayers(protocol_battle_mission_round_start_ack, SlotState.BATTLE, 0);
                }
            }
            object obj2 = object_0;
            lock (obj2)
            {
                this.RoundTime.StopJob();
            }
        }

        private void method_3(EventErrorEnum eventErrorEnum_0, Account account_0, SlotModel slotModel_0)
        {
            account_0.SendPacket(new PROTOCOL_SERVER_MESSAGE_KICK_BATTLE_PLAYER_ACK(eventErrorEnum_0));
            account_0.SendPacket(new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(account_0, 0));
            this.ChangeSlotState(slotModel_0.Id, SlotState.NORMAL, true);
            AllUtils.BattleEndPlayersCount(this, this.IsBotMode());
        }

        private void method_4()
        {
            foreach (SlotModel model in this.Slots)
            {
                if (model.PlayerId > 0L)
                {
                    if (!model.DeathState.HasFlag(DeadEnum.UseChat))
                    {
                        model.DeathState |= DeadEnum.UseChat;
                    }
                    if (model.Spectator)
                    {
                        model.Spectator = false;
                    }
                }
            }
            this.StopBomb();
            DateTime time = DateTimeUtil.Now();
            if (this.State == RoomState.BATTLE)
            {
                this.BattleStart = this.IsDinoMode("") ? time.AddSeconds(5.0) : time;
            }
            List<int> list = AllUtils.GetDinossaurs(this, false, -1);
            using (PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK protocol_battle_mission_round_pre_start_ack = new PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK(this, list))
            {
                this.SendPacketToPlayers(protocol_battle_mission_round_pre_start_ack, SlotState.BATTLE, 0);
            }
            if (this.method_2())
            {
                this.RoundTeamSwap.StartJob(0x1482, new TimerCallback(this.method_22));
            }
            else
            {
                this.method_5();
            }
            this.StopBomb();
        }

        private void method_5()
        {
            using (PROTOCOL_BATTLE_MISSION_ROUND_START_ACK protocol_battle_mission_round_start_ack = new PROTOCOL_BATTLE_MISSION_ROUND_START_ACK(this))
            {
                this.SendPacketToPlayers(protocol_battle_mission_round_start_ack, SlotState.BATTLE, 0);
            }
        }

        private void method_6()
        {
            if ((this.Slots[this.LeaderSlot].State == SlotState.READY) && (this.State == RoomState.COUNTDOWN))
            {
                this.StartBattle(true);
            }
            else
            {
                using (PROTOCOL_BATTLE_READYBATTLE_ACK protocol_battle_readybattle_ack = new PROTOCOL_BATTLE_READYBATTLE_ACK(0x80001008))
                {
                    byte[] completeBytes = protocol_battle_readybattle_ack.GetCompleteBytes("Room.ReadyBattle");
                    foreach (Account account in this.GetAllPlayers(SlotState.READY, 0))
                    {
                        SlotModel slot = this.GetSlot(account.SlotId);
                        if ((slot != null) && (slot.State == SlotState.READY))
                        {
                            account.SendCompletePacket(completeBytes, protocol_battle_readybattle_ack.GetType().Name);
                        }
                    }
                }
            }
        }

        private void method_7(TeamEnum teamEnum_0, bool bool_0)
        {
            ServerConfig config = GameXender.Client.Config;
            EventRankUpModel runningEvent = EventRankUpXML.GetRunningEvent();
            EventBoostModel model2 = EventBoostXML.GetRunningEvent();
            EventPlaytimeModel evPlaytime = EventPlaytimeXML.GetRunningEvent();
            BattlePassModel activeSeasonPass = SeasonChallengeXML.GetActiveSeasonPass();
            DateTime date = DateTimeUtil.Now();
            int[] numArray = new int[0x12];
            int index = 0;
            if (config == null)
            {
                CLogger.Print("Server Config Null. RoomResult canceled.", LoggerType.Warning, null);
            }
            else
            {
                List<SlotModel> slots = new List<SlotModel>();
                for (int i = 0; i < 0x12; i++)
                {
                    Account account;
                    SlotModel slot = this.Slots[i];
                    numArray[i] = (slot.PlayerId == 0) ? 0 : slot.AllKills;
                    if (numArray[i] > numArray[index])
                    {
                        index = i;
                    }
                    if (!slot.Check && ((slot.State == SlotState.BATTLE) && this.GetPlayerBySlot(slot, out account)))
                    {
                        StatisticTotal basic = account.Statistic.Basic;
                        StatisticSeason season = account.Statistic.Season;
                        StatisticDaily daily = account.Statistic.Daily;
                        StatisticWeapon weapon = account.Statistic.Weapon;
                        DBQuery query = new DBQuery();
                        DBQuery query2 = new DBQuery();
                        DBQuery query3 = new DBQuery();
                        DBQuery query4 = new DBQuery();
                        DBQuery query5 = new DBQuery();
                        slot.Check = true;
                        double num3 = slot.InBattleTime(date);
                        int gold = account.Gold;
                        int exp = account.Exp;
                        int cash = account.Cash;
                        if (bool_0)
                        {
                            int num17 = this.IngameAiLevel * (150 + slot.AllDeaths);
                            num17++;
                            int num18 = slot.Score / num17;
                            slot.Gold += num18;
                            slot.Exp += num18;
                        }
                        else
                        {
                            if (config.Missions)
                            {
                                AllUtils.EndMatchMission(this, account, slot, teamEnum_0);
                                if (slot.MissionsCompleted)
                                {
                                    account.Mission = slot.Missions;
                                    DaoManagerSQL.UpdateCurrentPlayerMissionList(account.PlayerId, account.Mission);
                                }
                                AllUtils.GenerateMissionAwards(account, query);
                            }
                            int num16 = ((slot.AllKills != 0) || (slot.AllDeaths != 0)) ? ((int) num3) : ((int) (num3 / 3.0));
                            if ((this.RoomType != RoomCondition.Bomb) && ((this.RoomType != RoomCondition.Annihilation) && (this.RoomType != RoomCondition.Ace)))
                            {
                                slot.Exp = ((int) ((slot.Score + (((double) num16) / 2.5)) + (slot.AllDeaths * 1.8))) + (slot.Objects * 20);
                                slot.Gold = ((int) ((slot.Score + (((double) num16) / 3.0)) + (slot.AllDeaths * 1.8))) + (slot.Objects * 20);
                                slot.Cash = ((int) (((((double) slot.Score) / 1.5) + (((double) num16) / 4.5)) + (slot.AllDeaths * 1.1))) + (slot.Objects * 20);
                            }
                            else
                            {
                                slot.Exp = ((int) ((slot.Score + (((double) num16) / 2.5)) + (slot.AllDeaths * 2.2))) + (slot.Objects * 20);
                                slot.Gold = ((int) ((slot.Score + (((double) num16) / 3.0)) + (slot.AllDeaths * 2.2))) + (slot.Objects * 20);
                                slot.Cash = ((int) (((slot.Score / 2) + (((double) num16) / 6.5)) + (slot.AllDeaths * 1.5))) + (slot.Objects * 10);
                            }
                            bool flag = slot.Team == teamEnum_0;
                            if ((this.Rule != MapRules.Chaos) && (this.Rule != MapRules.HeadHunter))
                            {
                                if ((basic != null) && (season != null))
                                {
                                    this.method_9(account, basic, season, slot, query2, query3, index, flag, (int) teamEnum_0);
                                }
                                if (daily != null)
                                {
                                    this.method_10(account, daily, slot, query4, index, flag, (int) teamEnum_0);
                                }
                                if (weapon != null)
                                {
                                    this.method_11(account, weapon, slot, query5);
                                }
                            }
                            if (flag || ((this.RoomType == RoomCondition.FreeForAll) && (teamEnum_0 == index)))
                            {
                                slot.Gold += ComDiv.Percentage(slot.Gold, 15);
                                slot.Exp += ComDiv.Percentage(slot.Exp, 20);
                            }
                            if (slot.EarnedEXP > 0)
                            {
                                slot.Exp += slot.EarnedEXP * 5;
                            }
                        }
                        slot.Exp = (slot.Exp > ConfigLoader.MaxExpReward) ? ConfigLoader.MaxExpReward : slot.Exp;
                        slot.Gold = (slot.Gold > ConfigLoader.MaxGoldReward) ? ConfigLoader.MaxGoldReward : slot.Gold;
                        slot.Cash = (slot.Cash > ConfigLoader.MaxCashReward) ? ConfigLoader.MaxCashReward : slot.Cash;
                        if (this.RoomType == RoomCondition.Ace)
                        {
                            if ((account.SlotId < 0) || (account.SlotId > 1))
                            {
                                slot.Exp = 0;
                                slot.Gold = 0;
                                slot.Cash = 0;
                            }
                        }
                        else if ((slot.Exp < 0) || ((slot.Gold < 0) || (slot.Cash < 0)))
                        {
                            slot.Exp = 2;
                            slot.Gold = 2;
                            slot.Cash = 2;
                        }
                        int num7 = 0;
                        int num8 = 0;
                        int num9 = 0;
                        int num10 = 0;
                        int num11 = 0;
                        int num12 = 0;
                        int num13 = 0;
                        if ((runningEvent != null) || (model2 != null))
                        {
                            int[] bonuses = runningEvent.GetBonuses(account.Rank);
                            if ((runningEvent != null) && (bonuses != null))
                            {
                                num11 += ComDiv.Percentage(bonuses[0], bonuses[2]);
                                num12 += ComDiv.Percentage(bonuses[1], bonuses[2]);
                            }
                            if ((model2 != null) && (model2.BoostType == PortalBoostEvent.Mode))
                            {
                                num11 += model2.BonusExp;
                                num12 += model2.BonusGold;
                            }
                            if (!slot.BonusFlags.HasFlag(ResultIcon.Event))
                            {
                                slot.BonusFlags |= ResultIcon.Event;
                            }
                            slot.BonusEventExp += num11;
                            slot.BonusEventPoint += num12;
                        }
                        PlayerBonus bonus = account.Bonus;
                        if ((bonus != null) && (bonus.Bonuses > 0))
                        {
                            if ((bonus.Bonuses & 8) == 8)
                            {
                                num7 += 100;
                            }
                            if ((bonus.Bonuses & 0x80) == 0x80)
                            {
                                num8 += 100;
                            }
                            if ((bonus.Bonuses & 4) == 4)
                            {
                                num7 += 50;
                            }
                            if ((bonus.Bonuses & 0x40) == 0x40)
                            {
                                num8 += 50;
                            }
                            if ((bonus.Bonuses & 2) == 2)
                            {
                                num7 += 30;
                            }
                            if ((bonus.Bonuses & 0x20) == 0x20)
                            {
                                num8 += 30;
                            }
                            if ((bonus.Bonuses & 1) == 1)
                            {
                                num7 += 10;
                            }
                            if ((bonus.Bonuses & 0x10) == 0x10)
                            {
                                num8 += 10;
                            }
                            if ((bonus.Bonuses & 0x200) == 0x200)
                            {
                                num13 += 20;
                            }
                            if ((bonus.Bonuses & 0x400) == 0x400)
                            {
                                num13 += 30;
                            }
                            if ((bonus.Bonuses & 0x800) == 0x800)
                            {
                                num13 += 50;
                            }
                            if ((bonus.Bonuses & 0x1000) == 0x1000)
                            {
                                num13 += 100;
                            }
                            if (!slot.BonusFlags.HasFlag(ResultIcon.Item))
                            {
                                slot.BonusFlags |= ResultIcon.Item;
                            }
                            slot.BonusItemExp += num7;
                            slot.BonusItemPoint += num8;
                            slot.BonusBattlePass += num13;
                        }
                        PCCafeModel pCCafe = TemplatePackXML.GetPCCafe(account.CafePC);
                        if (pCCafe != null)
                        {
                            PlayerVip playerVIP = DaoManagerSQL.GetPlayerVIP(account.PlayerId);
                            if ((playerVIP != null) && InternetCafeXML.IsValidAddress(DaoManagerSQL.GetPlayerIP4Address(account.PlayerId), playerVIP.Address))
                            {
                                InternetCafe cafe = InternetCafeXML.GetICafe(ConfigLoader.InternetCafeId);
                                if ((cafe != null) && ((account.CafePC == CafeEnum.Gold) || (account.CafePC == CafeEnum.Silver)))
                                {
                                    num9 += (account.CafePC == CafeEnum.Gold) ? cafe.PremiumExp : ((account.CafePC == CafeEnum.Silver) ? cafe.BasicExp : 0);
                                    num10 += (account.CafePC == CafeEnum.Gold) ? cafe.PremiumGold : ((account.CafePC == CafeEnum.Silver) ? cafe.BasicGold : 0);
                                }
                            }
                            num9 += pCCafe.ExpUp;
                            num10 += pCCafe.PointUp;
                            if ((account.CafePC == CafeEnum.Silver) && !slot.BonusFlags.HasFlag(ResultIcon.Pc))
                            {
                                slot.BonusFlags |= ResultIcon.Pc;
                            }
                            else if ((account.CafePC == CafeEnum.Gold) && !slot.BonusFlags.HasFlag(ResultIcon.PcPlus))
                            {
                                slot.BonusFlags |= ResultIcon.PcPlus;
                            }
                            slot.BonusCafeExp += num9;
                            slot.BonusCafePoint += num10;
                        }
                        if (bool_0)
                        {
                            if (slot.BonusItemExp > 300)
                            {
                                slot.BonusItemExp = 300;
                            }
                            if (slot.BonusItemPoint > 300)
                            {
                                slot.BonusItemPoint = 300;
                            }
                            if (slot.BonusCafeExp > 300)
                            {
                                slot.BonusCafeExp = 300;
                            }
                            if (slot.BonusCafePoint > 300)
                            {
                                slot.BonusCafePoint = 300;
                            }
                            if (slot.BonusEventExp > 300)
                            {
                                slot.BonusEventExp = 300;
                            }
                            if (slot.BonusEventPoint > 300)
                            {
                                slot.BonusEventPoint = 300;
                            }
                        }
                        int percent = (slot.BonusEventExp + slot.BonusCafeExp) + slot.BonusItemExp;
                        int num15 = (slot.BonusEventPoint + slot.BonusCafePoint) + slot.BonusItemPoint;
                        account.Exp += slot.Exp + ComDiv.Percentage(slot.Exp, percent);
                        account.Gold += slot.Gold + ComDiv.Percentage(slot.Gold, num15);
                        if (daily != null)
                        {
                            daily.ExpGained += slot.Exp + ComDiv.Percentage(slot.Exp, percent);
                            daily.PointGained += slot.Gold + ComDiv.Percentage(slot.Gold, num15);
                            daily.Playtime += (uint) num3;
                            query4.AddQuery("exp_gained", daily.ExpGained);
                            query4.AddQuery("point_gained", daily.PointGained);
                            query4.AddQuery("playtime", (long) daily.Playtime);
                        }
                        if (ConfigLoader.WinCashPerBattle)
                        {
                            account.Cash += slot.Cash;
                        }
                        RankModel rank = PlayerRankXML.GetRank(account.Rank);
                        if ((rank != null) && ((account.Exp >= (rank.OnNextLevel + rank.OnAllExp)) && (account.Rank <= 50)))
                        {
                            List<int> rewards = PlayerRankXML.GetRewards(account.Rank);
                            if (rewards.Count > 0)
                            {
                                using (List<int>.Enumerator enumerator = rewards.GetEnumerator())
                                {
                                    while (enumerator.MoveNext())
                                    {
                                        GoodsItem good = ShopManager.GetGood(enumerator.Current);
                                        if (good != null)
                                        {
                                            if ((ComDiv.GetIdStatics(good.Item.Id, 1) == 6) && (account.Character.GetCharacter(good.Item.Id) == null))
                                            {
                                                AllUtils.CreateCharacter(account, good.Item);
                                                continue;
                                            }
                                            account.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, account, good.Item));
                                        }
                                    }
                                }
                            }
                            account.Gold += rank.OnGoldUp;
                            account.LastRankUpDate = uint.Parse(date.ToString("yyMMddHHmm"));
                            int num19 = account.Rank + 1;
                            account.Rank = num19;
                            account.SendPacket(new PROTOCOL_BASE_RANK_UP_ACK(num19, rank.OnNextLevel));
                            query.AddQuery("last_rank_update", (long) account.LastRankUpDate);
                            query.AddQuery("rank", account.Rank);
                        }
                        if (evPlaytime != null)
                        {
                            AllUtils.PlayTimeEvent(account, evPlaytime, bool_0, slot, (long) num3);
                        }
                        if (activeSeasonPass != null)
                        {
                            account.UpdateSeasonpass = true;
                            AllUtils.CalculateBattlePass(account, slot, activeSeasonPass);
                        }
                        if (this.Competitive)
                        {
                            AllUtils.CalculateCompetitive(this, account, slot, teamEnum_0 == slot.Team);
                        }
                        AllUtils.DiscountPlayerItems(slot, account);
                        if (exp != account.Exp)
                        {
                            query.AddQuery("experience", account.Exp);
                        }
                        if (gold != account.Gold)
                        {
                            query.AddQuery("gold", account.Gold);
                        }
                        if (cash != account.Cash)
                        {
                            query.AddQuery("cash", account.Cash);
                        }
                        ComDiv.UpdateDB("accounts", "player_id", account.PlayerId, query.GetTables(), query.GetValues());
                        ComDiv.UpdateDB("player_stat_basics", "owner_id", account.PlayerId, query2.GetTables(), query2.GetValues());
                        ComDiv.UpdateDB("player_stat_seasons", "owner_id", account.PlayerId, query3.GetTables(), query3.GetValues());
                        ComDiv.UpdateDB("player_stat_dailies", "owner_id", account.PlayerId, query4.GetTables(), query4.GetValues());
                        ComDiv.UpdateDB("player_stat_weapons", "owner_id", account.PlayerId, query5.GetTables(), query5.GetValues());
                        if (ConfigLoader.WinCashPerBattle && ConfigLoader.ShowCashReceiveWarn)
                        {
                            object[] argumens = new object[] { slot.Cash };
                            account.SendPacket(new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(Translation.GetLabel("CashReceived", argumens)));
                        }
                        slots.Add(slot);
                    }
                }
                if (slots.Count > 0)
                {
                    this.SlotRewards = AllUtils.GetRewardData(this, slots);
                    this.method_8(slots, bool_0);
                }
                this.UpdateSlotsInfo();
                if (this.RoomType != RoomCondition.FreeForAll)
                {
                    this.method_14(teamEnum_0);
                }
            }
        }

        private void method_8(List<SlotModel> list_0, bool bool_0)
        {
            Account account;
            Func<SlotModel, int> keySelector = Class11.<>9__119_0;
            if (Class11.<>9__119_0 == null)
            {
                Func<SlotModel, int> local1 = Class11.<>9__119_0;
                keySelector = Class11.<>9__119_0 = new Func<SlotModel, int>(Class11.<>9.method_0);
            }
            SlotModel slot = list_0.OrderByDescending<SlotModel, int>(keySelector).FirstOrDefault<SlotModel>();
            if ((slot != null) && (slot.Check && ((slot.State == SlotState.BATTLE) && this.GetPlayerBySlot(slot, out account))))
            {
                StatisticTotal basic = account.Statistic.Basic;
                StatisticSeason season = account.Statistic.Season;
                if (!bool_0 && ((basic != null) && (season != null)))
                {
                    basic.MvpCount++;
                    season.MvpCount++;
                    ComDiv.UpdateDB("player_stat_basics", "mvp_count", basic.MvpCount, "owner_id", account.PlayerId);
                    ComDiv.UpdateDB("player_stat_seasons", "mvp_count", season.MvpCount, "owner_id", account.PlayerId);
                }
            }
        }

        private void method_9(Account account_0, StatisticTotal statisticTotal_0, StatisticSeason statisticSeason_0, SlotModel slotModel_0, DBQuery dbquery_0, DBQuery dbquery_1, int int_0, bool bool_0, int int_1)
        {
            statisticTotal_0.HeadshotsCount += slotModel_0.AllHeadshots;
            statisticTotal_0.KillsCount += slotModel_0.AllKills;
            statisticTotal_0.TotalKillsCount += slotModel_0.AllKills;
            statisticTotal_0.DeathsCount += slotModel_0.AllDeaths;
            statisticTotal_0.AssistsCount += slotModel_0.AllAssists;
            statisticSeason_0.HeadshotsCount += slotModel_0.AllHeadshots;
            statisticSeason_0.KillsCount += slotModel_0.AllKills;
            statisticSeason_0.TotalKillsCount += slotModel_0.AllKills;
            statisticSeason_0.DeathsCount += slotModel_0.AllDeaths;
            statisticSeason_0.AssistsCount += slotModel_0.AllAssists;
            this.method_12(slotModel_0, account_0.Statistic, dbquery_0, dbquery_1);
            if (this.RoomType == RoomCondition.FreeForAll)
            {
                AllUtils.UpdateMatchCountFFA(this, account_0, int_0, dbquery_0, dbquery_1);
            }
            else
            {
                AllUtils.UpdateMatchCount(bool_0, account_0, int_1, dbquery_0, dbquery_1);
            }
        }

        public string RandomName(int Random)
        {
            switch (Random)
            {
                case 1:
                    return "Feel the Headshots!!";

                case 2:
                    return "Land of Dead!";

                case 3:
                    return "Kill! or be Killed!!";

                case 4:
                    return "Show Me Your Skills!!";
            }
            return "Point Blank!!";
        }

        public void RemovePlayer(Account Player, bool WarnAllPlayers, int QuitMotive = 0)
        {
            SlotModel model;
            if ((Player != null) && this.GetSlot(Player.SlotId, out model))
            {
                this.method_16(Player, model, WarnAllPlayers, QuitMotive);
            }
        }

        public void RemovePlayer(Account Player, SlotModel Slot, bool WarnAllPlayers, int QuitMotive = 0)
        {
            if ((Player != null) && (Slot != null))
            {
                this.method_16(Player, Slot, WarnAllPlayers, QuitMotive);
            }
        }

        public int ResetReadyPlayers()
        {
            int num = 0;
            foreach (SlotModel model in this.Slots)
            {
                if (model.State == SlotState.READY)
                {
                    model.State = SlotState.NORMAL;
                    num++;
                }
            }
            return num;
        }

        public void RoundRestart()
        {
            this.StopBomb();
            foreach (SlotModel model in this.Slots)
            {
                if ((model.PlayerId > 0L) && (model.State == SlotState.BATTLE))
                {
                    if (!model.DeathState.HasFlag(DeadEnum.UseChat))
                    {
                        model.DeathState |= DeadEnum.UseChat;
                    }
                    if (model.Spectator)
                    {
                        model.Spectator = false;
                    }
                    if ((model.KillsOnLife >= 3) && ((this.RoomType == RoomCondition.Annihilation) || (this.RoomType == RoomCondition.Ace)))
                    {
                        model.Objects++;
                    }
                    model.KillsOnLife = 0;
                    model.LastKillState = 0;
                    model.RepeatLastState = false;
                    model.DamageBar1 = 0;
                    model.DamageBar2 = 0;
                }
            }
            this.RoundTime.StartJob(0x203a, new TimerCallback(this.method_21));
        }

        public void SendPacketToPlayers(GameServerPacket Packet)
        {
            List<Account> allPlayers = this.GetAllPlayers();
            if (allPlayers.Count != 0)
            {
                byte[] completeBytes = Packet.GetCompleteBytes("Room.SendPacketToPlayers(SendPacket)");
                using (List<Account>.Enumerator enumerator = allPlayers.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        enumerator.Current.SendCompletePacket(completeBytes, Packet.GetType().Name);
                    }
                }
            }
        }

        public void SendPacketToPlayers(GameServerPacket Packet, long PlayerId)
        {
            List<Account> allPlayers = this.GetAllPlayers(PlayerId);
            if (allPlayers.Count != 0)
            {
                byte[] completeBytes = Packet.GetCompleteBytes("Room.SendPacketToPlayers(SendPacket,long)");
                using (List<Account>.Enumerator enumerator = allPlayers.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        enumerator.Current.SendCompletePacket(completeBytes, Packet.GetType().Name);
                    }
                }
            }
        }

        public void SendPacketToPlayers(GameServerPacket Packet, SlotState State, int Type)
        {
            List<Account> allPlayers = this.GetAllPlayers(State, Type);
            if (allPlayers.Count != 0)
            {
                byte[] completeBytes = Packet.GetCompleteBytes("Room.SendPacketToPlayers(SendPacket,SLOT_STATE,int)");
                using (List<Account>.Enumerator enumerator = allPlayers.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        enumerator.Current.SendCompletePacket(completeBytes, Packet.GetType().Name);
                    }
                }
            }
        }

        public void SendPacketToPlayers(GameServerPacket Packet, SlotState State, int Type, int Exception)
        {
            List<Account> list = this.GetAllPlayers(State, Type, Exception);
            if (list.Count != 0)
            {
                byte[] completeBytes = Packet.GetCompleteBytes("Room.SendPacketToPlayers(SendPacket,SLOT_STATE,int,int)");
                using (List<Account>.Enumerator enumerator = list.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        enumerator.Current.SendCompletePacket(completeBytes, Packet.GetType().Name);
                    }
                }
            }
        }

        public void SendPacketToPlayers(GameServerPacket Packet, GameServerPacket Packet2, SlotState State, int Type)
        {
            List<Account> allPlayers = this.GetAllPlayers(State, Type);
            if (allPlayers.Count != 0)
            {
                byte[] completeBytes = Packet.GetCompleteBytes("Room.SendPacketToPlayers(SendPacket,SendPacket,SLOT_STATE,int)-1");
                byte[] data = Packet2.GetCompleteBytes("Room.SendPacketToPlayers(SendPacket,SendPacket,SLOT_STATE,int)-2");
                foreach (Account local1 in allPlayers)
                {
                    local1.SendCompletePacket(completeBytes, Packet.GetType().Name);
                    local1.SendCompletePacket(data, Packet2.GetType().Name);
                }
            }
        }

        public void SendPacketToPlayers(GameServerPacket Packet, SlotState State, int Type, int Exception, int Exception2)
        {
            List<Account> list = this.GetAllPlayers(State, Type, Exception, Exception2);
            if (list.Count != 0)
            {
                byte[] completeBytes = Packet.GetCompleteBytes("Room.SendPacketToPlayers(SendPacket,SLOT_STATE,int,int,int)");
                using (List<Account>.Enumerator enumerator = list.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        enumerator.Current.SendCompletePacket(completeBytes, Packet.GetType().Name);
                    }
                }
            }
        }

        public void SetBotLevel()
        {
            if (this.IsBotMode())
            {
                this.IngameAiLevel = this.AiLevel;
                for (int i = 0; i < 0x12; i++)
                {
                    this.Slots[i].AiLevel = this.IngameAiLevel;
                }
            }
        }

        public void SetNewLeader(int LeaderSlot, SlotState State, int OldLeader, bool UpdateInfo)
        {
            SlotModel[] slots = this.Slots;
            lock (slots)
            {
                if (LeaderSlot != -1)
                {
                    this.LeaderSlot = LeaderSlot;
                }
                else
                {
                    foreach (SlotModel model in this.Slots)
                    {
                        if ((model.Id != OldLeader) && ((model.PlayerId > 0L) && (model.State > State)))
                        {
                            this.LeaderSlot = model.Id;
                            break;
                        }
                    }
                }
                if (this.LeaderSlot != -1)
                {
                    SlotModel model2 = this.Slots[this.LeaderSlot];
                    if (model2.State == SlotState.READY)
                    {
                        model2.State = SlotState.NORMAL;
                    }
                    if (UpdateInfo)
                    {
                        this.UpdateSlotsInfo();
                    }
                }
            }
        }

        public void SetSlotCount(int Count, bool IsCreateRoom, bool IsUpdateRoom)
        {
            MapMatch mapLimit = SystemMapXML.GetMapLimit((int) this.MapId, (int) this.Rule);
            if (mapLimit != null)
            {
                if (Count > mapLimit.Limit)
                {
                    Count = mapLimit.Limit;
                }
                if (this.RoomType == RoomCondition.Tutorial)
                {
                    Count = 1;
                }
                if (this.IsBotMode())
                {
                    Count = 8;
                }
                if (Count <= 0)
                {
                    Count = 1;
                }
                if (IsCreateRoom)
                {
                    SlotModel[] slots = this.Slots;
                    lock (slots)
                    {
                        Func<SlotModel, bool> predicate = Class11.<>9__130_0;
                        if (Class11.<>9__130_0 == null)
                        {
                            Func<SlotModel, bool> local1 = Class11.<>9__130_0;
                            predicate = Class11.<>9__130_0 = new Func<SlotModel, bool>(Class11.<>9.method_1);
                        }
                        foreach (SlotModel model in this.Slots.Where<SlotModel>(predicate))
                        {
                            if (model.Id >= Count)
                            {
                                model.State = SlotState.CLOSE;
                            }
                        }
                    }
                }
                if (IsUpdateRoom)
                {
                    this.UpdateSlotsInfo();
                }
            }
        }

        public void SpawnReadyPlayers()
        {
            SlotModel[] slots = this.Slots;
            lock (slots)
            {
                bool flag2 = this.ThisModeHaveRounds() && (((this.CountdownIG == 3) || ((this.CountdownIG == 5) || (this.CountdownIG == 7))) || (this.CountdownIG == 9));
                if ((((this.State == RoomState.PRE_BATTLE) && !this.PreMatchCD) & flag2) && !this.IsBotMode())
                {
                    this.PreMatchCD = true;
                    using (PROTOCOL_BATTLE_COUNT_DOWN_ACK protocol_battle_count_down_ack = new PROTOCOL_BATTLE_COUNT_DOWN_ACK(this.CountdownIG))
                    {
                        this.SendPacketToPlayers(protocol_battle_count_down_ack);
                    }
                }
                int period = (this.CountdownIG == 0) ? 0 : ((this.CountdownIG * 0x3e8) + 250);
                this.PreMatchTime.StartJob(period, new TimerCallback(this.method_24));
            }
        }

        public void StartBattle(bool UpdateInfo)
        {
            SlotModel[] slots = this.Slots;
            lock (slots)
            {
                this.State = RoomState.LOADING;
                this.RequestRoomMaster.Clear();
                this.SetBotLevel();
                AllUtils.CheckClanMatchRestrict(this);
                this.StartTick = DateTimeUtil.Now().Ticks;
                this.StartDate = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
                using (PROTOCOL_BATTLE_START_GAME_ACK protocol_battle_start_game_ack = new PROTOCOL_BATTLE_START_GAME_ACK(this))
                {
                    byte[] completeBytes = protocol_battle_start_game_ack.GetCompleteBytes("Room.StartBattle");
                    foreach (Account account in this.GetAllPlayers(SlotState.READY, 0))
                    {
                        SlotModel slot = this.GetSlot(account.SlotId);
                        if (slot != null)
                        {
                            slot.WithHost = true;
                            slot.State = SlotState.LOAD;
                            slot.SetMissionsClone(account.Mission);
                            account.SendCompletePacket(completeBytes, protocol_battle_start_game_ack.GetType().Name);
                        }
                    }
                }
                if (UpdateInfo)
                {
                    this.UpdateSlotsInfo();
                }
                this.UpdateRoomInfo();
            }
        }

        public void StartBomb()
        {
            this.BombTime.StartJob(0xa410, new TimerCallback(this.method_19));
        }

        public void StartCountDown()
        {
            using (PROTOCOL_BATTLE_START_COUNTDOWN_ACK protocol_battle_start_countdown_ack = new PROTOCOL_BATTLE_START_COUNTDOWN_ACK(CountDownEnum.Start))
            {
                this.SendPacketToPlayers(protocol_battle_start_countdown_ack);
            }
            this.CountdownTime.StartJob(0x1482, new TimerCallback(this.method_23));
        }

        public void StartCounter(int Type, Account Player, SlotModel Slot)
        {
            Class12 class2 = new Class12 {
                slotModel_0 = Slot,
                roomModel_0 = this,
                account_0 = Player
            };
            int period = 0;
            class2.eventErrorEnum_0 = EventErrorEnum.SUCCESS;
            if (Type == 0)
            {
                class2.eventErrorEnum_0 = (EventErrorEnum) (-2147479542);
                period = 0x15f90;
            }
            else if (Type == 1)
            {
                class2.eventErrorEnum_0 = (EventErrorEnum) (-2147479541);
                period = 0x7530;
            }
            if (period > 0)
            {
                class2.slotModel_0.FirstInactivityOff = true;
            }
            class2.slotModel_0.Timing.StartJob(period, new TimerCallback(class2.method_0));
        }

        public void StartVote()
        {
            if (this.VoteKick != null)
            {
                this.VoteTime.StartJob(0x4e20, new TimerCallback(this.method_20));
            }
        }

        public void StopBomb()
        {
            if (this.ActiveC4)
            {
                this.ActiveC4 = false;
                if (this.BombTime != null)
                {
                    this.BombTime.StopJob();
                }
            }
        }

        public void StopCountDown(int SlotId)
        {
            if (this.State == RoomState.COUNTDOWN)
            {
                if (SlotId == this.LeaderSlot)
                {
                    this.StopCountDown(CountDownEnum.StopByHost, true);
                }
                else if (this.GetPlayingPlayers((TeamEnum) ((this.LeaderSlot % 2) == 0), SlotState.READY, 0) == 0)
                {
                    this.ChangeSlotState(this.LeaderSlot, SlotState.NORMAL, false);
                    this.StopCountDown(CountDownEnum.StopByPlayer, true);
                }
            }
        }

        public void StopCountDown(CountDownEnum Type, bool RefreshRoom = true)
        {
            this.State = RoomState.READY;
            if (RefreshRoom)
            {
                this.UpdateRoomInfo();
            }
            this.CountdownTime.StopJob();
            using (PROTOCOL_BATTLE_START_COUNTDOWN_ACK protocol_battle_start_countdown_ack = new PROTOCOL_BATTLE_START_COUNTDOWN_ACK(Type))
            {
                this.SendPacketToPlayers(protocol_battle_start_countdown_ack);
            }
        }

        public void SwitchSlots(List<SlotChange> SlotChanges, int NewSlotId, int OldSlotId, bool ChangeReady)
        {
            SlotModel model = this.Slots[NewSlotId];
            SlotModel model2 = this.Slots[OldSlotId];
            if (ChangeReady)
            {
                if (model.State == SlotState.READY)
                {
                    model.State = SlotState.NORMAL;
                }
                if (model2.State == SlotState.READY)
                {
                    model2.State = SlotState.NORMAL;
                }
            }
            model.SetSlotId(OldSlotId);
            model2.SetSlotId(NewSlotId);
            this.Slots[NewSlotId] = model2;
            this.Slots[OldSlotId] = model;
            SlotChanges.Add(new SlotChange(model, model2));
        }

        public void SwitchSlots(List<SlotChange> SlotChanges, Account Player, SlotModel OldSlot, SlotModel NewSlot, SlotState State = 9)
        {
            if ((Player != null) && ((OldSlot != null) && (NewSlot != null)))
            {
                NewSlot.ResetSlot();
                NewSlot.State = SlotState.NORMAL;
                NewSlot.PlayerId = Player.PlayerId;
                NewSlot.Equipment = Player.Equipment;
                if ((NewSlot.Id == 0x10) || (NewSlot.Id == 0x11))
                {
                    NewSlot.SpecGM = true;
                    NewSlot.ViewType = ViewerType.SpecGM;
                }
                OldSlot.ResetSlot();
                OldSlot.State = SlotState.EMPTY;
                OldSlot.PlayerId = 0L;
                OldSlot.Equipment = null;
                OldSlot.SpecGM = false;
                OldSlot.ViewType = ViewerType.Normal;
                if (Player.SlotId == this.LeaderSlot)
                {
                    this.LeaderName = Player.Nickname;
                    this.LeaderSlot = NewSlot.Id;
                }
                Player.SlotId = NewSlot.Id;
                SlotChanges.Add(new SlotChange(OldSlot, NewSlot));
            }
        }

        public bool ThisModeHaveCD() => 
            (this.RoomType == RoomCondition.Bomb) || ((this.RoomType == RoomCondition.Annihilation) || ((this.RoomType == RoomCondition.Boss) || ((this.RoomType == RoomCondition.CrossCounter) || ((this.RoomType == RoomCondition.Destroy) || ((this.RoomType == RoomCondition.Ace) || ((this.RoomType == RoomCondition.Escape) || (this.RoomType == RoomCondition.Glass)))))));

        public bool ThisModeHaveRounds() => 
            (this.RoomType == RoomCondition.Bomb) || ((this.RoomType == RoomCondition.Destroy) || ((this.RoomType == RoomCondition.Annihilation) || ((this.RoomType == RoomCondition.Defense) || ((this.RoomType == RoomCondition.Destroy) || ((this.RoomType == RoomCondition.Ace) || ((this.RoomType == RoomCondition.Escape) || (this.RoomType == RoomCondition.Glass)))))));

        public void UpdateRoomInfo()
        {
            this.GenerateSeed();
            using (PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK protocol_room_change_roominfo_ack = new PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK(this))
            {
                this.SendPacketToPlayers(protocol_room_change_roominfo_ack);
            }
        }

        public void UpdateSlotsInfo()
        {
            using (PROTOCOL_ROOM_GET_SLOTINFO_ACK protocol_room_get_slotinfo_ack = new PROTOCOL_ROOM_GET_SLOTINFO_ACK(this))
            {
                this.SendPacketToPlayers(protocol_room_get_slotinfo_ack);
            }
        }

        public TeamEnum ValidateTeam(TeamEnum Team, TeamEnum Costume) => 
            (this.RoomType != RoomCondition.FreeForAll) ? (!this.SwapRound ? Team : ((Team == TeamEnum.FR_TEAM) ? TeamEnum.CT_TEAM : TeamEnum.FR_TEAM)) : Costume;

        [Serializable, CompilerGenerated]
        private sealed class Class11
        {
            public static readonly RoomModel.Class11 <>9 = new RoomModel.Class11();
            public static Func<SlotModel, int> <>9__119_0;
            public static Func<SlotModel, bool> <>9__130_0;
            public static Func<SlotModel, bool> <>9__131_0;

            internal int method_0(SlotModel slotModel_0) => 
                slotModel_0.Score;

            internal bool method_1(SlotModel slotModel_0) => 
                (slotModel_0.Id != 0x10) && (slotModel_0.Id != 0x11);

            internal bool method_2(SlotModel slotModel_0) => 
                (slotModel_0.Id != 0x10) && (slotModel_0.Id != 0x11);
        }

        [CompilerGenerated]
        private sealed class Class12
        {
            public SlotModel slotModel_0;
            public RoomModel roomModel_0;
            public EventErrorEnum eventErrorEnum_0;
            public Account account_0;

            internal void method_0(object object_0)
            {
                if (!this.slotModel_0.FirstInactivityOff && ((this.slotModel_0.State < SlotState.BATTLE) && (this.slotModel_0.IsPlaying == 0)))
                {
                    this.roomModel_0.method_3(this.eventErrorEnum_0, this.account_0, this.slotModel_0);
                }
                object obj2 = object_0;
                lock (obj2)
                {
                    if (this.slotModel_0 != null)
                    {
                        this.slotModel_0.StopTiming();
                    }
                }
            }
        }

        [CompilerGenerated]
        private sealed class Class13
        {
            public int int_0;

            internal bool method_0(int int_1) => 
                int_1 == this.int_0;

            internal bool method_1(int int_1) => 
                int_1 == this.int_0;
        }
    }
}

