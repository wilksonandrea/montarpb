// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BASE_CREATE_NICK_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Server.Game.Network.ServerPacket;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_CREATE_NICK_REQ : GameClientPacket
{
  private string string_0;

  public virtual void Run()
  {
    this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_EVENT_PORTAL_ACK());
  }

  public virtual void Read()
  {
    ((PROTOCOL_BASE_CHATTING_REQ) this).chattingType_0 = (ChattingType) this.ReadH();
    ((PROTOCOL_BASE_CHATTING_REQ) this).string_0 = this.ReadU((int) this.ReadH() * 2);
  }
}
