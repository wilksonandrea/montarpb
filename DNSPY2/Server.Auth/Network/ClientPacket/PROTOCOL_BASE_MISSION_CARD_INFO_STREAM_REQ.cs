using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Auth.Network.ServerPacket;

namespace Server.Auth.Network.ClientPacket
{
    public class PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_REQ : AuthClientPacket
    {
        [Serializable]
        [CompilerGenerated]
        private sealed class Class2
        {
            public static readonly Class2 _003C_003E9 = new Class2();

            public static Func<Mission, int> _003C_003E9__5_0;

            internal int method_0(Mission mission_0)
            {
                return mission_0.Id;
            }
        }

        private static readonly List<Mission> list_0;

        static PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_REQ()
        {
            list_0 = smethod_0("Data/Missions/Basic");
        }

        public override void Read()
        {
        }

        public override void Run()
        {
            try
            {
                if (Client.Player == null)
                {
                    return;
                }
                foreach (Mission item in list_0)
                {
                    byte[] byte_ = method_0(item);
                    Client.SendPacket(new PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_ACK(byte_, (short)list_0.Count, 1));
                }
            }
            catch (Exception ex)
            {
                CLogger.Print(ex.Message, LoggerType.Error, ex);
            }
        }

        private byte[] method_0(Mission mission_0)
        {
            using (SyncServerPacket syncServerPacket = new SyncServerPacket())
            {
                syncServerPacket.WriteC((byte)mission_0.Id);
                syncServerPacket.WriteC((byte)mission_0.Name.Length);
                syncServerPacket.WriteN(mission_0.Name, mission_0.Name.Length, "UTF-16LE");
                syncServerPacket.WriteC((byte)mission_0.Description.Length);
                syncServerPacket.WriteN(mission_0.Description, mission_0.Description.Length, "UTF-16LE");
                syncServerPacket.WriteB(mission_0.ObjectivesData);
                syncServerPacket.WriteD(mission_0.RewardId);
                syncServerPacket.WriteD(mission_0.RewardCount);
                syncServerPacket.WriteB(new byte[32]);
                return syncServerPacket.ToArray();
            }
        }

        private static List<Mission> smethod_0(string string_0)
        {
            List<Mission> list = new List<Mission>();
            try
            {
                if (!Directory.Exists(string_0))
                {
                    CLogger.Print("Directory not found: " + string_0, LoggerType.Warning);
                    return list;
                }
                string[] files = Directory.GetFiles(string_0, "*.hex");
                for (int i = 0; i < files.Length; i++)
                {
                    string[] array = File.ReadAllLines(files[i]);
                    if (array.Length >= 6)
                    {
                        Mission mission = new Mission();
                        mission.Id = int.Parse(array[0].Split(new char[1] { '=' }, 2)[1]);
                        mission.Name = array[1].Split(new char[1] { '=' }, 2)[1];
                        mission.Description = array[2].Split(new char[1] { '=' }, 2)[1];
                        mission.RewardId = int.Parse(array[3].Split(new char[1] { '=' }, 2)[1]);
                        mission.RewardCount = int.Parse(array[4].Split(new char[1] { '=' }, 2)[1]);
                        Mission mission2 = mission;
                        string[] array2 = string.Join(" ", array.Skip(5)).Split(new char[1] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        List<byte> list2 = new List<byte>();
                        string[] array3 = array2;
                        foreach (string value in array3)
                        {
                            list2.Add(Convert.ToByte(value, 16));
                        }
                        mission2.ObjectivesData = list2.ToArray();
                        list.Add(mission2);
                    }
                }
            }
            catch (Exception ex)
            {
                CLogger.Print("Error loading missions: " + ex.Message, LoggerType.Error, ex);
            }
            return list.OrderBy((Mission mission_0) => mission_0.Id).ToList();
        }
    }
}
