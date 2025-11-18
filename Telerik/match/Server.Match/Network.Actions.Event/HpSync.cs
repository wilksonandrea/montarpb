using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;
using System;

namespace Server.Match.Network.Actions.Event
{
	public class HpSync
	{
		public HpSync()
		{
		}

		public static HPSyncInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
		{
			HPSyncInfo hPSyncInfo = new HPSyncInfo()
			{
				CharaLife = C.ReadUH()
			};
			if (GenLog)
			{
				CLogger.Print(string.Format("PVP Slot: {0}; is using Chara with HP ({1})", Action.Slot, hPSyncInfo.CharaLife), LoggerType.Warning, null);
			}
			return hPSyncInfo;
		}

		public static void WriteInfo(SyncServerPacket S, HPSyncInfo Info)
		{
			S.WriteH(Info.CharaLife);
		}
	}
}