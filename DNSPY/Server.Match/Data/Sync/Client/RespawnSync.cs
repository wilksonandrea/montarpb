using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Match.Data.Managers;
using Server.Match.Data.Models;
using Server.Match.Data.Utils;
using Server.Match.Data.XML;
using Server.Match.Network.Packets;

namespace Server.Match.Data.Sync.Client
{
	// Token: 0x0200002F RID: 47
	public class RespawnSync
	{
		// Token: 0x060000C7 RID: 199 RVA: 0x0000A304 File Offset: 0x00008504
		public static void Load(SyncClientPacket C)
		{
			RespawnSync.Class1 @class = new RespawnSync.Class1();
			uint num = C.ReadUD();
			uint num2 = C.ReadUD();
			long num3 = C.ReadQ();
			int num4 = (int)C.ReadC();
			int num5 = (int)C.ReadC();
			int num6 = (int)C.ReadC();
			int num7 = (int)C.ReadC();
			int num8 = (int)C.ReadC();
			int num9 = 0;
			int num10 = 0;
			int num11 = 0;
			int num12 = 0;
			int num13 = 0;
			int num14 = 0;
			int num15 = 0;
			int num16 = 0;
			bool flag = false;
			if (num4 != 0)
			{
				if (num4 != 2)
				{
					if (C.ToArray().Length > 23)
					{
						CLogger.Print(string.Format("RespawnSync (Length > 23): {0}", C.ToArray().Length), LoggerType.Warning, null);
						goto IL_10D;
					}
					goto IL_10D;
				}
			}
			num9 = C.ReadD();
			num10 = (int)C.ReadC();
			flag = C.ReadC() == 1;
			num11 = C.ReadD();
			num12 = C.ReadD();
			num13 = C.ReadD();
			num14 = C.ReadD();
			num15 = C.ReadD();
			num16 = C.ReadD();
			if (C.ToArray().Length > 53)
			{
				CLogger.Print(string.Format("RespawnSync (Length > 53): {0}", C.ToArray().Length), LoggerType.Warning, null);
			}
			IL_10D:
			@class.roomModel_0 = RoomsManager.GetRoom(num, num2);
			if (@class.roomModel_0 == null)
			{
				return;
			}
			@class.roomModel_0.ResyncTick(num3, num2);
			@class.playerModel_0 = @class.roomModel_0.GetPlayer(num6, true);
			if (@class.playerModel_0 != null && @class.playerModel_0.PlayerIdByUser != num8)
			{
				CLogger.Print(string.Format("Invalid User Ids: [By User: {0} / Server: {1}]", @class.playerModel_0.PlayerIdByUser, num8), LoggerType.Warning, null);
			}
			if (@class.playerModel_0 != null && @class.playerModel_0.PlayerIdByUser == num8)
			{
				@class.playerModel_0.PlayerIdByServer = num8;
				@class.playerModel_0.RespawnByServer = num7;
				@class.playerModel_0.Integrity = false;
				if (num5 > @class.roomModel_0.ServerRound)
				{
					@class.roomModel_0.ServerRound = num5;
				}
				if (num4 == 0 || num4 == 2)
				{
					AssistServerData assist = AssistManager.GetAssist(@class.playerModel_0.Slot, @class.roomModel_0.RoomId);
					if (assist != null && AssistManager.RemoveAssist(assist))
					{
						List<AssistServerData> assists = AssistManager.Assists;
						Predicate<AssistServerData> predicate;
						if ((predicate = @class.predicate_0) == null)
						{
							predicate = (@class.predicate_0 = new Predicate<AssistServerData>(@class.method_0));
						}
						foreach (AssistServerData assistServerData in assists.FindAll(predicate))
						{
							AssistManager.RemoveAssist(assistServerData);
						}
					}
					Equipment equipment = new Equipment
					{
						WpnPrimary = num11,
						WpnSecondary = num12,
						WpnMelee = num13,
						WpnExplosive = num14,
						WpnSpecial = num15,
						Accessory = num16
					};
					@class.playerModel_0.Dead = false;
					@class.playerModel_0.PlantDuration = ConfigLoader.PlantDuration;
					@class.playerModel_0.DefuseDuration = ConfigLoader.DefuseDuration;
					@class.playerModel_0.Equip = equipment;
					if (flag)
					{
						@class.playerModel_0.PlantDuration -= ComDiv.Percentage(ConfigLoader.PlantDuration, 50);
						@class.playerModel_0.DefuseDuration -= ComDiv.Percentage(ConfigLoader.DefuseDuration, 25);
					}
					if (!@class.roomModel_0.BotMode)
					{
						if (@class.roomModel_0.SourceToMap == -1)
						{
							@class.roomModel_0.RoundResetRoomF1(num5);
						}
						else
						{
							@class.roomModel_0.RoundResetRoomS1(num5);
						}
					}
					if (num9 == -1)
					{
						@class.playerModel_0.Immortal = true;
					}
					else
					{
						@class.playerModel_0.Immortal = false;
						int num17 = CharaStructureXML.GetCharaHP(num9);
						num17 += ComDiv.Percentage(num17, num10);
						@class.playerModel_0.MaxLife = num17;
						@class.playerModel_0.ResetLife();
					}
				}
				if (@class.roomModel_0.BotMode || num4 == 2 || !@class.roomModel_0.ObjectsIsValid())
				{
					return;
				}
				List<ObjectHitInfo> list = new List<ObjectHitInfo>();
				foreach (ObjectInfo objectInfo in @class.roomModel_0.Objects)
				{
					ObjectModel model = objectInfo.Model;
					if (model != null && ((num4 != 2 && model.Destroyable && objectInfo.Life != model.Life) || model.NeedSync))
					{
						ObjectHitInfo objectHitInfo = new ObjectHitInfo(3)
						{
							ObjSyncId = ((model.NeedSync > false) ? 1 : 0),
							AnimId1 = model.Animation,
							AnimId2 = ((objectInfo.Animation != null) ? objectInfo.Animation.Id : 255),
							DestroyState = objectInfo.DestroyState,
							ObjId = model.Id,
							ObjLife = objectInfo.Life,
							SpecialUse = AllUtils.GetDuration(objectInfo.UseDate)
						};
						list.Add(objectHitInfo);
					}
				}
				foreach (PlayerModel playerModel in @class.roomModel_0.Players)
				{
					if (playerModel.Slot != num6 && playerModel.AccountIdIsValid() && !playerModel.Immortal && playerModel.StartTime != default(DateTime) && (playerModel.MaxLife != playerModel.Life || playerModel.Dead))
					{
						ObjectHitInfo objectHitInfo2 = new ObjectHitInfo(4)
						{
							ObjId = playerModel.Slot,
							ObjLife = playerModel.Life
						};
						list.Add(objectHitInfo2);
					}
				}
				if (list.Count > 0)
				{
					byte[] array = PROTOCOL_EVENTS_ACTION.GET_CODE(list);
					byte[] array2 = AllUtils.BaseWriteCode(4, array, 255, AllUtils.GetDuration(@class.roomModel_0.StartTime), num5, num7, 0, num8);
					MatchXender.Client.SendPacket(array2, @class.playerModel_0.Client);
				}
				list.Clear();
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000020A2 File Offset: 0x000002A2
		public RespawnSync()
		{
		}

		// Token: 0x02000030 RID: 48
		[CompilerGenerated]
		private sealed class Class1
		{
			// Token: 0x060000C9 RID: 201 RVA: 0x000020A2 File Offset: 0x000002A2
			public Class1()
			{
			}

			// Token: 0x060000CA RID: 202 RVA: 0x00002370 File Offset: 0x00000570
			internal bool method_0(AssistServerData assistServerData_0)
			{
				return assistServerData_0.Victim == this.playerModel_0.Slot && assistServerData_0.RoomId == this.roomModel_0.RoomId;
			}

			// Token: 0x04000012 RID: 18
			public PlayerModel playerModel_0;

			// Token: 0x04000013 RID: 19
			public RoomModel roomModel_0;

			// Token: 0x04000014 RID: 20
			public Predicate<AssistServerData> predicate_0;
		}
	}
}
