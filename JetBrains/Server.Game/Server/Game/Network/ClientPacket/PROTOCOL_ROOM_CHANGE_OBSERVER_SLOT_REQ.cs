// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_ROOM_CHANGE_OBSERVER_SLOT_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_ROOM_CHANGE_OBSERVER_SLOT_REQ : GameClientPacket
{
  private ViewerType viewerType_0;

  public virtual void Run()
  {
  }

  public virtual void Read() => ((PROTOCOL_GM_KICK_COMMAND_REQ) this).byte_0 = this.ReadC();
}
