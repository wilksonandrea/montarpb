using System;
using System.Collections.Generic;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.XML;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200000B RID: 11
	public class PROTOCOL_BASE_EVENT_PORTAL_ACK : GameServerPacket
	{
		// Token: 0x06000051 RID: 81 RVA: 0x000026C7 File Offset: 0x000008C7
		public PROTOCOL_BASE_EVENT_PORTAL_ACK(bool bool_1)
		{
			this.bool_0 = bool_1;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00008E60 File Offset: 0x00007060
		public override void Write()
		{
			base.WriteH(2511);
			base.WriteC((this.bool_0 > false) ? 1 : 0);
			base.WriteC(1);
			base.WriteD(8192);
			base.WriteC((byte)PortalManager.AllEvents.Count);
			foreach (KeyValuePair<string, PortalEvents> keyValuePair in PortalManager.AllEvents)
			{
				if (keyValuePair.Key.Contains("Boost") && keyValuePair.Value == PortalEvents.BoostEvent)
				{
					EventBoostModel @event = EventBoostXML.GetEvent(PortalManager.GetInitialId(keyValuePair.Key));
					uint[] array = new uint[] { @event.BeginDate, @event.EndedDate };
					string[] array2 = new string[] { @event.Name, @event.Description };
					byte[] array3 = new byte[]
					{
						(!@event.Period) ? 1 : 0,
						(@event.Priority > false) ? 1 : 0
					};
					base.WriteB(PortalManager.InitEventData(keyValuePair.Value, @event.Id, array, array2, array3, 0));
					base.WriteB(PortalManager.InitBoostData(@event));
				}
				else if (keyValuePair.Key.Contains("RankUp") && keyValuePair.Value == PortalEvents.RankUpEvent)
				{
					EventRankUpModel event2 = EventRankUpXML.GetEvent(PortalManager.GetInitialId(keyValuePair.Key));
					uint[] array4 = new uint[] { event2.BeginDate, event2.EndedDate };
					string[] array5 = new string[] { event2.Name, event2.Description };
					byte[] array6 = new byte[]
					{
						(!event2.Period) ? 1 : 0,
						(event2.Priority > false) ? 1 : 0
					};
					base.WriteB(PortalManager.InitEventData(keyValuePair.Value, event2.Id, array4, array5, array6, 1));
					base.WriteB(PortalManager.InitRankUpData(event2));
				}
				else if (keyValuePair.Key.Contains("Login") && keyValuePair.Value == PortalEvents.LoginEvent)
				{
					EventLoginModel event3 = EventLoginXML.GetEvent(PortalManager.GetInitialId(keyValuePair.Key));
					uint[] array7 = new uint[] { event3.BeginDate, event3.EndedDate };
					string[] array8 = new string[] { event3.Name, event3.Description };
					byte[] array9 = new byte[]
					{
						(!event3.Period) ? 1 : 0,
						(event3.Priority > false) ? 1 : 0
					};
					base.WriteB(PortalManager.InitEventData(keyValuePair.Value, event3.Id, array7, array8, array9, 1));
					base.WriteB(PortalManager.InitLoginData(event3));
				}
				else if (keyValuePair.Key.Contains("Playtime") && keyValuePair.Value == PortalEvents.PlaytimeEvent)
				{
					EventPlaytimeModel event4 = EventPlaytimeXML.GetEvent(PortalManager.GetInitialId(keyValuePair.Key));
					uint[] array10 = new uint[] { event4.BeginDate, event4.EndedDate };
					string[] array11 = new string[] { event4.Name, event4.Description };
					byte[] array12 = new byte[]
					{
						(!event4.Period) ? 1 : 0,
						(event4.Priority > false) ? 1 : 0
					};
					base.WriteB(PortalManager.InitEventData(keyValuePair.Value, event4.Id, array10, array11, array12, 0));
					base.WriteB(PortalManager.InitPlaytimeData(event4));
				}
			}
		}

		// Token: 0x0400001F RID: 31
		private readonly bool bool_0;
	}
}
