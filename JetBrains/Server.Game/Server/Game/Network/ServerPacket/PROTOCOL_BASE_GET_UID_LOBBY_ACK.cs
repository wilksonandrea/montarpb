// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BASE_GET_UID_LOBBY_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.XML;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_GET_UID_LOBBY_ACK : GameServerPacket
{
  public PROTOCOL_BASE_GET_UID_LOBBY_ACK(bool Name)
  {
    ((PROTOCOL_BASE_EVENT_PORTAL_ACK) this).bool_0 = Name;
  }

  public virtual void Write()
  {
    this.WriteH((short) 2511);
    this.WriteC((byte) ((PROTOCOL_BASE_EVENT_PORTAL_ACK) this).bool_0);
    this.WriteC((byte) 1);
    this.WriteD(8192 /*0x2000*/);
    this.WriteC((byte) PortalManager.AllEvents.Count);
    foreach (KeyValuePair<string, PortalEvents> allEvent in PortalManager.AllEvents)
    {
      if (allEvent.Key.Contains("Boost") && allEvent.Value == PortalEvents.BoostEvent)
      {
        EventBoostModel eventBoostModel = EventBoostXML.GetEvent(PortalManager.GetInitialId(allEvent.Key));
        uint[] numArray1 = new uint[2]
        {
          eventBoostModel.BeginDate,
          eventBoostModel.EndedDate
        };
        string[] strArray = new string[2]
        {
          eventBoostModel.Name,
          eventBoostModel.Description
        };
        byte[] numArray2 = new byte[2]
        {
          (byte) !eventBoostModel.Period,
          (byte) eventBoostModel.Priority
        };
        this.WriteB(PortalManager.InitEventData(allEvent.Value, eventBoostModel.Id, numArray1, strArray, numArray2, (ushort) 0));
        this.WriteB(PortalManager.InitBoostData(eventBoostModel));
      }
      else if (allEvent.Key.Contains("RankUp") && allEvent.Value == PortalEvents.RankUpEvent)
      {
        EventRankUpModel eventRankUpModel = EventRankUpXML.GetEvent(PortalManager.GetInitialId(allEvent.Key));
        uint[] numArray3 = new uint[2]
        {
          eventRankUpModel.get_BeginDate(),
          eventRankUpModel.get_EndedDate()
        };
        string[] strArray = new string[2]
        {
          eventRankUpModel.Name,
          eventRankUpModel.Description
        };
        byte[] numArray4 = new byte[2]
        {
          (byte) !eventRankUpModel.Period,
          (byte) eventRankUpModel.Priority
        };
        this.WriteB(PortalManager.InitEventData(allEvent.Value, eventRankUpModel.Id, numArray3, strArray, numArray4, (ushort) 1));
        this.WriteB(PortalManager.InitRankUpData(eventRankUpModel));
      }
      else if (allEvent.Key.Contains("Login") && allEvent.Value == PortalEvents.LoginEvent)
      {
        EventLoginModel eventLoginModel = EventLoginXML.GetEvent(PortalManager.GetInitialId(allEvent.Key));
        uint[] numArray5 = new uint[2]
        {
          eventLoginModel.BeginDate,
          eventLoginModel.EndedDate
        };
        string[] strArray = new string[2]
        {
          eventLoginModel.get_Name(),
          eventLoginModel.Description
        };
        byte[] numArray6 = new byte[2]
        {
          (byte) !eventLoginModel.Period,
          (byte) eventLoginModel.Priority
        };
        this.WriteB(PortalManager.InitEventData(allEvent.Value, eventLoginModel.Id, numArray5, strArray, numArray6, (ushort) 1));
        this.WriteB(PortalManager.InitLoginData(eventLoginModel));
      }
      else if (allEvent.Key.Contains("Playtime") && allEvent.Value == PortalEvents.PlaytimeEvent)
      {
        EventPlaytimeModel eventPlaytimeModel = EventPlaytimeXML.GetEvent(PortalManager.GetInitialId(allEvent.Key));
        uint[] numArray7 = new uint[2]
        {
          eventPlaytimeModel.BeginDate,
          eventPlaytimeModel.EndedDate
        };
        string[] strArray = new string[2]
        {
          eventPlaytimeModel.Name,
          eventPlaytimeModel.Description
        };
        byte[] numArray8 = new byte[2]
        {
          (byte) !eventPlaytimeModel.Period,
          (byte) eventPlaytimeModel.Priority
        };
        this.WriteB(PortalManager.InitEventData(allEvent.Value, eventPlaytimeModel.Id, numArray7, strArray, numArray8, (ushort) 0));
        this.WriteB(PortalManager.InitPlaytimeData(eventPlaytimeModel));
      }
    }
  }
}
