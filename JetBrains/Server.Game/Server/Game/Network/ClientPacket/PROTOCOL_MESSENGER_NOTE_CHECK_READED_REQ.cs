// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_MESSENGER_NOTE_CHECK_READED_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_MESSENGER_NOTE_CHECK_READED_REQ : GameClientPacket
{
  private readonly List<int> list_0;

  public virtual void Run()
  {
    try
    {
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_MESSENGER_NOTE_CHECK_READED_ACK(false));
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_SHOP_GET_SAILLIST_ACK());
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_MATCH_CLAN_SEASON_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_MATCH_SERVER_IDX_REQ) this).short_0 = this.ReadH();
    int num = (int) this.ReadC();
  }
}
