// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ClientPacket.PROTOCOL_BASE_LOGOUT_REQ
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using dummy_ptr;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.JSON;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Auth.Data.Managers;
using Server.Auth.Data.Models;
using Server.Auth.Data.Sync;
using Server.Auth.Data.Sync.Client;
using Server.Auth.Data.Sync.Server;
using Server.Auth.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Auth.Network.ClientPacket;

public class PROTOCOL_BASE_LOGOUT_REQ : AuthClientPacket
{
  public virtual void Run()
  {
    try
    {
      uint num = ComDiv.Verificate(((PROTOCOL_BASE_LOGIN_REQ) this).byte_0, ((PROTOCOL_BASE_LOGIN_REQ) this).byte_1, ((PROTOCOL_BASE_LOGIN_REQ) this).byte_2, ((PROTOCOL_BASE_LOGIN_REQ) this).byte_3);
      if (num == 0U)
      {
        this.Client.Close(1000, false);
      }
      else
      {
        ServerConfig config = AuthXender.Client.Config;
        if (config != null && (ConfigLoader.IsTestMode || ConfigLoader.GameLocales.Contains(((PROTOCOL_BASE_LOGIN_REQ) this).clientLocale_0)) && ((PROTOCOL_BASE_LOGIN_REQ) this).string_0.Length >= ConfigLoader.MinUserSize && ((PROTOCOL_BASE_LOGIN_REQ) this).string_0.Length <= ConfigLoader.MaxUserSize && (ConfigLoader.IsTestMode || ((PROTOCOL_BASE_LOGIN_REQ) this).string_1.Length >= ConfigLoader.MinPassSize) && ((PROTOCOL_BASE_LOGIN_REQ) this).physicalAddress_0.GetAddressBytes() != new byte[6] && !(((PROTOCOL_BASE_LOGIN_REQ) this).string_2 != config.ClientVersion) && (!config.AccessUFL || !(((PROTOCOL_BASE_LOGIN_REQ) this).string_5 != config.UserFileList)) && !((PROTOCOL_BASE_LOGIN_REQ) this).string_4.Equals("0x0") && !ResolutionJSON.GetDisplay(((PROTOCOL_BASE_LOGIN_REQ) this).string_4).Equals("Invalid"))
        {
          Account eventVisitModel_1 = this.Client.Player = AccountManager.GetAccountDB((object) ((PROTOCOL_BASE_LOGIN_REQ) this).string_0, (object) null, 0, 95);
          if (eventVisitModel_1 == null && ConfigLoader.AutoAccount && !ClanManager.CreateAccount(ref eventVisitModel_1, ((PROTOCOL_BASE_LOGIN_REQ) this).string_0, ((PROTOCOL_BASE_LOGIN_REQ) this).string_1))
          {
            this.Client.SendPacket((AuthServerPacket) new PROTOCOL_BASE_LOGOUT_ACK(EventErrorEnum.LOGIN_DELETE_ACCOUNT, eventVisitModel_1, 0U));
            CLogger.Print($"Failed to create account automatically [{eventVisitModel_1.Username}]", LoggerType.Warning, (Exception) null);
            this.Client.Close(1000, false);
          }
          else
          {
            Account account1 = eventVisitModel_1;
            if (account1 != null && account1.ComparePassword(account1.Password))
            {
              if (!((ChannelModel) account1).IsBanned())
              {
                if (account1.MacAddress != ((PROTOCOL_BASE_LOGIN_REQ) this).physicalAddress_0)
                  ComDiv.UpdateDB("accounts", "mac_address", (object) ((PROTOCOL_BASE_LOGIN_REQ) this).physicalAddress_0, "player_id", (object) account1.PlayerId);
                bool flag1;
                bool flag2;
                DaoManagerSQL.GetBanStatus($"{((PROTOCOL_BASE_LOGIN_REQ) this).physicalAddress_0}", ((PROTOCOL_BASE_LOGIN_REQ) this).string_3, ref flag1, ref flag2);
                if (flag1 | flag2)
                {
                  CLogger.Print($"{(flag1 ? "MAC Address blocked" : "IP4 Address blocked")} [{account1.Username}]", LoggerType.Warning, (Exception) null);
                  this.Client.SendPacket((AuthServerPacket) new PROTOCOL_BASE_LOGOUT_ACK(flag2 ? EventErrorEnum.LOGIN_BLOCK_IP : EventErrorEnum.EVENT_LOG_IN_BLOCK_ACCOUNT, account1, 0U));
                  this.Client.Close(1000, false);
                }
                else if (account1.IsGM() && config.OnlyGM || account1.AuthLevel() >= AccessLevel.NORMAL && !config.OnlyGM)
                {
                  Account account2 = ClanManager.GetAccount(account1.PlayerId, true);
                  if (!account1.IsOnline)
                  {
                    if (DaoManagerSQL.GetAccountBan(account1.BanObjectId).EndDate > DateTimeUtil.Now())
                    {
                      this.Client.SendPacket((AuthServerPacket) new PROTOCOL_BASE_LOGOUT_ACK(EventErrorEnum.EVENT_LOG_IN_BLOCK_ACCOUNT, account1, 0U));
                      CLogger.Print($"Account with ban is Active [{account1.Username}]", LoggerType.Warning, (Exception) null);
                      this.Client.Close(1000, false);
                    }
                    else
                    {
                      account1.Connection = this.Client;
                      ((ChannelModel) account1).SetCountry(((PROTOCOL_BASE_LOGIN_REQ) this).string_3);
                      account1.SetPlayerId(account1.PlayerId, 8159);
                      this.Client.SendPacket((AuthServerPacket) new PROTOCOL_BASE_LOGOUT_ACK(EventErrorEnum.SUCCESS, account1, AuthSync.ValidateKey(account1.PlayerId, this.Client.SessionId, num)));
                      this.Client.SendPacket((AuthServerPacket) new PROTOCOL_MATCH_SERVER_IDX_ACK());
                      this.Client.SendPacket((AuthServerPacket) new PROTOCOL_SEASON_CHALLENGE_SEASON_CHANGE(0U, account1));
                      if (account1.ClanId > 0)
                      {
                        account1.ClanPlayers = \u007B1a8343ff\u002Da99e\u002D485e\u002Dad67\u002D094168725faa\u007D.GetClanPlayers(account1.ClanId, account1.PlayerId);
                        this.Client.SendPacket((AuthServerPacket) new PROTOCOL_MESSENGER_NOTE_LIST_ACK(account1.ClanPlayers));
                      }
                      account1.Status.SetData(uint.MaxValue, account1.PlayerId);
                      account1.Status.UpdateServer((byte) 0);
                      account1.SetOnlineStatus(true);
                      if (account2 != null)
                        account2.Connection = this.Client;
                      this.Client.HeartBeatCounter();
                      AccountInfo.RefreshAccount(account1, true);
                    }
                  }
                  else
                  {
                    this.Client.SendPacket((AuthServerPacket) new PROTOCOL_BASE_LOGOUT_ACK(EventErrorEnum.EVENT_LOG_IN_ALREADY_LOGIN, account1, 0U));
                    CLogger.Print($"Account online [{account1.Username}]", LoggerType.Warning, (Exception) null);
                    if (account2 != null && account2.Connection != null)
                    {
                      account2.SendPacket((AuthServerPacket) new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(1));
                      account2.SendPacket((AuthServerPacket) new Mission(2147487744U /*0x80001000*/));
                      account2.Close(1000);
                    }
                    else
                      UpdateServer.SendLoginKickInfo(account1);
                    this.Client.Close(1000, false);
                  }
                }
                else
                {
                  this.Client.SendPacket((AuthServerPacket) new PROTOCOL_BASE_LOGOUT_ACK(EventErrorEnum.EVENT_LOG_IN_TIME_OUT_2, account1, 0U));
                  CLogger.Print($"Invalid access level [{account1.Username}]", LoggerType.Warning, (Exception) null);
                  this.Client.Close(1000, false);
                }
              }
              else
              {
                this.Client.SendPacket((AuthServerPacket) new PROTOCOL_BASE_LOGOUT_ACK(EventErrorEnum.EVENT_LOG_IN_BLOCK_ACCOUNT, account1, 0U));
                CLogger.Print($"Permanently banned [{account1.Username}]", LoggerType.Warning, (Exception) null);
                this.Client.Close(1000, false);
              }
            }
            else
            {
              string str = "";
              EventErrorEnum account_1 = EventErrorEnum.FAIL;
              if (account1 == null)
              {
                str = "Account returned from DB is null";
                account_1 = EventErrorEnum.LOGIN_DELETE_ACCOUNT;
              }
              else if (!account1.ComparePassword(account1.Password))
              {
                str = "Invalid password";
                account_1 = EventErrorEnum.LOGIN_ID_PASS_INCORRECT;
              }
              this.Client.SendPacket((AuthServerPacket) new PROTOCOL_BASE_LOGOUT_ACK(account_1, account1, 0U));
              CLogger.Print($"{str} [{((PROTOCOL_BASE_LOGIN_REQ) this).string_0}]", LoggerType.Warning, (Exception) null);
              this.Client.Close(1000, false);
            }
          }
        }
        else
        {
          string str;
          if (config == null)
            str = $"Invalid server config [{((PROTOCOL_BASE_LOGIN_REQ) this).string_0}]";
          else if (!ConfigLoader.IsTestMode && !ConfigLoader.GameLocales.Contains(((PROTOCOL_BASE_LOGIN_REQ) this).clientLocale_0))
            str = $"Country: {((PROTOCOL_BASE_LOGIN_REQ) this).clientLocale_0} of blocked client [{((PROTOCOL_BASE_LOGIN_REQ) this).string_0}]";
          else if (((PROTOCOL_BASE_LOGIN_REQ) this).string_0.Length < ConfigLoader.MinUserSize)
            str = $"Username too short [{((PROTOCOL_BASE_LOGIN_REQ) this).string_0}]";
          else if (((PROTOCOL_BASE_LOGIN_REQ) this).string_0.Length > ConfigLoader.MaxUserSize)
            str = $"Username too long [{((PROTOCOL_BASE_LOGIN_REQ) this).string_0}]";
          else if (!ConfigLoader.IsTestMode && ((PROTOCOL_BASE_LOGIN_REQ) this).string_1.Length < ConfigLoader.MinPassSize)
            str = $"Password too short [{((PROTOCOL_BASE_LOGIN_REQ) this).string_0}]";
          else if (!ConfigLoader.IsTestMode && ((PROTOCOL_BASE_LOGIN_REQ) this).string_1.Length > ConfigLoader.MaxPassSize)
            str = $"Password too long [{((PROTOCOL_BASE_LOGIN_REQ) this).string_0}]";
          else if (((PROTOCOL_BASE_LOGIN_REQ) this).physicalAddress_0.GetAddressBytes() == new byte[6])
            str = $"Invalid MAC Address [{((PROTOCOL_BASE_LOGIN_REQ) this).string_0}]";
          else if (((PROTOCOL_BASE_LOGIN_REQ) this).string_2 != config.ClientVersion)
            str = $"Version: {((PROTOCOL_BASE_LOGIN_REQ) this).string_2} not supported [{((PROTOCOL_BASE_LOGIN_REQ) this).string_0}]";
          else if (config.AccessUFL && ((PROTOCOL_BASE_LOGIN_REQ) this).string_5 != config.UserFileList)
            str = $"UserFileList: {((PROTOCOL_BASE_LOGIN_REQ) this).string_5} not supported [{((PROTOCOL_BASE_LOGIN_REQ) this).string_0}]";
          else if (!((PROTOCOL_BASE_LOGIN_REQ) this).string_4.Equals("0x0") && !ResolutionJSON.GetDisplay(((PROTOCOL_BASE_LOGIN_REQ) this).string_4).Equals("Invalid"))
            str = "There is something wrong happened when trying to login " + ((PROTOCOL_BASE_LOGIN_REQ) this).string_0;
          else
            str = $"Invalid {((PROTOCOL_BASE_LOGIN_REQ) this).string_4} resolution [{((PROTOCOL_BASE_LOGIN_REQ) this).string_0}]";
          this.Client.SendPacket((AuthServerPacket) new PROTOCOL_BASE_KEEP_ALIVE_REQ(2147483904U /*0x80000100*/, false));
          CLogger.Print(str, LoggerType.Warning, (Exception) null);
          this.Client.Close(1000, true);
        }
      }
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
  }
}
