// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_AUTH_GET_POINT_CASH_REQ
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

public class PROTOCOL_AUTH_GET_POINT_CASH_REQ : GameClientPacket
{
  public virtual void Read()
  {
    ((PROTOCOL_AUTH_FRIEND_INVITED_REQUEST_REQ) this).int_0 = (int) this.ReadC();
  }

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      Account account = ((PROTOCOL_AUTH_SEND_WHISPER_NOPID_REQ) this).method_0(player);
      if (account != null)
      {
        if (account.Status.ServerId != byte.MaxValue && account.Status.ServerId != (byte) 0)
        {
          if (account.MatchSlot >= 0)
          {
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_GET_POINT_CASH_ACK(2147495939U /*0x80003003*/));
          }
          else
          {
            int friendIdx = account.Friend.GetFriendIdx(player.PlayerId);
            if (friendIdx == -1)
              this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_GET_POINT_CASH_ACK(2147487806U));
            else if (account.IsOnline)
              account.SendPacket((GameServerPacket) new PROTOCOL_AUTH_SHOP_GOODS_GIFT_ACK(friendIdx), false);
            else
              this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_GET_POINT_CASH_ACK(2147487807U));
          }
        }
        else
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_GET_POINT_CASH_ACK(2147495938U /*0x80003002*/));
      }
      else
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_GET_POINT_CASH_ACK(2147487805U));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_AUTH_FRIEND_INVITED_REQUEST_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }
}
