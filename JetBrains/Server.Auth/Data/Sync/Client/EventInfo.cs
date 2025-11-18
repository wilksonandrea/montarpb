// Decompiled with JetBrains decompiler
// Type: Server.Auth.Data.Sync.Client.EventInfo
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core.Network;
using Server.Auth.Data.Managers;
using Server.Auth.Data.Models;
using Server.Auth.Data.Utils;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Auth.Data.Sync.Client;

public class EventInfo
{
  public static void Load([In] SyncClientPacket obj0)
  {
    long system = obj0.ReadQ();
    int num = (int) obj0.ReadC();
    string str = obj0.ReadS((int) obj0.ReadC());
    byte[] numArray = obj0.ReadB((int) obj0.ReadUH());
    Account account = ClanManager.GetAccount(system, true);
    if (account == null)
      return;
    if (num == 0)
      account.SendPacket(numArray, str);
    else
      account.SendCompletePacket(numArray, str);
  }

  public static void Load([In] SyncClientPacket obj0)
  {
    int num1 = obj0.ReadD();
    int num2 = obj0.ReadD();
    int num3 = obj0.ReadD();
    int syncServerPacket_0 = num2;
    ChannelModel channel = AllUtils.GetChannel(num1, syncServerPacket_0);
    if (channel == null)
      return;
    channel.TotalPlayers = num3;
  }
}
