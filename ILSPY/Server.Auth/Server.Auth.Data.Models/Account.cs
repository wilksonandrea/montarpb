using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Auth.Data.Managers;
using Server.Auth.Data.Utils;
using Server.Auth.Network;

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
		LastLoginDate = DateTimeUtil.Now();
		Nickname = "";
		Password = "";
		Username = "";
		HardwareId = "";
		Email = "";
	}

	public void SimpleClear()
	{
		Connection = null;
		Config = new PlayerConfig();
		Bonus = new PlayerBonus();
		Event = new PlayerEvent();
		Title = new PlayerTitles();
		Inventory = new PlayerInventory();
		Statistic = new PlayerStatistic();
		Character = new PlayerCharacters();
		Equipment = new PlayerEquipment();
		Friend = new PlayerFriends();
		Status = new AccountStatus();
		Mission = new PlayerMissions();
		Quickstart = new PlayerQuickstart();
		Report = new PlayerReport();
		Battlepass = new PlayerBattlepass();
		Competitive = new PlayerCompetitive();
		ClanPlayers = new List<Account>();
		TopUps = new List<PlayerTopup>();
	}

	public void SetOnlineStatus(bool Online)
	{
		if (IsOnline != Online && ComDiv.UpdateDB("accounts", "online", Online, "player_id", PlayerId))
		{
			IsOnline = Online;
			CLogger.Print(string.Format("Account User: {0}, Player UID: {1}, Is {2}", Username, PlayerId, IsOnline ? "Connected" : "Disconnected"), LoggerType.Info);
		}
	}

	public void UpdateCacheInfo()
	{
		if (PlayerId == 0L)
		{
			return;
		}
		lock (AccountManager.Accounts)
		{
			AccountManager.Accounts[PlayerId] = this;
		}
	}

	public void Close(int time)
	{
		if (Connection != null)
		{
			Connection.Close(time, DestroyConnection: true);
		}
	}

	public void SendPacket(AuthServerPacket Packet)
	{
		if (Connection != null)
		{
			Connection.SendPacket(Packet);
		}
	}

	public void SendPacket(byte[] Data, string PacketName)
	{
		if (Connection != null)
		{
			Connection.SendPacket(Data, PacketName);
		}
	}

	public void SendCompletePacket(byte[] Data, string PacketName)
	{
		if (Connection != null)
		{
			Connection.SendCompletePacket(Data, PacketName);
		}
	}

	public long StatusId()
	{
		return (!string.IsNullOrEmpty(Nickname)) ? 1 : 0;
	}

	public bool ComparePassword(string Password)
	{
		if (!ConfigLoader.IsTestMode)
		{
			return this.Password == Password;
		}
		return true;
	}

	public void SetPlayerId(long PlayerId, int LoadType)
	{
		this.PlayerId = PlayerId;
		GetAccountInfos(LoadType);
	}

	public void GetAccountInfos(int LoadType)
	{
		if (LoadType > 0 && PlayerId > 0L)
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
			if ((LoadType & 0x10) == 16)
			{
				AllUtils.LoadPlayerBonus(this);
			}
			if ((LoadType & 0x20) == 32)
			{
				AllUtils.LoadPlayerFriend(this, LoadFulLDatabase: true);
			}
			if ((LoadType & 0x40) == 64)
			{
				AllUtils.LoadPlayerEvent(this);
			}
			if ((LoadType & 0x80) == 128)
			{
				AllUtils.LoadPlayerConfig(this);
			}
			if ((LoadType & 0x100) == 256)
			{
				AllUtils.LoadPlayerFriend(this, LoadFulLDatabase: false);
			}
			if ((LoadType & 0x200) == 512)
			{
				AllUtils.LoadPlayerQuickstarts(this);
			}
			if ((LoadType & 0x400) == 1024)
			{
				AllUtils.LoadPlayerReport(this);
			}
			if ((LoadType & 0x800) == 2048)
			{
				AllUtils.LoadPlayerBattlepass(this);
			}
			if ((LoadType & 0x1000) == 4096)
			{
				AllUtils.LoadPlayerCompetitive(this);
			}
		}
	}

	public int GetRank()
	{
		if (Bonus != null && Bonus.FakeRank != 55)
		{
			return Bonus.FakeRank;
		}
		return Rank;
	}

	public AccessLevel AuthLevel()
	{
		return Rank switch
		{
			54 => AccessLevel.MODERATOR, 
			53 => AccessLevel.GAMEMASTER, 
			_ => AccessLevel.NORMAL, 
		};
	}

	public bool IsGM()
	{
		return AuthLevel() > AccessLevel.NORMAL;
	}

	public bool HavePermission(string Permission)
	{
		return PermissionXML.HavePermission(Permission, Access);
	}

	public float PointUp()
	{
		PCCafeModel pCCafe = TemplatePackXML.GetPCCafe(CafePC);
		if (pCCafe != null)
		{
			return pCCafe.PointUp / 100;
		}
		return 0f;
	}

	public float ExpUp()
	{
		PCCafeModel pCCafe = TemplatePackXML.GetPCCafe(CafePC);
		if (pCCafe != null)
		{
			return pCCafe.ExpUp / 100;
		}
		return 0f;
	}

	public int TourneyLevel()
	{
		return CompetitiveXML.GetRank(Competitive.Level)?.TourneyLevel ?? 0;
	}

	public bool IsBanned()
	{
		int num = ComDiv.CountDB($"SELECT * FROM base_auto_ban WHERE owner_id = '{PlayerId}'");
		int num2 = ComDiv.CountDB($"SELECT * FROM base_ban_history WHERE owner_id = '{PlayerId}'");
		if (num <= 0)
		{
			return num2 > 0;
		}
		return true;
	}

	public void SetCountry(string ipAddress)
	{
		if (!string.IsNullOrEmpty(ipAddress))
		{
			CountryFlags countryFlags = (Country = CountryDetector.GetCountryByIp(ipAddress));
			ComDiv.UpdateDB("accounts", "country_flag", (int)countryFlags, "player_id", PlayerId);
			if (countryFlags != 0)
			{
				CLogger.Print($"Connect {Username} from: {countryFlags}", LoggerType.Info);
			}
		}
	}
}
