namespace Server.Match.Data.XML
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Match.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    public class CharaStructureXML
    {
        public static List<CharaModel> Charas = new List<CharaModel>();

        public static int GetCharaHP(int CharaId)
        {
            int hP;
            using (List<CharaModel>.Enumerator enumerator = Charas.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        CharaModel current = enumerator.Current;
                        if (current.Id != CharaId)
                        {
                            continue;
                        }
                        hP = current.HP;
                    }
                    else
                    {
                        return 100;
                    }
                    break;
                }
            }
            return hP;
        }

        public static void Load()
        {
            string path = "Data/Match/CharaHealth.xml";
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
            Charas.Clear();
            Load();
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
                                    if ("Chara".Equals(node2.Name))
                                    {
                                        XmlNamedNodeMap attributes = node2.Attributes;
                                        CharaModel model1 = new CharaModel();
                                        model1.Id = int.Parse(attributes.GetNamedItem("Id").Value);
                                        model1.HP = int.Parse(attributes.GetNamedItem("HP").Value);
                                        CharaModel item = model1;
                                        Charas.Add(item);
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
    }
}

