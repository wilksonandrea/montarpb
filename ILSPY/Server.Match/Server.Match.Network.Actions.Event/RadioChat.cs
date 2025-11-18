using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;

namespace Server.Match.Network.Actions.Event;

public class RadioChat
{
	public static RadioChatInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
	{
		RadioChatInfo radioChatInfo = new RadioChatInfo
		{
			RadioId = C.ReadC(),
			AreaId = C.ReadC()
		};
		if (GenLog)
		{
			CLogger.Print($"PVP Slot: {Action.Slot} Radio: {radioChatInfo.RadioId} Area: {radioChatInfo.AreaId}", LoggerType.Warning);
		}
		return radioChatInfo;
	}

	public static void WriteInfo(SyncServerPacket S, RadioChatInfo Info)
	{
		S.WriteC(Info.RadioId);
		S.WriteC(Info.AreaId);
	}
}
