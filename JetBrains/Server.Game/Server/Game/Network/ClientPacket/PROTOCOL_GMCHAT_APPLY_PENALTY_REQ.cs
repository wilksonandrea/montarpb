// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_GMCHAT_APPLY_PENALTY_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_GMCHAT_APPLY_PENALTY_REQ : GameClientPacket
{
  private long long_0;
  private string string_0;
  private string string_1;
  private int int_0;
  private byte byte_0;

  public virtual void Run()
  {
    try
    {
      if (this.Client.Player == null)
        return;
      Account account = ClanManager.GetAccount(((PROTOCOL_GMCHAT_END_CHAT_REQ) this).long_0, 31 /*0x1F*/);
      if (account != null)
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_GMCHAT_APPLY_PENALTY_ACK(0U, account));
      else
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_GMCHAT_APPLY_PENALTY_ACK(2147483648U /*0x80000000*/, (Account) null));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_GMCHAT_START_CHAT_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_GMCHAT_START_CHAT_REQ) this).string_0 = this.ReadU((int) this.ReadC() * 2);
    ((PROTOCOL_GMCHAT_START_CHAT_REQ) this).int_0 = this.ReadD();
    ((PROTOCOL_GMCHAT_START_CHAT_REQ) this).byte_0 = this.ReadC();
  }
}
