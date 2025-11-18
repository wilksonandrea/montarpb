using System;
using System.Collections.Generic;
using System.Net;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SharpDX;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Match.Data.Utils;
using Server.Match.Data.XML;

namespace Server.Match.Data.Models
{
	// Token: 0x02000040 RID: 64
	public class RoomModel
	{
		// Token: 0x0600019E RID: 414 RVA: 0x0000AC80 File Offset: 0x00008E80
		public RoomModel(int int_0)
		{
			this.Server = SChannelXML.GetServer(int_0);
			if (this.Server == null)
			{
				return;
			}
			this.ServerId = int_0;
			for (int i = 0; i < 18; i++)
			{
				this.Players[i] = new PlayerModel(i);
			}
			for (int j = 0; j < 200; j++)
			{
				this.Objects[j] = new ObjectInfo(j);
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000AD50 File Offset: 0x00008F50
		public void SyncInfo(List<ObjectHitInfo> Objs, int Type)
		{
			object obj = this.object_1;
			lock (obj)
			{
				if (!this.BotMode && this.ObjectsIsValid())
				{
					double duration = ComDiv.GetDuration(this.LastObjsSync);
					double duration2 = ComDiv.GetDuration(this.LastPlayersSync);
					if (duration >= 2.5 && (Type & 1) == 1)
					{
						this.LastObjsSync = DateTimeUtil.Now();
						foreach (ObjectInfo objectInfo in this.Objects)
						{
							ObjectModel model = objectInfo.Model;
							if (model != null && ((model.Destroyable && objectInfo.Life != model.Life) || model.NeedSync))
							{
								float duration3 = AllUtils.GetDuration(objectInfo.UseDate);
								AnimModel animation = objectInfo.Animation;
								if (animation != null && animation.Duration > 0f && duration3 >= animation.Duration)
								{
									model.GetAnim(animation.NextAnim, duration3, animation.Duration, objectInfo);
								}
								ObjectHitInfo objectHitInfo = new ObjectHitInfo(model.UpdateId)
								{
									ObjSyncId = ((model.NeedSync > false) ? 1 : 0),
									AnimId1 = model.Animation,
									AnimId2 = ((objectInfo.Animation != null) ? objectInfo.Animation.Id : 255),
									DestroyState = objectInfo.DestroyState,
									ObjId = model.Id,
									ObjLife = objectInfo.Life,
									SpecialUse = duration3
								};
								Objs.Add(objectHitInfo);
							}
						}
					}
					if (duration2 >= 1.5 && (Type & 2) == 2)
					{
						this.LastPlayersSync = DateTimeUtil.Now();
						foreach (PlayerModel playerModel in this.Players)
						{
							if (!playerModel.Immortal && (playerModel.MaxLife != playerModel.Life || playerModel.Dead))
							{
								ObjectHitInfo objectHitInfo2 = new ObjectHitInfo(4)
								{
									ObjId = playerModel.Slot,
									ObjLife = playerModel.Life
								};
								Objs.Add(objectHitInfo2);
							}
						}
					}
				}
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00002AEC File Offset: 0x00000CEC
		public bool ObjectsIsValid()
		{
			return this.ServerRound == this.ObjsSyncRound;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000AFA0 File Offset: 0x000091A0
		public void ResyncTick(long StartTick, uint Seed)
		{
			if (StartTick > this.LastStartTick)
			{
				this.StartTime = new DateTime(StartTick);
				if (this.LastStartTick > 0L)
				{
					this.ResetRoomInfo(Seed);
				}
				this.LastStartTick = StartTick;
				if (ConfigLoader.IsTestMode)
				{
					CLogger.Print(string.Format("New tick is defined. [{0}]", this.LastStartTick), LoggerType.Warning, null);
				}
			}
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000B004 File Offset: 0x00009204
		public void ResetRoomInfo(uint Seed)
		{
			for (int i = 0; i < 200; i++)
			{
				this.Objects[i] = new ObjectInfo(i);
			}
			this.MapId = (MapIdEnum)AllUtils.GetSeedInfo(Seed, 2);
			this.RoomType = (RoomCondition)AllUtils.GetSeedInfo(Seed, 0);
			this.Rule = (MapRules)AllUtils.GetSeedInfo(Seed, 1);
			this.SourceToMap = -1;
			this.Map = null;
			this.LastRound = 0;
			this.DropCounter = 0;
			this.BotMode = false;
			this.HasC4 = false;
			this.IsTeamSwap = false;
			this.ServerRound = 0;
			this.ObjsSyncRound = 0;
			this.LastObjsSync = default(DateTime);
			this.LastPlayersSync = default(DateTime);
			this.BombPosition = default(Half3);
			if (ConfigLoader.IsTestMode)
			{
				CLogger.Print("A room has been reseted by server.", LoggerType.Warning, null);
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000B0D0 File Offset: 0x000092D0
		public bool RoundResetRoomF1(int Round)
		{
			object obj = this.object_0;
			lock (obj)
			{
				if (this.LastRound != Round)
				{
					if (ConfigLoader.IsTestMode)
					{
						CLogger.Print(string.Format("Reseting room. [Last: {0}; New: {1}]", this.LastRound, Round), LoggerType.Warning, null);
					}
					DateTime dateTime = DateTimeUtil.Now();
					this.LastRound = Round;
					this.HasC4 = false;
					this.BombPosition = default(Half3);
					this.DropCounter = 0;
					this.ObjsSyncRound = 0;
					this.SourceToMap = (int)this.MapId;
					if (!this.BotMode)
					{
						for (int i = 0; i < 18; i++)
						{
							PlayerModel playerModel = this.Players[i];
							playerModel.Life = playerModel.MaxLife;
						}
						this.LastPlayersSync = dateTime;
						this.Map = MapStructureXML.GetMapId((int)this.MapId);
						MapModel map = this.Map;
						List<ObjectModel> list = ((map != null) ? map.Objects : null);
						if (list != null)
						{
							foreach (ObjectModel objectModel in list)
							{
								ObjectInfo objectInfo = this.Objects[objectModel.Id];
								objectInfo.Life = objectModel.Life;
								if (!objectModel.NoInstaSync)
								{
									objectModel.GetRandomAnimation(this, objectInfo);
								}
								else
								{
									objectInfo.Animation = new AnimModel
									{
										NextAnim = 1
									};
									objectInfo.UseDate = dateTime;
								}
								objectInfo.Model = objectModel;
								objectInfo.DestroyState = 0;
								MapStructureXML.SetObjectives(objectModel, this);
							}
						}
						this.LastObjsSync = dateTime;
						this.ObjsSyncRound = Round;
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000B2AC File Offset: 0x000094AC
		public bool RoundResetRoomS1(int Round)
		{
			object obj = this.object_0;
			lock (obj)
			{
				if (this.LastRound != Round)
				{
					if (ConfigLoader.IsTestMode)
					{
						CLogger.Print(string.Format("Reseting room. [Last: {0}; New: {1}]", this.LastRound, Round), LoggerType.Warning, null);
					}
					this.LastRound = Round;
					this.HasC4 = false;
					this.DropCounter = 0;
					this.BombPosition = default(Half3);
					if (!this.BotMode)
					{
						for (int i = 0; i < 18; i++)
						{
							PlayerModel playerModel = this.Players[i];
							playerModel.Life = playerModel.MaxLife;
						}
						DateTime dateTime = DateTimeUtil.Now();
						this.LastPlayersSync = dateTime;
						foreach (ObjectInfo objectInfo in this.Objects)
						{
							ObjectModel model = objectInfo.Model;
							if (model != null)
							{
								objectInfo.Life = model.Life;
								if (!model.NoInstaSync)
								{
									model.GetRandomAnimation(this, objectInfo);
								}
								else
								{
									objectInfo.Animation = new AnimModel
									{
										NextAnim = 1
									};
									objectInfo.UseDate = dateTime;
								}
								objectInfo.DestroyState = 0;
							}
						}
						this.LastObjsSync = dateTime;
						this.ObjsSyncRound = Round;
						if (this.RoomType == RoomCondition.Destroy || this.RoomType == RoomCondition.Defense)
						{
							this.Bar1 = this.Default1;
							this.Bar2 = this.Default2;
						}
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000B43C File Offset: 0x0000963C
		public PlayerModel AddPlayer(IPEndPoint Client, PacketModel Packet, string Udp)
		{
			if (ConfigLoader.UdpVersion != Udp)
			{
				if (ConfigLoader.IsTestMode)
				{
					CLogger.Print("Wrong UDP Version (" + Udp + "); Player can't be connected into it!", LoggerType.Warning, null);
				}
				return null;
			}
			try
			{
				PlayerModel playerModel = this.Players[Packet.Slot];
				if (!playerModel.CompareIp(Client))
				{
					playerModel.Client = Client;
					playerModel.StartTime = Packet.ReceiveDate;
					playerModel.PlayerIdByUser = Packet.AccountId;
					return playerModel;
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
			return null;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000B4D8 File Offset: 0x000096D8
		public bool GetPlayer(int Slot, out PlayerModel Player)
		{
			try
			{
				Player = this.Players[Slot];
			}
			catch
			{
				Player = null;
			}
			return Player != null;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000B50C File Offset: 0x0000970C
		public PlayerModel GetPlayer(int Slot, bool Active)
		{
			PlayerModel playerModel;
			try
			{
				playerModel = this.Players[Slot];
			}
			catch
			{
				playerModel = null;
			}
			if (playerModel != null && (!Active || playerModel.Client != null))
			{
				return playerModel;
			}
			return null;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000B54C File Offset: 0x0000974C
		public PlayerModel GetPlayer(int Slot, IPEndPoint Client)
		{
			PlayerModel playerModel;
			try
			{
				playerModel = this.Players[Slot];
			}
			catch
			{
				playerModel = null;
			}
			if (playerModel != null && playerModel.CompareIp(Client))
			{
				return playerModel;
			}
			return null;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000B588 File Offset: 0x00009788
		public ObjectInfo GetObject(int Id)
		{
			ObjectInfo objectInfo;
			try
			{
				objectInfo = this.Objects[Id];
			}
			catch
			{
				objectInfo = null;
			}
			return objectInfo;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000B5B8 File Offset: 0x000097B8
		public bool RemovePlayer(IPEndPoint Client, PacketModel Packet, string Udp)
		{
			if (ConfigLoader.UdpVersion != Udp)
			{
				if (ConfigLoader.IsTestMode)
				{
					CLogger.Print("Wrong UDP Version (" + Udp + "); Player can't be disconnected on it!", LoggerType.Warning, null);
				}
				return false;
			}
			try
			{
				PlayerModel playerModel = this.Players[Packet.Slot];
				if (playerModel.CompareIp(Client))
				{
					playerModel.ResetAllInfos();
					return true;
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
			return false;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000B63C File Offset: 0x0000983C
		public int GetPlayersCount()
		{
			int num = 0;
			for (int i = 0; i < 18; i++)
			{
				if (this.Players[i].Client != null)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x0400008E RID: 142
		public PlayerModel[] Players = new PlayerModel[18];

		// Token: 0x0400008F RID: 143
		public ObjectInfo[] Objects = new ObjectInfo[200];

		// Token: 0x04000090 RID: 144
		public long LastStartTick;

		// Token: 0x04000091 RID: 145
		public uint UniqueRoomId;

		// Token: 0x04000092 RID: 146
		public uint RoomSeed;

		// Token: 0x04000093 RID: 147
		public int ObjsSyncRound;

		// Token: 0x04000094 RID: 148
		public int ServerRound;

		// Token: 0x04000095 RID: 149
		public int SourceToMap = -1;

		// Token: 0x04000096 RID: 150
		public int ServerId;

		// Token: 0x04000097 RID: 151
		public int RoomId;

		// Token: 0x04000098 RID: 152
		public int ChannelId;

		// Token: 0x04000099 RID: 153
		public int LastRound;

		// Token: 0x0400009A RID: 154
		public int Bar1 = 6000;

		// Token: 0x0400009B RID: 155
		public int Bar2 = 6000;

		// Token: 0x0400009C RID: 156
		public int Default1 = 6000;

		// Token: 0x0400009D RID: 157
		public int Default2 = 6000;

		// Token: 0x0400009E RID: 158
		public byte DropCounter;

		// Token: 0x0400009F RID: 159
		public bool BotMode;

		// Token: 0x040000A0 RID: 160
		public bool HasC4;

		// Token: 0x040000A1 RID: 161
		public bool IsTeamSwap;

		// Token: 0x040000A2 RID: 162
		public MapIdEnum MapId;

		// Token: 0x040000A3 RID: 163
		public MapRules Rule;

		// Token: 0x040000A4 RID: 164
		public RoomCondition RoomType;

		// Token: 0x040000A5 RID: 165
		public SChannelModel Server;

		// Token: 0x040000A6 RID: 166
		public MapModel Map;

		// Token: 0x040000A7 RID: 167
		public Half3 BombPosition;

		// Token: 0x040000A8 RID: 168
		public DateTime StartTime;

		// Token: 0x040000A9 RID: 169
		public DateTime LastObjsSync;

		// Token: 0x040000AA RID: 170
		public DateTime LastPlayersSync;

		// Token: 0x040000AB RID: 171
		private readonly object object_0 = new object();

		// Token: 0x040000AC RID: 172
		private readonly object object_1 = new object();
	}
}
