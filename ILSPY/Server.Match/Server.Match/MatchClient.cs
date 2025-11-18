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

namespace Server.Match;

public class MatchClient
{
	private readonly Socket socket_0;

	private readonly IPEndPoint ipendPoint_0;

	public MatchClient(Socket socket_1, IPEndPoint ipendPoint_1)
	{
		socket_0 = socket_1;
		ipendPoint_0 = ipendPoint_1;
	}

	public void BeginReceive(byte[] Buffer, DateTime Date)
	{
		PacketModel packetModel = new PacketModel
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
		if (packetModel.Length > packetModel.Data.Length)
		{
			CLogger.Print($"Packet with invalid size canceled. [ Length: {packetModel.Length} DataLength: {packetModel.Data.Length} ]", LoggerType.Warning);
			return;
		}
		AllUtils.GetDecryptedData(packetModel);
		if (ConfigLoader.IsTestMode && packetModel.Unk1 > 0)
		{
			CLogger.Print(Bitwise.ToHexData($"[N] Test Mode, Packet Unk: {packetModel.Unk1}", packetModel.Data), LoggerType.Opcode);
			CLogger.Print(Bitwise.ToHexData($"[D] Test Mode, Packet Unk: {packetModel.Unk1}", packetModel.WithoutEndData), LoggerType.Opcode);
		}
		if (ConfigLoader.EnableLog && packetModel.Opcode != 131 && packetModel.Opcode != 132 && packetModel.Opcode != 3)
		{
			_ = packetModel.Opcode;
		}
		ReadPacket(packetModel);
	}

