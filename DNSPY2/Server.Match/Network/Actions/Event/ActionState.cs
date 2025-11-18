using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Enums;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;

namespace Server.Match.Network.Actions.Event
{
	// Token: 0x02000011 RID: 17
	public class ActionState
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00006F2C File Offset: 0x0000512C
		public static ActionStateInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
		{
			ActionStateInfo actionStateInfo = new ActionStateInfo
			{
				Action = (ActionFlag)C.ReadUH(),
				Value = C.ReadC(),
				Flag = (WeaponSyncType)C.ReadC()
			};
			if (GenLog)
			{
				CLogger.Print(string.Format("PVP Slot: {0}; Action {1}; Value: {2}; Flag: {3}", new object[] { Action.Slot, actionStateInfo.Action, actionStateInfo.Value, actionStateInfo.Flag }), LoggerType.Warning, null);
			}
			return actionStateInfo;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000219A File Offset: 0x0000039A
		public static void WriteInfo(SyncServerPacket S, ActionStateInfo Info)
		{
			S.WriteH((ushort)Info.Action);
			S.WriteC(Info.Value);
			S.WriteC((byte)Info.Flag);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000020A2 File Offset: 0x000002A2
		public ActionState()
		{
		}
	}
}
