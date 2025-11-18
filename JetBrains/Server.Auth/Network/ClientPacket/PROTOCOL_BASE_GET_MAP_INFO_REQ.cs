// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ClientPacket.PROTOCOL_BASE_GET_MAP_INFO_REQ
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.XML;
using Server.Auth.Data.Models;
using Server.Auth.Data.Utils;
using Server.Auth.Network.ServerPacket;
using System;
using System.Collections.Generic;

#nullable disable
namespace Server.Auth.Network.ClientPacket;

public class PROTOCOL_BASE_GET_MAP_INFO_REQ : AuthClientPacket
{
  public virtual void Run()
  {
    try
    {
      if (this.Client.Player == null)
        return;
      List<ChannelModel> channels = AllUtils.GetChannels(((PROTOCOL_BASE_GET_CHANNELLIST_REQ) this).int_0);
      if (channels.Count != 11)
        return;
      this.Client.SendPacket((AuthServerPacket) new PROTOCOL_BASE_GET_INVEN_INFO_ACK(SChannelXML.GetServer(((PROTOCOL_BASE_GET_CHANNELLIST_REQ) this).int_0), channels));
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
  }
}
