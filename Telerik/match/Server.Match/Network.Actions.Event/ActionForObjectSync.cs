using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event.Event;
using System;

namespace Server.Match.Network.Actions.Event
{
	public class ActionForObjectSync
	{
		public ActionForObjectSync()
		{
		}

		public static ActionObjectInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
		{
			ActionObjectInfo actionObjectInfo = new ActionObjectInfo()
			{
				Unk1 = C.ReadC(),
				Unk2 = C.ReadC()
			};
			if (GenLog)
			{
				CLogger.Print(string.Format("PVP Slot: {0} Unk1: {1}; Unk2: {2}", Action.Slot, actionObjectInfo.Unk1, actionObjectInfo.Unk2), LoggerType.Warning, null);
			}
			return actionObjectInfo;
		}

		public static void WriteInfo(SyncServerPacket S, ActionObjectInfo Info)
		{
			S.WriteC(Info.Unk1);
			S.WriteC(Info.Unk2);
		}
	}
}