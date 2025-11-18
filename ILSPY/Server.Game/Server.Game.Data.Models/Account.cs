using System;
using System.Collections.Generic;
using System.Net;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Managers;
using Server.Game.Data.Utils;
using Server.Game.Data.XML;
using Server.Game.Network;

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

	public bool UpdateSeasonpass = true;

	public CouponEffects Effects;

	public AccessLevel Access;

	public CafeEnum CafePC;

	public IPAddress PublicIP;

	public GameClient Connection;

	public RoomModel Room;

	public PlayerSession Session;

	public MatchModel Match;

	public PlayerConfig Config = new PlayerConfig();

	public PlayerBonus Bonus = new PlayerBonus();

	public PlayerEvent Event = new PlayerEvent();

	public PlayerTitles Title = new PlayerTitles();

	public PlayerInventory Inventory = new PlayerInventory();

	public PlayerStatistic Statistic = new PlayerStatistic();

	public PlayerCharacters Character = new PlayerCharacters();

	public PlayerEquipment Equipment = new PlayerEquipment();

	public PlayerFriends Friend = new PlayerFriends();

	public PlayerQuickstart Quickstart = new PlayerQuickstart();

	public PlayerMissions Mission = new PlayerMissions();

	public PlayerReport Report = new PlayerReport();

	public PlayerBattlepass Battlepass = new PlayerBattlepass();

	public PlayerCompetitive Competitive = new PlayerCompetitive();

	public AccountStatus Status = new AccountStatus();

	public List<PlayerTopup> TopUps = new List<PlayerTopup>();

	public DateTime LastLobbyEnter;

	public DateTime LastPingDebug;

	public Account()
	{
		LastLobbyEnter = DateTimeUtil.Now();
		Nickname = "";
		Password = "";
		Username = "";
		HardwareId = "";
		Email = "";
		FindPlayer = "";
		ServerId = -1;
		ChannelId = -1;
		SlotId = -1;
		MatchSlot = -1;
	}

	public void SimpleClear()
	{
		Title = new PlayerTitles();
		Equipment = new PlayerEquipment();
		Inventory = new PlayerInventory();
		Status = new AccountStatus();
		Character = new PlayerCharacters();
		Statistic = new PlayerStatistic();
		Quickstart = new PlayerQuickstart();
		Battlepass = new PlayerBattlepass();
		Competitive = new PlayerCompetitive();
		Report = new PlayerReport();
		Mission = new PlayerMissions();
		Bonus = new PlayerBonus();
		Event = new PlayerEvent();
		Config = new PlayerConfig();
		TopUps.Clear();
		Friend.CleanList();
		Session = null;
		Match = null;
		Room = null;
		Connection = null;
	}

	public void SetPublicIP(IPAddress address)
	{
		if (address == null)
		{
			PublicIP = new IPAddress(new byte[4]);
		}
		PublicIP = address;
	}

	public void SetPublicIP(string address)
	{
		PublicIP = IPAddress.Parse(address);
	}

	public ChannelModel GetChannel()
	{
		return ChannelsXML.GetChannel(ServerId, ChannelId);
	}

	public void ResetPages()
	{
		LastRoomPage = 0;
		LastPlayerPage = 0;
	}

	public bool GetChannel(out ChannelModel Channel)
	{
		Channel = ChannelsXML.GetChannel(ServerId, ChannelId);
		return Channel != null;
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

	public void Close(int time, bool kicked = false)
	{
		if (Connection != null)
		{
			Connection.Close(time, DestroyConnection: true, kicked);
		}
	}

	public void SendPacket(GameServerPacket Packet)
	{
		if (Connection != null)
		{
			Connection.SendPacket(Packet);
		}
	}

	public void SendPacket(GameServerPacket Packet, bool OnlyInServer)
	{
		if (Connection != null)
		{
			Connection.SendPacket(Packet);
		}
		else if (!OnlyInServer && Status.ServerId != byte.MaxValue && Status.ServerId != ServerId)
		{
			GameXender.Sync.SendBytes(PlayerId, Packet, Status.ServerId);
		}
	}

	public void SendPacket(byte[] Data, string PacketName)
	{
		if (Connection != null)
		{
			Connection.SendPacket(Data, PacketName);
		}
	}

	public void SendPacket(byte[] Data, string PacketName, bool OnlyInServer)
	{
		if (Connection != null)
		{
			Connection.SendPacket(Data, PacketName);
		}
		else if (!OnlyInServer && Status.ServerId != byte.MaxValue && Status.ServerId != ServerId)
		{
			GameXender.Sync.SendBytes(PlayerId, PacketName, Data, Status.ServerId);
		}
	}

	public void SendCompletePacket(byte[] Data, string PacketName)
	{
		if (Connection != null)
		{
			Connection.SendCompletePacket(Data, PacketName);
		}
	}

	public void SendCompletePacket(byte[] Data, string PacketName, bool OnlyInServer)
	{
		if (Connection != null)
		{
			Connection.SendCompletePacket(Data, PacketName);
		}
		else if (!OnlyInServer && Status.ServerId != byte.MaxValue && Status.ServerId != ServerId)
		{
			GameXender.Sync.SendCompleteBytes(PlayerId, PacketName, Data, Status.ServerId);
		}
	}

	public long StatusId()
	{
		return (!string.IsNullOrEmpty(Nickname)) ? 1 : 0;
	}

	public int GetSessionId()
	{
		if (Session == null)
		{
			return 0;
		}
		return Session.SessionId;
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

	public bool UseChatGM()
	{
		if (!HideGMcolor)
		{
			if (Rank != 53)
			{
				return Rank == 54;
			}
			return true;
		}
		return false;
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
