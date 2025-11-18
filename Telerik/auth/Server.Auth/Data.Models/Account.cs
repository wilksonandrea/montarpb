using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Auth;
using Server.Auth.Data.Managers;
using Server.Auth.Data.Utils;
using Server.Auth.Network;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace Server.Auth.Data.Models
{
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

		public PlayerBonus Bonus = new PlayerBonus();

		public PlayerConfig Config = new PlayerConfig();

		public PlayerEvent Event = new PlayerEvent();

		public PlayerTitles Title = new PlayerTitles();

		public PlayerInventory Inventory = new PlayerInventory();

		public AccountStatus Status = new AccountStatus();

		public PlayerFriends Friend = new PlayerFriends();

		public PlayerStatistic Statistic = new PlayerStatistic();

		public PlayerQuickstart Quickstart = new PlayerQuickstart();

		public PlayerCharacters Character = new PlayerCharacters();

		public PlayerEquipment Equipment = new PlayerEquipment();

		public PlayerMissions Mission = new PlayerMissions();

		public PlayerReport Report = new PlayerReport();

		public PlayerBattlepass Battlepass = new PlayerBattlepass();

		public PlayerCompetitive Competitive = new PlayerCompetitive();

		public List<Account> ClanPlayers = new List<Account>();

		public List<PlayerTopup> TopUps = new List<PlayerTopup>();

		public DateTime LastLoginDate;

		public Account()
		{
			this.LastLoginDate = DateTimeUtil.Now();
			this.Nickname = "";
			this.Password = "";
			this.Username = "";
			this.HardwareId = "";
			this.Email = "";
		}

		public AccessLevel AuthLevel()
		{
			int rank = this.Rank;
			if (rank == 53)
			{
				return AccessLevel.GAMEMASTER;
			}
			if (rank != 54)
			{
				return AccessLevel.NORMAL;
			}
			return AccessLevel.MODERATOR;
		}

		public void Close(int time)
		{
			if (this.Connection != null)
			{
				this.Connection.Close(time, true);
			}
		}

		public bool ComparePassword(string Password)
		{
			if (ConfigLoader.IsTestMode)
			{
				return true;
			}
			return this.Password == Password;
		}

		public float ExpUp()
		{
			PCCafeModel pCCafe = TemplatePackXML.GetPCCafe(this.CafePC);
			if (pCCafe == null)
			{
				return 0f;
			}
			return (float)(pCCafe.ExpUp / 100);
		}

		public void GetAccountInfos(int LoadType)
		{
			if (LoadType > 0 && this.PlayerId > 0L)
			{
				if ((LoadType & 1) == 1)
				{
					AllUtils.LoadPlayerEquipments(this);
				}
				if ((LoadType & 2) == 2)
				{
					AllUtils.LoadPlayerCharacters(this);
				}
				if ((LoadType & 4) == 4)
				{
					AllUtils.LoadPlayerStatistic(this);
				}
				if ((LoadType & 8) == 8)
				{
					AllUtils.LoadPlayerTitles(this);
				}
				if ((LoadType & 16) == 16)
				{
					AllUtils.LoadPlayerBonus(this);
				}
				if ((LoadType & 32) == 32)
				{
					AllUtils.LoadPlayerFriend(this, true);
				}
				if ((LoadType & 64) == 64)
				{
					AllUtils.LoadPlayerEvent(this);
				}
				if ((LoadType & 128) == 128)
				{
					AllUtils.LoadPlayerConfig(this);
				}
				if ((LoadType & 256) == 256)
				{
					AllUtils.LoadPlayerFriend(this, false);
				}
				if ((LoadType & 512) == 512)
				{
					AllUtils.LoadPlayerQuickstarts(this);
				}
				if ((LoadType & 1024) == 1024)
				{
					AllUtils.LoadPlayerReport(this);
				}
				if ((LoadType & 2048) == 2048)
				{
					AllUtils.LoadPlayerBattlepass(this);
				}
				if ((LoadType & 4096) == 4096)
				{
					AllUtils.LoadPlayerCompetitive(this);
				}
			}
		}

		public int GetRank()
		{
			if (this.Bonus == null || this.Bonus.FakeRank == 55)
			{
				return this.Rank;
			}
			return this.Bonus.FakeRank;
		}

		public bool HavePermission(string Permission)
		{
			return PermissionXML.HavePermission(Permission, this.Access);
		}

		public bool IsBanned()
		{
			int ınt32 = ComDiv.CountDB(string.Format("SELECT * FROM base_auto_ban WHERE owner_id = '{0}'", this.PlayerId));
			int ınt321 = ComDiv.CountDB(string.Format("SELECT * FROM base_ban_history WHERE owner_id = '{0}'", this.PlayerId));
			if (ınt32 > 0)
			{
				return true;
			}
			return ınt321 > 0;
		}

		public bool IsGM()
		{
			return this.AuthLevel() > AccessLevel.NORMAL;
		}

		public float PointUp()
		{
			PCCafeModel pCCafe = TemplatePackXML.GetPCCafe(this.CafePC);
			if (pCCafe == null)
			{
				return 0f;
			}
			return (float)(pCCafe.PointUp / 100);
		}

		public void SendCompletePacket(byte[] Data, string PacketName)
		{
			if (this.Connection != null)
			{
				this.Connection.SendCompletePacket(Data, PacketName);
			}
		}

		public void SendPacket(AuthServerPacket Packet)
		{
			if (this.Connection != null)
			{
				this.Connection.SendPacket(Packet);
			}
		}

		public void SendPacket(byte[] Data, string PacketName)
		{
			if (this.Connection != null)
			{
				this.Connection.SendPacket(Data, PacketName);
			}
		}

		public void SetCountry(string ipAddress)
		{
			if (string.IsNullOrEmpty(ipAddress))
			{
				return;
			}
			CountryFlags countryByIp = CountryDetector.GetCountryByIp(ipAddress);
			this.Country = countryByIp;
			ComDiv.UpdateDB("accounts", "country_flag", (int)countryByIp, "player_id", this.PlayerId);
			if (countryByIp != CountryFlags.None)
			{
				CLogger.Print(string.Format("Connect {0} from: {1}", this.Username, countryByIp), LoggerType.Info, null);
			}
		}

		public void SetOnlineStatus(bool Online)
		{
			if (this.IsOnline != Online && ComDiv.UpdateDB("accounts", "online", Online, "player_id", this.PlayerId))
			{
				this.IsOnline = Online;
				CLogger.Print(string.Format("Account User: {0}, Player UID: {1}, Is {2}", this.Username, this.PlayerId, (this.IsOnline ? "Connected" : "Disconnected")), LoggerType.Info, null);
			}
		}

		public void SetPlayerId(long PlayerId, int LoadType)
		{
			this.PlayerId = PlayerId;
			this.GetAccountInfos(LoadType);
		}

		public void SimpleClear()
		{
			this.Connection = null;
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

		public long StatusId()
		{
			return (long)(!string.IsNullOrEmpty(this.Nickname));
		}

		public int TourneyLevel()
		{
			CompetitiveRank rank = CompetitiveXML.GetRank(this.Competitive.Level);
			if (rank == null)
			{
				return 0;
			}
			return rank.TourneyLevel;
		}

		public void UpdateCacheInfo()
		{
			if (this.PlayerId == 0)
			{
				return;
			}
			lock (AccountManager.Accounts)
			{
				AccountManager.Accounts[this.PlayerId] = this;
			}
		}
	}
}