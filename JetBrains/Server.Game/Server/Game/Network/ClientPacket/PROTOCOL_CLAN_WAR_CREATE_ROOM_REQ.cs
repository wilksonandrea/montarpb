// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ : GameClientPacket
{
  private int int_0;
  private int int_1;
  private int int_2;
  private int int_3;
  private string string_0;
  private StageOptions stageOptions_0;
  private MapIdEnum mapIdEnum_0;
  private MapRules mapRules_0;
  private RoomCondition roomCondition_0;
  private RoomWeaponsFlag roomWeaponsFlag_0;
  private RoomStageFlag roomStageFlag_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      CharacterModel characterSlot = player.Character.GetCharacterSlot(((PROTOCOL_CHAR_DELETE_CHARA_REQ) this).int_0);
      if (characterSlot != null)
      {
        ItemsModel itemsModel = player.Inventory.GetItem(characterSlot.Id);
        if (itemsModel != null)
        {
          int OwnerId = 0;
          foreach (CharacterModel character in player.Character.Characters)
          {
            if (character.Slot != characterSlot.Slot)
            {
              character.Slot = OwnerId;
              DaoManagerSQL.UpdatePlayerCharacter(OwnerId, character.ObjectId, player.PlayerId);
              ++OwnerId;
            }
          }
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_ACCOUNT_KICK_ACK(0U, ((PROTOCOL_CHAR_DELETE_CHARA_REQ) this).int_0, player, itemsModel));
          if (DaoManagerSQL.DeletePlayerCharacter(characterSlot.ObjectId, player.PlayerId))
            player.Character.RemoveCharacter(characterSlot);
          if (!DaoManagerSQL.DeletePlayerInventoryItem(itemsModel.ObjectId, player.PlayerId))
            return;
          player.Inventory.RemoveItem(itemsModel);
        }
        else
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_ACCOUNT_KICK_ACK(2147487911U, -1, (Account) null, (ItemsModel) null));
      }
      else
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_ACCOUNT_KICK_ACK(2147487911U, -1, (Account) null, (ItemsModel) null));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_CHAR_DELETE_CHARA_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
  }
}
