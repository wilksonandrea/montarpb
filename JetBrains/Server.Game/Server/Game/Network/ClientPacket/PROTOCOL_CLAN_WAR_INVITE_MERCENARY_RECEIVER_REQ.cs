// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CLAN_WAR_INVITE_MERCENARY_RECEIVER_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CLAN_WAR_INVITE_MERCENARY_RECEIVER_REQ : GameClientPacket
{
  private int int_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      ChannelModel channel = player.GetChannel();
      if (channel != null && channel.Type == ChannelType.Clan && player.Room == null)
      {
        if (player.Match != null)
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_CLAN_WAR_INVITE_ACCEPT_ACK(2147487879U, (MatchModel) null));
        else if (player.ClanId == 0)
        {
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_CLAN_WAR_INVITE_ACCEPT_ACK(2147487835U, (MatchModel) null));
        }
        else
        {
          int num1 = -1;
          int num2 = -1;
          lock (channel.Matches)
          {
            for (int index = 0; index < 250; ++index)
            {
              if (channel.GetMatch(index) == null)
              {
                num1 = index;
                break;
              }
            }
            for (int index = 0; index < channel.Matches.Count; ++index)
            {
              MatchModel match = channel.Matches[index];
              if (match.Clan.Id == player.ClanId)
                ((PROTOCOL_CLAN_WAR_CREATE_TEAM_REQ) this).list_0.Add(match.FriendId);
            }
          }
          for (int index = 0; index < 25; ++index)
          {
            if (!((PROTOCOL_CLAN_WAR_CREATE_TEAM_REQ) this).list_0.Contains(index))
            {
              num2 = index;
              break;
            }
          }
          if (num1 == -1)
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_CLAN_WAR_INVITE_ACCEPT_ACK(2147487880U, (MatchModel) null));
          else if (num2 == -1)
          {
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_CLAN_WAR_INVITE_ACCEPT_ACK(2147487881U, (MatchModel) null));
          }
          else
          {
            MatchModel matchModel = new MatchModel(ClanManager.GetClan(player.ClanId))
            {
              MatchId = num1,
              FriendId = num2,
              Training = ((PROTOCOL_CLAN_WAR_CREATE_TEAM_REQ) this).int_0,
              ChannelId = player.ChannelId,
              ServerId = player.ServerId
            };
            matchModel.AddPlayer(player);
            channel.AddMatch(matchModel);
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_CLAN_WAR_INVITE_ACCEPT_ACK(0U, matchModel));
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_CLAN_WAR_TEAM_CHATTING_ACK(matchModel));
          }
        }
      }
      else
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_CLAN_WAR_INVITE_ACCEPT_ACK(2147483648U /*0x80000000*/, (MatchModel) null));
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public PROTOCOL_CLAN_WAR_INVITE_MERCENARY_RECEIVER_REQ()
  {
    ((PROTOCOL_CLAN_WAR_CREATE_TEAM_REQ) this).list_0 = new List<int>();
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  public virtual void Read()
  {
    this.ReadD();
    ((PROTOCOL_CLAN_WAR_INVITE_ACCEPT_REQ) this).int_0 = (int) this.ReadH();
    ((PROTOCOL_CLAN_WAR_INVITE_ACCEPT_REQ) this).int_1 = (int) this.ReadH();
    ((PROTOCOL_CLAN_WAR_INVITE_ACCEPT_REQ) this).int_2 = (int) this.ReadC();
  }
}
