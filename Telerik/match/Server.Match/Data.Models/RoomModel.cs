using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SharpDX;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Match.Data.Utils;
using Server.Match.Data.XML;
using System;
using System.Collections.Generic;
using System.Net;

namespace Server.Match.Data.Models
{
	public class RoomModel
	{
		public PlayerModel[] Players = new PlayerModel[18];

		public ObjectInfo[] Objects = new ObjectInfo[200];

		public long LastStartTick;

		public uint UniqueRoomId;

		public uint RoomSeed;

		public int ObjsSyncRound;

		public int ServerRound;

		public int SourceToMap = -1;

		public int ServerId;

		public int RoomId;

		public int ChannelId;

		public int LastRound;

		public int Bar1 = 6000;

		public int Bar2 = 6000;

		public int Default1 = 6000;

		public int Default2 = 6000;

		public byte DropCounter;

		public bool BotMode;

		public bool HasC4;

		public bool IsTeamSwap;

		public MapIdEnum MapId;

		public MapRules Rule;

		public RoomCondition RoomType;

		public SChannelModel Server;

		public MapModel Map;

		public Half3 BombPosition;

		public DateTime StartTime;

		public DateTime LastObjsSync;

		public DateTime LastPlayersSync;

		private readonly object object_0 = new object();

		private readonly object object_1 = new object();

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

