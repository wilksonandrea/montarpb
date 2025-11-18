// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_REQ : GameClientPacket
{
  public virtual void Read()
  {
    ((PROTOCOL_BATTLEBOX_AUTH_REQ) this).long_0 = (long) this.ReadUD();
    ((PROTOCOL_BATTLEBOX_AUTH_REQ) this).int_0 = this.ReadD();
  }

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      ItemsModel list_1 = player.Inventory.GetItem(((PROTOCOL_BATTLEBOX_AUTH_REQ) this).long_0);
      if (list_1 != null)
      {
        BattleBoxModel battleBox = BattleBoxXML.GetBattleBox(list_1.Id);
        if (battleBox != null && battleBox.RequireTags == ((PROTOCOL_BATTLEBOX_AUTH_REQ) this).int_0)
        {
          if (((PROTOCOL_BATTLEBOX_AUTH_REQ) this).int_0 > player.Tags)
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_GM_RESUME_ACK(2147483648U /*0x80000000*/, (Account) null, 0));
          else if (!DaoManagerSQL.UpdateAccountTags(player.PlayerId, player.Tags - ((PROTOCOL_BATTLEBOX_AUTH_REQ) this).int_0))
          {
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_GM_RESUME_ACK(2147483648U /*0x80000000*/, (Account) null, 0));
          }
          else
          {
            GoodsItem good = ShopManager.GetGood(battleBox.GetItemWithProbabilities<int>(battleBox.Goods, battleBox.Probabilities));
            if (good == null)
            {
              this.Client.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_GM_RESUME_ACK(2147483648U /*0x80000000*/, (Account) null, 0));
            }
            else
            {
              player.Tags -= ((PROTOCOL_BATTLEBOX_AUTH_REQ) this).int_0;
              if (ComDiv.UpdateDB("accounts", "tags", (object) player.Tags, "player_id", (object) player.PlayerId))
                this.Client.SendPacket((GameServerPacket) new PROTOCOL_LOBBY_CHATTING_ACK(0, player, good.Item));
              --list_1.Count;
              if (list_1.Count <= 0U)
              {
                if (DaoManagerSQL.DeletePlayerInventoryItem(list_1.ObjectId, player.PlayerId))
                  player.Inventory.RemoveItem(list_1);
                this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_SHOP_GOODSLIST_ACK(1U, ((PROTOCOL_BATTLEBOX_AUTH_REQ) this).long_0));
              }
              else
              {
                ComDiv.UpdateDB("player_items", "count", (object) (long) list_1.Count, "owner_id", (object) player.PlayerId, "id", (object) list_1.Id);
                this.Client.SendPacket((GameServerPacket) new PROTOCOL_LOBBY_CHATTING_ACK(1, player, list_1));
              }
              this.Client.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_GM_RESUME_ACK(0U, player, good.Item.Id));
            }
          }
        }
        else
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_GM_RESUME_ACK(2147483648U /*0x80000000*/, (Account) null, 0));
      }
      else
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_GM_RESUME_ACK(2147483648U /*0x80000000*/, (Account) null, 0));
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.ToString(), LoggerType.Error, (Exception) null);
    }
  }

  public void SendGiftMessage([In] Account obj0, ItemsModel list_0)
  {
    string label = Translation.GetLabel("BattleBoxGift");
    MessageModel message = ((PROTOCOL_BATTLE_DEATH_REQ) this).CreateMessage("GM", obj0.PlayerId, $"{label}\n\n{list_0.Name}");
    if (message == null)
      return;
    obj0.Connection.SendPacket((GameServerPacket) new PROTOCOL_ROOM_CHANGE_COSTUME_ACK(message));
  }
}
