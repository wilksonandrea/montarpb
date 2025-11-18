// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CS_SECESSION_CLAN_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_SECESSION_CLAN_REQ : GameClientPacket
{
  private uint uint_0;

  private void method_0(ClanInvite account_0, SyncServerPacket syncServerPacket_0)
  {
    syncServerPacket_0.WriteQ(account_0.PlayerId);
    Account account = ClanManager.GetAccount(account_0.PlayerId, 31 /*0x1F*/);
    if (account != null)
    {
      syncServerPacket_0.WriteU(account.Nickname, 66);
      syncServerPacket_0.WriteC((byte) account.Rank);
      syncServerPacket_0.WriteC((byte) account.NickColor);
      syncServerPacket_0.WriteD(account_0.InviteDate);
      syncServerPacket_0.WriteD(account.Statistic.Basic.KillsCount);
      syncServerPacket_0.WriteD(account.Statistic.Basic.DeathsCount);
      syncServerPacket_0.WriteD(account.Statistic.Basic.Matches);
      syncServerPacket_0.WriteD(account.Statistic.Basic.MatchWins);
      syncServerPacket_0.WriteD(account.Statistic.Basic.MatchLoses);
      syncServerPacket_0.WriteN(account_0.Text, account_0.Text.Length + 2, "UTF-16LE");
    }
    syncServerPacket_0.WriteD(account_0.InviteDate);
  }

  public virtual void Read() => ((PROTOCOL_CS_ROOM_INVITED_REQ) this).long_0 = this.ReadQ();

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null || player.ClanId == 0)
        return;
      Account account = ClanManager.GetAccount(((PROTOCOL_CS_ROOM_INVITED_REQ) this).long_0, 31 /*0x1F*/);
      if (account != null && account.ClanId == player.ClanId)
        account.SendPacket((GameServerPacket) new PROTOCOL_GMCHAT_END_CHAT_ACK(this.Client.PlayerId), false);
      player.SendPacket((GameServerPacket) new PROTOCOL_CS_SECESSION_CLAN_ACK(0));
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }
}
