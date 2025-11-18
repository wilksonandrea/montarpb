// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BATTLE_GM_RESUME_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_GM_RESUME_ACK : GameServerPacket
{
  private readonly uint uint_0;

  public virtual void Write()
  {
    this.WriteH((short) 2381);
    this.WriteD(((PROTOCOL_BASE_USER_TITLE_RELEASE_ACK) this).uint_0);
    this.WriteC((byte) ((PROTOCOL_BASE_USER_TITLE_RELEASE_ACK) this).int_0);
    this.WriteC((byte) ((PROTOCOL_BASE_USER_TITLE_RELEASE_ACK) this).int_1);
  }

  public PROTOCOL_BATTLE_GM_RESUME_ACK([In] uint obj0, [In] Account obj1, int int_3)
  {
    ((PROTOCOL_BATTLEBOX_AUTH_ACK) this).uint_0 = obj0;
    ((PROTOCOL_BATTLEBOX_AUTH_ACK) this).account_0 = obj1;
    ((PROTOCOL_BATTLEBOX_AUTH_ACK) this).int_0 = int_3;
  }
}
