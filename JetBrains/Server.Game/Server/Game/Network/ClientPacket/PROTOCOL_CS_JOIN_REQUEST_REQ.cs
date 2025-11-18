// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CS_JOIN_REQUEST_REQ
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
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_JOIN_REQUEST_REQ : GameClientPacket
{
  private int int_0;
  private string string_0;
  private uint uint_0;

  public virtual void Read()
  {
  }

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null || player.ClanId == 0 || player.FindPlayer == "" || player.FindPlayer.Length == 0)
        return;
      Account account = ClanManager.GetAccount(player.FindPlayer, 1, 0);
      if (account != null)
      {
        if (account.ClanId == 0 && player.ClanId != 0)
          this.method_0(account, player.ClanId);
        else
          ((PROTOCOL_CS_INVITE_REQ) this).uint_0 = 2147483648U /*0x80000000*/;
      }
      else
        ((PROTOCOL_CS_INVITE_REQ) this).uint_0 = 2147483648U /*0x80000000*/;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_MEMBER_CONTEXT_ACK(((PROTOCOL_CS_INVITE_REQ) this).uint_0));
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  private void method_0([In] Account obj0, int long_0)
  {
    if (DaoManagerSQL.GetMessagesCount(obj0.PlayerId) >= 100)
    {
      ((PROTOCOL_CS_INVITE_REQ) this).uint_0 = 2147483648U /*0x80000000*/;
    }
    else
    {
      MessageModel bool_1 = ((PROTOCOL_CS_MEMBER_CONTEXT_REQ) this).method_1(long_0, obj0.PlayerId, this.Client.PlayerId);
      if (bool_1 == null || !obj0.IsOnline)
        return;
      obj0.SendPacket((GameServerPacket) new PROTOCOL_ROOM_CHANGE_COSTUME_ACK(bool_1), false);
    }
  }
}
