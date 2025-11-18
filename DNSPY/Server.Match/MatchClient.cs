using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
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

namespace Server.Match
{
	// Token: 0x02000002 RID: 2
	public class MatchClient
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002050 File Offset: 0x00000250
		public MatchClient(Socket socket_1, IPEndPoint ipendPoint_1)
		{
			this.socket_0 = socket_1;
			this.ipendPoint_0 = ipendPoint_1;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002B14 File Offset: 0x00000D14
		public void BeginReceive(byte[] Buffer, DateTime Date)
		{
			PacketModel packetModel = new PacketModel
			{
				Data = Buffer,
				ReceiveDate = Date
			};
			SyncClientPacket syncClientPacket = new SyncClientPacket(packetModel.Data);
			packetModel.Opcode = (int)syncClientPacket.ReadC();
			packetModel.Slot = (int)syncClientPacket.ReadC();
			packetModel.Time = syncClientPacket.ReadT();
			packetModel.Round = (int)syncClientPacket.ReadC();
			packetModel.Length = (int)syncClientPacket.ReadUH();
			packetModel.Respawn = (int)syncClientPacket.ReadC();
			packetModel.RoundNumber = (int)syncClientPacket.ReadC();
			packetModel.AccountId = (int)syncClientPacket.ReadC();
			packetModel.Unk1 = (int)syncClientPacket.ReadC();
			packetModel.Unk2 = syncClientPacket.ReadD();
			if (packetModel.Length > packetModel.Data.Length)
			{
				CLogger.Print(string.Format("Packet with invalid size canceled. [ Length: {0} DataLength: {1} ]", packetModel.Length, packetModel.Data.Length), LoggerType.Warning, null);
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

		// Token: 0x06000008 RID: 8 RVA: 0x00002C90 File Offset: 0x00000E90
		public void ReadPacket(PacketModel Packet)
		{
			byte[] withEndData = Packet.WithEndData;
			byte[] withoutEndData = Packet.WithoutEndData;
			SyncClientPacket syncClientPacket = new SyncClientPacket(withEndData);
			int num = withoutEndData.Length;
			try
			{
				int opcode = Packet.Opcode;
				if (opcode <= 65)
				{
					if (opcode != 3)
					{
						if (opcode != 4)
						{
							if (opcode == 65)
							{
								string text = string.Format("{0}.{1}", syncClientPacket.ReadH(), syncClientPacket.ReadH());
								uint num2 = syncClientPacket.ReadUD();
								uint num3 = syncClientPacket.ReadUD();
								int num4 = (int)syncClientPacket.ReadC();
								RoomModel roomModel = RoomsManager.CreateOrGetRoom(num2, num3);
								if (roomModel == null)
								{
									goto IL_791;
								}
								PlayerModel playerModel = roomModel.AddPlayer(this.ipendPoint_0, Packet, text);
								if (playerModel == null)
								{
									goto IL_791;
								}
								if (!playerModel.Integrity)
								{
									playerModel.ResetBattleInfos();
								}
								byte[] code = PROTOCOL_CONNECT.GET_CODE();
								this.SendPacket(code, playerModel.Client);
								if (ConfigLoader.IsTestMode)
								{
									CLogger.Print(string.Format("Player Connected. [{0}:{1}]", playerModel.Client.Address, playerModel.Client.Port), LoggerType.Warning, null);
									goto IL_791;
								}
								goto IL_791;
							}
						}
						else
						{
							syncClientPacket.Advance(num);
							uint num5 = syncClientPacket.ReadUD();
							int num4 = (int)syncClientPacket.ReadC();
							uint num3 = syncClientPacket.ReadUD();
							RoomModel roomModel = RoomsManager.GetRoom(num5, num3);
							if (roomModel == null)
							{
								goto IL_791;
							}
							PlayerModel player = roomModel.GetPlayer(Packet.Slot, this.ipendPoint_0);
							if (player == null || player.PlayerIdByServer != Packet.AccountId)
							{
								goto IL_791;
							}
							player.RespawnByUser = Packet.Respawn;
							roomModel.BotMode = true;
							if (roomModel.StartTime == default(DateTime))
							{
								return;
							}
							byte[] array = this.WriteBotActionData(withoutEndData, roomModel);
							byte[] array2 = AllUtils.BaseWriteCode(4, array, Packet.Slot, AllUtils.GetDuration(player.StartTime), Packet.Round, Packet.Respawn, Packet.RoundNumber, Packet.AccountId);
							foreach (PlayerModel playerModel2 in roomModel.Players)
							{
								bool flag = playerModel2.Slot != Packet.Slot;
								if (playerModel2.Client != null && player.AccountIdIsValid() && (num4 == 255 && flag))
								{
									this.SendPacket(array2, playerModel2.Client);
								}
							}
							goto IL_791;
						}
					}
					else
					{
						syncClientPacket.Advance(num);
						uint num6 = syncClientPacket.ReadUD();
						int num4 = (int)syncClientPacket.ReadC();
						uint num3 = syncClientPacket.ReadUD();
						RoomModel roomModel = RoomsManager.GetRoom(num6, num3);
						if (roomModel == null)
						{
							goto IL_791;
						}
						PlayerModel player2 = roomModel.GetPlayer(Packet.Slot, this.ipendPoint_0);
						if (player2 == null || player2.PlayerIdByServer != Packet.AccountId)
						{
							goto IL_791;
						}
						player2.RespawnByUser = Packet.Respawn;
						if (roomModel.StartTime == default(DateTime))
						{
							return;
						}
						byte[] array4 = (roomModel.BotMode ? this.WriteBotActionData(withoutEndData, roomModel) : this.WritePlayerActionData(withoutEndData, roomModel, AllUtils.GetDuration(player2.StartTime), Packet));
						byte[] array5 = AllUtils.BaseWriteCode(4, array4, roomModel.BotMode ? Packet.Slot : 255, AllUtils.GetDuration(roomModel.StartTime), Packet.Round, Packet.Respawn, Packet.RoundNumber, Packet.AccountId);
						bool flag2 = !roomModel.BotMode && num4 != 255;
						foreach (PlayerModel playerModel3 in roomModel.Players)
						{
							bool flag3 = playerModel3.Slot != Packet.Slot;
							if (playerModel3.Client != null && player2.AccountIdIsValid() && ((num4 == 255 && flag3) || (roomModel.BotMode && flag3) || flag2))
							{
								this.SendPacket(array5, playerModel3.Client);
							}
						}
						goto IL_791;
					}
				}
				else if (opcode <= 97)
				{
					if (opcode != 67)
					{
						if (opcode == 97)
						{
							uint num7 = syncClientPacket.ReadUD();
							int num4 = (int)syncClientPacket.ReadC();
							uint num3 = syncClientPacket.ReadUD();
							RoomModel roomModel = RoomsManager.GetRoom(num7, num3);
							byte[] data = Packet.Data;
							if (roomModel == null)
							{
								goto IL_791;
							}
							PlayerModel player3 = roomModel.GetPlayer(Packet.Slot, this.ipendPoint_0);
							if (player3 == null)
							{
								goto IL_791;
							}
							player3.LastPing = Packet.ReceiveDate;
							this.SendPacket(data, this.ipendPoint_0);
							if (ConfigLoader.SendPingSync)
							{
								int num8;
								player3.Latency = AllUtils.PingTime(string.Format("{0}", this.ipendPoint_0.Address), data, (int)this.socket_0.Ttl, 120, false, out num8);
								player3.Ping = num8;
								SendMatchInfo.SendPingSync(roomModel, player3);
								goto IL_791;
							}
							goto IL_791;
						}
					}
					else
					{
						string text2 = string.Format("{0}.{1}", syncClientPacket.ReadH(), syncClientPacket.ReadH());
						uint num9 = syncClientPacket.ReadUD();
						uint num3 = syncClientPacket.ReadUD();
						int num4 = (int)syncClientPacket.ReadC();
						RoomModel roomModel = RoomsManager.GetRoom(num9, num3);
						if (roomModel == null)
						{
							goto IL_791;
						}
						if (roomModel.RemovePlayer(this.ipendPoint_0, Packet, text2) && ConfigLoader.IsTestMode)
						{
							CLogger.Print(string.Format("Player Disconnected. [{0}:{1}]", this.ipendPoint_0.Address, this.ipendPoint_0.Port), LoggerType.Warning, null);
						}
						if (roomModel.GetPlayersCount() == 0)
						{
							RoomsManager.RemoveRoom(roomModel.UniqueRoomId, roomModel.RoomSeed);
							goto IL_791;
						}
						goto IL_791;
					}
				}
				else if (opcode != 131)
				{
					if (opcode == 132)
					{
						syncClientPacket.Advance(num);
						uint num10 = syncClientPacket.ReadUD();
						int num4 = (int)syncClientPacket.ReadC();
						uint num3 = syncClientPacket.ReadUD();
						RoomModel roomModel = RoomsManager.GetRoom(num10, num3);
						if (roomModel == null)
						{
							goto IL_791;
						}
						PlayerModel player4 = roomModel.GetPlayer(Packet.Slot, this.ipendPoint_0);
						if (player4 != null && player4.PlayerIdByServer == Packet.AccountId)
						{
							roomModel.BotMode = true;
							byte[] array6 = PROTOCOL_BOTS_ACTION.GET_CODE(withoutEndData);
							byte[] array7 = AllUtils.BaseWriteCode(132, array6, Packet.Slot, AllUtils.GetDuration(player4.StartTime), Packet.Round, Packet.Respawn, Packet.RoundNumber, Packet.AccountId);
							foreach (PlayerModel playerModel4 in roomModel.Players)
							{
								if (playerModel4.Client != null && player4.AccountIdIsValid() && playerModel4.Slot != Packet.Slot)
								{
									this.SendPacket(array7, playerModel4.Client);
								}
							}
							goto IL_791;
						}
						goto IL_791;
					}
				}
				else
				{
					syncClientPacket.Advance(num);
					uint num11 = syncClientPacket.ReadUD();
					int num4 = (int)syncClientPacket.ReadC();
					uint num3 = syncClientPacket.ReadUD();
					RoomModel roomModel = RoomsManager.GetRoom(num11, num3);
					if (roomModel == null)
					{
						goto IL_791;
					}
					PlayerModel player5 = roomModel.GetPlayer(Packet.Slot, this.ipendPoint_0);
					if (player5 != null && player5.PlayerIdByServer == Packet.AccountId)
					{
						roomModel.BotMode = true;
						PlayerModel player6 = roomModel.GetPlayer(num4, false);
						byte[] array8 = PROTOCOL_BOTS_ACTION.GET_CODE(withoutEndData);
						byte[] array9 = AllUtils.BaseWriteCode(132, array8, num4, AllUtils.GetDuration(player6.StartTime), Packet.Round, Packet.Respawn, Packet.RoundNumber, Packet.AccountId);
						foreach (PlayerModel playerModel5 in roomModel.Players)
						{
							if (playerModel5.Client != null && player5.AccountIdIsValid() && playerModel5.Slot != Packet.Slot)
							{
								this.SendPacket(array9, playerModel5.Client);
							}
						}
						goto IL_791;
					}
					goto IL_791;
				}
				CLogger.Print(Bitwise.ToHexData(string.Format("Opcode Not Found: [{0}]", Packet.Opcode), withEndData), LoggerType.Opcode, null);
				IL_791:;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00003460 File Offset: 0x00001660
		private List<ObjectHitInfo> method_0(ActionModel actionModel_0, RoomModel roomModel_0, float float_0, out byte[] byte_0)
		{
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
			List<ObjectHitInfo> list = new List<ObjectHitInfo>();
			SyncClientPacket syncClientPacket = new SyncClientPacket(data);
			List<ObjectHitInfo> list10;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				uint num = 0U;
				PlayerModel player = roomModel_0.GetPlayer((int)actionModel_0.Slot, false);
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.ActionState))
				{
					num += 256U;
					ActionStateInfo actionStateInfo = ActionState.ReadInfo(actionModel_0, syncClientPacket, false);
					if (!roomModel_0.BotMode)
					{
						Equipment equip = player.Equip;
						if (player != null && equip != null)
						{
							int num2 = 0;
							byte b = 0;
							byte b2 = 0;
							if (actionStateInfo.Flag.HasFlag(WeaponSyncType.Primary))
							{
								num2 = equip.WpnPrimary;
								int idStatics = ComDiv.GetIdStatics(equip.Accessory, 1);
								int idStatics2 = ComDiv.GetIdStatics(equip.Accessory, 3);
								b = (byte)((idStatics == 30) ? idStatics2 : ((int)b));
							}
							if (actionStateInfo.Flag.HasFlag(WeaponSyncType.Secondary))
							{
								num2 = equip.WpnSecondary;
							}
							if (actionStateInfo.Flag.HasFlag(WeaponSyncType.Melee))
							{
								num2 = equip.WpnMelee;
							}
							if (actionStateInfo.Flag.HasFlag(WeaponSyncType.Explosive))
							{
								num2 = equip.WpnExplosive;
							}
							if (actionStateInfo.Flag.HasFlag(WeaponSyncType.Special))
							{
								num2 = equip.WpnSpecial;
							}
							if (actionStateInfo.Flag.HasFlag(WeaponSyncType.Mission) && roomModel_0.RoomType == RoomCondition.Bomb)
							{
								num2 = 5009001;
							}
							if (actionStateInfo.Flag.HasFlag(WeaponSyncType.Dual))
							{
								b2 = 16;
								if (actionStateInfo.Action.HasFlag(ActionFlag.Unk2048))
								{
									num2 = equip.WpnPrimary;
								}
								if (actionStateInfo.Action.HasFlag(ActionFlag.Unk4096))
								{
									num2 = equip.WpnPrimary;
								}
							}
							if (actionStateInfo.Flag.HasFlag(WeaponSyncType.Ext))
							{
								b2 = 16;
								if (actionStateInfo.Action.HasFlag(ActionFlag.Unk2048))
								{
									num2 = equip.WpnSecondary;
								}
								if (actionStateInfo.Action.HasFlag(ActionFlag.Unk4096))
								{
									num2 = equip.WpnSecondary;
								}
							}
							ObjectHitInfo objectHitInfo = new ObjectHitInfo(6)
							{
								ObjId = player.Slot,
								WeaponId = num2,
								Accessory = b,
								Extensions = b2
							};
							list.Add(objectHitInfo);
						}
					}
					ActionState.WriteInfo(syncServerPacket, actionStateInfo);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.Animation))
				{
					num += 2U;
					AnimationInfo animationInfo = Animation.ReadInfo(actionModel_0, syncClientPacket, false);
					Animation.WriteInfo(syncServerPacket, animationInfo);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.PosRotation))
				{
					num += 134217728U;
					PosRotationInfo posRotationInfo = PosRotation.ReadInfo(actionModel_0, syncClientPacket, false);
					if (player != null)
					{
						player.Position = new Half3(posRotationInfo.RotationX, posRotationInfo.RotationY, posRotationInfo.RotationZ);
					}
					actionModel_0.Flag |= UdpGameEvent.SoundPosRotation;
					PosRotation.WriteInfo(syncServerPacket, posRotationInfo);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.SoundPosRotation))
				{
					num += 8388608U;
					SoundPosRotationInfo soundPosRotationInfo = SoundPosRotation.ReadInfo(actionModel_0, syncClientPacket, float_0, false);
					SoundPosRotation.WriteInfo(syncServerPacket, soundPosRotationInfo);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.UseObject))
				{
					num += 4U;
					List<UseObjectInfo> list2 = UseObject.ReadInfo(actionModel_0, syncClientPacket, false);
					for (int i = 0; i < list2.Count; i++)
					{
						UseObjectInfo useObjectInfo = list2[i];
						if (!roomModel_0.BotMode && useObjectInfo.ObjectId != 65535)
						{
							ObjectInfo @object = roomModel_0.GetObject((int)useObjectInfo.ObjectId);
							if (@object != null)
							{
								bool flag = false;
								if (useObjectInfo.SpaceFlags.HasFlag(CharaMoves.HeliInMove) && @object.UseDate.ToString("yyMMddHHmm").Equals("0101010000"))
								{
									flag = true;
								}
								useObjectInfo.SpaceFlags.HasFlag(CharaMoves.HeliUnknown);
								useObjectInfo.SpaceFlags.HasFlag(CharaMoves.HeliLeave);
								if (useObjectInfo.SpaceFlags.HasFlag(CharaMoves.HeliStopped))
								{
									AnimModel animation = @object.Animation;
									if (animation != null && animation.Id == 0)
									{
										@object.Model.GetAnim(animation.NextAnim, 0f, 0f, @object);
									}
								}
								if (!flag)
								{
									ObjectHitInfo objectHitInfo2 = new ObjectHitInfo(3)
									{
										ObjSyncId = 1,
										ObjId = @object.Id,
										ObjLife = @object.Life,
										AnimId1 = 255,
										AnimId2 = ((@object.Animation != null) ? @object.Animation.Id : 255),
										SpecialUse = AllUtils.GetDuration(@object.UseDate)
									};
									list.Add(objectHitInfo2);
								}
							}
						}
						else
						{
							AllUtils.RemoveHit(list2, i--);
						}
					}
					UseObject.WriteInfo(syncServerPacket, list2);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.ActionForObjectSync))
				{
					num += 16U;
					ActionObjectInfo actionObjectInfo = ActionForObjectSync.ReadInfo(actionModel_0, syncClientPacket, false);
					if (player != null)
					{
						roomModel_0.SyncInfo(list, 1);
					}
					ActionForObjectSync.WriteInfo(syncServerPacket, actionObjectInfo);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.RadioChat))
				{
					num += 32U;
					RadioChatInfo radioChatInfo = RadioChat.ReadInfo(actionModel_0, syncClientPacket, false);
					RadioChat.WriteInfo(syncServerPacket, radioChatInfo);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.WeaponSync))
				{
					num += 67108864U;
					WeaponSyncInfo weaponSyncInfo = WeaponSync.ReadInfo(actionModel_0, syncClientPacket, false, false);
					if (player != null)
					{
						player.WeaponId = weaponSyncInfo.WeaponId;
						player.Accessory = weaponSyncInfo.Accessory;
						player.Extensions = weaponSyncInfo.Extensions;
						player.WeaponClass = weaponSyncInfo.WeaponClass;
						roomModel_0.SyncInfo(list, 2);
					}
					WeaponSync.WriteInfo(syncServerPacket, weaponSyncInfo);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.WeaponRecoil))
				{
					num += 128U;
					WeaponRecoilInfo weaponRecoilInfo = WeaponRecoil.ReadInfo(actionModel_0, syncClientPacket, false);
					WeaponRecoil.WriteInfo(syncServerPacket, weaponRecoilInfo);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.HpSync))
				{
					num += 8U;
					HPSyncInfo hpsyncInfo = HpSync.ReadInfo(actionModel_0, syncClientPacket, false);
					HpSync.WriteInfo(syncServerPacket, hpsyncInfo);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.Suicide))
				{
					num += 1048576U;
					List<SuicideInfo> list3 = Suicide.ReadInfo(actionModel_0, syncClientPacket, false, false);
					List<DeathServerData> list4 = new List<DeathServerData>();
					if (player != null)
					{
						int num3 = -1;
						int num4 = 0;
						for (int j = 0; j < list3.Count; j++)
						{
							SuicideInfo suicideInfo = list3[j];
							if (!player.Dead && player.Life > 0)
							{
								int hitDamageBot = AllUtils.GetHitDamageBot(suicideInfo.HitInfo);
								int killerId = AllUtils.GetKillerId(suicideInfo.HitInfo);
								int objectType = AllUtils.GetObjectType(suicideInfo.HitInfo);
								CharaHitPart charaHitPart = (CharaHitPart)((suicideInfo.HitInfo >> 4) & 63U);
								CharaDeath charaDeath = AllUtils.GetCharaDeath(suicideInfo.HitInfo);
								if (objectType == 1 || objectType == 0)
								{
									num3 = killerId;
								}
								num4 = suicideInfo.WeaponId;
								DamageManager.SimpleDeath(roomModel_0, list4, list, player, player, hitDamageBot, num4, charaHitPart, charaDeath);
								if (hitDamageBot > 0)
								{
									if (ConfigLoader.UseHitMarker)
									{
										SendMatchInfo.SendHitMarkerSync(roomModel_0, player, charaDeath, HitType.Normal, hitDamageBot);
									}
									ObjectHitInfo objectHitInfo3 = new ObjectHitInfo(2)
									{
										ObjId = player.Slot,
										ObjLife = player.Life,
										HitPart = charaHitPart,
										KillerSlot = num3,
										Position = suicideInfo.PlayerPos,
										DeathType = charaDeath,
										WeaponId = num4
									};
									list.Add(objectHitInfo3);
								}
							}
							else
							{
								AllUtils.RemoveHit(list3, j--);
							}
						}
						if (list4.Count > 0)
						{
							SendMatchInfo.SendDeathSync(roomModel_0, player, num3, num4, list4);
						}
					}
					else
					{
						list3 = new List<SuicideInfo>();
					}
					Suicide.WriteInfo(syncServerPacket, list3);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.MissionData))
				{
					num += 2048U;
					MissionDataInfo missionDataInfo = MissionData.ReadInfo(actionModel_0, syncClientPacket, float_0, false, false);
					if (roomModel_0.Map != null && player != null && !player.Dead && missionDataInfo.PlantTime > 0f && !missionDataInfo.BombEnum.HasFlag(BombFlag.Stop))
					{
						BombPosition bomb = roomModel_0.Map.GetBomb(missionDataInfo.BombId);
						if (bomb != null)
						{
							bool flag2;
							Vector3 vector = ((flag2 = missionDataInfo.BombEnum.HasFlag(BombFlag.Defuse)) ? roomModel_0.BombPosition : (missionDataInfo.BombEnum.HasFlag(BombFlag.Start) ? bomb.Position : new Half3(0, 0, 0)));
							double num5 = (double)Vector3.Distance(player.Position, vector);
							TeamEnum swappedTeam = AllUtils.GetSwappedTeam(player, roomModel_0);
							if ((bomb.EveryWhere || num5 <= 2.0) && ((swappedTeam == TeamEnum.CT_TEAM && flag2) || (swappedTeam == TeamEnum.FR_TEAM && !flag2)))
							{
								if (player.C4Time != missionDataInfo.PlantTime)
								{
									player.C4First = DateTimeUtil.Now();
									player.C4Time = missionDataInfo.PlantTime;
								}
								double duration = ComDiv.GetDuration(player.C4First);
								float num6 = (flag2 ? player.DefuseDuration : player.PlantDuration);
								if ((float_0 >= missionDataInfo.PlantTime + num6 || duration >= (double)num6) && ((!roomModel_0.HasC4 && missionDataInfo.BombEnum.HasFlag(BombFlag.Start)) || (roomModel_0.HasC4 && flag2)))
								{
									roomModel_0.HasC4 = !roomModel_0.HasC4;
									missionDataInfo.Bomb |= 2;
									SendMatchInfo.SendBombSync(roomModel_0, player, (missionDataInfo.BombEnum.HasFlag(BombFlag.Defuse) > false) ? 1 : 0, missionDataInfo.BombId);
								}
							}
						}
					}
					MissionData.WriteInfo(syncServerPacket, missionDataInfo);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.DropWeapon))
				{
					num += 4194304U;
					DropWeaponInfo dropWeaponInfo = DropWeapon.ReadInfo(actionModel_0, syncClientPacket, false);
					if (!roomModel_0.BotMode)
					{
						roomModel_0.DropCounter += 1;
						if ((ushort)roomModel_0.DropCounter > ConfigLoader.MaxDropWpnCount)
						{
							roomModel_0.DropCounter = 0;
						}
						dropWeaponInfo.Counter = roomModel_0.DropCounter;
						Equipment equip2 = player.Equip;
						if (player != null && equip2 != null)
						{
							int idStatics3 = ComDiv.GetIdStatics(dropWeaponInfo.WeaponId, 1);
							if (idStatics3 == 1)
							{
								equip2.WpnPrimary = 0;
							}
							if (idStatics3 == 2)
							{
								equip2.WpnSecondary = 0;
							}
						}
					}
					DropWeapon.WriteInfo(syncServerPacket, dropWeaponInfo);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.GetWeaponForClient))
				{
					num += 16777216U;
					WeaponClient weaponClient = GetWeaponForClient.ReadInfo(actionModel_0, syncClientPacket, false);
					if (!roomModel_0.BotMode)
					{
						Equipment equip3 = player.Equip;
						if (player != null && equip3 != null)
						{
							int idStatics4 = ComDiv.GetIdStatics(weaponClient.WeaponId, 1);
							if (idStatics4 == 1)
							{
								equip3.WpnPrimary = weaponClient.WeaponId;
							}
							if (idStatics4 == 2)
							{
								equip3.WpnSecondary = weaponClient.WeaponId;
							}
						}
					}
					GetWeaponForClient.WriteInfo(syncServerPacket, weaponClient);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.FireData))
				{
					num += 33554432U;
					FireDataInfo fireDataInfo = FireData.ReadInfo(actionModel_0, syncClientPacket, false);
					FireData.WriteInfo(syncServerPacket, fireDataInfo);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.CharaFireNHitData))
				{
					num += 1024U;
					List<CharaFireNHitDataInfo> list5 = CharaFireNHitData.ReadInfo(actionModel_0, syncClientPacket, false, false);
					CharaFireNHitData.WriteInfo(syncServerPacket, list5);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.HitData))
				{
					num += 131072U;
					List<HitDataInfo> list6 = HitData.ReadInfo(actionModel_0, syncClientPacket, false, false);
					List<DeathServerData> list7 = new List<DeathServerData>();
					if (player != null)
					{
						int num7 = 0;
						for (int k = 0; k < list6.Count; k++)
						{
							HitDataInfo hitDataInfo = list6[k];
							if (hitDataInfo.HitEnum != HitType.HelmetProtection && hitDataInfo.HitEnum != HitType.HeadshotProtection)
							{
								int num8;
								if (AllUtils.ValidateHitData(AllUtils.GetHitDamageNormal(hitDataInfo.HitIndex), hitDataInfo, out num8))
								{
									int objectId = (int)hitDataInfo.ObjectId;
									CharaHitPart hitPart = AllUtils.GetHitPart(hitDataInfo.HitIndex);
									CharaDeath charaDeath2 = CharaDeath.DEFAULT;
									num7 = hitDataInfo.WeaponId;
									ObjectType hitType = AllUtils.GetHitType(hitDataInfo.HitIndex);
									if (hitType == ObjectType.Object)
									{
										ObjectInfo object2 = roomModel_0.GetObject(objectId);
										ObjectModel objectModel = ((object2 != null) ? object2.Model : null);
										if (objectModel != null && objectModel.Destroyable)
										{
											if (object2.Life > 0)
											{
												object2.Life -= num8;
												if (object2.Life <= 0)
												{
													object2.Life = 0;
													DamageManager.BoomDeath(roomModel_0, player, num8, num7, list7, list, hitDataInfo.BoomPlayers, CharaHitPart.ALL, CharaDeath.OBJECT_EXPLOSION);
												}
												object2.DestroyState = objectModel.CheckDestroyState(object2.Life);
												DamageManager.SabotageDestroy(roomModel_0, player, objectModel, object2, num8);
												float duration2 = AllUtils.GetDuration(object2.UseDate);
												if (object2.Animation != null && object2.Animation.Duration > 0f && duration2 >= object2.Animation.Duration)
												{
													object2.Model.GetAnim(object2.Animation.NextAnim, duration2, object2.Animation.Duration, object2);
												}
												ObjectHitInfo objectHitInfo4 = new ObjectHitInfo(objectModel.UpdateId)
												{
													ObjId = object2.Id,
													ObjLife = object2.Life,
													KillerSlot = (int)actionModel_0.Slot,
													ObjSyncId = ((objectModel.NeedSync > false) ? 1 : 0),
													SpecialUse = duration2,
													AnimId1 = objectModel.Animation,
													AnimId2 = ((object2.Animation != null) ? object2.Animation.Id : 255),
													DestroyState = object2.DestroyState
												};
												list.Add(objectHitInfo4);
											}
										}
										else if (ConfigLoader.SendFailMsg && objectModel == null)
										{
											CLogger.Print(string.Format("Fire Obj: {0} Map: {1} Invalid Object.", objectId, roomModel_0.MapId), LoggerType.Warning, null);
											player.LogPlayerPos(hitDataInfo.EndBullet);
										}
									}
									else if (hitType == ObjectType.User)
									{
										PlayerModel playerModel;
										if (roomModel_0.GetPlayer(objectId, out playerModel) && player.RespawnIsValid() && !player.Dead && !playerModel.Dead && !playerModel.Immortal)
										{
											if (hitPart == CharaHitPart.HEAD)
											{
												charaDeath2 = CharaDeath.HEADSHOT;
											}
											if (roomModel_0.RoomType == RoomCondition.DeathMatch && roomModel_0.Rule == MapRules.HeadHunter && charaDeath2 != CharaDeath.HEADSHOT)
											{
												num8 = 1;
											}
											else if (roomModel_0.RoomType == RoomCondition.Boss && charaDeath2 == CharaDeath.HEADSHOT)
											{
												if ((roomModel_0.LastRound == 1 && playerModel.Team == TeamEnum.FR_TEAM) || (roomModel_0.LastRound == 2 && playerModel.Team == TeamEnum.CT_TEAM))
												{
													num8 /= 10;
												}
											}
											else if (roomModel_0.RoomType == RoomCondition.DeathMatch && roomModel_0.Rule == MapRules.Chaos)
											{
												num8 = 200;
											}
											DamageManager.SimpleDeath(roomModel_0, list7, list, player, playerModel, num8, num7, hitPart, charaDeath2);
											if (num8 > 0)
											{
												if (ConfigLoader.UseHitMarker)
												{
													SendMatchInfo.SendHitMarkerSync(roomModel_0, player, charaDeath2, hitDataInfo.HitEnum, num8);
												}
												ObjectHitInfo objectHitInfo5 = new ObjectHitInfo(2)
												{
													ObjId = playerModel.Slot,
													ObjLife = playerModel.Life,
													HitPart = hitPart,
													KillerSlot = player.Slot,
													Position = playerModel.Position - player.Position,
													DeathType = charaDeath2,
													WeaponId = num7
												};
												list.Add(objectHitInfo5);
											}
										}
										else
										{
											AllUtils.RemoveHit(list6, k--);
										}
									}
									else if (hitType != ObjectType.UserObject)
									{
										CLogger.Print(string.Format("HitType: ({0}/{1}) Slot: {2}", hitType, (int)hitType, actionModel_0.Slot), LoggerType.Warning, null);
										CLogger.Print(string.Format("BoomPlayers: {0} {1}", hitDataInfo.BoomInfo, hitDataInfo.BoomPlayers.Count), LoggerType.Warning, null);
									}
								}
								else
								{
									AllUtils.RemoveHit(list6, k--);
								}
							}
						}
						if (list7.Count > 0)
						{
							SendMatchInfo.SendDeathSync(roomModel_0, player, 255, num7, list7);
						}
					}
					else
					{
						list6 = new List<HitDataInfo>();
					}
					HitData.WriteInfo(syncServerPacket, list6);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.GrenadeHit))
				{
					num += 268435456U;
					List<GrenadeHitInfo> list8 = GrenadeHit.ReadInfo(actionModel_0, syncClientPacket, false, false);
					List<DeathServerData> list9 = new List<DeathServerData>();
					if (player != null)
					{
						int num9 = -1;
						int num10 = 0;
						for (int l = 0; l < list8.Count; l++)
						{
							GrenadeHitInfo grenadeHitInfo = list8[l];
							int num11;
							if (AllUtils.ValidateGrenadeHit(AllUtils.GetHitDamageNormal(grenadeHitInfo.HitInfo), grenadeHitInfo, out num11))
							{
								int objectId2 = (int)grenadeHitInfo.ObjectId;
								CharaHitPart hitPart2 = AllUtils.GetHitPart(grenadeHitInfo.HitInfo);
								num10 = grenadeHitInfo.WeaponId;
								ObjectType hitType2 = AllUtils.GetHitType(grenadeHitInfo.HitInfo);
								if (hitType2 == ObjectType.Object)
								{
									ObjectInfo object3 = roomModel_0.GetObject(objectId2);
									ObjectModel objectModel2 = ((object3 != null) ? object3.Model : null);
									if (objectModel2 != null && objectModel2.Destroyable && object3.Life > 0)
									{
										object3.Life -= num11;
										if (object3.Life <= 0)
										{
											object3.Life = 0;
											DamageManager.BoomDeath(roomModel_0, player, num11, num10, list9, list, grenadeHitInfo.BoomPlayers, CharaHitPart.ALL, CharaDeath.OBJECT_EXPLOSION);
										}
										object3.DestroyState = objectModel2.CheckDestroyState(object3.Life);
										DamageManager.SabotageDestroy(roomModel_0, player, objectModel2, object3, num11);
										if (num11 > 0)
										{
											ObjectHitInfo objectHitInfo6 = new ObjectHitInfo(objectModel2.UpdateId)
											{
												ObjId = object3.Id,
												ObjLife = object3.Life,
												KillerSlot = (int)actionModel_0.Slot,
												ObjSyncId = ((objectModel2.NeedSync > false) ? 1 : 0),
												AnimId1 = objectModel2.Animation,
												AnimId2 = ((object3.Animation != null) ? object3.Animation.Id : 255),
												DestroyState = object3.DestroyState,
												SpecialUse = AllUtils.GetDuration(object3.UseDate)
											};
											list.Add(objectHitInfo6);
										}
									}
									else if (ConfigLoader.SendFailMsg && objectModel2 == null)
									{
										CLogger.Print(string.Format("Boom Obj: {0} Map: {1} Invalid Object.", objectId2, roomModel_0.MapId), LoggerType.Warning, null);
										player.LogPlayerPos(grenadeHitInfo.HitPos);
									}
								}
								else if (hitType2 == ObjectType.User)
								{
									num9++;
									PlayerModel playerModel2;
									if (num11 > 0 && roomModel_0.GetPlayer(objectId2, out playerModel2) && player.RespawnIsValid() && !playerModel2.Dead && !playerModel2.Immortal)
									{
										TeamEnum teamEnum = ((num9 % 2 == 0) ? TeamEnum.FR_TEAM : TeamEnum.CT_TEAM);
										if (grenadeHitInfo.DeathType == CharaDeath.MEDICAL_KIT)
										{
											playerModel2.Life += num11;
											playerModel2.CheckLifeValue();
										}
										else if (grenadeHitInfo.DeathType == CharaDeath.BOOM && ClassType.Dino != grenadeHitInfo.WeaponClass && (teamEnum == TeamEnum.FR_TEAM || teamEnum == TeamEnum.CT_TEAM))
										{
											num11 = (int)Math.Ceiling((double)num11 / 2.7);
											DamageManager.SimpleDeath(roomModel_0, list9, list, player, playerModel2, num11, num10, hitPart2, grenadeHitInfo.DeathType);
										}
										else
										{
											DamageManager.SimpleDeath(roomModel_0, list9, list, player, playerModel2, num11, num10, hitPart2, grenadeHitInfo.DeathType);
										}
										if (num11 > 0)
										{
											if (ConfigLoader.UseHitMarker)
											{
												SendMatchInfo.SendHitMarkerSync(roomModel_0, player, grenadeHitInfo.DeathType, grenadeHitInfo.HitEnum, num11);
											}
											ObjectHitInfo objectHitInfo7 = new ObjectHitInfo(2)
											{
												ObjId = playerModel2.Slot,
												ObjLife = playerModel2.Life,
												HitPart = hitPart2,
												KillerSlot = player.Slot,
												Position = playerModel2.Position - player.Position,
												DeathType = grenadeHitInfo.DeathType,
												WeaponId = num10
											};
											list.Add(objectHitInfo7);
										}
									}
									else
									{
										AllUtils.RemoveHit(list8, l--);
									}
								}
								else if (hitType2 != ObjectType.UserObject)
								{
									CLogger.Print(string.Format("Grenade Boom, HitType: ({0}/{1})", hitType2, (int)hitType2), LoggerType.Warning, null);
								}
							}
							else
							{
								AllUtils.RemoveHit(list8, l--);
							}
						}
						if (list9.Count > 0)
						{
							SendMatchInfo.SendDeathSync(roomModel_0, player, 255, num10, list9);
						}
					}
					else
					{
						list8 = new List<GrenadeHitInfo>();
					}
					GrenadeHit.WriteInfo(syncServerPacket, list8);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.GetWeaponForHost))
				{
					num += 512U;
					WeaponHost weaponHost = GetWeaponForHost.ReadInfo(actionModel_0, syncClientPacket, false);
					GetWeaponForHost.WriteInfo(syncServerPacket, weaponHost);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.FireDataOnObject))
				{
					num += 1073741824U;
					FireDataObjectInfo fireDataObjectInfo = FireDataOnObject.ReadInfo(actionModel_0, syncClientPacket, false);
					FireDataOnObject.WriteInfo(syncServerPacket, fireDataObjectInfo);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.FireNHitDataOnObject))
				{
					num += 8192U;
					FireNHitDataObjectInfo fireNHitDataObjectInfo = FireNHitDataOnObject.ReadInfo(actionModel_0, syncClientPacket, false);
					if (player != null && !player.Dead)
					{
						SendMatchInfo.SendPortalPass(roomModel_0, player, (int)fireNHitDataObjectInfo.Portal);
					}
					FireNHitDataOnObject.WriteInfo(syncServerPacket, fireNHitDataObjectInfo);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.SeizeDataForClient))
				{
					num += 32768U;
					SeizeDataForClientInfo seizeDataForClientInfo = SeizeDataForClient.ReadInfo(actionModel_0, syncClientPacket, true);
					if (player == null)
					{
					}
					SeizeDataForClient.WriteInfo(syncServerPacket, seizeDataForClientInfo);
				}
				byte_0 = syncServerPacket.ToArray();
				if (num != (uint)actionModel_0.Flag && ConfigLoader.IsTestMode)
				{
					CLogger.Print(Bitwise.ToHexData(string.Format("PVP - Missing Flag Events: '{0}' | '{1}'", (uint)actionModel_0.Flag, actionModel_0.Flag - (UdpGameEvent)num), data), LoggerType.Opcode, null);
				}
				list10 = list;
			}
			return list10;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00004B58 File Offset: 0x00002D58
		public byte[] WritePlayerActionData(byte[] Data, RoomModel Room, float Time, PacketModel Packet)
		{
			SyncClientPacket syncClientPacket = new SyncClientPacket(Data);
			List<ObjectHitInfo> list = new List<ObjectHitInfo>();
			byte[] array2;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				for (int i = 0; i < 18; i++)
				{
					ActionModel actionModel = new ActionModel();
					try
					{
						bool flag = false;
						bool flag2;
						actionModel.Length = syncClientPacket.ReadUH(out flag2);
						if (flag2)
						{
							break;
						}
						actionModel.Slot = syncClientPacket.ReadUH();
						actionModel.SubHead = (UdpSubHead)syncClientPacket.ReadC();
						if (actionModel.SubHead == (UdpSubHead)255)
						{
							break;
						}
						syncServerPacket.WriteH(actionModel.Length);
						syncServerPacket.WriteH(actionModel.Slot);
						syncServerPacket.WriteC((byte)actionModel.SubHead);
						switch (actionModel.SubHead)
						{
						case UdpSubHead.User:
						case UdpSubHead.StageInfoChara:
						{
							actionModel.Flag = (UdpGameEvent)syncClientPacket.ReadUD();
							actionModel.Data = syncClientPacket.ReadB((int)(actionModel.Length - 9));
							AllUtils.CheckDataFlags(actionModel, Packet);
							byte[] array;
							list.AddRange(this.method_0(actionModel, Room, Time, out array));
							syncServerPacket.GoBack(5);
							syncServerPacket.WriteH((ushort)(array.Length + 9));
							syncServerPacket.WriteH(actionModel.Slot);
							syncServerPacket.WriteC((byte)actionModel.SubHead);
							syncServerPacket.WriteD((uint)actionModel.Flag);
							syncServerPacket.WriteB(array);
							if (actionModel.Data.Length == 0 && actionModel.Length - 9 != 0)
							{
								flag = true;
								goto IL_276;
							}
							goto IL_276;
						}
						case UdpSubHead.Grenade:
						{
							GrenadeInfo grenadeInfo = GrenadeSync.ReadInfo(syncClientPacket, false, false);
							GrenadeSync.WriteInfo(syncServerPacket, grenadeInfo);
							goto IL_276;
						}
						case UdpSubHead.DroppedWeapon:
						{
							DropedWeaponInfo dropedWeaponInfo = DropedWeapon.ReadInfo(syncClientPacket, false);
							DropedWeapon.WriteInfo(syncServerPacket, dropedWeaponInfo);
							goto IL_276;
						}
						case UdpSubHead.ObjectStatic:
						{
							ObjectStaticInfo objectStaticInfo = ObjectStatic.ReadInfo(syncClientPacket, false);
							ObjectStatic.WriteInfo(syncServerPacket, objectStaticInfo);
							goto IL_276;
						}
						case UdpSubHead.ObjectMove:
						{
							ObjectMoveInfo objectMoveInfo = ObjectMove.ReadInfo(syncClientPacket, false);
							ObjectMove.WriteInfo(syncServerPacket, objectMoveInfo);
							goto IL_276;
						}
						case UdpSubHead.ObjectAnim:
						{
							ObjectAnimInfo objectAnimInfo = ObjectAnim.ReadInfo(syncClientPacket, false);
							ObjectAnim.WriteInfo(syncServerPacket, objectAnimInfo);
							goto IL_276;
						}
						case UdpSubHead.StageInfoObjectStatic:
						{
							StageStaticInfo stageStaticInfo = StageInfoObjStatic.ReadInfo(syncClientPacket, false);
							StageInfoObjStatic.WriteInfo(syncServerPacket, stageStaticInfo);
							goto IL_276;
						}
						case UdpSubHead.StageInfoObjectAnim:
						{
							StageAnimInfo stageAnimInfo = StageInfoObjAnim.ReadInfo(syncClientPacket, false);
							StageInfoObjAnim.WriteInfo(syncServerPacket, stageAnimInfo);
							goto IL_276;
						}
						case UdpSubHead.StageInfoObjectControl:
						{
							StageControlInfo stageControlInfo = StageInfoObjControl.ReadInfo(syncClientPacket, false);
							StageInfoObjControl.WriteInfo(syncServerPacket, stageControlInfo);
							goto IL_276;
						}
						}
						if (ConfigLoader.IsTestMode)
						{
							CLogger.Print(Bitwise.ToHexData(string.Format("PVP Sub Head: '{0}' or '{1}'", actionModel.SubHead, (int)actionModel.SubHead), Data), LoggerType.Opcode, null);
						}
						IL_276:
						if (flag)
						{
							break;
						}
					}
					catch (Exception ex)
					{
						if (ConfigLoader.IsTestMode)
						{
							CLogger.Print(string.Format("PVP Action Data - Buffer (Length: {0}): | {1}", Data.Length, ex.Message), LoggerType.Error, ex);
						}
						list = new List<ObjectHitInfo>();
						break;
					}
				}
				if (list.Count > 0)
				{
					syncServerPacket.WriteB(PROTOCOL_EVENTS_ACTION.GET_CODE(list));
				}
				list = null;
				array2 = syncServerPacket.ToArray();
			}
			return array2;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00004E84 File Offset: 0x00003084
		private List<ObjectHitInfo> method_1(ActionModel actionModel_0, RoomModel roomModel_0, out byte[] byte_0)
		{
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
			List<ObjectHitInfo> list = new List<ObjectHitInfo>();
			SyncClientPacket syncClientPacket = new SyncClientPacket(data);
			List<ObjectHitInfo> list2;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				uint num = 0U;
				PlayerModel player = roomModel_0.GetPlayer((int)actionModel_0.Slot, false);
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.ActionState))
				{
					num += 256U;
					ActionFlag actionFlag = (ActionFlag)syncClientPacket.ReadUH();
					byte b = syncClientPacket.ReadC();
					WeaponSyncType weaponSyncType = (WeaponSyncType)syncClientPacket.ReadC();
					syncServerPacket.WriteH((ushort)actionFlag);
					syncServerPacket.WriteC(b);
					syncServerPacket.WriteC((byte)weaponSyncType);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.Animation))
				{
					num += 2U;
					ushort num2 = syncClientPacket.ReadUH();
					syncServerPacket.WriteH(num2);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.PosRotation))
				{
					num += 134217728U;
					ushort num3 = syncClientPacket.ReadUH();
					ushort num4 = syncClientPacket.ReadUH();
					ushort num5 = syncClientPacket.ReadUH();
					ushort num6 = syncClientPacket.ReadUH();
					ushort num7 = syncClientPacket.ReadUH();
					ushort num8 = syncClientPacket.ReadUH();
					if (player != null)
					{
						player.Position = new Half3(num7, num8, num6);
					}
					syncServerPacket.WriteH(num3);
					syncServerPacket.WriteH(num4);
					syncServerPacket.WriteH(num5);
					syncServerPacket.WriteH(num6);
					syncServerPacket.WriteH(num7);
					syncServerPacket.WriteH(num8);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.SoundPosRotation))
				{
					num += 8388608U;
					float num9 = syncClientPacket.ReadT();
					syncServerPacket.WriteT(num9);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.UseObject))
				{
					num += 4U;
					byte b2 = syncClientPacket.ReadC();
					syncServerPacket.WriteC(b2);
					for (int i = 0; i < (int)b2; i++)
					{
						ushort num10 = syncClientPacket.ReadUH();
						byte b3 = syncClientPacket.ReadC();
						CharaMoves charaMoves = (CharaMoves)syncClientPacket.ReadC();
						syncServerPacket.WriteH(num10);
						syncServerPacket.WriteC(b3);
						syncServerPacket.WriteC((byte)charaMoves);
					}
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.ActionForObjectSync))
				{
					num += 16U;
					byte b4 = syncClientPacket.ReadC();
					byte b5 = syncClientPacket.ReadC();
					if (player != null)
					{
						roomModel_0.SyncInfo(list, 1);
					}
					syncServerPacket.WriteC(b4);
					syncServerPacket.WriteC(b5);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.RadioChat))
				{
					num += 32U;
					byte b6 = syncClientPacket.ReadC();
					byte b7 = syncClientPacket.ReadC();
					syncServerPacket.WriteC(b6);
					syncServerPacket.WriteC(b7);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.WeaponSync))
				{
					num += 67108864U;
					int num11 = syncClientPacket.ReadD();
					byte b8 = syncClientPacket.ReadC();
					byte b9 = syncClientPacket.ReadC();
					if (player != null)
					{
						player.WeaponId = num11;
						player.Accessory = b8;
						player.Extensions = b9;
						player.WeaponClass = (ClassType)ComDiv.GetIdStatics(num11, 2);
						roomModel_0.SyncInfo(list, 2);
					}
					syncServerPacket.WriteD(num11);
					syncServerPacket.WriteC(b8);
					syncServerPacket.WriteC(b9);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.WeaponRecoil))
				{
					num += 128U;
					float num12 = syncClientPacket.ReadT();
					float num13 = syncClientPacket.ReadT();
					float num14 = syncClientPacket.ReadT();
					float num15 = syncClientPacket.ReadT();
					float num16 = syncClientPacket.ReadT();
					byte b10 = syncClientPacket.ReadC();
					int num17 = syncClientPacket.ReadD();
					byte b11 = syncClientPacket.ReadC();
					byte b12 = syncClientPacket.ReadC();
					CLogger.Print(string.Format("PVE (WeaponRecoil); Slot: {0}; Weapon Id: {1}; Extensions: {2}; Unknowns: {3}", new object[] { player.Slot, num17, b12, b11 }), LoggerType.Warning, null);
					syncServerPacket.WriteT(num12);
					syncServerPacket.WriteT(num13);
					syncServerPacket.WriteT(num14);
					syncServerPacket.WriteT(num15);
					syncServerPacket.WriteT(num16);
					syncServerPacket.WriteC(b10);
					syncServerPacket.WriteD(num17);
					syncServerPacket.WriteC(b11);
					syncServerPacket.WriteC(b12);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.HpSync))
				{
					num += 8U;
					ushort num18 = syncClientPacket.ReadUH();
					syncServerPacket.WriteH(num18);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.Suicide))
				{
					num += 1048576U;
					byte b13 = syncClientPacket.ReadC();
					syncServerPacket.WriteC(b13);
					for (int j = 0; j < (int)b13; j++)
					{
						Half3 half = syncClientPacket.ReadUHV();
						int num19 = syncClientPacket.ReadD();
						byte b14 = syncClientPacket.ReadC();
						byte b15 = syncClientPacket.ReadC();
						uint num20 = syncClientPacket.ReadUD();
						syncServerPacket.WriteHV(half);
						syncServerPacket.WriteD(num19);
						syncServerPacket.WriteC(b14);
						syncServerPacket.WriteC(b15);
						syncServerPacket.WriteD(num20);
					}
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.DropWeapon))
				{
					num += 4194304U;
					ushort num21 = syncClientPacket.ReadUH();
					ushort num22 = syncClientPacket.ReadUH();
					ushort num23 = syncClientPacket.ReadUH();
					ushort num24 = syncClientPacket.ReadUH();
					ushort num25 = syncClientPacket.ReadUH();
					ushort num26 = syncClientPacket.ReadUH();
					byte b16 = syncClientPacket.ReadC();
					int num27 = syncClientPacket.ReadD();
					byte b17 = syncClientPacket.ReadC();
					byte b18 = syncClientPacket.ReadC();
					if (ConfigLoader.UseMaxAmmoInDrop)
					{
						syncServerPacket.WriteH(ushort.MaxValue);
						syncServerPacket.WriteH(num22);
						syncServerPacket.WriteH(10000);
					}
					else
					{
						syncServerPacket.WriteH(num21);
						syncServerPacket.WriteH(num22);
						syncServerPacket.WriteH(num23);
					}
					syncServerPacket.WriteH(num24);
					syncServerPacket.WriteH(num25);
					syncServerPacket.WriteH(num26);
					syncServerPacket.WriteC(b16);
					syncServerPacket.WriteD(num27);
					syncServerPacket.WriteC(b17);
					syncServerPacket.WriteC(b18);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.GetWeaponForClient))
				{
					num += 16777216U;
					ushort num28 = syncClientPacket.ReadUH();
					ushort num29 = syncClientPacket.ReadUH();
					ushort num30 = syncClientPacket.ReadUH();
					ushort num31 = syncClientPacket.ReadUH();
					ushort num32 = syncClientPacket.ReadUH();
					ushort num33 = syncClientPacket.ReadUH();
					byte b19 = syncClientPacket.ReadC();
					int num34 = syncClientPacket.ReadD();
					byte b20 = syncClientPacket.ReadC();
					byte b21 = syncClientPacket.ReadC();
					if (ConfigLoader.UseMaxAmmoInDrop)
					{
						syncServerPacket.WriteH(ushort.MaxValue);
						syncServerPacket.WriteH(num29);
						syncServerPacket.WriteH(10000);
					}
					else
					{
						syncServerPacket.WriteH(num28);
						syncServerPacket.WriteH(num29);
						syncServerPacket.WriteH(num30);
					}
					syncServerPacket.WriteH(num31);
					syncServerPacket.WriteH(num32);
					syncServerPacket.WriteH(num33);
					syncServerPacket.WriteC(b19);
					syncServerPacket.WriteD(num34);
					syncServerPacket.WriteC(b20);
					syncServerPacket.WriteC(b21);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.FireData))
				{
					num += 33554432U;
					byte b22 = syncClientPacket.ReadC();
					byte b23 = syncClientPacket.ReadC();
					short num35 = syncClientPacket.ReadH();
					int num36 = syncClientPacket.ReadD();
					byte b24 = syncClientPacket.ReadC();
					byte b25 = syncClientPacket.ReadC();
					ushort num37 = syncClientPacket.ReadUH();
					ushort num38 = syncClientPacket.ReadUH();
					ushort num39 = syncClientPacket.ReadUH();
					syncServerPacket.WriteC(b22);
					syncServerPacket.WriteC(b23);
					syncServerPacket.WriteH(num35);
					syncServerPacket.WriteD(num36);
					syncServerPacket.WriteC(b24);
					syncServerPacket.WriteC(b25);
					syncServerPacket.WriteH(num37);
					syncServerPacket.WriteH(num38);
					syncServerPacket.WriteH(num39);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.CharaFireNHitData))
				{
					num += 1024U;
					byte b26 = syncClientPacket.ReadC();
					syncServerPacket.WriteC(b26);
					for (int k = 0; k < (int)b26; k++)
					{
						int num40 = syncClientPacket.ReadD();
						byte b27 = syncClientPacket.ReadC();
						byte b28 = syncClientPacket.ReadC();
						ushort num41 = syncClientPacket.ReadUH();
						uint num42 = syncClientPacket.ReadUD();
						ushort num43 = syncClientPacket.ReadUH();
						ushort num44 = syncClientPacket.ReadUH();
						ushort num45 = syncClientPacket.ReadUH();
						syncServerPacket.WriteD(num40);
						syncServerPacket.WriteC(b27);
						syncServerPacket.WriteC(b28);
						syncServerPacket.WriteH(num41);
						syncServerPacket.WriteD(num42);
						syncServerPacket.WriteH(num43);
						syncServerPacket.WriteH(num44);
						syncServerPacket.WriteH(num45);
					}
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.GetWeaponForHost))
				{
					num += 512U;
					CharaDeath charaDeath = (CharaDeath)syncClientPacket.ReadC();
					byte b29 = syncClientPacket.ReadC();
					ushort num46 = syncClientPacket.ReadUH();
					ushort num47 = syncClientPacket.ReadUH();
					ushort num48 = syncClientPacket.ReadUH();
					int num49 = syncClientPacket.ReadD();
					CharaHitPart charaHitPart = (CharaHitPart)syncClientPacket.ReadC();
					syncServerPacket.WriteC((byte)charaDeath);
					syncServerPacket.WriteC(b29);
					syncServerPacket.WriteH(num46);
					syncServerPacket.WriteH(num47);
					syncServerPacket.WriteH(num48);
					syncServerPacket.WriteD(num49);
					syncServerPacket.WriteC((byte)charaHitPart);
				}
				if (actionModel_0.Flag.HasFlag(UdpGameEvent.FireDataOnObject))
				{
					num += 1073741824U;
					byte b30 = syncClientPacket.ReadC();
					CharaHitPart charaHitPart2 = (CharaHitPart)syncClientPacket.ReadC();
					byte b31 = syncClientPacket.ReadC();
					syncServerPacket.WriteC(b30);
					syncServerPacket.WriteC((byte)charaHitPart2);
					syncServerPacket.WriteC(b31);
				}
				byte_0 = syncServerPacket.ToArray();
				if (num != (uint)actionModel_0.Flag)
				{
					CLogger.Print(Bitwise.ToHexData(string.Format("PVE - Missing Flag Events: '{0}' | '{1}'", (uint)actionModel_0.Flag, actionModel_0.Flag - (UdpGameEvent)num), data), LoggerType.Opcode, null);
				}
				list2 = list;
			}
			return list2;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00005844 File Offset: 0x00003A44
		public byte[] WriteBotActionData(byte[] Data, RoomModel Room)
		{
			SyncClientPacket syncClientPacket = new SyncClientPacket(Data);
			List<ObjectHitInfo> list = new List<ObjectHitInfo>();
			byte[] array9;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				for (int i = 0; i < 18; i++)
				{
					ActionModel actionModel = new ActionModel();
					try
					{
						bool flag = false;
						bool flag2;
						actionModel.Length = syncClientPacket.ReadUH(out flag2);
						if (flag2)
						{
							break;
						}
						actionModel.Slot = syncClientPacket.ReadUH();
						actionModel.SubHead = (UdpSubHead)syncClientPacket.ReadC();
						if (actionModel.SubHead == (UdpSubHead)255)
						{
							break;
						}
						syncServerPacket.WriteH(actionModel.Length);
						syncServerPacket.WriteH(actionModel.Slot);
						syncServerPacket.WriteC((byte)actionModel.SubHead);
						switch (actionModel.SubHead)
						{
						case UdpSubHead.User:
						case UdpSubHead.StageInfoChara:
						{
							actionModel.Flag = (UdpGameEvent)syncClientPacket.ReadUD();
							actionModel.Data = syncClientPacket.ReadB((int)(actionModel.Length - 9));
							byte[] array;
							list.AddRange(this.method_1(actionModel, Room, out array));
							syncServerPacket.GoBack(5);
							syncServerPacket.WriteH((ushort)(array.Length + 9));
							syncServerPacket.WriteH(actionModel.Slot);
							syncServerPacket.WriteC((byte)actionModel.SubHead);
							syncServerPacket.WriteD((uint)actionModel.Flag);
							syncServerPacket.WriteB(array);
							if (actionModel.Data.Length == 0 && actionModel.Length - 9 != 0)
							{
								flag = true;
								goto IL_327;
							}
							goto IL_327;
						}
						case UdpSubHead.Grenade:
						{
							byte b = syncClientPacket.ReadC();
							byte b2 = syncClientPacket.ReadC();
							byte b3 = syncClientPacket.ReadC();
							byte b4 = syncClientPacket.ReadC();
							ushort num = syncClientPacket.ReadUH();
							int num2 = syncClientPacket.ReadD();
							byte b5 = syncClientPacket.ReadC();
							byte b6 = syncClientPacket.ReadC();
							ushort num3 = syncClientPacket.ReadUH();
							ushort num4 = syncClientPacket.ReadUH();
							ushort num5 = syncClientPacket.ReadUH();
							int num6 = syncClientPacket.ReadD();
							int num7 = syncClientPacket.ReadD();
							int num8 = syncClientPacket.ReadD();
							syncServerPacket.WriteC(b);
							syncServerPacket.WriteC(b2);
							syncServerPacket.WriteC(b3);
							syncServerPacket.WriteC(b4);
							syncServerPacket.WriteH(num);
							syncServerPacket.WriteD(num2);
							syncServerPacket.WriteC(b5);
							syncServerPacket.WriteC(b6);
							syncServerPacket.WriteH(num3);
							syncServerPacket.WriteH(num4);
							syncServerPacket.WriteH(num5);
							syncServerPacket.WriteD(num6);
							syncServerPacket.WriteD(num7);
							syncServerPacket.WriteD(num8);
							goto IL_327;
						}
						case UdpSubHead.DroppedWeapon:
						{
							byte[] array2 = syncClientPacket.ReadB(31);
							syncServerPacket.WriteB(array2);
							goto IL_327;
						}
						case UdpSubHead.ObjectStatic:
						{
							byte[] array3 = syncClientPacket.ReadB(10);
							syncServerPacket.WriteB(array3);
							goto IL_327;
						}
						case UdpSubHead.ObjectMove:
						{
							byte[] array4 = syncClientPacket.ReadB(16);
							syncServerPacket.WriteB(array4);
							goto IL_327;
						}
						case UdpSubHead.ObjectAnim:
						{
							byte[] array5 = syncClientPacket.ReadB(8);
							syncServerPacket.WriteB(array5);
							goto IL_327;
						}
						case UdpSubHead.StageInfoObjectStatic:
						{
							byte[] array6 = syncClientPacket.ReadB(1);
							syncServerPacket.WriteB(array6);
							goto IL_327;
						}
						case UdpSubHead.StageInfoObjectAnim:
						{
							byte[] array7 = syncClientPacket.ReadB(9);
							syncServerPacket.WriteB(array7);
							goto IL_327;
						}
						case UdpSubHead.StageInfoObjectControl:
						{
							byte[] array8 = syncClientPacket.ReadB(9);
							syncServerPacket.WriteB(array8);
							goto IL_327;
						}
						}
						if (ConfigLoader.IsTestMode)
						{
							CLogger.Print(Bitwise.ToHexData(string.Format("PVP Sub Head: '{0}' or '{1}'", actionModel.SubHead, (int)actionModel.SubHead), Data), LoggerType.Opcode, null);
						}
						IL_327:
						if (flag)
						{
							break;
						}
					}
					catch (Exception ex)
					{
						if (ConfigLoader.IsTestMode)
						{
							CLogger.Print(string.Format("PVE Action Data - Buffer (Length: {0}): | {1}", Data.Length, ex.Message), LoggerType.Error, ex);
						}
						list = new List<ObjectHitInfo>();
						break;
					}
				}
				if (list.Count > 0)
				{
					syncServerPacket.WriteB(PROTOCOL_EVENTS_ACTION.GET_CODE(list));
				}
				list = null;
				array9 = syncServerPacket.ToArray();
			}
			return array9;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00005C20 File Offset: 0x00003E20
		public void SendPacket(byte[] Data, IPEndPoint Address)
		{
			try
			{
				this.socket_0.BeginSendTo(Data, 0, Data.Length, SocketFlags.None, Address, new AsyncCallback(this.method_2), this.socket_0);
			}
			catch (Exception ex)
			{
				CLogger.Print(string.Format("Failed to send package to {0}: {1}", Address, ex.Message), LoggerType.Error, ex);
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00005C80 File Offset: 0x00003E80
		private void method_2(IAsyncResult iasyncResult_0)
		{
			try
			{
				Socket socket = iasyncResult_0.AsyncState as Socket;
				if (socket != null && socket.Connected)
				{
					socket.EndSend(iasyncResult_0);
				}
			}
			catch (SocketException ex)
			{
				CLogger.Print(string.Format("Socket Error on Send: {0}", ex.SocketErrorCode), LoggerType.Warning, null);
			}
			catch (ObjectDisposedException)
			{
				CLogger.Print("Socket was closed while sending.", LoggerType.Warning, null);
			}
			catch (Exception ex2)
			{
				CLogger.Print("Error during EndSendCallback: " + ex2.Message, LoggerType.Error, ex2);
			}
		}

		// Token: 0x04000001 RID: 1
		private readonly Socket socket_0;

		// Token: 0x04000002 RID: 2
		private readonly IPEndPoint ipendPoint_0;
	}
}
