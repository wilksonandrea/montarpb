using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	// Token: 0x020003C7 RID: 967
	[ComVisible(true)]
	[Serializable]
	public class JulianCalendar : Calendar
	{
		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06003064 RID: 12388 RVA: 0x000B9A6C File Offset: 0x000B7C6C
		[ComVisible(false)]
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return DateTime.MinValue;
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06003065 RID: 12389 RVA: 0x000B9A73 File Offset: 0x000B7C73
		[ComVisible(false)]
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return DateTime.MaxValue;
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06003066 RID: 12390 RVA: 0x000B9A7A File Offset: 0x000B7C7A
		[ComVisible(false)]
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.SolarCalendar;
			}
		}

		// Token: 0x06003067 RID: 12391 RVA: 0x000B9A7D File Offset: 0x000B7C7D
		public JulianCalendar()
		{
			this.twoDigitYearMax = 2029;
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06003068 RID: 12392 RVA: 0x000B9A9B File Offset: 0x000B7C9B
		internal override int ID
		{
			get
			{
				return 13;
			}
		}

		// Token: 0x06003069 RID: 12393 RVA: 0x000B9A9F File Offset: 0x000B7C9F
		internal static void CheckEraRange(int era)
		{
			if (era != 0 && era != JulianCalendar.JulianEra)
			{
				throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
			}
		}

		// Token: 0x0600306A RID: 12394 RVA: 0x000B9AC4 File Offset: 0x000B7CC4
		internal void CheckYearEraRange(int year, int era)
		{
			JulianCalendar.CheckEraRange(era);
			if (year <= 0 || year > this.MaxYear)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 1, this.MaxYear));
			}
		}

		// Token: 0x0600306B RID: 12395 RVA: 0x000B9B14 File Offset: 0x000B7D14
		internal static void CheckMonthRange(int month)
		{
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("ArgumentOutOfRange_Month"));
			}
		}

		// Token: 0x0600306C RID: 12396 RVA: 0x000B9B34 File Offset: 0x000B7D34
		internal static void CheckDayRange(int year, int month, int day)
		{
			if (year == 1 && month == 1 && day < 3)
			{
				throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("ArgumentOutOfRange_BadYearMonthDay"));
			}
			int[] array = ((year % 4 == 0) ? JulianCalendar.DaysToMonth366 : JulianCalendar.DaysToMonth365);
			int num = array[month] - array[month - 1];
			if (day < 1 || day > num)
			{
				throw new ArgumentOutOfRangeException("day", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 1, num));
			}
		}

		// Token: 0x0600306D RID: 12397 RVA: 0x000B9BB4 File Offset: 0x000B7DB4
		internal static int GetDatePart(long ticks, int part)
		{
			long num = ticks + 1728000000000L;
			int i = (int)(num / 864000000000L);
			int num2 = i / 1461;
			i -= num2 * 1461;
			int num3 = i / 365;
			if (num3 == 4)
			{
				num3 = 3;
			}
			if (part == 0)
			{
				return num2 * 4 + num3 + 1;
			}
			i -= num3 * 365;
			if (part == 1)
			{
				return i + 1;
			}
			int[] array = ((num3 == 3) ? JulianCalendar.DaysToMonth366 : JulianCalendar.DaysToMonth365);
			int num4 = i >> 6;
			while (i >= array[num4])
			{
				num4++;
			}
			if (part == 2)
			{
				return num4;
			}
			return i - array[num4 - 1] + 1;
		}

		// Token: 0x0600306E RID: 12398 RVA: 0x000B9C58 File Offset: 0x000B7E58
		internal static long DateToTicks(int year, int month, int day)
		{
			int[] array = ((year % 4 == 0) ? JulianCalendar.DaysToMonth366 : JulianCalendar.DaysToMonth365);
			int num = year - 1;
			int num2 = num * 365 + num / 4 + array[month - 1] + day - 1;
			return (long)(num2 - 2) * 864000000000L;
		}

		// Token: 0x0600306F RID: 12399 RVA: 0x000B9CA0 File Offset: 0x000B7EA0
		public override DateTime AddMonths(DateTime time, int months)
		{
			if (months < -120000 || months > 120000)
			{
				throw new ArgumentOutOfRangeException("months", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), -120000, 120000));
			}
			int num = JulianCalendar.GetDatePart(time.Ticks, 0);
			int num2 = JulianCalendar.GetDatePart(time.Ticks, 2);
			int num3 = JulianCalendar.GetDatePart(time.Ticks, 3);
			int num4 = num2 - 1 + months;
			if (num4 >= 0)
			{
				num2 = num4 % 12 + 1;
				num += num4 / 12;
			}
			else
			{
				num2 = 12 + (num4 + 1) % 12;
				num += (num4 - 11) / 12;
			}
			int[] array = ((num % 4 == 0 && (num % 100 != 0 || num % 400 == 0)) ? JulianCalendar.DaysToMonth366 : JulianCalendar.DaysToMonth365);
			int num5 = array[num2] - array[num2 - 1];
			if (num3 > num5)
			{
				num3 = num5;
			}
			long num6 = JulianCalendar.DateToTicks(num, num2, num3) + time.Ticks % 864000000000L;
			Calendar.CheckAddResult(num6, this.MinSupportedDateTime, this.MaxSupportedDateTime);
			return new DateTime(num6);
		}

		// Token: 0x06003070 RID: 12400 RVA: 0x000B9DB5 File Offset: 0x000B7FB5
		public override DateTime AddYears(DateTime time, int years)
		{
			return this.AddMonths(time, years * 12);
		}

		// Token: 0x06003071 RID: 12401 RVA: 0x000B9DC2 File Offset: 0x000B7FC2
		public override int GetDayOfMonth(DateTime time)
		{
			return JulianCalendar.GetDatePart(time.Ticks, 3);
		}

		// Token: 0x06003072 RID: 12402 RVA: 0x000B9DD1 File Offset: 0x000B7FD1
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			return (DayOfWeek)(time.Ticks / 864000000000L + 1L) % (DayOfWeek)7;
		}

		// Token: 0x06003073 RID: 12403 RVA: 0x000B9DEA File Offset: 0x000B7FEA
		public override int GetDayOfYear(DateTime time)
		{
			return JulianCalendar.GetDatePart(time.Ticks, 1);
		}

		// Token: 0x06003074 RID: 12404 RVA: 0x000B9DFC File Offset: 0x000B7FFC
		public override int GetDaysInMonth(int year, int month, int era)
		{
			this.CheckYearEraRange(year, era);
			JulianCalendar.CheckMonthRange(month);
			int[] array = ((year % 4 == 0) ? JulianCalendar.DaysToMonth366 : JulianCalendar.DaysToMonth365);
			return array[month] - array[month - 1];
		}

		// Token: 0x06003075 RID: 12405 RVA: 0x000B9E32 File Offset: 0x000B8032
		public override int GetDaysInYear(int year, int era)
		{
			if (!this.IsLeapYear(year, era))
			{
				return 365;
			}
			return 366;
		}

		// Token: 0x06003076 RID: 12406 RVA: 0x000B9E49 File Offset: 0x000B8049
		public override int GetEra(DateTime time)
		{
			return JulianCalendar.JulianEra;
		}

		// Token: 0x06003077 RID: 12407 RVA: 0x000B9E50 File Offset: 0x000B8050
		public override int GetMonth(DateTime time)
		{
			return JulianCalendar.GetDatePart(time.Ticks, 2);
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06003078 RID: 12408 RVA: 0x000B9E5F File Offset: 0x000B805F
		public override int[] Eras
		{
			get
			{
				return new int[] { JulianCalendar.JulianEra };
			}
		}

		// Token: 0x06003079 RID: 12409 RVA: 0x000B9E6F File Offset: 0x000B806F
		public override int GetMonthsInYear(int year, int era)
		{
			this.CheckYearEraRange(year, era);
			return 12;
		}

		// Token: 0x0600307A RID: 12410 RVA: 0x000B9E7B File Offset: 0x000B807B
		public override int GetYear(DateTime time)
		{
			return JulianCalendar.GetDatePart(time.Ticks, 0);
		}

		// Token: 0x0600307B RID: 12411 RVA: 0x000B9E8A File Offset: 0x000B808A
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			JulianCalendar.CheckMonthRange(month);
			if (this.IsLeapYear(year, era))
			{
				JulianCalendar.CheckDayRange(year, month, day);
				return month == 2 && day == 29;
			}
			JulianCalendar.CheckDayRange(year, month, day);
			return false;
		}

		// Token: 0x0600307C RID: 12412 RVA: 0x000B9EBA File Offset: 0x000B80BA
		[ComVisible(false)]
		public override int GetLeapMonth(int year, int era)
		{
			this.CheckYearEraRange(year, era);
			return 0;
		}

		// Token: 0x0600307D RID: 12413 RVA: 0x000B9EC5 File Offset: 0x000B80C5
		public override bool IsLeapMonth(int year, int month, int era)
		{
			this.CheckYearEraRange(year, era);
			JulianCalendar.CheckMonthRange(month);
			return false;
		}

		// Token: 0x0600307E RID: 12414 RVA: 0x000B9ED6 File Offset: 0x000B80D6
		public override bool IsLeapYear(int year, int era)
		{
			this.CheckYearEraRange(year, era);
			return year % 4 == 0;
		}

		// Token: 0x0600307F RID: 12415 RVA: 0x000B9EE8 File Offset: 0x000B80E8
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			this.CheckYearEraRange(year, era);
			JulianCalendar.CheckMonthRange(month);
			JulianCalendar.CheckDayRange(year, month, day);
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 0, 999));
			}
			if (hour >= 0 && hour < 24 && minute >= 0 && minute < 60 && second >= 0 && second < 60)
			{
				return new DateTime(JulianCalendar.DateToTicks(year, month, day) + new TimeSpan(0, hour, minute, second, millisecond).Ticks);
			}
			throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("ArgumentOutOfRange_BadHourMinuteSecond"));
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06003080 RID: 12416 RVA: 0x000B9F9F File Offset: 0x000B819F
		// (set) Token: 0x06003081 RID: 12417 RVA: 0x000B9FA8 File Offset: 0x000B81A8
		public override int TwoDigitYearMax
		{
			get
			{
				return this.twoDigitYearMax;
			}
			set
			{
				base.VerifyWritable();
				if (value < 99 || value > this.MaxYear)
				{
					throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 99, this.MaxYear));
				}
				this.twoDigitYearMax = value;
			}
		}

		// Token: 0x06003082 RID: 12418 RVA: 0x000BA004 File Offset: 0x000B8204
		public override int ToFourDigitYear(int year)
		{
			if (year < 0)
			{
				throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (year > this.MaxYear)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Bounds_Lower_Upper"), 1, this.MaxYear));
			}
			return base.ToFourDigitYear(year);
		}

		// Token: 0x06003083 RID: 12419 RVA: 0x000BA06A File Offset: 0x000B826A
		// Note: this type is marked as 'beforefieldinit'.
		static JulianCalendar()
		{
		}

		// Token: 0x040014AC RID: 5292
		public static readonly int JulianEra = 1;

		// Token: 0x040014AD RID: 5293
		private const int DatePartYear = 0;

		// Token: 0x040014AE RID: 5294
		private const int DatePartDayOfYear = 1;

		// Token: 0x040014AF RID: 5295
		private const int DatePartMonth = 2;

		// Token: 0x040014B0 RID: 5296
		private const int DatePartDay = 3;

		// Token: 0x040014B1 RID: 5297
		private const int JulianDaysPerYear = 365;

		// Token: 0x040014B2 RID: 5298
		private const int JulianDaysPer4Years = 1461;

		// Token: 0x040014B3 RID: 5299
		private static readonly int[] DaysToMonth365 = new int[]
		{
			0, 31, 59, 90, 120, 151, 181, 212, 243, 273,
			304, 334, 365
		};

		// Token: 0x040014B4 RID: 5300
		private static readonly int[] DaysToMonth366 = new int[]
		{
			0, 31, 60, 91, 121, 152, 182, 213, 244, 274,
			305, 335, 366
		};

		// Token: 0x040014B5 RID: 5301
		internal int MaxYear = 9999;
	}
}
