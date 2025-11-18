// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_AUTH_SHOP_AUTH_GIFT_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_AUTH_SHOP_AUTH_GIFT_REQ : GameClientPacket
{
  private long long_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null || player.Nickname == ((PROTOCOL_AUTH_RECV_WHISPER_REQ) this).string_0)
        return;
      Account account = ClanManager.GetAccount(((PROTOCOL_AUTH_RECV_WHISPER_REQ) this).string_0, 1, 0);
      if (account != null && !(account.Nickname != ((PROTOCOL_AUTH_RECV_WHISPER_REQ) this).string_0) && account.IsOnline)
        account.SendPacket((GameServerPacket) new PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK(player.Nickname, ((PROTOCOL_AUTH_RECV_WHISPER_REQ) this).string_1, player.UseChatGM()), false);
      else
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_SHOP_CAPSULE_ACK(((PROTOCOL_AUTH_RECV_WHISPER_REQ) this).string_0, ((PROTOCOL_AUTH_RECV_WHISPER_REQ) this).string_1, 2147483648U /*0x80000000*/));
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_AUTH_SEND_WHISPER_REQ) this).long_0 = this.ReadQ();
    ((PROTOCOL_AUTH_SEND_WHISPER_REQ) this).string_0 = this.ReadU(66);
    ((PROTOCOL_AUTH_SEND_WHISPER_REQ) this).string_1 = this.ReadU((int) this.ReadH() * 2);
  }
}
