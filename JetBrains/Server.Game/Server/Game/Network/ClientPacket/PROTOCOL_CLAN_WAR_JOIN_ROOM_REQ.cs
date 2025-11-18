// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CLAN_WAR_JOIN_ROOM_REQ
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

public class PROTOCOL_CLAN_WAR_JOIN_ROOM_REQ : GameClientPacket
{
  private int int_0;
  private int int_1;
  private TeamEnum teamEnum_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      MatchModel match1 = player.Match;
      MatchModel match2 = AllUtils.GetChannel(((PROTOCOL_CLAN_WAR_INVITE_ACCEPT_REQ) this).int_1, ((PROTOCOL_CLAN_WAR_INVITE_ACCEPT_REQ) this).int_1 - ((PROTOCOL_CLAN_WAR_INVITE_ACCEPT_REQ) this).int_1 / 10 * 10).GetMatch(((PROTOCOL_CLAN_WAR_INVITE_ACCEPT_REQ) this).int_0);
      if (match1 != null && match2 != null && player.MatchSlot == match1.Leader)
      {
        if (((PROTOCOL_CLAN_WAR_INVITE_ACCEPT_REQ) this).int_2 == 1)
        {
          if (match1.Training != match2.Training)
            ((PROTOCOL_CLAN_WAR_INVITE_ACCEPT_REQ) this).uint_0 = 2147487890U;
          else if (((RoomModel) match2).GetCountPlayers() == match1.Training && ((RoomModel) match1).GetCountPlayers() == match1.Training)
          {
            if (match2.State != MatchState.Play && match1.State != MatchState.Play)
            {
              match1.State = MatchState.Play;
              Account leader = ((RoomModel) match2).GetLeader();
              if (leader != null && leader.Match != null)
              {
                leader.SendPacket((GameServerPacket) new PROTOCOL_CLAN_WAR_INVITE_MERCENARY_RECEIVER_ACK(match1));
                leader.SendPacket((GameServerPacket) new PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK(match1));
                match2.Slots[leader.MatchSlot].State = SlotMatchState.Ready;
              }
              match2.State = MatchState.Play;
            }
            else
              ((PROTOCOL_CLAN_WAR_INVITE_ACCEPT_REQ) this).uint_0 = 2147487888U /*0x80001090*/;
          }
          else
            ((PROTOCOL_CLAN_WAR_INVITE_ACCEPT_REQ) this).uint_0 = 2147487889U;
        }
        else
        {
          Account leader = ((RoomModel) match2).GetLeader();
          if (leader != null && leader.Match != null)
            leader.SendPacket((GameServerPacket) new PROTOCOL_CLAN_WAR_JOIN_ROOM_ACK(2147487891U));
        }
      }
      else
        ((PROTOCOL_CLAN_WAR_INVITE_ACCEPT_REQ) this).uint_0 = 2147487892U;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_CLAN_WAR_CHANGE_MAX_PER_ACK(((PROTOCOL_CLAN_WAR_INVITE_ACCEPT_REQ) this).uint_0));
    }
    catch (Exception ex)
    {
      CLogger.Print("CLAN_WAR_ACCEPT_BATTLE_REC: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_CLAN_WAR_INVITE_MERCENARY_RECEIVER_REQ) this).int_0 = (int) this.ReadC();
  }
}
