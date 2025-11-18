// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CS_CLIENT_ENTER_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_CLIENT_ENTER_REQ : GameClientPacket
{
  public PROTOCOL_CS_CLIENT_ENTER_REQ()
  {
    ((PROTOCOL_CS_CLAN_MATCH_RESULT_LIST_REQ) this).list_0 = new List<MatchModel>();
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  public virtual void Read()
  {
    ((PROTOCOL_CS_CLAN_LIST_FILTER_REQ) this).byte_0 = this.ReadC();
    ((PROTOCOL_CS_CLAN_LIST_FILTER_REQ) this).string_0 = this.ReadU((int) this.ReadC() * 2);
    ((PROTOCOL_CS_CLAN_LIST_FILTER_REQ) this).byte_1 = this.ReadC();
    ((PROTOCOL_CS_CLAN_LIST_FILTER_REQ) this).clanSearchType_0 = (ClanSearchType) this.ReadC();
  }

  public virtual void Run()
  {
    try
    {
      int clanModel_1 = 0;
      using (SyncServerPacket matchModel_0 = new SyncServerPacket())
      {
        lock (ClanManager.Clans)
        {
          for (byte byte0 = ((PROTOCOL_CS_CLAN_LIST_FILTER_REQ) this).byte_0; (int) byte0 < ClanManager.Clans.Count; ++byte0)
          {
            ((PROTOCOL_CS_CLIENT_LEAVE_REQ) this).method_0(ClanManager.Clans[(int) byte0], matchModel_0);
            if (++clanModel_1 == 15)
              break;
          }
        }
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_CLIENT_LEAVE_ACK(((PROTOCOL_CS_CLAN_LIST_FILTER_REQ) this).byte_0, clanModel_1, matchModel_0.ToArray()));
      }
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }
}
