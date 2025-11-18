using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.SharpDX;
using Plugin.Core.Utility;
using Server.Match.Data.Enums;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;
using Server.Match.Data.XML;
using System;
using System.Collections;
using System.Net.NetworkInformation;

namespace Server.Match.Data.Utils
{
	public static class AllUtils
	{
		public static byte[] BaseWriteCode(int Opcode, byte[] Actions, int SlotId, float Time, int Round, int Respawn, int RoundNumber, int AccountId)
		{
			byte[] array;
			int length = (17 + (int)Actions.Length) % 6 + 1;
			byte[] numArray = Bitwise.Encrypt(Actions, length);
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				syncServerPacket.WriteC((byte)Opcode);
				syncServerPacket.WriteC((byte)SlotId);
				syncServerPacket.WriteT(Time);
				syncServerPacket.WriteC((byte)Round);
				syncServerPacket.WriteH((ushort)(17 + (int)numArray.Length));
				syncServerPacket.WriteC((byte)Respawn);
				syncServerPacket.WriteC((byte)RoundNumber);
				syncServerPacket.WriteC((byte)AccountId);
				syncServerPacket.WriteC(0);
				syncServerPacket.WriteD(0);
				syncServerPacket.WriteB(numArray);
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		public static void CheckDataFlags(ActionModel Action, PacketModel Packet)
		{
			UdpGameEvent flag = Action.Flag;
			if (flag.HasFlag(UdpGameEvent.WeaponSync))
			{
				if (Packet.Opcode != 4)
				{
					if ((int)(flag & (UdpGameEvent.DropWeapon | UdpGameEvent.GetWeaponForClient)) > 0)
					{
						ActionModel action = Action;
						action.Flag = (UdpGameEvent)((uint)action.Flag - (uint)UdpGameEvent.WeaponSync);
					}
					return;
				}
			}
		}

		public static CharaDeath GetCharaDeath(uint HitInfo)
		{
			return (CharaDeath)(HitInfo & 15);
		}

		public static void GetDecryptedData(PacketModel Packet)
		{
			try
			{
				if ((int)Packet.Data.Length < Packet.Length)
				{
					CLogger.Print(string.Format("Invalid packet size. (Packet.Data.Length >= Packet.Length): [ {0} | {1} ]", (int)Packet.Data.Length, Packet.Length), LoggerType.Warning, null);
				}
				else
				{
					byte[] numArray = new byte[Packet.Length - 17];
					Array.Copy(Packet.Data, 17, numArray, 0, (int)numArray.Length);
					byte[] numArray1 = Bitwise.Decrypt(numArray, Packet.Length % 6 + 1);
					byte[] numArray2 = new byte[(int)numArray1.Length - 9];
					Array.Copy(numArray1, numArray2, (int)numArray2.Length);
					Packet.WithEndData = numArray1;
					Packet.WithoutEndData = numArray2;
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}

		public static float GetDuration(DateTime Date)
		{
			return (float)(DateTimeUtil.Now() - Date).TotalSeconds;
		}

		public static int GetHitDamageBot(uint HitInfo)
		{
			return (int)(HitInfo >> 20);
		}

		public static int GetHitDamageNormal(uint HitInfo)
		{
			return (int)(HitInfo >> 21);
		}

		public static int GetHitHelmet(uint info)
		{
			return (int)(info >> 17 & 7);
		}

		public static CharaHitPart GetHitPart(uint HitInfo)
		{
			return (CharaHitPart)(HitInfo >> 11 & 63);
		}

		public static ObjectType GetHitType(uint HitInfo)
		{
			return (ObjectType)(HitInfo & 3);
		}

		public static int GetHitWho(uint HitInfo)
		{
			return (int)(HitInfo >> 2 & 511);
		}

		public static int GetKillerId(uint HitInfo)
		{
			return (int)(HitInfo >> 11 & 511);
		}

		public static int GetObjectType(uint HitInfo)
		{
			return (int)(HitInfo >> 10 & 1);
		}

		public static int GetRoomInfo(uint UniqueRoomId, int Type)
		{
			if (Type == 0)
			{
				return (int)(UniqueRoomId & 4095);
			}
			if (Type == 1)
			{
				return (int)(UniqueRoomId >> 12 & 255);
			}
			if (Type != 2)
			{
				return 0;
			}
			return (int)(UniqueRoomId >> 20 & 4095);
		}

		public static int GetSeedInfo(uint Seed, int Type)
		{
			if (Type == 0)
			{
				return (int)(Seed & 4095);
			}
			if (Type == 1)
			{
				return (int)(Seed >> 12 & 255);
			}
			if (Type != 2)
			{
				return 0;
			}
			return (int)(Seed >> 20 & 4095);
		}

		public static TeamEnum GetSwappedTeam(PlayerModel Player, RoomModel Room)
		{
			if (Player == null || Room == null)
			{
				return TeamEnum.TEAM_DRAW;
			}
			TeamEnum team = Player.Team;
			if (Room.IsTeamSwap)
			{
				team = (team == TeamEnum.FR_TEAM ? TeamEnum.CT_TEAM : TeamEnum.FR_TEAM);
			}
			return team;
		}

		public static ItemClass ItemClassified(ClassType ClassWeapon)
		{
			ItemClass ıtemClass = ItemClass.Unknown;
			if (ClassWeapon != ClassType.Assault)
			{
				if (ClassWeapon != ClassType.SMG)
				{
					if (ClassWeapon == ClassType.DualSMG)
					{
						goto Label5;
					}
					if (ClassWeapon != ClassType.Sniper)
					{
						if (ClassWeapon != ClassType.Shotgun)
						{
							if (ClassWeapon == ClassType.DualShotgun)
							{
								ıtemClass = ItemClass.Primary;
								return ıtemClass;
							}
							if (ClassWeapon != ClassType.Machinegun)
							{
								if (ClassWeapon != ClassType.HandGun && ClassWeapon != ClassType.DualHandGun)
								{
									if (ClassWeapon == ClassType.CIC)
									{
										ıtemClass = ItemClass.Secondary;
										return ıtemClass;
									}
									if (ClassWeapon != ClassType.Knife && ClassWeapon != ClassType.DualKnife)
									{
										if (ClassWeapon == ClassType.Knuckle)
										{
											ıtemClass = ItemClass.Melee;
											return ıtemClass;
										}
										if (ClassWeapon == ClassType.ThrowingGrenade)
										{
											ıtemClass = ItemClass.Explosive;
											return ıtemClass;
										}
										else if (ClassWeapon == ClassType.ThrowingSpecial)
										{
											ıtemClass = ItemClass.Special;
											return ıtemClass;
										}
										else if (ClassWeapon == ClassType.Dino)
										{
											ıtemClass = ItemClass.Unknown;
											return ıtemClass;
										}
										else
										{
											return ıtemClass;
										}
									}
									ıtemClass = ItemClass.Melee;
									return ıtemClass;
								}
								ıtemClass = ItemClass.Secondary;
								return ıtemClass;
							}
							else
							{
								ıtemClass = ItemClass.Primary;
								return ıtemClass;
							}
						}
						ıtemClass = ItemClass.Primary;
						return ıtemClass;
					}
					else
					{
						ıtemClass = ItemClass.Primary;
						return ıtemClass;
					}
				}
			Label5:
				ıtemClass = ItemClass.Primary;
			}
			else
			{
				ıtemClass = ItemClass.Primary;
			}
			return ıtemClass;
		}

		public static int PingTime(string Address, byte[] Buffer, int TTL, int TimeOut, bool IsFragmented, out int Ping)
		{
			int ınt32 = 0;
			try
			{
				PingOptions pingOption = new PingOptions()
				{
					Ttl = TTL,
					DontFragment = IsFragmented
				};
				using (System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping())
				{
					PingReply pingReply = ping.Send(Address, TimeOut, Buffer, pingOption);
					if (pingReply.Status == IPStatus.Success)
					{
						ınt32 = Convert.ToInt32(pingReply.RoundtripTime);
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
			Ping = AllUtils.smethod_0(ınt32);
			return ınt32;
		}

		public static void RemoveHit(IList List, int Idx)
		{
			List.RemoveAt(Idx);
		}

		private static byte smethod_0(int int_0)
		{
			if (int_0 <= 100)
			{
				return (byte)5;
			}
			if (int_0 >= 100 && int_0 <= 200)
			{
				return (byte)4;
			}
			if (int_0 >= 200 && int_0 <= 300)
			{
				return (byte)3;
			}
			if (int_0 >= 300 && int_0 <= 400)
			{
				return (byte)2;
			}
			if (int_0 >= 400 && int_0 <= 500)
			{
				return (byte)1;
			}
			return (byte)0;
		}

		public static bool ValidateGrenadeHit(int RawDamage, GrenadeHitInfo Hit, out int Damage)
		{
			if (!ConfigLoader.AntiScript)
			{
				Damage = (AllUtils.ItemClassified(Hit.WeaponClass) == ItemClass.Explosive ? RawDamage * ConfigLoader.GrenateDamageMultipler : RawDamage);
				return true;
			}
			ItemsStatistic ıtemStats = ItemStatisticXML.GetItemStats(Hit.WeaponId);
			if (ıtemStats == null)
			{
				CLogger.Print(string.Format("The Item Statistic was not found. Please add: {0} to config!", Hit.WeaponId), LoggerType.Warning, null);
				Damage = 0;
				return false;
			}
			ItemClass ıtemClass = AllUtils.ItemClassified(Hit.WeaponClass);
			float single = Vector3.Distance(Hit.FirePos, Hit.HitPos);
			if (ıtemClass == ItemClass.Explosive)
			{
				if (single > ıtemStats.Range)
				{
					Damage = 0;
					return false;
				}
				if (RawDamage > ıtemStats.Damage)
				{
					Damage = 0;
					return false;
				}
			}
			Damage = (AllUtils.ItemClassified(Hit.WeaponClass) == ItemClass.Explosive ? RawDamage * ConfigLoader.GrenateDamageMultipler : RawDamage);
			return true;
		}

		public static bool ValidateHitData(int RawDamage, HitDataInfo Hit, out int Damage)
		{
			if (!ConfigLoader.AntiScript)
			{
				Damage = RawDamage;
				return true;
			}
			ItemsStatistic ıtemStats = ItemStatisticXML.GetItemStats(Hit.WeaponId);
			if (ıtemStats == null)
			{
				CLogger.Print(string.Format("The Item Statistic was not found. Please add: {0} to config!", Hit.WeaponId), LoggerType.Warning, null);
				Damage = 0;
				return false;
			}
			ItemClass ıtemClass = AllUtils.ItemClassified(Hit.WeaponClass);
			float single = Vector3.Distance(Hit.StartBullet, Hit.EndBullet);
			if (ıtemClass != ItemClass.Melee && single > ıtemStats.Range)
			{
				Damage = 0;
				return false;
			}
			if (ıtemClass == ItemClass.Melee && single > ıtemStats.Range)
			{
				Damage = 0;
				return false;
			}
			if (AllUtils.GetHitPart(Hit.HitIndex) != CharaHitPart.HEAD)
			{
				int damage = ıtemStats.Damage + ıtemStats.Damage * 30 / 100;
				if (ıtemClass != ItemClass.Melee && RawDamage > damage)
				{
					Damage = 0;
					return false;
				}
				if (ıtemClass == ItemClass.Melee && RawDamage > ıtemStats.Damage)
				{
					Damage = 0;
					return false;
				}
			}
			Damage = RawDamage;
			return true;
		}
	}
}