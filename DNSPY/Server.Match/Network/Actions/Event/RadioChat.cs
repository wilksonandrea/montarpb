using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;

namespace Server.Match.Network.Actions.Event
{
	// Token: 0x0200001F RID: 31
	public class RadioChat
	{
		// Token: 0x0600006E RID: 110 RVA: 0x0000813C File Offset: 0x0000633C
		public static RadioChatInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
		{
			RadioChatInfo radioChatInfo = new RadioChatInfo
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

		// Token: 0x0600006F RID: 111 RVA: 0x0000222D File Offset: 0x0000042D
		public static void WriteInfo(SyncServerPacket S, RadioChatInfo Info)
		{
			S.WriteC(Info.RadioId);
			S.WriteC(Info.AreaId);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000020A2 File Offset: 0x000002A2
		public RadioChat()
		{
		}
	}
}
