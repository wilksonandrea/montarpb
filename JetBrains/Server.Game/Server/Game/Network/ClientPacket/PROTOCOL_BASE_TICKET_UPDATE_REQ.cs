// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BASE_TICKET_UPDATE_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_TICKET_UPDATE_REQ : GameClientPacket
{
  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null || player.ChannelId >= 0)
        return;
      ChannelModel channel = AllUtils.GetChannel(this.Client.ServerId, ((PROTOCOL_BASE_SELECT_CHANNEL_REQ) this).int_0);
      if (channel != null)
      {
        if (AllUtils.ChannelRequirementCheck(player, channel))
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_USER_LEAVE_ACK(2147484162U /*0x80000202*/, -1, -1));
        else if (channel.Players.Count >= SChannelXML.GetServer(this.Client.ServerId).ChannelPlayers)
        {
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_USER_LEAVE_ACK(2147484161U /*0x80000201*/, -1, -1));
        }
        else
        {
          player.ServerId = channel.ServerId;
          player.ChannelId = channel.Id;
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_USER_LEAVE_ACK(0U, player.ServerId, player.ChannelId));
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_INVENTORY_GET_INFO_ACK());
          player.Status.UpdateServer((byte) player.ServerId);
          player.Status.UpdateChannel((byte) player.ChannelId);
          player.UpdateCacheInfo();
        }
      }
      else
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_USER_LEAVE_ACK(2147483648U /*0x80000000*/, -1, -1));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_BASE_SELECT_CHANNEL_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
  }
}
