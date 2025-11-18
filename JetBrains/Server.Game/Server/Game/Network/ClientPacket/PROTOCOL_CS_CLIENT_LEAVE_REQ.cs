// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CS_CLIENT_LEAVE_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Plugin.Core.Network;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_CLIENT_LEAVE_REQ : GameClientPacket
{
  private void method_0(ClanModel account_0, SyncServerPacket matchModel_0)
  {
    matchModel_0.WriteD(account_0.Id);
    matchModel_0.WriteC((byte) (account_0.Name.Length + 1));
    matchModel_0.WriteN(account_0.Name, account_0.Name.Length + 2, "UTF-16LE");
    matchModel_0.WriteD(account_0.Logo);
    matchModel_0.WriteH((ushort) (account_0.Info.Length + 1));
    matchModel_0.WriteN(account_0.Info, account_0.Info.Length + 2, "UTF-16LE");
    matchModel_0.WriteC((byte) 0);
  }

  public virtual void Read()
  {
  }
}
