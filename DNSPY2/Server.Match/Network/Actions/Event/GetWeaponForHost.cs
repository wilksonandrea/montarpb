using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;

namespace Server.Match.Network.Actions.Event
{
	// Token: 0x02000019 RID: 25
	public class GetWeaponForHost
	{
		// Token: 0x0600005C RID: 92 RVA: 0x0000778C File Offset: 0x0000598C
		public static WeaponHost ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
		{
			WeaponHost weaponHost = new WeaponHost
			{
				DeathType = C.ReadC(),
				X = C.ReadUH(),
				Y = C.ReadUH(),
				Z = C.ReadUH(),
				Unks = C.ReadC(),
				UNK = C.ReadD(),
				HitPart = C.ReadC()
			};
			if (GenLog)
			{
				CLogger.Print(string.Format("PVP Slot: {0}; UNK: {1}; Death Type: {2}; Hit: {3}; X: {4}; Y: {5}; Z: {6}", new object[] { Action.Slot, weaponHost.UNK, weaponHost.DeathType, weaponHost.HitPart, weaponHost.X, weaponHost.Y, weaponHost.Z }), LoggerType.Warning, null);
			}
			return weaponHost;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00007870 File Offset: 0x00005A70
		public static void WriteInfo(SyncServerPacket S, WeaponHost Info)
		{
			S.WriteC(Info.DeathType);
			S.WriteH(Info.X);
			S.WriteH(Info.Y);
			S.WriteH(Info.Z);
			S.WriteC(Info.Unks);
			S.WriteD(Info.UNK);
			S.WriteC(Info.HitPart);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000020A2 File Offset: 0x000002A2
		public GetWeaponForHost()
		{
		}
	}
}
