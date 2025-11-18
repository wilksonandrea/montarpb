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

namespace Server.Match.Data.Models;

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
		Server = SChannelXML.GetServer(int_0);
		if (Server != null)
		{
			ServerId = int_0;
			for (int i = 0; i < 18; i++)
			{
				Players[i] = new PlayerModel(i);
			}
			for (int j = 0; j < 200; j++)
			{
				Objects[j] = new ObjectInfo(j);
			}
		}
	}

	public void SyncInfo(List<ObjectHitInfo> Objs, int Type)
	{
		lock (object_1)
		{
			if (BotMode || !ObjectsIsValid())
			{
				return;
			}
			double duration = ComDiv.GetDuration(LastObjsSync);
			double duration2 = ComDiv.GetDuration(LastPlayersSync);
			if (duration >= 2.5 && (Type & 1) == 1)
			{
				LastObjsSync = DateTimeUtil.Now();
				ObjectInfo[] objects = Objects;
				foreach (ObjectInfo objectInfo in objects)
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
						ObjectHitInfo item = new ObjectHitInfo(model.UpdateId)
						{
							ObjSyncId = (model.NeedSync ? 1 : 0),
							AnimId1 = model.Animation,
							AnimId2 = ((objectInfo.Animation != null) ? objectInfo.Animation.Id : 255),
							DestroyState = objectInfo.DestroyState,
							ObjId = model.Id,
							ObjLife = objectInfo.Life,
							SpecialUse = duration3
						};
						Objs.Add(item);
					}
				}
			}
			if (!(duration2 >= 1.5) || (Type & 2) != 2)
			{
				return;
			}
			LastPlayersSync = DateTimeUtil.Now();
			PlayerModel[] players = Players;
			foreach (PlayerModel playerModel in players)
			{
				if (!playerModel.Immortal && (playerModel.MaxLife != playerModel.Life || playerModel.Dead))
				{
					ObjectHitInfo item2 = new ObjectHitInfo(4)
					{
						ObjId = playerModel.Slot,
						ObjLife = playerModel.Life
					};
					Objs.Add(item2);
				}
			}
		}
	}

	public bool ObjectsIsValid()
	{
		return ServerRound == ObjsSyncRound;
	}

	public void ResyncTick(long StartTick, uint Seed)
	{
		if (StartTick > LastStartTick)
		{
			StartTime = new DateTime(StartTick);
			if (LastStartTick > 0L)
			{
				ResetRoomInfo(Seed);
			}
			LastStartTick = StartTick;
			if (ConfigLoader.IsTestMode)
			{
				CLogger.Print($"New tick is defined. [{LastStartTick}]", LoggerType.Warning);
			}
		}
	}

	public void ResetRoomInfo(uint Seed)
	{
		for (int i = 0; i < 200; i++)
		{
			Objects[i] = new ObjectInfo(i);
		}
		MapId = (MapIdEnum)AllUtils.GetSeedInfo(Seed, 2);
		RoomType = (RoomCondition)AllUtils.GetSeedInfo(Seed, 0);
		Rule = (MapRules)AllUtils.GetSeedInfo(Seed, 1);
		SourceToMap = -1;
		Map = null;
		LastRound = 0;
		DropCounter = 0;
		BotMode = false;
		HasC4 = false;
		IsTeamSwap = false;
		ServerRound = 0;
		ObjsSyncRound = 0;
		LastObjsSync = default(DateTime);
		LastPlayersSync = default(DateTime);
		BombPosition = default(Half3);
		if (ConfigLoader.IsTestMode)
		{
			CLogger.Print("A room has been reseted by server.", LoggerType.Warning);
		}
	}

	public bool RoundResetRoomF1(int Round)
	{
		lock (object_0)
		{
			if (LastRound != Round)
			{
				if (ConfigLoader.IsTestMode)
				{
					CLogger.Print($"Reseting room. [Last: {LastRound}; New: {Round}]", LoggerType.Warning);
				}
				DateTime dateTime = DateTimeUtil.Now();
				LastRound = Round;
				HasC4 = false;
				BombPosition = default(Half3);
				DropCounter = 0;
				ObjsSyncRound = 0;
				SourceToMap = (int)MapId;
				if (!BotMode)
				{
					for (int i = 0; i < 18; i++)
					{
						PlayerModel obj = Players[i];
						obj.Life = obj.MaxLife;
					}
					LastPlayersSync = dateTime;
					Map = MapStructureXML.GetMapId((int)MapId);
					List<ObjectModel> list = Map?.Objects;
					if (list != null)
					{
						foreach (ObjectModel item in list)
						{
							ObjectInfo objectInfo = Objects[item.Id];
							objectInfo.Life = item.Life;
							if (!item.NoInstaSync)
							{
								item.GetRandomAnimation(this, objectInfo);
							}
							else
							{
								objectInfo.Animation = new AnimModel
								{
									NextAnim = 1
								};
								objectInfo.UseDate = dateTime;
							}
							objectInfo.Model = item;
							objectInfo.DestroyState = 0;
							MapStructureXML.SetObjectives(item, this);
						}
					}
					LastObjsSync = dateTime;
					ObjsSyncRound = Round;
				}
				return true;
			}
		}
		return false;
	}

	public bool RoundResetRoomS1(int Round)
	{
		lock (object_0)
		{
			if (LastRound != Round)
			{
				if (ConfigLoader.IsTestMode)
				{
					CLogger.Print($"Reseting room. [Last: {LastRound}; New: {Round}]", LoggerType.Warning);
				}
				LastRound = Round;
				HasC4 = false;
				DropCounter = 0;
				BombPosition = default(Half3);
				if (!BotMode)
				{
					for (int i = 0; i < 18; i++)
					{
						PlayerModel obj = Players[i];
						obj.Life = obj.MaxLife;
					}
					DateTime dateTime = (LastPlayersSync = DateTimeUtil.Now());
					ObjectInfo[] objects = Objects;
					foreach (ObjectInfo objectInfo in objects)
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
					LastObjsSync = dateTime;
					ObjsSyncRound = Round;
					if (RoomType == RoomCondition.Destroy || RoomType == RoomCondition.Defense)
					{
						Bar1 = Default1;
						Bar2 = Default2;
					}
				}
				return true;
			}
		}
		return false;
	}

	public PlayerModel AddPlayer(IPEndPoint Client, PacketModel Packet, string Udp)
	{
		if (ConfigLoader.UdpVersion != Udp)
		{
			if (ConfigLoader.IsTestMode)
			{
				CLogger.Print("Wrong UDP Version (" + Udp + "); Player can't be connected into it!", LoggerType.Warning);
			}
			return null;
		}
		try
		{
			PlayerModel playerModel = Players[Packet.Slot];
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

	public bool GetPlayer(int Slot, out PlayerModel Player)
	{
		try
		{
			Player = Players[Slot];
		}
		catch
		{
			Player = null;
		}
		return Player != null;
	}

	public PlayerModel GetPlayer(int Slot, bool Active)
	{
		PlayerModel playerModel;
		try
		{
			playerModel = Players[Slot];
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

	public PlayerModel GetPlayer(int Slot, IPEndPoint Client)
	{
		PlayerModel playerModel;
		try
		{
			playerModel = Players[Slot];
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

	public ObjectInfo GetObject(int Id)
	{
		try
		{
			return Objects[Id];
		}
		catch
		{
			return null;
		}
	}

	public bool RemovePlayer(IPEndPoint Client, PacketModel Packet, string Udp)
	{
		if (ConfigLoader.UdpVersion != Udp)
		{
			if (ConfigLoader.IsTestMode)
			{
				CLogger.Print("Wrong UDP Version (" + Udp + "); Player can't be disconnected on it!", LoggerType.Warning);
			}
			return false;
		}
		try
		{
			PlayerModel playerModel = Players[Packet.Slot];
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

	public int GetPlayersCount()
	{
		int num = 0;
		for (int i = 0; i < 18; i++)
		{
			if (Players[i].Client != null)
			{
				num++;
			}
		}
		return num;
	}
}
