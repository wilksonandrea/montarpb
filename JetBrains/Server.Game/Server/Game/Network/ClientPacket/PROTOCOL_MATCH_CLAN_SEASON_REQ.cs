// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_MATCH_CLAN_SEASON_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_MATCH_CLAN_SEASON_REQ : GameClientPacket
{
  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      player.Quickstart.Quickjoins[((PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ) this).int_0] = ((PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ) this).list_1[((PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ) this).int_0];
      ComDiv.UpdateDB("player_quickstarts", "owner_id", (object) player.PlayerId, new string[4]
      {
        $"list{((PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ) this).int_0}_map_id",
        $"list{((PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ) this).int_0}_map_rule",
        $"list{((PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ) this).int_0}_map_stage",
        $"list{((PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ) this).int_0}_map_type"
      }, new object[4]
      {
        (object) ((PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ) this).list_1[((PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ) this).int_0].MapId,
        (object) ((PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ) this).list_1[((PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ) this).int_0].Rule,
        (object) ((PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ) this).list_1[((PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ) this).int_0].StageOptions,
        (object) ((PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ) this).list_1[((PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ) this).int_0].Type
      });
      ChannelModel C;
      if (player.Nickname.Length > 0 && player.Room == null && player.Match == null && player.GetChannel(ref C))
      {
        lock (C.Rooms)
        {
          foreach (RoomModel room in C.Rooms)
          {
            if (room.RoomType != RoomCondition.Tutorial)
            {
              ((PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ) this).quickstartModel_0 = ((PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ) this).list_1[((PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ) this).int_0];
              if ((MapIdEnum) ((PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ) this).quickstartModel_0.MapId == room.MapId && (MapRules) ((PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ) this).quickstartModel_0.Rule == room.Rule && (StageOptions) ((PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ) this).quickstartModel_0.StageOptions == room.Stage && (RoomCondition) ((PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ) this).quickstartModel_0.Type == room.RoomType && room.Password.Length == 0 && room.Limit == (byte) 0 && (!room.KickedPlayersVote.Contains(player.PlayerId) || player.IsGM()))
              {
                foreach (SlotModel slot in room.Slots)
                {
                  if (slot.PlayerId == 0L && slot.State == SlotState.EMPTY)
                  {
                    ((PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ) this).list_0.Add(room);
                    break;
                  }
                }
              }
            }
          }
        }
      }
      if (((PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ) this).list_0.Count == 0)
      {
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK(2147483648U /*0x80000000*/, ((PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ) this).list_1, (RoomModel) null, (QuickstartModel) null));
      }
      else
      {
        RoomModel roomModel = ((PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ) this).list_0[new Random().Next(((PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ) this).list_0.Count)];
        if (roomModel != null && roomModel.GetLeader() != null && roomModel.AddPlayer(player) >= 0)
        {
          player.ResetPages();
          using (PROTOCOL_ROOM_GET_SLOTONEINFO_ACK LeaderSlot = (PROTOCOL_ROOM_GET_SLOTONEINFO_ACK) new PROTOCOL_ROOM_GET_USER_ITEM_ACK(player))
            roomModel.SendPacketToPlayers((GameServerPacket) LeaderSlot, player.PlayerId);
          roomModel.UpdateSlotsInfo();
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_ROOM_LOADING_START_ACK(0U, player));
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK(0U, ((PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ) this).list_1, roomModel, ((PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ) this).quickstartModel_0));
        }
        else
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK(2147483648U /*0x80000000*/, (List<QuickstartModel>) null, (RoomModel) null, (QuickstartModel) null));
      }
      ((PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ) this).list_0 = (List<RoomModel>) null;
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public PROTOCOL_MATCH_CLAN_SEASON_REQ()
  {
    ((PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ) this).list_0 = new List<RoomModel>();
    ((PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ) this).list_1 = new List<QuickstartModel>();
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  public virtual void Read()
  {
    ((PROTOCOL_BASE_GET_USER_DETAIL_INFO_REQ) this).int_0 = this.ReadD();
  }
}
