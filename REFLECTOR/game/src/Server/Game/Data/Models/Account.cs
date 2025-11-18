namespace Server.Game.Data.Models
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Utility;
    using Plugin.Core.XML;
    using Server.Game;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Utils;
    using Server.Game.Data.XML;
    using Server.Game.Network;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Runtime.InteropServices;

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
        public string FindPlayer = "";
        public uint LastRankUpDate;
        public uint ClanDate;
        public int InventoryPlus;
        public int Sight;
        public int LastRoomPage;
        public int LastPlayerPage;
        public int ServerId = -1;
        public int ChannelId = -1;
        public int ClanAccess;
        public int Exp;
        public int Gold;
        public int Cash;
        public int ClanId;
        public int SlotId = -1;
        public int NickColor;
        public int Rank;
        public int Ribbon;
        public int Ensign;
        public int Medal;
        public int MasterMedal;
        public int MatchSlot = -1;
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
        public DateTime LastLobbyEnter = DateTimeUtil.Now();
        public DateTime LastPingDebug;

        public AccessLevel AuthLevel()
        {
            int rank = this.Rank;
            return ((rank == 0x35) ? AccessLevel.GAMEMASTER : ((rank == 0x36) ? AccessLevel.MODERATOR : AccessLevel.NORMAL));
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

        public ChannelModel GetChannel() => 
            ChannelsXML.GetChannel(this.ServerId, this.ChannelId);

        public bool GetChannel(out ChannelModel Channel)
        {
            Channel = ChannelsXML.GetChannel(this.ServerId, this.ChannelId);
            return (Channel != null);
        }

        public int GetRank() => 
            ((this.Bonus == null) || (this.Bonus.FakeRank == 0x37)) ? this.Rank : this.Bonus.FakeRank;

        public int GetSessionId() => 
            (this.Session != null) ? this.Session.SessionId : 0;

        public bool HavePermission(string Permission) => 
            PermissionXML.HavePermission(Permission, this.Access);

        public bool IsGM() => 
            this.AuthLevel() > AccessLevel.NORMAL;

        public float PointUp()
        {
            PCCafeModel pCCafe = TemplatePackXML.GetPCCafe(this.CafePC);
            return ((pCCafe == null) ? 0f : ((float) (pCCafe.PointUp / 100)));
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
            }
            else if (!OnlyInServer && ((this.Status.ServerId != 0xff) && (this.Status.ServerId != this.ServerId)))
            {
                GameXender.Sync.SendCompleteBytes(this.PlayerId, PacketName, Data, this.Status.ServerId);
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
            }
            else if (!OnlyInServer && ((this.Status.ServerId != 0xff) && (this.Status.ServerId != this.ServerId)))
            {
                GameXender.Sync.SendBytes(this.PlayerId, Packet, this.Status.ServerId);
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
            }
            else if (!OnlyInServer && ((this.Status.ServerId != 0xff) && (this.Status.ServerId != this.ServerId)))
            {
                GameXender.Sync.SendBytes(this.PlayerId, PacketName, Data, this.Status.ServerId);
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

        public bool UseChatGM() => 
            !this.HideGMcolor && ((this.Rank == 0x35) || (this.Rank == 0x36));
    }
}

