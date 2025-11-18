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

namespace Server.Game.Data.Models
{
	// Token: 0x02000201 RID: 513
	public class Account
	{
		// Token: 0x060005F9 RID: 1529 RVA: 0x00031244 File Offset: 0x0002F444
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

		// Token: 0x060005FA RID: 1530 RVA: 0x00031378 File Offset: 0x0002F578
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

		// Token: 0x060005FB RID: 1531 RVA: 0x00005BBB File Offset: 0x00003DBB
		public void SetPublicIP(IPAddress address)
		{
			if (address == null)
			{
				this.PublicIP = new IPAddress(new byte[4]);
			}
			this.PublicIP = address;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00005BD8 File Offset: 0x00003DD8
		public void SetPublicIP(string address)
		{
			this.PublicIP = IPAddress.Parse(address);
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00005BE6 File Offset: 0x00003DE6
		public ChannelModel GetChannel()
		{
			return ChannelsXML.GetChannel(this.ServerId, this.ChannelId);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00005BF9 File Offset: 0x00003DF9
		public void ResetPages()
		{
			this.LastRoomPage = 0;
			this.LastPlayerPage = 0;
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00005C09 File Offset: 0x00003E09
		public bool GetChannel(out ChannelModel Channel)
		{
			Channel = ChannelsXML.GetChannel(this.ServerId, this.ChannelId);
			return Channel != null;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00031454 File Offset: 0x0002F654
		public void SetOnlineStatus(bool Online)
		{
			if (this.IsOnline != Online && ComDiv.UpdateDB("accounts", "online", Online, "player_id", this.PlayerId))
			{
				this.IsOnline = Online;
				CLogger.Print(string.Format("Account User: {0}, Player UID: {1}, Is {2}", this.Username, this.PlayerId, this.IsOnline ? "Connected" : "Disconnected"), LoggerType.Info, null);
			}
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x000314D0 File Offset: 0x0002F6D0
		public void UpdateCacheInfo()
		{
			if (this.PlayerId == 0L)
			{
				return;
			}
			SortedList<long, Account> accounts = AccountManager.Accounts;
			lock (accounts)
			{
				AccountManager.Accounts[this.PlayerId] = this;
			}
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00005C23 File Offset: 0x00003E23
		public void Close(int time, bool kicked = false)
		{
			if (this.Connection != null)
			{
				this.Connection.Close(time, true, kicked);
			}
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00005C3B File Offset: 0x00003E3B
		public void SendPacket(GameServerPacket Packet)
		{
			if (this.Connection != null)
			{
				this.Connection.SendPacket(Packet);
			}
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00031524 File Offset: 0x0002F724
		public void SendPacket(GameServerPacket Packet, bool OnlyInServer)
		{
			if (this.Connection != null)
			{
				this.Connection.SendPacket(Packet);
				return;
			}
			if (!OnlyInServer && this.Status.ServerId != 255 && (int)this.Status.ServerId != this.ServerId)
			{
				GameXender.Sync.SendBytes(this.PlayerId, Packet, (int)this.Status.ServerId);
			}
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00005C51 File Offset: 0x00003E51
		public void SendPacket(byte[] Data, string PacketName)
		{
			if (this.Connection != null)
			{
				this.Connection.SendPacket(Data, PacketName);
			}
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0003158C File Offset: 0x0002F78C
		public void SendPacket(byte[] Data, string PacketName, bool OnlyInServer)
		{
			if (this.Connection != null)
			{
				this.Connection.SendPacket(Data, PacketName);
				return;
			}
			if (!OnlyInServer && this.Status.ServerId != 255 && (int)this.Status.ServerId != this.ServerId)
			{
				GameXender.Sync.SendBytes(this.PlayerId, PacketName, Data, (int)this.Status.ServerId);
			}
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00005C68 File Offset: 0x00003E68
		public void SendCompletePacket(byte[] Data, string PacketName)
		{
			if (this.Connection != null)
			{
				this.Connection.SendCompletePacket(Data, PacketName);
			}
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x000315F4 File Offset: 0x0002F7F4
		public void SendCompletePacket(byte[] Data, string PacketName, bool OnlyInServer)
		{
			if (this.Connection != null)
			{
				this.Connection.SendCompletePacket(Data, PacketName);
				return;
			}
			if (!OnlyInServer && this.Status.ServerId != 255 && (int)this.Status.ServerId != this.ServerId)
			{
				GameXender.Sync.SendCompleteBytes(this.PlayerId, PacketName, Data, (int)this.Status.ServerId);
			}
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00005C7F File Offset: 0x00003E7F
		public long StatusId()
		{
			return (!string.IsNullOrEmpty(this.Nickname)) ? 1L : 0L;
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00005C90 File Offset: 0x00003E90
		public int GetSessionId()
		{
			if (this.Session == null)
			{
				return 0;
			}
			return this.Session.SessionId;
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00005CA7 File Offset: 0x00003EA7
		public void SetPlayerId(long PlayerId, int LoadType)
		{
			this.PlayerId = PlayerId;
			this.GetAccountInfos(LoadType);
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0003165C File Offset: 0x0002F85C
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

		// Token: 0x0600060D RID: 1549 RVA: 0x00005CB7 File Offset: 0x00003EB7
		public int GetRank()
		{
			if (this.Bonus != null && this.Bonus.FakeRank != 55)
			{
				return this.Bonus.FakeRank;
			}
			return this.Rank;
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00005CE2 File Offset: 0x00003EE2
		public bool UseChatGM()
		{
			return !this.HideGMcolor && (this.Rank == 53 || this.Rank == 54);
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00031758 File Offset: 0x0002F958
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

		// Token: 0x06000610 RID: 1552 RVA: 0x00005D04 File Offset: 0x00003F04
		public bool IsGM()
		{
			return this.AuthLevel() > AccessLevel.NORMAL;
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00005D0F File Offset: 0x00003F0F
		public bool HavePermission(string Permission)
		{
			return PermissionXML.HavePermission(Permission, this.Access);
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00031780 File Offset: 0x0002F980
		public float PointUp()
		{
			PCCafeModel pccafe = TemplatePackXML.GetPCCafe(this.CafePC);
			if (pccafe != null)
			{
				return (float)(pccafe.PointUp / 100);
			}
			return 0f;
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x000317AC File Offset: 0x0002F9AC
		public float ExpUp()
		{
			PCCafeModel pccafe = TemplatePackXML.GetPCCafe(this.CafePC);
			if (pccafe != null)
			{
				return (float)(pccafe.ExpUp / 100);
			}
			return 0f;
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x000317D8 File Offset: 0x0002F9D8
		public int TourneyLevel()
		{
			CompetitiveRank rank = CompetitiveXML.GetRank(this.Competitive.Level);
			if (rank != null)
			{
				return rank.TourneyLevel;
			}
			return 0;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00031804 File Offset: 0x0002FA04
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

		// Token: 0x0400039C RID: 924
		public CountryFlags Country;

		// Token: 0x0400039D RID: 925
		public long PlayerId;

		// Token: 0x0400039E RID: 926
		public long BanObjectId;

		// Token: 0x0400039F RID: 927
		public string Nickname;

		// Token: 0x040003A0 RID: 928
		public string Password;

		// Token: 0x040003A1 RID: 929
		public string Username;

		// Token: 0x040003A2 RID: 930
		public string HardwareId;

		// Token: 0x040003A3 RID: 931
		public string Email;

		// Token: 0x040003A4 RID: 932
		public string FindPlayer;

		// Token: 0x040003A5 RID: 933
		public uint LastRankUpDate;

		// Token: 0x040003A6 RID: 934
		public uint ClanDate;

		// Token: 0x040003A7 RID: 935
		public int InventoryPlus;

		// Token: 0x040003A8 RID: 936
		public int Sight;

		// Token: 0x040003A9 RID: 937
		public int LastRoomPage;

		// Token: 0x040003AA RID: 938
		public int LastPlayerPage;

		// Token: 0x040003AB RID: 939
		public int ServerId;

		// Token: 0x040003AC RID: 940
		public int ChannelId;

		// Token: 0x040003AD RID: 941
		public int ClanAccess;

		// Token: 0x040003AE RID: 942
		public int Exp;

		// Token: 0x040003AF RID: 943
		public int Gold;

		// Token: 0x040003B0 RID: 944
		public int Cash;

		// Token: 0x040003B1 RID: 945
		public int ClanId;

		// Token: 0x040003B2 RID: 946
		public int SlotId;

		// Token: 0x040003B3 RID: 947
		public int NickColor;

		// Token: 0x040003B4 RID: 948
		public int Rank;

		// Token: 0x040003B5 RID: 949
		public int Ribbon;

		// Token: 0x040003B6 RID: 950
		public int Ensign;

		// Token: 0x040003B7 RID: 951
		public int Medal;

		// Token: 0x040003B8 RID: 952
		public int MasterMedal;

		// Token: 0x040003B9 RID: 953
		public int MatchSlot;

		// Token: 0x040003BA RID: 954
		public int Age;

		// Token: 0x040003BB RID: 955
		public int Tags;

		// Token: 0x040003BC RID: 956
		public int FindClanId;

		// Token: 0x040003BD RID: 957
		public bool IsOnline;

		// Token: 0x040003BE RID: 958
		public bool HideGMcolor;

		// Token: 0x040003BF RID: 959
		public bool AntiKickGM;

		// Token: 0x040003C0 RID: 960
		public bool LoadedShop;

		// Token: 0x040003C1 RID: 961
		public bool UpdateSeasonpass = true;

		// Token: 0x040003C2 RID: 962
		public CouponEffects Effects;

		// Token: 0x040003C3 RID: 963
		public AccessLevel Access;

		// Token: 0x040003C4 RID: 964
		public CafeEnum CafePC;

		// Token: 0x040003C5 RID: 965
		public IPAddress PublicIP;

		// Token: 0x040003C6 RID: 966
		public GameClient Connection;

		// Token: 0x040003C7 RID: 967
		public RoomModel Room;

		// Token: 0x040003C8 RID: 968
		public PlayerSession Session;

		// Token: 0x040003C9 RID: 969
		public MatchModel Match;

		// Token: 0x040003CA RID: 970
		public PlayerConfig Config = new PlayerConfig();

		// Token: 0x040003CB RID: 971
		public PlayerBonus Bonus = new PlayerBonus();

		// Token: 0x040003CC RID: 972
		public PlayerEvent Event = new PlayerEvent();

		// Token: 0x040003CD RID: 973
		public PlayerTitles Title = new PlayerTitles();

		// Token: 0x040003CE RID: 974
		public PlayerInventory Inventory = new PlayerInventory();

		// Token: 0x040003CF RID: 975
		public PlayerStatistic Statistic = new PlayerStatistic();

		// Token: 0x040003D0 RID: 976
		public PlayerCharacters Character = new PlayerCharacters();

		// Token: 0x040003D1 RID: 977
		public PlayerEquipment Equipment = new PlayerEquipment();

		// Token: 0x040003D2 RID: 978
		public PlayerFriends Friend = new PlayerFriends();

		// Token: 0x040003D3 RID: 979
		public PlayerQuickstart Quickstart = new PlayerQuickstart();

		// Token: 0x040003D4 RID: 980
		public PlayerMissions Mission = new PlayerMissions();

		// Token: 0x040003D5 RID: 981
		public PlayerReport Report = new PlayerReport();

		// Token: 0x040003D6 RID: 982
		public PlayerBattlepass Battlepass = new PlayerBattlepass();

		// Token: 0x040003D7 RID: 983
		public PlayerCompetitive Competitive = new PlayerCompetitive();

		// Token: 0x040003D8 RID: 984
		public AccountStatus Status = new AccountStatus();

		// Token: 0x040003D9 RID: 985
		public List<PlayerTopup> TopUps = new List<PlayerTopup>();

		// Token: 0x040003DA RID: 986
		public DateTime LastLobbyEnter;

		// Token: 0x040003DB RID: 987
		public DateTime LastPingDebug;
	}
}
