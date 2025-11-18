namespace Server.Auth.Data.Models
{
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

    public class Account
    {
        public CountryFlags Country;
        public long PlayerId;
        public long BanObjectId;
        public string Nickname = "";
        public string Password = "";
        public string Username = "";
        public string HardwareId = "";
        public string Email = "";
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
        public DateTime LastLoginDate = DateTimeUtil.Now();

        public AccessLevel AuthLevel()
        {
            int rank = this.Rank;
            return ((rank == 0x35) ? AccessLevel.GAMEMASTER : ((rank == 0x36) ? AccessLevel.MODERATOR : AccessLevel.NORMAL));
        }

        public void Close(int time)
        {
            if (this.Connection != null)
            {
                this.Connection.Close(time, true);
            }
        }

        public bool ComparePassword(string Password) => 
            ConfigLoader.IsTestMode || (this.Password == Password);

        public float ExpUp()
        {
            PCCafeModel pCCafe = TemplatePackXML.GetPCCafe(this.CafePC);
            return ((pCCafe == null) ? 0f : ((float) (pCCafe.ExpUp / 100)));
        }

        public void GetAccountInfos(int LoadType)
        {
            if ((LoadType > 0) && (this.PlayerId > 0L))
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
                if ((LoadType & 0x10) == 0x10)
                {
                    AllUtils.LoadPlayerBonus(this);
                }
                if ((LoadType & 0x20) == 0x20)
                {
                    AllUtils.LoadPlayerFriend(this, true);
                }
                if ((LoadType & 0x40) == 0x40)
                {
                    AllUtils.LoadPlayerEvent(this);
                }
                if ((LoadType & 0x80) == 0x80)
                {
                    AllUtils.LoadPlayerConfig(this);
                }
                if ((LoadType & 0x100) == 0x100)
                {
                    AllUtils.LoadPlayerFriend(this, false);
                }
                if ((LoadType & 0x200) == 0x200)
                {
                    AllUtils.LoadPlayerQuickstarts(this);
                }
                if ((LoadType & 0x400) == 0x400)
                {
                    AllUtils.LoadPlayerReport(this);
                }
                if ((LoadType & 0x800) == 0x800)
                {
                    AllUtils.LoadPlayerBattlepass(this);
                }
                if ((LoadType & 0x1000) == 0x1000)
                {
                    AllUtils.LoadPlayerCompetitive(this);
                }
            }
        }

        public int GetRank() => 
            ((this.Bonus == null) || (this.Bonus.FakeRank == 0x37)) ? this.Rank : this.Bonus.FakeRank;

        public bool HavePermission(string Permission) => 
            PermissionXML.HavePermission(Permission, this.Access);

        public bool IsBanned()
        {
            int num = ComDiv.CountDB($"SELECT * FROM base_ban_history WHERE owner_id = '{this.PlayerId}'");
            return ((ComDiv.CountDB($"SELECT * FROM base_auto_ban WHERE owner_id = '{this.PlayerId}'") > 0) || (num > 0));
        }

        public bool IsGM() => 
            this.AuthLevel() > AccessLevel.NORMAL;

        public float PointUp()
        {
            PCCafeModel pCCafe = TemplatePackXML.GetPCCafe(this.CafePC);
            return ((pCCafe == null) ? 0f : ((float) (pCCafe.PointUp / 100)));
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
            if (!string.IsNullOrEmpty(ipAddress))
            {
                CountryFlags countryByIp = CountryDetector.GetCountryByIp(ipAddress);
                this.Country = countryByIp;
                ComDiv.UpdateDB("accounts", "country_flag", (int) countryByIp, "player_id", this.PlayerId);
                if (countryByIp != CountryFlags.None)
                {
                    CLogger.Print($"Connect {this.Username} from: {countryByIp}", LoggerType.Info, null);
                }
            }
        }

        public void SetOnlineStatus(bool Online)
        {
            if ((this.IsOnline != Online) && ComDiv.UpdateDB("accounts", "online", Online, "player_id", this.PlayerId))
            {
                this.IsOnline = Online;
                CLogger.Print($"Account User: {this.Username}, Player UID: {this.PlayerId}, Is {this.IsOnline ? "Connected" : "Disconnected"}", LoggerType.Info, null);
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

        public long StatusId() => 
            (long) !string.IsNullOrEmpty(this.Nickname);

        public int TourneyLevel()
        {
            CompetitiveRank rank = CompetitiveXML.GetRank(this.Competitive.Level);
            return ((rank == null) ? 0 : rank.TourneyLevel);
        }

        public void UpdateCacheInfo()
        {
            if (this.PlayerId != 0)
            {
                SortedList<long, Account> accounts = AccountManager.Accounts;
                lock (accounts)
                {
                    AccountManager.Accounts[this.PlayerId] = this;
                }
            }
        }
    }
}

