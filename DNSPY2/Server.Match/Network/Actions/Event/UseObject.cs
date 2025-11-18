using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Enums;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;

namespace Server.Match.Network.Actions.Event
{
	// Token: 0x02000023 RID: 35
	public class UseObject
	{
		// Token: 0x0600007B RID: 123 RVA: 0x00008454 File Offset: 0x00006654
		public static List<UseObjectInfo> ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
		{
			List<UseObjectInfo> list = new List<UseObjectInfo>();
			int num = (int)C.ReadC();
			for (int i = 0; i < num; i++)
			{
				UseObjectInfo useObjectInfo = new UseObjectInfo
				{
					ObjectId = C.ReadUH(),
					Use = C.ReadC(),
					SpaceFlags = (CharaMoves)C.ReadC()
				};
				if (GenLog)
				{
					CLogger.Print(string.Format("PVP Slot: {0}; Use Object: {1}; Flag: {2}; ObjectId: {3}", new object[] { Action.Slot, useObjectInfo.Use, useObjectInfo.SpaceFlags, useObjectInfo.ObjectId }), LoggerType.Warning, null);
				}
				list.Add(useObjectInfo);
			}
			return list;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00008504 File Offset: 0x00006704
		public static void WriteInfo(SyncServerPacket S, List<UseObjectInfo> Infos)
		{
			S.WriteC((byte)Infos.Count);
			foreach (UseObjectInfo useObjectInfo in Infos)
			{
				S.WriteH(useObjectInfo.ObjectId);
				S.WriteC(useObjectInfo.Use);
				S.WriteC((byte)useObjectInfo.SpaceFlags);
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000020A2 File Offset: 0x000002A2
		public UseObject()
		{
		}
	}
}
