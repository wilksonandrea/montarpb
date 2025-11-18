// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BASE_GET_USER_SUBTASK_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_GET_USER_SUBTASK_REQ : GameClientPacket
{
  private int int_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      PlayerMissions mission1 = player.Mission;
      if (mission1 != null && mission1.Mission1 != ((PROTOCOL_BASE_QUEST_BUY_CARD_SET_REQ) this).int_0 && mission1.Mission2 != ((PROTOCOL_BASE_QUEST_BUY_CARD_SET_REQ) this).int_0 && mission1.Mission3 != ((PROTOCOL_BASE_QUEST_BUY_CARD_SET_REQ) this).int_0)
      {
        MissionStore mission2 = MissionConfigXML.GetMission(((PROTOCOL_BASE_QUEST_BUY_CARD_SET_REQ) this).int_0);
        if (mission2 != null && ShopManager.GetItemId(mission2.ItemId) != null)
        {
          if (mission1.Mission1 == 0)
          {
            if (DaoManagerSQL.UpdatePlayerMissionId(player.PlayerId, ((PROTOCOL_BASE_QUEST_BUY_CARD_SET_REQ) this).int_0, 0))
            {
              mission1.Mission1 = ((PROTOCOL_BASE_QUEST_BUY_CARD_SET_REQ) this).int_0;
              mission1.List1 = new byte[40];
              mission1.ActualMission = 0;
              mission1.Card1 = 0;
            }
            else
              ((PROTOCOL_BASE_QUEST_BUY_CARD_SET_REQ) this).eventErrorEnum_0 = EventErrorEnum.MISSION_FAIL_BUY_CARD_BY_NO_CARD_INFO;
          }
          else if (mission1.Mission2 == 0)
          {
            if (DaoManagerSQL.UpdatePlayerMissionId(player.PlayerId, ((PROTOCOL_BASE_QUEST_BUY_CARD_SET_REQ) this).int_0, 1))
            {
              mission1.Mission2 = ((PROTOCOL_BASE_QUEST_BUY_CARD_SET_REQ) this).int_0;
              mission1.List2 = new byte[40];
              mission1.ActualMission = 1;
              mission1.Card2 = 0;
            }
            else
              ((PROTOCOL_BASE_QUEST_BUY_CARD_SET_REQ) this).eventErrorEnum_0 = EventErrorEnum.MISSION_FAIL_BUY_CARD_BY_NO_CARD_INFO;
          }
          else if (mission1.Mission3 == 0)
          {
            if (DaoManagerSQL.UpdatePlayerMissionId(player.PlayerId, ((PROTOCOL_BASE_QUEST_BUY_CARD_SET_REQ) this).int_0, 2))
            {
              mission1.Mission3 = ((PROTOCOL_BASE_QUEST_BUY_CARD_SET_REQ) this).int_0;
              mission1.List3 = new byte[40];
              mission1.ActualMission = 2;
              mission1.Card3 = 0;
            }
            else
              ((PROTOCOL_BASE_QUEST_BUY_CARD_SET_REQ) this).eventErrorEnum_0 = EventErrorEnum.MISSION_FAIL_BUY_CARD_BY_NO_CARD_INFO;
          }
          else
            ((PROTOCOL_BASE_QUEST_BUY_CARD_SET_REQ) this).eventErrorEnum_0 = EventErrorEnum.MISSION_LIMIT_CARD_COUNT;
        }
        else
        {
          CLogger.Print("There is an error on Mission Config. Please check the configuration!", LoggerType.Warning, (Exception) null);
          ((PROTOCOL_BASE_QUEST_BUY_CARD_SET_REQ) this).eventErrorEnum_0 = EventErrorEnum.MISSION_NO_POINT_TO_GET_ITEM;
        }
        int priceGold = ShopManager.GetItemId(mission2.ItemId).PriceGold;
        if (((PROTOCOL_BASE_QUEST_BUY_CARD_SET_REQ) this).eventErrorEnum_0 == EventErrorEnum.SUCCESS && 0 <= player.Gold - priceGold)
        {
          if (priceGold != 0 && !DaoManagerSQL.UpdateAccountGold(player.PlayerId, player.Gold - priceGold))
            ((PROTOCOL_BASE_QUEST_BUY_CARD_SET_REQ) this).eventErrorEnum_0 = EventErrorEnum.MISSION_FAIL_BUY_CARD_BY_NO_CARD_INFO;
          else
            player.Gold -= priceGold;
        }
        else
          ((PROTOCOL_BASE_QUEST_BUY_CARD_SET_REQ) this).eventErrorEnum_0 = EventErrorEnum.MISSION_NO_POINT_TO_GET_ITEM;
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_QUEST_DELETE_CARD_SET_ACK(((PROTOCOL_BASE_QUEST_BUY_CARD_SET_REQ) this).eventErrorEnum_0, player));
      }
      else
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_QUEST_DELETE_CARD_SET_ACK(EventErrorEnum.MISSION_NO_POINT_TO_GET_ITEM, (Account) null));
    }
    catch (Exception ex)
    {
      CLogger.Print($"{this.GetType().Name}: {ex.Message}", LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_BASE_QUEST_DELETE_CARD_SET_REQ) this).int_0 = (int) this.ReadC();
  }
}
