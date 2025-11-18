// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_GM_KICK_COMMAND_REQ
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

public class PROTOCOL_GM_KICK_COMMAND_REQ : GameClientPacket
{
  private byte byte_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      Account account = ClanManager.GetAccount(((PROTOCOL_GMCHAT_SEND_CHAT_REQ) this).string_0, 1, 31 /*0x1F*/);
      if (account == null || !(player.Nickname != account.Nickname))
        return;
      account.SendPacket((GameServerPacket) new PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK(((PROTOCOL_GMCHAT_SEND_CHAT_REQ) this).string_0, ((PROTOCOL_GMCHAT_SEND_CHAT_REQ) this).string_2, ((PROTOCOL_GMCHAT_SEND_CHAT_REQ) this).string_1, account));
    }
    catch (Exception ex)
    {
      CLogger.Print($"{this.GetType().Name}: {ex.Message}", LoggerType.Error, ex);
    }
  }

  public virtual void Read() => ((PROTOCOL_GM_EXIT_COMMAND_REQ) this).byte_0 = this.ReadC();
}
