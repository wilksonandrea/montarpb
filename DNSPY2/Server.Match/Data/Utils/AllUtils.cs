using System;
using System.Collections;
using System.Net.NetworkInformation;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.SharpDX;
using Plugin.Core.Utility;
using Server.Match.Data.Enums;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;
using Server.Match.Data.XML;

namespace Server.Match.Data.Utils
{
	// Token: 0x02000029 RID: 41
	public static class AllUtils
	{
		// Token: 0x0600009B RID: 155 RVA: 0x000092A0 File Offset: 0x000074A0
		public static float GetDuration(DateTime Date)
		{
			return (float)(DateTimeUtil.Now() - Date).TotalSeconds;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000092C4 File Offset: 0x000074C4
		public static ItemClass ItemClassified(ClassType ClassWeapon)
		{
			ItemClass itemClass = ItemClass.Unknown;
			if (ClassWeapon == ClassType.Assault)
			{
				itemClass = ItemClass.Primary;
			}
			else
			{
				if (ClassWeapon != ClassType.SMG)
				{
					if (ClassWeapon != ClassType.DualSMG)
					{
						if (ClassWeapon == ClassType.Sniper)
						{
							return ItemClass.Primary;
						}
						if (ClassWeapon != ClassType.Shotgun)
						{
							if (ClassWeapon != ClassType.DualShotgun)
							{
								if (ClassWeapon == ClassType.Machinegun)
								{
									return ItemClass.Primary;
								}
								if (ClassWeapon != ClassType.HandGun && ClassWeapon != ClassType.DualHandGun)
								{
									if (ClassWeapon != ClassType.CIC)
									{
										if (ClassWeapon != ClassType.Knife && ClassWeapon != ClassType.DualKnife)
										{
											if (ClassWeapon != ClassType.Knuckle)
											{
												if (ClassWeapon == ClassType.ThrowingGrenade)
												{
													return ItemClass.Explosive;
												}
												if (ClassWeapon == ClassType.ThrowingSpecial)
												{
													return ItemClass.Special;
												}
												if (ClassWeapon == ClassType.Dino)
												{
													return ItemClass.Unknown;
												}
												return itemClass;
											}
										}
										return ItemClass.Melee;
									}
								}
								return ItemClass.Secondary;
							}
						}
						return ItemClass.Primary;
					}
				}
				itemClass = ItemClass.Primary;
			}
			return itemClass;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000022EC File Offset: 0x000004EC
		public static ObjectType GetHitType(uint HitInfo)
		{
			return (ObjectType)(HitInfo & 3U);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000022F1 File Offset: 0x000004F1
		public static int GetHitWho(uint HitInfo)
		{
			return (int)((HitInfo >> 2) & 511U);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000022FC File Offset: 0x000004FC
		public static CharaHitPart GetHitPart(uint HitInfo)
		{
			return (CharaHitPart)((HitInfo >> 11) & 63U);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00002305 File Offset: 0x00000505
		public static int GetHitDamageBot(uint HitInfo)
		{
			return (int)(HitInfo >> 20);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000230B File Offset: 0x0000050B
		public static int GetHitDamageNormal(uint HitInfo)
		{
			return (int)(HitInfo >> 21);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00002311 File Offset: 0x00000511
		public static int GetHitHelmet(uint info)
		{
			return (int)((info >> 17) & 7U);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00002319 File Offset: 0x00000519
		public static CharaDeath GetCharaDeath(uint HitInfo)
		{
			return (CharaDeath)(HitInfo & 15U);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000231F File Offset: 0x0000051F
		public static int GetKillerId(uint HitInfo)
		{
			return (int)((HitInfo >> 11) & 511U);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000232B File Offset: 0x0000052B
		public static int GetObjectType(uint HitInfo)
		{
			return (int)((HitInfo >> 10) & 1U);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00002333 File Offset: 0x00000533
		public static int GetRoomInfo(uint UniqueRoomId, int Type)
		{
			if (Type == 0)
			{
				return (int)(UniqueRoomId & 4095U);
			}
			if (Type == 1)
			{
				return (int)((UniqueRoomId >> 12) & 255U);
			}
			if (Type == 2)
			{
				return (int)((UniqueRoomId >> 20) & 4095U);
			}
			return 0;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00002333 File Offset: 0x00000533
		public static int GetSeedInfo(uint Seed, int Type)
		{
			if (Type == 0)
			{
				return (int)(Seed & 4095U);
			}
			if (Type == 1)
			{
				return (int)((Seed >> 12) & 255U);
			}
			if (Type == 2)
			{
				return (int)((Seed >> 20) & 4095U);
			}
			return 0;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000934C File Offset: 0x0000754C
		public static byte[] BaseWriteCode(int Opcode, byte[] Actions, int SlotId, float Time, int Round, int Respawn, int RoundNumber, int AccountId)
		{
			int num = (17 + Actions.Length) % 6 + 1;
			byte[] array = Bitwise.Encrypt(Actions, num);
			byte[] array2;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				syncServerPacket.WriteC((byte)Opcode);
				syncServerPacket.WriteC((byte)SlotId);
				syncServerPacket.WriteT(Time);
				syncServerPacket.WriteC((byte)Round);
				syncServerPacket.WriteH((ushort)(17 + array.Length));
				syncServerPacket.WriteC((byte)Respawn);
				syncServerPacket.WriteC((byte)RoundNumber);
				syncServerPacket.WriteC((byte)AccountId);
				syncServerPacket.WriteC(0);
				syncServerPacket.WriteD(0);
				syncServerPacket.WriteB(array);
				array2 = syncServerPacket.ToArray();
			}
			return array2;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000093F4 File Offset: 0x000075F4
		public static bool ValidateHitData(int RawDamage, HitDataInfo Hit, out int Damage)
		{
			if (!ConfigLoader.AntiScript)
			{
				Damage = RawDamage;
				return true;
			}
			ItemsStatistic itemStats = ItemStatisticXML.GetItemStats(Hit.WeaponId);
			if (itemStats == null)
			{
				CLogger.Print(string.Format("The Item Statistic was not found. Please add: {0} to config!", Hit.WeaponId), LoggerType.Warning, null);
				Damage = 0;
				return false;
			}
			ItemClass itemClass = AllUtils.ItemClassified(Hit.WeaponClass);
			float num = Vector3.Distance(Hit.StartBullet, Hit.EndBullet);
			if (itemClass != ItemClass.Melee && num > itemStats.Range)
			{
				Damage = 0;
				return false;
			}
			if (itemClass == ItemClass.Melee && num > itemStats.Range)
			{
				Damage = 0;
				return false;
			}
			if (AllUtils.GetHitPart(Hit.HitIndex) != CharaHitPart.HEAD)
			{
				int num2 = itemStats.Damage + itemStats.Damage * 30 / 100;
				if (itemClass != ItemClass.Melee && RawDamage > num2)
				{
					Damage = 0;
					return false;
				}
				if (itemClass == ItemClass.Melee && RawDamage > itemStats.Damage)
				{
					Damage = 0;
					return false;
				}
			}
			Damage = RawDamage;
			return true;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000094D0 File Offset: 0x000076D0
		public static bool ValidateGrenadeHit(int RawDamage, GrenadeHitInfo Hit, out int Damage)
		{
			if (!ConfigLoader.AntiScript)
			{
				Damage = ((AllUtils.ItemClassified(Hit.WeaponClass) == ItemClass.Explosive) ? (RawDamage * ConfigLoader.GrenateDamageMultipler) : RawDamage);
				return true;
			}
			ItemsStatistic itemStats = ItemStatisticXML.GetItemStats(Hit.WeaponId);
			if (itemStats == null)
			{
				CLogger.Print(string.Format("The Item Statistic was not found. Please add: {0} to config!", Hit.WeaponId), LoggerType.Warning, null);
				Damage = 0;
				return false;
			}
			int num = (int)AllUtils.ItemClassified(Hit.WeaponClass);
			float num2 = Vector3.Distance(Hit.FirePos, Hit.HitPos);
			if (num == 4)
			{
				if (num2 > itemStats.Range)
				{
					Damage = 0;
					return false;
				}
				if (RawDamage > itemStats.Damage)
				{
					Damage = 0;
					return false;
				}
			}
			Damage = ((AllUtils.ItemClassified(Hit.WeaponClass) == ItemClass.Explosive) ? (RawDamage * ConfigLoader.GrenateDamageMultipler) : RawDamage);
			return true;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00009594 File Offset: 0x00007794
		public static void GetDecryptedData(PacketModel Packet)
		{
			try
			{
				if (Packet.Data.Length >= Packet.Length)
				{
					byte[] array = new byte[Packet.Length - 17];
					Array.Copy(Packet.Data, 17, array, 0, array.Length);
					byte[] array2 = Bitwise.Decrypt(array, Packet.Length % 6 + 1);
					byte[] array3 = new byte[array2.Length - 9];
					Array.Copy(array2, array3, array3.Length);
					Packet.WithEndData = array2;
					Packet.WithoutEndData = array3;
				}
				else
				{
					CLogger.Print(string.Format("Invalid packet size. (Packet.Data.Length >= Packet.Length): [ {0} | {1} ]", Packet.Data.Length, Packet.Length), LoggerType.Warning, null);
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000235F File Offset: 0x0000055F
		public static void RemoveHit(IList List, int Idx)
		{
			List.RemoveAt(Idx);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00009654 File Offset: 0x00007854
		public static void CheckDataFlags(ActionModel Action, PacketModel Packet)
		{
			UdpGameEvent flag = Action.Flag;
			if (flag.HasFlag(UdpGameEvent.WeaponSync))
			{
				if (Packet.Opcode != 4)
				{
					if ((flag & (UdpGameEvent.DropWeapon | UdpGameEvent.GetWeaponForClient)) > (UdpGameEvent)0U)
					{
						Action.Flag -= 67108864U;
					}
					return;
				}
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000096A8 File Offset: 0x000078A8
		public static int PingTime(string Address, byte[] Buffer, int TTL, int TimeOut, bool IsFragmented, out int Ping)
		{
			int num = 0;
			try
			{
				PingOptions pingOptions = new PingOptions
				{
					Ttl = TTL,
					DontFragment = IsFragmented
				};
				using (Ping ping = new Ping())
				{
					PingReply pingReply = ping.Send(Address, TimeOut, Buffer, pingOptions);
					if (pingReply.Status == IPStatus.Success)
					{
						num = Convert.ToInt32(pingReply.RoundtripTime);
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
			Ping = (int)AllUtils.smethod_0(num);
			return num;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00009738 File Offset: 0x00007938
		private static byte smethod_0(int int_0)
		{
			if (int_0 <= 100)
			{
				return 5;
			}
			if (int_0 >= 100 && int_0 <= 200)
			{
				return 4;
			}
			if (int_0 >= 200 && int_0 <= 300)
			{
				return 3;
			}
			if (int_0 >= 300 && int_0 <= 400)
			{
				return 2;
			}
			if (int_0 >= 400 && int_0 <= 500)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00009794 File Offset: 0x00007994
		public static TeamEnum GetSwappedTeam(PlayerModel Player, RoomModel Room)
		{
			if (Player != null && Room != null)
			{
				TeamEnum teamEnum = Player.Team;
				if (Room.IsTeamSwap)
				{
					teamEnum = ((teamEnum == TeamEnum.FR_TEAM) ? TeamEnum.CT_TEAM : TeamEnum.FR_TEAM);
				}
				return teamEnum;
			}
			return TeamEnum.TEAM_DRAW;
		}
	}
}
