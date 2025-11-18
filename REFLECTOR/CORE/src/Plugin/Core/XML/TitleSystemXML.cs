namespace Plugin.Core.XML
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Xml;

    public class TitleSystemXML
    {
        private static List<TitleModel> list_0 = new List<TitleModel>();

        public static void Get2Titles(int titleId1, int titleId2, out TitleModel title1, out TitleModel title2, bool ReturnNull = true)
        {
            if (!ReturnNull)
            {
                title1 = new TitleModel();
                title2 = new TitleModel();
            }
            else
            {
                title1 = null;
                title2 = null;
            }
            if ((titleId1 != 0) || (titleId2 != 0))
            {
                foreach (TitleModel model in list_0)
                {
                    if (model.Id == titleId1)
                    {
                        title1 = model;
                        continue;
                    }
                    if (model.Id == titleId2)
                    {
                        title2 = model;
                    }
                }
            }
        }

        public static void Get3Titles(int titleId1, int titleId2, int titleId3, out TitleModel title1, out TitleModel title2, out TitleModel title3, bool ReturnNull)
        {
            if (!ReturnNull)
            {
                title1 = new TitleModel();
                title2 = new TitleModel();
                title3 = new TitleModel();
            }
            else
            {
                title1 = null;
                title2 = null;
                title3 = null;
            }
            if ((titleId1 != 0) || ((titleId2 != 0) || (titleId3 != 0)))
            {
                foreach (TitleModel model in list_0)
                {
                    if (model.Id == titleId1)
                    {
                        title1 = model;
                        continue;
                    }
                    if (model.Id == titleId2)
                    {
                        title2 = model;
                        continue;
                    }
                    if (model.Id == titleId3)
                    {
                        title3 = model;
                    }
                }
            }
        }

        public static TitleModel GetTitle(int titleId, bool ReturnNull = true)
        {
            TitleModel model2;
            if (titleId == 0)
            {
                return (ReturnNull ? null : new TitleModel());
            }
            using (List<TitleModel>.Enumerator enumerator = list_0.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        TitleModel current = enumerator.Current;
                        if (current.Id != titleId)
                        {
                            continue;
                        }
                        model2 = current;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return model2;
        }

        public static void Load()
        {
            string path = "Data/Titles/System.xml";
            if (File.Exists(path))
            {
                smethod_0(path);
            }
            else
            {
                CLogger.Print("File not found: " + path, LoggerType.Warning, null);
            }
            CLogger.Print($"Plugin Loaded: {list_0.Count} Title System", LoggerType.Info, null);
        }

        public static void Reload()
        {
            list_0.Clear();
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
                                    if ("Title".Equals(node2.Name))
                                    {
                                        XmlNamedNodeMap attributes = node2.Attributes;
                                        TitleModel model1 = new TitleModel(int.Parse(attributes.GetNamedItem("Id").Value));
                                        model1.ClassId = int.Parse(attributes.GetNamedItem("List").Value);
                                        model1.Ribbon = int.Parse(attributes.GetNamedItem("Ribbon").Value);
                                        model1.Ensign = int.Parse(attributes.GetNamedItem("Ensign").Value);
                                        model1.Medal = int.Parse(attributes.GetNamedItem("Medal").Value);
                                        model1.MasterMedal = int.Parse(attributes.GetNamedItem("MasterMedal").Value);
                                        model1.Rank = int.Parse(attributes.GetNamedItem("Rank").Value);
                                        model1.Slot = int.Parse(attributes.GetNamedItem("Slot").Value);
                                        model1.Req1 = int.Parse(attributes.GetNamedItem("ReqT1").Value);
                                        model1.Req2 = int.Parse(attributes.GetNamedItem("ReqT2").Value);
                                        TitleModel item = model1;
                                        list_0.Add(item);
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

