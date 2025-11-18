using Plugin.Core;
using Plugin.Core.Enums;
using System;
using System.Globalization;

namespace Plugin.Core.Utility
{
	public class DateTimeUtil
	{
		public DateTimeUtil()
		{
		}

		public static DateTime Convert(string Now)
		{
			DateTime dateTime;
			string[] strArrays = new string[] { "yyMMddHHmm", "yyMMdd" };
			try
			{
				if (Now.Length < 6)
				{
					Now = "101010";
				}
				DateTime dateTime1 = DateTime.ParseExact(Now, strArrays, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
				dateTime = dateTime1;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				dateTime = new DateTime();
			}
			return dateTime;
		}

		public static DateTime Now()
		{
			DateTime dateTime;
			try
			{
				DateTime now = DateTime.Now;
				dateTime = (ConfigLoader.CustomYear ? now.AddYears(-ConfigLoader.BackYear) : now);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				dateTime = new DateTime();
			}
			return dateTime;
		}

		public static string Now(string Format)
		{
			return DateTimeUtil.Now().ToString(Format);
		}
	}
}