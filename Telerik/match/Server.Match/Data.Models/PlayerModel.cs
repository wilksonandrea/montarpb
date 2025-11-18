using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.SharpDX;
using Server.Match.Data.Enums;
using System;
using System.Net;

namespace Server.Match.Data.Models
{
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
			this.Slot = int_0;
			this.Team = (TeamEnum)(int_0 % 2);
		}

		public bool AccountIdIsValid()
		{
			return this.PlayerIdByServer == this.PlayerIdByUser;
		}

		public void CheckLifeValue()
		{
			if (this.Life > this.MaxLife)
			{
				this.Life = this.MaxLife;
			}
		}

		public bool CompareIp(IPEndPoint Address)
		{
			if (this.Client == null || Address == null || !this.Client.Address.Equals(Address.Address))
			{
				return false;
			}
			return this.Client.Port == Address.Port;
		}

		public void LogPlayerPos(Half3 EndBullet)
		{
			CLogger.Print(string.Format("Player Position X: {0} Y: {1} Z: {2}", this.Position.X, this.Position.Y, this.Position.Z), LoggerType.Warning, null);
			CLogger.Print(string.Format("End Bullet Position X: {0} Y: {1} Z: {2}", EndBullet.X, EndBullet.Y, EndBullet.Z), LoggerType.Warning, null);
		}

		public void ResetAllInfos()
		{
			this.Client = null;
			this.StartTime = new DateTime();
			this.PlayerIdByUser = -2;
			this.PlayerIdByServer = -1;
			this.Integrity = true;
			this.ResetBattleInfos();
		}

		public void ResetBattleInfos()
		{
			this.RespawnByUser = -2;
			this.RespawnByServer = -1;
			this.Immortal = false;
			this.Dead = true;
			this.NeverRespawn = true;
			this.WeaponId = 0;
			this.Accessory = 0;
			this.Extensions = 0;
			this.WeaponClass = ClassType.Unknown;
			this.LastPing = new DateTime();
			this.LastDie = new DateTime();
			this.C4First = new DateTime();
			this.C4Time = 0f;
			this.Position = new Half3();
			this.Life = 100;
			this.MaxLife = 100;
			this.Ping = 5;
			this.Latency = 0;
			this.PlantDuration = ConfigLoader.PlantDuration;
			this.DefuseDuration = ConfigLoader.DefuseDuration;
		}

		public void ResetLife()
		{
			this.Life = this.MaxLife;
		}

		public bool RespawnIsValid()
		{
			return this.RespawnByServer == this.RespawnByUser;
		}
	}
}