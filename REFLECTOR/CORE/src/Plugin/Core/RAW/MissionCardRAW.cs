namespace Plugin.Core.RAW
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Network;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    public class MissionCardRAW
    {
        private static List<MissionItemAward> list_0 = new List<MissionItemAward>();
        private static List<MissionCardModel> list_1 = new List<MissionCardModel>();
        private static List<MissionCardAwards> list_2 = new List<MissionCardAwards>();

        public static MissionCardAwards GetAward(int mission, int cartao)
        {
            MissionCardAwards awards2;
            using (List<MissionCardAwards>.Enumerator enumerator = list_2.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        MissionCardAwards current = enumerator.Current;
                        if ((current.Id != mission) || (current.Card != cartao))
                        {
                            continue;
                        }
                        awards2 = current;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return awards2;
        }

        public static List<MissionCardModel> GetCards(int MissionId)
        {
            List<MissionCardModel> list = new List<MissionCardModel>();
            List<MissionCardModel> list2 = list_1;
            lock (list2)
            {
                foreach (MissionCardModel model in list_1)
                {
                    if (model.MissionId == MissionId)
                    {
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        public static List<MissionCardModel> GetCards(List<MissionCardModel> Cards, int CardBasicId)
        {
            if (CardBasicId == -1)
            {
                return Cards;
            }
            List<MissionCardModel> list = new List<MissionCardModel>();
            foreach (MissionCardModel model in list_1)
            {
                if (((CardBasicId >= 0) && (model.CardBasicId == CardBasicId)) || (CardBasicId == -1))
                {
                    list.Add(model);
                }
            }
            return list;
        }

        public static List<MissionCardModel> GetCards(int MissionId, int CardBasicId)
        {
            List<MissionCardModel> list = new List<MissionCardModel>();
            List<MissionCardModel> list2 = list_1;
            lock (list2)
            {
                foreach (MissionCardModel model in list_1)
                {
                    if ((model.MissionId == MissionId) && (((CardBasicId >= 0) && (model.CardBasicId == CardBasicId)) || (CardBasicId == -1)))
                    {
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        public static List<ItemsModel> GetMissionAwards(int MissionId)
        {
            List<ItemsModel> list = new List<ItemsModel>();
            List<MissionItemAward> list2 = list_0;
            lock (list2)
            {
                foreach (MissionItemAward award in list_0)
                {
                    if (award.MissionId == MissionId)
                    {
                        list.Add(award.Item);
                    }
                }
            }
            return list;
        }

        public static void LoadBasicCards(int Type)
        {
            smethod_0("TutorialCard_Russia", Type);
            smethod_0("Dino_Tutorial", Type);
            smethod_0("Human_Tutorial", Type);
            smethod_0("AssaultCard", Type);
            smethod_0("BackUpCard", Type);
            smethod_0("InfiltrationCard", Type);
            smethod_0("SpecialCard", Type);
            smethod_0("DefconCard", Type);
            smethod_0("Commissioned_o", Type);
            smethod_0("Company_o", Type);
            smethod_0("Field_o", Type);
            smethod_0("EventCard", Type);
            smethod_0("Dino_Basic", Type);
            smethod_0("Human_Basic", Type);
            smethod_0("Dino_Intensify", Type);
            smethod_0("Human_Intensify", Type);
            CLogger.Print($"Plugin Loaded: {list_1.Count} Mission Card List", LoggerType.Info, null);
            if (Type == 1)
            {
                CLogger.Print($"Plugin Loaded: {list_2.Count} Mission Card Awards", LoggerType.Info, null);
            }
            else if (Type == 2)
            {
                CLogger.Print($"Plugin Loaded: {list_0.Count} Mission Reward Items", LoggerType.Info, null);
            }
        }

        private static void smethod_0(string string_0, int int_0)
        {
            string path = "Data/Missions/" + string_0 + ".mqf";
            if (File.Exists(path))
            {
                smethod_2(path, string_0, int_0);
            }
            else
            {
                CLogger.Print("File not found: " + path, LoggerType.Warning, null);
            }
        }

        private static int smethod_1(string string_0)
        {
            int num = 0;
            if (string_0 != null)
            {
                char ch;
                switch (string_0.Length)
                {
                    case 7:
                        if (string_0 == "Field_o")
                        {
                            num = 12;
                        }
                        break;

                    case 9:
                        ch = string_0[0];
                        if (ch != 'C')
                        {
                            if ((ch == 'E') && (string_0 == "EventCard"))
                            {
                                num = 13;
                            }
                        }
                        else if (string_0 == "Company_o")
                        {
                            num = 11;
                        }
                        break;

                    case 10:
                        ch = string_0[1];
                        if (ch == 'a')
                        {
                            if (string_0 == "BackUpCard")
                            {
                                num = 6;
                            }
                        }
                        else if (ch != 'e')
                        {
                            if ((ch == 'i') && (string_0 == "Dino_Basic"))
                            {
                                num = 14;
                            }
                        }
                        else if (string_0 == "DefconCard")
                        {
                            num = 9;
                        }
                        break;

                    case 11:
                        ch = string_0[0];
                        if (ch == 'A')
                        {
                            if (string_0 == "AssaultCard")
                            {
                                num = 5;
                            }
                        }
                        else if (ch != 'H')
                        {
                            if ((ch == 'S') && (string_0 == "SpecialCard"))
                            {
                                num = 8;
                            }
                        }
                        else if (string_0 == "Human_Basic")
                        {
                            num = 15;
                        }
                        break;

                    case 13:
                        if (string_0 == "Dino_Tutorial")
                        {
                            num = 2;
                        }
                        break;

                    case 14:
                        ch = string_0[0];
                        if (ch == 'C')
                        {
                            if (string_0 == "Commissioned_o")
                            {
                                num = 10;
                            }
                        }
                        else if (ch != 'D')
                        {
                            if ((ch == 'H') && (string_0 == "Human_Tutorial"))
                            {
                                num = 3;
                            }
                        }
                        else if (string_0 == "Dino_Intensify")
                        {
                            num = 0x10;
                        }
                        break;

                    case 15:
                        if (string_0 == "Human_Intensify")
                        {
                            num = 0x11;
                        }
                        break;

                    case 0x10:
                        if (string_0 == "InfiltrationCard")
                        {
                            num = 7;
                        }
                        break;

                    case 0x13:
                        if (string_0 == "TutorialCard_Russia")
                        {
                            num = 1;
                        }
                        break;

                    default:
                        break;
                }
            }
            return num;
        }

        private static void smethod_2(string string_0, string string_1, int int_0)
        {
            byte[] buffer;
            int num = smethod_1(string_1);
            if (num == 0)
            {
                CLogger.Print("Invalid: " + string_1, LoggerType.Warning, null);
            }
            try
            {
                buffer = File.ReadAllBytes(string_0);
            }
            catch
            {
                buffer = new byte[0];
            }
            if (buffer.Length != 0)
            {
                try
                {
                    SyncClientPacket packet = new SyncClientPacket(buffer);
                    packet.ReadS(4);
                    int num2 = packet.ReadD();
                    packet.ReadB(0x10);
                    int num3 = 0;
                    int num4 = 0;
                    int num6 = 0;
                    while (true)
                    {
                        if (num6 >= 40)
                        {
                            int num5 = (num2 == 2) ? 5 : 1;
                            int num12 = 0;
                            while (true)
                            {
                                if (num12 >= 10)
                                {
                                    if (num2 == 2)
                                    {
                                        packet.ReadD();
                                        packet.ReadB(8);
                                        for (int i = 0; i < 5; i++)
                                        {
                                            int num18 = packet.ReadD();
                                            packet.ReadD();
                                            int num19 = packet.ReadD();
                                            uint num20 = packet.ReadUD();
                                            if ((num18 > 0) && (int_0 == 1))
                                            {
                                                MissionItemAward award1 = new MissionItemAward();
                                                award1.MissionId = num;
                                                award1.Item = new ItemsModel(num19, "Mission Item", ItemEquipType.Durable, num20);
                                                MissionItemAward award = award1;
                                                list_0.Add(award);
                                            }
                                        }
                                    }
                                    break;
                                }
                                int num13 = packet.ReadD();
                                int num14 = packet.ReadD();
                                int num15 = packet.ReadD();
                                int num16 = 0;
                                while (true)
                                {
                                    if (num16 >= num5)
                                    {
                                        if (int_0 == 1)
                                        {
                                            MissionCardAwards awards1 = new MissionCardAwards();
                                            awards1.Id = num;
                                            awards1.Card = num12;
                                            awards1.Exp = (num2 == 1) ? (num14 * 10) : num14;
                                            MissionCardAwards local2 = awards1;
                                            local2.Gold = num13;
                                            MissionCardAwards awards = local2;
                                            smethod_3(awards, num15);
                                            if (!awards.Unusable())
                                            {
                                                list_2.Add(awards);
                                            }
                                        }
                                        num12++;
                                        break;
                                    }
                                    packet.ReadD();
                                    packet.ReadD();
                                    packet.ReadD();
                                    packet.ReadD();
                                    num16++;
                                }
                            }
                            break;
                        }
                        int num7 = num4++;
                        int num1 = num3;
                        if (num4 == 4)
                        {
                            num4 = 0;
                            num3++;
                        }
                        packet.ReadUH();
                        int num8 = packet.ReadC();
                        int num9 = packet.ReadC();
                        int num10 = packet.ReadC();
                        ClassType type = (ClassType) packet.ReadC();
                        int num11 = packet.ReadUH();
                        MissionCardModel model1 = new MissionCardModel(num1, num7);
                        model1.MapId = num9;
                        model1.WeaponReq = type;
                        model1.WeaponReqId = num11;
                        model1.MissionType = (MissionType) num8;
                        model1.MissionLimit = num10;
                        model1.MissionId = num;
                        MissionCardModel item = model1;
                        list_1.Add(item);
                        if (num2 == 1)
                        {
                            packet.ReadB(0x18);
                        }
                        num6++;
                    }
                }
                catch (XmlException exception)
                {
                    CLogger.Print("File error: " + string_0 + "; " + exception.Message, LoggerType.Error, exception);
                }
            }
        }

        private static void smethod_3(MissionCardAwards missionCardAwards_0, int int_0)
        {
            if (int_0 != 0)
            {
                if ((int_0 >= 1) && (int_0 <= 50))
                {
                    missionCardAwards_0.Ribbon++;
                }
                else if ((int_0 >= 0x33) && (int_0 <= 100))
                {
                    missionCardAwards_0.Ensign++;
                }
                else if ((int_0 >= 0x65) && (int_0 <= 0x74))
                {
                    missionCardAwards_0.Medal++;
                }
            }
        }
    }
}

