namespace Server.Auth.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.JSON;
    using Plugin.Core.Models;
    using Plugin.Core.SQL;
    using Plugin.Core.Utility;
    using Server.Auth;
    using Server.Auth.Data.Managers;
    using Server.Auth.Data.Models;
    using Server.Auth.Data.Sync.Server;
    using Server.Auth.Data.Utils;
    using Server.Auth.Network;
    using Server.Auth.Network.ServerPacket;
    using System;
    using System.Net.NetworkInformation;

    public class PROTOCOL_BASE_LOGIN_REQ : AuthClientPacket
    {
        private byte byte_0;
        private byte byte_1;
        private byte byte_2;
        private byte byte_3;
        private string string_0;
        private string string_1;
        private string string_2;
        private string string_3;
        private string string_4;
        private string string_5;
        private ClientLocale clientLocale_0;
        private PhysicalAddress physicalAddress_0;

        public override void Read()
        {
            this.byte_0 = base.ReadC();
            this.byte_1 = base.ReadC();
            this.byte_2 = base.ReadC();
            this.byte_3 = base.ReadC();
            this.physicalAddress_0 = new PhysicalAddress(base.ReadB(6));
            base.ReadB(20);
            this.string_4 = $"{base.ReadH()}x{base.ReadH()}";
            base.ReadB(9);
            this.string_5 = base.ReadS(base.ReadC());
            base.ReadB(0x10);
            this.clientLocale_0 = (ClientLocale) base.ReadC();
            this.string_2 = $"{base.ReadC()}.{base.ReadH()}";
            base.ReadH();
            this.string_1 = base.ReadS(base.ReadC());
            this.string_0 = base.ReadS(base.ReadC());
            this.string_3 = base.Client.GetIPAddress();
        }

        public override void Run()
        {
            try
            {
                uint unknown = ComDiv.Verificate(this.byte_0, this.byte_1, this.byte_2, this.byte_3);
                if (unknown == 0)
                {
                    base.Client.Close(0x3e8, false);
                }
                else
                {
                    ServerConfig config = AuthXender.Client.Config;
                    if ((((config == null) || (!ConfigLoader.IsTestMode && !ConfigLoader.GameLocales.Contains(this.clientLocale_0))) || (((this.string_0.Length < ConfigLoader.MinUserSize) || ((this.string_0.Length > ConfigLoader.MaxUserSize) || (!ConfigLoader.IsTestMode && (this.string_1.Length < ConfigLoader.MinPassSize)))) || (((this.physicalAddress_0.GetAddressBytes() == new byte[6]) || ((this.string_2 != config.ClientVersion) || (config.AccessUFL && (this.string_5 != config.UserFileList)))) || this.string_4.Equals("0x0")))) || ResolutionJSON.GetDisplay(this.string_4).Equals("Invalid"))
                    {
                        string text = "";
                        if (config == null)
                        {
                            text = "Invalid server config [" + this.string_0 + "]";
                        }
                        else if (!ConfigLoader.IsTestMode && !ConfigLoader.GameLocales.Contains(this.clientLocale_0))
                        {
                            text = $"Country: {this.clientLocale_0} of blocked client [{this.string_0}]";
                        }
                        else if (this.string_0.Length < ConfigLoader.MinUserSize)
                        {
                            text = "Username too short [" + this.string_0 + "]";
                        }
                        else if (this.string_0.Length > ConfigLoader.MaxUserSize)
                        {
                            text = "Username too long [" + this.string_0 + "]";
                        }
                        else if (!ConfigLoader.IsTestMode && (this.string_1.Length < ConfigLoader.MinPassSize))
                        {
                            text = "Password too short [" + this.string_0 + "]";
                        }
                        else if (!ConfigLoader.IsTestMode && (this.string_1.Length > ConfigLoader.MaxPassSize))
                        {
                            text = "Password too long [" + this.string_0 + "]";
                        }
                        else if (this.physicalAddress_0.GetAddressBytes() == new byte[6])
                        {
                            text = "Invalid MAC Address [" + this.string_0 + "]";
                        }
                        else if (this.string_2 != config.ClientVersion)
                        {
                            string[] textArray1 = new string[] { "Version: ", this.string_2, " not supported [", this.string_0, "]" };
                            text = string.Concat(textArray1);
                        }
                        else if (config.AccessUFL && (this.string_5 != config.UserFileList))
                        {
                            string[] textArray2 = new string[] { "UserFileList: ", this.string_5, " not supported [", this.string_0, "]" };
                            text = string.Concat(textArray2);
                        }
                        else if (!this.string_4.Equals("0x0") && !ResolutionJSON.GetDisplay(this.string_4).Equals("Invalid"))
                        {
                            text = "There is something wrong happened when trying to login " + this.string_0;
                        }
                        else
                        {
                            string[] textArray3 = new string[] { "Invalid ", this.string_4, " resolution [", this.string_0, "]" };
                            text = string.Concat(textArray3);
                        }
                        base.Client.SendPacket(new PROTOCOL_SERVER_MESSAGE_DISCONNECTIONSUCCESS_ACK(0x80000100, false));
                        CLogger.Print(text, LoggerType.Warning, null);
                        base.Client.Close(0x3e8, true);
                    }
                    else
                    {
                        Account player = base.Client.Player = AccountManager.GetAccountDB(this.string_0, null, 0, 0x5f);
                        if ((player == null) && (ConfigLoader.AutoAccount && !AccountManager.CreateAccount(out player, this.string_0, this.string_1)))
                        {
                            base.Client.SendPacket(new PROTOCOL_BASE_LOGIN_ACK((EventErrorEnum) (-2147483367), player, 0));
                            CLogger.Print("Failed to create account automatically [" + player.Username + "]", LoggerType.Warning, null);
                            base.Client.Close(0x3e8, false);
                        }
                        else
                        {
                            Account account3 = player;
                            if ((account3 == null) || !account3.ComparePassword(account3.Password))
                            {
                                string str2 = "";
                                EventErrorEnum enum2 = (EventErrorEnum) (-2147483648);
                                if (account3 == null)
                                {
                                    str2 = "Account returned from DB is null";
                                    enum2 = (EventErrorEnum) (-2147483367);
                                }
                                else if (!account3.ComparePassword(account3.Password))
                                {
                                    str2 = "Invalid password";
                                    enum2 = (EventErrorEnum) (-2147483369);
                                }
                                base.Client.SendPacket(new PROTOCOL_BASE_LOGIN_ACK(enum2, account3, 0));
                                CLogger.Print(str2 + " [" + this.string_0 + "]", LoggerType.Warning, null);
                                base.Client.Close(0x3e8, false);
                            }
                            else if (account3.IsBanned())
                            {
                                base.Client.SendPacket(new PROTOCOL_BASE_LOGIN_ACK((EventErrorEnum) (-2147483385), account3, 0));
                                CLogger.Print("Permanently banned [" + account3.Username + "]", LoggerType.Warning, null);
                                base.Client.Close(0x3e8, false);
                            }
                            else
                            {
                                bool flag;
                                bool flag2;
                                if (!ReferenceEquals(account3.MacAddress, this.physicalAddress_0))
                                {
                                    ComDiv.UpdateDB("accounts", "mac_address", this.physicalAddress_0, "player_id", account3.PlayerId);
                                }
                                DaoManagerSQL.GetBanStatus($"{this.physicalAddress_0}", this.string_3, out flag, out flag2);
                                if (flag | flag2)
                                {
                                    CLogger.Print((flag ? "MAC Address blocked" : "IP4 Address blocked") + " [" + account3.Username + "]", LoggerType.Warning, null);
                                    base.Client.SendPacket(new PROTOCOL_BASE_LOGIN_ACK(flag2 ? ((EventErrorEnum) (-2147483359)) : ((EventErrorEnum) (-2147483385)), account3, 0));
                                    base.Client.Close(0x3e8, false);
                                }
                                else if ((!account3.IsGM() || !config.OnlyGM) && ((account3.AuthLevel() < AccessLevel.NORMAL) || config.OnlyGM))
                                {
                                    base.Client.SendPacket(new PROTOCOL_BASE_LOGIN_ACK((EventErrorEnum) (-2147483386), account3, 0));
                                    CLogger.Print("Invalid access level [" + account3.Username + "]", LoggerType.Warning, null);
                                    base.Client.Close(0x3e8, false);
                                }
                                else
                                {
                                    Account account = AccountManager.GetAccount(account3.PlayerId, true);
                                    if (account3.IsOnline)
                                    {
                                        base.Client.SendPacket(new PROTOCOL_BASE_LOGIN_ACK((EventErrorEnum) (-2147483391), account3, 0));
                                        CLogger.Print("Account online [" + account3.Username + "]", LoggerType.Warning, null);
                                        if ((account == null) || (account.Connection == null))
                                        {
                                            AuthLogin.SendLoginKickInfo(account3);
                                        }
                                        else
                                        {
                                            account.SendPacket(new PROTOCOL_AUTH_ACCOUNT_KICK_ACK(1));
                                            account.SendPacket(new PROTOCOL_SERVER_MESSAGE_ERROR_ACK(0x80001000));
                                            account.Close(0x3e8);
                                        }
                                        base.Client.Close(0x3e8, false);
                                    }
                                    else if (DaoManagerSQL.GetAccountBan(account3.BanObjectId).EndDate > DateTimeUtil.Now())
                                    {
                                        base.Client.SendPacket(new PROTOCOL_BASE_LOGIN_ACK((EventErrorEnum) (-2147483385), account3, 0));
                                        CLogger.Print("Account with ban is Active [" + account3.Username + "]", LoggerType.Warning, null);
                                        base.Client.Close(0x3e8, false);
                                    }
                                    else
                                    {
                                        account3.Connection = base.Client;
                                        account3.SetCountry(this.string_3);
                                        account3.SetPlayerId(account3.PlayerId, 0x1fdf);
                                        base.Client.SendPacket(new PROTOCOL_BASE_LOGIN_ACK(EventErrorEnum.SUCCESS, account3, AllUtils.ValidateKey(account3.PlayerId, base.Client.SessionId, unknown)));
                                        base.Client.SendPacket(new PROTOCOL_CS_MEDAL_INFO_ACK());
                                        base.Client.SendPacket(new PROTOCOL_AUTH_GET_POINT_CASH_ACK(0, account3));
                                        if (account3.ClanId > 0)
                                        {
                                            account3.ClanPlayers = ClanManager.GetClanPlayers(account3.ClanId, account3.PlayerId);
                                            base.Client.SendPacket(new PROTOCOL_CS_MEMBER_INFO_ACK(account3.ClanPlayers));
                                        }
                                        account3.Status.SetData(uint.MaxValue, account3.PlayerId);
                                        account3.Status.UpdateServer(0);
                                        account3.SetOnlineStatus(true);
                                        if (account != null)
                                        {
                                            account.Connection = base.Client;
                                        }
                                        base.Client.HeartBeatCounter();
                                        SendRefresh.RefreshAccount(account3, true);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

