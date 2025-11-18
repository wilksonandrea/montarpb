using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;

namespace Server.Match.Network.Actions.Event;

public class SeizeDataForClient
{
	public static SeizeDataForClientInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
	{
		SeizeDataForClientInfo seizeDataForClientInfo = new SeizeDataForClientInfo
		{
			UseTime = C.ReadT(),
			Flags = C.ReadC()
		};
		if (GenLog)
		{
			CLogger.Print($"PVP Slot: {Action.Slot}; Use Time: {seizeDataForClientInfo.UseTime}; Flags: {seizeDataForClientInfo.Flags}", LoggerType.Warning);
		}
		return seizeDataForClientInfo;
	}

	public static void WriteInfo(SyncServerPacket S, SeizeDataForClientInfo Info)
	{
		S.WriteT(Info.UseTime);
		S.WriteC(Info.Flags);
	}
}
