// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BASE_GET_RECORD_INFO_DB_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_GET_RECORD_INFO_DB_REQ : GameClientPacket
{
  private long long_0;

  public virtual void Run()
  {
    try
    {
      if (this.Client.Player == null)
        return;
      List<ChannelModel> channels = AllUtils.GetChannels(((PROTOCOL_BASE_GET_CHANNELLIST_REQ) this).int_0);
      if (channels.Count != 11)
        return;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_GET_MYINFO_RECORD_ACK(SChannelXML.GetServer(((PROTOCOL_BASE_GET_CHANNELLIST_REQ) this).int_0), channels));
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_BASE_GET_ROOM_USER_DETAIL_INFO_REQ) this).int_0 = this.ReadD();
  }
}
