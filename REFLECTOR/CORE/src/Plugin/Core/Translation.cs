namespace Plugin.Core
{
    using Plugin.Core.Enums;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public static class Translation
    {
        public static SortedList<string, string> Strings = new SortedList<string, string>();

        static Translation()
        {
            smethod_0();
        }

        public static string GetLabel(string Title)
        {
            try
            {
                string str;
                return (!Strings.TryGetValue(Title, out str) ? Title : str.Replace(@"\n", '\n'.ToString()));
            }
            catch
            {
                return Title;
            }
        }

        public static string GetLabel(string Title, params object[] Argumens) => 
            string.Format(GetLabel(Title), Argumens);

        private static void smethod_0()
        {
            string path = "Config/Translate/Strings.ini";
            if (File.Exists(path))
            {
                smethod_1(path);
            }
            else
            {
                CLogger.Print("File not found: " + path, LoggerType.Warning, null);
            }
        }

        private static void smethod_1(string string_0)
        {
            try
            {
                using (StreamReader reader = new StreamReader(string_0))
                {
                    while (true)
                    {
                        string str = reader.ReadLine();
                        if (str == null)
                        {
                            reader.Close();
                            break;
                        }
                        int index = str.IndexOf(" = ");
                        if (index >= 0)
                        {
                            string key = str.Substring(0, index);
                            Strings.Add(key, str.Substring(index + 3));
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("Translation: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