		public PlayerModel AddPlayer(IPEndPoint Client, PacketModel Packet, string Udp)
		{
			if (ConfigLoader.UdpVersion != Udp)
			{
				if (ConfigLoader.IsTestMode)
				{
					CLogger.Print(string.Concat("Wrong UDP Version (", Udp, "); Player can't be connected into it!"), LoggerType.Warning, null);
				}
				return null;
			}
			try
			{
				PlayerModel players = this.Players[Packet.Slot];
				if (!players.CompareIp(Client))
				{
					players.Client = Client;
					players.StartTime = Packet.ReceiveDate;
					players.PlayerIdByUser = Packet.AccountId;
					return players;
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
			return null;
		}

		public ObjectInfo GetObject(int Id)
		{
			ObjectInfo objects;
			try
			{
				objects = this.Objects[Id];
			}
			catch
			{
				objects = null;
			}
			return objects;
		}

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

		public PlayerModel GetPlayer(int Slot, bool Active)
		{
			PlayerModel players;
			try
			{
				players = this.Players[Slot];
			}
			catch
			{
				players = null;
			}
			if (players != null && (!Active || players.Client != null))
			{
				return players;
			}
			return null;
		}

		public PlayerModel GetPlayer(int Slot, IPEndPoint Client)
		{
			PlayerModel players;
			try
			{
				players = this.Players[Slot];
			}
			catch
			{
				players = null;
			}
			if (players != null && players.CompareIp(Client))
			{
				return players;
			}
			return null;
		}

		public int GetPlayersCount()
		{
			int ınt32 = 0;
			for (int i = 0; i < 18; i++)
			{
				if (this.Players[i].Client != null)
				{
					ınt32++;
				}
			}
			return ınt32;
		}

		public bool ObjectsIsValid()
		{
			return this.ServerRound == this.ObjsSyncRound;
		}

		public bool RemovePlayer(IPEndPoint Client, PacketModel Packet, string Udp)
		{
			if (ConfigLoader.UdpVersion != Udp)
			{
				if (ConfigLoader.IsTestMode)
				{
					CLogger.Print(string.Concat("Wrong UDP Version (", Udp, "); Player can't be disconnected on it!"), LoggerType.Warning, null);
				}
				return false;
			}
			try
			{
				PlayerModel players = this.Players[Packet.Slot];
				if (players.CompareIp(Client))
				{
					players.ResetAllInfos();
					return true;
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
			return false;
		}

		public void ResetRoomInfo(uint Seed)
		{
			for (int i = 0; i < 200; i++)
			{
				this.Objects[i] = new ObjectInfo(i);
			}
			this.MapId = (MapIdEnum)AllUtils.GetSeedInfo(Seed, 2);
			this.RoomType = (RoomCondition)AllUtils.GetSeedInfo(Seed, 0);
			this.Rule = (MapRules)((byte)AllUtils.GetSeedInfo(Seed, 1));
			this.SourceToMap = -1;
			this.Map = null;
			this.LastRound = 0;
			this.DropCounter = 0;
			this.BotMode = false;
			this.HasC4 = false;
			this.IsTeamSwap = false;
			this.ServerRound = 0;
			this.ObjsSyncRound = 0;
			this.LastObjsSync = new DateTime();
			this.LastPlayersSync = new DateTime();
			this.BombPosition = new Half3();
			if (ConfigLoader.IsTestMode)
			{
				CLogger.Print("A room has been reseted by server.", LoggerType.Warning, null);
			}
		}

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

		public bool RoundResetRoomF1(int Round)
		{
			bool flag;
			List<ObjectModel> objects;
			lock (this.object_0)
			{
				if (this.LastRound == Round)
				{
					return false;
				}
				else
				{
					if (ConfigLoader.IsTestMode)
					{
						CLogger.Print(string.Format("Reseting room. [Last: {0}; New: {1}]", this.LastRound, Round), LoggerType.Warning, null);
					}
					DateTime dateTime = DateTimeUtil.Now();
					this.LastRound = Round;
					this.HasC4 = false;
					this.BombPosition = new Half3();
					this.DropCounter = 0;
					this.ObjsSyncRound = 0;
					this.SourceToMap = (int)this.MapId;
					if (!this.BotMode)
					{
						for (int i = 0; i < 18; i++)
						{
							PlayerModel players = this.Players[i];
							players.Life = players.MaxLife;
						}
						this.LastPlayersSync = dateTime;
						this.Map = MapStructureXML.GetMapId((int)this.MapId);
						MapModel map = this.Map;
						if (map != null)
						{
							objects = map.Objects;
						}
						else
						{
							objects = null;
						}
						List<ObjectModel> objectModels = objects;
						if (objectModels != null)
						{
							foreach (ObjectModel objectModel in objectModels)
							{
								ObjectInfo life = this.Objects[objectModel.Id];
								life.Life = objectModel.Life;
								if (objectModel.NoInstaSync)
								{
									life.Animation = new AnimModel()
									{
										NextAnim = 1
									};
									life.UseDate = dateTime;
								}
								else
								{
									objectModel.GetRandomAnimation(this, life);
								}
								life.Model = objectModel;
								life.DestroyState = 0;
								MapStructureXML.SetObjectives(objectModel, this);
							}
						}
						this.LastObjsSync = dateTime;
						this.ObjsSyncRound = Round;
					}
					flag = true;
				}
			}
			return flag;
		}

		public bool RoundResetRoomS1(int Round)
		{
			bool flag;
			lock (this.object_0)
			{
				if (this.LastRound == Round)
				{
					return false;
				}
				else
				{
					if (ConfigLoader.IsTestMode)
					{
						CLogger.Print(string.Format("Reseting room. [Last: {0}; New: {1}]", this.LastRound, Round), LoggerType.Warning, null);
					}
					this.LastRound = Round;
					this.HasC4 = false;
					this.DropCounter = 0;
					this.BombPosition = new Half3();
					if (!this.BotMode)
					{
						for (int i = 0; i < 18; i++)
						{
							PlayerModel players = this.Players[i];
							players.Life = players.MaxLife;
						}
						DateTime dateTime = DateTimeUtil.Now();
						this.LastPlayersSync = dateTime;
						ObjectInfo[] objects = this.Objects;
						for (int j = 0; j < (int)objects.Length; j++)
						{
							ObjectInfo life = objects[j];
							ObjectModel model = life.Model;
							if (model != null)
							{
								life.Life = model.Life;
								if (model.NoInstaSync)
								{
									life.Animation = new AnimModel()
									{
										NextAnim = 1
									};
									life.UseDate = dateTime;
								}
								else
								{
									model.GetRandomAnimation(this, life);
								}
								life.DestroyState = 0;
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
					flag = true;
				}
			}
			return flag;
		}

		public void SyncInfo(List<ObjectHitInfo> Objs, int Type)
		{
			int i;
			lock (this.object_1)
			{
				if (!this.BotMode && this.ObjectsIsValid())
				{
					double duration = ComDiv.GetDuration(this.LastObjsSync);
					double num = ComDiv.GetDuration(this.LastPlayersSync);
					if (duration >= 2.5 && (Type & 1) == 1)
					{
						this.LastObjsSync = DateTimeUtil.Now();
						ObjectInfo[] objects = this.Objects;
						for (i = 0; i < (int)objects.Length; i++)
						{
							ObjectInfo objectInfo = objects[i];
							ObjectModel model = objectInfo.Model;
							if (model != null && (model.Destroyable && objectInfo.Life != model.Life || model.NeedSync))
							{
								float single = AllUtils.GetDuration(objectInfo.UseDate);
								AnimModel animation = objectInfo.Animation;
								if (animation != null && animation.Duration > 0f && single >= animation.Duration)
								{
									model.GetAnim(animation.NextAnim, single, animation.Duration, objectInfo);
								}
								Objs.Add(new ObjectHitInfo(model.UpdateId)
								{
									ObjSyncId = model.NeedSync,
									AnimId1 = model.Animation,
									AnimId2 = (objectInfo.Animation != null ? objectInfo.Animation.Id : 255),
									DestroyState = objectInfo.DestroyState,
									ObjId = model.Id,
									ObjLife = objectInfo.Life,
									SpecialUse = single
								});
							}
						}
					}
					if (num >= 1.5 && (Type & 2) == 2)
					{
						this.LastPlayersSync = DateTimeUtil.Now();
						PlayerModel[] players = this.Players;
						for (i = 0; i < (int)players.Length; i++)
						{
							PlayerModel playerModel = players[i];
							if (!playerModel.Immortal && (playerModel.MaxLife != playerModel.Life || playerModel.Dead))
							{
								Objs.Add(new ObjectHitInfo(4)
								{
									ObjId = playerModel.Slot,
									ObjLife = playerModel.Life
								});
							}
						}
					}
				}
			}
		}
	}
}