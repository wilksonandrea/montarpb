using System;
using System.Net;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.SharpDX;
using Server.Match.Data.Enums;

namespace Server.Match.Data.Models
{
	// Token: 0x0200003E RID: 62
	public class PlayerModel
	{
		// Token: 0x06000188 RID: 392 RVA: 0x0000AAD0 File Offset: 0x00008CD0
		public PlayerModel(int int_0)
		{
			this.Slot = int_0;
			this.Team = (TeamEnum)(int_0 % 2);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000029D1 File Offset: 0x00000BD1
		public bool CompareIp(IPEndPoint Address)
		{
			return this.Client != null && Address != null && this.Client.Address.Equals(Address.Address) && this.Client.Port == Address.Port;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00002A0B File Offset: 0x00000C0B
		public bool RespawnIsValid()
		{
			return this.RespawnByServer == this.RespawnByUser;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00002A1B File Offset: 0x00000C1B
		public bool AccountIdIsValid()
		{
			return this.PlayerIdByServer == this.PlayerIdByUser;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00002A2B File Offset: 0x00000C2B
		public void CheckLifeValue()
		{
			if (this.Life > this.MaxLife)
			{
				this.Life = this.MaxLife;
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00002A47 File Offset: 0x00000C47
		public void ResetAllInfos()
		{
			this.Client = null;
			this.StartTime = default(DateTime);
			this.PlayerIdByUser = -2;
			this.PlayerIdByServer = -1;
			this.Integrity = true;
			this.ResetBattleInfos();
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000AB44 File Offset: 0x00008D44
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
			this.LastPing = default(DateTime);
			this.LastDie = default(DateTime);
			this.C4First = default(DateTime);
			this.C4Time = 0f;
			this.Position = default(Half3);
			this.Life = 100;
			this.MaxLife = 100;
			this.Ping = 5;
			this.Latency = 0;
			this.PlantDuration = ConfigLoader.PlantDuration;
			this.DefuseDuration = ConfigLoader.DefuseDuration;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00002A78 File Offset: 0x00000C78
		public void ResetLife()
		{
			this.Life = this.MaxLife;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000AC00 File Offset: 0x00008E00
		public void LogPlayerPos(Half3 EndBullet)
		{
			CLogger.Print(string.Format("Player Position X: {0} Y: {1} Z: {2}", this.Position.X, this.Position.Y, this.Position.Z), LoggerType.Warning, null);
			CLogger.Print(string.Format("End Bullet Position X: {0} Y: {1} Z: {2}", EndBullet.X, EndBullet.Y, EndBullet.Z), LoggerType.Warning, null);
		}

		// Token: 0x0400006B RID: 107
		public int Slot = -1;

		// Token: 0x0400006C RID: 108
		public TeamEnum Team;

		// Token: 0x0400006D RID: 109
		public int Life = 100;

		// Token: 0x0400006E RID: 110
		public int MaxLife = 100;

		// Token: 0x0400006F RID: 111
		public int PlayerIdByUser = -2;

		// Token: 0x04000070 RID: 112
		public int PlayerIdByServer = -1;

		// Token: 0x04000071 RID: 113
		public int RespawnByUser = -2;

		// Token: 0x04000072 RID: 114
		public int RespawnByServer = -1;

		// Token: 0x04000073 RID: 115
		public int Ping = 5;

		// Token: 0x04000074 RID: 116
		public int Latency;

		// Token: 0x04000075 RID: 117
		public int WeaponId;

		// Token: 0x04000076 RID: 118
		public byte Accessory;

		// Token: 0x04000077 RID: 119
		public byte Extensions;

		// Token: 0x04000078 RID: 120
		public float PlantDuration;

		// Token: 0x04000079 RID: 121
		public float DefuseDuration;

		// Token: 0x0400007A RID: 122
		public float C4Time;

		// Token: 0x0400007B RID: 123
		public Half3 Position;

		// Token: 0x0400007C RID: 124
		public IPEndPoint Client;

		// Token: 0x0400007D RID: 125
		public DateTime StartTime;

		// Token: 0x0400007E RID: 126
		public DateTime LastPing;

		// Token: 0x0400007F RID: 127
		public DateTime LastDie;

		// Token: 0x04000080 RID: 128
		public DateTime C4First;

		// Token: 0x04000081 RID: 129
		public Equipment Equip;

		// Token: 0x04000082 RID: 130
		public ClassType WeaponClass;

		// Token: 0x04000083 RID: 131
		public CharaResId CharaRes;

		// Token: 0x04000084 RID: 132
		public bool Dead = true;

		// Token: 0x04000085 RID: 133
		public bool NeverRespawn = true;

		// Token: 0x04000086 RID: 134
		public bool Integrity = true;

		// Token: 0x04000087 RID: 135
		public bool Immortal;
	}
}
