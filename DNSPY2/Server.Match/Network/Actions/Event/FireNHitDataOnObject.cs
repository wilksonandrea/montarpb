using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;

namespace Server.Match.Network.Actions.Event
{
	// Token: 0x02000017 RID: 23
	public class FireNHitDataOnObject
	{
		// Token: 0x06000056 RID: 86 RVA: 0x00007588 File Offset: 0x00005788
		public static FireNHitDataObjectInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
		{
			FireNHitDataObjectInfo fireNHitDataObjectInfo = new FireNHitDataObjectInfo
			{
				Portal = C.ReadUH()
			};
			if (GenLog)
			{
				CLogger.Print(string.Format("PVP Slot: {0}; Passed on the portal [{1}]", Action.Slot, fireNHitDataObjectInfo.Portal), LoggerType.Warning, null);
			}
			return fireNHitDataObjectInfo;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000021F6 File Offset: 0x000003F6
		public static void WriteInfo(SyncServerPacket S, FireNHitDataObjectInfo Info)
		{
			S.WriteH(Info.Portal);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000020A2 File Offset: 0x000002A2
		public FireNHitDataOnObject()
		{
		}
	}
}
