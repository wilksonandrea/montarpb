// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Sync.Client.ServerWarning
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Data.Sync.Client;

public class ServerWarning
{
  public static void EndRound(RoomModel C, [In] TeamEnum obj1)
  {
    switch (obj1)
    {
      case TeamEnum.FR_TEAM:
        ++C.FRRounds;
        break;
      case TeamEnum.CT_TEAM:
        ++C.CTRounds;
        break;
    }
    AllUtils.BattleEndRound(C, obj1, RoundEndType.Normal);
  }

  public ServerWarning()
  {
  }

  public static void Load([In] SyncClientPacket obj0)
  {
    int num1 = obj0.ReadD();
    int num2 = obj0.ReadD();
    SChannelModel server = SChannelXML.GetServer(num1);
    if (server == null)
      return;
    server.LastPlayers = num2;
  }

  public ServerWarning()
  {
  }

  public static void Load(SyncClientPacket C)
  {
    byte mission = C.ReadC();
    string str = C.ReadS((int) mission);
    if (string.IsNullOrEmpty(str) || mission > (byte) 60)
      return;
    int num = 0;
    using (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK iasyncResult_0 = (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK) new PROTOCOL_SERVER_MESSAGE_DISCONNECTED_HACK(str))
      num = GameXender.Client.SendPacketToAllClients((GameServerPacket) iasyncResult_0);
    CLogger.Print($"Message sent to '{num}' Players", LoggerType.Command, (Exception) null);
  }
}
