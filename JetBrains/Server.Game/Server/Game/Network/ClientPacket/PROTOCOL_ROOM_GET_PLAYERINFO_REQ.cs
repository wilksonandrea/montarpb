// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_ROOM_GET_PLAYERINFO_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_ROOM_GET_PLAYERINFO_REQ : GameClientPacket
{
  private int int_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      ChannelModel channel = player.GetChannel();
      if (channel != null && player.Nickname.Length != 0 && player.Room == null && player.Match == null)
      {
        lock (channel.Rooms)
        {
          for (int index = 0; index < channel.MaxRooms; ++index)
          {
            if (((MatchModel) channel).GetRoom(index) == null)
            {
              RoomModel roomModel = new RoomModel(index, channel)
              {
                Name = ((PROTOCOL_ROOM_CREATE_REQ) this).string_0,
                MapId = ((PROTOCOL_ROOM_CREATE_REQ) this).mapIdEnum_0,
                Rule = ((PROTOCOL_ROOM_CREATE_REQ) this).mapRules_0,
                Stage = ((PROTOCOL_ROOM_CREATE_REQ) this).stageOptions_0,
                RoomType = ((PROTOCOL_ROOM_CREATE_REQ) this).roomCondition_0
              };
              roomModel.GenerateSeed();
              roomModel.State = ((PROTOCOL_ROOM_CREATE_REQ) this).roomState_0 < RoomState.READY ? RoomState.READY : ((PROTOCOL_ROOM_CREATE_REQ) this).roomState_0;
              roomModel.LeaderName = ((PROTOCOL_ROOM_CREATE_REQ) this).string_2.Equals("") || !((PROTOCOL_ROOM_CREATE_REQ) this).string_2.Equals(player.Nickname) ? player.Nickname : ((PROTOCOL_ROOM_CREATE_REQ) this).string_2;
              roomModel.Ping = ((PROTOCOL_ROOM_CREATE_REQ) this).int_1;
              roomModel.WeaponsFlag = ((PROTOCOL_ROOM_CREATE_REQ) this).roomWeaponsFlag_0;
              roomModel.Flag = ((PROTOCOL_ROOM_CREATE_REQ) this).roomStageFlag_0;
              roomModel.NewInt = ((PROTOCOL_ROOM_CREATE_REQ) this).int_4;
              bool flag;
              if (!(flag = roomModel.IsBotMode()) || roomModel.ChannelType != ChannelType.Clan)
              {
                roomModel.KillTime = ((PROTOCOL_ROOM_CREATE_REQ) this).int_2;
                roomModel.Limit = channel.Type == ChannelType.Clan ? (byte) 1 : ((PROTOCOL_ROOM_CREATE_REQ) this).byte_2;
                roomModel.WatchRuleFlag = roomModel.RoomType == RoomCondition.Ace ? (byte) 142 : ((PROTOCOL_ROOM_CREATE_REQ) this).byte_3;
                roomModel.BalanceType = channel.Type == ChannelType.Clan || roomModel.RoomType == RoomCondition.Ace ? TeamBalance.None : ((PROTOCOL_ROOM_CREATE_REQ) this).teamBalance_0;
                roomModel.RandomMaps = ((PROTOCOL_ROOM_CREATE_REQ) this).byte_0;
                roomModel.CountdownIG = ((PROTOCOL_ROOM_CREATE_REQ) this).byte_8;
                roomModel.LeaderAddr = ((PROTOCOL_ROOM_CREATE_REQ) this).byte_1;
                roomModel.KillCam = ((PROTOCOL_ROOM_CREATE_REQ) this).byte_7;
                roomModel.Password = ((PROTOCOL_ROOM_CREATE_REQ) this).string_1;
                if (flag)
                {
                  roomModel.AiCount = ((PROTOCOL_ROOM_CREATE_REQ) this).byte_4;
                  roomModel.AiLevel = ((PROTOCOL_ROOM_CREATE_REQ) this).byte_5;
                  roomModel.AiType = ((PROTOCOL_ROOM_CREATE_REQ) this).byte_6;
                }
                roomModel.SetSlotCount(((PROTOCOL_ROOM_CREATE_REQ) this).int_0, true, false);
                roomModel.CountPlayers = ((PROTOCOL_ROOM_CREATE_REQ) this).int_3;
                roomModel.CountMaxSlots = ((PROTOCOL_ROOM_CREATE_REQ) this).int_0;
                if (roomModel.AddPlayer(player) >= 0)
                {
                  player.ResetPages();
                  channel.AddRoom(roomModel);
                  this.Client.SendPacket((GameServerPacket) new PROTOCOL_ROOM_GET_COLOR_NICK_ACK(((PROTOCOL_ROOM_CREATE_REQ) this).uint_0, roomModel));
                  if (!roomModel.IsBotMode())
                    break;
                  roomModel.ChangeSlotState(1, SlotState.CLOSE, true);
                  roomModel.ChangeSlotState(3, SlotState.CLOSE, true);
                  roomModel.ChangeSlotState(5, SlotState.CLOSE, true);
                  roomModel.ChangeSlotState(7, SlotState.CLOSE, true);
                  this.Client.SendPacket((GameServerPacket) new PROTOCOL_ROOM_GET_SLOTONEINFO_ACK(roomModel));
                  break;
                }
              }
              else
              {
                ((PROTOCOL_ROOM_CREATE_REQ) this).uint_0 = 2147487869U;
                break;
              }
            }
          }
        }
      }
      else
        ((PROTOCOL_ROOM_CREATE_REQ) this).uint_0 = 2147483648U /*0x80000000*/;
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_LOBBY_CREATE_ROOM_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
  }
}
