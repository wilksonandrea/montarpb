// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CLAN_WAR_REMOVE_MERCENARY_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CLAN_WAR_REMOVE_MERCENARY_ACK : GameServerPacket
{
  public virtual void Write()
  {
    this.WriteH((short) 5206);
    this.WriteD(((PROTOCOL_BATTLE_GM_PAUSE_ACK) this).uint_0);
    if (((PROTOCOL_BATTLE_GM_PAUSE_ACK) this).uint_0 != 0U)
      return;
    this.WriteD(1);
  }

  public PROTOCOL_CLAN_WAR_REMOVE_MERCENARY_ACK([In] uint obj0)
  {
    ((PROTOCOL_BATTLE_GM_RESUME_ACK) this).uint_0 = obj0;
  }
}
