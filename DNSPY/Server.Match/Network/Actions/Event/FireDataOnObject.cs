using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;

namespace Server.Match.Network.Actions.Event
{
	// Token: 0x02000016 RID: 22
	public class FireDataOnObject
	{
		// Token: 0x06000053 RID: 83 RVA: 0x00007518 File Offset: 0x00005718
		public static FireDataObjectInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
		{
			FireDataObjectInfo fireDataObjectInfo = new FireDataObjectInfo
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

		// Token: 0x06000054 RID: 84 RVA: 0x000021CF File Offset: 0x000003CF
		public static void WriteInfo(SyncServerPacket S, FireDataObjectInfo Info)
		{
			S.WriteC((byte)Info.DeathType);
			S.WriteC(Info.HitPart);
			S.WriteC(Info.Unk);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000020A2 File Offset: 0x000002A2
		public FireDataOnObject()
		{
		}
	}
}
