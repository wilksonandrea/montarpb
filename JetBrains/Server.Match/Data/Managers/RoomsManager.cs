// Decompiled with JetBrains decompiler
// Type: Server.Match.Data.Managers.RoomsManager
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Server.Match.Data.Models;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Match.Data.Managers;

public static class RoomsManager
{
  private static readonly List<RoomModel> list_0;

  public static void AddAssist([In] AssistServerData obj0)
  {
    lock (AssistManager.Assists)
      AssistManager.Assists.Add(obj0);
  }

  public static bool RemoveAssist(AssistServerData Id)
  {
    lock (AssistManager.Assists)
      return AssistManager.Assists.Remove(Id);
  }

  public static AssistServerData GetAssist(int Client, int Packet)
  {
    lock (AssistManager.Assists)
    {
      foreach (AssistServerData assist in AssistManager.Assists)
      {
        if (assist.Victim == Client && assist.RoomId == Packet)
          return assist;
      }
    }
    return (AssistServerData) null;
  }
}
