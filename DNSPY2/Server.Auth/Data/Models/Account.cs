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

namespace Server.Auth.Data.Models
{
	// Token: 0x0200005E RID: 94
	public class Account
	{
		// Token: 0x06000148 RID: 328 RVA: 0x0000BD54 File Offset: 0x00009F54
		public Account()
		{
			this.LastLoginDate = DateTimeUtil.Now();
			this.Nickname = "";
			this.Password = "";
			this.Username = "";
			this.HardwareId = "";
			this.Email = "";
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000BE64 File Offset: 0x0000A064
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

		// Token: 0x0600014A RID: 330 RVA: 0x0000BF34 File Offset: 0x0000A134
		public void SetOnlineStatus(bool Online)
		{
			if (this.IsOnline != Online && ComDiv.UpdateDB("accounts", "online", Online, "player_id", this.PlayerId))
			{
				this.IsOnline = Online;
				CLogger.Print(string.Format("Account User: {0}, Player UID: {1}, Is {2}", this.Username, this.PlayerId, this.IsOnline ? "Connected" : "Disconnected"), LoggerType.Info, null);
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000BFB0 File Offset: 0x0000A1B0
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

		// Token: 0x0600014C RID: 332 RVA: 0x00002B19 File Offset: 0x00000D19
		public void Close(int time)
		{
			if (this.Connection != null)
			{
				this.Connection.Close(time, true);
			}
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00002B30 File Offset: 0x00000D30
		public void SendPacket(AuthServerPacket Packet)
		{
			if (this.Connection != null)
			{
				this.Connection.SendPacket(Packet);
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00002B46 File Offset: 0x00000D46
		public void SendPacket(byte[] Data, string PacketName)
		{
			if (this.Connection != null)
			{
				this.Connection.SendPacket(Data, PacketName);
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00002B5D File Offset: 0x00000D5D
		public void SendCompletePacket(byte[] Data, string PacketName)
		{
			if (this.Connection != null)
			{
				this.Connection.SendCompletePacket(Data, PacketName);
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00002B74 File Offset: 0x00000D74
		public long StatusId()
		{
			return (!string.IsNullOrEmpty(this.Nickname)) ? 1L : 0L;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00002B85 File Offset: 0x00000D85
		public bool ComparePassword(string Password)
		{
			return ConfigLoader.IsTestMode || this.Password == Password;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00002B9C File Offset: 0x00000D9C
		public void SetPlayerId(long PlayerId, int LoadType)
		{
			this.PlayerId = PlayerId;
			this.GetAccountInfos(LoadType);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000C004 File Offset: 0x0000A204
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

		// Token: 0x06000154 RID: 340 RVA: 0x00002BAC File Offset: 0x00000DAC
		public int GetRank()
		{
			if (this.Bonus != null && this.Bonus.FakeRank != 55)
			{
				return this.Bonus.FakeRank;
			}
			return this.Rank;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000C100 File Offset: 0x0000A300
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

		// Token: 0x06000156 RID: 342 RVA: 0x00002BD7 File Offset: 0x00000DD7
		public bool IsGM()
		{
			return this.AuthLevel() > AccessLevel.NORMAL;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00002BE2 File Offset: 0x00000DE2
		public bool HavePermission(string Permission)
		{
			return PermissionXML.HavePermission(Permission, this.Access);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000C128 File Offset: 0x0000A328
		public float PointUp()
		{
			PCCafeModel pccafe = TemplatePackXML.GetPCCafe(this.CafePC);
			if (pccafe != null)
			{
				return (float)(pccafe.PointUp / 100);
			}
			return 0f;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000C154 File Offset: 0x0000A354
		public float ExpUp()
		{
			PCCafeModel pccafe = TemplatePackXML.GetPCCafe(this.CafePC);
			if (pccafe != null)
			{
				return (float)(pccafe.ExpUp / 100);
			}
			return 0f;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000C180 File Offset: 0x0000A380
		public int TourneyLevel()
		{
			CompetitiveRank rank = CompetitiveXML.GetRank(this.Competitive.Level);
			if (rank != null)
			{
				return rank.TourneyLevel;
			}
			return 0;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000C1AC File Offset: 0x0000A3AC
		public bool IsBanned()
		{
			int num = ComDiv.CountDB(string.Format("SELECT * FROM base_auto_ban WHERE owner_id = '{0}'", this.PlayerId));
			int num2 = ComDiv.CountDB(string.Format("SELECT * FROM base_ban_history WHERE owner_id = '{0}'", this.PlayerId));
			return num > 0 || num2 > 0;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000C1F8 File Offset: 0x0000A3F8
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

		// Token: 0x0400009C RID: 156
		public CountryFlags Country;

		// Token: 0x0400009D RID: 157
		public long PlayerId;

		// Token: 0x0400009E RID: 158
		public long BanObjectId;

		// Token: 0x0400009F RID: 159
		public string Nickname;

		// Token: 0x040000A0 RID: 160
		public string Password;

		// Token: 0x040000A1 RID: 161
		public string Username;

		// Token: 0x040000A2 RID: 162
		public string HardwareId;

		// Token: 0x040000A3 RID: 163
		public string Email;

		// Token: 0x040000A4 RID: 164
		public uint LastRankUpDate;

		// Token: 0x040000A5 RID: 165
		public int InventoryPlus;

		// Token: 0x040000A6 RID: 166
		public int Exp;

		// Token: 0x040000A7 RID: 167
		public int Gold;

		// Token: 0x040000A8 RID: 168
		public int ClanId;

		// Token: 0x040000A9 RID: 169
		public int ClanAccess;

		// Token: 0x040000AA RID: 170
		public int Cash;

		// Token: 0x040000AB RID: 171
		public int Rank;

		// Token: 0x040000AC RID: 172
		public int Ribbon;

		// Token: 0x040000AD RID: 173
		public int Ensign;

		// Token: 0x040000AE RID: 174
		public int Medal;

		// Token: 0x040000AF RID: 175
		public int MasterMedal;

		// Token: 0x040000B0 RID: 176
		public int NickColor;

		// Token: 0x040000B1 RID: 177
		public int Age;

		// Token: 0x040000B2 RID: 178
		public int Tags;

		// Token: 0x040000B3 RID: 179
		public bool MyConfigsLoaded;

		// Token: 0x040000B4 RID: 180
		public bool IsOnline;

		// Token: 0x040000B5 RID: 181
		public AccessLevel Access;

		// Token: 0x040000B6 RID: 182
		public CouponEffects Effects;

		// Token: 0x040000B7 RID: 183
		public CafeEnum CafePC;

		// Token: 0x040000B8 RID: 184
		public PhysicalAddress MacAddress;

		// Token: 0x040000B9 RID: 185
		public AuthClient Connection;

		// Token: 0x040000BA RID: 186
		public PlayerBonus Bonus = new PlayerBonus();

		// Token: 0x040000BB RID: 187
		public PlayerConfig Config = new PlayerConfig();

		// Token: 0x040000BC RID: 188
		public PlayerEvent Event = new PlayerEvent();

		// Token: 0x040000BD RID: 189
		public PlayerTitles Title = new PlayerTitles();

		// Token: 0x040000BE RID: 190
		public PlayerInventory Inventory = new PlayerInventory();

		// Token: 0x040000BF RID: 191
		public AccountStatus Status = new AccountStatus();

		// Token: 0x040000C0 RID: 192
		public PlayerFriends Friend = new PlayerFriends();

		// Token: 0x040000C1 RID: 193
		public PlayerStatistic Statistic = new PlayerStatistic();

		// Token: 0x040000C2 RID: 194
		public PlayerQuickstart Quickstart = new PlayerQuickstart();

		// Token: 0x040000C3 RID: 195
		public PlayerCharacters Character = new PlayerCharacters();

		// Token: 0x040000C4 RID: 196
		public PlayerEquipment Equipment = new PlayerEquipment();

		// Token: 0x040000C5 RID: 197
		public PlayerMissions Mission = new PlayerMissions();

		// Token: 0x040000C6 RID: 198
		public PlayerReport Report = new PlayerReport();

		// Token: 0x040000C7 RID: 199
		public PlayerBattlepass Battlepass = new PlayerBattlepass();

		// Token: 0x040000C8 RID: 200
		public PlayerCompetitive Competitive = new PlayerCompetitive();

		// Token: 0x040000C9 RID: 201
		public List<Account> ClanPlayers = new List<Account>();

		// Token: 0x040000CA RID: 202
		public List<PlayerTopup> TopUps = new List<PlayerTopup>();

		// Token: 0x040000CB RID: 203
		public DateTime LastLoginDate;
	}
}
