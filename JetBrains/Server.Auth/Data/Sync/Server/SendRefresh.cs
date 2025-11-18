// Decompiled with JetBrains decompiler
// Type: Server.Auth.Data.Sync.Server.SendRefresh
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Server.Auth.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Auth.Data.Sync.Server;

public static class SendRefresh
{
  public static void RemoveMember(Account iasyncResult_0, [In] long obj1)
  {
    lock (iasyncResult_0.ClanPlayers)
    {
      for (int index = 0; index < iasyncResult_0.ClanPlayers.Count; ++index)
      {
        if (iasyncResult_0.ClanPlayers[index].PlayerId == obj1)
        {
          iasyncResult_0.ClanPlayers.RemoveAt(index);
          break;
        }
      }
    }
  }

  public static void ClearList(Account object_0)
  {
    lock (object_0.ClanPlayers)
    {
      object_0.ClanId = 0;
      object_0.ClanAccess = 0;
      object_0.ClanPlayers.Clear();
    }
  }
}
