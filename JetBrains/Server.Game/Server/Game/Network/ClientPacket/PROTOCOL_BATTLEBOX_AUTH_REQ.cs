// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BATTLEBOX_AUTH_REQ
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
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BATTLEBOX_AUTH_REQ : GameClientPacket
{
  private long long_0;
  private int int_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      PlayerTitles title1 = player.Title;
      TitleModel title2 = TitleSystemXML.GetTitle((int) ((PROTOCOL_BASE_USER_TITLE_EQUIP_REQ) this).byte_1, true);
      TitleModel titleModel1;
      TitleModel titleModel2;
      TitleModel titleModel3;
      TitleSystemXML.Get3Titles(title1.Equiped1, title1.Equiped2, title1.Equiped3, ref titleModel1, ref titleModel2, ref titleModel3, false);
      if (((PROTOCOL_BASE_USER_TITLE_EQUIP_REQ) this).byte_0 < (byte) 3 && ((PROTOCOL_BASE_USER_TITLE_EQUIP_REQ) this).byte_1 < (byte) 45 && title1 != null && title2 != null && (title2.ClassId != titleModel1.ClassId || ((PROTOCOL_BASE_USER_TITLE_EQUIP_REQ) this).byte_0 == (byte) 0) && (title2.ClassId != titleModel2.ClassId || ((PROTOCOL_BASE_USER_TITLE_EQUIP_REQ) this).byte_0 == (byte) 1) && (title2.ClassId != titleModel3.ClassId || ((PROTOCOL_BASE_USER_TITLE_EQUIP_REQ) this).byte_0 == (byte) 2) && title1.Contains(title2.Flag) && title1.Equiped1 != (int) ((PROTOCOL_BASE_USER_TITLE_EQUIP_REQ) this).byte_1 && title1.Equiped2 != (int) ((PROTOCOL_BASE_USER_TITLE_EQUIP_REQ) this).byte_1 && title1.Equiped3 != (int) ((PROTOCOL_BASE_USER_TITLE_EQUIP_REQ) this).byte_1)
      {
        if (DaoManagerSQL.UpdateEquipedPlayerTitle(title1.OwnerId, (int) ((PROTOCOL_BASE_USER_TITLE_EQUIP_REQ) this).byte_0, (int) ((PROTOCOL_BASE_USER_TITLE_EQUIP_REQ) this).byte_1))
        {
          title1.SetEquip((int) ((PROTOCOL_BASE_USER_TITLE_EQUIP_REQ) this).byte_0, (int) ((PROTOCOL_BASE_USER_TITLE_EQUIP_REQ) this).byte_1);
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_USER_TITLE_RELEASE_ACK(0U, (int) ((PROTOCOL_BASE_USER_TITLE_EQUIP_REQ) this).byte_0, (int) ((PROTOCOL_BASE_USER_TITLE_EQUIP_REQ) this).byte_1));
        }
        else
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_USER_TITLE_RELEASE_ACK(2147483648U /*0x80000000*/, -1, -1));
      }
      else
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_USER_TITLE_RELEASE_ACK(2147483648U /*0x80000000*/, -1, -1));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_BASE_USER_TITLE_EQUIP_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public PROTOCOL_BATTLEBOX_AUTH_REQ()
  {
  }

  public virtual void Read()
  {
    ((PROTOCOL_BASE_USER_TITLE_RELEASE_REQ) this).int_0 = (int) this.ReadC();
    ((PROTOCOL_BASE_USER_TITLE_RELEASE_REQ) this).int_1 = (int) this.ReadC();
  }

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null || ((PROTOCOL_BASE_USER_TITLE_RELEASE_REQ) this).int_0 >= 3 || player.Title == null)
        return;
      PlayerTitles title = player.Title;
      int equip = title.GetEquip(((PROTOCOL_BASE_USER_TITLE_RELEASE_REQ) this).int_0);
      if (((PROTOCOL_BASE_USER_TITLE_RELEASE_REQ) this).int_0 < 3 && ((PROTOCOL_BASE_USER_TITLE_RELEASE_REQ) this).int_1 < 45 && equip == ((PROTOCOL_BASE_USER_TITLE_RELEASE_REQ) this).int_1 && DaoManagerSQL.UpdateEquipedPlayerTitle(title.OwnerId, ((PROTOCOL_BASE_USER_TITLE_RELEASE_REQ) this).int_0, 0))
      {
        title.SetEquip(((PROTOCOL_BASE_USER_TITLE_RELEASE_REQ) this).int_0, 0);
        if (TitleAwardXML.Contains(equip, player.Equipment.BeretItem) && ComDiv.UpdateDB("player_equipments", "beret_item_part", (object) 0, "owner_id", (object) player.PlayerId))
        {
          player.Equipment.BeretItem = 0;
          RoomModel room = player.Room;
          if (room != null)
            AllUtils.UpdateSlotEquips(player, room);
        }
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_GM_PAUSE_ACK(0U, ((PROTOCOL_BASE_USER_TITLE_RELEASE_REQ) this).int_0, ((PROTOCOL_BASE_USER_TITLE_RELEASE_REQ) this).int_1));
      }
      else
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_GM_PAUSE_ACK(2147483648U /*0x80000000*/, -1, -1));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_BASE_USER_TITLE_RELEASE_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public PROTOCOL_BATTLEBOX_AUTH_REQ()
  {
  }
}
