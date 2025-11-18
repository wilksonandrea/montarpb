using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game;
using Server.Game.Data.Managers;
using Server.Game.Data.Sync;
using Server.Game.Data.Utils;
using Server.Game.Data.XML;
using Server.Game.Network;
using System;
using System.Collections.Generic;
using System.Net;

namespace Server.Game.Data.Models
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

		public void Close(int time, bool kicked = false)
		{
			if (this.Connection != null)
			{
				this.Connection.Close(time, true, kicked);
			}
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

		public ChannelModel GetChannel()
		{
			return ChannelsXML.GetChannel(this.ServerId, this.ChannelId);
		}

		public bool GetChannel(out ChannelModel Channel)
		{
			Channel = ChannelsXML.GetChannel(this.ServerId, this.ChannelId);
			return Channel != null;
		}

		public int GetRank()
		{
			if (this.Bonus == null || this.Bonus.FakeRank == 55)
			{
				return this.Rank;
			}
			return this.Bonus.FakeRank;
		}

		public int GetSessionId()
		{
			if (this.Session == null)
			{
				return 0;
			}
			return this.Session.SessionId;
		}

		public bool HavePermission(string Permission)
		{
			return PermissionXML.HavePermission(Permission, this.Access);
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

		public void ResetPages()
		{
			this.LastRoomPage = 0;
			this.LastPlayerPage = 0;
		}

		public void SendCompletePacket(byte[] Data, string PacketName)
		{
			if (this.Connection != null)
			{
				this.Connection.SendCompletePacket(Data, PacketName);
			}
		}

		public void SendCompletePacket(byte[] Data, string PacketName, bool OnlyInServer)
		{
			if (this.Connection != null)
			{
				this.Connection.SendCompletePacket(Data, PacketName);
				return;
			}
			if (!OnlyInServer && this.Status.ServerId != 255 && this.Status.ServerId != this.ServerId)
			{
				GameXender.Sync.SendCompleteBytes(this.PlayerId, PacketName, Data, (int)this.Status.ServerId);
			}
		}

		public void SendPacket(GameServerPacket Packet)
		{
			if (this.Connection != null)
			{
				this.Connection.SendPacket(Packet);
			}
		}

		public void SendPacket(GameServerPacket Packet, bool OnlyInServer)
		{
			if (this.Connection != null)
			{
				this.Connection.SendPacket(Packet);
				return;
			}
			if (!OnlyInServer && this.Status.ServerId != 255 && this.Status.ServerId != this.ServerId)
			{
				GameXender.Sync.SendBytes(this.PlayerId, Packet, (int)this.Status.ServerId);
			}
		}

		public void SendPacket(byte[] Data, string PacketName)
		{
			if (this.Connection != null)
			{
				this.Connection.SendPacket(Data, PacketName);
			}
		}

		public void SendPacket(byte[] Data, string PacketName, bool OnlyInServer)
		{
			if (this.Connection != null)
			{
				this.Connection.SendPacket(Data, PacketName);
				return;
			}
			if (!OnlyInServer && this.Status.ServerId != 255 && this.Status.ServerId != this.ServerId)
			{
				GameXender.Sync.SendBytes(this.PlayerId, PacketName, Data, (int)this.Status.ServerId);
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

		public void SetPublicIP(IPAddress address)
		{
			if (address == null)
			{
				this.PublicIP = new IPAddress(new byte[4]);
			}
			this.PublicIP = address;
		}

		public void SetPublicIP(string address)
		{
			this.PublicIP = IPAddress.Parse(address);
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
			this.Session = null;
			this.Match = null;
			this.Room = null;
			this.Connection = null;
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

		public bool UseChatGM()
		{
			if (this.HideGMcolor)
			{
				return false;
			}
			if (this.Rank == 53)
			{
				return true;
			}
			return this.Rank == 54;
		}
	}
}