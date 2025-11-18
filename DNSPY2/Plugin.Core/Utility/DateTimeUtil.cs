using System;
using System.Globalization;
using Plugin.Core.Enums;

namespace Plugin.Core.Utility
{
	// Token: 0x0200002F RID: 47
	public class DateTimeUtil
	{
		// Token: 0x060001C8 RID: 456 RVA: 0x000186F0 File Offset: 0x000168F0
		public static DateTime Now()
		{
			DateTime dateTime;
			try
			{
				DateTime now = DateTime.Now;
				dateTime = (ConfigLoader.CustomYear ? now.AddYears(-ConfigLoader.BackYear) : now);
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				dateTime = default(DateTime);
			}
			return dateTime;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00018748 File Offset: 0x00016948
		public static string Now(string Format)
		{
			return DateTimeUtil.Now().ToString(Format);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00018764 File Offset: 0x00016964
		public static DateTime Convert(string Now)
		{
			string[] array = new string[] { "yyMMddHHmm", "yyMMdd" };
			DateTime dateTime2;
			try
			{
				if (Now.Length < 6)
				{
					Now = "101010";
				}
				DateTime dateTime = DateTime.ParseExact(Now, array, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
				dateTime2 = dateTime;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				dateTime2 = default(DateTime);
			}
			return dateTime2;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00002116 File Offset: 0x00000316
		public DateTimeUtil()
		{
		}
	}
}
