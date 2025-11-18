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

namespace Server.Match.Data.Utils;

public static class AllUtils
{
	public static float GetDuration(DateTime Date)
	{
		return (float)(DateTimeUtil.Now() - Date).TotalSeconds;
	}

	public static ItemClass ItemClassified(ClassType ClassWeapon)
	{
		ItemClass result = ItemClass.Unknown;
		switch (ClassWeapon)
		{
		case ClassType.Assault:
			result = ItemClass.Primary;
			break;
		case ClassType.Sniper:
			result = ItemClass.Primary;
			break;
		case ClassType.Machinegun:
			result = ItemClass.Primary;
			break;
		case ClassType.ThrowingGrenade:
			result = ItemClass.Explosive;
			break;
		case ClassType.ThrowingSpecial:
			result = ItemClass.Special;
			break;
		case ClassType.Dino:
			result = ItemClass.Unknown;
			break;
		case ClassType.Knife:
		case ClassType.DualKnife:
		case ClassType.Knuckle:
			result = ItemClass.Melee;
			break;
		case ClassType.HandGun:
		case ClassType.CIC:
		case ClassType.DualHandGun:
			result = ItemClass.Secondary;
			break;
		case ClassType.Shotgun:
		case ClassType.DualShotgun:
			result = ItemClass.Primary;
			break;
		case ClassType.SMG:
		case ClassType.DualSMG:
			result = ItemClass.Primary;
			break;
		}
		return result;
	}

	public static ObjectType GetHitType(uint HitInfo)
	{
		return (ObjectType)((int)HitInfo & 3);
	}

	public static int GetHitWho(uint HitInfo)
	{
		return (int)((HitInfo >> 2) & 0x1FF);
	}

	public static CharaHitPart GetHitPart(uint HitInfo)
	{
		return (CharaHitPart)((int)(HitInfo >> 11) & 0x3F);
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
		return (int)((info >> 17) & 7);
	}

	public static CharaDeath GetCharaDeath(uint HitInfo)
	{
		return (CharaDeath)((int)HitInfo & 0xF);
	}

	public static int GetKillerId(uint HitInfo)
	{
		return (int)((HitInfo >> 11) & 0x1FF);
	}

	public static int GetObjectType(uint HitInfo)
	{
		return (int)((HitInfo >> 10) & 1);
	}

	public static int GetRoomInfo(uint UniqueRoomId, int Type)
	{
		return Type switch
		{
			0 => (int)(UniqueRoomId & 0xFFF), 
			1 => (int)((UniqueRoomId >> 12) & 0xFF), 
			2 => (int)((UniqueRoomId >> 20) & 0xFFF), 
			_ => 0, 
		};
	}

	public static int GetSeedInfo(uint Seed, int Type)
	{
		return Type switch
		{
			0 => (int)(Seed & 0xFFF), 
			1 => (int)((Seed >> 12) & 0xFF), 
			2 => (int)((Seed >> 20) & 0xFFF), 
			_ => 0, 
		};
	}

	public static byte[] BaseWriteCode(int Opcode, byte[] Actions, int SlotId, float Time, int Round, int Respawn, int RoundNumber, int AccountId)
	{
		int shift = (17 + Actions.Length) % 6 + 1;
		byte[] array = Bitwise.Encrypt(Actions, shift);
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
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
		return syncServerPacket.ToArray();
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
			CLogger.Print($"The Item Statistic was not found. Please add: {Hit.WeaponId} to config!", LoggerType.Warning);
			Damage = 0;
			return false;
		}
		ItemClass ıtemClass = ItemClassified(Hit.WeaponClass);
		float num = Vector3.Distance(Hit.StartBullet, Hit.EndBullet);
		if (ıtemClass != ItemClass.Melee && num > ıtemStats.Range)
		{
			Damage = 0;
			return false;
		}
		if (ıtemClass == ItemClass.Melee && num > ıtemStats.Range)
		{
			Damage = 0;
			return false;
		}
		if (GetHitPart(Hit.HitIndex) != CharaHitPart.HEAD)
		{
			int num2 = ıtemStats.Damage + ıtemStats.Damage * 30 / 100;
			if (ıtemClass != ItemClass.Melee && RawDamage > num2)
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

	public static bool ValidateGrenadeHit(int RawDamage, GrenadeHitInfo Hit, out int Damage)
	{
		if (!ConfigLoader.AntiScript)
		{
			Damage = ((ItemClassified(Hit.WeaponClass) == ItemClass.Explosive) ? (RawDamage * ConfigLoader.GrenateDamageMultipler) : RawDamage);
			return true;
		}
		ItemsStatistic ıtemStats = ItemStatisticXML.GetItemStats(Hit.WeaponId);
		if (ıtemStats == null)
		{
			CLogger.Print($"The Item Statistic was not found. Please add: {Hit.WeaponId} to config!", LoggerType.Warning);
			Damage = 0;
			return false;
		}
		ItemClass num = ItemClassified(Hit.WeaponClass);
		float num2 = Vector3.Distance(Hit.FirePos, Hit.HitPos);
		if (num == ItemClass.Explosive)
		{
			if (num2 > ıtemStats.Range)
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
		Damage = ((ItemClassified(Hit.WeaponClass) == ItemClass.Explosive) ? (RawDamage * ConfigLoader.GrenateDamageMultipler) : RawDamage);
		return true;
	}

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
				CLogger.Print($"Invalid packet size. (Packet.Data.Length >= Packet.Length): [ {Packet.Data.Length} | {Packet.Length} ]", LoggerType.Warning);
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	public static void RemoveHit(IList List, int Idx)
	{
		List.RemoveAt(Idx);
	}

	public static void CheckDataFlags(ActionModel Action, PacketModel Packet)
	{
		UdpGameEvent flag = Action.Flag;
		if (flag.HasFlag(UdpGameEvent.WeaponSync) && Packet.Opcode != 4 && (flag & (UdpGameEvent.DropWeapon | UdpGameEvent.GetWeaponForClient)) != 0)
		{
			Action.Flag -= 67108864u;
		}
	}

	public static int PingTime(string Address, byte[] Buffer, int TTL, int TimeOut, bool IsFragmented, out int Ping)
	{
		int num = 0;
		try
		{
			PingOptions options = new PingOptions
			{
				Ttl = TTL,
				DontFragment = IsFragmented
			};
			using Ping ping = new Ping();
			PingReply pingReply = ping.Send(Address, TimeOut, Buffer, options);
			if (pingReply.Status == IPStatus.Success)
			{
				num = Convert.ToInt32(pingReply.RoundtripTime);
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
		Ping = smethod_0(num);
		return num;
	}

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
