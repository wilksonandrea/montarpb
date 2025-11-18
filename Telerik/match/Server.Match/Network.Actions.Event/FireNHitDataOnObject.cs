using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;
using System;

namespace Server.Match.Network.Actions.Event
{
	public class FireNHitDataOnObject
	{
		public FireNHitDataOnObject()
		{
		}

		public static FireNHitDataObjectInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
		{
			FireNHitDataObjectInfo fireNHitDataObjectInfo = new FireNHitDataObjectInfo()
			{
				Portal = C.ReadUH()
			};
			if (GenLog)
			{
				CLogger.Print(string.Format("PVP Slot: {0}; Passed on the portal [{1}]", Action.Slot, fireNHitDataObjectInfo.Portal), LoggerType.Warning, null);
			}
			return fireNHitDataObjectInfo;
		}

		public static void WriteInfo(SyncServerPacket S, FireNHitDataObjectInfo Info)
		{
			S.WriteH(Info.Portal);
		}
	}
}