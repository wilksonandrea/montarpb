// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_SHOP_GET_SAILLIST_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_SHOP_GET_SAILLIST_REQ : GameClientPacket
{
  private string string_0;

  public virtual void Run()
  {
    try
    {
      CLogger.Print(this.GetType().Name + " CALLLED!", LoggerType.Warning, (Exception) null);
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_MATCH_CLAN_SEASON_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read() => this.ReadS(32 /*0x20*/);
}
