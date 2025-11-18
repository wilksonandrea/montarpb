using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;

namespace Server.Match.Network.Actions.Event
{
	// Token: 0x0200001C RID: 28
	public class HpSync
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00007F18 File Offset: 0x00006118
		public static HPSyncInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
		{
			HPSyncInfo hpsyncInfo = new HPSyncInfo
			{
				CharaLife = C.ReadUH()
			};
			if (GenLog)
			{
				CLogger.Print(string.Format("PVP Slot: {0}; is using Chara with HP ({1})", Action.Slot, hpsyncInfo.CharaLife), LoggerType.Warning, null);
			}
			return hpsyncInfo;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002204 File Offset: 0x00000404
		public static void WriteInfo(SyncServerPacket S, HPSyncInfo Info)
		{
			S.WriteH(Info.CharaLife);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000020A2 File Offset: 0x000002A2
		public HpSync()
		{
		}
	}
}
