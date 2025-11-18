using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;
using System;

namespace Server.Match.Network.Actions.Event
{
	public class SeizeDataForClient
	{
		public SeizeDataForClient()
		{
		}

		public static SeizeDataForClientInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
		{
			SeizeDataForClientInfo seizeDataForClientInfo = new SeizeDataForClientInfo()
			{
				UseTime = C.ReadT(),
				Flags = C.ReadC()
			};
			if (GenLog)
			{
				CLogger.Print(string.Format("PVP Slot: {0}; Use Time: {1}; Flags: {2}", Action.Slot, seizeDataForClientInfo.UseTime, seizeDataForClientInfo.Flags), LoggerType.Warning, null);
			}
			return seizeDataForClientInfo;
		}

		public static void WriteInfo(SyncServerPacket S, SeizeDataForClientInfo Info)
		{
			S.WriteT(Info.UseTime);
			S.WriteC(Info.Flags);
		}
	}
}