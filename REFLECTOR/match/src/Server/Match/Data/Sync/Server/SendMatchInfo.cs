namespace Server.Match.Data.Sync.Server
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Plugin.Core.Utility;
    using Plugin.Core.XML;
    using Server.Match;
    using Server.Match.Data.Enums;
    using Server.Match.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Net;

    public class SendMatchInfo
    {
        public static void SendBombSync(RoomModel Room, PlayerModel Player, int Type, int BombArea)
        {
            try
            {
                IPEndPoint connection = SynchronizeXML.GetServer(Room.Server.Port).Connection;
                using (SyncServerPacket packet = new SyncServerPacket())
                {
                    packet.WriteH((short) 2);
                    packet.WriteH((short) Room.RoomId);
                    packet.WriteH((short) Room.ChannelId);
                    packet.WriteH((short) Room.ServerId);
                    packet.WriteC((byte) Type);
                    packet.WriteC((byte) Player.Slot);
                    if (Type == 0)
                    {
                        packet.WriteC((byte) BombArea);
                        packet.WriteTV(Player.Position);
                        packet.WriteH((short) 0x2a);
                        Room.BombPosition = Player.Position;
                    }
                    MatchXender.Sync.SendPacket(packet.ToArray(), connection);
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }

        public static void SendDeathSync(RoomModel Room, PlayerModel Killer, int ObjectId, int WeaponId, List<DeathServerData> Deaths)
        {
            try
            {
                IPEndPoint connection = SynchronizeXML.GetServer(Room.Server.Port).Connection;
                using (SyncServerPacket packet = new SyncServerPacket())
                {
                    packet.WriteH((short) 3);
                    packet.WriteH((short) Room.RoomId);
                    packet.WriteH((short) Room.ChannelId);
                    packet.WriteH((short) Room.ServerId);
                    packet.WriteC((byte) Deaths.Count);
                    packet.WriteC((byte) Killer.Slot);
                    packet.WriteD(WeaponId);
                    packet.WriteTV(Killer.Position);
                    packet.WriteC((byte) ObjectId);
                    packet.WriteC(0);
                    foreach (DeathServerData data in Deaths)
                    {
                        packet.WriteC((byte) data.Player.Slot);
                        packet.WriteC((byte) ComDiv.GetIdStatics(WeaponId, 2));
                        packet.WriteC((byte) ((data.DeathType * ((CharaDeath) 0x10)) + data.Player.Slot));
                        packet.WriteTV(data.Player.Position);
                        packet.WriteC((byte) data.AssistSlot);
                        packet.WriteC(0);
                        packet.WriteB(new byte[8]);
                    }
                    MatchXender.Sync.SendPacket(packet.ToArray(), connection);
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }

        public static void SendHitMarkerSync(RoomModel Room, PlayerModel Player, CharaDeath DeathType, HitType HitEnum, int Damage)
        {
            try
            {
                IPEndPoint connection = SynchronizeXML.GetServer(Room.Server.Port).Connection;
                using (SyncServerPacket packet = new SyncServerPacket())
                {
                    packet.WriteH((short) 4);
                    packet.WriteH((short) Room.RoomId);
                    packet.WriteH((short) Room.ChannelId);
                    packet.WriteH((short) Room.ServerId);
                    packet.WriteC((byte) Player.Slot);
                    packet.WriteC((byte) DeathType);
                    packet.WriteC((byte) HitEnum);
                    packet.WriteD(Damage);
                    MatchXender.Sync.SendPacket(packet.ToArray(), connection);
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }

        public static void SendPingSync(RoomModel Room, PlayerModel Player)
        {
            try
            {
                IPEndPoint connection = SynchronizeXML.GetServer(Room.Server.Port).Connection;
                using (SyncServerPacket packet = new SyncServerPacket())
                {
                    packet.WriteH((short) 6);
                    packet.WriteH((short) Room.RoomId);
                    packet.WriteH((short) Room.ChannelId);
                    packet.WriteH((short) Room.ServerId);
                    packet.WriteC((byte) Player.Slot);
                    packet.WriteC((byte) Player.Ping);
                    packet.WriteH((ushort) Player.Latency);
                    MatchXender.Sync.SendPacket(packet.ToArray(), connection);
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }

        public static void SendPortalPass(RoomModel Room, PlayerModel Player, int Portal)
        {
            try
            {
                if (Room.RoomType == RoomCondition.Boss)
                {
                    Player.Life = Player.MaxLife;
                    IPEndPoint connection = SynchronizeXML.GetServer(Room.Server.Port).Connection;
                    using (SyncServerPacket packet = new SyncServerPacket())
                    {
                        packet.WriteH((short) 1);
                        packet.WriteH((short) Room.RoomId);
                        packet.WriteH((short) Room.ChannelId);
                        packet.WriteH((short) Room.ServerId);
                        packet.WriteC((byte) Player.Slot);
                        packet.WriteC((byte) Portal);
                        MatchXender.Sync.SendPacket(packet.ToArray(), connection);
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }

        public static void SendSabotageSync(RoomModel Room, PlayerModel Player, int Damage, int UltraSync)
        {
            try
            {
                IPEndPoint connection = SynchronizeXML.GetServer(Room.Server.Port).Connection;
                using (SyncServerPacket packet = new SyncServerPacket())
                {
                    packet.WriteH((short) 5);
                    packet.WriteH((short) Room.RoomId);
                    packet.WriteH((short) Room.ChannelId);
                    packet.WriteH((short) Room.ServerId);
                    packet.WriteC((byte) Player.Slot);
                    packet.WriteH((ushort) Room.Bar1);
                    packet.WriteH((ushort) Room.Bar2);
                    packet.WriteC((byte) UltraSync);
                    packet.WriteH((ushort) Damage);
                    MatchXender.Sync.SendPacket(packet.ToArray(), connection);
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

