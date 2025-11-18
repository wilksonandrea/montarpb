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
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Server.Game.Data.Models
{
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

		public readonly int[] TIMES = new int[] { 3, 3, 3, 5, 7, 5, 10, 15, 20, 25, 30 };

		public readonly int[] KILLS = new int[] { 15, 30, 50, 60, 80, 100, 120, 140, 160 };

		public readonly int[] ROUNDS = new int[] { 1, 2, 3, 5, 7, 9 };

		public readonly int[] FR_TEAM = new int[] { 0, 2, 4, 6, 8, 10, 12, 14, 16 };

		public readonly int[] CT_TEAM = new int[] { 1, 3, 5, 7, 9, 11, 13, 15, 17 };

		public readonly int[] ALL_TEAM = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };

		public readonly int[] INVERT_FR_TEAM = new int[] { 16, 14, 12, 10, 8, 6, 4, 2, 0 };

		public readonly int[] INVERT_CT_TEAM = new int[] { 17, 15, 13, 11, 9, 7, 5, 3, 1 };

		public byte[] RandomMaps;

		public byte[] LeaderAddr = new byte[4];

		public byte[] HitParts = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34 };

		public ValueTuple<byte[], int[]> SlotRewards;

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
			for (int i = 0; i < (int)this.Slots.Length; i++)
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
			DateTime dateTime = DateTimeUtil.Now();
			this.LastChangeTeam = dateTime;
			this.LastPingSync = dateTime;
		}

		public int AddPlayer(Account Player)
		{
			int ıd;
			lock (this.Slots)
			{
				SlotModel[] slots = this.Slots;
				int ınt32 = 0;
				while (ınt32 < (int)slots.Length)
				{
					SlotModel playerId = slots[ınt32];
					if (playerId.PlayerId != 0 || playerId.State != SlotState.EMPTY)
					{
						ınt32++;
					}
					else if ((playerId.Id == 16 || playerId.Id == 17) && !Player.IsGM())
					{
						ıd = -1;
						return ıd;
					}
					else
					{
						playerId.PlayerId = Player.PlayerId;
						playerId.State = SlotState.NORMAL;
						Player.Room = this;
						Player.SlotId = playerId.Id;
						playerId.Equipment = Player.Equipment;
						Player.Status.UpdateRoom((byte)this.RoomId);
						AllUtils.SyncPlayerToClanMembers(Player);
						AllUtils.SyncPlayerToFriends(Player, false);
						Player.UpdateCacheInfo();
						ıd = playerId.Id;
						return ıd;
					}
				}
				return -1;
			}
			return ıd;
		}

		public int AddPlayer(Account Player, TeamEnum TeamIdx)
		{
			int ıd;
			lock (this.Slots)
			{
				int[] teamArray = this.GetTeamArray(TeamIdx);
				int ınt32 = 0;
				while (ınt32 < (int)teamArray.Length)
				{
					int ınt321 = teamArray[ınt32];
					SlotModel slots = this.Slots[ınt321];
					if (slots.PlayerId != 0 || slots.State != SlotState.EMPTY)
					{
						ınt32++;
					}
					else if ((slots.Id == 16 || slots.Id == 17) && !Player.IsGM())
					{
						ıd = -1;
						return ıd;
					}
					else
					{
						slots.PlayerId = Player.PlayerId;
						slots.State = SlotState.NORMAL;
						Player.Room = this;
						Player.SlotId = slots.Id;
						slots.Equipment = Player.Equipment;
						Player.Status.UpdateRoom((byte)this.RoomId);
						AllUtils.SyncPlayerToClanMembers(Player);
						AllUtils.SyncPlayerToFriends(Player, false);
						Player.UpdateCacheInfo();
						ıd = slots.Id;
						return ıd;
					}
				}
				return -1;
			}
			return ıd;
		}

		public void CalculateResult()
		{
			lock (this.Slots)
			{
				this.method_7(AllUtils.GetWinnerTeam(this), this.IsBotMode());
			}
		}

		public void CalculateResult(TeamEnum resultType)
		{
			lock (this.Slots)
			{
				this.method_7(resultType, this.IsBotMode());
			}
		}

		public void CalculateResult(TeamEnum resultType, bool isBotMode)
		{
			lock (this.Slots)
			{
				this.method_7(resultType, isBotMode);
			}
		}

		public void CalculateResultFreeForAll(int SlotWin)
		{
			lock (this.Slots)
			{
				this.method_7((TeamEnum)SlotWin, false);
			}
		}

		public void ChangeRounds()
		{
			this.Rounds++;
			if (this.method_2() && !this.SwapRound)
			{
				this.SwapRound = true;
			}
		}

		public void ChangeSlotState(int SlotId, SlotState State, bool SendInfo)
		{
			this.ChangeSlotState(this.GetSlot(SlotId), State, SendInfo);
		}

		public void ChangeSlotState(SlotModel Slot, SlotState State, bool SendInfo)
		{
			if (Slot != null)
			{
				if (Slot.State != State)
				{
					Slot.State = State;
					if (State == SlotState.EMPTY || State == SlotState.CLOSE)
					{
						AllUtils.ResetSlotInfo(this, Slot, false);
						Slot.PlayerId = 0L;
					}
					if (SendInfo)
					{
						this.UpdateSlotsInfo();
					}
					return;
				}
			}
		}

		public void CheckGhostSlot(SlotModel Slot)
		{
			if (Slot.State != SlotState.EMPTY && Slot.State != SlotState.CLOSE && Slot.PlayerId == 0 && !this.IsBotMode())
			{
				Slot.ResetSlot();
				Slot.State = SlotState.EMPTY;
			}
		}

		public TeamEnum CheckTeam(int SlotIdx)
		{
			if (Array.Exists<int>(this.FR_TEAM, (int int_1) => int_1 == SlotIdx))
			{
				return TeamEnum.FR_TEAM;
			}
			if (Array.Exists<int>(this.CT_TEAM, (int int_1) => int_1 == SlotIdx))
			{
				return TeamEnum.CT_TEAM;
			}
			return TeamEnum.ALL_TEAM;
		}

		public void CheckToEndWaitingBattle(bool host)
		{
			if ((this.State == RoomState.COUNTDOWN || this.State == RoomState.LOADING || this.State == RoomState.RENDEZVOUS) && (host || this.Slots[this.LeaderSlot].State == SlotState.BATTLE_READY))
			{
				AllUtils.EndBattleNoPoints(this);
			}
		}

		public void GenerateSeed()
		{
			this.Seed = (uint)((int)(this.MapId & (MapIdEnum.PortAkaba | MapIdEnum.Redrock | MapIdEnum.Library | MapIdEnum.MStation | MapIdEnum.MidnightZone | MapIdEnum.Uptown | MapIdEnum.BurningHall | MapIdEnum.DSquad | MapIdEnum.Crackdown | MapIdEnum.SaintMansion | MapIdEnum.EasternRoad | MapIdEnum.Downtown | MapIdEnum.LuxVille | MapIdEnum.Blowcity | MapIdEnum.Stormtube | MapIdEnum.Giran2 | MapIdEnum.BreakDown | MapIdEnum.TrainingCamp | MapIdEnum.Sentrybase | MapIdEnum.DesertCamp | MapIdEnum.Kickpoint | MapIdEnum.FaceRock | MapIdEnum.SupplyBase | MapIdEnum.SandStorm | MapIdEnum.ShoppingCenter | MapIdEnum.Safari | MapIdEnum.DragonAlley | MapIdEnum.MachuPichu | MapIdEnum.Twotowers | MapIdEnum.AngkorRuins | MapIdEnum.GhostTown | MapIdEnum.GrandBazaar | MapIdEnum.Under23 | MapIdEnum.TaipeiCityMall | MapIdEnum.Safari2 | MapIdEnum.Metro | MapIdEnum.RushHour | MapIdEnum.CargoPort | MapIdEnum.BlackMamba | MapIdEnum.Holiday | MapIdEnum.WestStation | MapIdEnum.HouseMuseum | MapIdEnum.Outpost | MapIdEnum.Hospital | MapIdEnum.Midtown | MapIdEnum.Cargoship | MapIdEnum.Airport | MapIdEnum.SafeHouse | MapIdEnum.Hardrock | MapIdEnum.Giran | MapIdEnum.Helispot | MapIdEnum.BlackPanther | MapIdEnum.BreedingNest | MapIdEnum.D_Uptown | MapIdEnum.D_BreakDown | MapIdEnum.DinoLab | MapIdEnum.Tutorial | MapIdEnum.Unknown1 | MapIdEnum.Unknown2 | MapIdEnum.Unknown3 | MapIdEnum.Unknown4 | MapIdEnum.Unknown5 | MapIdEnum.Unknown6 | MapIdEnum.Unknown7 | MapIdEnum.WaterComplex | MapIdEnum.HotelCamouflage | MapIdEnum.PumpkinHollow | MapIdEnum.Unknown8 | MapIdEnum.BattleShip | MapIdEnum.RampartTown | MapIdEnum.Ballistic | MapIdEnum.Unknown9 | MapIdEnum.Holiday2 | MapIdEnum.RothenVillage | MapIdEnum.MerryWhiteVillage | MapIdEnum.FalluCity | MapIdEnum.Hindrance | MapIdEnum.Sewerage | MapIdEnum.SlumArea | MapIdEnum.New_Library | MapIdEnum.C_Sandstorm | MapIdEnum.DinoRuins | MapIdEnum.FatalZone | MapIdEnum.MarineWave | MapIdEnum.StillRaid | MapIdEnum.OldDock | MapIdEnum.BioLab | MapIdEnum.BrokenAlley | MapIdEnum.BankHall | MapIdEnum.Provence | MapIdEnum.M_Bridge | MapIdEnum.Mihawk | MapIdEnum.DesertCanyon | MapIdEnum.AmperaBridge | MapIdEnum.S_Twotowers | MapIdEnum.ThaiStadium | MapIdEnum.FortSantiago | MapIdEnum.Roadside | MapIdEnum.FFA_Uptown | MapIdEnum.InfectionAirport | MapIdEnum.Unknown10 | MapIdEnum.KhodsanRoad | MapIdEnum.SpacePort | MapIdEnum.JunkYard | MapIdEnum.NewCrackdown | MapIdEnum.OctagonalPavilion | MapIdEnum.DrainTunnel | MapIdEnum.Cemetery | MapIdEnum.MX_EasternRoad | MapIdEnum.NewMidtown | MapIdEnum.Mansion | MapIdEnum.XinyiPlaza | MapIdEnum.Wilderness | MapIdEnum.MX_Uptown | MapIdEnum.MX_Downtown | MapIdEnum.Gustav | MapIdEnum.RuinedVillage | MapIdEnum.WaterPark | MapIdEnum.BR_HistoricSite | MapIdEnum.Relic | MapIdEnum.LostValley)) << (int)MapIdEnum.DesertCamp | (byte)(this.Rule & (MapRules.Space | MapRules.HeadHunter | MapRules.Chaos | MapRules.Round | MapRules.HARDCORE | MapRules.HHHO)) << 12 | (int)(this.RoomType & (RoomCondition.DeathMatch | RoomCondition.Escape | RoomCondition.Bomb | RoomCondition.BattleRoyale | RoomCondition.Destroy | RoomCondition.Convoy | RoomCondition.Annihilation | RoomCondition.Defense | RoomCondition.FreeForAll | RoomCondition.Boss | RoomCondition.Ace | RoomCondition.StepUp | RoomCondition.Tutorial | RoomCondition.Domination | RoomCondition.CrossCounter | RoomCondition.Convoy1 | RoomCondition.Runaway | RoomCondition.Convoy2 | RoomCondition.Seize | RoomCondition.Glass)));
		}

		public List<Account> GetAllPlayers(SlotState State, int Type)
		{
			List<Account> accounts = new List<Account>();
			lock (this.Slots)
			{
				SlotModel[] slots = this.Slots;
				for (int i = 0; i < (int)slots.Length; i++)
				{
					SlotModel slotModel = slots[i];
					if (slotModel.PlayerId > 0L && (Type == 0 && slotModel.State == State || Type == 1 && slotModel.State > State))
					{
						Account account = AccountManager.GetAccount(slotModel.PlayerId, true);
						if (account != null && account.SlotId != -1)
						{
							accounts.Add(account);
						}
					}
				}
			}
			return accounts;
		}

		public List<Account> GetAllPlayers(SlotState State, int Type, int Exception)
		{
			List<Account> accounts = new List<Account>();
			lock (this.Slots)
			{
				SlotModel[] slots = this.Slots;
				for (int i = 0; i < (int)slots.Length; i++)
				{
					SlotModel slotModel = slots[i];
					if (slotModel.PlayerId > 0L && slotModel.Id != Exception && (Type == 0 && slotModel.State == State || Type == 1 && slotModel.State > State))
					{
						Account account = AccountManager.GetAccount(slotModel.PlayerId, true);
						if (account != null && account.SlotId != -1)
						{
							accounts.Add(account);
						}
					}
				}
			}
			return accounts;
		}

		public List<Account> GetAllPlayers(SlotState State, int Type, int Exception, int Exception2)
		{
			List<Account> accounts = new List<Account>();
			lock (this.Slots)
			{
				SlotModel[] slots = this.Slots;
				for (int i = 0; i < (int)slots.Length; i++)
				{
					SlotModel slotModel = slots[i];
					if (slotModel.PlayerId > 0L && slotModel.Id != Exception && slotModel.Id != Exception2 && (Type == 0 && slotModel.State == State || Type == 1 && slotModel.State > State))
					{
						Account account = AccountManager.GetAccount(slotModel.PlayerId, true);
						if (account != null && account.SlotId != -1)
						{
							accounts.Add(account);
						}
					}
				}
			}
			return accounts;
		}

		public List<Account> GetAllPlayers(int Exception)
		{
			List<Account> accounts = new List<Account>();
			lock (this.Slots)
			{
				SlotModel[] slots = this.Slots;
				for (int i = 0; i < (int)slots.Length; i++)
				{
					SlotModel slotModel = slots[i];
					long playerId = slotModel.PlayerId;
					if (playerId > 0L && slotModel.Id != Exception)
					{
						Account account = AccountManager.GetAccount(playerId, true);
						if (account != null && account.SlotId != -1)
						{
							accounts.Add(account);
						}
					}
				}
			}
			return accounts;
		}

		public List<Account> GetAllPlayers(long Exception)
		{
			List<Account> accounts = new List<Account>();
			lock (this.Slots)
			{
				SlotModel[] slots = this.Slots;
				for (int i = 0; i < (int)slots.Length; i++)
				{
					SlotModel slotModel = slots[i];
					if (slotModel.PlayerId > 0L && slotModel.PlayerId != Exception)
					{
						Account account = AccountManager.GetAccount(slotModel.PlayerId, true);
						if (account != null && account.SlotId != -1)
						{
							accounts.Add(account);
						}
					}
				}
			}
			return accounts;
		}

		public List<Account> GetAllPlayers()
		{
			List<Account> accounts = new List<Account>();
			lock (this.Slots)
			{
				SlotModel[] slots = this.Slots;
				for (int i = 0; i < (int)slots.Length; i++)
				{
					SlotModel slotModel = slots[i];
					if (slotModel.PlayerId > 0L)
					{
						Account account = AccountManager.GetAccount(slotModel.PlayerId, true);
						if (account != null && account.SlotId != -1)
						{
							accounts.Add(account);
						}
					}
				}
			}
			return accounts;
		}

		public ChannelModel GetChannel()
		{
			return ChannelsXML.GetChannel(this.ServerId, this.ChannelId);
		}

		public bool GetChannel(out ChannelModel Channel)
		{
			Channel = ChannelsXML.GetChannel(this.ServerId, this.ChannelId);
			return Channel != null;
		}

		public int GetCountPlayers()
		{
			int ınt32 = 0;
			lock (this.Slots)
			{
				SlotModel[] slots = this.Slots;
				for (int i = 0; i < (int)slots.Length; i++)
				{
					SlotModel slotModel = slots[i];
					if (slotModel.PlayerId > 0L)
					{
						Account account = AccountManager.GetAccount(slotModel.PlayerId, true);
						if (account != null && account.SlotId != -1)
						{
							ınt32++;
						}
					}
				}
			}
			return ınt32;
		}

		public int GetFlag()
		{
			int ınt32 = 0;
			if (this.Flag.HasFlag(RoomStageFlag.TEAM_SWAP))
			{
				ınt32++;
			}
			if (this.Flag.HasFlag(RoomStageFlag.RANDOM_MAP))
			{
				ınt32 += 2;
			}
			if (this.Flag.HasFlag(RoomStageFlag.PASSWORD) || this.Password.Length > 0)
			{
				ınt32 += 4;
			}
			if (this.Flag.HasFlag(RoomStageFlag.OBSERVER_MODE))
			{
				ınt32 += 8;
			}
			if (this.Flag.HasFlag(RoomStageFlag.REAL_IP))
			{
				ınt32 += 16;
			}
			if (this.Flag.HasFlag(RoomStageFlag.TEAM_BALANCE) || this.BalanceType == TeamBalance.Count)
			{
				ınt32 += 32;
			}
			if (this.Flag.HasFlag(RoomStageFlag.OBSERVER))
			{
				ınt32 += 64;
			}
			if (this.Flag.HasFlag(RoomStageFlag.INTER_ENTER) || this.Limit > 0 && this.IsStartingMatch())
			{
				ınt32 += 128;
			}
			this.Flag = (RoomStageFlag)ınt32;
			return ınt32;
		}

		public int GetInBattleTime()
		{
			int duration = 0;
			DateTime battleStart = this.BattleStart;
			DateTime dateTime = new DateTime();
			if (battleStart != dateTime && (this.State == RoomState.BATTLE || this.State == RoomState.PRE_BATTLE))
			{
				duration = (int)ComDiv.GetDuration(this.BattleStart);
				if (duration < 0)
				{
					duration = 0;
				}
			}
			return duration;
		}

		public int GetInBattleTimeLeft()
		{
			return this.GetTimeByMask() * 60 - this.GetInBattleTime();
		}

		public int GetKillsByMask()
		{
			return this.KILLS[this.KillTime & 15];
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
			return Player != null;
		}

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
			if (this.LeaderSlot == -1)
			{
				return null;
			}
			return AccountManager.GetAccount(this.Slots[this.LeaderSlot].PlayerId, true);
		}

		public Account GetPlayerBySlot(SlotModel Slot)
		{
			Account account;
			Account account1;
			try
			{
				long playerId = Slot.PlayerId;
				if (playerId > 0L)
				{
					account1 = AccountManager.GetAccount(playerId, true);
				}
				else
				{
					account1 = null;
				}
				account = account1;
			}
			catch
			{
				account = null;
			}
			return account;
		}

		public Account GetPlayerBySlot(int SlotId)
		{
			Account account;
			Account account1;
			try
			{
				long playerId = this.Slots[SlotId].PlayerId;
				if (playerId > 0L)
				{
					account1 = AccountManager.GetAccount(playerId, true);
				}
				else
				{
					account1 = null;
				}
				account = account1;
			}
			catch
			{
				account = null;
			}
			return account;
		}

		public bool GetPlayerBySlot(int SlotId, out Account Player)
		{
			bool player;
			Account account;
			try
			{
				long playerId = this.Slots[SlotId].PlayerId;
				if (playerId > 0L)
				{
					account = AccountManager.GetAccount(playerId, true);
				}
				else
				{
					account = null;
				}
				Player = account;
				player = Player != null;
			}
			catch
			{
				Player = null;
				player = false;
			}
			return player;
		}

		public bool GetPlayerBySlot(SlotModel Slot, out Account Player)
		{
			bool player;
			Account account;
			try
			{
				long playerId = Slot.PlayerId;
				if (playerId > 0L)
				{
					account = AccountManager.GetAccount(playerId, true);
				}
				else
				{
					account = null;
				}
				Player = account;
				player = Player != null;
			}
			catch
			{
				Player = null;
				player = false;
			}
			return player;
		}

		public int GetPlayingPlayers(TeamEnum Team, bool InBattle)
		{
			int ınt32 = 0;
			lock (this.Slots)
			{
				SlotModel[] slots = this.Slots;
				for (int i = 0; i < (int)slots.Length; i++)
				{
					SlotModel slotModel = slots[i];
					if (slotModel.PlayerId > 0L && (slotModel.Team == Team || Team == TeamEnum.TEAM_DRAW) && (InBattle && slotModel.State == SlotState.BATTLE_LOAD && !slotModel.Spectator || !InBattle && slotModel.State >= SlotState.LOAD))
					{
						ınt32++;
					}
				}
			}
			return ınt32;
		}

		public int GetPlayingPlayers(TeamEnum Team, SlotState State, int Type)
		{
			int ınt32 = 0;
			lock (this.Slots)
			{
				SlotModel[] slots = this.Slots;
				for (int i = 0; i < (int)slots.Length; i++)
				{
					SlotModel slotModel = slots[i];
					if (slotModel.PlayerId > 0L && (Type == 0 && slotModel.State == State || Type == 1 && slotModel.State > State) && (Team == TeamEnum.TEAM_DRAW || slotModel.Team == Team))
					{
						ınt32++;
					}
				}
			}
			return ınt32;
		}

		public int GetPlayingPlayers(TeamEnum Team, SlotState State, int Type, int Exception)
		{
			int ınt32 = 0;
			lock (this.Slots)
			{
				SlotModel[] slots = this.Slots;
				for (int i = 0; i < (int)slots.Length; i++)
				{
					SlotModel slotModel = slots[i];
					if (slotModel.Id != Exception && slotModel.PlayerId > 0L && (Type == 0 && slotModel.State == State || Type == 1 && slotModel.State > State) && (Team == TeamEnum.TEAM_DRAW || slotModel.Team == Team))
					{
						ınt32++;
					}
				}
			}
			return ınt32;
		}

		public void GetPlayingPlayers(bool InBattle, out int PlayerFR, out int PlayerCT)
		{
			PlayerFR = 0;
			PlayerCT = 0;
			lock (this.Slots)
			{
				SlotModel[] slots = this.Slots;
				for (int i = 0; i < (int)slots.Length; i++)
				{
					SlotModel slotModel = slots[i];
					if (slotModel.PlayerId > 0L && (InBattle && slotModel.State == SlotState.BATTLE && !slotModel.Spectator || !InBattle && slotModel.State >= SlotState.RENDEZVOUS))
					{
						if (slotModel.Team != TeamEnum.FR_TEAM)
						{
							PlayerCT++;
						}
						else
						{
							PlayerFR++;
						}
					}
				}
			}
		}

		public void GetPlayingPlayers(bool InBattle, out int PlayerFR, out int PlayerCT, out int DeathFR, out int DeathCT)
		{
			PlayerFR = 0;
			PlayerCT = 0;
			DeathFR = 0;
			DeathCT = 0;
			lock (this.Slots)
			{
				SlotModel[] slots = this.Slots;
				for (int i = 0; i < (int)slots.Length; i++)
				{
					SlotModel slotModel = slots[i];
					if (slotModel.DeathState.HasFlag(DeadEnum.Dead))
					{
						if (slotModel.Team != TeamEnum.FR_TEAM)
						{
							DeathCT++;
						}
						else
						{
							DeathFR++;
						}
					}
					if (slotModel.PlayerId > 0L && (InBattle && slotModel.State == SlotState.BATTLE && !slotModel.Spectator || !InBattle && slotModel.State >= SlotState.RENDEZVOUS))
					{
						if (slotModel.Team != TeamEnum.FR_TEAM)
						{
							PlayerCT++;
						}
						else
						{
							PlayerFR++;
						}
					}
				}
			}
		}

		public int GetReadyPlayers()
		{
			int ınt32 = 0;
			SlotModel[] slots = this.Slots;
			for (int i = 0; i < (int)slots.Length; i++)
			{
				SlotModel slotModel = slots[i];
				if (slotModel != null && slotModel.State >= SlotState.READY && slotModel.Equipment != null)
				{
					Account playerBySlot = this.GetPlayerBySlot(slotModel);
					if (playerBySlot != null && playerBySlot.SlotId == slotModel.Id)
					{
						ınt32++;
					}
				}
			}
			return ınt32;
		}

		public int GetRoundsByMask()
		{
			return this.ROUNDS[this.KillTime & 15];
		}

		public bool GetSlot(int SlotId, out SlotModel Slot)
		{
			bool slot;
			Slot = null;
			lock (this.Slots)
			{
				if (SlotId >= 0 && SlotId <= 17)
				{
					Slot = this.Slots[SlotId];
				}
				slot = Slot != null;
			}
			return slot;
		}

		public SlotModel GetSlot(int SlotIdx)
		{
			SlotModel slots;
			lock (this.Slots)
			{
				if (SlotIdx < 0 || SlotIdx > 17)
				{
					slots = null;
				}
				else
				{
					slots = this.Slots[SlotIdx];
				}
			}
			return slots;
		}

		public int GetSlotCount()
		{
			int ınt32;
			lock (this.Slots)
			{
				int ınt321 = 0;
				foreach (SlotModel slotModel in ((IEnumerable<SlotModel>)this.Slots).Where<SlotModel>((SlotModel slotModel_0) => {
					if (slotModel_0.Id == 16)
					{
						return false;
					}
					return slotModel_0.Id != 17;
				}))
				{
					if (slotModel.State == SlotState.CLOSE)
					{
						continue;
					}
					ınt321++;
				}
				ınt32 = ınt321;
			}
			return ınt32;
		}

		public int[] GetTeamArray(TeamEnum Team)
		{
			if (Team == TeamEnum.FR_TEAM)
			{
				return this.FR_TEAM;
			}
			if (Team != TeamEnum.CT_TEAM)
			{
				return this.ALL_TEAM;
			}
			return this.CT_TEAM;
		}

		public int GetTimeByMask()
		{
			return this.TIMES[this.KillTime >> 4];
		}

		public bool IsBotMode()
		{
			if (this.Stage == StageOptions.AI || this.Stage == StageOptions.DieHard)
			{
				return true;
			}
			return this.Stage == StageOptions.Infection;
		}

		public bool IsDinoMode(string Dino = "")
		{
			if (Dino.Equals("DE"))
			{
				return this.RoomType == RoomCondition.Boss;
			}
			if (Dino.Equals("CC"))
			{
				return this.RoomType == RoomCondition.CrossCounter;
			}
			if (this.RoomType == RoomCondition.Boss)
			{
				return true;
			}
			return this.RoomType == RoomCondition.CrossCounter;
		}

		public bool IsPreparing()
		{
			return this.State >= RoomState.LOADING;
		}

		public bool IsStartingMatch()
		{
			return this.State > RoomState.READY;
		}

		private void method_0()
		{
			this.UniqueRoomId = (uint)((this.ServerId & 255) << 20 | (this.ChannelId & 255) << 12 | this.RoomId & 4095);
		}

		private void method_1()
		{
			if (this.RoomType == RoomCondition.Defense)
			{
				if (this.MapId == MapIdEnum.BlackPanther)
				{
					this.Bar1 = 6000;
					this.Bar2 = 9000;
					return;
				}
			}
			else if (this.RoomType == RoomCondition.Destroy)
			{
				if (this.MapId == MapIdEnum.Helispot)
				{
					this.Bar1 = 12000;
					this.Bar2 = 12000;
					return;
				}
				if (this.MapId == MapIdEnum.BreakDown)
				{
					this.Bar1 = 6000;
					this.Bar2 = 6000;
				}
			}
		}

		private void method_10(Account account_0, StatisticDaily statisticDaily_0, SlotModel slotModel_0, DBQuery dbquery_0, int int_0, bool bool_0, int int_1)
		{
			StatisticDaily statisticDaily0 = statisticDaily_0;
			statisticDaily0.KillsCount = statisticDaily0.KillsCount + slotModel_0.AllKills;
			StatisticDaily deathsCount = statisticDaily_0;
			deathsCount.DeathsCount = deathsCount.DeathsCount + slotModel_0.AllDeaths;
			StatisticDaily headshotsCount = statisticDaily_0;
			headshotsCount.HeadshotsCount = headshotsCount.HeadshotsCount + slotModel_0.AllHeadshots;
			this.method_13(slotModel_0, account_0.Statistic, dbquery_0);
			if (this.RoomType == RoomCondition.FreeForAll)
			{
				AllUtils.UpdateMatchDailyRecordFFA(this, account_0, int_0, dbquery_0);
				return;
			}
			AllUtils.UpdateDailyRecord(bool_0, account_0, int_1, dbquery_0);
		}

		private void method_11(Account account_0, StatisticWeapon statisticWeapon_0, SlotModel slotModel_0, DBQuery dbquery_0)
		{
			StatisticWeapon statisticWeapon0 = statisticWeapon_0;
			statisticWeapon0.AssaultKills = statisticWeapon0.AssaultKills + slotModel_0.AR[0];
			StatisticWeapon assaultDeaths = statisticWeapon_0;
			assaultDeaths.AssaultDeaths = assaultDeaths.AssaultDeaths + slotModel_0.AR[1];
			StatisticWeapon smgKills = statisticWeapon_0;
			smgKills.SmgKills = smgKills.SmgKills + slotModel_0.SMG[0];
			StatisticWeapon smgDeaths = statisticWeapon_0;
			smgDeaths.SmgDeaths = smgDeaths.SmgDeaths + slotModel_0.SMG[1];
			StatisticWeapon sniperKills = statisticWeapon_0;
			sniperKills.SniperKills = sniperKills.SniperKills + slotModel_0.SR[0];
			StatisticWeapon sniperDeaths = statisticWeapon_0;
			sniperDeaths.SniperDeaths = sniperDeaths.SniperDeaths + slotModel_0.SR[1];
			StatisticWeapon shotgunKills = statisticWeapon_0;
			shotgunKills.ShotgunKills = shotgunKills.ShotgunKills + slotModel_0.SG[0];
			StatisticWeapon shotgunDeaths = statisticWeapon_0;
			shotgunDeaths.ShotgunDeaths = shotgunDeaths.ShotgunDeaths + slotModel_0.SG[1];
			StatisticWeapon machinegunKills = statisticWeapon_0;
			machinegunKills.MachinegunKills = machinegunKills.MachinegunKills + slotModel_0.MG[0];
			StatisticWeapon machinegunDeaths = statisticWeapon_0;
			machinegunDeaths.MachinegunDeaths = machinegunDeaths.MachinegunDeaths + slotModel_0.MG[1];
			StatisticWeapon shieldKills = statisticWeapon_0;
			shieldKills.ShieldKills = shieldKills.ShieldKills + slotModel_0.SHD[0];
			StatisticWeapon shieldDeaths = statisticWeapon_0;
			shieldDeaths.ShieldDeaths = shieldDeaths.ShieldDeaths + slotModel_0.SHD[1];
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
			int i;
			Account account;
			if (this.ChannelType != Plugin.Core.Enums.ChannelType.Clan || this.BlockedClan)
			{
				return;
			}
			SortedList<int, ClanModel> ınt32s = new SortedList<int, ClanModel>();
			SlotModel[] slots = this.Slots;
			for (i = 0; i < (int)slots.Length; i++)
			{
				SlotModel slotModel = slots[i];
				if (slotModel.State == SlotState.BATTLE && this.GetPlayerBySlot(slotModel, out account))
				{
					ClanModel clan = ClanManager.GetClan(account.ClanId);
					if (clan.Id != 0)
					{
						bool team = slotModel.Team == teamEnum_0;
						clan.Exp += slotModel.Exp;
						clan.BestPlayers.SetBestExp(slotModel);
						clan.BestPlayers.SetBestKills(slotModel);
						clan.BestPlayers.SetBestHeadshot(slotModel);
						clan.BestPlayers.SetBestWins(account.Statistic, slotModel, team);
						clan.BestPlayers.SetBestParticipation(account.Statistic, slotModel);
						if (!ınt32s.ContainsKey(account.ClanId))
						{
							ınt32s.Add(account.ClanId, clan);
							if (teamEnum_0 != TeamEnum.TEAM_DRAW)
							{
								this.method_15(clan, teamEnum_0, slotModel.Team);
								if (!team)
								{
									clan.MatchLoses++;
								}
								else
								{
									clan.MatchWins++;
								}
							}
							clan.Matches++;
							DaoManagerSQL.UpdateClanBattles(clan.Id, clan.Matches, clan.MatchWins, clan.MatchLoses);
						}
					}
				}
			}
			foreach (ClanModel value in ınt32s.Values)
			{
				DaoManagerSQL.UpdateClanExp(value.Id, value.Exp);
				DaoManagerSQL.UpdateClanPoints(value.Id, value.Points);
				DaoManagerSQL.UpdateClanBestPlayers(value);
				RankModel rank = ClanRankXML.GetRank(value.Rank);
				if (rank == null || value.Exp < rank.OnNextLevel + rank.OnAllExp)
				{
					continue;
				}
				ClanModel clanModel = value;
				i = clanModel.Rank + 1;
				clanModel.Rank = i;
				DaoManagerSQL.UpdateClanRank(value.Id, i);
			}
		}

		private void method_15(ClanModel clanModel_0, TeamEnum teamEnum_0, TeamEnum teamEnum_1)
		{
			float single;
			float single1;
			if (teamEnum_0 == TeamEnum.TEAM_DRAW)
			{
				return;
			}
			if (teamEnum_0 == teamEnum_1)
			{
				single = (this.RoomType != RoomCondition.DeathMatch ? (float)((teamEnum_1 == TeamEnum.FR_TEAM ? this.FRRounds : this.CTRounds)) : (float)((teamEnum_1 == TeamEnum.FR_TEAM ? this.FRKills : this.CTKills) / 20));
				clanModel_0.Points += 25f + single;
				return;
			}
			if (clanModel_0.Points == 0f)
			{
				return;
			}
			single1 = (this.RoomType != RoomCondition.DeathMatch ? (float)((teamEnum_1 == TeamEnum.FR_TEAM ? this.FRRounds : this.CTRounds)) : (float)((teamEnum_1 == TeamEnum.FR_TEAM ? this.FRKills : this.CTKills) / 20));
			clanModel_0.Points -= 40f - single1;
		}

		private void method_16(Account account_0, SlotModel slotModel_0, bool bool_0, int int_0)
		{
			lock (this.Slots)
			{
				bool flag = false;
				bool flag1 = false;
				if (account_0 != null && slotModel_0 != null)
				{
					if (slotModel_0.State >= SlotState.LOAD)
					{
						if (this.LeaderSlot == slotModel_0.Id)
						{
							int leaderSlot = this.LeaderSlot;
							SlotState slotState = SlotState.CLOSE;
							if (this.State == RoomState.BATTLE)
							{
								slotState = SlotState.BATTLE_READY;
							}
							else if (this.State >= RoomState.LOADING)
							{
								slotState = SlotState.READY;
							}
							if (this.GetAllPlayers(slotModel_0.Id).Count >= 1)
							{
								this.SetNewLeader(-1, slotState, leaderSlot, false);
							}
							if (this.GetPlayingPlayers(TeamEnum.TEAM_DRAW, SlotState.READY, 1) >= 2)
							{
								using (PROTOCOL_BATTLE_LEAVEP2PSERVER_ACK pROTOCOLBATTLELEAVEP2PSERVERACK = new PROTOCOL_BATTLE_LEAVEP2PSERVER_ACK(this))
								{
									this.SendPacketToPlayers(pROTOCOLBATTLELEAVEP2PSERVERACK, SlotState.RENDEZVOUS, 1, slotModel_0.Id);
								}
							}
							flag1 = true;
						}
						using (PROTOCOL_BATTLE_GIVEUPBATTLE_ACK pROTOCOLBATTLEGIVEUPBATTLEACK = new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(account_0, int_0))
						{
							this.SendPacketToPlayers(pROTOCOLBATTLEGIVEUPBATTLEACK, SlotState.READY, 1, (!bool_0 ? slotModel_0.Id : -1));
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
					else if (slotModel_0.Id == this.LeaderSlot)
					{
						this.State = RoomState.READY;
						flag = true;
						this.CountdownTime.StopJob();
						using (PROTOCOL_BATTLE_START_COUNTDOWN_ACK pROTOCOLBATTLESTARTCOUNTDOWNACK = new PROTOCOL_BATTLE_START_COUNTDOWN_ACK(CountDownEnum.StopByHost))
						{
							this.SendPacketToPlayers(pROTOCOLBATTLESTARTCOUNTDOWNACK);
						}
					}
					else if (this.GetPlayingPlayers(slotModel_0.Team, SlotState.READY, 0) == 0)
					{
						if (slotModel_0.Id != this.LeaderSlot)
						{
							this.ChangeSlotState(this.LeaderSlot, SlotState.NORMAL, false);
						}
						this.StopCountDown(CountDownEnum.StopByPlayer, false);
						flag = true;
					}
					this.CheckToEndWaitingBattle(flag1);
					this.RequestRoomMaster.Remove(account_0.PlayerId);
					if (this.VoteTime.IsTimer() && this.VoteKick != null && this.VoteKick.VictimIdx == account_0.SlotId && int_0 != 2)
					{
						this.VoteTime.StopJob();
						this.VoteKick = null;
						using (PROTOCOL_BATTLE_NOTIFY_KICKVOTE_CANCEL_ACK pROTOCOLBATTLENOTIFYKICKVOTECANCELACK = new PROTOCOL_BATTLE_NOTIFY_KICKVOTE_CANCEL_ACK())
						{
							this.SendPacketToPlayers(pROTOCOLBATTLENOTIFYKICKVOTECANCELACK, SlotState.BATTLE, 0);
						}
					}
					MatchModel match = account_0.Match;
					if (match != null && account_0.MatchSlot >= 0)
					{
						match.Slots[account_0.MatchSlot].State = SlotMatchState.Normal;
						using (PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK pROTOCOLCLANWARREGISTMERCENARYACK = new PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK(match))
						{
							match.SendPacketToPlayers(pROTOCOLCLANWARREGISTMERCENARYACK);
						}
					}
					account_0.Room = null;
					account_0.SlotId = -1;
					account_0.Status.UpdateRoom(255);
					AllUtils.SyncPlayerToClanMembers(account_0);
					AllUtils.SyncPlayerToFriends(account_0, false);
					account_0.UpdateCacheInfo();
				}
				this.UpdateSlotsInfo();
				if (flag)
				{
					this.UpdateRoomInfo();
				}
			}
		}

		private void method_17()
		{
			int i;
			Account account;
			DateTime dateTime = DateTimeUtil.Now();
			SlotModel[] slots = this.Slots;
			for (i = 0; i < (int)slots.Length; i++)
			{
				SlotModel slotModel = slots[i];
				if (slotModel.State == SlotState.BATTLE_READY && slotModel.IsPlaying == 0 && slotModel.PlayerId > 0L)
				{
					slotModel.IsPlaying = 1;
					slotModel.StartTime = dateTime;
					slotModel.State = SlotState.BATTLE;
					if (this.State == RoomState.BATTLE && (this.RoomType == RoomCondition.Bomb || this.RoomType == RoomCondition.Annihilation || this.RoomType == RoomCondition.Destroy || this.RoomType == RoomCondition.Ace || this.RoomType == RoomCondition.Glass))
					{
						slotModel.Spectator = true;
					}
				}
			}
			this.UpdateSlotsInfo();
			List<int> dinossaurs = AllUtils.GetDinossaurs(this, false, -1);
			if (this.State == RoomState.PRE_BATTLE)
			{
				this.BattleStart = (this.IsDinoMode("") ? dateTime.AddMinutes(5) : dateTime);
				this.method_1();
			}
			bool flag = false;
			using (PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK pROTOCOLBATTLEMISSIONROUNDPRESTARTACK = new PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK(this, dinossaurs))
			{
				using (PROTOCOL_BATTLE_MISSION_ROUND_START_ACK pROTOCOLBATTLEMISSIONROUNDSTARTACK = new PROTOCOL_BATTLE_MISSION_ROUND_START_ACK(this))
				{
					using (PROTOCOL_BATTLE_RECORD_ACK pROTOCOLBATTLERECORDACK = new PROTOCOL_BATTLE_RECORD_ACK(this))
					{
						byte[] completeBytes = pROTOCOLBATTLEMISSIONROUNDPRESTARTACK.GetCompleteBytes("Room.BaseSpawnReadyPlayers-1");
						byte[] numArray = pROTOCOLBATTLEMISSIONROUNDSTARTACK.GetCompleteBytes("Room.BaseSpawnReadyPlayers-2");
						byte[] completeBytes1 = pROTOCOLBATTLERECORDACK.GetCompleteBytes("Room.BaseSpawnReadyPlayers-3");
						slots = this.Slots;
						for (i = 0; i < (int)slots.Length; i++)
						{
							SlotModel slotModel1 = slots[i];
							if (slotModel1.State == SlotState.BATTLE && slotModel1.IsPlaying == 1 && this.GetPlayerBySlot(slotModel1, out account))
							{
								slotModel1.FirstInactivityOff = true;
								slotModel1.IsPlaying = 2;
								if (this.State == RoomState.PRE_BATTLE)
								{
									using (PROTOCOL_BATTLE_STARTBATTLE_ACK pROTOCOLBATTLESTARTBATTLEACK = new PROTOCOL_BATTLE_STARTBATTLE_ACK(slotModel1, account, dinossaurs, true))
									{
										this.SendPacketToPlayers(pROTOCOLBATTLESTARTBATTLEACK, SlotState.READY, 1);
									}
									account.SendCompletePacket(completeBytes, pROTOCOLBATTLEMISSIONROUNDPRESTARTACK.GetType().Name);
									if (!this.IsDinoMode(""))
									{
										account.SendCompletePacket(numArray, pROTOCOLBATTLEMISSIONROUNDSTARTACK.GetType().Name);
									}
									else
									{
										flag = true;
									}
								}
								else if (this.State == RoomState.BATTLE)
								{
									using (PROTOCOL_BATTLE_STARTBATTLE_ACK pROTOCOLBATTLESTARTBATTLEACK1 = new PROTOCOL_BATTLE_STARTBATTLE_ACK(slotModel1, account, dinossaurs, false))
									{
										this.SendPacketToPlayers(pROTOCOLBATTLESTARTBATTLEACK1, SlotState.READY, 1);
									}
									if (this.RoomType != RoomCondition.Bomb && this.RoomType != RoomCondition.Annihilation && this.RoomType != RoomCondition.Destroy && this.RoomType != RoomCondition.Ace)
									{
										if (this.RoomType == RoomCondition.Glass)
										{
											goto Label2;
										}
										account.SendCompletePacket(completeBytes, pROTOCOLBATTLEMISSIONROUNDPRESTARTACK.GetType().Name);
										goto Label0;
									}
								Label2:
									EquipmentSync.SendUDPPlayerSync(this, slotModel1, 0L, 1);
								Label0:
									account.SendCompletePacket(numArray, pROTOCOLBATTLEMISSIONROUNDSTARTACK.GetType().Name);
									account.SendCompletePacket(completeBytes1, pROTOCOLBATTLERECORDACK.GetType().Name);
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
			this.RoundTime.StartJob(5250, (object object_0) => {
				if (this.State == RoomState.BATTLE)
				{
					using (PROTOCOL_BATTLE_MISSION_ROUND_START_ACK pROTOCOLBATTLEMISSIONROUNDSTARTACK = new PROTOCOL_BATTLE_MISSION_ROUND_START_ACK(this))
					{
						this.SendPacketToPlayers(pROTOCOLBATTLEMISSIONROUNDSTARTACK, SlotState.BATTLE, 0);
					}
				}
				lock (object_0)
				{
					this.RoundTime.StopJob();
				}
			});
		}

		private bool method_2()
		{
			if (!this.Flag.HasFlag(RoomStageFlag.TEAM_SWAP))
			{
				return false;
			}
			if (this.IsDinoMode(""))
			{
				return this.Rounds == 2;
			}
			int roundsByMask = this.GetRoundsByMask() - 1;
			if (this.FRRounds == roundsByMask && this.CTRounds == 0 || this.CTRounds == roundsByMask && this.FRRounds == 0)
			{
				return true;
			}
			return false;
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
			SlotModel[] slots = this.Slots;
			for (int i = 0; i < (int)slots.Length; i++)
			{
				SlotModel slotModel = slots[i];
				if (slotModel.PlayerId > 0L)
				{
					if (!slotModel.DeathState.HasFlag(DeadEnum.UseChat))
					{
						slotModel.DeathState |= DeadEnum.UseChat;
					}
					if (slotModel.Spectator)
					{
						slotModel.Spectator = false;
					}
				}
			}
			this.StopBomb();
			DateTime dateTime = DateTimeUtil.Now();
			if (this.State == RoomState.BATTLE)
			{
				this.BattleStart = (this.IsDinoMode("") ? dateTime.AddSeconds(5) : dateTime);
			}
			List<int> dinossaurs = AllUtils.GetDinossaurs(this, false, -1);
			using (PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK pROTOCOLBATTLEMISSIONROUNDPRESTARTACK = new PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK(this, dinossaurs))
			{
				this.SendPacketToPlayers(pROTOCOLBATTLEMISSIONROUNDPRESTARTACK, SlotState.BATTLE, 0);
			}
			if (!this.method_2())
			{
				this.method_5();
			}
			else
			{
				this.RoundTeamSwap.StartJob(5250, (object object_0) => {
					this.method_5();
					lock (object_0)
					{
						this.RoundTeamSwap.StopJob();
					}
				});
			}
			this.StopBomb();
		}

		private void method_5()
		{
			using (PROTOCOL_BATTLE_MISSION_ROUND_START_ACK pROTOCOLBATTLEMISSIONROUNDSTARTACK = new PROTOCOL_BATTLE_MISSION_ROUND_START_ACK(this))
			{
				this.SendPacketToPlayers(pROTOCOLBATTLEMISSIONROUNDSTARTACK, SlotState.BATTLE, 0);
			}
		}

		private void method_6()
		{
			if (this.Slots[this.LeaderSlot].State == SlotState.READY && this.State == RoomState.COUNTDOWN)
			{
				this.StartBattle(true);
				return;
			}
			using (PROTOCOL_BATTLE_READYBATTLE_ACK pROTOCOLBATTLEREADYBATTLEACK = new PROTOCOL_BATTLE_READYBATTLE_ACK(-2147479544))
			{
				byte[] completeBytes = pROTOCOLBATTLEREADYBATTLEACK.GetCompleteBytes("Room.ReadyBattle");
				foreach (Account allPlayer in this.GetAllPlayers(SlotState.READY, 0))
				{
					SlotModel slot = this.GetSlot(allPlayer.SlotId);
					if (slot == null || slot.State != SlotState.READY)
					{
						continue;
					}
					allPlayer.SendCompletePacket(completeBytes, pROTOCOLBATTLEREADYBATTLEACK.GetType().Name);
				}
			}
		}

		private void method_7(TeamEnum teamEnum_0, bool bool_0)
		{
			Account missions;
			int premiumExp;
			int premiumGold;
			ServerConfig config = GameXender.Client.Config;
			EventRankUpModel runningEvent = EventRankUpXML.GetRunningEvent();
			EventBoostModel eventBoostModel = EventBoostXML.GetRunningEvent();
			EventPlaytimeModel eventPlaytimeModel = EventPlaytimeXML.GetRunningEvent();
			BattlePassModel activeSeasonPass = SeasonChallengeXML.GetActiveSeasonPass();
			DateTime dateTime = DateTimeUtil.Now();
			int[] allKills = new int[18];
			int ınt32 = 0;
			if (config == null)
			{
				CLogger.Print("Server Config Null. RoomResult canceled.", LoggerType.Warning, null);
				return;
			}
			List<SlotModel> slotModels = new List<SlotModel>();
			for (int i = 0; i < 18; i++)
			{
				SlotModel slots = this.Slots[i];
				if (slots.PlayerId == 0)
				{
					allKills[i] = 0;
				}
				else
				{
					allKills[i] = slots.AllKills;
				}
				if (allKills[i] > allKills[ınt32])
				{
					ınt32 = i;
				}
				if (!slots.Check && slots.State == SlotState.BATTLE && this.GetPlayerBySlot(slots, out missions))
				{
					StatisticTotal basic = missions.Statistic.Basic;
					StatisticSeason season = missions.Statistic.Season;
					StatisticDaily daily = missions.Statistic.Daily;
					StatisticWeapon weapon = missions.Statistic.Weapon;
					DBQuery dBQuery = new DBQuery();
					DBQuery dBQuery1 = new DBQuery();
					DBQuery dBQuery2 = new DBQuery();
					DBQuery dBQuery3 = new DBQuery();
					DBQuery dBQuery4 = new DBQuery();
					slots.Check = true;
					double num = slots.InBattleTime(dateTime);
					int gold = missions.Gold;
					int exp = missions.Exp;
					int cash = missions.Cash;
					if (bool_0)
					{
						int ıngameAiLevel = this.IngameAiLevel * (150 + slots.AllDeaths);
						if (ıngameAiLevel == 0)
						{
							ıngameAiLevel++;
						}
						int score = slots.Score / ıngameAiLevel;
						slots.Gold += score;
						slots.Exp += score;
					}
					else
					{
						if (config.Missions)
						{
							AllUtils.EndMatchMission(this, missions, slots, teamEnum_0);
							if (slots.MissionsCompleted)
							{
								missions.Mission = slots.Missions;
								DaoManagerSQL.UpdateCurrentPlayerMissionList(missions.PlayerId, missions.Mission);
							}
							AllUtils.GenerateMissionAwards(missions, dBQuery);
						}
						int ınt321 = (slots.AllKills != 0 || slots.AllDeaths != 0 ? (int)num : (int)(num / 3));
						if (this.RoomType != RoomCondition.Bomb && this.RoomType != RoomCondition.Annihilation)
						{
							if (this.RoomType == RoomCondition.Ace)
							{
								goto Label2;
							}
							slots.Exp = (int)((double)slots.Score + (double)ınt321 / 2.5 + (double)slots.AllDeaths * 1.8 + (double)(slots.Objects * 20));
							slots.Gold = (int)((double)slots.Score + (double)ınt321 / 3 + (double)slots.AllDeaths * 1.8 + (double)(slots.Objects * 20));
							slots.Cash = (int)((double)slots.Score / 1.5 + (double)ınt321 / 4.5 + (double)slots.AllDeaths * 1.1 + (double)(slots.Objects * 20));
							goto Label0;
						}
					Label2:
						slots.Exp = (int)((double)slots.Score + (double)ınt321 / 2.5 + (double)slots.AllDeaths * 2.2 + (double)(slots.Objects * 20));
						slots.Gold = (int)((double)slots.Score + (double)ınt321 / 3 + (double)slots.AllDeaths * 2.2 + (double)(slots.Objects * 20));
						slots.Cash = (int)((double)(slots.Score / 2) + (double)ınt321 / 6.5 + (double)slots.AllDeaths * 1.5 + (double)(slots.Objects * 10));
					Label0:
						bool team = slots.Team == teamEnum_0;
						if (this.Rule != MapRules.Chaos && this.Rule != MapRules.HeadHunter)
						{
							if (basic != null && season != null)
							{
								this.method_9(missions, basic, season, slots, dBQuery1, dBQuery2, ınt32, team, (int)teamEnum_0);
							}
							if (daily != null)
							{
								this.method_10(missions, daily, slots, dBQuery3, ınt32, team, (int)teamEnum_0);
							}
							if (weapon != null)
							{
								this.method_11(missions, weapon, slots, dBQuery4);
							}
						}
						if (team || this.RoomType == RoomCondition.FreeForAll && (int)teamEnum_0 == ınt32)
						{
							slots.Gold += ComDiv.Percentage(slots.Gold, 15);
							slots.Exp += ComDiv.Percentage(slots.Exp, 20);
						}
						if (slots.EarnedEXP > 0)
						{
							SlotModel slotModel = slots;
							slotModel.Exp = slotModel.Exp + slots.EarnedEXP * 5;
						}
					}
					slots.Exp = (slots.Exp > ConfigLoader.MaxExpReward ? ConfigLoader.MaxExpReward : slots.Exp);
					slots.Gold = (slots.Gold > ConfigLoader.MaxGoldReward ? ConfigLoader.MaxGoldReward : slots.Gold);
					slots.Cash = (slots.Cash > ConfigLoader.MaxCashReward ? ConfigLoader.MaxCashReward : slots.Cash);
					if (this.RoomType == RoomCondition.Ace)
					{
						if (missions.SlotId < 0 || missions.SlotId > 1)
						{
							slots.Exp = 0;
							slots.Gold = 0;
							slots.Cash = 0;
						}
					}
					else if (slots.Exp < 0 || slots.Gold < 0 || slots.Cash < 0)
					{
						slots.Exp = 2;
						slots.Gold = 2;
						slots.Cash = 2;
					}
					int ınt322 = 0;
					int ınt323 = 0;
					int expUp = 0;
					int pointUp = 0;
					int bonusExp = 0;
					int bonusGold = 0;
					int ınt324 = 0;
					if (runningEvent != null || eventBoostModel != null)
					{
						int[] bonuses = runningEvent.GetBonuses(missions.Rank);
						if (runningEvent != null && bonuses != null)
						{
							bonusExp += ComDiv.Percentage(bonuses[0], bonuses[2]);
							bonusGold += ComDiv.Percentage(bonuses[1], bonuses[2]);
						}
						if (eventBoostModel != null && eventBoostModel.BoostType == PortalBoostEvent.Mode)
						{
							bonusExp += eventBoostModel.BonusExp;
							bonusGold += eventBoostModel.BonusGold;
						}
						if (!slots.BonusFlags.HasFlag(ResultIcon.Event))
						{
							slots.BonusFlags |= ResultIcon.Event;
						}
						slots.BonusEventExp += bonusExp;
						slots.BonusEventPoint += bonusGold;
					}
					PlayerBonus bonus = missions.Bonus;
					if (bonus != null && bonus.Bonuses > 0)
					{
						if ((bonus.Bonuses & 8) == 8)
						{
							ınt322 += 100;
						}
						if ((bonus.Bonuses & 128) == 128)
						{
							ınt323 += 100;
						}
						if ((bonus.Bonuses & 4) == 4)
						{
							ınt322 += 50;
						}
						if ((bonus.Bonuses & 64) == 64)
						{
							ınt323 += 50;
						}
						if ((bonus.Bonuses & 2) == 2)
						{
							ınt322 += 30;
						}
						if ((bonus.Bonuses & 32) == 32)
						{
							ınt323 += 30;
						}
						if ((bonus.Bonuses & 1) == 1)
						{
							ınt322 += 10;
						}
						if ((bonus.Bonuses & 16) == 16)
						{
							ınt323 += 10;
						}
						if ((bonus.Bonuses & 512) == 512)
						{
							ınt324 += 20;
						}
						if ((bonus.Bonuses & 1024) == 1024)
						{
							ınt324 += 30;
						}
						if ((bonus.Bonuses & 2048) == 2048)
						{
							ınt324 += 50;
						}
						if ((bonus.Bonuses & 4096) == 4096)
						{
							ınt324 += 100;
						}
						if (!slots.BonusFlags.HasFlag(ResultIcon.Item))
						{
							slots.BonusFlags |= ResultIcon.Item;
						}
						slots.BonusItemExp += ınt322;
						slots.BonusItemPoint += ınt323;
						slots.BonusBattlePass += ınt324;
					}
					PCCafeModel pCCafe = TemplatePackXML.GetPCCafe(missions.CafePC);
					if (pCCafe != null)
					{
						PlayerVip playerVIP = DaoManagerSQL.GetPlayerVIP(missions.PlayerId);
						if (playerVIP != null && InternetCafeXML.IsValidAddress(DaoManagerSQL.GetPlayerIP4Address(missions.PlayerId), playerVIP.Address))
						{
							InternetCafe cafe = InternetCafeXML.GetICafe(ConfigLoader.InternetCafeId);
							if (cafe != null && (missions.CafePC == CafeEnum.Gold || missions.CafePC == CafeEnum.Silver))
							{
								int ınt325 = expUp;
								if (missions.CafePC == CafeEnum.Gold)
								{
									premiumExp = cafe.PremiumExp;
								}
								else
								{
									premiumExp = (missions.CafePC == CafeEnum.Silver ? cafe.BasicExp : 0);
								}
								expUp = ınt325 + premiumExp;
								int ınt326 = pointUp;
								if (missions.CafePC == CafeEnum.Gold)
								{
									premiumGold = cafe.PremiumGold;
								}
								else
								{
									premiumGold = (missions.CafePC == CafeEnum.Silver ? cafe.BasicGold : 0);
								}
								pointUp = ınt326 + premiumGold;
							}
						}
						expUp += pCCafe.ExpUp;
						pointUp += pCCafe.PointUp;
						if (missions.CafePC == CafeEnum.Silver && !slots.BonusFlags.HasFlag(ResultIcon.Pc))
						{
							slots.BonusFlags |= ResultIcon.Pc;
						}
						else if (missions.CafePC == CafeEnum.Gold && !slots.BonusFlags.HasFlag(ResultIcon.PcPlus))
						{
							slots.BonusFlags |= ResultIcon.PcPlus;
						}
						slots.BonusCafeExp += expUp;
						slots.BonusCafePoint += pointUp;
					}
					if (bool_0)
					{
						if (slots.BonusItemExp > 300)
						{
							slots.BonusItemExp = 300;
						}
						if (slots.BonusItemPoint > 300)
						{
							slots.BonusItemPoint = 300;
						}
						if (slots.BonusCafeExp > 300)
						{
							slots.BonusCafeExp = 300;
						}
						if (slots.BonusCafePoint > 300)
						{
							slots.BonusCafePoint = 300;
						}
						if (slots.BonusEventExp > 300)
						{
							slots.BonusEventExp = 300;
						}
						if (slots.BonusEventPoint > 300)
						{
							slots.BonusEventPoint = 300;
						}
					}
					int bonusEventExp = slots.BonusEventExp + slots.BonusCafeExp + slots.BonusItemExp;
					int bonusEventPoint = slots.BonusEventPoint + slots.BonusCafePoint + slots.BonusItemPoint;
					Account account = missions;
					account.Exp = account.Exp + slots.Exp + ComDiv.Percentage(slots.Exp, bonusEventExp);
					Account gold1 = missions;
					gold1.Gold = gold1.Gold + slots.Gold + ComDiv.Percentage(slots.Gold, bonusEventPoint);
					if (daily != null)
					{
						StatisticDaily expGained = daily;
						expGained.ExpGained = expGained.ExpGained + slots.Exp + ComDiv.Percentage(slots.Exp, bonusEventExp);
						StatisticDaily pointGained = daily;
						pointGained.PointGained = pointGained.PointGained + slots.Gold + ComDiv.Percentage(slots.Gold, bonusEventPoint);
						StatisticDaily playtime = daily;
						playtime.Playtime = playtime.Playtime + (uint)num;
						dBQuery3.AddQuery("exp_gained", daily.ExpGained);
						dBQuery3.AddQuery("point_gained", daily.PointGained);
						dBQuery3.AddQuery("playtime", (long)((ulong)daily.Playtime));
					}
					if (ConfigLoader.WinCashPerBattle)
					{
						missions.Cash += slots.Cash;
					}
					RankModel rank = PlayerRankXML.GetRank(missions.Rank);
					if (rank != null && missions.Exp >= rank.OnNextLevel + rank.OnAllExp && missions.Rank <= 50)
					{
						List<int> rewards = PlayerRankXML.GetRewards(missions.Rank);
						if (rewards.Count > 0)
						{
							foreach (int reward in rewards)
							{
								GoodsItem good = ShopManager.GetGood(reward);
								if (good == null)
								{
									continue;
								}
								if (ComDiv.GetIdStatics(good.Item.Id, 1) != 6 || missions.Character.GetCharacter(good.Item.Id) != null)
								{
									missions.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, missions, good.Item));
								}
								else
								{
									AllUtils.CreateCharacter(missions, good.Item);
								}
							}
						}
						missions.Gold += rank.OnGoldUp;
						missions.LastRankUpDate = uint.Parse(dateTime.ToString("yyMMddHHmm"));
						Account account1 = missions;
						int rank1 = account1.Rank + 1;
						account1.Rank = rank1;
						missions.SendPacket(new PROTOCOL_BASE_RANK_UP_ACK(rank1, rank.OnNextLevel));
						dBQuery.AddQuery("last_rank_update", (long)((ulong)missions.LastRankUpDate));
						dBQuery.AddQuery("rank", missions.Rank);
					}
					if (eventPlaytimeModel != null)
					{
						AllUtils.PlayTimeEvent(missions, eventPlaytimeModel, bool_0, slots, (long)num);
					}
					if (activeSeasonPass != null)
					{
						missions.UpdateSeasonpass = true;
						AllUtils.CalculateBattlePass(missions, slots, activeSeasonPass);
					}
					if (this.Competitive)
					{
						AllUtils.CalculateCompetitive(this, missions, slots, teamEnum_0 == slots.Team);
					}
					AllUtils.DiscountPlayerItems(slots, missions);
					if (exp != missions.Exp)
					{
						dBQuery.AddQuery("experience", missions.Exp);
					}
					if (gold != missions.Gold)
					{
						dBQuery.AddQuery("gold", missions.Gold);
					}
					if (cash != missions.Cash)
					{
						dBQuery.AddQuery("cash", missions.Cash);
					}
					ComDiv.UpdateDB("accounts", "player_id", missions.PlayerId, dBQuery.GetTables(), dBQuery.GetValues());
					ComDiv.UpdateDB("player_stat_basics", "owner_id", missions.PlayerId, dBQuery1.GetTables(), dBQuery1.GetValues());
					ComDiv.UpdateDB("player_stat_seasons", "owner_id", missions.PlayerId, dBQuery2.GetTables(), dBQuery2.GetValues());
					ComDiv.UpdateDB("player_stat_dailies", "owner_id", missions.PlayerId, dBQuery3.GetTables(), dBQuery3.GetValues());
					ComDiv.UpdateDB("player_stat_weapons", "owner_id", missions.PlayerId, dBQuery4.GetTables(), dBQuery4.GetValues());
					if (ConfigLoader.WinCashPerBattle && ConfigLoader.ShowCashReceiveWarn)
					{
						missions.SendPacket(new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(Translation.GetLabel("CashReceived", new object[] { slots.Cash })));
					}
					slotModels.Add(slots);
				}
			}
			if (slotModels.Count > 0)
			{
				this.SlotRewards = AllUtils.GetRewardData(this, slotModels);
				this.method_8(slotModels, bool_0);
			}
			this.UpdateSlotsInfo();
			if (this.RoomType != RoomCondition.FreeForAll)
			{
				this.method_14(teamEnum_0);
			}
		}

		private void method_8(List<SlotModel> list_0, bool bool_0)
		{
			Account account;
			SlotModel slotModel = (
				from slotModel_0 in list_0
				orderby slotModel_0.Score descending
				select slotModel_0).FirstOrDefault<SlotModel>();
			if (slotModel != null && slotModel.Check && slotModel.State == SlotState.BATTLE && this.GetPlayerBySlot(slotModel, out account))
			{
				StatisticTotal basic = account.Statistic.Basic;
				StatisticSeason season = account.Statistic.Season;
				if (!bool_0 && basic != null && season != null)
				{
					StatisticTotal mvpCount = basic;
					mvpCount.MvpCount = mvpCount.MvpCount + 1;
					StatisticSeason statisticSeason = season;
					statisticSeason.MvpCount = statisticSeason.MvpCount + 1;
					ComDiv.UpdateDB("player_stat_basics", "mvp_count", basic.MvpCount, "owner_id", account.PlayerId);
					ComDiv.UpdateDB("player_stat_seasons", "mvp_count", season.MvpCount, "owner_id", account.PlayerId);
				}
			}
		}

		private void method_9(Account account_0, StatisticTotal statisticTotal_0, StatisticSeason statisticSeason_0, SlotModel slotModel_0, DBQuery dbquery_0, DBQuery dbquery_1, int int_0, bool bool_0, int int_1)
		{
			StatisticTotal statisticTotal0 = statisticTotal_0;
			statisticTotal0.HeadshotsCount = statisticTotal0.HeadshotsCount + slotModel_0.AllHeadshots;
			StatisticTotal killsCount = statisticTotal_0;
			killsCount.KillsCount = killsCount.KillsCount + slotModel_0.AllKills;
			StatisticTotal totalKillsCount = statisticTotal_0;
			totalKillsCount.TotalKillsCount = totalKillsCount.TotalKillsCount + slotModel_0.AllKills;
			StatisticTotal deathsCount = statisticTotal_0;
			deathsCount.DeathsCount = deathsCount.DeathsCount + slotModel_0.AllDeaths;
			StatisticTotal assistsCount = statisticTotal_0;
			assistsCount.AssistsCount = assistsCount.AssistsCount + slotModel_0.AllAssists;
			StatisticSeason statisticSeason0 = statisticSeason_0;
			statisticSeason0.HeadshotsCount = statisticSeason0.HeadshotsCount + slotModel_0.AllHeadshots;
			StatisticSeason statisticSeason = statisticSeason_0;
			statisticSeason.KillsCount = statisticSeason.KillsCount + slotModel_0.AllKills;
			StatisticSeason statisticSeason01 = statisticSeason_0;
			statisticSeason01.TotalKillsCount = statisticSeason01.TotalKillsCount + slotModel_0.AllKills;
			StatisticSeason deathsCount1 = statisticSeason_0;
			deathsCount1.DeathsCount = deathsCount1.DeathsCount + slotModel_0.AllDeaths;
			StatisticSeason assistsCount1 = statisticSeason_0;
			assistsCount1.AssistsCount = assistsCount1.AssistsCount + slotModel_0.AllAssists;
			this.method_12(slotModel_0, account_0.Statistic, dbquery_0, dbquery_1);
			if (this.RoomType == RoomCondition.FreeForAll)
			{
				AllUtils.UpdateMatchCountFFA(this, account_0, int_0, dbquery_0, dbquery_1);
				return;
			}
			AllUtils.UpdateMatchCount(bool_0, account_0, int_1, dbquery_0, dbquery_1);
		}

		public string RandomName(int Random)
		{
			switch (Random)
			{
				case 1:
				{
					return "Feel the Headshots!!";
				}
				case 2:
				{
					return "Land of Dead!";
				}
				case 3:
				{
					return "Kill! or be Killed!!";
				}
				case 4:
				{
					return "Show Me Your Skills!!";
				}
				default:
				{
					return "Point Blank!!";
				}
			}
		}

		public void RemovePlayer(Account Player, bool WarnAllPlayers, int QuitMotive = 0)
		{
			SlotModel slotModel;
			if (Player == null || !this.GetSlot(Player.SlotId, out slotModel))
			{
				return;
			}
			this.method_16(Player, slotModel, WarnAllPlayers, QuitMotive);
		}

		public void RemovePlayer(Account Player, SlotModel Slot, bool WarnAllPlayers, int QuitMotive = 0)
		{
			if (Player == null || Slot == null)
			{
				return;
			}
			this.method_16(Player, Slot, WarnAllPlayers, QuitMotive);
		}

		public int ResetReadyPlayers()
		{
			int ınt32 = 0;
			SlotModel[] slots = this.Slots;
			for (int i = 0; i < (int)slots.Length; i++)
			{
				SlotModel slotModel = slots[i];
				if (slotModel.State == SlotState.READY)
				{
					slotModel.State = SlotState.NORMAL;
					ınt32++;
				}
			}
			return ınt32;
		}

		public void RoundRestart()
		{
			this.StopBomb();
			SlotModel[] slots = this.Slots;
			for (int i = 0; i < (int)slots.Length; i++)
			{
				SlotModel slotModel = slots[i];
				if (slotModel.PlayerId > 0L && slotModel.State == SlotState.BATTLE)
				{
					if (!slotModel.DeathState.HasFlag(DeadEnum.UseChat))
					{
						slotModel.DeathState |= DeadEnum.UseChat;
					}
					if (slotModel.Spectator)
					{
						slotModel.Spectator = false;
					}
					if (slotModel.KillsOnLife >= 3 && (this.RoomType == RoomCondition.Annihilation || this.RoomType == RoomCondition.Ace))
					{
						slotModel.Objects++;
					}
					slotModel.KillsOnLife = 0;
					slotModel.LastKillState = 0;
					slotModel.RepeatLastState = false;
					slotModel.DamageBar1 = 0;
					slotModel.DamageBar2 = 0;
				}
			}
			this.RoundTime.StartJob(8250, (object object_0) => {
				this.method_4();
				lock (object_0)
				{
					this.RoundTime.StopJob();
				}
			});
		}

		public void SendPacketToPlayers(GameServerPacket Packet)
		{
			List<Account> allPlayers = this.GetAllPlayers();
			if (allPlayers.Count == 0)
			{
				return;
			}
			byte[] completeBytes = Packet.GetCompleteBytes("Room.SendPacketToPlayers(SendPacket)");
			foreach (Account allPlayer in allPlayers)
			{
				allPlayer.SendCompletePacket(completeBytes, Packet.GetType().Name);
			}
		}

		public void SendPacketToPlayers(GameServerPacket Packet, long PlayerId)
		{
			List<Account> allPlayers = this.GetAllPlayers(PlayerId);
			if (allPlayers.Count == 0)
			{
				return;
			}
			byte[] completeBytes = Packet.GetCompleteBytes("Room.SendPacketToPlayers(SendPacket,long)");
			foreach (Account allPlayer in allPlayers)
			{
				allPlayer.SendCompletePacket(completeBytes, Packet.GetType().Name);
			}
		}

		public void SendPacketToPlayers(GameServerPacket Packet, SlotState State, int Type)
		{
			List<Account> allPlayers = this.GetAllPlayers(State, Type);
			if (allPlayers.Count == 0)
			{
				return;
			}
			byte[] completeBytes = Packet.GetCompleteBytes("Room.SendPacketToPlayers(SendPacket,SLOT_STATE,int)");
			foreach (Account allPlayer in allPlayers)
			{
				allPlayer.SendCompletePacket(completeBytes, Packet.GetType().Name);
			}
		}

		public void SendPacketToPlayers(GameServerPacket Packet, GameServerPacket Packet2, SlotState State, int Type)
		{
			List<Account> allPlayers = this.GetAllPlayers(State, Type);
			if (allPlayers.Count == 0)
			{
				return;
			}
			byte[] completeBytes = Packet.GetCompleteBytes("Room.SendPacketToPlayers(SendPacket,SendPacket,SLOT_STATE,int)-1");
			byte[] numArray = Packet2.GetCompleteBytes("Room.SendPacketToPlayers(SendPacket,SendPacket,SLOT_STATE,int)-2");
			foreach (Account allPlayer in allPlayers)
			{
				allPlayer.SendCompletePacket(completeBytes, Packet.GetType().Name);
				allPlayer.SendCompletePacket(numArray, Packet2.GetType().Name);
			}
		}

		public void SendPacketToPlayers(GameServerPacket Packet, SlotState State, int Type, int Exception)
		{
			List<Account> allPlayers = this.GetAllPlayers(State, Type, Exception);
			if (allPlayers.Count == 0)
			{
				return;
			}
			byte[] completeBytes = Packet.GetCompleteBytes("Room.SendPacketToPlayers(SendPacket,SLOT_STATE,int,int)");
			foreach (Account allPlayer in allPlayers)
			{
				allPlayer.SendCompletePacket(completeBytes, Packet.GetType().Name);
			}
		}

		public void SendPacketToPlayers(GameServerPacket Packet, SlotState State, int Type, int Exception, int Exception2)
		{
			List<Account> allPlayers = this.GetAllPlayers(State, Type, Exception, Exception2);
			if (allPlayers.Count == 0)
			{
				return;
			}
			byte[] completeBytes = Packet.GetCompleteBytes("Room.SendPacketToPlayers(SendPacket,SLOT_STATE,int,int,int)");
			foreach (Account allPlayer in allPlayers)
			{
				allPlayer.SendCompletePacket(completeBytes, Packet.GetType().Name);
			}
		}

		public void SetBotLevel()
		{
			if (!this.IsBotMode())
			{
				return;
			}
			this.IngameAiLevel = this.AiLevel;
			for (int i = 0; i < 18; i++)
			{
				this.Slots[i].AiLevel = this.IngameAiLevel;
			}
		}

		public void SetNewLeader(int LeaderSlot, SlotState State, int OldLeader, bool UpdateInfo)
		{
			lock (this.Slots)
			{
				if (LeaderSlot != -1)
				{
					this.LeaderSlot = LeaderSlot;
				}
				else
				{
					SlotModel[] slots = this.Slots;
					int ınt32 = 0;
					while (ınt32 < (int)slots.Length)
					{
						SlotModel slotModel = slots[ınt32];
						if (slotModel.Id == OldLeader || slotModel.PlayerId <= 0L || slotModel.State <= State)
						{
							ınt32++;
						}
						else
						{
							this.LeaderSlot = slotModel.Id;
							goto Label0;
						}
					}
				}
			Label0:
				if (this.LeaderSlot != -1)
				{
					SlotModel slots1 = this.Slots[this.LeaderSlot];
					if (slots1.State == SlotState.READY)
					{
						slots1.State = SlotState.NORMAL;
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
			MapMatch mapLimit = SystemMapXML.GetMapLimit((int)this.MapId, (int)this.Rule);
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
					lock (this.Slots)
					{
						foreach (SlotModel slotModel in ((IEnumerable<SlotModel>)this.Slots).Where<SlotModel>((SlotModel slotModel_0) => {
							if (slotModel_0.Id == 16)
							{
								return false;
							}
							return slotModel_0.Id != 17;
						}))
						{
							if (slotModel.Id < Count)
							{
								continue;
							}
							slotModel.State = SlotState.CLOSE;
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
			bool flag;
			lock (this.Slots)
			{
				if (!this.ThisModeHaveRounds())
				{
					flag = false;
				}
				else
				{
					flag = (this.CountdownIG == 3 || this.CountdownIG == 5 || this.CountdownIG == 7 ? true : this.CountdownIG == 9);
				}
				bool flag1 = flag;
				if ((this.State != RoomState.PRE_BATTLE ? false : !this.PreMatchCD) & flag1 && !this.IsBotMode())
				{
					this.PreMatchCD = true;
					using (PROTOCOL_BATTLE_COUNT_DOWN_ACK pROTOCOLBATTLECOUNTDOWNACK = new PROTOCOL_BATTLE_COUNT_DOWN_ACK((int)this.CountdownIG))
					{
						this.SendPacketToPlayers(pROTOCOLBATTLECOUNTDOWNACK);
					}
				}
				this.PreMatchTime.StartJob((this.CountdownIG == 0 ? 0 : this.CountdownIG * 1000 + 250), (object object_0) => {
					this.method_17();
					lock (object_0)
					{
						this.PreMatchTime.StopJob();
					}
				});
			}
		}

		public void StartBattle(bool UpdateInfo)
		{
			lock (this.Slots)
			{
				this.State = RoomState.LOADING;
				this.RequestRoomMaster.Clear();
				this.SetBotLevel();
				AllUtils.CheckClanMatchRestrict(this);
				this.StartTick = DateTimeUtil.Now().Ticks;
				this.StartDate = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
				using (PROTOCOL_BATTLE_START_GAME_ACK pROTOCOLBATTLESTARTGAMEACK = new PROTOCOL_BATTLE_START_GAME_ACK(this))
				{
					byte[] completeBytes = pROTOCOLBATTLESTARTGAMEACK.GetCompleteBytes("Room.StartBattle");
					foreach (Account allPlayer in this.GetAllPlayers(SlotState.READY, 0))
					{
						SlotModel slot = this.GetSlot(allPlayer.SlotId);
						if (slot == null)
						{
							continue;
						}
						slot.WithHost = true;
						slot.State = SlotState.LOAD;
						slot.SetMissionsClone(allPlayer.Mission);
						allPlayer.SendCompletePacket(completeBytes, pROTOCOLBATTLESTARTGAMEACK.GetType().Name);
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
			this.BombTime.StartJob(42000, (object object_0) => {
				if (this != null && this.ActiveC4)
				{
					if (!this.SwapRound)
					{
						this.FRRounds++;
					}
					else
					{
						this.CTRounds++;
					}
					this.ActiveC4 = false;
					AllUtils.BattleEndRound(this, TeamEnum.FR_TEAM, RoundEndType.BombFire);
				}
				lock (object_0)
				{
					this.BombTime.StopJob();
				}
			});
		}

		public void StartCountDown()
		{
			using (PROTOCOL_BATTLE_START_COUNTDOWN_ACK pROTOCOLBATTLESTARTCOUNTDOWNACK = new PROTOCOL_BATTLE_START_COUNTDOWN_ACK(CountDownEnum.Start))
			{
				this.SendPacketToPlayers(pROTOCOLBATTLESTARTCOUNTDOWNACK);
			}
			this.CountdownTime.StartJob(5250, (object object_0) => {
				this.method_6();
				lock (object_0)
				{
					this.CountdownTime.StopJob();
				}
			});
		}

		public void StartCounter(int Type, Account Player, SlotModel Slot)
		{
			int ınt32 = 0;
			EventErrorEnum eventErrorEnum = EventErrorEnum.SUCCESS;
			if (Type == 0)
			{
				eventErrorEnum = EventErrorEnum.BATTLE_FIRST_MAINLOAD;
				ınt32 = 90000;
			}
			else if (Type == 1)
			{
				eventErrorEnum = EventErrorEnum.BATTLE_FIRST_HOLE;
				ınt32 = 30000;
			}
			if (ınt32 > 0)
			{
				Slot.FirstInactivityOff = true;
			}
			Slot.Timing.StartJob(ınt32, (object object_0) => {
				if (!Slot.FirstInactivityOff && Slot.State < SlotState.BATTLE && Slot.IsPlaying == 0)
				{
					this.method_3(eventErrorEnum, Player, Slot);
				}
				lock (object_0)
				{
					if (Slot != null)
					{
						Slot.StopTiming();
					}
				}
			});
		}

		public void StartVote()
		{
			if (this.VoteKick == null)
			{
				return;
			}
			this.VoteTime.StartJob(20000, (object object_0) => {
				AllUtils.VotekickResult(this);
				lock (object_0)
				{
					this.VoteTime.StopJob();
				}
			});
		}

		public void StopBomb()
		{
			if (!this.ActiveC4)
			{
				return;
			}
			this.ActiveC4 = false;
			if (this.BombTime != null)
			{
				this.BombTime.StopJob();
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
			using (PROTOCOL_BATTLE_START_COUNTDOWN_ACK pROTOCOLBATTLESTARTCOUNTDOWNACK = new PROTOCOL_BATTLE_START_COUNTDOWN_ACK(Type))
			{
				this.SendPacketToPlayers(pROTOCOLBATTLESTARTCOUNTDOWNACK);
			}
		}

		public void StopCountDown(int SlotId)
		{
			if (this.State != RoomState.COUNTDOWN)
			{
				return;
			}
			if (SlotId == this.LeaderSlot)
			{
				this.StopCountDown(CountDownEnum.StopByHost, true);
				return;
			}
			if (this.GetPlayingPlayers((TeamEnum)(this.LeaderSlot % 2 == 0), SlotState.READY, 0) == 0)
			{
				this.ChangeSlotState(this.LeaderSlot, SlotState.NORMAL, false);
				this.StopCountDown(CountDownEnum.StopByPlayer, true);
			}
		}

		public void SwitchSlots(List<SlotChange> SlotChanges, Account Player, SlotModel OldSlot, SlotModel NewSlot, SlotState State = 9)
		{
			if (Player == null || OldSlot == null || NewSlot == null)
			{
				return;
			}
			NewSlot.ResetSlot();
			NewSlot.State = SlotState.NORMAL;
			NewSlot.PlayerId = Player.PlayerId;
			NewSlot.Equipment = Player.Equipment;
			if (NewSlot.Id == 16 || NewSlot.Id == 17)
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

		public void SwitchSlots(List<SlotChange> SlotChanges, int NewSlotId, int OldSlotId, bool ChangeReady)
		{
			SlotModel slots = this.Slots[NewSlotId];
			SlotModel slotModel = this.Slots[OldSlotId];
			if (ChangeReady)
			{
				if (slots.State == SlotState.READY)
				{
					slots.State = SlotState.NORMAL;
				}
				if (slotModel.State == SlotState.READY)
				{
					slotModel.State = SlotState.NORMAL;
				}
			}
			slots.SetSlotId(OldSlotId);
			slotModel.SetSlotId(NewSlotId);
			this.Slots[NewSlotId] = slotModel;
			this.Slots[OldSlotId] = slots;
			SlotChanges.Add(new SlotChange(slots, slotModel));
		}

		public bool ThisModeHaveCD()
		{
			if (this.RoomType == RoomCondition.Bomb || this.RoomType == RoomCondition.Annihilation || this.RoomType == RoomCondition.Boss || this.RoomType == RoomCondition.CrossCounter || this.RoomType == RoomCondition.Destroy || this.RoomType == RoomCondition.Ace || this.RoomType == RoomCondition.Escape)
			{
				return true;
			}
			return this.RoomType == RoomCondition.Glass;
		}

		public bool ThisModeHaveRounds()
		{
			if (this.RoomType == RoomCondition.Bomb || this.RoomType == RoomCondition.Destroy || this.RoomType == RoomCondition.Annihilation || this.RoomType == RoomCondition.Defense || this.RoomType == RoomCondition.Destroy || this.RoomType == RoomCondition.Ace || this.RoomType == RoomCondition.Escape)
			{
				return true;
			}
			return this.RoomType == RoomCondition.Glass;
		}

		public void UpdateRoomInfo()
		{
			this.GenerateSeed();
			using (PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK pROTOCOLROOMCHANGEROOMINFOACK = new PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK(this))
			{
				this.SendPacketToPlayers(pROTOCOLROOMCHANGEROOMINFOACK);
			}
		}

		public void UpdateSlotsInfo()
		{
			using (PROTOCOL_ROOM_GET_SLOTINFO_ACK pROTOCOLROOMGETSLOTINFOACK = new PROTOCOL_ROOM_GET_SLOTINFO_ACK(this))
			{
				this.SendPacketToPlayers(pROTOCOLROOMGETSLOTINFOACK);
			}
		}

		public TeamEnum ValidateTeam(TeamEnum Team, TeamEnum Costume)
		{
			if (this.RoomType == RoomCondition.FreeForAll)
			{
				return Costume;
			}
			if (!this.SwapRound)
			{
				return Team;
			}
			if (Team != TeamEnum.FR_TEAM)
			{
				return TeamEnum.FR_TEAM;
			}
			return TeamEnum.CT_TEAM;
		}
	}
}