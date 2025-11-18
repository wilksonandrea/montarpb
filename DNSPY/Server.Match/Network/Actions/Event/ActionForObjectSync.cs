using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event.Event;

namespace Server.Match.Network.Actions.Event
{
	// Token: 0x02000010 RID: 16
	public class ActionForObjectSync
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00006EC8 File Offset: 0x000050C8
		public static ActionObjectInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
		{
			ActionObjectInfo actionObjectInfo = new ActionObjectInfo
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

		// Token: 0x06000042 RID: 66 RVA: 0x00002180 File Offset: 0x00000380
		public static void WriteInfo(SyncServerPacket S, ActionObjectInfo Info)
		{
			S.WriteC(Info.Unk1);
			S.WriteC(Info.Unk2);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000020A2 File Offset: 0x000002A2
		public ActionForObjectSync()
		{
		}
	}
}
