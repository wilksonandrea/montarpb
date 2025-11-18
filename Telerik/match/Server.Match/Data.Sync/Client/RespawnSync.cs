using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Match;
using Server.Match.Data.Managers;
using Server.Match.Data.Models;
using Server.Match.Data.Utils;
using Server.Match.Data.XML;
using Server.Match.Network.Packets;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Server.Match.Data.Sync.Client
{
	public class RespawnSync
	{
		public RespawnSync()
		{
		}

		public static void Load(SyncClientPacket C)
		{
			int i;
			Predicate<AssistServerData> predicate = null;
			uint uInt32 = C.ReadUD();
			uint uInt321 = C.ReadUD();
			long ınt64 = C.ReadQ();
			int ınt32 = C.ReadC();
			int ınt321 = C.ReadC();
			int ınt322 = C.ReadC();
			int ınt323 = C.ReadC();
			int ınt324 = C.ReadC();
			int ınt325 = 0;
			int ınt326 = 0;
			int ınt327 = 0;
			int ınt328 = 0;
			int ınt329 = 0;
			int ınt3210 = 0;
			int ınt3211 = 0;
			int ınt3212 = 0;
			bool flag = false;
			if (ınt32 != 0)
			{
				if (ınt32 == 2)
				{
					goto Label2;
				}
				if ((int)C.ToArray().Length > 23)
				{
					CLogger.Print(string.Format("RespawnSync (Length > 23): {0}", (int)C.ToArray().Length), LoggerType.Warning, null);
					goto Label0;
				}
				else
				{
					goto Label0;
				}
			}
		Label2:
			ınt325 = C.ReadD();
			ınt326 = C.ReadC();
			flag = C.ReadC() == 1;
			ınt327 = C.ReadD();
			ınt328 = C.ReadD();
			ınt329 = C.ReadD();
			ınt3210 = C.ReadD();
			ınt3211 = C.ReadD();
			ınt3212 = C.ReadD();
			if ((int)C.ToArray().Length > 53)
			{
				CLogger.Print(string.Format("RespawnSync (Length > 53): {0}", (int)C.ToArray().Length), LoggerType.Warning, null);
			}
		Label0:
			RoomModel room = RoomsManager.GetRoom(uInt32, uInt321);
			if (room == null)
			{
				return;
			}
			room.ResyncTick(ınt64, uInt321);
			PlayerModel player = room.GetPlayer(ınt322, true);
			if (player != null && player.PlayerIdByUser != ınt324)
			{
				CLogger.Print(string.Format("Invalid User Ids: [By User: {0} / Server: {1}]", player.PlayerIdByUser, ınt324), LoggerType.Warning, null);
			}
			if (player != null && player.PlayerIdByUser == ınt324)
			{
				player.PlayerIdByServer = ınt324;
				player.RespawnByServer = ınt323;
				player.Integrity = false;
				if (ınt321 > room.ServerRound)
				{
					room.ServerRound = ınt321;
				}
				if (ınt32 == 0 || ınt32 == 2)
				{
					AssistServerData assist = AssistManager.GetAssist(player.Slot, room.RoomId);
					if (assist != null && AssistManager.RemoveAssist(assist))
					{
						List<AssistServerData> assists = AssistManager.Assists;
						Predicate<AssistServerData> predicate1 = predicate;
						if (predicate1 == null)
						{
							Predicate<AssistServerData> victim = (AssistServerData assistServerData_0) => {
								if (assistServerData_0.Victim != player.Slot)
								{
									return false;
								}
								return assistServerData_0.RoomId == room.RoomId;
							};
							Predicate<AssistServerData> predicate2 = victim;
							predicate = victim;
							predicate1 = predicate2;
						}
						foreach (AssistServerData assistServerDatum in assists.FindAll(predicate1))
						{
							AssistManager.RemoveAssist(assistServerDatum);
						}
					}
					Equipment equipment = new Equipment()
					{
						WpnPrimary = ınt327,
						WpnSecondary = ınt328,
						WpnMelee = ınt329,
						WpnExplosive = ınt3210,
						WpnSpecial = ınt3211,
						Accessory = ınt3212
					};
					player.Dead = false;
					player.PlantDuration = ConfigLoader.PlantDuration;
					player.DefuseDuration = ConfigLoader.DefuseDuration;
					player.Equip = equipment;
					if (flag)
					{
						player.PlantDuration -= ComDiv.Percentage(ConfigLoader.PlantDuration, 50);
						player.DefuseDuration -= ComDiv.Percentage(ConfigLoader.DefuseDuration, 25);
					}
					if (!room.BotMode)
					{
						if (room.SourceToMap != -1)
						{
							room.RoundResetRoomS1(ınt321);
						}
						else
						{
							room.RoundResetRoomF1(ınt321);
						}
					}
					if (ınt325 != -1)
					{
						player.Immortal = false;
						int charaHP = CharaStructureXML.GetCharaHP(ınt325);
						charaHP += ComDiv.Percentage(charaHP, ınt326);
						player.MaxLife = charaHP;
						player.ResetLife();
					}
					else
					{
						player.Immortal = true;
					}
				}
				if (room.BotMode || ınt32 == 2 || !room.ObjectsIsValid())
				{
					return;
				}
				List<ObjectHitInfo> objectHitInfos = new List<ObjectHitInfo>();
				ObjectInfo[] objects = room.Objects;
				for (i = 0; i < (int)objects.Length; i++)
				{
					ObjectInfo objectInfo = objects[i];
					ObjectModel model = objectInfo.Model;
					if (model != null && (ınt32 != 2 && model.Destroyable && objectInfo.Life != model.Life || model.NeedSync))
					{
						objectHitInfos.Add(new ObjectHitInfo(3)
						{
							ObjSyncId = model.NeedSync,
							AnimId1 = model.Animation,
							AnimId2 = (objectInfo.Animation != null ? objectInfo.Animation.Id : 255),
							DestroyState = objectInfo.DestroyState,
							ObjId = model.Id,
							ObjLife = objectInfo.Life,
							SpecialUse = AllUtils.GetDuration(objectInfo.UseDate)
						});
					}
				}
				PlayerModel[] players = room.Players;
				for (i = 0; i < (int)players.Length; i++)
				{
					PlayerModel playerModel = players[i];
					if (playerModel.Slot != ınt322 && playerModel.AccountIdIsValid() && !playerModel.Immortal)
					{
						DateTime startTime = playerModel.StartTime;
						DateTime dateTime = new DateTime();
						if (startTime != dateTime && (playerModel.MaxLife != playerModel.Life || playerModel.Dead))
						{
							objectHitInfos.Add(new ObjectHitInfo(4)
							{
								ObjId = playerModel.Slot,
								ObjLife = playerModel.Life
							});
						}
					}
				}
				if (objectHitInfos.Count > 0)
				{
					byte[] cODE = PROTOCOL_EVENTS_ACTION.GET_CODE(objectHitInfos);
					byte[] numArray = AllUtils.BaseWriteCode(4, cODE, 255, AllUtils.GetDuration(room.StartTime), ınt321, ınt323, 0, ınt324);
					MatchXender.Client.SendPacket(numArray, player.Client);
				}
				objectHitInfos.Clear();
				objectHitInfos = null;
			}
		}
	}
}