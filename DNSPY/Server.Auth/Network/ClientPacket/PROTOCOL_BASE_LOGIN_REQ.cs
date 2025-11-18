using System;
using System.Net.NetworkInformation;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.JSON;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Auth.Data.Managers;
using Server.Auth.Data.Models;
using Server.Auth.Data.Sync.Server;
using Server.Auth.Data.Utils;
using Server.Auth.Network.ServerPacket;

namespace Server.Auth.Network.ClientPacket
{
	// Token: 0x02000043 RID: 67
	public class PROTOCOL_BASE_LOGIN_REQ : AuthClientPacket
	{
		// Token: 0x060000E5 RID: 229 RVA: 0x00007B40 File Offset: 0x00005D40
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

		// Token: 0x060000E6 RID: 230 RVA: 0x00007C50 File Offset: 0x00005E50
		public override void Run()
		{
			try
			{
				uint num = ComDiv.Verificate(this.byte_0, this.byte_1, this.byte_2, this.byte_3);
				if (num == 0U)
				{
					this.Client.Close(1000, false);
				}
				else
				{
					ServerConfig config = AuthXender.Client.Config;
					if (config != null && (ConfigLoader.IsTestMode || ConfigLoader.GameLocales.Contains(this.clientLocale_0)) && this.string_0.Length >= ConfigLoader.MinUserSize && this.string_0.Length <= ConfigLoader.MaxUserSize && (ConfigLoader.IsTestMode || this.string_1.Length >= ConfigLoader.MinPassSize) && this.physicalAddress_0.GetAddressBytes() != new byte[6] && !(this.string_2 != config.ClientVersion) && (!config.AccessUFL || !(this.string_5 != config.UserFileList)) && !this.string_4.Equals("0x0") && !ResolutionJSON.GetDisplay(this.string_4).Equals("Invalid"))
					{
						Account account = (this.Client.Player = AccountManager.GetAccountDB(this.string_0, null, 0, 95));
						if (account == null && ConfigLoader.AutoAccount && !AccountManager.CreateAccount(out account, this.string_0, this.string_1))
						{
							this.Client.SendPacket(new PROTOCOL_BASE_LOGIN_ACK((EventErrorEnum)2147483929U, account, 0U));
							CLogger.Print("Failed to create account automatically [" + account.Username + "]", LoggerType.Warning, null);
							this.Client.Close(1000, false);
						}
						else
						{
							Account account2 = account;
							if (account2 != null && account2.ComparePassword(account2.Password))
							{
								if (!account2.IsBanned())
								{
									if (account2.MacAddress != this.physicalAddress_0)
									{
										ComDiv.UpdateDB("accounts", "mac_address", this.physicalAddress_0, "player_id", account2.PlayerId);
									}
									bool flag;
									bool flag2;
									DaoManagerSQL.GetBanStatus(string.Format("{0}", this.physicalAddress_0), this.string_3, out flag, out flag2);
									if (flag || flag2)
									{
										CLogger.Print((flag ? "MAC Address blocked" : "IP4 Address blocked") + " [" + account2.Username + "]", LoggerType.Warning, null);
										this.Client.SendPacket(new PROTOCOL_BASE_LOGIN_ACK(flag2 ? ((EventErrorEnum)2147483937U) : ((EventErrorEnum)2147483911U), account2, 0U));
										this.Client.Close(1000, false);
									}
									else if ((account2.IsGM() && config.OnlyGM) || (account2.AuthLevel() >= AccessLevel.NORMAL && !config.OnlyGM))
									{
										Account account3 = AccountManager.GetAccount(account2.PlayerId, true);
										if (!account2.IsOnline)
										{
											if (DaoManagerSQL.GetAccountBan(account2.BanObjectId).EndDate > DateTimeUtil.Now())
											{
												this.Client.SendPacket(new PROTOCOL_BASE_LOGIN_ACK((EventErrorEnum)2147483911U, account2, 0U));
												CLogger.Print("Account with ban is Active [" + account2.Username + "]", LoggerType.Warning, null);
												this.Client.Close(1000, false);
											}
											else
											{
												account2.Connection = this.Client;
												account2.SetCountry(this.string_3);
												account2.SetPlayerId(account2.PlayerId, 8159);
												this.Client.SendPacket(new PROTOCOL_BASE_LOGIN_ACK(EventErrorEnum.SUCCESS, account2, AllUtils.ValidateKey(account2.PlayerId, this.Client.SessionId, num)));
												this.Client.SendPacket(new PROTOCOL_CS_MEDAL_INFO_ACK());
												this.Client.SendPacket(new PROTOCOL_AUTH_GET_POINT_CASH_ACK(0U, account2));
												if (account2.ClanId > 0)
												{
													account2.ClanPlayers = ClanManager.GetClanPlayers(account2.ClanId, account2.PlayerId);
													this.Client.SendPacket(new PROTOCOL_CS_MEMBER_INFO_ACK(account2.ClanPlayers));
												}
												account2.Status.SetData(uint.MaxValue, account2.PlayerId);
												account2.Status.UpdateServer(0);
												account2.SetOnlineStatus(true);
												if (account3 != null)
												{
													account3.Connection = this.Client;
												}
												this.Client.HeartBeatCounter();
												SendRefresh.RefreshAccount(account2, true);
											}
										}
										else
										{
											this.Client.SendPacket(new PROTOCOL_BASE_LOGIN_ACK((EventErrorEnum)2147483905U, account2, 0U));
											CLogger.Print("Account online [" + account2.Username + "]", LoggerType.Warning, null);
											if (account3 != null && account3.Connection != null)
											{
												account3.SendPacket(new PROTOCOL_AUTH_ACCOUNT_KICK_ACK(1));
												account3.SendPacket(new PROTOCOL_SERVER_MESSAGE_ERROR_ACK(2147487744U));
												account3.Close(1000);
											}
											else
											{
												AuthLogin.SendLoginKickInfo(account2);
											}
											this.Client.Close(1000, false);
										}
									}
									else
									{
										this.Client.SendPacket(new PROTOCOL_BASE_LOGIN_ACK((EventErrorEnum)2147483910U, account2, 0U));
										CLogger.Print("Invalid access level [" + account2.Username + "]", LoggerType.Warning, null);
										this.Client.Close(1000, false);
									}
								}
								else
								{
									this.Client.SendPacket(new PROTOCOL_BASE_LOGIN_ACK((EventErrorEnum)2147483911U, account2, 0U));
									CLogger.Print("Permanently banned [" + account2.Username + "]", LoggerType.Warning, null);
									this.Client.Close(1000, false);
								}
							}
							else
							{
								string text = "";
								EventErrorEnum eventErrorEnum = (EventErrorEnum)2147483648U;
								if (account2 == null)
								{
									text = "Account returned from DB is null";
									eventErrorEnum = (EventErrorEnum)2147483929U;
								}
								else if (!account2.ComparePassword(account2.Password))
								{
									text = "Invalid password";
									eventErrorEnum = (EventErrorEnum)2147483927U;
								}
								this.Client.SendPacket(new PROTOCOL_BASE_LOGIN_ACK(eventErrorEnum, account2, 0U));
								CLogger.Print(text + " [" + this.string_0 + "]", LoggerType.Warning, null);
								this.Client.Close(1000, false);
							}
						}
					}
					else
					{
						string text2;
						if (config == null)
						{
							text2 = "Invalid server config [" + this.string_0 + "]";
						}
						else if (!ConfigLoader.IsTestMode && !ConfigLoader.GameLocales.Contains(this.clientLocale_0))
						{
							text2 = string.Format("Country: {0} of blocked client [{1}]", this.clientLocale_0, this.string_0);
						}
						else if (this.string_0.Length < ConfigLoader.MinUserSize)
						{
							text2 = "Username too short [" + this.string_0 + "]";
						}
						else if (this.string_0.Length > ConfigLoader.MaxUserSize)
						{
							text2 = "Username too long [" + this.string_0 + "]";
						}
						else if (!ConfigLoader.IsTestMode && this.string_1.Length < ConfigLoader.MinPassSize)
						{
							text2 = "Password too short [" + this.string_0 + "]";
						}
						else if (!ConfigLoader.IsTestMode && this.string_1.Length > ConfigLoader.MaxPassSize)
						{
							text2 = "Password too long [" + this.string_0 + "]";
						}
						else if (this.physicalAddress_0.GetAddressBytes() == new byte[6])
						{
							text2 = "Invalid MAC Address [" + this.string_0 + "]";
						}
						else if (this.string_2 != config.ClientVersion)
						{
							text2 = string.Concat(new string[] { "Version: ", this.string_2, " not supported [", this.string_0, "]" });
						}
						else if (config.AccessUFL && this.string_5 != config.UserFileList)
						{
							text2 = string.Concat(new string[] { "UserFileList: ", this.string_5, " not supported [", this.string_0, "]" });
						}
						else if (!this.string_4.Equals("0x0") && !ResolutionJSON.GetDisplay(this.string_4).Equals("Invalid"))
						{
							text2 = "There is something wrong happened when trying to login " + this.string_0;
						}
						else
						{
							text2 = string.Concat(new string[] { "Invalid ", this.string_4, " resolution [", this.string_0, "]" });
						}
						this.Client.SendPacket(new PROTOCOL_SERVER_MESSAGE_DISCONNECTIONSUCCESS_ACK(2147483904U, false));
						CLogger.Print(text2, LoggerType.Warning, null);
						this.Client.Close(1000, true);
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00002A1D File Offset: 0x00000C1D
		public PROTOCOL_BASE_LOGIN_REQ()
		{
		}

		// Token: 0x04000073 RID: 115
		private byte byte_0;

		// Token: 0x04000074 RID: 116
		private byte byte_1;

		// Token: 0x04000075 RID: 117
		private byte byte_2;

		// Token: 0x04000076 RID: 118
		private byte byte_3;

		// Token: 0x04000077 RID: 119
		private string string_0;

		// Token: 0x04000078 RID: 120
		private string string_1;

		// Token: 0x04000079 RID: 121
		private string string_2;

		// Token: 0x0400007A RID: 122
		private string string_3;

		// Token: 0x0400007B RID: 123
		private string string_4;

		// Token: 0x0400007C RID: 124
		private string string_5;

		// Token: 0x0400007D RID: 125
		private ClientLocale clientLocale_0;

		// Token: 0x0400007E RID: 126
		private PhysicalAddress physicalAddress_0;
	}
}
