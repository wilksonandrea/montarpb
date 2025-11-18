// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BASE_RANDOMBOX_LIST_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_RANDOMBOX_LIST_REQ : GameClientPacket
{
  private string string_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      PlayerMissions mission = player.Mission;
      bool flag = false;
      if (((PROTOCOL_BASE_QUEST_DELETE_CARD_SET_REQ) this).int_0 >= 3 || ((PROTOCOL_BASE_QUEST_DELETE_CARD_SET_REQ) this).int_0 == 0 && mission.Mission1 == 0 || ((PROTOCOL_BASE_QUEST_DELETE_CARD_SET_REQ) this).int_0 == 1 && mission.Mission2 == 0 || ((PROTOCOL_BASE_QUEST_DELETE_CARD_SET_REQ) this).int_0 == 2 && mission.Mission3 == 0)
        flag = true;
      if (!flag && DaoManagerSQL.UpdatePlayerMissionId(player.PlayerId, 0, ((PROTOCOL_BASE_QUEST_DELETE_CARD_SET_REQ) this).int_0))
      {
        if (ComDiv.UpdateDB("player_missions", "owner_id", (object) player.PlayerId, new string[2]
        {
          $"card{((PROTOCOL_BASE_QUEST_DELETE_CARD_SET_REQ) this).int_0 + 1}",
          $"mission{((PROTOCOL_BASE_QUEST_DELETE_CARD_SET_REQ) this).int_0 + 1}_raw"
        }, new object[2]{ (object) 0, (object) new byte[0] }))
        {
          if (((PROTOCOL_BASE_QUEST_DELETE_CARD_SET_REQ) this).int_0 == 0)
          {
            mission.Mission1 = 0;
            mission.Card1 = 0;
            mission.List1 = new byte[40];
            goto label_12;
          }
          if (((PROTOCOL_BASE_QUEST_DELETE_CARD_SET_REQ) this).int_0 == 1)
          {
            mission.Mission2 = 0;
            mission.Card2 = 0;
            mission.List2 = new byte[40];
            goto label_12;
          }
          if (((PROTOCOL_BASE_QUEST_DELETE_CARD_SET_REQ) this).int_0 == 2)
          {
            mission.Mission3 = 0;
            mission.Card3 = 0;
            mission.List3 = new byte[40];
            goto label_12;
          }
          goto label_12;
        }
      }
      ((PROTOCOL_BASE_QUEST_DELETE_CARD_SET_REQ) this).uint_0 = 2147487824U /*0x80001050*/;
label_12:
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_GET_USER_SUBTASK_ACK(((PROTOCOL_BASE_QUEST_DELETE_CARD_SET_REQ) this).uint_0, player));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_BASE_QUEST_DELETE_CARD_SET_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read() => ((PROTOCOL_BASE_GET_USER_SUBTASK_REQ) this).int_0 = this.ReadD();
}
