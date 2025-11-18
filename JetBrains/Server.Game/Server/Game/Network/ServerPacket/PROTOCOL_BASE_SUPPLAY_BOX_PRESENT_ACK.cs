// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BASE_SUPPLAY_BOX_PRESENT_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_SUPPLAY_BOX_PRESENT_ACK : GameServerPacket
{
  private readonly uint uint_0;
  private readonly List<ItemsModel> list_0;
  private readonly List<ItemsModel> list_1;
  private readonly List<ItemsModel> list_2;

  public virtual void Write()
  {
    this.WriteH((short) 2448);
    this.WriteD(((PROTOCOL_BASE_GET_USER_SUBTASK_ACK) this).int_0);
    this.WriteH((short) 0);
    this.WriteC((byte) 0);
    this.WriteD(((PROTOCOL_BASE_GET_USER_SUBTASK_ACK) this).int_0);
    this.WriteC((byte) 0);
  }

  public PROTOCOL_BASE_SUPPLAY_BOX_PRESENT_ACK([In] bool obj0)
  {
    ((PROTOCOL_BASE_RANDOMBOX_LIST_ACK) this).bool_0 = obj0;
  }
}
