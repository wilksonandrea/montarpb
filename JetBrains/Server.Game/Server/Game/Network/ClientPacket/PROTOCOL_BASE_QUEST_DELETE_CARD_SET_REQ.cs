// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BASE_QUEST_DELETE_CARD_SET_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_QUEST_DELETE_CARD_SET_REQ : GameClientPacket
{
  private uint uint_0;
  private int int_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      DBQuery dbQuery = new DBQuery();
      PlayerMissions mission = player.Mission;
      if (mission.GetCard(((PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_REQ) this).int_1) != ((PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_REQ) this).int_0)
      {
        if (((PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_REQ) this).int_1 == 0)
          mission.Card1 = ((PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_REQ) this).int_0;
        else if (((PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_REQ) this).int_1 == 1)
          mission.Card2 = ((PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_REQ) this).int_0;
        else if (((PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_REQ) this).int_1 == 2)
          mission.Card3 = ((PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_REQ) this).int_0;
        else if (((PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_REQ) this).int_1 == 3)
          mission.Card4 = ((PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_REQ) this).int_0;
        dbQuery.AddQuery($"card{((PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_REQ) this).int_1 + 1}", (object) ((PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_REQ) this).int_0);
      }
      mission.SelectedCard = ((PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_REQ) this).int_2 == (int) ushort.MaxValue;
      if (mission.ActualMission != ((PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_REQ) this).int_1)
      {
        dbQuery.AddQuery("current_mission", (object) ((PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_REQ) this).int_1);
        mission.ActualMission = ((PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_REQ) this).int_1;
      }
      ComDiv.UpdateDB("player_missions", "owner_id", (object) this.Client.PlayerId, dbQuery.GetTables(), dbQuery.GetValues());
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_BASE_QUEST_BUY_CARD_SET_REQ) this).int_0 = (int) this.ReadC();
    int num = (int) this.ReadC();
  }
}
