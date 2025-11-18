// Decompiled with JetBrains decompiler
// Type: Server.Auth.Data.Sync.Client.FriendInfo
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core.Network;
using Server.Auth.Data.Managers;
using Server.Auth.Data.Models;
using Server.Auth.Data.Sync.Server;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Auth.Data.Sync.Client;

public class FriendInfo
{
  public static void Load([In] SyncClientPacket obj0)
  {
    long system = obj0.ReadQ();
    int num1 = (int) obj0.ReadC();
    Account account = ClanManager.GetAccount(system, true);
    if (account == null)
      return;
    switch (num1)
    {
      case 0:
        SendRefresh.ClearList(account);
        break;
      case 1:
        long num2 = obj0.ReadQ();
        string str = obj0.ReadS((int) obj0.ReadC());
        byte[] numArray = obj0.ReadB(4);
        byte num3 = obj0.ReadC();
        Account Address = new Account()
        {
          PlayerId = num2,
          Nickname = str,
          Rank = (int) num3
        };
        Address.Status.SetData(numArray, num2);
        AuthLogin.AddMember(account, Address);
        break;
      case 2:
        long num4 = obj0.ReadQ();
        SendRefresh.RemoveMember(account, num4);
        break;
      case 3:
        int num5 = obj0.ReadD();
        int num6 = (int) obj0.ReadC();
        account.ClanId = num5;
        account.ClanAccess = num6;
        break;
    }
  }
}
