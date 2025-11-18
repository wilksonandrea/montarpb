// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CHAR_DELETE_CHARA_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CHAR_DELETE_CHARA_REQ : GameClientPacket
{
  private int int_0;

  public PROTOCOL_CHAR_DELETE_CHARA_REQ()
  {
    ((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).int_2 = new int[2];
    ((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).int_3 = new int[14];
    ((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).sortedList_0 = new SortedList<int, int>();
    ((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).sortedList_1 = new SortedList<int, int>();
    ((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).sortedList_2 = new SortedList<int, int>();
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  public virtual void Read()
  {
    int num1 = (int) this.ReadC();
    ((PROTOCOL_CHAR_CREATE_CHARA_REQ) this).string_0 = this.ReadU((int) this.ReadC() * 2);
    int num2 = (int) this.ReadC();
    ((PROTOCOL_CHAR_CREATE_CHARA_REQ) this).list_0.Add(new CartGoods()
    {
      GoodId = this.ReadD(),
      BuyType = (int) this.ReadC()
    });
    int num3 = (int) this.ReadC();
  }

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      if (player.Inventory.Items.Count < 500 && player.Character.Characters.Count < 64 /*0x40*/)
      {
        int num1;
        int num2;
        int num3;
        List<GoodsItem> goods = ShopManager.GetGoods(((PROTOCOL_CHAR_CREATE_CHARA_REQ) this).list_0, ref num1, ref num2, ref num3);
        if (goods.Count == 0)
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_CLAN_WAR_CANCEL_MATCHMAKING_ACK(2147487767U, byte.MaxValue, (CharacterModel) null, (Account) null));
        else if (0 <= player.Gold - num1 && 0 <= player.Cash - num2 && 0 <= player.Tags - num3)
        {
          if (DaoManagerSQL.UpdateAccountValuable(player.PlayerId, player.Gold - num1, player.Cash - num2, player.Tags - num3))
          {
            player.Gold -= num1;
            player.Cash -= num2;
            player.Tags -= num3;
            CharacterModel OwnerId = ((PROTOCOL_CLAN_WAR_CANCEL_MATCHMAKING_REQ) this).method_0(player, goods);
            if (OwnerId != null)
            {
              player.Character.AddCharacter(OwnerId);
              if (player.Character.GetCharacter(OwnerId.Id) != null)
                DaoManagerSQL.CreatePlayerCharacter(OwnerId, player.PlayerId);
            }
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_INVENTORY_LEAVE_ACK(0, player, goods));
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_CLAN_WAR_CANCEL_MATCHMAKING_ACK(0U, (byte) 1, OwnerId, player));
          }
          else
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_CLAN_WAR_CANCEL_MATCHMAKING_ACK(2147487769U, byte.MaxValue, (CharacterModel) null, (Account) null));
        }
        else
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_CLAN_WAR_CANCEL_MATCHMAKING_ACK(2147487768U, byte.MaxValue, (CharacterModel) null, (Account) null));
      }
      else
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_CLAN_WAR_CANCEL_MATCHMAKING_ACK(2147487929U, byte.MaxValue, (CharacterModel) null, (Account) null));
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }
}
