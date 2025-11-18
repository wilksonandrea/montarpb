// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ClientPacket.PROTOCOL_BASE_LOGIN_REQ
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Auth.Data.Models;
using Server.Auth.Network.ServerPacket;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

#nullable disable
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

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      ServerConfig config = AuthXender.Client.Config;
      if (config != null)
      {
        this.Client.SendPacket((AuthServerPacket) new PROTOCOL_BASE_URL_LIST_ACK(config));
        if (config.OfficialBannerEnabled)
          this.Client.SendPacket((AuthServerPacket) new PROTOCOL_BASE_USER_GIFTLIST_ACK(config));
        this.Client.SendPacket((AuthServerPacket) new PROTOCOL_BASE_CHANNELTYPE_CONDITION_ACK());
        this.Client.SendPacket((AuthServerPacket) new PROTOCOL_BASE_DAILY_RECORD_ACK());
        this.Client.SendPacket((AuthServerPacket) new PROTOCOL_BASE_GET_USER_INFO_ACK(config));
        this.Client.SendPacket((AuthServerPacket) new PROTOCOL_CS_MEDAL_INFO_ACK());
      }
      if (!player.MyConfigsLoaded)
        this.Client.SendPacket((AuthServerPacket) new PROTOCOL_BASE_GET_SYSTEM_INFO_ACK(0, player.Config));
      List<MessageModel> giftMessages = DaoManagerSQL.GetGiftMessages(player.PlayerId);
      if (giftMessages.Count <= 0)
        return;
      DaoManagerSQL.RecycleMessages(player.PlayerId, giftMessages);
      if (giftMessages.Count <= 0)
        return;
      this.Client.SendPacket((AuthServerPacket) new PROTOCOL_CS_MEMBER_INFO_ACK(0, giftMessages));
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
