// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BASE_USER_TITLE_RELEASE_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_USER_TITLE_RELEASE_REQ : GameClientPacket
{
  private int int_0;
  private int int_1;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null || ((PROTOCOL_BASE_USER_TITLE_CHANGE_REQ) this).int_0 >= 45)
        return;
      if (player.Title.OwnerId == 0L)
      {
        DaoManagerSQL.CreatePlayerTitlesDB(player.PlayerId);
        player.Title = new PlayerTitles()
        {
          OwnerId = player.PlayerId
        };
      }
      TitleModel title = TitleSystemXML.GetTitle(((PROTOCOL_BASE_USER_TITLE_CHANGE_REQ) this).int_0, true);
      if (title != null)
      {
        TitleModel titleModel1;
        TitleModel titleModel2;
        TitleSystemXML.Get2Titles(title.Req1, title.Req2, ref titleModel1, ref titleModel2, false);
        if ((title.Req1 == 0 || titleModel1 != null) && (title.Req2 == 0 || titleModel2 != null) && player.Rank >= title.Rank && player.Ribbon >= title.Ribbon && player.Medal >= title.Medal && player.MasterMedal >= title.MasterMedal && player.Ensign >= title.Ensign && !player.Title.Contains(title.Flag) && player.Title.Contains(titleModel1.Flag) && player.Title.Contains(titleModel2.Flag))
        {
          player.Ribbon -= title.Ribbon;
          player.Medal -= title.Medal;
          player.MasterMedal -= title.MasterMedal;
          player.Ensign -= title.Ensign;
          long ObjectId = player.Title.Add(title.Flag);
          DaoManagerSQL.UpdatePlayerTitlesFlags(player.PlayerId, ObjectId);
          List<ItemsModel> awards = TitleAwardXML.GetAwards(((PROTOCOL_BASE_USER_TITLE_CHANGE_REQ) this).int_0);
          if (awards.Count > 0)
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_LOBBY_CHATTING_ACK(0, player, awards));
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_QUEST_BUY_CARD_SET_ACK(player));
          DaoManagerSQL.UpdatePlayerTitleRequi(player.PlayerId, player.Medal, player.Ensign, player.MasterMedal, player.Ribbon);
          if (player.Title.Slots < title.Slot)
          {
            player.Title.Slots = title.Slot;
            ComDiv.UpdateDB("player_titles", "slots", (object) player.Title.Slots, "owner_id", (object) player.PlayerId);
          }
        }
        else
          ((PROTOCOL_BASE_USER_TITLE_CHANGE_REQ) this).uint_0 = 2147487875U;
      }
      else
        ((PROTOCOL_BASE_USER_TITLE_CHANGE_REQ) this).uint_0 = 2147487875U;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_USER_TITLE_INFO_ACK(((PROTOCOL_BASE_USER_TITLE_CHANGE_REQ) this).uint_0, player.Title.Slots));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_BASE_USER_TITLE_CHANGE_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_BASE_USER_TITLE_EQUIP_REQ) this).byte_0 = this.ReadC();
    ((PROTOCOL_BASE_USER_TITLE_EQUIP_REQ) this).byte_1 = this.ReadC();
  }
}
