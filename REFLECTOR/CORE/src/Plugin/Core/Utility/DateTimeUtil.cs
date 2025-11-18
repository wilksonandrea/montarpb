namespace Plugin.Core.Utility
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using System;
    using System.Globalization;

    public class DateTimeUtil
    {
        public static DateTime Convert(string Now)
        {
            string[] formats = new string[] { "yyMMddHHmm", "yyMMdd" };
            try
            {
                if (Now.Length < 6)
                {
                    Now = "101010";
                }
                return DateTime.ParseExact(Now, formats, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return new DateTime();
            }
        }

        public static DateTime Now()
        {
            try
            {
                DateTime now = DateTime.Now;
                return (ConfigLoader.CustomYear ? now.AddYears(-ConfigLoader.BackYear) : now);
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return new DateTime();
            }
        }

        public static string Now(string Format) => 
            Now().ToString(Format);
    }
}

