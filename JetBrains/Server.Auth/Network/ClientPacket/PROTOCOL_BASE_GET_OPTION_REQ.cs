// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ClientPacket.PROTOCOL_BASE_GET_OPTION_REQ
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.XML;
using Server.Auth.Data.Models;
using Server.Auth.Data.Sync;
using Server.Auth.Network.ServerPacket;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Server.Auth.Network.ClientPacket;

public class PROTOCOL_BASE_GET_OPTION_REQ : AuthClientPacket
{
  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      List<ItemsModel> byte_1 = AuthSync.LimitationIndex(player, player.Inventory.Items);
      if (byte_1.Count <= 0)
        return;
      int count = TemplatePackXML.Basics.Count;
      if (TemplatePackXML.GetPCCafe(player.CafePC) != null)
        count += TemplatePackXML.GetPCCafeRewards(player.CafePC).Count;
      this.Client.SendPacket((AuthServerPacket) new PROTOCOL_BASE_GET_SYSTEM_INFO_ACK(0U, byte_1, count));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_BASE_GET_INVEN_INFO_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public PROTOCOL_BASE_GET_OPTION_REQ()
  {
  }

  public virtual void Read()
  {
  }

  public virtual void Run()
  {
    try
    {
      this.Client.SendPacket((AuthServerPacket) new PROTOCOL_BASE_OPTION_SAVE_ACK());
      foreach (IEnumerable<MapMatch> source in SystemMapXML.Matches.Split<MapMatch>(100))
      {
        List<MapMatch> list = source.ToList<MapMatch>();
        if (list.Count > 0)
          this.Client.SendPacket((AuthServerPacket) new PROTOCOL_BASE_NOTICE_ACK(list, list.Count));
      }
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_BASE_GET_MAP_INFO_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public PROTOCOL_BASE_GET_OPTION_REQ()
  {
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
      if (!player.MyConfigsLoaded && player.Friend.Friends.Count > 0)
        this.Client.SendPacket((AuthServerPacket) new PROTOCOL_AUTH_GET_POINT_CASH_ACK(player.Friend.Friends));
      List<MessageModel> messages = DaoManagerSQL.GetMessages(player.PlayerId);
      if (messages.Count <= 0)
        return;
      DaoManagerSQL.RecycleMessages(player.PlayerId, messages);
      if (messages.Count <= 0)
        return;
      int num1 = (int) Math.Ceiling((double) messages.Count / 25.0);
      int int_1 = 0;
      int int_0 = 0;
      if (0 >= num1)
        int_0 = 0;
      byte[] numArray1 = ((PROTOCOL_BASE_GET_SYSTEM_INFO_REQ) this).method_0(int_0, ref int_1, messages);
      byte[] numArray2 = ((PROTOCOL_BASE_GET_SYSTEM_INFO_REQ) this).method_2(int_0, ref int_1, messages);
      AuthClient client = this.Client;
      int count = messages.Count;
      int num2 = int_0;
      int num3 = num2 + 1;
      byte[] numArray3 = numArray1;
      byte[] numArray4 = numArray2;
      PROTOCOL_SERVER_MESSAGE_DISCONNECTIONSUCCESS_ACK disposing = new PROTOCOL_SERVER_MESSAGE_DISCONNECTIONSUCCESS_ACK(count, num2, numArray3, numArray4);
      client.SendPacket((AuthServerPacket) disposing);
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_BASE_GET_OPTION_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }
}
