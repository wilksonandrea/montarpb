using System.Collections.Generic;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.XML;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_EVENT_PORTAL_ACK : GameServerPacket
{
	private readonly bool bool_0;

	public PROTOCOL_BASE_EVENT_PORTAL_ACK(bool bool_1)
	{
		bool_0 = bool_1;
	}

	public override void Write()
	{
		WriteH(2511);
		WriteC(bool_0 ? ((byte)1) : ((byte)0));
		WriteC(1);
		WriteD(8192);
		WriteC((byte)PortalManager.AllEvents.Count);
		foreach (KeyValuePair<string, PortalEvents> allEvent in PortalManager.AllEvents)
		{
			if (allEvent.Key.Contains("Boost") && allEvent.Value == PortalEvents.BoostEvent)
			{
				EventBoostModel @event = EventBoostXML.GetEvent(PortalManager.GetInitialId(allEvent.Key));
				uint[] dateTime = new uint[2] { @event.BeginDate, @event.EndedDate };
				string[] ınfo = new string[2] { @event.Name, @event.Description };
				byte[] type = new byte[2]
				{
					(!@event.Period) ? ((byte)1) : ((byte)0),
					@event.Priority ? ((byte)1) : ((byte)0)
				};
				WriteB(PortalManager.InitEventData(allEvent.Value, @event.Id, dateTime, ınfo, type, 0));
				WriteB(PortalManager.InitBoostData(@event));
			}
			else if (allEvent.Key.Contains("RankUp") && allEvent.Value == PortalEvents.RankUpEvent)
			{
				EventRankUpModel event2 = EventRankUpXML.GetEvent(PortalManager.GetInitialId(allEvent.Key));
				uint[] dateTime2 = new uint[2] { event2.BeginDate, event2.EndedDate };
				string[] ınfo2 = new string[2] { event2.Name, event2.Description };
				byte[] type2 = new byte[2]
				{
					(!event2.Period) ? ((byte)1) : ((byte)0),
					event2.Priority ? ((byte)1) : ((byte)0)
				};
				WriteB(PortalManager.InitEventData(allEvent.Value, event2.Id, dateTime2, ınfo2, type2, 1));
				WriteB(PortalManager.InitRankUpData(event2));
			}
			else if (allEvent.Key.Contains("Login") && allEvent.Value == PortalEvents.LoginEvent)
			{
				EventLoginModel event3 = EventLoginXML.GetEvent(PortalManager.GetInitialId(allEvent.Key));
				uint[] dateTime3 = new uint[2] { event3.BeginDate, event3.EndedDate };
				string[] ınfo3 = new string[2] { event3.Name, event3.Description };
				byte[] type3 = new byte[2]
				{
					(!event3.Period) ? ((byte)1) : ((byte)0),
					event3.Priority ? ((byte)1) : ((byte)0)
				};
				WriteB(PortalManager.InitEventData(allEvent.Value, event3.Id, dateTime3, ınfo3, type3, 1));
				WriteB(PortalManager.InitLoginData(event3));
			}
			else if (allEvent.Key.Contains("Playtime") && allEvent.Value == PortalEvents.PlaytimeEvent)
			{
				EventPlaytimeModel event4 = EventPlaytimeXML.GetEvent(PortalManager.GetInitialId(allEvent.Key));
				uint[] dateTime4 = new uint[2] { event4.BeginDate, event4.EndedDate };
				string[] ınfo4 = new string[2] { event4.Name, event4.Description };
				byte[] type4 = new byte[2]
				{
					(!event4.Period) ? ((byte)1) : ((byte)0),
					event4.Priority ? ((byte)1) : ((byte)0)
				};
				WriteB(PortalManager.InitEventData(allEvent.Value, event4.Id, dateTime4, ınfo4, type4, 0));
				WriteB(PortalManager.InitPlaytimeData(event4));
			}
		}
	}
}
