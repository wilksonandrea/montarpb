// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_GM_LOG_ROOM_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Managers;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_GM_LOG_ROOM_REQ : GameClientPacket
{
  private int int_0;

  public virtual void Run()
  {
    try
    {
      if (this.Client.Player == null)
        return;
      if (ClanManager.GetAccount(((PROTOCOL_GMCHAT_APPLY_PENALTY_REQ) this).long_0, 31 /*0x1F*/) != null)
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_GM_LOG_ROOM_ACK(0U));
      else
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_GM_LOG_ROOM_ACK(2147483648U /*0x80000000*/));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_GMCHAT_APPLY_PENALTY_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read() => ((PROTOCOL_GM_LOG_LOBBY_REQ) this).int_0 = this.ReadD();
}
