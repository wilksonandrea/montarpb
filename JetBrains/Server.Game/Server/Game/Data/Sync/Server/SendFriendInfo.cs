// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Sync.Server.SendFriendInfo
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using System;
using System.Net;

#nullable disable
namespace Server.Game.Data.Sync.Server;

public class SendFriendInfo
{
  public static void Load(Account Room, Account Slot, int Effects)
  {
    try
    {
      if (Room == null)
        return;
      SChannelModel server = GameXender.Sync.GetServer(Room.Status);
      if (server == null)
        return;
      IPEndPoint connection = SynchronizeXML.GetServer((int) server.Port).Connection;
      using (SyncServerPacket syncServerPacket = new SyncServerPacket())
      {
        syncServerPacket.WriteH((short) 16 /*0x10*/);
        syncServerPacket.WriteQ(Room.PlayerId);
        syncServerPacket.WriteC((byte) Effects);
        switch (Effects)
        {
          case 1:
            syncServerPacket.WriteQ(Slot.PlayerId);
            syncServerPacket.WriteC((byte) (Slot.Nickname.Length + 1));
            syncServerPacket.WriteS(Slot.Nickname, Slot.Nickname.Length + 1);
            syncServerPacket.WriteB(Slot.Status.Buffer);
            syncServerPacket.WriteC((byte) Slot.Rank);
            break;
          case 2:
            syncServerPacket.WriteQ(Slot.PlayerId);
            break;
          case 3:
            syncServerPacket.WriteD(Room.ClanId);
            syncServerPacket.WriteC((byte) Room.ClanAccess);
            break;
        }
        // ISSUE: reference to a compiler-generated method
        ((GameSync.Class10) GameXender.Sync).SendPacket(syncServerPacket.ToArray(), connection);
      }
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }
}
