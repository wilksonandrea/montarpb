// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_LOBBY_NEW_MYINFO_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_LOBBY_NEW_MYINFO_REQ : GameClientPacket
{
  private void method_3(Account roomModel_0, SyncServerPacket syncServerPacket_0)
  {
    ClanModel clan = ClanManager.GetClan(roomModel_0.ClanId);
    syncServerPacket_0.WriteD(roomModel_0.GetSessionId());
    syncServerPacket_0.WriteD(clan.Logo);
    syncServerPacket_0.WriteC((byte) clan.Effect);
    syncServerPacket_0.WriteU(clan.Name, 34);
    syncServerPacket_0.WriteH((short) roomModel_0.GetRank());
    syncServerPacket_0.WriteU(roomModel_0.Nickname, 66);
    syncServerPacket_0.WriteC((byte) roomModel_0.NickColor);
    syncServerPacket_0.WriteC((byte) this.NATIONS);
    syncServerPacket_0.WriteD(roomModel_0.Equipment.NameCardId);
    syncServerPacket_0.WriteC((byte) roomModel_0.Bonus.NickBorderColor);
  }

  public virtual void Read()
  {
  }
}
