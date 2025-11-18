using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Enums;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;
using System;

namespace Server.Match.Network.Actions.Event
{
	public class ActionState
	{
		public ActionState()
		{
		}

		public static ActionStateInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
		{
			ActionStateInfo actionStateInfo = new ActionStateInfo()
			{
				Action = (ActionFlag)C.ReadUH(),
				Value = C.ReadC(),
				Flag = (WeaponSyncType)C.ReadC()
			};
			if (GenLog)
			{
				CLogger.Print(string.Format("PVP Slot: {0}; Action {1}; Value: {2}; Flag: {3}", new object[] { Action.Slot, actionStateInfo.Action, actionStateInfo.Value, actionStateInfo.Flag }), LoggerType.Warning, null);
			}
			return actionStateInfo;
		}

		public static void WriteInfo(SyncServerPacket S, ActionStateInfo Info)
		{
			S.WriteH((ushort)Info.Action);
			S.WriteC(Info.Value);
			S.WriteC((byte)Info.Flag);
		}
	}
}