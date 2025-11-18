// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_ROOM_CHANGE_ROOM_OPTIONINFO_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_CHANGE_ROOM_OPTIONINFO_ACK : GameServerPacket
{
  private readonly RoomModel roomModel_0;

  public PROTOCOL_ROOM_CHANGE_ROOM_OPTIONINFO_ACK([In] string obj0)
  {
    ((PROTOCOL_ROOM_CHANGE_PASSWD_ACK) this).string_0 = obj0;
  }

  public virtual void Write()
  {
    this.WriteH((short) 3603);
    this.WriteS(((PROTOCOL_ROOM_CHANGE_PASSWD_ACK) this).string_0, 4);
  }
}
