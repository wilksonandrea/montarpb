namespace Server.Auth.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Server.Auth.Network;
    using Server.Auth.Network.ServerPacket;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_REQ : AuthClientPacket
    {
        private static readonly List<Mission> list_0 = smethod_0("Data/Missions/Basic");

        private byte[] method_0(Mission mission_0)
        {
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                packet.WriteC((byte) mission_0.Id);
                packet.WriteC((byte) mission_0.Name.Length);
                packet.WriteN(mission_0.Name, mission_0.Name.Length, "UTF-16LE");
                packet.WriteC((byte) mission_0.Description.Length);
                packet.WriteN(mission_0.Description, mission_0.Description.Length, "UTF-16LE");
                packet.WriteB(mission_0.ObjectivesData);
                packet.WriteD(mission_0.RewardId);
                packet.WriteD(mission_0.RewardCount);
                packet.WriteB(new byte[0x20]);
                return packet.ToArray();
            }
        }

        public override void Read()
        {
        }

        public override void Run()
        {
            try
            {
                if (base.Client.Player != null)
                {
                    foreach (Mission mission in list_0)
                    {
                        byte[] buffer = this.method_0(mission);
                        base.Client.SendPacket(new PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_ACK(buffer, (short) list_0.Count, 1));
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }

        private static List<Mission> smethod_0(string string_0)
        {
            List<Mission> source = new List<Mission>();
            try
            {
                if (Directory.Exists(string_0))
                {
                    string[] files = Directory.GetFiles(string_0, "*.hex");
                    for (int i = 0; i < files.Length; i++)
                    {
                        string[] strArray2 = File.ReadAllLines(files[i]);
                        if (strArray2.Length >= 6)
                        {
                            Mission mission2 = new Mission();
                            char[] separator = new char[] { '=' };
                            mission2.Id = int.Parse(strArray2[0].Split(separator, 2)[1]);
                            char[] chArray2 = new char[] { '=' };
                            mission2.Name = strArray2[1].Split(chArray2, 2)[1];
                            char[] chArray3 = new char[] { '=' };
                            mission2.Description = strArray2[2].Split(chArray3, 2)[1];
                            char[] chArray4 = new char[] { '=' };
                            mission2.RewardId = int.Parse(strArray2[3].Split(chArray4, 2)[1]);
                            char[] chArray5 = new char[] { '=' };
                            mission2.RewardCount = int.Parse(strArray2[4].Split(chArray5, 2)[1]);
                            Mission item = mission2;
                            char[] chArray6 = new char[] { ' ' };
                            List<byte> list3 = new List<byte>();
                            string[] strArray3 = string.Join(" ", strArray2.Skip<string>(5)).Split(chArray6, StringSplitOptions.RemoveEmptyEntries);
                            int index = 0;
                            while (true)
                            {
                                if (index >= strArray3.Length)
                                {
                                    item.ObjectivesData = list3.ToArray();
                                    source.Add(item);
                                    break;
                                }
                                string str = strArray3[index];
                                list3.Add(Convert.ToByte(str, 0x10));
                                index++;
                            }
                        }
                    }
                }
                else
                {
                    CLogger.Print("Directory not found: " + string_0, LoggerType.Warning, null);
                    return source;
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("Error loading missions: " + exception.Message, LoggerType.Error, exception);
            }
            Func<Mission, int> keySelector = Class2.<>9__5_0;
            if (Class2.<>9__5_0 == null)
            {
                Func<Mission, int> local1 = Class2.<>9__5_0;
                keySelector = Class2.<>9__5_0 = new Func<Mission, int>(Class2.<>9.method_0);
            }
            return source.OrderBy<Mission, int>(keySelector).ToList<Mission>();
        }

        [Serializable, CompilerGenerated]
        private sealed class Class2
        {
            public static readonly PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_REQ.Class2 <>9 = new PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_REQ.Class2();
            public static Func<Mission, int> <>9__5_0;

            internal int method_0(Mission mission_0) => 
                mission_0.Id;
        }
    }
}

