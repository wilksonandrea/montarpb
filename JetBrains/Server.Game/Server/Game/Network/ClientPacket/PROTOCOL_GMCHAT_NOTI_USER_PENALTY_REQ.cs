// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_GMCHAT_NOTI_USER_PENALTY_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_GMCHAT_NOTI_USER_PENALTY_REQ : GameClientPacket
{
  private long long_0;
  private int int_0;
  private byte byte_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null || player.Nickname.Length == 0 || player.Nickname == ((PROTOCOL_BASE_GET_USER_MANAGEMENT_POPUP_REQ) this).string_0)
        return;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_FRIEND_INSERT_REQ());
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_BASE_UNKNOWN_3635_REQ) this).byte_0 = this.ReadC();
    ((PROTOCOL_BASE_UNKNOWN_3635_REQ) this).string_0 = this.ReadU(66);
    this.ReadD();
    int num1 = (int) this.ReadH();
    ((PROTOCOL_BASE_UNKNOWN_3635_REQ) this).byte_1 = this.ReadC();
    int num2 = (int) this.ReadH();
    this.ReadB(16 /*0x10*/);
    this.ReadB(12);
    ((PROTOCOL_BASE_UNKNOWN_3635_REQ) this).short_0 = this.ReadH();
    ((PROTOCOL_BASE_UNKNOWN_3635_REQ) this).byte_2 = this.ReadC();
  }
}
