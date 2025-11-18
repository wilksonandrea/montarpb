// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_MATCH_SERVER_IDX_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_MATCH_SERVER_IDX_REQ : GameClientPacket
{
  private short short_0;

  public virtual void Run()
  {
    try
    {
      Account player1 = this.Client.Player;
      if (player1 == null)
        return;
      PlayerSession player2 = player1.GetChannel().GetPlayer(((PROTOCOL_BASE_GET_USER_DETAIL_INFO_REQ) this).int_0);
      if (player2 == null)
        return;
      Account account = ClanManager.GetAccount(player2.PlayerId, true);
      if (account == null)
        return;
      if (player1.Nickname != account.Nickname)
        player1.FindPlayer = account.Nickname;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_MATCH_CLAN_SEASON_ACK(0U, account, int.MaxValue));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_LOBBY_USER_STATISTICS_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
  }
}
