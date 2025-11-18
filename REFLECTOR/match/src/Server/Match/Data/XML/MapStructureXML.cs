namespace Server.Match.Data.XML
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.SharpDX;
    using Server.Match.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    public class MapStructureXML
    {
        public static List<MapModel> Maps = new List<MapModel>();

        public static MapModel GetMapId(int MapId)
        {
            List<MapModel> maps = Maps;
            lock (maps)
            {
                using (List<MapModel>.Enumerator enumerator = Maps.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        MapModel current = enumerator.Current;
                        if (current.Id == MapId)
                        {
                            return current;
                        }
                    }
                }
                return null;
            }
        }

        public static void Load()
        {
            string path = "Data/Match/MapStructure.xml";
            if (File.Exists(path))
            {
                smethod_0(path);
            }
            else
            {
                CLogger.Print("File not found: " + path, LoggerType.Warning, null);
            }
        }

        public static void Reload()
        {
            Maps.Clear();
            Load();
        }

        public static void SetObjectives(ObjectModel obj, RoomModel room)
        {
            if (obj.UltraSync != 0)
            {
                if ((obj.UltraSync == 1) || (obj.UltraSync == 3))
                {
                    room.Bar1 = obj.Life;
                    room.Default1 = room.Bar1;
                }
                else if ((obj.UltraSync == 2) || (obj.UltraSync == 4))
                {
                    room.Bar2 = obj.Life;
                    room.Default2 = room.Bar2;
                }
            }
        }

        private static void smethod_0(string string_0)
        {
            XmlDocument document = new XmlDocument();
            using (FileStream stream = new FileStream(string_0, FileMode.Open))
            {
                if (stream.Length == 0)
                {
                    CLogger.Print("File is empty: " + string_0, LoggerType.Warning, null);
                }
                else
                {
                    try
                    {
                        document.Load(stream);
                        for (XmlNode node = document.FirstChild; node != null; node = node.NextSibling)
                        {
                            if ("List".Equals(node.Name))
                            {
                                for (XmlNode node2 = node.FirstChild; node2 != null; node2 = node2.NextSibling)
                                {
                                    if ("Map".Equals(node2.Name))
                                    {
                                        XmlNamedNodeMap attributes = node2.Attributes;
                                        MapModel model1 = new MapModel();
                                        model1.Id = int.Parse(attributes.GetNamedItem("Id").Value);
                                        model1.Objects = new List<ObjectModel>();
                                        model1.Bombs = new List<BombPosition>();
                                        MapModel model = model1;
                                        smethod_1(node2, model);
                                        smethod_2(node2, model);
                                        Maps.Add(model);
                                    }
                                }
                            }
                        }
                    }
                    catch (XmlException exception)
                    {
                        CLogger.Print(exception.Message, LoggerType.Error, exception);
                    }
                }
                stream.Dispose();
                stream.Close();
            }
        }

        private static void smethod_1(XmlNode xmlNode_0, MapModel mapModel_0)
        {
            for (XmlNode node = xmlNode_0.FirstChild; node != null; node = node.NextSibling)
            {
                if ("BombPositions".Equals(node.Name))
                {
                    for (XmlNode node2 = node.FirstChild; node2 != null; node2 = node2.NextSibling)
                    {
                        if ("Bomb".Equals(node2.Name))
                        {
                            XmlNamedNodeMap attributes = node2.Attributes;
                            BombPosition position1 = new BombPosition();
                            position1.X = float.Parse(attributes.GetNamedItem("X").Value);
                            position1.Y = float.Parse(attributes.GetNamedItem("Y").Value);
                            position1.Z = float.Parse(attributes.GetNamedItem("Z").Value);
                            BombPosition item = position1;
                            item.Position = new Half3(item.X, item.Y, item.Z);
                            if ((item.X == 0f) && ((item.Y == 0f) && (item.Z == 0f)))
                            {
                                item.EveryWhere = true;
                            }
                            mapModel_0.Bombs.Add(item);
                        }
                    }
                }
            }
        }

        private static void smethod_2(XmlNode xmlNode_0, MapModel mapModel_0)
        {
            for (XmlNode node = xmlNode_0.FirstChild; node != null; node = node.NextSibling)
            {
                if ("Objects".Equals(node.Name))
                {
                    for (XmlNode node2 = node.FirstChild; node2 != null; node2 = node2.NextSibling)
                    {
                        if ("Obj".Equals(node2.Name))
                        {
                            XmlNamedNodeMap attributes = node2.Attributes;
                            ObjectModel model1 = new ObjectModel(bool.Parse(attributes.GetNamedItem("NeedSync").Value));
                            model1.Id = int.Parse(attributes.GetNamedItem("Id").Value);
                            model1.Life = int.Parse(attributes.GetNamedItem("Life").Value);
                            model1.Animation = int.Parse(attributes.GetNamedItem("Animation").Value);
                            ObjectModel model = model1;
                            if (model.Life > -1)
                            {
                                model.Destroyable = true;
                            }
                            if (model.Animation > 0xff)
                            {
                                if (model.Animation == 0x100)
                                {
                                    model.UltraSync = 1;
                                }
                                else if (model.Animation == 0x101)
                                {
                                    model.UltraSync = 2;
                                }
                                else if (model.Animation == 0x102)
                                {
                                    model.UltraSync = 3;
                                }
                                else if (model.Animation == 0x103)
                                {
                                    model.UltraSync = 4;
                                }
                                model.Animation = 0xff;
                            }
                            smethod_3(node2, model);
                            smethod_4(node2, model);
                            mapModel_0.Objects.Add(model);
                        }
                    }
                }
            }
        }

        private static void smethod_3(XmlNode xmlNode_0, ObjectModel objectModel_0)
        {
            for (XmlNode node = xmlNode_0.FirstChild; node != null; node = node.NextSibling)
            {
                if ("Anims".Equals(node.Name))
                {
                    for (XmlNode node2 = node.FirstChild; node2 != null; node2 = node2.NextSibling)
                    {
                        if ("Sync".Equals(node2.Name))
                        {
                            XmlNamedNodeMap attributes = node2.Attributes;
                            AnimModel model1 = new AnimModel();
                            model1.Id = int.Parse(attributes.GetNamedItem("Id").Value);
                            model1.Duration = float.Parse(attributes.GetNamedItem("Date").Value);
                            model1.NextAnim = int.Parse(attributes.GetNamedItem("Next").Value);
                            model1.OtherObj = int.Parse(attributes.GetNamedItem("OtherOBJ").Value);
                            model1.OtherAnim = int.Parse(attributes.GetNamedItem("OtherANIM").Value);
                            AnimModel item = model1;
                            if (item.Id == 0)
                            {
                                objectModel_0.NoInstaSync = true;
                            }
                            if (item.Id != 0xff)
                            {
                                objectModel_0.UpdateId = 3;
                            }
                            objectModel_0.Animations.Add(item);
                        }
                    }
                }
            }
        }

        private static void smethod_4(XmlNode xmlNode_0, ObjectModel objectModel_0)
        {
            for (XmlNode node = xmlNode_0.FirstChild; node != null; node = node.NextSibling)
            {
                if ("DestroyEffects".Equals(node.Name))
                {
                    for (XmlNode node2 = node.FirstChild; node2 != null; node2 = node2.NextSibling)
                    {
                        if ("Effect".Equals(node2.Name))
                        {
                            XmlNamedNodeMap attributes = node2.Attributes;
                            DeffectModel model1 = new DeffectModel();
                            model1.Id = int.Parse(attributes.GetNamedItem("Id").Value);
                            model1.Life = int.Parse(attributes.GetNamedItem("Percent").Value);
                            DeffectModel item = model1;
                            objectModel_0.Effects.Add(item);
                        }
                    }
                }
            }
        }
    }
}

