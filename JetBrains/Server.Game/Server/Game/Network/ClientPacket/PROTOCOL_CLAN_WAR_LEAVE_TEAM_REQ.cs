// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CLAN_WAR_LEAVE_TEAM_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CLAN_WAR_LEAVE_TEAM_REQ : GameClientPacket
{
  private uint uint_0;

  public virtual void Read()
  {
    ((PROTOCOL_CLAN_WAR_JOIN_TEAM_REQ) this).int_0 = (int) this.ReadH();
    ((PROTOCOL_CLAN_WAR_JOIN_TEAM_REQ) this).int_1 = (int) this.ReadH();
    ((PROTOCOL_CLAN_WAR_JOIN_TEAM_REQ) this).int_2 = (int) this.ReadC();
  }

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (((PROTOCOL_CLAN_WAR_JOIN_TEAM_REQ) this).int_2 < 2 && player != null && player.Match == null && player.Room == null)
      {
        int num = ((PROTOCOL_CLAN_WAR_JOIN_TEAM_REQ) this).int_1 - ((PROTOCOL_CLAN_WAR_JOIN_TEAM_REQ) this).int_1 / 10 * 10;
        ChannelModel channel = AllUtils.GetChannel(((PROTOCOL_CLAN_WAR_JOIN_TEAM_REQ) this).int_1, ((PROTOCOL_CLAN_WAR_JOIN_TEAM_REQ) this).int_2 == 0 ? num : player.ChannelId);
        if (channel != null)
        {
          if (player.ClanId == 0)
          {
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_CLAN_WAR_MATCH_PROPOSE_ACK(2147487835U, (MatchModel) null));
          }
          else
          {
            MatchModel bool_0 = ((PROTOCOL_CLAN_WAR_JOIN_TEAM_REQ) this).int_2 == 1 ? ((MatchModel) channel).GetMatch(((PROTOCOL_CLAN_WAR_JOIN_TEAM_REQ) this).int_0, player.ClanId) : channel.GetMatch(((PROTOCOL_CLAN_WAR_JOIN_TEAM_REQ) this).int_0);
            if (bool_0 != null)
              ((PROTOCOL_CLAN_WAR_MATCH_PROPOSE_REQ) this).method_0(player, bool_0);
            else
              this.Client.SendPacket((GameServerPacket) new PROTOCOL_CLAN_WAR_MATCH_PROPOSE_ACK(2147483648U /*0x80000000*/, (MatchModel) null));
          }
        }
        else
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_CLAN_WAR_MATCH_PROPOSE_ACK(2147483648U /*0x80000000*/, (MatchModel) null));
      }
      else
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_CLAN_WAR_MATCH_PROPOSE_ACK(2147483648U /*0x80000000*/, (MatchModel) null));
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }
}
