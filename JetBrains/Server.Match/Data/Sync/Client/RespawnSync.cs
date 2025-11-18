// Decompiled with JetBrains decompiler
// Type: Server.Match.Data.Sync.Client.RespawnSync
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core.Network;
using Server.Match.Data.Managers;
using Server.Match.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Match.Data.Sync.Client;

public class RespawnSync
{
  public static void Load([In] SyncClientPacket obj0)
  {
    int Assist = (int) obj0.ReadUD();
    uint num1 = obj0.ReadUD();
    int num2 = (int) obj0.ReadC();
    bool flag = obj0.ReadC() == (byte) 1;
    int num3 = (int) num1;
    RoomModel room = DamageManager.GetRoom((uint) Assist, (uint) num3);
    if (room == null)
      return;
    room.ServerRound = num2;
    room.IsTeamSwap = flag;
  }
}
