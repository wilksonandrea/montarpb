using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.XML;
using Server.Game.Network;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BASE_EVENT_PORTAL_ACK : GameServerPacket
	{
		private readonly bool bool_0;

		public PROTOCOL_BASE_EVENT_PORTAL_ACK(bool bool_1)
		{
			this.bool_0 = bool_1;
		}

		public override void Write()
		{
			base.WriteH(2511);
			base.WriteC((byte)this.bool_0);
			base.WriteC(1);
			base.WriteD(8192);
			base.WriteC((byte)PortalManager.AllEvents.Count);
			foreach (KeyValuePair<string, PortalEvents> allEvent in PortalManager.AllEvents)
			{
				if (allEvent.Key.Contains("Boost") && allEvent.Value == PortalEvents.BoostEvent)
				{
					EventBoostModel @event = EventBoostXML.GetEvent(PortalManager.GetInitialId(allEvent.Key));
					uint[] beginDate = new uint[] { @event.BeginDate, @event.EndedDate };
					string[] name = new string[] { @event.Name, @event.Description };
					byte[] period = new byte[] { (byte)(!@event.Period), (byte)@event.Priority };
					base.WriteB(PortalManager.InitEventData(allEvent.Value, @event.Id, beginDate, name, period, 0));
					base.WriteB(PortalManager.InitBoostData(@event));
				}
				else if (allEvent.Key.Contains("RankUp") && allEvent.Value == PortalEvents.RankUpEvent)
				{
					EventRankUpModel eventRankUpModel = EventRankUpXML.GetEvent(PortalManager.GetInitialId(allEvent.Key));
					uint[] uInt32Array = new uint[] { eventRankUpModel.BeginDate, eventRankUpModel.EndedDate };
					string[] strArrays = new string[] { eventRankUpModel.Name, eventRankUpModel.Description };
					byte[] numArray = new byte[] { (byte)(!eventRankUpModel.Period), (byte)eventRankUpModel.Priority };
					base.WriteB(PortalManager.InitEventData(allEvent.Value, eventRankUpModel.Id, uInt32Array, strArrays, numArray, 1));
					base.WriteB(PortalManager.InitRankUpData(eventRankUpModel));
				}
				else if (!allEvent.Key.Contains("Login") || allEvent.Value != PortalEvents.LoginEvent)
				{
					if (!allEvent.Key.Contains("Playtime") || allEvent.Value != PortalEvents.PlaytimeEvent)
					{
						continue;
					}
					EventPlaytimeModel eventPlaytimeModel = EventPlaytimeXML.GetEvent(PortalManager.GetInitialId(allEvent.Key));
					uint[] beginDate1 = new uint[] { eventPlaytimeModel.BeginDate, eventPlaytimeModel.EndedDate };
					string[] name1 = new string[] { eventPlaytimeModel.Name, eventPlaytimeModel.Description };
					byte[] period1 = new byte[] { (byte)(!eventPlaytimeModel.Period), (byte)eventPlaytimeModel.Priority };
					base.WriteB(PortalManager.InitEventData(allEvent.Value, eventPlaytimeModel.Id, beginDate1, name1, period1, 0));
					base.WriteB(PortalManager.InitPlaytimeData(eventPlaytimeModel));
				}
				else
				{
					EventLoginModel eventLoginModel = EventLoginXML.GetEvent(PortalManager.GetInitialId(allEvent.Key));
					uint[] uInt32Array1 = new uint[] { eventLoginModel.BeginDate, eventLoginModel.EndedDate };
					string[] strArrays1 = new string[] { eventLoginModel.Name, eventLoginModel.Description };
					byte[] numArray1 = new byte[] { (byte)(!eventLoginModel.Period), (byte)eventLoginModel.Priority };
					base.WriteB(PortalManager.InitEventData(allEvent.Value, eventLoginModel.Id, uInt32Array1, strArrays1, numArray1, 1));
					base.WriteB(PortalManager.InitLoginData(eventLoginModel));
				}
			}
		}
	}
}