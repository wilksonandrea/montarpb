// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ : GameClientPacket
{
  private int int_0;
  private List<RoomModel> list_0;
  private List<QuickstartModel> list_1;
  private QuickstartModel quickstartModel_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      ChannelModel channel = player.GetChannel();
      if (player.Room != null || player.Match != null)
        return;
      if (channel == null || player.Session == null || !((MatchModel) channel).RemovePlayer(player))
        ((PROTOCOL_LOBBY_LEAVE_REQ) this).uint_0 = 2147483648U /*0x80000000*/;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_LOBBY_QUICKJOIN_ROOM_ACK(((PROTOCOL_LOBBY_LEAVE_REQ) this).uint_0));
      if (((PROTOCOL_LOBBY_LEAVE_REQ) this).uint_0 == 0U)
      {
        player.ResetPages();
        player.Status.UpdateChannel(byte.MaxValue);
        AllUtils.SyncPlayerToFriends(player, false);
        AllUtils.SyncPlayerToClanMembers(player);
      }
      else
        this.Client.Close(1000, true, false);
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_LOBBY_LEAVE_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
  }
}
