using System;
using System.Globalization;
using Plugin.Core.Enums;

namespace Plugin.Core.Utility;

public class DateTimeUtil
{
	public static DateTime Now()
	{
		try
		{
			DateTime now = DateTime.Now;
			return ConfigLoader.CustomYear ? now.AddYears(-ConfigLoader.BackYear) : now;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return default(DateTime);
		}
	}

	public static string Now(string Format)
	{
		return Now().ToString(Format);
	}

	public static DateTime Convert(string Now)
	{
		string[] formats = new string[2] { "yyMMddHHmm", "yyMMdd" };
		try
		{
			if (Now.Length < 6)
			{
				Now = "101010";
			}
			return DateTime.ParseExact(Now, formats, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return default(DateTime);
		}
	}
}
