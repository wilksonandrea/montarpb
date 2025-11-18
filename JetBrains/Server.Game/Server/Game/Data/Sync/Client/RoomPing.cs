// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Sync.Client.RoomPing
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Data.Sync.Client;

public static class RoomPing
{
  private static void smethod_1([In] RoomModel obj0, [In] ref TeamEnum obj1, int int_0)
  {
    switch (int_0)
    {
      case 1:
        if (obj0.SwapRound)
        {
          obj1 = TeamEnum.FR_TEAM;
          ++obj0.FRRounds;
          break;
        }
        obj1 = TeamEnum.CT_TEAM;
        ++obj0.CTRounds;
        break;
      case 2:
        if (obj0.SwapRound)
        {
          obj1 = TeamEnum.CT_TEAM;
          ++obj0.CTRounds;
          break;
        }
        obj1 = TeamEnum.FR_TEAM;
        ++obj0.FRRounds;
        break;
    }
  }
}
