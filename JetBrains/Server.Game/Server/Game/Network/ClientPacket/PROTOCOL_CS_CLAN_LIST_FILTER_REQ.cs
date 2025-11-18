// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CS_CLAN_LIST_FILTER_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_CLAN_LIST_FILTER_REQ : GameClientPacket
{
  private byte byte_0;
  private byte byte_1;
  private ClanSearchType clanSearchType_0;
  private string string_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      if (player.ClanId > 0)
      {
        ChannelModel channel = player.GetChannel();
        if (channel != null && channel.Type == ChannelType.Clan)
        {
          lock (channel.Matches)
          {
            for (int index = 0; index < channel.Matches.Count; ++index)
            {
              if (channel.Matches[index].Clan.Id == player.ClanId)
                ((PROTOCOL_CS_CLAN_MATCH_RESULT_CONTEXT_REQ) this).int_0 = ((PROTOCOL_CS_CLAN_MATCH_RESULT_CONTEXT_REQ) this).int_0 + 1;
            }
          }
        }
      }
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_CLAN_LIST_FILTER_ACK(((PROTOCOL_CS_CLAN_MATCH_RESULT_CONTEXT_REQ) this).int_0));
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_CS_CLAN_MATCH_RESULT_LIST_REQ) this).int_0 = (int) this.ReadC();
  }

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      if (player.ClanId > 0)
      {
        ChannelModel channel = player.GetChannel();
        if (channel != null && channel.Type == ChannelType.Clan)
        {
          lock (channel.Matches)
          {
            foreach (MatchModel match in channel.Matches)
            {
              if (match.Clan.Id == player.ClanId)
                ((PROTOCOL_CS_CLAN_MATCH_RESULT_LIST_REQ) this).list_0.Add(match);
            }
          }
        }
      }
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_CLIENT_ENTER_ACK(player.ClanId == 0 ? 91 : 0, ((PROTOCOL_CS_CLAN_MATCH_RESULT_LIST_REQ) this).list_0));
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }
}
