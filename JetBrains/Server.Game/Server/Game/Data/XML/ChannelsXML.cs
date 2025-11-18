// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.XML.ChannelsXML
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network;
using Server.Game.Network.ClientPacket;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Server.Game.Data.XML;

public static class ChannelsXML
{
  public static List<ChannelModel> Channels;

  public virtual void Run()
  {
    try
    {
      Account player = ((GameClientPacket) this).Client.Player;
      if (player == null)
        return;
      player.Room?.ChangeSlotState(player.SlotId, SlotState.NORMAL, true);
      ((GameClientPacket) this).Client.SendPacket((GameServerPacket) new PROTOCOL_SHOP_PLUS_POINT_ACK());
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_SHOP_LEAVE_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public ChannelsXML()
    : this()
  {
  }

  public virtual void Read()
  {
    ((PROTOCOL_SHOP_REPAIR_REQ) this).int_0 = (int) ((BaseClientPacket) this).ReadC();
    for (int index = 0; index < ((PROTOCOL_SHOP_REPAIR_REQ) this).int_0; ++index)
      ((PROTOCOL_SHOP_REPAIR_REQ) this).list_0.Add((long) ((BaseClientPacket) this).ReadUD());
  }

  public virtual void Run()
  {
    try
    {
      Account player = ((GameClientPacket) this).Client.Player;
      if (player == null)
        return;
      int num1;
      int num2;
      uint int_3;
      List<ItemsModel> int_4 = AllUtils.RepairableItems(player, ((PROTOCOL_SHOP_REPAIR_REQ) this).list_0, ref num1, ref num2, ref int_3);
      if (int_4.Count <= 0)
        return;
      player.Gold -= num1;
      player.Cash -= num2;
      if (ComDiv.UpdateDB("accounts", "player_id", (object) player.PlayerId, new string[2]
      {
        "gold",
        "cash"
      }, new object[2]
      {
        (object) player.Gold,
        (object) player.Cash
      }))
        ((GameClientPacket) this).Client.SendPacket((GameServerPacket) new PROTOCOL_LOBBY_CHATTING_ACK(2, player, int_4));
      ((GameClientPacket) this).Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_GET_USER_MANAGEMENT_POPUP_ACK(int_3, int_4, player));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_SHOP_REPAIR_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public ChannelsXML()
  {
    ((PROTOCOL_SHOP_REPAIR_REQ) this).list_0 = new List<long>();
    // ISSUE: explicit constructor call
    ((GameClientPacket) this).\u002Ector();
  }

  public static void Load()
  {
    string str = "Data/Server/Channels.xml";
    if (File.Exists(str))
      AllUtils.smethod_0(str);
    else
      CLogger.Print("File not found: " + str, LoggerType.Warning, (Exception) null);
  }

  public static void Reload()
  {
    ChannelsXML.Channels.Clear();
    ChannelsXML.Load();
  }
}
