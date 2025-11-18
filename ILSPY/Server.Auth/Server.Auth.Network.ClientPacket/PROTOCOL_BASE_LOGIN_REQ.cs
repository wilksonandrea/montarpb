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

namespace Server.Auth.Network.ClientPacket;

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
		byte_0 = ReadC();
		byte_1 = ReadC();
		byte_2 = ReadC();
		byte_3 = ReadC();
		physicalAddress_0 = new PhysicalAddress(ReadB(6));
		ReadB(20);
		string_4 = $"{ReadH()}x{ReadH()}";
		ReadB(9);
		string_5 = ReadS(ReadC());
		ReadB(16);
		clientLocale_0 = (ClientLocale)ReadC();
		string_2 = $"{ReadC()}.{ReadH()}";
		ReadH();
		string_1 = ReadS(ReadC());
		string_0 = ReadS(ReadC());
		string_3 = Client.GetIPAddress();
	}

	public override void Run()
	{
		try
		{
			uint num = ComDiv.Verificate(byte_0, byte_1, byte_2, byte_3);
			if (num == 0)
			{
				Client.Close(1000, DestroyConnection: false);
				return;
			}
			ServerConfig config = AuthXender.Client.Config;
			if (config != null && (ConfigLoader.IsTestMode || ConfigLoader.GameLocales.Contains(clientLocale_0)) && string_0.Length >= ConfigLoader.MinUserSize && string_0.Length <= ConfigLoader.MaxUserSize && (ConfigLoader.IsTestMode || string_1.Length >= ConfigLoader.MinPassSize) && physicalAddress_0.GetAddressBytes() != new byte[6] && !(string_2 != config.ClientVersion) && (!config.AccessUFL || !(string_5 != config.UserFileList)) && !string_4.Equals("0x0") && !ResolutionJSON.GetDisplay(string_4).Equals("Invalid"))
			{
				Account Player = (Client.Player = AccountManager.GetAccountDB(string_0, null, 0, 95));
				if (Player == null && ConfigLoader.AutoAccount && !AccountManager.CreateAccount(out Player, string_0, string_1))
				{
					Client.SendPacket(new PROTOCOL_BASE_LOGIN_ACK(EventErrorEnum.LOGIN_DELETE_ACCOUNT, Player, 0u));
					CLogger.Print("Failed to create account automatically [" + Player.Username + "]", LoggerType.Warning);
					Client.Close(1000, DestroyConnection: false);
					return;
				}
				Account account = Player;
				if (account != null && account.ComparePassword(account.Password))
				{
					if (!account.IsBanned())
					{
						if (account.MacAddress != physicalAddress_0)
						{
							ComDiv.UpdateDB("accounts", "mac_address", physicalAddress_0, "player_id", account.PlayerId);
						}
						DaoManagerSQL.GetBanStatus($"{physicalAddress_0}", string_3, out var ValidMac, out var ValidIp);
						if (ValidMac || ValidIp)
						{
							CLogger.Print((ValidMac ? "MAC Address blocked" : "IP4 Address blocked") + " [" + account.Username + "]", LoggerType.Warning);
							Client.SendPacket(new PROTOCOL_BASE_LOGIN_ACK(ValidIp ? EventErrorEnum.LOGIN_BLOCK_IP : EventErrorEnum.EVENT_LOG_IN_BLOCK_ACCOUNT, account, 0u));
							Client.Close(1000, DestroyConnection: false);
						}
						else if ((account.IsGM() && config.OnlyGM) || (account.AuthLevel() >= AccessLevel.NORMAL && !config.OnlyGM))
						{
							Account account2 = AccountManager.GetAccount(account.PlayerId, noUseDB: true);
							if (!account.IsOnline)
							{
								if (DaoManagerSQL.GetAccountBan(account.BanObjectId).EndDate > DateTimeUtil.Now())
								{
									Client.SendPacket(new PROTOCOL_BASE_LOGIN_ACK(EventErrorEnum.EVENT_LOG_IN_BLOCK_ACCOUNT, account, 0u));
									CLogger.Print("Account with ban is Active [" + account.Username + "]", LoggerType.Warning);
									Client.Close(1000, DestroyConnection: false);
									return;
								}
								account.Connection = Client;
								account.SetCountry(string_3);
								account.SetPlayerId(account.PlayerId, 8159);
								Client.SendPacket(new PROTOCOL_BASE_LOGIN_ACK(EventErrorEnum.SUCCESS, account, AllUtils.ValidateKey(account.PlayerId, Client.SessionId, num)));
								Client.SendPacket(new PROTOCOL_CS_MEDAL_INFO_ACK());
								Client.SendPacket(new PROTOCOL_AUTH_GET_POINT_CASH_ACK(0u, account));
								if (account.ClanId > 0)
								{
									account.ClanPlayers = ClanManager.GetClanPlayers(account.ClanId, account.PlayerId);
									Client.SendPacket(new PROTOCOL_CS_MEMBER_INFO_ACK(account.ClanPlayers));
								}
								account.Status.SetData(uint.MaxValue, account.PlayerId);
								account.Status.UpdateServer(0);
								account.SetOnlineStatus(Online: true);
								if (account2 != null)
								{
									account2.Connection = Client;
								}
								Client.HeartBeatCounter();
								SendRefresh.RefreshAccount(account, IsConnect: true);
							}
							else
							{
								Client.SendPacket(new PROTOCOL_BASE_LOGIN_ACK(EventErrorEnum.EVENT_LOG_IN_ALREADY_LOGIN, account, 0u));
								CLogger.Print("Account online [" + account.Username + "]", LoggerType.Warning);
								if (account2 != null && account2.Connection != null)
								{
									account2.SendPacket(new PROTOCOL_AUTH_ACCOUNT_KICK_ACK(1));
									account2.SendPacket(new PROTOCOL_SERVER_MESSAGE_ERROR_ACK(2147487744u));
									account2.Close(1000);
								}
								else
								{
									AuthLogin.SendLoginKickInfo(account);
								}
								Client.Close(1000, DestroyConnection: false);
							}
						}
						else
						{
							Client.SendPacket(new PROTOCOL_BASE_LOGIN_ACK(EventErrorEnum.EVENT_LOG_IN_TIME_OUT_2, account, 0u));
							CLogger.Print("Invalid access level [" + account.Username + "]", LoggerType.Warning);
							Client.Close(1000, DestroyConnection: false);
						}
					}
					else
					{
						Client.SendPacket(new PROTOCOL_BASE_LOGIN_ACK(EventErrorEnum.EVENT_LOG_IN_BLOCK_ACCOUNT, account, 0u));
						CLogger.Print("Permanently banned [" + account.Username + "]", LoggerType.Warning);
						Client.Close(1000, DestroyConnection: false);
					}
				}
				else
				{
					string text = "";
					EventErrorEnum eventErrorEnum_ = EventErrorEnum.FAIL;
					if (account == null)
					{
						text = "Account returned from DB is null";
						eventErrorEnum_ = EventErrorEnum.LOGIN_DELETE_ACCOUNT;
					}
					else if (!account.ComparePassword(account.Password))
					{
						text = "Invalid password";
						eventErrorEnum_ = EventErrorEnum.LOGIN_ID_PASS_INCORRECT;
					}
					Client.SendPacket(new PROTOCOL_BASE_LOGIN_ACK(eventErrorEnum_, account, 0u));
					CLogger.Print(text + " [" + string_0 + "]", LoggerType.Warning);
					Client.Close(1000, DestroyConnection: false);
				}
			}
			else
			{
				string text2 = "";
				text2 = ((config == null) ? ("Invalid server config [" + string_0 + "]") : ((!ConfigLoader.IsTestMode && !ConfigLoader.GameLocales.Contains(clientLocale_0)) ? $"Country: {clientLocale_0} of blocked client [{string_0}]" : ((string_0.Length < ConfigLoader.MinUserSize) ? ("Username too short [" + string_0 + "]") : ((string_0.Length > ConfigLoader.MaxUserSize) ? ("Username too long [" + string_0 + "]") : ((!ConfigLoader.IsTestMode && string_1.Length < ConfigLoader.MinPassSize) ? ("Password too short [" + string_0 + "]") : ((!ConfigLoader.IsTestMode && string_1.Length > ConfigLoader.MaxPassSize) ? ("Password too long [" + string_0 + "]") : ((physicalAddress_0.GetAddressBytes() == new byte[6]) ? ("Invalid MAC Address [" + string_0 + "]") : ((string_2 != config.ClientVersion) ? ("Version: " + string_2 + " not supported [" + string_0 + "]") : ((config.AccessUFL && string_5 != config.UserFileList) ? ("UserFileList: " + string_5 + " not supported [" + string_0 + "]") : ((string_4.Equals("0x0") || ResolutionJSON.GetDisplay(string_4).Equals("Invalid")) ? ("Invalid " + string_4 + " resolution [" + string_0 + "]") : ("There is something wrong happened when trying to login " + string_0)))))))))));
				Client.SendPacket(new PROTOCOL_SERVER_MESSAGE_DISCONNECTIONSUCCESS_ACK(2147483904u, bool_1: false));
				CLogger.Print(text2, LoggerType.Warning);
				Client.Close(1000, DestroyConnection: true);
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
