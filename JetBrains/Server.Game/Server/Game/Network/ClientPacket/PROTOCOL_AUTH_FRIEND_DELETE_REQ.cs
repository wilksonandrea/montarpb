// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_AUTH_FRIEND_DELETE_REQ
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

public class PROTOCOL_AUTH_FRIEND_DELETE_REQ : GameClientPacket
{
  private int int_0;
  private uint uint_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      Account account = ClanManager.GetAccount(((PROTOCOL_AUTH_FIND_USER_REQ) this).string_0, 1, 286);
      if (account != null && player.Nickname.Length > 0 && player.Nickname != ((PROTOCOL_AUTH_FIND_USER_REQ) this).string_0)
      {
        if (player.Nickname != account.Nickname)
          player.FindPlayer = account.Nickname;
      }
      else
        ((PROTOCOL_AUTH_FIND_USER_REQ) this).uint_0 = 2147489795U;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_MATCH_CLAN_SEASON_ACK(((PROTOCOL_AUTH_FIND_USER_REQ) this).uint_0, account, int.MaxValue));
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read() => ((PROTOCOL_AUTH_FRIEND_ACCEPT_REQ) this).int_0 = (int) this.ReadC();
}
