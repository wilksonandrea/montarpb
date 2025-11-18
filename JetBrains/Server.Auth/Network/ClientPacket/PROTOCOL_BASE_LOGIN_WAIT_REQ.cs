// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ClientPacket.PROTOCOL_BASE_LOGIN_WAIT_REQ
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Server.Auth.Data.Models;
using Server.Auth.Data.Utils;
using Server.Auth.Network.ServerPacket;
using System;
using System.Net.NetworkInformation;

#nullable disable
namespace Server.Auth.Network.ClientPacket;

public class PROTOCOL_BASE_LOGIN_WAIT_REQ : AuthClientPacket
{
  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null || player.Inventory.Items.Count != 0)
        return;
      AllUtils.ValidateAuthLevel(player);
      AllUtils.LoadPlayerInventory(player);
      AllUtils.LoadPlayerMissions(player);
      AllUtils.ValidatePlayerInventoryStatus(player);
      AllUtils.DiscountPlayerItems(player);
      AllUtils.CheckGameEvents(player);
      this.Client.SendPacket((AuthServerPacket) new PROTOCOL_BASE_GET_USER_INFO_ACK(player));
      this.Client.SendPacket((AuthServerPacket) new PROTOCOL_BASE_GET_OPTION_ACK(player));
      AllUtils.ProcessBattlepass(player);
      this.Client.SendPacket((AuthServerPacket) new PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK());
      this.Client.SendPacket((AuthServerPacket) new PROTOCOL_BASE_BOOSTEVENT_INFO_ACK(player));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_BASE_GET_USER_INFO_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_BASE_LOGIN_REQ) this).byte_0 = this.ReadC();
    ((PROTOCOL_BASE_LOGIN_REQ) this).byte_1 = this.ReadC();
    ((PROTOCOL_BASE_LOGIN_REQ) this).byte_2 = this.ReadC();
    ((PROTOCOL_BASE_LOGIN_REQ) this).byte_3 = this.ReadC();
    ((PROTOCOL_BASE_LOGIN_REQ) this).physicalAddress_0 = new PhysicalAddress(this.ReadB(6));
    this.ReadB(20);
    ((PROTOCOL_BASE_LOGIN_REQ) this).string_4 = $"{this.ReadH()}x{this.ReadH()}";
    this.ReadB(9);
    ((PROTOCOL_BASE_LOGIN_REQ) this).string_5 = this.ReadS((int) this.ReadC());
    this.ReadB(16 /*0x10*/);
    ((PROTOCOL_BASE_LOGIN_REQ) this).clientLocale_0 = (ClientLocale) this.ReadC();
    ((PROTOCOL_BASE_LOGIN_REQ) this).string_2 = $"{this.ReadC()}.{this.ReadH()}";
    int num = (int) this.ReadH();
    ((PROTOCOL_BASE_LOGIN_REQ) this).string_1 = this.ReadS((int) this.ReadC());
    ((PROTOCOL_BASE_LOGIN_REQ) this).string_0 = this.ReadS((int) this.ReadC());
    ((PROTOCOL_BASE_LOGIN_REQ) this).string_3 = this.Client.GetIPAddress();
  }
}
