using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.SharpDX;
using Plugin.Core.Utility;
using Server.Match.Data.Enums;
using Server.Match.Data.Managers;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;
using Server.Match.Data.Models.Event.Event;
using Server.Match.Data.Models.SubHead;
using Server.Match.Data.Sync.Server;
using Server.Match.Data.Utils;
using Server.Match.Network.Actions.Event;
using Server.Match.Network.Actions.SubHead;
using Server.Match.Network.Packets;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Server.Match
{
	public class MatchClient
	{
		private readonly Socket socket_0;

		private readonly IPEndPoint ipendPoint_0;

		public MatchClient(Socket socket_1, IPEndPoint ipendPoint_1)
		{
			this.socket_0 = socket_1;
			this.ipendPoint_0 = ipendPoint_1;
		}

		public void BeginReceive(byte[] Buffer, DateTime Date)
		{
			PacketModel packetModel = new PacketModel()
			{
				Data = Buffer,
				ReceiveDate = Date
			};
			SyncClientPacket syncClientPacket = new SyncClientPacket(packetModel.Data);
			packetModel.Opcode = syncClientPacket.ReadC();
			packetModel.Slot = syncClientPacket.ReadC();
			packetModel.Time = syncClientPacket.ReadT();
			packetModel.Round = syncClientPacket.ReadC();
			packetModel.Length = syncClientPacket.ReadUH();
			packetModel.Respawn = syncClientPacket.ReadC();
			packetModel.RoundNumber = syncClientPacket.ReadC();
			packetModel.AccountId = syncClientPacket.ReadC();
			packetModel.Unk1 = syncClientPacket.ReadC();
			packetModel.Unk2 = syncClientPacket.ReadD();
			if (packetModel.Length > (int)packetModel.Data.Length)
			{
				CLogger.Print(string.Format("Packet with invalid size canceled. [ Length: {0} DataLength: {1} ]", packetModel.Length, (int)packetModel.Data.Length), LoggerType.Warning, null);
				return;
			}
			AllUtils.GetDecryptedData(packetModel);
			if (ConfigLoader.IsTestMode && packetModel.Unk1 > 0)
			{
				CLogger.Print(Bitwise.ToHexData(string.Format("[N] Test Mode, Packet Unk: {0}", packetModel.Unk1), packetModel.Data), LoggerType.Opcode, null);
				CLogger.Print(Bitwise.ToHexData(string.Format("[D] Test Mode, Packet Unk: {0}", packetModel.Unk1), packetModel.WithoutEndData), LoggerType.Opcode, null);
			}
			if (ConfigLoader.EnableLog && packetModel.Opcode != 131 && packetModel.Opcode != 132 && packetModel.Opcode != 3)
			{
				int opcode = packetModel.Opcode;
			}
			this.ReadPacket(packetModel);
		}

		private List<ObjectHitInfo> method_0(ActionModel actionModel_0, RoomModel roomModel_0, float float_0, out byte[] byte_0)
		{
			int ınt32;
			PlayerModel playerModel;
			int ınt321;
			PlayerModel playerModel1;
			List<ObjectHitInfo> objectHitInfos;
			ObjectModel model;
			ObjectModel objectModel;
			Half3 bombPosition;
			object obj;
			byte_0 = new byte[0];
			if (roomModel_0 == null)
			{
				return null;
			}
			if (actionModel_0.Data.Length == 0)
			{
				return new List<ObjectHitInfo>();
			}
			byte[] data = actionModel_0.Data;
			List<ObjectHitInfo> objectHitInfos1 = new List<ObjectHitInfo>();
			SyncClientPacket syncClientPacket = new SyncClientPacket(data);
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				uint uInt32 = 0;
				PlayerModel player = roomModel_0.GetPlayer((int)actionModel_0.Slot, false);
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.ActionState))
				{
					uInt32 += 256;
					ActionStateInfo actionStateInfo = ActionState.ReadInfo(actionModel_0, syncClientPacket, false);
					if (!roomModel_0.BotMode)
					{
						Equipment equip = player.Equip;
						if (player != null && equip != null)
						{
							int wpnPrimary = 0;
							byte num = 0;
							byte num1 = 0;
							if (actionStateInfo.Flag.HasFlag(WeaponSyncType.Primary))
							{
								wpnPrimary = equip.WpnPrimary;
								int ıdStatics = ComDiv.GetIdStatics(equip.Accessory, 1);
								int ıdStatics1 = ComDiv.GetIdStatics(equip.Accessory, 3);
								if (ıdStatics == 30)
								{
									obj = ıdStatics1;
								}
								else
								{
									obj = num;
								}
								num = (byte)obj;
							}
							if (actionStateInfo.Flag.HasFlag(WeaponSyncType.Secondary))
							{
								wpnPrimary = equip.WpnSecondary;
							}
							if (actionStateInfo.Flag.HasFlag(WeaponSyncType.Melee))
							{
								wpnPrimary = equip.WpnMelee;
							}
							if (actionStateInfo.Flag.HasFlag(WeaponSyncType.Explosive))
							{
								wpnPrimary = equip.WpnExplosive;
							}
							if (actionStateInfo.Flag.HasFlag(WeaponSyncType.Special))
							{
								wpnPrimary = equip.WpnSpecial;
							}
							if (actionStateInfo.Flag.HasFlag(WeaponSyncType.Mission) && roomModel_0.RoomType == RoomCondition.Bomb)
							{
								wpnPrimary = 5009001;
							}
							if (actionStateInfo.Flag.HasFlag(WeaponSyncType.Dual))
							{
								num1 = 16;
								if (actionStateInfo.Action.HasFlag(ActionFlag.Unk2048))
								{
									wpnPrimary = equip.WpnPrimary;
								}
								if (actionStateInfo.Action.HasFlag(ActionFlag.Unk4096))
								{
									wpnPrimary = equip.WpnPrimary;
								}
							}
							if (actionStateInfo.Flag.HasFlag(WeaponSyncType.Ext))
							{
								num1 = 16;
								if (actionStateInfo.Action.HasFlag(ActionFlag.Unk2048))
								{
									wpnPrimary = equip.WpnSecondary;
								}
								if (actionStateInfo.Action.HasFlag(ActionFlag.Unk4096))
								{
									wpnPrimary = equip.WpnSecondary;
								}
							}
							objectHitInfos1.Add(new ObjectHitInfo(6)
							{
								ObjId = player.Slot,
								WeaponId = wpnPrimary,
								Accessory = num,
								Extensions = num1
							});
						}
					}
					ActionState.WriteInfo(syncServerPacket, actionStateInfo);
					actionStateInfo = null;
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.Animation))
				{
					uInt32 += 2;
					Animation.WriteInfo(syncServerPacket, Animation.ReadInfo(actionModel_0, syncClientPacket, false));
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.PosRotation))
				{
					uInt32 += 134217728;
					PosRotationInfo posRotationInfo = PosRotation.ReadInfo(actionModel_0, syncClientPacket, false);
					if (player != null)
					{
						player.Position = new Half3(posRotationInfo.RotationX, posRotationInfo.RotationY, posRotationInfo.RotationZ);
					}
					ActionModel actionModel0 = actionModel_0;
					actionModel0.Flag = actionModel0.Flag | UdpGameEvent.SoundPosRotation;
					PosRotation.WriteInfo(syncServerPacket, posRotationInfo);
					posRotationInfo = null;
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.SoundPosRotation))
				{
					uInt32 += 8388608;
					SoundPosRotationInfo soundPosRotationInfo = SoundPosRotation.ReadInfo(actionModel_0, syncClientPacket, float_0, false);
					SoundPosRotation.WriteInfo(syncServerPacket, soundPosRotationInfo);
					soundPosRotationInfo = null;
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.UseObject))
				{
					uInt32 += 4;
					List<UseObjectInfo> useObjectInfos = UseObject.ReadInfo(actionModel_0, syncClientPacket, false);
					for (int i = 0; i < useObjectInfos.Count; i++)
					{
						UseObjectInfo ıtem = useObjectInfos[i];
						if (roomModel_0.BotMode || ıtem.ObjectId == 65535)
						{
							int ınt322 = i;
							i = ınt322 - 1;
							AllUtils.RemoveHit(useObjectInfos, ınt322);
						}
						else
						{
							ObjectInfo objectInfo = roomModel_0.GetObject((int)ıtem.ObjectId);
							if (objectInfo != null)
							{
								bool flag = false;
								if (ıtem.SpaceFlags.HasFlag(CharaMoves.HeliInMove) && objectInfo.UseDate.ToString("yyMMddHHmm").Equals("0101010000"))
								{
									flag = true;
								}
								ıtem.SpaceFlags.HasFlag(CharaMoves.HeliUnknown);
								ıtem.SpaceFlags.HasFlag(CharaMoves.HeliLeave);
								if (ıtem.SpaceFlags.HasFlag(CharaMoves.HeliStopped))
								{
									AnimModel animation = objectInfo.Animation;
									if (animation != null && animation.Id == 0)
									{
										objectInfo.Model.GetAnim(animation.NextAnim, 0f, 0f, objectInfo);
									}
								}
								if (!flag)
								{
									objectHitInfos1.Add(new ObjectHitInfo(3)
									{
										ObjSyncId = 1,
										ObjId = objectInfo.Id,
										ObjLife = objectInfo.Life,
										AnimId1 = 255,
										AnimId2 = (objectInfo.Animation != null ? objectInfo.Animation.Id : 255),
										SpecialUse = AllUtils.GetDuration(objectInfo.UseDate)
									});
								}
							}
						}
					}
					UseObject.WriteInfo(syncServerPacket, useObjectInfos);
					useObjectInfos = null;
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.ActionForObjectSync))
				{
					uInt32 += 16;
					ActionObjectInfo actionObjectInfo = ActionForObjectSync.ReadInfo(actionModel_0, syncClientPacket, false);
					if (player != null)
					{
						roomModel_0.SyncInfo(objectHitInfos1, 1);
					}
					ActionForObjectSync.WriteInfo(syncServerPacket, actionObjectInfo);
					actionObjectInfo = null;
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.RadioChat))
				{
					uInt32 += 32;
					RadioChat.WriteInfo(syncServerPacket, RadioChat.ReadInfo(actionModel_0, syncClientPacket, false));
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.WeaponSync))
				{
					uInt32 += 67108864;
					WeaponSyncInfo weaponSyncInfo = WeaponSync.ReadInfo(actionModel_0, syncClientPacket, false, false);
					if (player != null)
					{
						player.WeaponId = weaponSyncInfo.WeaponId;
						player.Accessory = weaponSyncInfo.Accessory;
						player.Extensions = weaponSyncInfo.Extensions;
						player.WeaponClass = weaponSyncInfo.WeaponClass;
						roomModel_0.SyncInfo(objectHitInfos1, 2);
					}
					WeaponSync.WriteInfo(syncServerPacket, weaponSyncInfo);
					weaponSyncInfo = null;
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.WeaponRecoil))
				{
					uInt32 += 128;
					WeaponRecoil.WriteInfo(syncServerPacket, WeaponRecoil.ReadInfo(actionModel_0, syncClientPacket, false));
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.HpSync))
				{
					uInt32 += 8;
					HpSync.WriteInfo(syncServerPacket, HpSync.ReadInfo(actionModel_0, syncClientPacket, false));
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.Suicide))
				{
					uInt32 += 1048576;
					List<SuicideInfo> suicideInfos = Suicide.ReadInfo(actionModel_0, syncClientPacket, false, false);
					List<DeathServerData> deathServerDatas = new List<DeathServerData>();
					if (player == null)
					{
						suicideInfos = new List<SuicideInfo>();
					}
					else
					{
						int ınt323 = -1;
						int weaponId = 0;
						for (int j = 0; j < suicideInfos.Count; j++)
						{
							SuicideInfo suicideInfo = suicideInfos[j];
							if (player.Dead || player.Life <= 0)
							{
								int ınt324 = j;
								j = ınt324 - 1;
								AllUtils.RemoveHit(suicideInfos, ınt324);
							}
							else
							{
								int hitDamageBot = AllUtils.GetHitDamageBot(suicideInfo.HitInfo);
								int killerId = AllUtils.GetKillerId(suicideInfo.HitInfo);
								int objectType = AllUtils.GetObjectType(suicideInfo.HitInfo);
								CharaHitPart hitInfo = (CharaHitPart)(suicideInfo.HitInfo >> 4 & 63);
								CharaDeath charaDeath = AllUtils.GetCharaDeath(suicideInfo.HitInfo);
								if (objectType == 1 || objectType == 0)
								{
									ınt323 = killerId;
								}
								weaponId = suicideInfo.WeaponId;
								DamageManager.SimpleDeath(roomModel_0, deathServerDatas, objectHitInfos1, player, player, hitDamageBot, weaponId, hitInfo, charaDeath);
								if (hitDamageBot > 0)
								{
									if (ConfigLoader.UseHitMarker)
									{
										SendMatchInfo.SendHitMarkerSync(roomModel_0, player, charaDeath, HitType.Normal, hitDamageBot);
									}
									objectHitInfos1.Add(new ObjectHitInfo(2)
									{
										ObjId = player.Slot,
										ObjLife = player.Life,
										HitPart = hitInfo,
										KillerSlot = ınt323,
										Position = suicideInfo.PlayerPos,
										DeathType = charaDeath,
										WeaponId = weaponId
									});
								}
							}
						}
						if (deathServerDatas.Count > 0)
						{
							SendMatchInfo.SendDeathSync(roomModel_0, player, ınt323, weaponId, deathServerDatas);
						}
						deathServerDatas = null;
					}
					Suicide.WriteInfo(syncServerPacket, suicideInfos);
					deathServerDatas = null;
					suicideInfos = null;
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.MissionData))
				{
					uInt32 += 2048;
					MissionDataInfo missionDataInfo = MissionData.ReadInfo(actionModel_0, syncClientPacket, float_0, false, false);
					if (roomModel_0.Map != null && player != null && !player.Dead && missionDataInfo.PlantTime > 0f && !missionDataInfo.BombEnum.HasFlag(BombFlag.Stop))
					{
						BombPosition bomb = roomModel_0.Map.GetBomb(missionDataInfo.BombId);
						if (bomb != null)
						{
							bool flag1 = missionDataInfo.BombEnum.HasFlag(BombFlag.Defuse);
							bool flag2 = flag1;
							if (flag1)
							{
								bombPosition = roomModel_0.BombPosition;
							}
							else
							{
								bombPosition = (missionDataInfo.BombEnum.HasFlag(BombFlag.Start) ? bomb.Position : new Half3(0, 0, 0));
							}
							double num2 = (double)Vector3.Distance(player.Position, bombPosition);
							TeamEnum swappedTeam = AllUtils.GetSwappedTeam(player, roomModel_0);
							if ((bomb.EveryWhere || num2 <= 2) && (swappedTeam == TeamEnum.CT_TEAM & flag2 || swappedTeam == TeamEnum.FR_TEAM && !flag2))
							{
								if (player.C4Time != missionDataInfo.PlantTime)
								{
									player.C4First = DateTimeUtil.Now();
									player.C4Time = missionDataInfo.PlantTime;
								}
								double duration = ComDiv.GetDuration(player.C4First);
								float single = (flag2 ? player.DefuseDuration : player.PlantDuration);
								if ((float_0 >= missionDataInfo.PlantTime + single || duration >= (double)single) && (!roomModel_0.HasC4 && missionDataInfo.BombEnum.HasFlag(BombFlag.Start) || roomModel_0.HasC4 & flag2))
								{
									roomModel_0.HasC4 = !roomModel_0.HasC4;
									missionDataInfo.Bomb |= 2;
									SendMatchInfo.SendBombSync(roomModel_0, player, missionDataInfo.BombEnum.HasFlag(BombFlag.Defuse), missionDataInfo.BombId);
								}
							}
						}
					}
					MissionData.WriteInfo(syncServerPacket, missionDataInfo);
					missionDataInfo = null;
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.DropWeapon))
				{
					uInt32 += 4194304;
					DropWeaponInfo dropCounter = DropWeapon.ReadInfo(actionModel_0, syncClientPacket, false);
					if (!roomModel_0.BotMode)
					{
						RoomModel roomModel0 = roomModel_0;
						roomModel0.DropCounter = (byte)(roomModel0.DropCounter + 1);
						if (roomModel_0.DropCounter > ConfigLoader.MaxDropWpnCount)
						{
							roomModel_0.DropCounter = 0;
						}
						dropCounter.Counter = roomModel_0.DropCounter;
						Equipment equipment = player.Equip;
						if (player != null && equipment != null)
						{
							int ıdStatics2 = ComDiv.GetIdStatics(dropCounter.WeaponId, 1);
							if (ıdStatics2 == 1)
							{
								equipment.WpnPrimary = 0;
							}
							if (ıdStatics2 == 2)
							{
								equipment.WpnSecondary = 0;
							}
						}
					}
					DropWeapon.WriteInfo(syncServerPacket, dropCounter);
					dropCounter = null;
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.GetWeaponForClient))
				{
					uInt32 += 16777216;
					WeaponClient weaponClient = GetWeaponForClient.ReadInfo(actionModel_0, syncClientPacket, false);
					if (!roomModel_0.BotMode)
					{
						Equipment equip1 = player.Equip;
						if (player != null && equip1 != null)
						{
							int ıdStatics3 = ComDiv.GetIdStatics(weaponClient.WeaponId, 1);
							if (ıdStatics3 == 1)
							{
								equip1.WpnPrimary = weaponClient.WeaponId;
							}
							if (ıdStatics3 == 2)
							{
								equip1.WpnSecondary = weaponClient.WeaponId;
							}
						}
					}
					GetWeaponForClient.WriteInfo(syncServerPacket, weaponClient);
					weaponClient = null;
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.FireData))
				{
					uInt32 += 33554432;
					FireData.WriteInfo(syncServerPacket, FireData.ReadInfo(actionModel_0, syncClientPacket, false));
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.CharaFireNHitData))
				{
					uInt32 += 1024;
					List<CharaFireNHitDataInfo> charaFireNHitDataInfos = CharaFireNHitData.ReadInfo(actionModel_0, syncClientPacket, false, false);
					CharaFireNHitData.WriteInfo(syncServerPacket, charaFireNHitDataInfos);
					charaFireNHitDataInfos = null;
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.HitData))
				{
					uInt32 += 131072;
					List<HitDataInfo> hitDataInfos = HitData.ReadInfo(actionModel_0, syncClientPacket, false, false);
					List<DeathServerData> deathServerDatas1 = new List<DeathServerData>();
					if (player == null)
					{
						hitDataInfos = new List<HitDataInfo>();
					}
					else
					{
						int weaponId1 = 0;
						for (int k = 0; k < hitDataInfos.Count; k++)
						{
							HitDataInfo hitDataInfo = hitDataInfos[k];
							if (hitDataInfo.HitEnum != HitType.HelmetProtection && hitDataInfo.HitEnum != HitType.HeadshotProtection)
							{
								if (!AllUtils.ValidateHitData(AllUtils.GetHitDamageNormal(hitDataInfo.HitIndex), hitDataInfo, out ınt32))
								{
									int ınt325 = k;
									k = ınt325 - 1;
									AllUtils.RemoveHit(hitDataInfos, ınt325);
								}
								else
								{
									int objectId = hitDataInfo.ObjectId;
									CharaHitPart hitPart = AllUtils.GetHitPart(hitDataInfo.HitIndex);
									CharaDeath charaDeath1 = CharaDeath.DEFAULT;
									weaponId1 = hitDataInfo.WeaponId;
									ObjectType hitType = AllUtils.GetHitType(hitDataInfo.HitIndex);
									if (hitType == ObjectType.Object)
									{
										ObjectInfo obj1 = roomModel_0.GetObject(objectId);
										if (obj1 != null)
										{
											objectModel = obj1.Model;
										}
										else
										{
											objectModel = null;
										}
										ObjectModel objectModel1 = objectModel;
										if (objectModel1 != null && objectModel1.Destroyable)
										{
											if (obj1.Life > 0)
											{
												ObjectInfo life = obj1;
												life.Life = life.Life - ınt32;
												if (obj1.Life <= 0)
												{
													obj1.Life = 0;
													DamageManager.BoomDeath(roomModel_0, player, ınt32, weaponId1, deathServerDatas1, objectHitInfos1, hitDataInfo.BoomPlayers, CharaHitPart.ALL, CharaDeath.OBJECT_EXPLOSION);
												}
												obj1.DestroyState = objectModel1.CheckDestroyState(obj1.Life);
												DamageManager.SabotageDestroy(roomModel_0, player, objectModel1, obj1, ınt32);
												float duration1 = AllUtils.GetDuration(obj1.UseDate);
												if (obj1.Animation != null && obj1.Animation.Duration > 0f && duration1 >= obj1.Animation.Duration)
												{
													obj1.Model.GetAnim(obj1.Animation.NextAnim, duration1, obj1.Animation.Duration, obj1);
												}
												objectHitInfos1.Add(new ObjectHitInfo(objectModel1.UpdateId)
												{
													ObjId = obj1.Id,
													ObjLife = obj1.Life,
													KillerSlot = actionModel_0.Slot,
													ObjSyncId = objectModel1.NeedSync,
													SpecialUse = duration1,
													AnimId1 = objectModel1.Animation,
													AnimId2 = (obj1.Animation != null ? obj1.Animation.Id : 255),
													DestroyState = obj1.DestroyState
												});
											}
										}
										else if (ConfigLoader.SendFailMsg && objectModel1 == null)
										{
											CLogger.Print(string.Format("Fire Obj: {0} Map: {1} Invalid Object.", objectId, roomModel_0.MapId), LoggerType.Warning, null);
											player.LogPlayerPos(hitDataInfo.EndBullet);
										}
									}
									else if (hitType == ObjectType.User)
									{
										if (!roomModel_0.GetPlayer(objectId, out playerModel) || !player.RespawnIsValid() || player.Dead || playerModel.Dead || playerModel.Immortal)
										{
											int ınt326 = k;
											k = ınt326 - 1;
											AllUtils.RemoveHit(hitDataInfos, ınt326);
										}
										else
										{
											if (hitPart == CharaHitPart.HEAD)
											{
												charaDeath1 = CharaDeath.HEADSHOT;
											}
											if (roomModel_0.RoomType == RoomCondition.DeathMatch && roomModel_0.Rule == MapRules.HeadHunter && charaDeath1 != CharaDeath.HEADSHOT)
											{
												ınt32 = 1;
											}
											else if (roomModel_0.RoomType == RoomCondition.Boss && charaDeath1 == CharaDeath.HEADSHOT)
											{
												if (roomModel_0.LastRound == 1 && playerModel.Team == TeamEnum.FR_TEAM || roomModel_0.LastRound == 2 && playerModel.Team == TeamEnum.CT_TEAM)
												{
													ınt32 /= 10;
												}
											}
											else if (roomModel_0.RoomType == RoomCondition.DeathMatch && roomModel_0.Rule == MapRules.Chaos)
											{
												ınt32 = 200;
											}
											DamageManager.SimpleDeath(roomModel_0, deathServerDatas1, objectHitInfos1, player, playerModel, ınt32, weaponId1, hitPart, charaDeath1);
											if (ınt32 > 0)
											{
												if (ConfigLoader.UseHitMarker)
												{
													SendMatchInfo.SendHitMarkerSync(roomModel_0, player, charaDeath1, hitDataInfo.HitEnum, ınt32);
												}
												objectHitInfos1.Add(new ObjectHitInfo(2)
												{
													ObjId = playerModel.Slot,
													ObjLife = playerModel.Life,
													HitPart = hitPart,
													KillerSlot = player.Slot,
													Position = playerModel.Position - player.Position,
													DeathType = charaDeath1,
													WeaponId = weaponId1
												});
											}
										}
									}
									else if (hitType != ObjectType.UserObject)
									{
										CLogger.Print(string.Format("HitType: ({0}/{1}) Slot: {2}", hitType, (int)hitType, actionModel_0.Slot), LoggerType.Warning, null);
										CLogger.Print(string.Format("BoomPlayers: {0} {1}", hitDataInfo.BoomInfo, hitDataInfo.BoomPlayers.Count), LoggerType.Warning, null);
									}
								}
							}
						}
						if (deathServerDatas1.Count > 0)
						{
							SendMatchInfo.SendDeathSync(roomModel_0, player, 255, weaponId1, deathServerDatas1);
						}
					}
					HitData.WriteInfo(syncServerPacket, hitDataInfos);
					deathServerDatas1 = null;
					hitDataInfos = null;
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.GrenadeHit))
				{
					uInt32 += 268435456;
					List<GrenadeHitInfo> grenadeHitInfos = GrenadeHit.ReadInfo(actionModel_0, syncClientPacket, false, false);
					List<DeathServerData> deathServerDatas2 = new List<DeathServerData>();
					if (player == null)
					{
						grenadeHitInfos = new List<GrenadeHitInfo>();
					}
					else
					{
						int ınt327 = -1;
						int weaponId2 = 0;
						for (int l = 0; l < grenadeHitInfos.Count; l++)
						{
							GrenadeHitInfo grenadeHitInfo = grenadeHitInfos[l];
							if (!AllUtils.ValidateGrenadeHit(AllUtils.GetHitDamageNormal(grenadeHitInfo.HitInfo), grenadeHitInfo, out ınt321))
							{
								int ınt328 = l;
								l = ınt328 - 1;
								AllUtils.RemoveHit(grenadeHitInfos, ınt328);
							}
							else
							{
								int objectId1 = grenadeHitInfo.ObjectId;
								CharaHitPart charaHitPart = AllUtils.GetHitPart(grenadeHitInfo.HitInfo);
								weaponId2 = grenadeHitInfo.WeaponId;
								ObjectType hitType1 = AllUtils.GetHitType(grenadeHitInfo.HitInfo);
								if (hitType1 == ObjectType.Object)
								{
									ObjectInfo objectInfo1 = roomModel_0.GetObject(objectId1);
									if (objectInfo1 != null)
									{
										model = objectInfo1.Model;
									}
									else
									{
										model = null;
									}
									ObjectModel objectModel2 = model;
									if (objectModel2 != null && objectModel2.Destroyable && objectInfo1.Life > 0)
									{
										ObjectInfo life1 = objectInfo1;
										life1.Life = life1.Life - ınt321;
										if (objectInfo1.Life <= 0)
										{
											objectInfo1.Life = 0;
											DamageManager.BoomDeath(roomModel_0, player, ınt321, weaponId2, deathServerDatas2, objectHitInfos1, grenadeHitInfo.BoomPlayers, CharaHitPart.ALL, CharaDeath.OBJECT_EXPLOSION);
										}
										objectInfo1.DestroyState = objectModel2.CheckDestroyState(objectInfo1.Life);
										DamageManager.SabotageDestroy(roomModel_0, player, objectModel2, objectInfo1, ınt321);
										if (ınt321 > 0)
										{
											objectHitInfos1.Add(new ObjectHitInfo(objectModel2.UpdateId)
											{
												ObjId = objectInfo1.Id,
												ObjLife = objectInfo1.Life,
												KillerSlot = actionModel_0.Slot,
												ObjSyncId = objectModel2.NeedSync,
												AnimId1 = objectModel2.Animation,
												AnimId2 = (objectInfo1.Animation != null ? objectInfo1.Animation.Id : 255),
												DestroyState = objectInfo1.DestroyState,
												SpecialUse = AllUtils.GetDuration(objectInfo1.UseDate)
											});
										}
									}
									else if (ConfigLoader.SendFailMsg && objectModel2 == null)
									{
										CLogger.Print(string.Format("Boom Obj: {0} Map: {1} Invalid Object.", objectId1, roomModel_0.MapId), LoggerType.Warning, null);
										player.LogPlayerPos(grenadeHitInfo.HitPos);
									}
								}
								else if (hitType1 == ObjectType.User)
								{
									ınt327++;
									if (ınt321 <= 0 || !roomModel_0.GetPlayer(objectId1, out playerModel1) || !player.RespawnIsValid() || playerModel1.Dead || playerModel1.Immortal)
									{
										int ınt329 = l;
										l = ınt329 - 1;
										AllUtils.RemoveHit(grenadeHitInfos, ınt329);
									}
									else
									{
										TeamEnum teamEnum = (ınt327 % 2 == 0 ? TeamEnum.FR_TEAM : TeamEnum.CT_TEAM);
										if (grenadeHitInfo.DeathType == CharaDeath.MEDICAL_KIT)
										{
											playerModel1.Life += ınt321;
											playerModel1.CheckLifeValue();
										}
										else if (grenadeHitInfo.DeathType != CharaDeath.BOOM || ClassType.Dino == grenadeHitInfo.WeaponClass || teamEnum != TeamEnum.FR_TEAM && teamEnum != TeamEnum.CT_TEAM)
										{
											DamageManager.SimpleDeath(roomModel_0, deathServerDatas2, objectHitInfos1, player, playerModel1, ınt321, weaponId2, charaHitPart, grenadeHitInfo.DeathType);
										}
										else
										{
											ınt321 = (int)Math.Ceiling((double)ınt321 / 2.7);
											DamageManager.SimpleDeath(roomModel_0, deathServerDatas2, objectHitInfos1, player, playerModel1, ınt321, weaponId2, charaHitPart, grenadeHitInfo.DeathType);
										}
										if (ınt321 > 0)
										{
											if (ConfigLoader.UseHitMarker)
											{
												SendMatchInfo.SendHitMarkerSync(roomModel_0, player, grenadeHitInfo.DeathType, grenadeHitInfo.HitEnum, ınt321);
											}
											objectHitInfos1.Add(new ObjectHitInfo(2)
											{
												ObjId = playerModel1.Slot,
												ObjLife = playerModel1.Life,
												HitPart = charaHitPart,
												KillerSlot = player.Slot,
												Position = playerModel1.Position - player.Position,
												DeathType = grenadeHitInfo.DeathType,
												WeaponId = weaponId2
											});
										}
									}
								}
								else if (hitType1 != ObjectType.UserObject)
								{
									CLogger.Print(string.Format("Grenade Boom, HitType: ({0}/{1})", hitType1, (int)hitType1), LoggerType.Warning, null);
								}
							}
						}
						if (deathServerDatas2.Count > 0)
						{
							SendMatchInfo.SendDeathSync(roomModel_0, player, 255, weaponId2, deathServerDatas2);
						}
					}
					GrenadeHit.WriteInfo(syncServerPacket, grenadeHitInfos);
					deathServerDatas2 = null;
					grenadeHitInfos = null;
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.GetWeaponForHost))
				{
					uInt32 += 512;
					GetWeaponForHost.WriteInfo(syncServerPacket, GetWeaponForHost.ReadInfo(actionModel_0, syncClientPacket, false));
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.FireDataOnObject))
				{
					uInt32 += 1073741824;
					FireDataOnObject.WriteInfo(syncServerPacket, FireDataOnObject.ReadInfo(actionModel_0, syncClientPacket, false));
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.FireNHitDataOnObject))
				{
					uInt32 += 8192;
					FireNHitDataObjectInfo fireNHitDataObjectInfo = FireNHitDataOnObject.ReadInfo(actionModel_0, syncClientPacket, false);
					if (player != null && !player.Dead)
					{
						SendMatchInfo.SendPortalPass(roomModel_0, player, (int)fireNHitDataObjectInfo.Portal);
					}
					FireNHitDataOnObject.WriteInfo(syncServerPacket, fireNHitDataObjectInfo);
					fireNHitDataObjectInfo = null;
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.SeizeDataForClient))
				{
					uInt32 += 32768;
					SeizeDataForClientInfo seizeDataForClientInfo = SeizeDataForClient.ReadInfo(actionModel_0, syncClientPacket, true);
					player != null;
					SeizeDataForClient.WriteInfo(syncServerPacket, seizeDataForClientInfo);
					seizeDataForClientInfo = null;
				}
				byte_0 = syncServerPacket.ToArray();
				if (uInt32 != (uint)actionModel_0.Flag && ConfigLoader.IsTestMode)
				{
					CLogger.Print(Bitwise.ToHexData(string.Format("PVP - Missing Flag Events: '{0}' | '{1}'", (uint)actionModel_0.Flag, (uint)((uint)actionModel_0.Flag - uInt32)), data), LoggerType.Opcode, null);
				}
				objectHitInfos = objectHitInfos1;
			}
			return objectHitInfos;
		}

		private List<ObjectHitInfo> method_1(ActionModel actionModel_0, RoomModel roomModel_0, out byte[] byte_0)
		{
			List<ObjectHitInfo> objectHitInfos;
			byte_0 = new byte[0];
			if (roomModel_0 == null)
			{
				return null;
			}
			if (actionModel_0.Data.Length == 0)
			{
				return new List<ObjectHitInfo>();
			}
			byte[] data = actionModel_0.Data;
			List<ObjectHitInfo> objectHitInfos1 = new List<ObjectHitInfo>();
			SyncClientPacket syncClientPacket = new SyncClientPacket(data);
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				uint uInt32 = 0;
				PlayerModel player = roomModel_0.GetPlayer((int)actionModel_0.Slot, false);
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.ActionState))
				{
					uInt32 += 256;
					ActionFlag actionFlag = (ActionFlag)syncClientPacket.ReadUH();
					byte num = syncClientPacket.ReadC();
					WeaponSyncType weaponSyncType = (WeaponSyncType)syncClientPacket.ReadC();
					syncServerPacket.WriteH((ushort)actionFlag);
					syncServerPacket.WriteC(num);
					syncServerPacket.WriteC((byte)weaponSyncType);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.Animation))
				{
					uInt32 += 2;
					syncServerPacket.WriteH(syncClientPacket.ReadUH());
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.PosRotation))
				{
					uInt32 += 134217728;
					ushort uInt16 = syncClientPacket.ReadUH();
					ushort uInt161 = syncClientPacket.ReadUH();
					ushort uInt162 = syncClientPacket.ReadUH();
					ushort uInt163 = syncClientPacket.ReadUH();
					ushort uInt164 = syncClientPacket.ReadUH();
					ushort uInt165 = syncClientPacket.ReadUH();
					if (player != null)
					{
						player.Position = new Half3(uInt164, uInt165, uInt163);
					}
					syncServerPacket.WriteH(uInt16);
					syncServerPacket.WriteH(uInt161);
					syncServerPacket.WriteH(uInt162);
					syncServerPacket.WriteH(uInt163);
					syncServerPacket.WriteH(uInt164);
					syncServerPacket.WriteH(uInt165);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.SoundPosRotation))
				{
					uInt32 += 8388608;
					syncServerPacket.WriteT(syncClientPacket.ReadT());
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.UseObject))
				{
					uInt32 += 4;
					byte num1 = syncClientPacket.ReadC();
					syncServerPacket.WriteC(num1);
					for (int i = 0; i < num1; i++)
					{
						ushort uInt166 = syncClientPacket.ReadUH();
						byte num2 = syncClientPacket.ReadC();
						CharaMoves charaMove = (CharaMoves)syncClientPacket.ReadC();
						syncServerPacket.WriteH(uInt166);
						syncServerPacket.WriteC(num2);
						syncServerPacket.WriteC((byte)charaMove);
					}
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.ActionForObjectSync))
				{
					uInt32 += 16;
					byte num3 = syncClientPacket.ReadC();
					byte num4 = syncClientPacket.ReadC();
					if (player != null)
					{
						roomModel_0.SyncInfo(objectHitInfos1, 1);
					}
					syncServerPacket.WriteC(num3);
					syncServerPacket.WriteC(num4);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.RadioChat))
				{
					uInt32 += 32;
					byte num5 = syncClientPacket.ReadC();
					byte num6 = syncClientPacket.ReadC();
					syncServerPacket.WriteC(num5);
					syncServerPacket.WriteC(num6);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.WeaponSync))
				{
					uInt32 += 67108864;
					int ınt32 = syncClientPacket.ReadD();
					byte num7 = syncClientPacket.ReadC();
					byte num8 = syncClientPacket.ReadC();
					if (player != null)
					{
						player.WeaponId = ınt32;
						player.Accessory = num7;
						player.Extensions = num8;
						player.WeaponClass = (ClassType)ComDiv.GetIdStatics(ınt32, 2);
						roomModel_0.SyncInfo(objectHitInfos1, 2);
					}
					syncServerPacket.WriteD(ınt32);
					syncServerPacket.WriteC(num7);
					syncServerPacket.WriteC(num8);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.WeaponRecoil))
				{
					uInt32 += 128;
					float single = syncClientPacket.ReadT();
					float single1 = syncClientPacket.ReadT();
					float single2 = syncClientPacket.ReadT();
					float single3 = syncClientPacket.ReadT();
					float single4 = syncClientPacket.ReadT();
					byte num9 = syncClientPacket.ReadC();
					int ınt321 = syncClientPacket.ReadD();
					byte num10 = syncClientPacket.ReadC();
					byte num11 = syncClientPacket.ReadC();
					CLogger.Print(string.Format("PVE (WeaponRecoil); Slot: {0}; Weapon Id: {1}; Extensions: {2}; Unknowns: {3}", new object[] { player.Slot, ınt321, num11, num10 }), LoggerType.Warning, null);
					syncServerPacket.WriteT(single);
					syncServerPacket.WriteT(single1);
					syncServerPacket.WriteT(single2);
					syncServerPacket.WriteT(single3);
					syncServerPacket.WriteT(single4);
					syncServerPacket.WriteC(num9);
					syncServerPacket.WriteD(ınt321);
					syncServerPacket.WriteC(num10);
					syncServerPacket.WriteC(num11);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.HpSync))
				{
					uInt32 += 8;
					syncServerPacket.WriteH(syncClientPacket.ReadUH());
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.Suicide))
				{
					uInt32 += 1048576;
					byte num12 = syncClientPacket.ReadC();
					syncServerPacket.WriteC(num12);
					for (int j = 0; j < num12; j++)
					{
						Half3 half3 = syncClientPacket.ReadUHV();
						int ınt322 = syncClientPacket.ReadD();
						byte num13 = syncClientPacket.ReadC();
						byte num14 = syncClientPacket.ReadC();
						uint uInt321 = syncClientPacket.ReadUD();
						syncServerPacket.WriteHV(half3);
						syncServerPacket.WriteD(ınt322);
						syncServerPacket.WriteC(num13);
						syncServerPacket.WriteC(num14);
						syncServerPacket.WriteD(uInt321);
					}
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.DropWeapon))
				{
					uInt32 += 4194304;
					ushort uInt167 = syncClientPacket.ReadUH();
					ushort uInt168 = syncClientPacket.ReadUH();
					ushort uInt169 = syncClientPacket.ReadUH();
					ushort uInt1610 = syncClientPacket.ReadUH();
					ushort uInt1611 = syncClientPacket.ReadUH();
					ushort uInt1612 = syncClientPacket.ReadUH();
					byte num15 = syncClientPacket.ReadC();
					int ınt323 = syncClientPacket.ReadD();
					byte num16 = syncClientPacket.ReadC();
					byte num17 = syncClientPacket.ReadC();
					if (!ConfigLoader.UseMaxAmmoInDrop)
					{
						syncServerPacket.WriteH(uInt167);
						syncServerPacket.WriteH(uInt168);
						syncServerPacket.WriteH(uInt169);
					}
					else
					{
						syncServerPacket.WriteH(65535);
						syncServerPacket.WriteH(uInt168);
						syncServerPacket.WriteH(10000);
					}
					syncServerPacket.WriteH(uInt1610);
					syncServerPacket.WriteH(uInt1611);
					syncServerPacket.WriteH(uInt1612);
					syncServerPacket.WriteC(num15);
					syncServerPacket.WriteD(ınt323);
					syncServerPacket.WriteC(num16);
					syncServerPacket.WriteC(num17);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.GetWeaponForClient))
				{
					uInt32 += 16777216;
					ushort uInt1613 = syncClientPacket.ReadUH();
					ushort uInt1614 = syncClientPacket.ReadUH();
					ushort uInt1615 = syncClientPacket.ReadUH();
					ushort uInt1616 = syncClientPacket.ReadUH();
					ushort uInt1617 = syncClientPacket.ReadUH();
					ushort uInt1618 = syncClientPacket.ReadUH();
					byte num18 = syncClientPacket.ReadC();
					int ınt324 = syncClientPacket.ReadD();
					byte num19 = syncClientPacket.ReadC();
					byte num20 = syncClientPacket.ReadC();
					if (!ConfigLoader.UseMaxAmmoInDrop)
					{
						syncServerPacket.WriteH(uInt1613);
						syncServerPacket.WriteH(uInt1614);
						syncServerPacket.WriteH(uInt1615);
					}
					else
					{
						syncServerPacket.WriteH(65535);
						syncServerPacket.WriteH(uInt1614);
						syncServerPacket.WriteH(10000);
					}
					syncServerPacket.WriteH(uInt1616);
					syncServerPacket.WriteH(uInt1617);
					syncServerPacket.WriteH(uInt1618);
					syncServerPacket.WriteC(num18);
					syncServerPacket.WriteD(ınt324);
					syncServerPacket.WriteC(num19);
					syncServerPacket.WriteC(num20);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.FireData))
				{
					uInt32 += 33554432;
					byte num21 = syncClientPacket.ReadC();
					byte num22 = syncClientPacket.ReadC();
					short ınt16 = syncClientPacket.ReadH();
					int ınt325 = syncClientPacket.ReadD();
					byte num23 = syncClientPacket.ReadC();
					byte num24 = syncClientPacket.ReadC();
					ushort uInt1619 = syncClientPacket.ReadUH();
					ushort uInt1620 = syncClientPacket.ReadUH();
					ushort uInt1621 = syncClientPacket.ReadUH();
					syncServerPacket.WriteC(num21);
					syncServerPacket.WriteC(num22);
					syncServerPacket.WriteH(ınt16);
					syncServerPacket.WriteD(ınt325);
					syncServerPacket.WriteC(num23);
					syncServerPacket.WriteC(num24);
					syncServerPacket.WriteH(uInt1619);
					syncServerPacket.WriteH(uInt1620);
					syncServerPacket.WriteH(uInt1621);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.CharaFireNHitData))
				{
					uInt32 += 1024;
					byte num25 = syncClientPacket.ReadC();
					syncServerPacket.WriteC(num25);
					for (int k = 0; k < num25; k++)
					{
						int ınt326 = syncClientPacket.ReadD();
						byte num26 = syncClientPacket.ReadC();
						byte num27 = syncClientPacket.ReadC();
						ushort uInt1622 = syncClientPacket.ReadUH();
						uint uInt322 = syncClientPacket.ReadUD();
						ushort uInt1623 = syncClientPacket.ReadUH();
						ushort uInt1624 = syncClientPacket.ReadUH();
						ushort uInt1625 = syncClientPacket.ReadUH();
						syncServerPacket.WriteD(ınt326);
						syncServerPacket.WriteC(num26);
						syncServerPacket.WriteC(num27);
						syncServerPacket.WriteH(uInt1622);
						syncServerPacket.WriteD(uInt322);
						syncServerPacket.WriteH(uInt1623);
						syncServerPacket.WriteH(uInt1624);
						syncServerPacket.WriteH(uInt1625);
					}
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.GetWeaponForHost))
				{
					uInt32 += 512;
					CharaDeath charaDeath = (CharaDeath)syncClientPacket.ReadC();
					byte num28 = syncClientPacket.ReadC();
					ushort uInt1626 = syncClientPacket.ReadUH();
					ushort uInt1627 = syncClientPacket.ReadUH();
					ushort uInt1628 = syncClientPacket.ReadUH();
					int ınt327 = syncClientPacket.ReadD();
					CharaHitPart charaHitPart = (CharaHitPart)syncClientPacket.ReadC();
					syncServerPacket.WriteC((byte)charaDeath);
					syncServerPacket.WriteC(num28);
					syncServerPacket.WriteH(uInt1626);
					syncServerPacket.WriteH(uInt1627);
					syncServerPacket.WriteH(uInt1628);
					syncServerPacket.WriteD(ınt327);
					syncServerPacket.WriteC((byte)charaHitPart);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.FireDataOnObject))
				{
					uInt32 += 1073741824;
					byte num29 = syncClientPacket.ReadC();
					CharaHitPart charaHitPart1 = (CharaHitPart)syncClientPacket.ReadC();
					byte num30 = syncClientPacket.ReadC();
					syncServerPacket.WriteC(num29);
					syncServerPacket.WriteC((byte)charaHitPart1);
					syncServerPacket.WriteC(num30);
				}
				byte_0 = syncServerPacket.ToArray();
				if (uInt32 != (uint)actionModel_0.Flag)
				{
					CLogger.Print(Bitwise.ToHexData(string.Format("PVE - Missing Flag Events: '{0}' | '{1}'", (uint)actionModel_0.Flag, (uint)((uint)actionModel_0.Flag - uInt32)), data), LoggerType.Opcode, null);
				}
				objectHitInfos = objectHitInfos1;
			}
			return objectHitInfos;
		}

		private void method_2(IAsyncResult iasyncResult_0)
		{
			try
			{
				Socket asyncState = iasyncResult_0.AsyncState as Socket;
				if (asyncState != null && asyncState.Connected)
				{
					asyncState.EndSend(iasyncResult_0);
				}
			}
			catch (SocketException socketException1)
			{
				SocketException socketException = socketException1;
				CLogger.Print(string.Format("Socket Error on Send: {0}", socketException.SocketErrorCode), LoggerType.Warning, null);
			}
			catch (ObjectDisposedException objectDisposedException)
			{
				CLogger.Print("Socket was closed while sending.", LoggerType.Warning, null);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("Error during EndSendCallback: ", exception.Message), LoggerType.Error, exception);
			}
		}

		public void ReadPacket(PacketModel Packet)
		{
			PlayerModel[] players;
			int i;
			int ınt32;
			DateTime dateTime;
			byte[] withEndData = Packet.WithEndData;
			byte[] withoutEndData = Packet.WithoutEndData;
			SyncClientPacket syncClientPacket = new SyncClientPacket(withEndData);
			int length = (int)withoutEndData.Length;
			try
			{
				uint uInt32 = 0;
				int ınt321 = 0;
				RoomModel room = null;
				int opcode = Packet.Opcode;
				if (opcode <= 65)
				{
					if (opcode == 3)
					{
						syncClientPacket.Advance(length);
						uint uInt321 = syncClientPacket.ReadUD();
						ınt321 = syncClientPacket.ReadC();
						room = RoomsManager.GetRoom(uInt321, syncClientPacket.ReadUD());
						if (room != null)
						{
							PlayerModel player = room.GetPlayer(Packet.Slot, this.ipendPoint_0);
							if (player != null && player.PlayerIdByServer == Packet.AccountId)
							{
								player.RespawnByUser = Packet.Respawn;
								DateTime startTime = room.StartTime;
								dateTime = new DateTime();
								if (startTime != dateTime)
								{
									byte[] numArray = AllUtils.BaseWriteCode(4, (room.BotMode ? this.WriteBotActionData(withoutEndData, room) : this.WritePlayerActionData(withoutEndData, room, AllUtils.GetDuration(player.StartTime), Packet)), (room.BotMode ? Packet.Slot : 255), AllUtils.GetDuration(room.StartTime), Packet.Round, Packet.Respawn, Packet.RoundNumber, Packet.AccountId);
									bool flag = (room.BotMode ? false : ınt321 != 255);
									players = room.Players;
									for (i = 0; i < (int)players.Length; i++)
									{
										PlayerModel playerModel = players[i];
										bool slot = playerModel.Slot != Packet.Slot;
										if (playerModel.Client != null && player.AccountIdIsValid())
										{
											if ((ınt321 == 255 & slot ? true : room.BotMode & slot) | flag)
											{
												this.SendPacket(numArray, playerModel.Client);
											}
										}
									}
								}
								else
								{
									return;
								}
							}
						}
					}
					else if (opcode == 4)
					{
						syncClientPacket.Advance(length);
						uint uInt322 = syncClientPacket.ReadUD();
						ınt321 = syncClientPacket.ReadC();
						room = RoomsManager.GetRoom(uInt322, syncClientPacket.ReadUD());
						if (room != null)
						{
							PlayerModel respawn = room.GetPlayer(Packet.Slot, this.ipendPoint_0);
							if (respawn != null && respawn.PlayerIdByServer == Packet.AccountId)
							{
								respawn.RespawnByUser = Packet.Respawn;
								room.BotMode = true;
								DateTime startTime1 = room.StartTime;
								dateTime = new DateTime();
								if (startTime1 != dateTime)
								{
									byte[] numArray1 = this.WriteBotActionData(withoutEndData, room);
									byte[] numArray2 = AllUtils.BaseWriteCode(4, numArray1, Packet.Slot, AllUtils.GetDuration(respawn.StartTime), Packet.Round, Packet.Respawn, Packet.RoundNumber, Packet.AccountId);
									players = room.Players;
									for (i = 0; i < (int)players.Length; i++)
									{
										PlayerModel playerModel1 = players[i];
										bool slot1 = playerModel1.Slot != Packet.Slot;
										if (playerModel1.Client != null && respawn.AccountIdIsValid() && ınt321 == 255 & slot1)
										{
											this.SendPacket(numArray2, playerModel1.Client);
										}
									}
								}
								else
								{
									return;
								}
							}
						}
					}
					else
					{
						if (opcode != 65)
						{
							CLogger.Print(Bitwise.ToHexData(string.Format("Opcode Not Found: [{0}]", Packet.Opcode), withEndData), LoggerType.Opcode, null);
							return;
						}
						string str = string.Format("{0}.{1}", syncClientPacket.ReadH(), syncClientPacket.ReadH());
						uint uInt323 = syncClientPacket.ReadUD();
						uInt32 = syncClientPacket.ReadUD();
						ınt321 = syncClientPacket.ReadC();
						room = RoomsManager.CreateOrGetRoom(uInt323, uInt32);
						if (room != null)
						{
							PlayerModel playerModel2 = room.AddPlayer(this.ipendPoint_0, Packet, str);
							if (playerModel2 != null)
							{
								if (!playerModel2.Integrity)
								{
									playerModel2.ResetBattleInfos();
								}
								this.SendPacket(PROTOCOL_CONNECT.GET_CODE(), playerModel2.Client);
								if (ConfigLoader.IsTestMode)
								{
									CLogger.Print(string.Format("Player Connected. [{0}:{1}]", playerModel2.Client.Address, playerModel2.Client.Port), LoggerType.Warning, null);
								}
							}
						}
					}
				}
				else if (opcode <= 97)
				{
					if (opcode == 67)
					{
						string str1 = string.Format("{0}.{1}", syncClientPacket.ReadH(), syncClientPacket.ReadH());
						uint uInt324 = syncClientPacket.ReadUD();
						uInt32 = syncClientPacket.ReadUD();
						ınt321 = syncClientPacket.ReadC();
						room = RoomsManager.GetRoom(uInt324, uInt32);
						if (room != null)
						{
							if (room.RemovePlayer(this.ipendPoint_0, Packet, str1) && ConfigLoader.IsTestMode)
							{
								CLogger.Print(string.Format("Player Disconnected. [{0}:{1}]", this.ipendPoint_0.Address, this.ipendPoint_0.Port), LoggerType.Warning, null);
							}
							if (room.GetPlayersCount() == 0)
							{
								RoomsManager.RemoveRoom(room.UniqueRoomId, room.RoomSeed);
							}
						}
					}
					else
					{
						if (opcode != 97)
						{
							CLogger.Print(Bitwise.ToHexData(string.Format("Opcode Not Found: [{0}]", Packet.Opcode), withEndData), LoggerType.Opcode, null);
							return;
						}
						uint uInt325 = syncClientPacket.ReadUD();
						ınt321 = syncClientPacket.ReadC();
						room = RoomsManager.GetRoom(uInt325, syncClientPacket.ReadUD());
						byte[] data = Packet.Data;
						if (room != null)
						{
							PlayerModel receiveDate = room.GetPlayer(Packet.Slot, this.ipendPoint_0);
							if (receiveDate != null)
							{
								receiveDate.LastPing = Packet.ReceiveDate;
								this.SendPacket(data, this.ipendPoint_0);
								if (ConfigLoader.SendPingSync)
								{
									receiveDate.Latency = AllUtils.PingTime(string.Format("{0}", this.ipendPoint_0.Address), data, this.socket_0.Ttl, 120, false, out ınt32);
									receiveDate.Ping = ınt32;
									SendMatchInfo.SendPingSync(room, receiveDate);
								}
							}
						}
					}
				}
				else if (opcode == 131)
				{
					syncClientPacket.Advance(length);
					uint uInt326 = syncClientPacket.ReadUD();
					ınt321 = syncClientPacket.ReadC();
					room = RoomsManager.GetRoom(uInt326, syncClientPacket.ReadUD());
					if (room != null)
					{
						PlayerModel player1 = room.GetPlayer(Packet.Slot, this.ipendPoint_0);
						if (player1 != null && player1.PlayerIdByServer == Packet.AccountId)
						{
							room.BotMode = true;
							PlayerModel player2 = room.GetPlayer(ınt321, false);
							byte[] cODE = PROTOCOL_BOTS_ACTION.GET_CODE(withoutEndData);
							byte[] numArray3 = AllUtils.BaseWriteCode(132, cODE, ınt321, AllUtils.GetDuration(player2.StartTime), Packet.Round, Packet.Respawn, Packet.RoundNumber, Packet.AccountId);
							players = room.Players;
							for (i = 0; i < (int)players.Length; i++)
							{
								PlayerModel playerModel3 = players[i];
								if (playerModel3.Client != null && player1.AccountIdIsValid() && playerModel3.Slot != Packet.Slot)
								{
									this.SendPacket(numArray3, playerModel3.Client);
								}
							}
						}
					}
				}
				else
				{
					if (opcode != 132)
					{
						CLogger.Print(Bitwise.ToHexData(string.Format("Opcode Not Found: [{0}]", Packet.Opcode), withEndData), LoggerType.Opcode, null);
						return;
					}
					syncClientPacket.Advance(length);
					uint uInt327 = syncClientPacket.ReadUD();
					ınt321 = syncClientPacket.ReadC();
					room = RoomsManager.GetRoom(uInt327, syncClientPacket.ReadUD());
					if (room != null)
					{
						PlayerModel player3 = room.GetPlayer(Packet.Slot, this.ipendPoint_0);
						if (player3 != null && player3.PlayerIdByServer == Packet.AccountId)
						{
							room.BotMode = true;
							byte[] cODE1 = PROTOCOL_BOTS_ACTION.GET_CODE(withoutEndData);
							byte[] numArray4 = AllUtils.BaseWriteCode(132, cODE1, Packet.Slot, AllUtils.GetDuration(player3.StartTime), Packet.Round, Packet.Respawn, Packet.RoundNumber, Packet.AccountId);
							players = room.Players;
							for (i = 0; i < (int)players.Length; i++)
							{
								PlayerModel playerModel4 = players[i];
								if (playerModel4.Client != null && player3.AccountIdIsValid() && playerModel4.Slot != Packet.Slot)
								{
									this.SendPacket(numArray4, playerModel4.Client);
								}
							}
						}
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}

		public void SendPacket(byte[] Data, IPEndPoint Address)
		{
			try
			{
				this.socket_0.BeginSendTo(Data, 0, (int)Data.Length, SocketFlags.None, Address, new AsyncCallback(this.method_2), this.socket_0);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Format("Failed to send package to {0}: {1}", Address, exception.Message), LoggerType.Error, exception);
			}
		}

		public byte[] WriteBotActionData(byte[] Data, RoomModel Room)
		{
			bool flag;
			byte[] numArray;
			byte[] array;
			SyncClientPacket syncClientPacket = new SyncClientPacket(Data);
			List<ObjectHitInfo> objectHitInfos = new List<ObjectHitInfo>();
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				for (int i = 0; i < 18; i++)
				{
					ActionModel actionModel = new ActionModel();
					try
					{
						bool flag1 = false;
						actionModel.Length = syncClientPacket.ReadUH(out flag);
						if (!flag)
						{
							actionModel.Slot = syncClientPacket.ReadUH();
							actionModel.SubHead = (UdpSubHead)syncClientPacket.ReadC();
							if (actionModel.SubHead != (UdpSubHead.Grenade | UdpSubHead.DroppedWeapon | UdpSubHead.ObjectStatic | UdpSubHead.ObjectMove | UdpSubHead.ObjectDynamic | UdpSubHead.ObjectAnim | UdpSubHead.NonPlayerCharacter | UdpSubHead.StageInfoChara | UdpSubHead.StageInfoObjectStatic | UdpSubHead.StageInfoObjectMove | UdpSubHead.StageInfoObjectDyamic | UdpSubHead.StageInfoObjectAnim | UdpSubHead.StageInfoObjectControl | UdpSubHead.StageInfoMission | UdpSubHead.ArtificialIntelligence | UdpSubHead.DomiSkillObject | UdpSubHead.DomiEvent | UdpSubHead.Sentrygun))
							{
								syncServerPacket.WriteH(actionModel.Length);
								syncServerPacket.WriteH(actionModel.Slot);
								syncServerPacket.WriteC((byte)actionModel.SubHead);
								switch (actionModel.SubHead)
								{
									case UdpSubHead.User:
									case UdpSubHead.StageInfoChara:
									{
										actionModel.Flag = (UdpGameEvent)syncClientPacket.ReadUD();
										actionModel.Data = syncClientPacket.ReadB(actionModel.Length - 9);
										objectHitInfos.AddRange(this.method_1(actionModel, Room, out numArray));
										syncServerPacket.GoBack(5);
										syncServerPacket.WriteH((ushort)((int)numArray.Length + 9));
										syncServerPacket.WriteH(actionModel.Slot);
										syncServerPacket.WriteC((byte)actionModel.SubHead);
										syncServerPacket.WriteD((uint)actionModel.Flag);
										syncServerPacket.WriteB(numArray);
										if (actionModel.Data.Length != 0 || actionModel.Length - 9 == 0)
										{
											break;
										}
										flag1 = true;
										break;
									}
									case UdpSubHead.Grenade:
									{
										byte num = syncClientPacket.ReadC();
										byte num1 = syncClientPacket.ReadC();
										byte num2 = syncClientPacket.ReadC();
										byte num3 = syncClientPacket.ReadC();
										ushort uInt16 = syncClientPacket.ReadUH();
										int ınt32 = syncClientPacket.ReadD();
										byte num4 = syncClientPacket.ReadC();
										byte num5 = syncClientPacket.ReadC();
										ushort uInt161 = syncClientPacket.ReadUH();
										ushort uInt162 = syncClientPacket.ReadUH();
										ushort uInt163 = syncClientPacket.ReadUH();
										int ınt321 = syncClientPacket.ReadD();
										int ınt322 = syncClientPacket.ReadD();
										int ınt323 = syncClientPacket.ReadD();
										syncServerPacket.WriteC(num);
										syncServerPacket.WriteC(num1);
										syncServerPacket.WriteC(num2);
										syncServerPacket.WriteC(num3);
										syncServerPacket.WriteH(uInt16);
										syncServerPacket.WriteD(ınt32);
										syncServerPacket.WriteC(num4);
										syncServerPacket.WriteC(num5);
										syncServerPacket.WriteH(uInt161);
										syncServerPacket.WriteH(uInt162);
										syncServerPacket.WriteH(uInt163);
										syncServerPacket.WriteD(ınt321);
										syncServerPacket.WriteD(ınt322);
										syncServerPacket.WriteD(ınt323);
										break;
									}
									case UdpSubHead.DroppedWeapon:
									{
										syncServerPacket.WriteB(syncClientPacket.ReadB(31));
										break;
									}
									case UdpSubHead.ObjectStatic:
									{
										syncServerPacket.WriteB(syncClientPacket.ReadB(10));
										break;
									}
									case UdpSubHead.ObjectMove:
									{
										syncServerPacket.WriteB(syncClientPacket.ReadB(16));
										break;
									}
									case UdpSubHead.ObjectDynamic:
									case UdpSubHead.NonPlayerCharacter:
									case UdpSubHead.StageInfoObjectMove:
									case UdpSubHead.StageInfoObjectDyamic:
									{
										if (!ConfigLoader.IsTestMode)
										{
											break;
										}
										CLogger.Print(Bitwise.ToHexData(string.Format("PVP Sub Head: '{0}' or '{1}'", actionModel.SubHead, (int)actionModel.SubHead), Data), LoggerType.Opcode, null);
										break;
									}
									case UdpSubHead.ObjectAnim:
									{
										syncServerPacket.WriteB(syncClientPacket.ReadB(8));
										break;
									}
									case UdpSubHead.StageInfoObjectStatic:
									{
										syncServerPacket.WriteB(syncClientPacket.ReadB(1));
										break;
									}
									case UdpSubHead.StageInfoObjectAnim:
									{
										syncServerPacket.WriteB(syncClientPacket.ReadB(9));
										break;
									}
									case UdpSubHead.StageInfoObjectControl:
									{
										syncServerPacket.WriteB(syncClientPacket.ReadB(9));
										break;
									}
									default:
									{
										goto case UdpSubHead.StageInfoObjectDyamic;
									}
								}
								if (flag1)
								{
									break;
								}
							}
							else
							{
								break;
							}
						}
						else
						{
							break;
						}
					}
					catch (Exception exception1)
					{
						Exception exception = exception1;
						if (ConfigLoader.IsTestMode)
						{
							CLogger.Print(string.Format("PVE Action Data - Buffer (Length: {0}): | {1}", (int)Data.Length, exception.Message), LoggerType.Error, exception);
						}
						objectHitInfos = new List<ObjectHitInfo>();
						break;
					}
				}
				if (objectHitInfos.Count > 0)
				{
					syncServerPacket.WriteB(PROTOCOL_EVENTS_ACTION.GET_CODE(objectHitInfos));
				}
				objectHitInfos = null;
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		public byte[] WritePlayerActionData(byte[] Data, RoomModel Room, float Time, PacketModel Packet)
		{
			bool flag;
			byte[] numArray;
			byte[] array;
			SyncClientPacket syncClientPacket = new SyncClientPacket(Data);
			List<ObjectHitInfo> objectHitInfos = new List<ObjectHitInfo>();
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				for (int i = 0; i < 18; i++)
				{
					ActionModel actionModel = new ActionModel();
					try
					{
						bool flag1 = false;
						actionModel.Length = syncClientPacket.ReadUH(out flag);
						if (!flag)
						{
							actionModel.Slot = syncClientPacket.ReadUH();
							actionModel.SubHead = (UdpSubHead)syncClientPacket.ReadC();
							if (actionModel.SubHead != (UdpSubHead.Grenade | UdpSubHead.DroppedWeapon | UdpSubHead.ObjectStatic | UdpSubHead.ObjectMove | UdpSubHead.ObjectDynamic | UdpSubHead.ObjectAnim | UdpSubHead.NonPlayerCharacter | UdpSubHead.StageInfoChara | UdpSubHead.StageInfoObjectStatic | UdpSubHead.StageInfoObjectMove | UdpSubHead.StageInfoObjectDyamic | UdpSubHead.StageInfoObjectAnim | UdpSubHead.StageInfoObjectControl | UdpSubHead.StageInfoMission | UdpSubHead.ArtificialIntelligence | UdpSubHead.DomiSkillObject | UdpSubHead.DomiEvent | UdpSubHead.Sentrygun))
							{
								syncServerPacket.WriteH(actionModel.Length);
								syncServerPacket.WriteH(actionModel.Slot);
								syncServerPacket.WriteC((byte)actionModel.SubHead);
								switch (actionModel.SubHead)
								{
									case UdpSubHead.User:
									case UdpSubHead.StageInfoChara:
									{
										actionModel.Flag = (UdpGameEvent)syncClientPacket.ReadUD();
										actionModel.Data = syncClientPacket.ReadB(actionModel.Length - 9);
										AllUtils.CheckDataFlags(actionModel, Packet);
										objectHitInfos.AddRange(this.method_0(actionModel, Room, Time, out numArray));
										syncServerPacket.GoBack(5);
										syncServerPacket.WriteH((ushort)((int)numArray.Length + 9));
										syncServerPacket.WriteH(actionModel.Slot);
										syncServerPacket.WriteC((byte)actionModel.SubHead);
										syncServerPacket.WriteD((uint)actionModel.Flag);
										syncServerPacket.WriteB(numArray);
										if (actionModel.Data.Length != 0 || actionModel.Length - 9 == 0)
										{
											break;
										}
										flag1 = true;
										break;
									}
									case UdpSubHead.Grenade:
									{
										GrenadeSync.WriteInfo(syncServerPacket, GrenadeSync.ReadInfo(syncClientPacket, false, false));
										break;
									}
									case UdpSubHead.DroppedWeapon:
									{
										DropedWeapon.WriteInfo(syncServerPacket, DropedWeapon.ReadInfo(syncClientPacket, false));
										break;
									}
									case UdpSubHead.ObjectStatic:
									{
										ObjectStatic.WriteInfo(syncServerPacket, ObjectStatic.ReadInfo(syncClientPacket, false));
										break;
									}
									case UdpSubHead.ObjectMove:
									{
										ObjectMove.WriteInfo(syncServerPacket, ObjectMove.ReadInfo(syncClientPacket, false));
										break;
									}
									case UdpSubHead.ObjectDynamic:
									case UdpSubHead.NonPlayerCharacter:
									case UdpSubHead.StageInfoObjectMove:
									case UdpSubHead.StageInfoObjectDyamic:
									{
										if (!ConfigLoader.IsTestMode)
										{
											break;
										}
										CLogger.Print(Bitwise.ToHexData(string.Format("PVP Sub Head: '{0}' or '{1}'", actionModel.SubHead, (int)actionModel.SubHead), Data), LoggerType.Opcode, null);
										break;
									}
									case UdpSubHead.ObjectAnim:
									{
										ObjectAnim.WriteInfo(syncServerPacket, ObjectAnim.ReadInfo(syncClientPacket, false));
										break;
									}
									case UdpSubHead.StageInfoObjectStatic:
									{
										StageInfoObjStatic.WriteInfo(syncServerPacket, StageInfoObjStatic.ReadInfo(syncClientPacket, false));
										break;
									}
									case UdpSubHead.StageInfoObjectAnim:
									{
										StageInfoObjAnim.WriteInfo(syncServerPacket, StageInfoObjAnim.ReadInfo(syncClientPacket, false));
										break;
									}
									case UdpSubHead.StageInfoObjectControl:
									{
										StageInfoObjControl.WriteInfo(syncServerPacket, StageInfoObjControl.ReadInfo(syncClientPacket, false));
										break;
									}
									default:
									{
										goto case UdpSubHead.StageInfoObjectDyamic;
									}
								}
								if (flag1)
								{
									break;
								}
							}
							else
							{
								break;
							}
						}
						else
						{
							break;
						}
					}
					catch (Exception exception1)
					{
						Exception exception = exception1;
						if (ConfigLoader.IsTestMode)
						{
							CLogger.Print(string.Format("PVP Action Data - Buffer (Length: {0}): | {1}", (int)Data.Length, exception.Message), LoggerType.Error, exception);
						}
						objectHitInfos = new List<ObjectHitInfo>();
						break;
					}
				}
				if (objectHitInfos.Count > 0)
				{
					syncServerPacket.WriteB(PROTOCOL_EVENTS_ACTION.GET_CODE(objectHitInfos));
				}
				objectHitInfos = null;
				array = syncServerPacket.ToArray();
			}
			return array;
		}
	}
}