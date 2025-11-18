// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BASE_ENTER_PASS_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Filters;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_ENTER_PASS_REQ : GameClientPacket
{
  private int int_0;
  private string string_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      if (player.Nickname.Length == 0 && !string.IsNullOrEmpty(((PROTOCOL_BASE_CREATE_NICK_REQ) this).string_0) && ((PROTOCOL_BASE_CREATE_NICK_REQ) this).string_0.Length >= ConfigLoader.MinNickSize && ((PROTOCOL_BASE_CREATE_NICK_REQ) this).string_0.Length <= ConfigLoader.MaxNickSize)
      {
        foreach (string filter in NickFilter.Filters)
        {
          if (((PROTOCOL_BASE_CREATE_NICK_REQ) this).string_0.Contains(filter))
          {
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_GAMEGUARD_ACK(2147487763U, (Account) null));
            break;
          }
        }
        if (!DaoManagerSQL.IsPlayerNameExist(((PROTOCOL_BASE_CREATE_NICK_REQ) this).string_0))
        {
          if (ClanManager.UpdatePlayerName(((PROTOCOL_BASE_CREATE_NICK_REQ) this).string_0, player.PlayerId))
          {
            DaoManagerSQL.CreatePlayerNickHistory(player.PlayerId, player.Nickname, ((PROTOCOL_BASE_CREATE_NICK_REQ) this).string_0, "First nick choosed");
            player.Nickname = ((PROTOCOL_BASE_CREATE_NICK_REQ) this).string_0;
            List<ItemsModel> basics = TemplatePackXML.Basics;
            if (basics.Count > 0)
            {
              foreach (ItemsModel Opening in basics)
              {
                if (ComDiv.GetIdStatics(Opening.Id, 1) == 6 && player.Character.GetCharacter(Opening.Id) == null)
                  AllUtils.CreateCharacter(player, Opening);
              }
            }
            List<ItemsModel> awards = TemplatePackXML.Awards;
            if (awards.Count > 0)
            {
              foreach (ItemsModel itemsModel in awards)
              {
                if (ComDiv.GetIdStatics(itemsModel.Id, 1) == 6 && player.Character.GetCharacter(itemsModel.Id) == null)
                  AllUtils.CreateCharacter(player, itemsModel);
                else
                  this.Client.SendPacket((GameServerPacket) new PROTOCOL_LOBBY_CHATTING_ACK(0, player, itemsModel));
              }
            }
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_GAMEGUARD_ACK(0U, player));
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_SHOP_LIMITED_SALE_LIST_ACK(player.Gold, player.Gold, 4));
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_RANDOMBOX_LIST_ACK(player));
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(player.Friend.Friends));
          }
          else
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_GAMEGUARD_ACK(2147487763U, (Account) null));
        }
        else
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_GAMEGUARD_ACK(2147483923U, (Account) null));
      }
      else
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_GAMEGUARD_ACK(2147483923U, (Account) null));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_LOBBY_CREATE_NICK_NAME_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
  }
}
