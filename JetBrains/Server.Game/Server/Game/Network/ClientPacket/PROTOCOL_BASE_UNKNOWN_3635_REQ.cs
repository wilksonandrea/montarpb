// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BASE_UNKNOWN_3635_REQ
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

public class PROTOCOL_BASE_UNKNOWN_3635_REQ : GameClientPacket
{
  private byte byte_0;
  private byte byte_1;
  private byte byte_2;
  private string string_0;
  private short short_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      Account accountDb = AccountManager.GetAccountDB((object) ((PROTOCOL_BASE_GET_USER_BASIC_INFO_REQ) this).long_0, 2, 31 /*0x1F*/);
      if (accountDb != null && player.Nickname.Length > 0 && player.PlayerId != ((PROTOCOL_BASE_GET_USER_BASIC_INFO_REQ) this).long_0)
      {
        if (player.Nickname != accountDb.Nickname)
          player.FindPlayer = accountDb.Nickname;
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_MATCH_CLAN_SEASON_ACK(((PROTOCOL_BASE_GET_USER_BASIC_INFO_REQ) this).uint_0, accountDb, int.MaxValue));
      }
      else
        ((PROTOCOL_BASE_GET_USER_BASIC_INFO_REQ) this).uint_0 = 2147489795U;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_INV_ITEM_DATA_ACK(((PROTOCOL_BASE_GET_USER_BASIC_INFO_REQ) this).uint_0));
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_BASE_GET_USER_MANAGEMENT_POPUP_REQ) this).string_0 = this.ReadU(33);
  }
}
