using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;

namespace Server.Match.Network.Actions.Event
{
	// Token: 0x02000020 RID: 32
	public class SeizeDataForClient
	{
		// Token: 0x06000071 RID: 113 RVA: 0x000081A0 File Offset: 0x000063A0
		public static SeizeDataForClientInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
		{
			SeizeDataForClientInfo seizeDataForClientInfo = new SeizeDataForClientInfo
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

		// Token: 0x06000072 RID: 114 RVA: 0x00002247 File Offset: 0x00000447
		public static void WriteInfo(SyncServerPacket S, SeizeDataForClientInfo Info)
		{
			S.WriteT(Info.UseTime);
			S.WriteC(Info.Flags);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000020A2 File Offset: 0x000002A2
		public SeizeDataForClient()
		{
		}
	}
}
