using System;
using System.Net;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.SharpDX;
using Server.Match.Data.Enums;

namespace Server.Match.Data.Models;

public class PlayerModel
{
	public int Slot = -1;

	public TeamEnum Team;

	public int Life = 100;

	public int MaxLife = 100;

	public int PlayerIdByUser = -2;

	public int PlayerIdByServer = -1;

	public int RespawnByUser = -2;

	public int RespawnByServer = -1;

	public int Ping = 5;

	public int Latency;

	public int WeaponId;

	public byte Accessory;

	public byte Extensions;

	public float PlantDuration;

	public float DefuseDuration;

	public float C4Time;

	public Half3 Position;

	public IPEndPoint Client;

	public DateTime StartTime;

	public DateTime LastPing;

	public DateTime LastDie;

	public DateTime C4First;

	public Equipment Equip;

	public ClassType WeaponClass;

	public CharaResId CharaRes;

	public bool Dead = true;

	public bool NeverRespawn = true;

	public bool Integrity = true;

	public bool Immortal;

	public PlayerModel(int int_0)
	{
		Slot = int_0;
		Team = (TeamEnum)(int_0 % 2);
	}

	public bool CompareIp(IPEndPoint Address)
	{
		if (Client != null && Address != null && Client.Address.Equals(Address.Address))
		{
			return Client.Port == Address.Port;
		}
		return false;
	}

	public bool RespawnIsValid()
	{
		return RespawnByServer == RespawnByUser;
	}

	public bool AccountIdIsValid()
	{
		return PlayerIdByServer == PlayerIdByUser;
	}

	public void CheckLifeValue()
	{
		if (Life > MaxLife)
		{
			Life = MaxLife;
		}
	}

	public void ResetAllInfos()
	{
		Client = null;
		StartTime = default(DateTime);
		PlayerIdByUser = -2;
		PlayerIdByServer = -1;
		Integrity = true;
		ResetBattleInfos();
	}

	public void ResetBattleInfos()
	{
		RespawnByUser = -2;
		RespawnByServer = -1;
		Immortal = false;
		Dead = true;
		NeverRespawn = true;
		WeaponId = 0;
		Accessory = 0;
		Extensions = 0;
		WeaponClass = ClassType.Unknown;
		LastPing = default(DateTime);
		LastDie = default(DateTime);
		C4First = default(DateTime);
		C4Time = 0f;
		Position = default(Half3);
		Life = 100;
		MaxLife = 100;
		Ping = 5;
		Latency = 0;
		PlantDuration = ConfigLoader.PlantDuration;
		DefuseDuration = ConfigLoader.DefuseDuration;
	}

	public void ResetLife()
	{
		Life = MaxLife;
	}

	public void LogPlayerPos(Half3 EndBullet)
	{
		CLogger.Print($"Player Position X: {Position.X} Y: {Position.Y} Z: {Position.Z}", LoggerType.Warning);
		CLogger.Print($"End Bullet Position X: {EndBullet.X} Y: {EndBullet.Y} Z: {EndBullet.Z}", LoggerType.Warning);
	}
}
