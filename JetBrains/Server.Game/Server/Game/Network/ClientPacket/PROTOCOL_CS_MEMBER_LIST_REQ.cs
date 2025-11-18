// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CS_MEMBER_LIST_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_MEMBER_LIST_REQ : GameClientPacket
{
  private byte byte_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      ClanInvite OwnerId = new ClanInvite()
      {
        Id = ((PROTOCOL_CS_JOIN_REQUEST_REQ) this).int_0,
        PlayerId = this.Client.PlayerId,
        Text = ((PROTOCOL_CS_JOIN_REQUEST_REQ) this).string_0,
        InviteDate = uint.Parse(DateTimeUtil.Now("yyyyMMdd"))
      };
      if (player.ClanId <= 0 && player.Nickname.Length != 0)
      {
        if (ClanManager.GetClan(((PROTOCOL_CS_JOIN_REQUEST_REQ) this).int_0).Id == 0)
          ((PROTOCOL_CS_JOIN_REQUEST_REQ) this).uint_0 = 2147483648U /*0x80000000*/;
        else if (DaoManagerSQL.GetRequestClanInviteCount(((PROTOCOL_CS_JOIN_REQUEST_REQ) this).int_0) >= 100)
          ((PROTOCOL_CS_JOIN_REQUEST_REQ) this).uint_0 = 2147487831U;
        else if (!DaoManagerSQL.CreateClanInviteInDB(OwnerId))
          ((PROTOCOL_CS_JOIN_REQUEST_REQ) this).uint_0 = 2147487848U;
      }
      else
        ((PROTOCOL_CS_JOIN_REQUEST_REQ) this).uint_0 = 2147487836U;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_MEMBER_INFO_ACK(((PROTOCOL_CS_JOIN_REQUEST_REQ) this).uint_0, ((PROTOCOL_CS_JOIN_REQUEST_REQ) this).int_0));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_CS_JOIN_REQUEST_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
  }

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      int num1 = player.ClanId == 0 ? player.FindClanId : player.ClanId;
      int account_1;
      int num2;
      if (num1 == 0)
      {
        account_1 = -1;
        num2 = 0;
      }
      else
      {
        account_1 = 0;
        num2 = DaoManagerSQL.GetClanPlayers(num1);
      }
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK(account_1, num2));
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }
}
