using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.JSON;
using Plugin.Core.Models;
using Plugin.Core.Network;
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
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace Server.Auth.Network.ClientPacket
{
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

		public PROTOCOL_BASE_LOGIN_REQ()
		{
		}

		public override void Read()
		{
			this.byte_0 = base.ReadC();
			this.byte_1 = base.ReadC();
			this.byte_2 = base.ReadC();
			this.byte_3 = base.ReadC();
			this.physicalAddress_0 = new PhysicalAddress(base.ReadB(6));
			base.ReadB(20);
			this.string_4 = string.Format("{0}x{1}", base.ReadH(), base.ReadH());
			base.ReadB(9);
			this.string_5 = base.ReadS((int)base.ReadC());
			base.ReadB(16);
			this.clientLocale_0 = (ClientLocale)base.ReadC();
			this.string_2 = string.Format("{0}.{1}", base.ReadC(), base.ReadH());
			base.ReadH();
			this.string_1 = base.ReadS((int)base.ReadC());
			this.string_0 = base.ReadS((int)base.ReadC());
			this.string_3 = this.Client.GetIPAddress();
		}

		public override void Run()
		{
			bool flag;
			bool flag1;
			try
			{
				uint uInt32 = ComDiv.Verificate(this.byte_0, this.byte_1, this.byte_2, this.byte_3);
				if (uInt32 != 0)
				{
					ServerConfig config = AuthXender.Client.Config;
					if (config == null || !ConfigLoader.IsTestMode && !ConfigLoader.GameLocales.Contains(this.clientLocale_0) || this.string_0.Length < ConfigLoader.MinUserSize || this.string_0.Length > ConfigLoader.MaxUserSize || !ConfigLoader.IsTestMode && this.string_1.Length < ConfigLoader.MinPassSize || this.physicalAddress_0.GetAddressBytes() == new byte[6] || this.string_2 != config.ClientVersion || config.AccessUFL && this.string_5 != config.UserFileList || this.string_4.Equals("0x0") || ResolutionJSON.GetDisplay(this.string_4).Equals("Invalid"))
					{
						string str = "";
						if (config == null)
						{
							str = string.Concat("Invalid server config [", this.string_0, "]");
						}
						else if (!ConfigLoader.IsTestMode && !ConfigLoader.GameLocales.Contains(this.clientLocale_0))
						{
							str = string.Format("Country: {0} of blocked client [{1}]", this.clientLocale_0, this.string_0);
						}
						else if (this.string_0.Length < ConfigLoader.MinUserSize)
						{
							str = string.Concat("Username too short [", this.string_0, "]");
						}
						else if (this.string_0.Length > ConfigLoader.MaxUserSize)
						{
							str = string.Concat("Username too long [", this.string_0, "]");
						}
						else if (!ConfigLoader.IsTestMode && this.string_1.Length < ConfigLoader.MinPassSize)
						{
							str = string.Concat("Password too short [", this.string_0, "]");
						}
						else if (!ConfigLoader.IsTestMode && this.string_1.Length > ConfigLoader.MaxPassSize)
						{
							str = string.Concat("Password too long [", this.string_0, "]");
						}
						else if (this.physicalAddress_0.GetAddressBytes() == new byte[6])
						{
							str = string.Concat("Invalid MAC Address [", this.string_0, "]");
						}
						else if (this.string_2 != config.ClientVersion)
						{
							str = string.Concat(new string[] { "Version: ", this.string_2, " not supported [", this.string_0, "]" });
						}
						else if (!config.AccessUFL || !(this.string_5 != config.UserFileList))
						{
							str = (this.string_4.Equals("0x0") || ResolutionJSON.GetDisplay(this.string_4).Equals("Invalid") ? string.Concat(new string[] { "Invalid ", this.string_4, " resolution [", this.string_0, "]" }) : string.Concat("There is something wrong happened when trying to login ", this.string_0));
						}
						else
						{
							str = string.Concat(new string[] { "UserFileList: ", this.string_5, " not supported [", this.string_0, "]" });
						}
						this.Client.SendPacket(new PROTOCOL_SERVER_MESSAGE_DISCONNECTIONSUCCESS_ACK(-2147483392, false));
						CLogger.Print(str, LoggerType.Warning, null);
						this.Client.Close(1000, true);
					}
					else
					{
						AuthClient client = this.Client;
						Account accountDB = AccountManager.GetAccountDB(this.string_0, null, 0, 95);
						Account account = accountDB;
						client.Player = accountDB;
						Account account1 = account;
						if (account1 != null || !ConfigLoader.AutoAccount || AccountManager.CreateAccount(out account1, this.string_0, this.string_1))
						{
							Account clanPlayers = account1;
							if (clanPlayers == null || !clanPlayers.ComparePassword(clanPlayers.Password))
							{
								string str1 = "";
								EventErrorEnum eventErrorEnum = EventErrorEnum.FAIL;
								if (clanPlayers == null)
								{
									str1 = "Account returned from DB is null";
									eventErrorEnum = EventErrorEnum.LOGIN_DELETE_ACCOUNT;
								}
								else if (!clanPlayers.ComparePassword(clanPlayers.Password))
								{
									str1 = "Invalid password";
									eventErrorEnum = EventErrorEnum.LOGIN_ID_PASS_INCORRECT;
								}
								this.Client.SendPacket(new PROTOCOL_BASE_LOGIN_ACK(eventErrorEnum, clanPlayers, 0));
								CLogger.Print(string.Concat(str1, " [", this.string_0, "]"), LoggerType.Warning, null);
								this.Client.Close(1000, false);
							}
							else if (clanPlayers.IsBanned())
							{
								this.Client.SendPacket(new PROTOCOL_BASE_LOGIN_ACK(EventErrorEnum.EVENT_LOG_IN_BLOCK_ACCOUNT, clanPlayers, 0));
								CLogger.Print(string.Concat("Permanently banned [", clanPlayers.Username, "]"), LoggerType.Warning, null);
								this.Client.Close(1000, false);
							}
							else
							{
								if (clanPlayers.MacAddress != this.physicalAddress_0)
								{
									ComDiv.UpdateDB("accounts", "mac_address", this.physicalAddress_0, "player_id", clanPlayers.PlayerId);
								}
								DaoManagerSQL.GetBanStatus(string.Format("{0}", this.physicalAddress_0), this.string_3, out flag, out flag1);
								if (flag | flag1)
								{
									CLogger.Print(string.Concat((flag ? "MAC Address blocked" : "IP4 Address blocked"), " [", clanPlayers.Username, "]"), LoggerType.Warning, null);
									this.Client.SendPacket(new PROTOCOL_BASE_LOGIN_ACK((flag1 ? EventErrorEnum.LOGIN_BLOCK_IP : EventErrorEnum.EVENT_LOG_IN_BLOCK_ACCOUNT), clanPlayers, 0));
									this.Client.Close(1000, false);
								}
								else if ((!clanPlayers.IsGM() || !config.OnlyGM) && (clanPlayers.AuthLevel() < AccessLevel.NORMAL || config.OnlyGM))
								{
									this.Client.SendPacket(new PROTOCOL_BASE_LOGIN_ACK(EventErrorEnum.EVENT_LOG_IN_TIME_OUT_2, clanPlayers, 0));
									CLogger.Print(string.Concat("Invalid access level [", clanPlayers.Username, "]"), LoggerType.Warning, null);
									this.Client.Close(1000, false);
								}
								else
								{
									Account client1 = AccountManager.GetAccount(clanPlayers.PlayerId, true);
									if (clanPlayers.IsOnline)
									{
										this.Client.SendPacket(new PROTOCOL_BASE_LOGIN_ACK(EventErrorEnum.EVENT_LOG_IN_ALREADY_LOGIN, clanPlayers, 0));
										CLogger.Print(string.Concat("Account online [", clanPlayers.Username, "]"), LoggerType.Warning, null);
										if (client1 == null || client1.Connection == null)
										{
											AuthLogin.SendLoginKickInfo(clanPlayers);
										}
										else
										{
											client1.SendPacket(new PROTOCOL_AUTH_ACCOUNT_KICK_ACK(1));
											client1.SendPacket(new PROTOCOL_SERVER_MESSAGE_ERROR_ACK(-2147479552));
											client1.Close(1000);
										}
										this.Client.Close(1000, false);
									}
									else if (DaoManagerSQL.GetAccountBan(clanPlayers.BanObjectId).EndDate <= DateTimeUtil.Now())
									{
										clanPlayers.Connection = this.Client;
										clanPlayers.SetCountry(this.string_3);
										clanPlayers.SetPlayerId(clanPlayers.PlayerId, 8159);
										this.Client.SendPacket(new PROTOCOL_BASE_LOGIN_ACK(EventErrorEnum.SUCCESS, clanPlayers, AllUtils.ValidateKey(clanPlayers.PlayerId, this.Client.SessionId, uInt32)));
										this.Client.SendPacket(new PROTOCOL_CS_MEDAL_INFO_ACK());
										this.Client.SendPacket(new PROTOCOL_AUTH_GET_POINT_CASH_ACK(0, clanPlayers));
										if (clanPlayers.ClanId > 0)
										{
											clanPlayers.ClanPlayers = ClanManager.GetClanPlayers(clanPlayers.ClanId, clanPlayers.PlayerId);
											this.Client.SendPacket(new PROTOCOL_CS_MEMBER_INFO_ACK(clanPlayers.ClanPlayers));
										}
										clanPlayers.Status.SetData(-1, clanPlayers.PlayerId);
										clanPlayers.Status.UpdateServer(0);
										clanPlayers.SetOnlineStatus(true);
										if (client1 != null)
										{
											client1.Connection = this.Client;
										}
										this.Client.HeartBeatCounter();
										SendRefresh.RefreshAccount(clanPlayers, true);
									}
									else
									{
										this.Client.SendPacket(new PROTOCOL_BASE_LOGIN_ACK(EventErrorEnum.EVENT_LOG_IN_BLOCK_ACCOUNT, clanPlayers, 0));
										CLogger.Print(string.Concat("Account with ban is Active [", clanPlayers.Username, "]"), LoggerType.Warning, null);
										this.Client.Close(1000, false);
									}
								}
							}
						}
						else
						{
							this.Client.SendPacket(new PROTOCOL_BASE_LOGIN_ACK(EventErrorEnum.LOGIN_DELETE_ACCOUNT, account1, 0));
							CLogger.Print(string.Concat("Failed to create account automatically [", account1.Username, "]"), LoggerType.Warning, null);
							this.Client.Close(1000, false);
						}
					}
				}
				else
				{
					this.Client.Close(1000, false);
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}
	}
}