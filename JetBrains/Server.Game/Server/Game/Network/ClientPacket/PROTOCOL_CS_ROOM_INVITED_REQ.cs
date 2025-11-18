// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CS_ROOM_INVITED_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_ROOM_INVITED_REQ : GameClientPacket
{
  private long long_0;

  public virtual void Read() => ((PROTOCOL_CS_REQUEST_LIST_REQ) this).int_0 = (int) this.ReadC();

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      if (player.ClanId == 0)
      {
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_ROOM_INVITED_RESULT_ACK(-1));
      }
      else
      {
        List<ClanInvite> clanRequestList = DaoManagerSQL.GetClanRequestList(player.ClanId);
        using (SyncServerPacket syncServerPacket_0 = new SyncServerPacket())
        {
          int num = 0;
          for (int index = ((PROTOCOL_CS_REQUEST_LIST_REQ) this).int_0 * (((PROTOCOL_CS_REQUEST_LIST_REQ) this).int_0 != 0 ? 14 : 13); index < clanRequestList.Count; ++index)
          {
            ((PROTOCOL_CS_SECESSION_CLAN_REQ) this).method_0(clanRequestList[index], syncServerPacket_0);
            if (++num == 13)
              break;
          }
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_ROOM_INVITED_RESULT_ACK(0, num, ((PROTOCOL_CS_REQUEST_LIST_REQ) this).int_0, syncServerPacket_0.ToArray()));
        }
      }
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_CS_REQUEST_LIST_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }
}
