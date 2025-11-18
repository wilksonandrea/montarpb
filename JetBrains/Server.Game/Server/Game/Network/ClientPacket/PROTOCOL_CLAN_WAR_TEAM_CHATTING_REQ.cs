// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CLAN_WAR_TEAM_CHATTING_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CLAN_WAR_TEAM_CHATTING_REQ : GameClientPacket
{
  private ChattingType chattingType_0;
  private string string_0;

  public virtual void Run()
  {
    try
    {
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_CHANGE_NICKNAME_ACK());
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_CLAN_WAR_REMOVE_MERCENARY_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read() => ((PROTOCOL_CLAN_WAR_RESULT_REQ) this).int_0 = this.ReadD();
}
