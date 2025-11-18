using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Enums;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;
using System;
using System.Collections.Generic;

namespace Server.Match.Network.Actions.Event
{
	public class UseObject
	{
		public UseObject()
		{
		}

		public static List<UseObjectInfo> ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
		{
			List<UseObjectInfo> useObjectInfos = new List<UseObjectInfo>();
			int ınt32 = C.ReadC();
			for (int i = 0; i < ınt32; i++)
			{
				UseObjectInfo useObjectInfo = new UseObjectInfo()
				{
					ObjectId = C.ReadUH(),
					Use = C.ReadC(),
					SpaceFlags = (CharaMoves)C.ReadC()
				};
				if (GenLog)
				{
					CLogger.Print(string.Format("PVP Slot: {0}; Use Object: {1}; Flag: {2}; ObjectId: {3}", new object[] { Action.Slot, useObjectInfo.Use, useObjectInfo.SpaceFlags, useObjectInfo.ObjectId }), LoggerType.Warning, null);
				}
				useObjectInfos.Add(useObjectInfo);
			}
			return useObjectInfos;
		}

		public static void WriteInfo(SyncServerPacket S, List<UseObjectInfo> Infos)
		{
			S.WriteC((byte)Infos.Count);
			foreach (UseObjectInfo ınfo in Infos)
			{
				S.WriteH(ınfo.ObjectId);
				S.WriteC(ınfo.Use);
				S.WriteC((byte)ınfo.SpaceFlags);
			}
		}
	}
}