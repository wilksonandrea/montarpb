// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CS_DENIAL_REQUEST_REQ
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
using Server.Game.Data.Sync.Server;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_DENIAL_REQUEST_REQ : GameClientPacket
{
  private List<long> list_0;
  private int int_0;

  public virtual void Run()
  {
    try
    {
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_DEPORTATION_ACK());
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_CS_CREATE_CLAN_CONDITION_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    this.ReadD();
    ((PROTOCOL_CS_CREATE_CLAN_REQ) this).string_0 = this.ReadU(34);
    ((PROTOCOL_CS_CREATE_CLAN_REQ) this).string_1 = this.ReadU(510);
    this.ReadD();
  }

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      ClanModel clanModel = new ClanModel()
      {
        Name = ((PROTOCOL_CS_CREATE_CLAN_REQ) this).string_0,
        Info = ((PROTOCOL_CS_CREATE_CLAN_REQ) this).string_1,
        Logo = 0,
        OwnerId = player.PlayerId,
        CreationDate = uint.Parse(DateTimeUtil.Now("yyyyMMdd"))
      };
      if (player.ClanId <= 0 && DaoManagerSQL.GetRequestClanId(player.PlayerId) <= 0)
      {
        if (0 <= player.Gold - ConfigLoader.MinCreateGold && ConfigLoader.MinCreateRank <= player.Rank)
        {
          if (CommandManager.IsClanNameExist(clanModel.Name))
          {
            ((PROTOCOL_CS_CREATE_CLAN_REQ) this).uint_0 = 2147487834U;
            return;
          }
          if (ClanManager.Clans.Count > ConfigLoader.MaxActiveClans)
            ((PROTOCOL_CS_CREATE_CLAN_REQ) this).uint_0 = 2147487829U;
          else if (DaoManagerSQL.CreateClan(ref clanModel.Id, clanModel.Name, clanModel.OwnerId, clanModel.Info, clanModel.CreationDate) && DaoManagerSQL.UpdateAccountGold(player.PlayerId, player.Gold - ConfigLoader.MinCreateGold))
          {
            clanModel.BestPlayers.SetDefault();
            player.ClanDate = clanModel.CreationDate;
            if (ComDiv.UpdateDB("accounts", "player_id", (object) player.PlayerId, new string[3]
            {
              "clan_access",
              "clan_date",
              "clan_id"
            }, new object[3]
            {
              (object) 1,
              (object) (long) clanModel.CreationDate,
              (object) clanModel.Id
            }))
            {
              if (clanModel.Id > 0)
              {
                player.ClanId = clanModel.Id;
                player.ClanAccess = 1;
                CommandManager.AddClan(clanModel);
                SendItemInfo.Load(clanModel, 0);
                player.Gold -= ConfigLoader.MinCreateGold;
              }
              else
                ((PROTOCOL_CS_CREATE_CLAN_REQ) this).uint_0 = 2147487819U;
            }
            else
              ((PROTOCOL_CS_CREATE_CLAN_REQ) this).uint_0 = 2147487816U;
          }
          else
            ((PROTOCOL_CS_CREATE_CLAN_REQ) this).uint_0 = 2147487816U;
        }
        else
          ((PROTOCOL_CS_CREATE_CLAN_REQ) this).uint_0 = 2147487818U;
      }
      else
        ((PROTOCOL_CS_CREATE_CLAN_REQ) this).uint_0 = 2147487836U;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_INVITE_ACK((int) ((PROTOCOL_CS_CREATE_CLAN_REQ) this).uint_0, clanModel));
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_DENIAL_REQUEST_ACK(((PROTOCOL_CS_CREATE_CLAN_REQ) this).uint_0, clanModel, player));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_CS_CREATE_CLAN_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }
}
