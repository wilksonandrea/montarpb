// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BATTLE_RESPAWN_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BATTLE_RESPAWN_REQ : GameClientPacket
{
  private int[] int_0;
  private int int_1;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      RoomModel room = player.Room;
      ChannelModel channelModel;
      SlotModel Data;
      if (room == null || room.GetLeader() == null || !room.GetChannel(ref channelModel) || !room.GetSlot(player.SlotId, ref Data))
        return;
      if (Data.Equipment == null)
      {
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_RESPAWN_ACK(2147487915U));
      }
      else
      {
        MapMatch mapLimit = SystemMapXML.GetMapLimit((int) room.MapId, (int) room.Rule);
        if (mapLimit == null)
          return;
        bool flag = room.IsBotMode();
        if (Data.ViewType != ((PROTOCOL_BATTLE_READYBATTLE_REQ) this).viewerType_0)
          Data.ViewType = ((PROTOCOL_BATTLE_READYBATTLE_REQ) this).viewerType_0;
        Data.SpecGM = Data.ViewType == ViewerType.SpecGM && player.IsGM() || room.RoomType == RoomCondition.Ace && (Data.Id < 0 || Data.Id > 1);
        if (!flag && ConfigLoader.TournamentRule && AllUtils.ClassicModeCheck(player, room))
          return;
        int int_0 = 0;
        int classType_0 = 0;
        int classType_1 = 0;
        AllUtils.GetReadyPlayers(room, ref classType_0, ref classType_1, ref int_0);
        if (room.LeaderSlot == player.SlotId)
        {
          if (room.State != RoomState.READY && room.State != RoomState.COUNTDOWN)
            return;
          if (mapLimit.Limit == 8 && AllUtils.Check4vs4(room, true, ref classType_0, ref classType_1, ref int_0))
          {
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_SEASON_CHALLENGE_INFO_ACK());
          }
          else
          {
            uint TotalEnemys;
            if (AllUtils.ClanMatchCheck(room, channelModel.Type, int_0, ref TotalEnemys))
            {
              this.Client.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_RESPAWN_ACK(TotalEnemys));
            }
            else
            {
              uint uint_1;
              if (AllUtils.CompetitiveMatchCheck(player, room, ref uint_1))
                this.Client.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_RESPAWN_ACK(uint_1));
              else if (int_0 == 0 && (flag || room.RoomType == RoomCondition.Tutorial))
              {
                room.ChangeSlotState(Data, SlotState.READY, false);
                room.StartBattle(false);
                room.UpdateSlotsInfo();
              }
              else if (!flag && int_0 > 0)
              {
                room.ChangeSlotState(Data, SlotState.READY, false);
                if (room.BalanceType != TeamBalance.None)
                  AllUtils.TryBalanceTeams(room);
                if (room.ThisModeHaveCD())
                {
                  if (room.State == RoomState.READY)
                  {
                    SlotModel[] slotModelArray = new SlotModel[2]
                    {
                      room.GetSlot(0),
                      room.GetSlot(1)
                    };
                    if (room.RoomType == RoomCondition.Ace && (slotModelArray[0].State != SlotState.READY || slotModelArray[1].State != SlotState.READY))
                    {
                      this.Client.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_RESPAWN_ACK(2147487753U /*0x80001009*/));
                      room.ChangeSlotState(room.LeaderSlot, SlotState.NORMAL, false);
                      room.StopCountDown(CountDownEnum.StopByHost, true);
                    }
                    else
                    {
                      room.State = RoomState.COUNTDOWN;
                      room.UpdateRoomInfo();
                      room.StartCountDown();
                    }
                  }
                  else if (room.State == RoomState.COUNTDOWN)
                  {
                    room.ChangeSlotState(room.LeaderSlot, SlotState.NORMAL, false);
                    room.StopCountDown(CountDownEnum.StopByHost, true);
                  }
                }
                else
                  room.StartBattle(false);
                room.UpdateSlotsInfo();
              }
              else
              {
                if (int_0 != 0 || flag)
                  return;
                this.Client.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_RESPAWN_ACK(2147487753U /*0x80001009*/));
              }
            }
          }
        }
        else if (room.Slots[room.LeaderSlot].State >= SlotState.LOAD && room.IsPreparing())
        {
          if (Data.State != SlotState.NORMAL)
            return;
          if (mapLimit.Limit == 8 && AllUtils.Check4vs4(room, false, ref classType_0, ref classType_0, ref int_0))
          {
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_SEASON_CHALLENGE_INFO_ACK());
          }
          else
          {
            if (room.BalanceType != TeamBalance.None && !flag)
              AllUtils.TryBalancePlayer(room, player, true, out Data);
            room.ChangeSlotState(Data, SlotState.LOAD, true);
            Data.SetMissionsClone(player.Mission);
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_RESPAWN_ACK((uint) Data.State));
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_START_GAME_TRANS_ACK(room));
            using (PROTOCOL_BATTLE_START_GAME_TRANS_ACK startGameTransAck = (PROTOCOL_BATTLE_START_GAME_TRANS_ACK) new PROTOCOL_BATTLE_SUGGEST_KICKVOTE_ACK(room, Data, player.Title))
              room.SendPacketToPlayers((GameServerPacket) startGameTransAck, SlotState.READY, 1, Data.Id);
          }
        }
        else if (Data.State == SlotState.NORMAL)
        {
          room.ChangeSlotState(Data, SlotState.READY, true);
        }
        else
        {
          if (Data.State != SlotState.READY)
            return;
          room.ChangeSlotState(Data, SlotState.NORMAL, false);
          if (room.State == RoomState.COUNTDOWN && room.GetPlayingPlayers((TeamEnum) (room.LeaderSlot % 2 == 0), SlotState.READY, 0) == 0)
          {
            room.ChangeSlotState(room.LeaderSlot, SlotState.NORMAL, false);
            room.StopCountDown(CountDownEnum.StopByPlayer, true);
          }
          room.UpdateSlotsInfo();
        }
      }
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_BATTLE_READYBATTLE_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read() => ((PROTOCOL_BATTLE_RESPAWN_FOR_AI_REQ) this).int_0 = this.ReadD();
}
