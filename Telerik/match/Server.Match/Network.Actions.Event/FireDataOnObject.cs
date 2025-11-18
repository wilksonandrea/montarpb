using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;
using System;

namespace Server.Match.Network.Actions.Event
{
	public class FireDataOnObject
	{
		public FireDataOnObject()
		{
		}

		public static FireDataObjectInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
		{
			FireDataObjectInfo fireDataObjectInfo = new FireDataObjectInfo()
			{
				DeathType = (CharaDeath)C.ReadC(),
				HitPart = C.ReadC(),
				Unk = C.ReadC()
			};
			if (GenLog)
			{
				CLogger.Print(string.Format("PVP Slot: {0}; Death Type: {1}; Hit Part: {2}", Action.Slot, fireDataObjectInfo.DeathType, fireDataObjectInfo.HitPart), LoggerType.Warning, null);
			}
			return fireDataObjectInfo;
		}

		public static void WriteInfo(SyncServerPacket S, FireDataObjectInfo Info)
		{
			S.WriteC((byte)Info.DeathType);
			S.WriteC(Info.HitPart);
			S.WriteC(Info.Unk);
		}
	}
}