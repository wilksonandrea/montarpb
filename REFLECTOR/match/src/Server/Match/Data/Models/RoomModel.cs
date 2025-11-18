namespace Server.Match.Data.Models
{
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
    using System.Runtime.InteropServices;

    public class RoomModel
    {
        public PlayerModel[] Players = new PlayerModel[0x12];
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
        public int Bar1 = 0x1770;
        public int Bar2 = 0x1770;
        public int Default1 = 0x1770;
        public int Default2 = 0x1770;
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
            if (this.Server != null)
            {
                this.ServerId = int_0;
                for (int i = 0; i < 0x12; i++)
                {
                    this.Players[i] = new PlayerModel(i);
                }
                for (int j = 0; j < 200; j++)
                {
                    this.Objects[j] = new ObjectInfo(j);
                }
            }
        }

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
                PlayerModel model = this.Players[Packet.Slot];
                if (!model.CompareIp(Client))
                {
                    model.Client = Client;
                    model.StartTime = Packet.ReceiveDate;
                    model.PlayerIdByUser = Packet.AccountId;
                    return model;
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
            return null;
        }

        public ObjectInfo GetObject(int Id)
        {
            try
            {
                return this.Objects[Id];
            }
            catch
            {
                return null;
            }
        }

        public PlayerModel GetPlayer(int Slot, bool Active)
        {
            PlayerModel model;
            try
            {
                model = this.Players[Slot];
            }
            catch
            {
                model = null;
            }
            return (((model == null) || (Active && (model.Client == null))) ? null : model);
        }

        public PlayerModel GetPlayer(int Slot, IPEndPoint Client)
        {
            PlayerModel model;
            try
            {
                model = this.Players[Slot];
            }
            catch
            {
                model = null;
            }
            return (((model == null) || !model.CompareIp(Client)) ? null : model);
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
            return (Player != null);
        }

        public int GetPlayersCount()
        {
            int num = 0;
            for (int i = 0; i < 0x12; i++)
            {
                if (this.Players[i].Client != null)
                {
                    num++;
                }
            }
            return num;
        }

        public bool ObjectsIsValid() => 
            this.ServerRound == this.ObjsSyncRound;

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
                PlayerModel model = this.Players[Packet.Slot];
                if (model.CompareIp(Client))
                {
                    model.ResetAllInfos();
                    return true;
                }
            }
            catch (Exception exception)
            {
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
            this.MapId = (MapIdEnum) AllUtils.GetSeedInfo(Seed, 2);
            this.RoomType = (RoomCondition) AllUtils.GetSeedInfo(Seed, 0);
            this.Rule = (MapRules) ((byte) AllUtils.GetSeedInfo(Seed, 1));
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
                    CLogger.Print($"New tick is defined. [{this.LastStartTick}]", LoggerType.Warning, null);
                }
            }
        }

        public bool RoundResetRoomF1(int Round)
        {
            object obj2 = this.object_0;
            lock (obj2)
            {
                if (this.LastRound != Round)
                {
                    if (ConfigLoader.IsTestMode)
                    {
                        CLogger.Print($"Reseting room. [Last: {this.LastRound}; New: {Round}]", LoggerType.Warning, null);
                    }
                    DateTime time = DateTimeUtil.Now();
                    this.LastRound = Round;
                    this.HasC4 = false;
                    this.BombPosition = new Half3();
                    this.DropCounter = 0;
                    this.ObjsSyncRound = 0;
                    this.SourceToMap = (int) this.MapId;
                    if (!this.BotMode)
                    {
                        int index = 0;
                        while (true)
                        {
                            if (index >= 0x12)
                            {
                                List<ObjectModel> objects;
                                this.LastPlayersSync = time;
                                this.Map = MapStructureXML.GetMapId((int) this.MapId);
                                if (this.Map != null)
                                {
                                    objects = this.Map.Objects;
                                }
                                else
                                {
                                    MapModel map = this.Map;
                                    objects = null;
                                }
                                List<ObjectModel> list = objects;
                                if (list != null)
                                {
                                    foreach (ObjectModel model in list)
                                    {
                                        ObjectInfo info = this.Objects[model.Id];
                                        info.Life = model.Life;
                                        if (!model.NoInstaSync)
                                        {
                                            model.GetRandomAnimation(this, info);
                                        }
                                        else
                                        {
                                            AnimModel model2 = new AnimModel();
                                            model2.NextAnim = 1;
                                            info.Animation = model2;
                                            info.UseDate = time;
                                        }
                                        info.Model = model;
                                        info.DestroyState = 0;
                                        MapStructureXML.SetObjectives(model, this);
                                    }
                                }
                                this.LastObjsSync = time;
                                this.ObjsSyncRound = Round;
                                break;
                            }
                            PlayerModel model1 = this.Players[index];
                            model1.Life = model1.MaxLife;
                            index++;
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        public bool RoundResetRoomS1(int Round)
        {
            object obj2 = this.object_0;
            lock (obj2)
            {
                if (this.LastRound != Round)
                {
                    if (ConfigLoader.IsTestMode)
                    {
                        CLogger.Print($"Reseting room. [Last: {this.LastRound}; New: {Round}]", LoggerType.Warning, null);
                    }
                    this.LastRound = Round;
                    this.HasC4 = false;
                    this.DropCounter = 0;
                    this.BombPosition = new Half3();
                    if (!this.BotMode)
                    {
                        int index = 0;
                        while (true)
                        {
                            if (index >= 0x12)
                            {
                                DateTime time = DateTimeUtil.Now();
                                this.LastPlayersSync = time;
                                ObjectInfo[] objects = this.Objects;
                                int num2 = 0;
                                while (true)
                                {
                                    if (num2 >= objects.Length)
                                    {
                                        this.LastObjsSync = time;
                                        this.ObjsSyncRound = Round;
                                        if ((this.RoomType == RoomCondition.Destroy) || (this.RoomType == RoomCondition.Defense))
                                        {
                                            this.Bar1 = this.Default1;
                                            this.Bar2 = this.Default2;
                                        }
                                        break;
                                    }
                                    ObjectInfo info = objects[num2];
                                    ObjectModel model = info.Model;
                                    if (model != null)
                                    {
                                        info.Life = model.Life;
                                        if (!model.NoInstaSync)
                                        {
                                            model.GetRandomAnimation(this, info);
                                        }
                                        else
                                        {
                                            AnimModel model2 = new AnimModel();
                                            model2.NextAnim = 1;
                                            info.Animation = model2;
                                            info.UseDate = time;
                                        }
                                        info.DestroyState = 0;
                                    }
                                    num2++;
                                }
                                break;
                            }
                            PlayerModel model1 = this.Players[index];
                            model1.Life = model1.MaxLife;
                            index++;
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        public void SyncInfo(List<ObjectHitInfo> Objs, int Type)
        {
            object obj2 = this.object_1;
            lock (obj2)
            {
                if (!this.BotMode && this.ObjectsIsValid())
                {
                    double duration = ComDiv.GetDuration(this.LastPlayersSync);
                    if ((ComDiv.GetDuration(this.LastObjsSync) >= 2.5) && ((Type & 1) == 1))
                    {
                        this.LastObjsSync = DateTimeUtil.Now();
                        foreach (ObjectInfo info in this.Objects)
                        {
                            ObjectModel model = info.Model;
                            if ((model != null) && ((model.Destroyable && (info.Life != model.Life)) || model.NeedSync))
                            {
                                float time = AllUtils.GetDuration(info.UseDate);
                                AnimModel animation = info.Animation;
                                if ((animation != null) && ((animation.Duration > 0f) && (time >= animation.Duration)))
                                {
                                    model.GetAnim(animation.NextAnim, time, animation.Duration, info);
                                }
                                ObjectHitInfo info1 = new ObjectHitInfo(model.UpdateId);
                                info1.ObjSyncId = (int) model.NeedSync;
                                info1.AnimId1 = model.Animation;
                                info1.AnimId2 = (info.Animation != null) ? info.Animation.Id : 0xff;
                                ObjectHitInfo local1 = info1;
                                local1.DestroyState = info.DestroyState;
                                local1.ObjId = model.Id;
                                local1.ObjLife = info.Life;
                                local1.SpecialUse = time;
                                ObjectHitInfo item = local1;
                                Objs.Add(item);
                            }
                        }
                    }
                    if ((duration >= 1.5) && ((Type & 2) == 2))
                    {
                        this.LastPlayersSync = DateTimeUtil.Now();
                        foreach (PlayerModel model3 in this.Players)
                        {
                            if (!model3.Immortal && ((model3.MaxLife != model3.Life) || model3.Dead))
                            {
                                ObjectHitInfo info4 = new ObjectHitInfo(4);
                                info4.ObjId = model3.Slot;
                                info4.ObjLife = model3.Life;
                                ObjectHitInfo item = info4;
                                Objs.Add(item);
                            }
                        }
                    }
                }
            }
        }
    }
}

