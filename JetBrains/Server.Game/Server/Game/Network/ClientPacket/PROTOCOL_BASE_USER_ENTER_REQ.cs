// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BASE_USER_ENTER_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_USER_ENTER_REQ : GameClientPacket
{
  private uint uint_0;
  private long long_0;
  private string string_0;

  public virtual void Run()
  {
    try
    {
      ServerConfig config = GameXender.Client.Config;
      if (!(config != null & config.OfficialBannerEnabled))
        return;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_COMMUNITY_USER_REPORT_ACK(config));
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
