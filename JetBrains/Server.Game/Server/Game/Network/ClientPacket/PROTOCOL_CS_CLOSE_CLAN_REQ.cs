// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CS_CLOSE_CLAN_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_CLOSE_CLAN_REQ : GameClientPacket
{
  private uint uint_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      RoomModel room = player.Room;
      if (room != null)
      {
        room.ChangeSlotState(player.SlotId, SlotState.CLAN, false);
        room.StopCountDown(player.SlotId);
        room.UpdateSlotsInfo();
      }
      int num = 0;
      ClanModel clan = ClanManager.GetClan(player.ClanId);
      if (player.ClanId == 0 && player.Nickname.Length > 0)
        num = DaoManagerSQL.GetRequestClanId(player.PlayerId);
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_CLOSE_CLAN_ACK(num > 0 ? num : clan.Id, player.ClanAccess));
      if (clan.Id <= 0 || num != 0)
        return;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_INVITE_ACK(0, clan));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_CS_CLIENT_ENTER_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
  }
}
