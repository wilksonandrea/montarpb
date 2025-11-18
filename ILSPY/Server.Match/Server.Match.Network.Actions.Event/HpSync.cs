using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;

namespace Server.Match.Network.Actions.Event;

public class HpSync
{
	public static HPSyncInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
	{
		HPSyncInfo hPSyncInfo = new HPSyncInfo
		{
			CharaLife = C.ReadUH()
		};
		if (GenLog)
		{
			CLogger.Print($"PVP Slot: {Action.Slot}; is using Chara with HP ({hPSyncInfo.CharaLife})", LoggerType.Warning);
		}
		return hPSyncInfo;
	}

	public static void WriteInfo(SyncServerPacket S, HPSyncInfo Info)
	{
		S.WriteH(Info.CharaLife);
	}
}
