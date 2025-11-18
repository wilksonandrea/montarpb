using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;
using System;

namespace Server.Match.Network.Actions.Event
{
	public class RadioChat
	{
		public RadioChat()
		{
		}

		public static RadioChatInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
		{
			RadioChatInfo radioChatInfo = new RadioChatInfo()
			{
				RadioId = C.ReadC(),
				AreaId = C.ReadC()
			};
			if (GenLog)
			{
				CLogger.Print(string.Format("PVP Slot: {0} Radio: {1} Area: {2}", Action.Slot, radioChatInfo.RadioId, radioChatInfo.AreaId), LoggerType.Warning, null);
			}
			return radioChatInfo;
		}

		public static void WriteInfo(SyncServerPacket S, RadioChatInfo Info)
		{
			S.WriteC(Info.RadioId);
			S.WriteC(Info.AreaId);
		}
	}
}