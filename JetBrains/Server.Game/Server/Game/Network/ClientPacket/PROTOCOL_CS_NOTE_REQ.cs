// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CS_NOTE_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_NOTE_REQ : GameClientPacket
{
  private int int_0;
  private string string_0;

  public virtual void Read() => ((PROTOCOL_CS_MEMBER_LIST_REQ) this).byte_0 = this.ReadC();

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      int num1 = player.ClanId == 0 ? player.FindClanId : player.ClanId;
      if (ClanManager.GetClan(num1).Id == 0)
      {
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_PAGE_CHATTING_ACK(uint.MaxValue, byte.MaxValue, byte.MaxValue, new byte[0]));
      }
      else
      {
        List<Account> clanPlayers = ClanManager.GetClanPlayers(num1, -1L, false);
        using (SyncServerPacket int_0 = new SyncServerPacket())
        {
          byte num2 = 0;
          for (int index = (int) ((PROTOCOL_CS_MEMBER_LIST_REQ) this).byte_0 * 14; index < clanPlayers.Count; ++index)
          {
            this.method_0(clanPlayers[index], int_0);
            if (++num2 == (byte) 14)
              break;
          }
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_PAGE_CHATTING_ACK(0U, ((PROTOCOL_CS_MEMBER_LIST_REQ) this).byte_0, num2, int_0.ToArray()));
        }
      }
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_CS_MEMBER_LIST_REQ " + ex.Message, LoggerType.Error, ex);
    }
  }

  private void method_0(Account account_0, SyncServerPacket int_0)
  {
    int_0.WriteQ(account_0.PlayerId);
    int_0.WriteU(account_0.Nickname, 66);
    int_0.WriteC((byte) account_0.Rank);
    int_0.WriteC((byte) account_0.ClanAccess);
    int_0.WriteQ(ComDiv.GetClanStatus(account_0.Status, account_0.IsOnline));
    int_0.WriteD(account_0.ClanDate);
    int_0.WriteC((byte) account_0.NickColor);
    int_0.WriteD(account_0.Statistic.Clan.MatchWins);
    int_0.WriteD(account_0.Statistic.Clan.MatchLoses);
    int_0.WriteD(account_0.Equipment.NameCardId);
    int_0.WriteC((byte) 0);
    int_0.WriteD(10);
    int_0.WriteD(20);
    int_0.WriteD(30);
  }
}
