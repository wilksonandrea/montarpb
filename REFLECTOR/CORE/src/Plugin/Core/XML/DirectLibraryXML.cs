namespace Plugin.Core.XML
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    public class DirectLibraryXML
    {
        public static List<string> HashFiles = new List<string>();

        public static bool IsValid(string md5)
        {
            if (string.IsNullOrEmpty(md5))
            {
                return true;
            }
            for (int i = 0; i < HashFiles.Count; i++)
            {
                if (HashFiles[i] == md5)
                {
                    return true;
                }
            }
            return false;
        }

        public static void Load()
        {
            string path = "Data/DirectLibrary.xml";
            if (File.Exists(path))
            {
                smethod_0(path);
            }
            else
            {
                CLogger.Print("File not found: " + path, LoggerType.Warning, null);
            }
            CLogger.Print($"Plugin Loaded: {HashFiles.Count} Lib Hases", LoggerType.Info, null);
        }

        public static void Reload()
        {
            HashFiles.Clear();
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
                                    if ("D3D9".Equals(node2.Name))
                                    {
                                        XmlNamedNodeMap attributes = node2.Attributes;
                                        HashFiles.Add(attributes.GetNamedItem("MD5").Value);
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