	public void ReadPacket(PacketModel Packet)
	{
		byte[] withEndData = Packet.WithEndData;
		byte[] withoutEndData = Packet.WithoutEndData;
		SyncClientPacket syncClientPacket = new SyncClientPacket(withEndData);
		int bytes = withoutEndData.Length;
		try
		{
			uint num = 0u;
			int num2 = 0;
			RoomModel roomModel = null;
			switch (Packet.Opcode)
			{
			case 65:
			{
				string udp = $"{syncClientPacket.ReadH()}.{syncClientPacket.ReadH()}";
				uint uniqueRoomId2 = syncClientPacket.ReadUD();
				num = syncClientPacket.ReadUD();
				num2 = syncClientPacket.ReadC();
				roomModel = RoomsManager.CreateOrGetRoom(uniqueRoomId2, num);
				if (roomModel == null)
				{
					break;
				}
				PlayerModel playerModel2 = roomModel.AddPlayer(ipendPoint_0, Packet, udp);
				if (playerModel2 != null)
				{
					if (!playerModel2.Integrity)
					{
						playerModel2.ResetBattleInfos();
					}
					byte[] cODE = PROTOCOL_CONNECT.GET_CODE();
					SendPacket(cODE, playerModel2.Client);
					if (ConfigLoader.IsTestMode)
					{
						CLogger.Print($"Player Connected. [{playerModel2.Client.Address}:{playerModel2.Client.Port}]", LoggerType.Warning);
					}
				}
				break;
			}
			case 4:
			{
				syncClientPacket.Advance(bytes);
				uint uniqueRoomId6 = syncClientPacket.ReadUD();
				num2 = syncClientPacket.ReadC();
				num = syncClientPacket.ReadUD();
				roomModel = RoomsManager.GetRoom(uniqueRoomId6, num);
				if (roomModel == null)
				{
					break;
				}
				PlayerModel player5 = roomModel.GetPlayer(Packet.Slot, ipendPoint_0);
				if (player5 == null || player5.PlayerIdByServer != Packet.AccountId)
				{
					break;
				}
				player5.RespawnByUser = Packet.Respawn;
				roomModel.BotMode = true;
				if (roomModel.StartTime == default(DateTime))
				{
					break;
				}
				byte[] actions3 = WriteBotActionData(withoutEndData, roomModel);
				byte[] data4 = AllUtils.BaseWriteCode(4, actions3, Packet.Slot, AllUtils.GetDuration(player5.StartTime), Packet.Round, Packet.Respawn, Packet.RoundNumber, Packet.AccountId);
				PlayerModel[] players = roomModel.Players;
				foreach (PlayerModel playerModel4 in players)
				{
					bool flag = playerModel4.Slot != Packet.Slot;
					if (playerModel4.Client != null && player5.AccountIdIsValid() && num2 == 255 && flag)
					{
						SendPacket(data4, playerModel4.Client);
					}
				}
				break;
			}
			case 3:
			{
				syncClientPacket.Advance(bytes);
				uint uniqueRoomId7 = syncClientPacket.ReadUD();
				num2 = syncClientPacket.ReadC();
				num = syncClientPacket.ReadUD();
				roomModel = RoomsManager.GetRoom(uniqueRoomId7, num);
				if (roomModel == null)
				{
					break;
				}
				PlayerModel player6 = roomModel.GetPlayer(Packet.Slot, ipendPoint_0);
				if (player6 == null || player6.PlayerIdByServer != Packet.AccountId)
				{
					break;
				}
				player6.RespawnByUser = Packet.Respawn;
				if (roomModel.StartTime == default(DateTime))
				{
					break;
				}
				byte[] actions4 = (roomModel.BotMode ? WriteBotActionData(withoutEndData, roomModel) : WritePlayerActionData(withoutEndData, roomModel, AllUtils.GetDuration(player6.StartTime), Packet));
				byte[] data5 = AllUtils.BaseWriteCode(4, actions4, roomModel.BotMode ? Packet.Slot : 255, AllUtils.GetDuration(roomModel.StartTime), Packet.Round, Packet.Respawn, Packet.RoundNumber, Packet.AccountId);
				bool flag2 = !roomModel.BotMode && num2 != 255;
				PlayerModel[] players = roomModel.Players;
				foreach (PlayerModel playerModel5 in players)
				{
					bool flag3 = playerModel5.Slot != Packet.Slot;
					if (playerModel5.Client != null && player6.AccountIdIsValid() && ((num2 == 255 && flag3) || (roomModel.BotMode && flag3) || flag2))
					{
						SendPacket(data5, playerModel5.Client);
					}
				}
				break;
			}
			case 97:
			{
				uint uniqueRoomId3 = syncClientPacket.ReadUD();
				num2 = syncClientPacket.ReadC();
				num = syncClientPacket.ReadUD();
				roomModel = RoomsManager.GetRoom(uniqueRoomId3, num);
				byte[] data2 = Packet.Data;
				if (roomModel == null)
				{
					break;
				}
				PlayerModel player3 = roomModel.GetPlayer(Packet.Slot, ipendPoint_0);
				if (player3 != null)
				{
					player3.LastPing = Packet.ReceiveDate;
					SendPacket(data2, ipendPoint_0);
					if (ConfigLoader.SendPingSync)
					{
						player3.Latency = AllUtils.PingTime($"{ipendPoint_0.Address}", data2, socket_0.Ttl, 120, IsFragmented: false, out var Ping);
						player3.Ping = Ping;
						SendMatchInfo.SendPingSync(roomModel, player3);
					}
				}
				break;
			}
			case 67:
			{
				string udp2 = $"{syncClientPacket.ReadH()}.{syncClientPacket.ReadH()}";
				uint uniqueRoomId4 = syncClientPacket.ReadUD();
				num = syncClientPacket.ReadUD();
				num2 = syncClientPacket.ReadC();
				roomModel = RoomsManager.GetRoom(uniqueRoomId4, num);
				if (roomModel != null)
				{
					if (roomModel.RemovePlayer(ipendPoint_0, Packet, udp2) && ConfigLoader.IsTestMode)
					{
						CLogger.Print($"Player Disconnected. [{ipendPoint_0.Address}:{ipendPoint_0.Port}]", LoggerType.Warning);
					}
					if (roomModel.GetPlayersCount() == 0)
					{
						RoomsManager.RemoveRoom(roomModel.UniqueRoomId, roomModel.RoomSeed);
					}
				}
				break;
			}
			default:
				CLogger.Print(Bitwise.ToHexData($"Opcode Not Found: [{Packet.Opcode}]", withEndData), LoggerType.Opcode);
				break;
			case 132:
			{
				syncClientPacket.Advance(bytes);
				uint uniqueRoomId5 = syncClientPacket.ReadUD();
				num2 = syncClientPacket.ReadC();
				num = syncClientPacket.ReadUD();
				roomModel = RoomsManager.GetRoom(uniqueRoomId5, num);
				if (roomModel == null)
				{
					break;
				}
				PlayerModel player4 = roomModel.GetPlayer(Packet.Slot, ipendPoint_0);
				if (player4 == null || player4.PlayerIdByServer != Packet.AccountId)
				{
					break;
				}
				roomModel.BotMode = true;
				byte[] actions2 = PROTOCOL_BOTS_ACTION.GET_CODE(withoutEndData);
				byte[] data3 = AllUtils.BaseWriteCode(132, actions2, Packet.Slot, AllUtils.GetDuration(player4.StartTime), Packet.Round, Packet.Respawn, Packet.RoundNumber, Packet.AccountId);
				PlayerModel[] players = roomModel.Players;
				foreach (PlayerModel playerModel3 in players)
				{
					if (playerModel3.Client != null && player4.AccountIdIsValid() && playerModel3.Slot != Packet.Slot)
					{
						SendPacket(data3, playerModel3.Client);
					}
				}
				break;
			}
			case 131:
			{
				syncClientPacket.Advance(bytes);
				uint uniqueRoomId = syncClientPacket.ReadUD();
				num2 = syncClientPacket.ReadC();
				num = syncClientPacket.ReadUD();
				roomModel = RoomsManager.GetRoom(uniqueRoomId, num);
				if (roomModel == null)
				{
					break;
				}
				PlayerModel player = roomModel.GetPlayer(Packet.Slot, ipendPoint_0);
				if (player == null || player.PlayerIdByServer != Packet.AccountId)
				{
					break;
				}
				roomModel.BotMode = true;
				PlayerModel player2 = roomModel.GetPlayer(num2, Active: false);
				byte[] actions = PROTOCOL_BOTS_ACTION.GET_CODE(withoutEndData);
				byte[] data = AllUtils.BaseWriteCode(132, actions, num2, AllUtils.GetDuration(player2.StartTime), Packet.Round, Packet.Respawn, Packet.RoundNumber, Packet.AccountId);
				PlayerModel[] players = roomModel.Players;
				foreach (PlayerModel playerModel in players)
				{
					if (playerModel.Client != null && player.AccountIdIsValid() && playerModel.Slot != Packet.Slot)
					{
						SendPacket(data, playerModel.Client);
					}
				}
				break;
			}
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

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
		SyncClientPacket c = new SyncClientPacket(data);
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		uint num = 0u;
		PlayerModel player = roomModel_0.GetPlayer(actionModel_0.Slot, Active: false);
		if (actionModel_0.Flag.HasFlag(UdpGameEvent.ActionState))
		{
			num += 256;
			ActionStateInfo actionStateInfo = ActionState.ReadInfo(actionModel_0, c, GenLog: false);
			if (!roomModel_0.BotMode)
			{
				Equipment equip = player.Equip;
				if (player != null && equip != null)
				{
					int weaponId = 0;
					byte b = 0;
					byte extensions = 0;
					if (actionStateInfo.Flag.HasFlag(WeaponSyncType.Primary))
					{
						weaponId = equip.WpnPrimary;
						int ıdStatics = ComDiv.GetIdStatics(equip.Accessory, 1);
						int ıdStatics2 = ComDiv.GetIdStatics(equip.Accessory, 3);
						b = (byte)((ıdStatics == 30) ? ıdStatics2 : b);
					}
					if (actionStateInfo.Flag.HasFlag(WeaponSyncType.Secondary))
					{
						weaponId = equip.WpnSecondary;
					}
					if (actionStateInfo.Flag.HasFlag(WeaponSyncType.Melee))
					{
						weaponId = equip.WpnMelee;
					}
					if (actionStateInfo.Flag.HasFlag(WeaponSyncType.Explosive))
					{
						weaponId = equip.WpnExplosive;
					}
					if (actionStateInfo.Flag.HasFlag(WeaponSyncType.Special))
					{
						weaponId = equip.WpnSpecial;
					}
					if (actionStateInfo.Flag.HasFlag(WeaponSyncType.Mission) && roomModel_0.RoomType == RoomCondition.Bomb)
					{
						weaponId = 5009001;
					}
					if (actionStateInfo.Flag.HasFlag(WeaponSyncType.Dual))
					{
						extensions = 16;
						if (actionStateInfo.Action.HasFlag(ActionFlag.Unk2048))
						{
							weaponId = equip.WpnPrimary;
						}
						if (actionStateInfo.Action.HasFlag(ActionFlag.Unk4096))
						{
							weaponId = equip.WpnPrimary;
						}
					}
					if (actionStateInfo.Flag.HasFlag(WeaponSyncType.Ext))
					{
						extensions = 16;
						if (actionStateInfo.Action.HasFlag(ActionFlag.Unk2048))
						{
							weaponId = equip.WpnSecondary;
						}
						if (actionStateInfo.Action.HasFlag(ActionFlag.Unk4096))
						{
							weaponId = equip.WpnSecondary;
						}
					}
					ObjectHitInfo item = new ObjectHitInfo(6)
					{
						ObjId = player.Slot,
						WeaponId = weaponId,
						Accessory = b,
						Extensions = extensions
					};
					list.Add(item);
				}
			}
			ActionState.WriteInfo(syncServerPacket, actionStateInfo);
			actionStateInfo = null;
		}
		if (actionModel_0.Flag.HasFlag(UdpGameEvent.Animation))
		{
			num += 2;
			AnimationInfo ınfo = Animation.ReadInfo(actionModel_0, c, GenLog: false);
			Animation.WriteInfo(syncServerPacket, ınfo);
			ınfo = null;
		}
		if (actionModel_0.Flag.HasFlag(UdpGameEvent.PosRotation))
		{
			num += 134217728;
			PosRotationInfo posRotationInfo = PosRotation.ReadInfo(actionModel_0, c, GenLog: false);
			if (player != null)
			{
				player.Position = new Half3(posRotationInfo.RotationX, posRotationInfo.RotationY, posRotationInfo.RotationZ);
			}
			actionModel_0.Flag |= UdpGameEvent.SoundPosRotation;
			PosRotation.WriteInfo(syncServerPacket, posRotationInfo);
			posRotationInfo = null;
		}
		if (actionModel_0.Flag.HasFlag(UdpGameEvent.SoundPosRotation))
		{
			num += 8388608;
			SoundPosRotationInfo ınfo2 = SoundPosRotation.ReadInfo(actionModel_0, c, float_0, GenLog: false);
			SoundPosRotation.WriteInfo(syncServerPacket, ınfo2);
			ınfo2 = null;
		}
		if (actionModel_0.Flag.HasFlag(UdpGameEvent.UseObject))
		{
			num += 4;
			List<UseObjectInfo> list2 = UseObject.ReadInfo(actionModel_0, c, GenLog: false);
			for (int i = 0; i < list2.Count; i++)
			{
				UseObjectInfo useObjectInfo = list2[i];
				if (!roomModel_0.BotMode && useObjectInfo.ObjectId != ushort.MaxValue)
				{
					ObjectInfo @object = roomModel_0.GetObject(useObjectInfo.ObjectId);
					if (@object == null)
					{
						continue;
					}
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
						ObjectHitInfo item2 = new ObjectHitInfo(3)
						{
							ObjSyncId = 1,
							ObjId = @object.Id,
							ObjLife = @object.Life,
							AnimId1 = 255,
							AnimId2 = ((@object.Animation != null) ? @object.Animation.Id : 255),
							SpecialUse = AllUtils.GetDuration(@object.UseDate)
						};
						list.Add(item2);
					}
				}
				else
				{
					AllUtils.RemoveHit(list2, i--);
				}
			}
			UseObject.WriteInfo(syncServerPacket, list2);
			list2 = null;
		}
		if (actionModel_0.Flag.HasFlag(UdpGameEvent.ActionForObjectSync))
		{
			num += 16;
			ActionObjectInfo ınfo3 = ActionForObjectSync.ReadInfo(actionModel_0, c, GenLog: false);
			if (player != null)
			{
				roomModel_0.SyncInfo(list, 1);
			}
			ActionForObjectSync.WriteInfo(syncServerPacket, ınfo3);
			ınfo3 = null;
		}
		if (actionModel_0.Flag.HasFlag(UdpGameEvent.RadioChat))
		{
			num += 32;
			RadioChatInfo ınfo4 = RadioChat.ReadInfo(actionModel_0, c, GenLog: false);
			RadioChat.WriteInfo(syncServerPacket, ınfo4);
			ınfo4 = null;
		}
		if (actionModel_0.Flag.HasFlag(UdpGameEvent.WeaponSync))
		{
			num += 67108864;
			WeaponSyncInfo weaponSyncInfo = WeaponSync.ReadInfo(actionModel_0, c, GenLog: false);
			if (player != null)
			{
				player.WeaponId = weaponSyncInfo.WeaponId;
				player.Accessory = weaponSyncInfo.Accessory;
				player.Extensions = weaponSyncInfo.Extensions;
				player.WeaponClass = weaponSyncInfo.WeaponClass;
				roomModel_0.SyncInfo(list, 2);
			}
			WeaponSync.WriteInfo(syncServerPacket, weaponSyncInfo);
			weaponSyncInfo = null;
		}
		if (actionModel_0.Flag.HasFlag(UdpGameEvent.WeaponRecoil))
		{
			num += 128;
			WeaponRecoilInfo ınfo5 = WeaponRecoil.ReadInfo(actionModel_0, c, GenLog: false);
			WeaponRecoil.WriteInfo(syncServerPacket, ınfo5);
			ınfo5 = null;
		}
		if (actionModel_0.Flag.HasFlag(UdpGameEvent.HpSync))
		{
			num += 8;
			HPSyncInfo ınfo6 = HpSync.ReadInfo(actionModel_0, c, GenLog: false);
			HpSync.WriteInfo(syncServerPacket, ınfo6);
			ınfo6 = null;
		}
		if (actionModel_0.Flag.HasFlag(UdpGameEvent.Suicide))
		{
			num += 1048576;
			List<SuicideInfo> list3 = Suicide.ReadInfo(actionModel_0, c, GenLog: false);
			List<DeathServerData> list4 = new List<DeathServerData>();
			if (player != null)
			{
				int num2 = -1;
				int weaponId2 = 0;
				for (int j = 0; j < list3.Count; j++)
				{
					SuicideInfo suicideInfo = list3[j];
					if (!player.Dead && player.Life > 0)
					{
						int hitDamageBot = AllUtils.GetHitDamageBot(suicideInfo.HitInfo);
						int killerId = AllUtils.GetKillerId(suicideInfo.HitInfo);
						int objectType = AllUtils.GetObjectType(suicideInfo.HitInfo);
						CharaHitPart hitPart = (CharaHitPart)((int)(suicideInfo.HitInfo >> 4) & 0x3F);
						CharaDeath charaDeath = AllUtils.GetCharaDeath(suicideInfo.HitInfo);
						if (objectType == 1 || objectType == 0)
						{
							num2 = killerId;
						}
						weaponId2 = suicideInfo.WeaponId;
						DamageManager.SimpleDeath(roomModel_0, list4, list, player, player, hitDamageBot, weaponId2, hitPart, charaDeath);
						if (hitDamageBot > 0)
						{
							if (ConfigLoader.UseHitMarker)
							{
								SendMatchInfo.SendHitMarkerSync(roomModel_0, player, charaDeath, HitType.Normal, hitDamageBot);
							}
							ObjectHitInfo item3 = new ObjectHitInfo(2)
							{
								ObjId = player.Slot,
								ObjLife = player.Life,
								HitPart = hitPart,
								KillerSlot = num2,
								Position = suicideInfo.PlayerPos,
								DeathType = charaDeath,
								WeaponId = weaponId2
							};
							list.Add(item3);
						}
					}
					else
					{
						AllUtils.RemoveHit(list3, j--);
					}
				}
				if (list4.Count > 0)
				{
					SendMatchInfo.SendDeathSync(roomModel_0, player, num2, weaponId2, list4);
				}
				list4 = null;
			}
			else
			{
				list3 = new List<SuicideInfo>();
			}
			Suicide.WriteInfo(syncServerPacket, list3);
			list4 = null;
			list3 = null;
		}
		if (actionModel_0.Flag.HasFlag(UdpGameEvent.MissionData))
		{
			num += 2048;
			MissionDataInfo missionDataInfo = MissionData.ReadInfo(actionModel_0, c, float_0, GenLog: false);
			if (roomModel_0.Map != null && player != null && !player.Dead && missionDataInfo.PlantTime > 0f && !missionDataInfo.BombEnum.HasFlag(BombFlag.Stop))
			{
				BombPosition bomb = roomModel_0.Map.GetBomb(missionDataInfo.BombId);
				if (bomb != null)
				{
					bool flag2;
					Vector3 value = ((flag2 = missionDataInfo.BombEnum.HasFlag(BombFlag.Defuse)) ? roomModel_0.BombPosition : (missionDataInfo.BombEnum.HasFlag(BombFlag.Start) ? bomb.Position : new Half3(0, 0, 0)));
					double num3 = Vector3.Distance(player.Position, value);
					TeamEnum swappedTeam = AllUtils.GetSwappedTeam(player, roomModel_0);
					if ((bomb.EveryWhere || num3 <= 2.0) && ((swappedTeam == TeamEnum.CT_TEAM && flag2) || (swappedTeam == TeamEnum.FR_TEAM && !flag2)))
					{
						if (player.C4Time != missionDataInfo.PlantTime)
						{
							player.C4First = DateTimeUtil.Now();
							player.C4Time = missionDataInfo.PlantTime;
						}
						double duration = ComDiv.GetDuration(player.C4First);
						float num4 = (flag2 ? player.DefuseDuration : player.PlantDuration);
						if ((float_0 >= missionDataInfo.PlantTime + num4 || duration >= (double)num4) && ((!roomModel_0.HasC4 && missionDataInfo.BombEnum.HasFlag(BombFlag.Start)) || (roomModel_0.HasC4 && flag2)))
						{
							roomModel_0.HasC4 = !roomModel_0.HasC4;
							missionDataInfo.Bomb |= 2;
							SendMatchInfo.SendBombSync(roomModel_0, player, missionDataInfo.BombEnum.HasFlag(BombFlag.Defuse) ? 1 : 0, missionDataInfo.BombId);
						}
					}
				}
			}
			MissionData.WriteInfo(syncServerPacket, missionDataInfo);
			missionDataInfo = null;
		}
		if (actionModel_0.Flag.HasFlag(UdpGameEvent.DropWeapon))
		{
			num += 4194304;
			DropWeaponInfo dropWeaponInfo = DropWeapon.ReadInfo(actionModel_0, c, GenLog: false);
			if (!roomModel_0.BotMode)
			{
				roomModel_0.DropCounter++;
				if (roomModel_0.DropCounter > ConfigLoader.MaxDropWpnCount)
				{
					roomModel_0.DropCounter = 0;
				}
				dropWeaponInfo.Counter = roomModel_0.DropCounter;
				Equipment equip2 = player.Equip;
				if (player != null && equip2 != null)
				{
					int ıdStatics3 = ComDiv.GetIdStatics(dropWeaponInfo.WeaponId, 1);
					if (ıdStatics3 == 1)
					{
						equip2.WpnPrimary = 0;
					}
					if (ıdStatics3 == 2)
					{
						equip2.WpnSecondary = 0;
					}
				}
			}
			DropWeapon.WriteInfo(syncServerPacket, dropWeaponInfo);
			dropWeaponInfo = null;
		}
		if (actionModel_0.Flag.HasFlag(UdpGameEvent.GetWeaponForClient))
		{
			num += 16777216;
			WeaponClient weaponClient = GetWeaponForClient.ReadInfo(actionModel_0, c, GenLog: false);
			if (!roomModel_0.BotMode)
			{
				Equipment equip3 = player.Equip;
				if (player != null && equip3 != null)
				{
					int ıdStatics4 = ComDiv.GetIdStatics(weaponClient.WeaponId, 1);
					if (ıdStatics4 == 1)
					{
						equip3.WpnPrimary = weaponClient.WeaponId;
					}
					if (ıdStatics4 == 2)
					{
						equip3.WpnSecondary = weaponClient.WeaponId;
					}
				}
			}
			GetWeaponForClient.WriteInfo(syncServerPacket, weaponClient);
			weaponClient = null;
		}
		if (actionModel_0.Flag.HasFlag(UdpGameEvent.FireData))
		{
			num += 33554432;
			FireDataInfo ınfo7 = FireData.ReadInfo(actionModel_0, c, GenLog: false);
			FireData.WriteInfo(syncServerPacket, ınfo7);
			ınfo7 = null;
		}
		if (actionModel_0.Flag.HasFlag(UdpGameEvent.CharaFireNHitData))
		{
			num += 1024;
			List<CharaFireNHitDataInfo> hits = CharaFireNHitData.ReadInfo(actionModel_0, c, GenLog: false);
			CharaFireNHitData.WriteInfo(syncServerPacket, hits);
			hits = null;
		}
		if (actionModel_0.Flag.HasFlag(UdpGameEvent.HitData))
		{
			num += 131072;
			List<HitDataInfo> list5 = HitData.ReadInfo(actionModel_0, c, genLog: false);
			List<DeathServerData> list6 = new List<DeathServerData>();
			if (player != null)
			{
				int weaponId3 = 0;
				for (int k = 0; k < list5.Count; k++)
				{
					HitDataInfo hitDataInfo = list5[k];
					if (hitDataInfo.HitEnum == HitType.HelmetProtection || hitDataInfo.HitEnum == HitType.HeadshotProtection)
					{
						continue;
					}
					if (AllUtils.ValidateHitData(AllUtils.GetHitDamageNormal(hitDataInfo.HitIndex), hitDataInfo, out var Damage))
					{
						int objectId = hitDataInfo.ObjectId;
						CharaHitPart hitPart2 = AllUtils.GetHitPart(hitDataInfo.HitIndex);
						CharaDeath charaDeath2 = CharaDeath.DEFAULT;
						weaponId3 = hitDataInfo.WeaponId;
						ObjectType hitType = AllUtils.GetHitType(hitDataInfo.HitIndex);
						switch (hitType)
						{
						case ObjectType.Object:
						{
							ObjectInfo object2 = roomModel_0.GetObject(objectId);
							ObjectModel objectModel = object2?.Model;
							if (objectModel != null && objectModel.Destroyable)
							{
								if (object2.Life > 0)
								{
									object2.Life -= Damage;
									if (object2.Life <= 0)
									{
										object2.Life = 0;
										DamageManager.BoomDeath(roomModel_0, player, Damage, weaponId3, list6, list, hitDataInfo.BoomPlayers, CharaHitPart.ALL, CharaDeath.OBJECT_EXPLOSION);
									}
									object2.DestroyState = objectModel.CheckDestroyState(object2.Life);
									DamageManager.SabotageDestroy(roomModel_0, player, objectModel, object2, Damage);
									float duration2 = AllUtils.GetDuration(object2.UseDate);
									if (object2.Animation != null && object2.Animation.Duration > 0f && duration2 >= object2.Animation.Duration)
									{
										object2.Model.GetAnim(object2.Animation.NextAnim, duration2, object2.Animation.Duration, object2);
									}
									ObjectHitInfo item5 = new ObjectHitInfo(objectModel.UpdateId)
									{
										ObjId = object2.Id,
										ObjLife = object2.Life,
										KillerSlot = actionModel_0.Slot,
										ObjSyncId = (objectModel.NeedSync ? 1 : 0),
										SpecialUse = duration2,
										AnimId1 = objectModel.Animation,
										AnimId2 = ((object2.Animation != null) ? object2.Animation.Id : 255),
										DestroyState = object2.DestroyState
									};
									list.Add(item5);
								}
							}
							else if (ConfigLoader.SendFailMsg && objectModel == null)
							{
								CLogger.Print($"Fire Obj: {objectId} Map: {roomModel_0.MapId} Invalid Object.", LoggerType.Warning);
								player.LogPlayerPos(hitDataInfo.EndBullet);
							}
							break;
						}
						case ObjectType.User:
						{
							if (roomModel_0.GetPlayer(objectId, out var Player) && player.RespawnIsValid() && !player.Dead && !Player.Dead && !Player.Immortal)
							{
								if (hitPart2 == CharaHitPart.HEAD)
								{
									charaDeath2 = CharaDeath.HEADSHOT;
								}
								if (roomModel_0.RoomType == RoomCondition.DeathMatch && roomModel_0.Rule == MapRules.HeadHunter && charaDeath2 != CharaDeath.HEADSHOT)
								{
									Damage = 1;
								}
								else if (roomModel_0.RoomType == RoomCondition.Boss && charaDeath2 == CharaDeath.HEADSHOT)
								{
									if ((roomModel_0.LastRound == 1 && Player.Team == TeamEnum.FR_TEAM) || (roomModel_0.LastRound == 2 && Player.Team == TeamEnum.CT_TEAM))
									{
										Damage /= 10;
									}
								}
								else if (roomModel_0.RoomType == RoomCondition.DeathMatch && roomModel_0.Rule == MapRules.Chaos)
								{
									Damage = 200;
								}
								DamageManager.SimpleDeath(roomModel_0, list6, list, player, Player, Damage, weaponId3, hitPart2, charaDeath2);
								if (Damage > 0)
								{
									if (ConfigLoader.UseHitMarker)
									{
										SendMatchInfo.SendHitMarkerSync(roomModel_0, player, charaDeath2, hitDataInfo.HitEnum, Damage);
									}
									ObjectHitInfo item4 = new ObjectHitInfo(2)
									{
										ObjId = Player.Slot,
										ObjLife = Player.Life,
										HitPart = hitPart2,
										KillerSlot = player.Slot,
										Position = (Vector3)Player.Position - (Vector3)player.Position,
										DeathType = charaDeath2,
										WeaponId = weaponId3
									};
									list.Add(item4);
								}
							}
							else
							{
								AllUtils.RemoveHit(list5, k--);
							}
							break;
						}
						default:
							CLogger.Print($"HitType: ({hitType}/{(int)hitType}) Slot: {actionModel_0.Slot}", LoggerType.Warning);
							CLogger.Print($"BoomPlayers: {hitDataInfo.BoomInfo} {hitDataInfo.BoomPlayers.Count}", LoggerType.Warning);
							break;
						case ObjectType.UserObject:
							break;
						}
					}
					else
					{
						AllUtils.RemoveHit(list5, k--);
					}
				}
				if (list6.Count > 0)
				{
					SendMatchInfo.SendDeathSync(roomModel_0, player, 255, weaponId3, list6);
				}
			}
			else
			{
				list5 = new List<HitDataInfo>();
			}
			HitData.WriteInfo(syncServerPacket, list5);
			list6 = null;
			list5 = null;
		}
		if (actionModel_0.Flag.HasFlag(UdpGameEvent.GrenadeHit))
		{
			num += 268435456;
			List<GrenadeHitInfo> list7 = GrenadeHit.ReadInfo(actionModel_0, c, GenLog: false);
			List<DeathServerData> list8 = new List<DeathServerData>();
			if (player != null)
			{
				int num5 = -1;
				int weaponId4 = 0;
				for (int l = 0; l < list7.Count; l++)
				{
					GrenadeHitInfo grenadeHitInfo = list7[l];
					if (AllUtils.ValidateGrenadeHit(AllUtils.GetHitDamageNormal(grenadeHitInfo.HitInfo), grenadeHitInfo, out var Damage2))
					{
						int objectId2 = grenadeHitInfo.ObjectId;
						CharaHitPart hitPart3 = AllUtils.GetHitPart(grenadeHitInfo.HitInfo);
						weaponId4 = grenadeHitInfo.WeaponId;
						ObjectType hitType2 = AllUtils.GetHitType(grenadeHitInfo.HitInfo);
						switch (hitType2)
						{
						case ObjectType.Object:
						{
							ObjectInfo object3 = roomModel_0.GetObject(objectId2);
							ObjectModel objectModel2 = object3?.Model;
							if (objectModel2 != null && objectModel2.Destroyable && object3.Life > 0)
							{
								object3.Life -= Damage2;
								if (object3.Life <= 0)
								{
									object3.Life = 0;
									DamageManager.BoomDeath(roomModel_0, player, Damage2, weaponId4, list8, list, grenadeHitInfo.BoomPlayers, CharaHitPart.ALL, CharaDeath.OBJECT_EXPLOSION);
								}
								object3.DestroyState = objectModel2.CheckDestroyState(object3.Life);
								DamageManager.SabotageDestroy(roomModel_0, player, objectModel2, object3, Damage2);
								if (Damage2 > 0)
								{
									ObjectHitInfo item7 = new ObjectHitInfo(objectModel2.UpdateId)
									{
										ObjId = object3.Id,
										ObjLife = object3.Life,
										KillerSlot = actionModel_0.Slot,
										ObjSyncId = (objectModel2.NeedSync ? 1 : 0),
										AnimId1 = objectModel2.Animation,
										AnimId2 = ((object3.Animation != null) ? object3.Animation.Id : 255),
										DestroyState = object3.DestroyState,
										SpecialUse = AllUtils.GetDuration(object3.UseDate)
									};
									list.Add(item7);
								}
							}
							else if (ConfigLoader.SendFailMsg && objectModel2 == null)
							{
								CLogger.Print($"Boom Obj: {objectId2} Map: {roomModel_0.MapId} Invalid Object.", LoggerType.Warning);
								player.LogPlayerPos(grenadeHitInfo.HitPos);
							}
							break;
						}
						case ObjectType.User:
						{
							num5++;
							if (Damage2 > 0 && roomModel_0.GetPlayer(objectId2, out var Player2) && player.RespawnIsValid() && !Player2.Dead && !Player2.Immortal)
							{
								TeamEnum teamEnum = ((num5 % 2 != 0) ? TeamEnum.CT_TEAM : TeamEnum.FR_TEAM);
								if (grenadeHitInfo.DeathType == CharaDeath.MEDICAL_KIT)
								{
									Player2.Life += Damage2;
									Player2.CheckLifeValue();
								}
								else if (grenadeHitInfo.DeathType == CharaDeath.BOOM && ClassType.Dino != grenadeHitInfo.WeaponClass && (teamEnum == TeamEnum.FR_TEAM || teamEnum == TeamEnum.CT_TEAM))
								{
									Damage2 = (int)Math.Ceiling((double)Damage2 / 2.7);
									DamageManager.SimpleDeath(roomModel_0, list8, list, player, Player2, Damage2, weaponId4, hitPart3, grenadeHitInfo.DeathType);
								}
								else
								{
									DamageManager.SimpleDeath(roomModel_0, list8, list, player, Player2, Damage2, weaponId4, hitPart3, grenadeHitInfo.DeathType);
								}
								if (Damage2 > 0)
								{
									if (ConfigLoader.UseHitMarker)
									{
										SendMatchInfo.SendHitMarkerSync(roomModel_0, player, grenadeHitInfo.DeathType, grenadeHitInfo.HitEnum, Damage2);
									}
									ObjectHitInfo item6 = new ObjectHitInfo(2)
									{
										ObjId = Player2.Slot,
										ObjLife = Player2.Life,
										HitPart = hitPart3,
										KillerSlot = player.Slot,
										Position = (Vector3)Player2.Position - (Vector3)player.Position,
										DeathType = grenadeHitInfo.DeathType,
										WeaponId = weaponId4
									};
									list.Add(item6);
								}
							}
							else
							{
								AllUtils.RemoveHit(list7, l--);
							}
							break;
						}
						default:
							CLogger.Print($"Grenade Boom, HitType: ({hitType2}/{(int)hitType2})", LoggerType.Warning);
							break;
						case ObjectType.UserObject:
							break;
						}
					}
					else
					{
						AllUtils.RemoveHit(list7, l--);
					}
				}
				if (list8.Count > 0)
				{
					SendMatchInfo.SendDeathSync(roomModel_0, player, 255, weaponId4, list8);
				}
			}
			else
			{
				list7 = new List<GrenadeHitInfo>();
			}
			GrenadeHit.WriteInfo(syncServerPacket, list7);
			list8 = null;
			list7 = null;
		}
		if (actionModel_0.Flag.HasFlag(UdpGameEvent.GetWeaponForHost))
		{
			num += 512;
			WeaponHost ınfo8 = GetWeaponForHost.ReadInfo(actionModel_0, c, GenLog: false);
			GetWeaponForHost.WriteInfo(syncServerPacket, ınfo8);
			ınfo8 = null;
		}
		if (actionModel_0.Flag.HasFlag(UdpGameEvent.FireDataOnObject))
		{
			num += 1073741824;
			FireDataObjectInfo ınfo9 = FireDataOnObject.ReadInfo(actionModel_0, c, GenLog: false);
			FireDataOnObject.WriteInfo(syncServerPacket, ınfo9);
			ınfo9 = null;
		}
		if (actionModel_0.Flag.HasFlag(UdpGameEvent.FireNHitDataOnObject))
		{
			num += 8192;
			FireNHitDataObjectInfo fireNHitDataObjectInfo = FireNHitDataOnObject.ReadInfo(actionModel_0, c, GenLog: false);
			if (player != null && !player.Dead)
			{
				SendMatchInfo.SendPortalPass(roomModel_0, player, fireNHitDataObjectInfo.Portal);
			}
			FireNHitDataOnObject.WriteInfo(syncServerPacket, fireNHitDataObjectInfo);
			fireNHitDataObjectInfo = null;
		}
		if (actionModel_0.Flag.HasFlag(UdpGameEvent.SeizeDataForClient))
		{
			num += 32768;
			SeizeDataForClientInfo ınfo10 = SeizeDataForClient.ReadInfo(actionModel_0, c, GenLog: true);
			if (player == null)
			{
			}
			SeizeDataForClient.WriteInfo(syncServerPacket, ınfo10);
			ınfo10 = null;
		}
		byte_0 = syncServerPacket.ToArray();
		if (num != (uint)actionModel_0.Flag && ConfigLoader.IsTestMode)
		{
			CLogger.Print(Bitwise.ToHexData($"PVP - Missing Flag Events: '{(uint)actionModel_0.Flag}' | '{(uint)(actionModel_0.Flag - num)}'", data), LoggerType.Opcode);
		}
		return list;
	}

	public byte[] WritePlayerActionData(byte[] Data, RoomModel Room, float Time, PacketModel Packet)
	{
		SyncClientPacket syncClientPacket = new SyncClientPacket(Data);
		List<ObjectHitInfo> list = new List<ObjectHitInfo>();
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		for (int i = 0; i < 18; i++)
		{
			ActionModel actionModel = new ActionModel();
			try
			{
				bool flag = false;
				actionModel.Length = syncClientPacket.ReadUH(out var Exception);
				if (Exception)
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
				case UdpSubHead.Grenade:
				{
					GrenadeInfo ınfo6 = GrenadeSync.ReadInfo(syncClientPacket, GenLog: false);
					GrenadeSync.WriteInfo(syncServerPacket, ınfo6);
					ınfo6 = null;
					break;
				}
				case UdpSubHead.DroppedWeapon:
				{
					DropedWeaponInfo ınfo5 = DropedWeapon.ReadInfo(syncClientPacket, GenLog: false);
					DropedWeapon.WriteInfo(syncServerPacket, ınfo5);
					ınfo5 = null;
					break;
				}
				case UdpSubHead.ObjectStatic:
				{
					ObjectStaticInfo ınfo3 = ObjectStatic.ReadInfo(syncClientPacket, GenLog: false);
					ObjectStatic.WriteInfo(syncServerPacket, ınfo3);
					ınfo3 = null;
					break;
				}
				case UdpSubHead.ObjectMove:
				{
					ObjectMoveInfo ınfo8 = ObjectMove.ReadInfo(syncClientPacket, GenLog: false);
					ObjectMove.WriteInfo(syncServerPacket, ınfo8);
					ınfo8 = null;
					break;
				}
				case UdpSubHead.ObjectAnim:
				{
					ObjectAnimInfo ınfo7 = ObjectAnim.ReadInfo(syncClientPacket, GenLog: false);
					ObjectAnim.WriteInfo(syncServerPacket, ınfo7);
					ınfo7 = null;
					break;
				}
				case UdpSubHead.User:
				case UdpSubHead.StageInfoChara:
				{
					actionModel.Flag = (UdpGameEvent)syncClientPacket.ReadUD();
					actionModel.Data = syncClientPacket.ReadB(actionModel.Length - 9);
					AllUtils.CheckDataFlags(actionModel, Packet);
					list.AddRange(method_0(actionModel, Room, Time, out var byte_));
					syncServerPacket.GoBack(5);
					syncServerPacket.WriteH((ushort)(byte_.Length + 9));
					syncServerPacket.WriteH(actionModel.Slot);
					syncServerPacket.WriteC((byte)actionModel.SubHead);
					syncServerPacket.WriteD((uint)actionModel.Flag);
					syncServerPacket.WriteB(byte_);
					if (actionModel.Data.Length == 0 && actionModel.Length - 9 != 0)
					{
						flag = true;
					}
					break;
				}
				case UdpSubHead.StageInfoObjectStatic:
				{
					StageStaticInfo ınfo4 = StageInfoObjStatic.ReadInfo(syncClientPacket, GenLog: false);
					StageInfoObjStatic.WriteInfo(syncServerPacket, ınfo4);
					ınfo4 = null;
					break;
				}
				default:
					if (ConfigLoader.IsTestMode)
					{
						CLogger.Print(Bitwise.ToHexData($"PVP Sub Head: '{actionModel.SubHead}' or '{(int)actionModel.SubHead}'", Data), LoggerType.Opcode);
					}
					break;
				case UdpSubHead.StageInfoObjectAnim:
				{
					StageAnimInfo ınfo2 = StageInfoObjAnim.ReadInfo(syncClientPacket, GenLog: false);
					StageInfoObjAnim.WriteInfo(syncServerPacket, ınfo2);
					ınfo2 = null;
					break;
				}
				case UdpSubHead.StageInfoObjectControl:
				{
					StageControlInfo ınfo = StageInfoObjControl.ReadInfo(syncClientPacket, GenLog: false);
					StageInfoObjControl.WriteInfo(syncServerPacket, ınfo);
					ınfo = null;
					break;
				}
				}
				if (flag)
				{
					break;
				}
				continue;
			}
			catch (Exception ex)
			{
				if (ConfigLoader.IsTestMode)
				{
					CLogger.Print($"PVP Action Data - Buffer (Length: {Data.Length}): | {ex.Message}", LoggerType.Error, ex);
				}
				list = new List<ObjectHitInfo>();
			}
			break;
		}
		if (list.Count > 0)
		{
			syncServerPacket.WriteB(PROTOCOL_EVENTS_ACTION.GET_CODE(list));
		}
		list = null;
		return syncServerPacket.ToArray();
	}

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
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		uint num = 0u;
		PlayerModel player = roomModel_0.GetPlayer(actionModel_0.Slot, Active: false);
		if (actionModel_0.Flag.HasFlag(UdpGameEvent.ActionState))
		{
			num += 256;
			ActionFlag value = (ActionFlag)syncClientPacket.ReadUH();
			byte value2 = syncClientPacket.ReadC();
			WeaponSyncType weaponSyncType = (WeaponSyncType)syncClientPacket.ReadC();
			syncServerPacket.WriteH((ushort)value);
			syncServerPacket.WriteC(value2);
			syncServerPacket.WriteC((byte)weaponSyncType);
		}
		if (actionModel_0.Flag.HasFlag(UdpGameEvent.Animation))
		{
			num += 2;
			ushort value3 = syncClientPacket.ReadUH();
			syncServerPacket.WriteH(value3);
		}
		if (actionModel_0.Flag.HasFlag(UdpGameEvent.PosRotation))
		{
			num += 134217728;
			ushort value4 = syncClientPacket.ReadUH();
			ushort value5 = syncClientPacket.ReadUH();
			ushort value6 = syncClientPacket.ReadUH();
			ushort num2 = syncClientPacket.ReadUH();
			ushort num3 = syncClientPacket.ReadUH();
			ushort num4 = syncClientPacket.ReadUH();
			if (player != null)
			{
				player.Position = new Half3(num3, num4, num2);
			}
			syncServerPacket.WriteH(value4);
			syncServerPacket.WriteH(value5);
			syncServerPacket.WriteH(value6);
			syncServerPacket.WriteH(num2);
			syncServerPacket.WriteH(num3);
			syncServerPacket.WriteH(num4);
		}
		if (actionModel_0.Flag.HasFlag(UdpGameEvent.SoundPosRotation))
		{
			num += 8388608;
			float value7 = syncClientPacket.ReadT();
			syncServerPacket.WriteT(value7);
		}
		if (actionModel_0.Flag.HasFlag(UdpGameEvent.UseObject))
		{
			num += 4;
			byte b = syncClientPacket.ReadC();
			syncServerPacket.WriteC(b);
			for (int i = 0; i < b; i++)
			{
				ushort value8 = syncClientPacket.ReadUH();
				byte value9 = syncClientPacket.ReadC();
				CharaMoves charaMoves = (CharaMoves)syncClientPacket.ReadC();
				syncServerPacket.WriteH(value8);
				syncServerPacket.WriteC(value9);
				syncServerPacket.WriteC((byte)charaMoves);
			}
		}
		if (actionModel_0.Flag.HasFlag(UdpGameEvent.ActionForObjectSync))
		{
			num += 16;
			byte value10 = syncClientPacket.ReadC();
			byte value11 = syncClientPacket.ReadC();
			if (player != null)
			{
				roomModel_0.SyncInfo(list, 1);
			}
			syncServerPacket.WriteC(value10);
			syncServerPacket.WriteC(value11);
		}
		if (actionModel_0.Flag.HasFlag(UdpGameEvent.RadioChat))
		{
			num += 32;
			byte value12 = syncClientPacket.ReadC();
			byte value13 = syncClientPacket.ReadC();
			syncServerPacket.WriteC(value12);
			syncServerPacket.WriteC(value13);
		}
		if (actionModel_0.Flag.HasFlag(UdpGameEvent.WeaponSync))
		{
			num += 67108864;
			int num5 = syncClientPacket.ReadD();
			byte b2 = syncClientPacket.ReadC();
			byte b3 = syncClientPacket.ReadC();
			if (player != null)
			{
				player.WeaponId = num5;
				player.Accessory = b2;
				player.Extensions = b3;
				player.WeaponClass = (ClassType)ComDiv.GetIdStatics(num5, 2);
				roomModel_0.SyncInfo(list, 2);
			}
			syncServerPacket.WriteD(num5);
			syncServerPacket.WriteC(b2);
			syncServerPacket.WriteC(b3);
		}
		if (actionModel_0.Flag.HasFlag(UdpGameEvent.WeaponRecoil))
		{
			num += 128;
			float value14 = syncClientPacket.ReadT();
			float value15 = syncClientPacket.ReadT();
			float value16 = syncClientPacket.ReadT();
			float value17 = syncClientPacket.ReadT();
			float value18 = syncClientPacket.ReadT();
			byte value19 = syncClientPacket.ReadC();
			int num6 = syncClientPacket.ReadD();
			byte b4 = syncClientPacket.ReadC();
			byte b5 = syncClientPacket.ReadC();
			CLogger.Print($"PVE (WeaponRecoil); Slot: {player.Slot}; Weapon Id: {num6}; Extensions: {b5}; Unknowns: {b4}", LoggerType.Warning);
			syncServerPacket.WriteT(value14);
			syncServerPacket.WriteT(value15);
			syncServerPacket.WriteT(value16);
			syncServerPacket.WriteT(value17);
			syncServerPacket.WriteT(value18);
			syncServerPacket.WriteC(value19);
			syncServerPacket.WriteD(num6);
			syncServerPacket.WriteC(b4);
			syncServerPacket.WriteC(b5);
		}
		if (actionModel_0.Flag.HasFlag(UdpGameEvent.HpSync))
		{
			num += 8;
			ushort value20 = syncClientPacket.ReadUH();
			syncServerPacket.WriteH(value20);
		}
		if (actionModel_0.Flag.HasFlag(UdpGameEvent.Suicide))
		{
			num += 1048576;
			byte b6 = syncClientPacket.ReadC();
			syncServerPacket.WriteC(b6);
			for (int j = 0; j < b6; j++)
			{
				Half3 half = syncClientPacket.ReadUHV();
				int value21 = syncClientPacket.ReadD();
				byte value22 = syncClientPacket.ReadC();
				byte value23 = syncClientPacket.ReadC();
				uint value24 = syncClientPacket.ReadUD();
				syncServerPacket.WriteHV(half);
				syncServerPacket.WriteD(value21);
				syncServerPacket.WriteC(value22);
				syncServerPacket.WriteC(value23);
				syncServerPacket.WriteD(value24);
			}
		}
		if (actionModel_0.Flag.HasFlag(UdpGameEvent.DropWeapon))
		{
			num += 4194304;
			ushort value25 = syncClientPacket.ReadUH();
			ushort value26 = syncClientPacket.ReadUH();
			ushort value27 = syncClientPacket.ReadUH();
			ushort value28 = syncClientPacket.ReadUH();
			ushort value29 = syncClientPacket.ReadUH();
			ushort value30 = syncClientPacket.ReadUH();
			byte value31 = syncClientPacket.ReadC();
			int value32 = syncClientPacket.ReadD();
			byte value33 = syncClientPacket.ReadC();
			byte value34 = syncClientPacket.ReadC();
			if (ConfigLoader.UseMaxAmmoInDrop)
			{
				syncServerPacket.WriteH(ushort.MaxValue);
				syncServerPacket.WriteH(value26);
				syncServerPacket.WriteH(10000);
			}
			else
			{
				syncServerPacket.WriteH(value25);
				syncServerPacket.WriteH(value26);
				syncServerPacket.WriteH(value27);
			}
			syncServerPacket.WriteH(value28);
			syncServerPacket.WriteH(value29);
			syncServerPacket.WriteH(value30);
			syncServerPacket.WriteC(value31);
			syncServerPacket.WriteD(value32);
			syncServerPacket.WriteC(value33);
			syncServerPacket.WriteC(value34);
		}
		if (actionModel_0.Flag.HasFlag(UdpGameEvent.GetWeaponForClient))
		{
			num += 16777216;
			ushort value35 = syncClientPacket.ReadUH();
			ushort value36 = syncClientPacket.ReadUH();
			ushort value37 = syncClientPacket.ReadUH();
			ushort value38 = syncClientPacket.ReadUH();
			ushort value39 = syncClientPacket.ReadUH();
			ushort value40 = syncClientPacket.ReadUH();
			byte value41 = syncClientPacket.ReadC();
			int value42 = syncClientPacket.ReadD();
			byte value43 = syncClientPacket.ReadC();
			byte value44 = syncClientPacket.ReadC();
			if (ConfigLoader.UseMaxAmmoInDrop)
			{
				syncServerPacket.WriteH(ushort.MaxValue);
				syncServerPacket.WriteH(value36);
				syncServerPacket.WriteH(10000);
			}
			else
			{
				syncServerPacket.WriteH(value35);
				syncServerPacket.WriteH(value36);
				syncServerPacket.WriteH(value37);
			}
			syncServerPacket.WriteH(value38);
			syncServerPacket.WriteH(value39);
			syncServerPacket.WriteH(value40);
			syncServerPacket.WriteC(value41);
			syncServerPacket.WriteD(value42);
			syncServerPacket.WriteC(value43);
			syncServerPacket.WriteC(value44);
		}
		if (actionModel_0.Flag.HasFlag(UdpGameEvent.FireData))
		{
			num += 33554432;
			byte value45 = syncClientPacket.ReadC();
			byte value46 = syncClientPacket.ReadC();
			short value47 = syncClientPacket.ReadH();
			int value48 = syncClientPacket.ReadD();
			byte value49 = syncClientPacket.ReadC();
			byte value50 = syncClientPacket.ReadC();
			ushort value51 = syncClientPacket.ReadUH();
			ushort value52 = syncClientPacket.ReadUH();
			ushort value53 = syncClientPacket.ReadUH();
			syncServerPacket.WriteC(value45);
			syncServerPacket.WriteC(value46);
			syncServerPacket.WriteH(value47);
			syncServerPacket.WriteD(value48);
			syncServerPacket.WriteC(value49);
			syncServerPacket.WriteC(value50);
			syncServerPacket.WriteH(value51);
			syncServerPacket.WriteH(value52);
			syncServerPacket.WriteH(value53);
		}
		if (actionModel_0.Flag.HasFlag(UdpGameEvent.CharaFireNHitData))
		{
			num += 1024;
			byte b7 = syncClientPacket.ReadC();
			syncServerPacket.WriteC(b7);
			for (int k = 0; k < b7; k++)
			{
				int value54 = syncClientPacket.ReadD();
				byte value55 = syncClientPacket.ReadC();
				byte value56 = syncClientPacket.ReadC();
				ushort value57 = syncClientPacket.ReadUH();
				uint value58 = syncClientPacket.ReadUD();
				ushort value59 = syncClientPacket.ReadUH();
				ushort value60 = syncClientPacket.ReadUH();
				ushort value61 = syncClientPacket.ReadUH();
				syncServerPacket.WriteD(value54);
				syncServerPacket.WriteC(value55);
				syncServerPacket.WriteC(value56);
				syncServerPacket.WriteH(value57);
				syncServerPacket.WriteD(value58);
				syncServerPacket.WriteH(value59);
				syncServerPacket.WriteH(value60);
				syncServerPacket.WriteH(value61);
			}
		}
		if (actionModel_0.Flag.HasFlag(UdpGameEvent.GetWeaponForHost))
		{
			num += 512;
			CharaDeath charaDeath = (CharaDeath)syncClientPacket.ReadC();
			byte value62 = syncClientPacket.ReadC();
			ushort value63 = syncClientPacket.ReadUH();
			ushort value64 = syncClientPacket.ReadUH();
			ushort value65 = syncClientPacket.ReadUH();
			int value66 = syncClientPacket.ReadD();
			CharaHitPart charaHitPart = (CharaHitPart)syncClientPacket.ReadC();
			syncServerPacket.WriteC((byte)charaDeath);
			syncServerPacket.WriteC(value62);
			syncServerPacket.WriteH(value63);
			syncServerPacket.WriteH(value64);
			syncServerPacket.WriteH(value65);
			syncServerPacket.WriteD(value66);
			syncServerPacket.WriteC((byte)charaHitPart);
		}
		if (actionModel_0.Flag.HasFlag(UdpGameEvent.FireDataOnObject))
		{
			num += 1073741824;
			byte value67 = syncClientPacket.ReadC();
			CharaHitPart charaHitPart2 = (CharaHitPart)syncClientPacket.ReadC();
			byte value68 = syncClientPacket.ReadC();
			syncServerPacket.WriteC(value67);
			syncServerPacket.WriteC((byte)charaHitPart2);
			syncServerPacket.WriteC(value68);
		}
		byte_0 = syncServerPacket.ToArray();
		if (num != (uint)actionModel_0.Flag)
		{
			CLogger.Print(Bitwise.ToHexData($"PVE - Missing Flag Events: '{(uint)actionModel_0.Flag}' | '{(uint)(actionModel_0.Flag - num)}'", data), LoggerType.Opcode);
		}
		return list;
	}

	public byte[] WriteBotActionData(byte[] Data, RoomModel Room)
	{
		SyncClientPacket syncClientPacket = new SyncClientPacket(Data);
		List<ObjectHitInfo> list = new List<ObjectHitInfo>();
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		for (int i = 0; i < 18; i++)
		{
			ActionModel actionModel = new ActionModel();
			try
			{
				bool flag = false;
				actionModel.Length = syncClientPacket.ReadUH(out var Exception);
				if (Exception)
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
				case UdpSubHead.Grenade:
				{
					byte value6 = syncClientPacket.ReadC();
					byte value7 = syncClientPacket.ReadC();
					byte value8 = syncClientPacket.ReadC();
					byte value9 = syncClientPacket.ReadC();
					ushort value10 = syncClientPacket.ReadUH();
					int value11 = syncClientPacket.ReadD();
					byte value12 = syncClientPacket.ReadC();
					byte value13 = syncClientPacket.ReadC();
					ushort value14 = syncClientPacket.ReadUH();
					ushort value15 = syncClientPacket.ReadUH();
					ushort value16 = syncClientPacket.ReadUH();
					int value17 = syncClientPacket.ReadD();
					int value18 = syncClientPacket.ReadD();
					int value19 = syncClientPacket.ReadD();
					syncServerPacket.WriteC(value6);
					syncServerPacket.WriteC(value7);
					syncServerPacket.WriteC(value8);
					syncServerPacket.WriteC(value9);
					syncServerPacket.WriteH(value10);
					syncServerPacket.WriteD(value11);
					syncServerPacket.WriteC(value12);
					syncServerPacket.WriteC(value13);
					syncServerPacket.WriteH(value14);
					syncServerPacket.WriteH(value15);
					syncServerPacket.WriteH(value16);
					syncServerPacket.WriteD(value17);
					syncServerPacket.WriteD(value18);
					syncServerPacket.WriteD(value19);
					break;
				}
				case UdpSubHead.DroppedWeapon:
				{
					byte[] value5 = syncClientPacket.ReadB(31);
					syncServerPacket.WriteB(value5);
					break;
				}
				case UdpSubHead.ObjectStatic:
				{
					byte[] value3 = syncClientPacket.ReadB(10);
					syncServerPacket.WriteB(value3);
					break;
				}
				case UdpSubHead.ObjectMove:
				{
					byte[] value21 = syncClientPacket.ReadB(16);
					syncServerPacket.WriteB(value21);
					break;
				}
				case UdpSubHead.ObjectAnim:
				{
					byte[] value20 = syncClientPacket.ReadB(8);
					syncServerPacket.WriteB(value20);
					break;
				}
				case UdpSubHead.User:
				case UdpSubHead.StageInfoChara:
				{
					actionModel.Flag = (UdpGameEvent)syncClientPacket.ReadUD();
					actionModel.Data = syncClientPacket.ReadB(actionModel.Length - 9);
					list.AddRange(method_1(actionModel, Room, out var byte_));
					syncServerPacket.GoBack(5);
					syncServerPacket.WriteH((ushort)(byte_.Length + 9));
					syncServerPacket.WriteH(actionModel.Slot);
					syncServerPacket.WriteC((byte)actionModel.SubHead);
					syncServerPacket.WriteD((uint)actionModel.Flag);
					syncServerPacket.WriteB(byte_);
					if (actionModel.Data.Length == 0 && actionModel.Length - 9 != 0)
					{
						flag = true;
					}
					break;
				}
				case UdpSubHead.StageInfoObjectStatic:
				{
					byte[] value4 = syncClientPacket.ReadB(1);
					syncServerPacket.WriteB(value4);
					break;
				}
				default:
					if (ConfigLoader.IsTestMode)
					{
						CLogger.Print(Bitwise.ToHexData($"PVP Sub Head: '{actionModel.SubHead}' or '{(int)actionModel.SubHead}'", Data), LoggerType.Opcode);
					}
					break;
				case UdpSubHead.StageInfoObjectAnim:
				{
					byte[] value2 = syncClientPacket.ReadB(9);
					syncServerPacket.WriteB(value2);
					break;
				}
				case UdpSubHead.StageInfoObjectControl:
				{
					byte[] value = syncClientPacket.ReadB(9);
					syncServerPacket.WriteB(value);
					break;
				}
				}
				if (flag)
				{
					break;
				}
				continue;
			}
			catch (Exception ex)
			{
				if (ConfigLoader.IsTestMode)
				{
					CLogger.Print($"PVE Action Data - Buffer (Length: {Data.Length}): | {ex.Message}", LoggerType.Error, ex);
				}
				list = new List<ObjectHitInfo>();
			}
			break;
		}
		if (list.Count > 0)
		{
			syncServerPacket.WriteB(PROTOCOL_EVENTS_ACTION.GET_CODE(list));
		}
		list = null;
		return syncServerPacket.ToArray();
	}

	public void SendPacket(byte[] Data, IPEndPoint Address)
	{
		try
		{
			socket_0.BeginSendTo(Data, 0, Data.Length, SocketFlags.None, Address, method_2, socket_0);
		}
		catch (Exception ex)
		{
			CLogger.Print($"Failed to send package to {Address}: {ex.Message}", LoggerType.Error, ex);
		}
	}

	private void method_2(IAsyncResult iasyncResult_0)
	{
		try
		{
			if (iasyncResult_0.AsyncState is Socket socket && socket.Connected)
			{
				socket.EndSend(iasyncResult_0);
			}
		}
		catch (SocketException ex)
		{
			CLogger.Print($"Socket Error on Send: {ex.SocketErrorCode}", LoggerType.Warning);
		}
		catch (ObjectDisposedException)
		{
			CLogger.Print("Socket was closed while sending.", LoggerType.Warning);
		}
		catch (Exception ex3)
		{
			CLogger.Print("Error during EndSendCallback: " + ex3.Message, LoggerType.Error, ex3);
		}
	}
}
