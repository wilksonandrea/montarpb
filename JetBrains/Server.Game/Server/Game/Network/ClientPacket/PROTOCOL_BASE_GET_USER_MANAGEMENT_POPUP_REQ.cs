// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BASE_GET_USER_MANAGEMENT_POPUP_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_GET_USER_MANAGEMENT_POPUP_REQ : GameClientPacket
{
  private string string_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      Account account = ClanManager.GetAccount(((PROTOCOL_BASE_GET_RECORD_INFO_DB_REQ) this).long_0, 31 /*0x1F*/);
      if (account == null || player.PlayerId == account.PlayerId)
        return;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_GUIDE_COMPLETE_ACK(account));
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_BASE_GET_USER_BASIC_INFO_REQ) this).long_0 = this.ReadQ();
  }
}
