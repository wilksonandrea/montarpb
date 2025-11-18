// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_ROOM_CREATE_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_ROOM_CREATE_REQ : GameClientPacket
{
  private uint uint_0;
  private string string_0;
  private string string_1;
  private string string_2;
  private MapIdEnum mapIdEnum_0;
  private MapRules mapRules_0;
  private StageOptions stageOptions_0;
  private TeamBalance teamBalance_0;
  private byte[] byte_0;
  private byte[] byte_1;
  private int int_0;
  private int int_1;
  private int int_2;
  private int int_3;
  private int int_4;
  private byte byte_2;
  private byte byte_3;
  private byte byte_4;
  private byte byte_5;
  private byte byte_6;
  private byte byte_7;
  private byte byte_8;
  private RoomCondition roomCondition_0;
  private RoomState roomState_0;
  private RoomWeaponsFlag roomWeaponsFlag_0;
  private RoomStageFlag roomStageFlag_0;

  private void method_1([In] Account obj0, RoomModel long_0, SlotModel long_1)
  {
    if ((((PROTOCOL_ROOM_CHANGE_SLOT_REQ) this).int_0 & 268435456 /*0x10000000*/) != 268435456 /*0x10000000*/ || long_1.State != SlotState.CLOSE)
      return;
    MapMatch mapLimit = SystemMapXML.GetMapLimit((int) long_0.MapId, (int) long_0.Rule);
    if (mapLimit == null || long_1.Id >= mapLimit.Limit)
      return;
    if (long_0.Competitive && !AllUtils.CanOpenSlotCompetitive(long_0, long_1))
      obj0.SendPacket((GameServerPacket) new PROTOCOL_LOBBY_GET_ROOMINFOADD_ACK(Translation.GetLabel("Competitive"), obj0.Session.SessionId, obj0.NickColor, true, Translation.GetLabel("CompetitiveSlotMax")));
    if (long_0.IsBotMode())
      ;
    long_0.ChangeSlotState(long_1, SlotState.EMPTY, true);
  }

  public virtual void Server\u002EGame\u002ENetwork\u002EGameClientPacket\u002ERead()
  {
  }
}
