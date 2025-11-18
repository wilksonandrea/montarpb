using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32;

namespace System.Globalization
{
	// Token: 0x020003CD RID: 973
	[ComVisible(true)]
	[Serializable]
	public class JapaneseCalendar : Calendar
	{
		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x0600310D RID: 12557 RVA: 0x000BCB69 File Offset: 0x000BAD69
		[ComVisible(false)]
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return JapaneseCalendar.calendarMinValue;
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x0600310E RID: 12558 RVA: 0x000BCB70 File Offset: 0x000BAD70
		[ComVisible(false)]
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return DateTime.MaxValue;
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x0600310F RID: 12559 RVA: 0x000BCB77 File Offset: 0x000BAD77
		[ComVisible(false)]
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.SolarCalendar;
			}
		}

		// Token: 0x06003110 RID: 12560 RVA: 0x000BCB7C File Offset: 0x000BAD7C
		internal static EraInfo[] GetEraInfo()
		{
			if (JapaneseCalendar.japaneseEraInfo == null)
			{
				JapaneseCalendar.japaneseEraInfo = JapaneseCalendar.GetErasFromRegistry();
				if (JapaneseCalendar.japaneseEraInfo == null)
				{
					JapaneseCalendar.japaneseEraInfo = new EraInfo[]
					{
						new EraInfo(4, 1989, 1, 8, 1988, 1, 8011, "平成", "平", "H"),
						new EraInfo(3, 1926, 12, 25, 1925, 1, 64, "昭和", "昭", "S"),
						new EraInfo(2, 1912, 7, 30, 1911, 1, 15, "大正", "大", "T"),
						new EraInfo(1, 1868, 1, 1, 1867, 1, 45, "明治", "明", "M")
					};
				}
			}
			return JapaneseCalendar.japaneseEraInfo;
		}

		// Token: 0x06003111 RID: 12561 RVA: 0x000BCC68 File Offset: 0x000BAE68
		[SecuritySafeCritical]
		private static EraInfo[] GetErasFromRegistry()
		{
			int num = 0;
			EraInfo[] array = null;
			try
			{
				PermissionSet permissionSet = new PermissionSet(PermissionState.None);
				permissionSet.AddPermission(new RegistryPermission(RegistryPermissionAccess.Read, "HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Control\\Nls\\Calendars\\Japanese\\Eras"));
				permissionSet.Assert();
				RegistryKey registryKey = RegistryKey.GetBaseKey(RegistryKey.HKEY_LOCAL_MACHINE).OpenSubKey("System\\CurrentControlSet\\Control\\Nls\\Calendars\\Japanese\\Eras", false);
				if (registryKey == null)
				{
					return null;
				}
				string[] valueNames = registryKey.GetValueNames();
				if (valueNames != null && valueNames.Length != 0)
				{
					array = new EraInfo[valueNames.Length];
					for (int i = 0; i < valueNames.Length; i++)
					{
						EraInfo eraFromValue = JapaneseCalendar.GetEraFromValue(valueNames[i], registryKey.GetValue(valueNames[i]).ToString());
						if (eraFromValue != null)
						{
							array[num] = eraFromValue;
							num++;
						}
					}
				}
			}
			catch (SecurityException)
			{
				return null;
			}
			catch (IOException)
			{
				return null;
			}
			catch (UnauthorizedAccessException)
			{
				return null;
			}
			if (num < 4)
			{
				return null;
			}
			Array.Resize<EraInfo>(ref array, num);
			Array.Sort<EraInfo>(array, new Comparison<EraInfo>(JapaneseCalendar.CompareEraRanges));
			for (int j = 0; j < array.Length; j++)
			{
				array[j].era = array.Length - j;
				if (j == 0)
				{
					array[0].maxEraYear = 9999 - array[0].yearOffset;
				}
				else
				{
					array[j].maxEraYear = array[j - 1].yearOffset + 1 - array[j].yearOffset;
				}
			}
			return array;
		}

		// Token: 0x06003112 RID: 12562 RVA: 0x000BCDD4 File Offset: 0x000BAFD4
		private static int CompareEraRanges(EraInfo a, EraInfo b)
		{
			return b.ticks.CompareTo(a.ticks);
		}

		// Token: 0x06003113 RID: 12563 RVA: 0x000BCDE8 File Offset: 0x000BAFE8
		private static EraInfo GetEraFromValue(string value, string data)
		{
			if (value == null || data == null)
			{
				return null;
			}
			if (value.Length != 10)
			{
				return null;
			}
			int num;
			int num2;
			int num3;
			if (!Number.TryParseInt32(value.Substring(0, 4), NumberStyles.None, NumberFormatInfo.InvariantInfo, out num) || !Number.TryParseInt32(value.Substring(5, 2), NumberStyles.None, NumberFormatInfo.InvariantInfo, out num2) || !Number.TryParseInt32(value.Substring(8, 2), NumberStyles.None, NumberFormatInfo.InvariantInfo, out num3))
			{
				return null;
			}
			string[] array = data.Split(new char[] { '_' });
			if (array.Length != 4)
			{
				return null;
			}
			if (array[0].Length == 0 || array[1].Length == 0 || array[2].Length == 0 || array[3].Length == 0)
			{
				return null;
			}
			return new EraInfo(0, num, num2, num3, num - 1, 1, 0, array[0], array[1], array[3]);
		}

		// Token: 0x06003114 RID: 12564 RVA: 0x000BCEAB File Offset: 0x000BB0AB
		internal static Calendar GetDefaultInstance()
		{
			if (JapaneseCalendar.s_defaultInstance == null)
			{
				JapaneseCalendar.s_defaultInstance = new JapaneseCalendar();
			}
			return JapaneseCalendar.s_defaultInstance;
		}

		// Token: 0x06003115 RID: 12565 RVA: 0x000BCECC File Offset: 0x000BB0CC
		public JapaneseCalendar()
		{
			try
			{
				new CultureInfo("ja-JP");
			}
			catch (ArgumentException ex)
			{
				throw new TypeInitializationException(base.GetType().FullName, ex);
			}
			this.helper = new GregorianCalendarHelper(this, JapaneseCalendar.GetEraInfo());
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06003116 RID: 12566 RVA: 0x000BCF20 File Offset: 0x000BB120
		internal override int ID
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x06003117 RID: 12567 RVA: 0x000BCF23 File Offset: 0x000BB123
		public override DateTime AddMonths(DateTime time, int months)
		{
			return this.helper.AddMonths(time, months);
		}

		// Token: 0x06003118 RID: 12568 RVA: 0x000BCF32 File Offset: 0x000BB132
		public override DateTime AddYears(DateTime time, int years)
		{
			return this.helper.AddYears(time, years);
		}

		// Token: 0x06003119 RID: 12569 RVA: 0x000BCF41 File Offset: 0x000BB141
		public override int GetDaysInMonth(int year, int month, int era)
		{
			return this.helper.GetDaysInMonth(year, month, era);
		}

		// Token: 0x0600311A RID: 12570 RVA: 0x000BCF51 File Offset: 0x000BB151
		public override int GetDaysInYear(int year, int era)
		{
			return this.helper.GetDaysInYear(year, era);
		}

		// Token: 0x0600311B RID: 12571 RVA: 0x000BCF60 File Offset: 0x000BB160
		public override int GetDayOfMonth(DateTime time)
		{
			return this.helper.GetDayOfMonth(time);
		}

		// Token: 0x0600311C RID: 12572 RVA: 0x000BCF6E File Offset: 0x000BB16E
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			return this.helper.GetDayOfWeek(time);
		}

		// Token: 0x0600311D RID: 12573 RVA: 0x000BCF7C File Offset: 0x000BB17C
		public override int GetDayOfYear(DateTime time)
		{
			return this.helper.GetDayOfYear(time);
		}

		// Token: 0x0600311E RID: 12574 RVA: 0x000BCF8A File Offset: 0x000BB18A
		public override int GetMonthsInYear(int year, int era)
		{
			return this.helper.GetMonthsInYear(year, era);
		}

		// Token: 0x0600311F RID: 12575 RVA: 0x000BCF99 File Offset: 0x000BB199
		[ComVisible(false)]
		public override int GetWeekOfYear(DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
		{
			return this.helper.GetWeekOfYear(time, rule, firstDayOfWeek);
		}

		// Token: 0x06003120 RID: 12576 RVA: 0x000BCFA9 File Offset: 0x000BB1A9
		public override int GetEra(DateTime time)
		{
			return this.helper.GetEra(time);
		}

		// Token: 0x06003121 RID: 12577 RVA: 0x000BCFB7 File Offset: 0x000BB1B7
		public override int GetMonth(DateTime time)
		{
			return this.helper.GetMonth(time);
		}

		// Token: 0x06003122 RID: 12578 RVA: 0x000BCFC5 File Offset: 0x000BB1C5
		public override int GetYear(DateTime time)
		{
			return this.helper.GetYear(time);
		}

		// Token: 0x06003123 RID: 12579 RVA: 0x000BCFD3 File Offset: 0x000BB1D3
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			return this.helper.IsLeapDay(year, month, day, era);
		}

		// Token: 0x06003124 RID: 12580 RVA: 0x000BCFE5 File Offset: 0x000BB1E5
		public override bool IsLeapYear(int year, int era)
		{
			return this.helper.IsLeapYear(year, era);
		}

		// Token: 0x06003125 RID: 12581 RVA: 0x000BCFF4 File Offset: 0x000BB1F4
		[ComVisible(false)]
		public override int GetLeapMonth(int year, int era)
		{
			return this.helper.GetLeapMonth(year, era);
		}

		// Token: 0x06003126 RID: 12582 RVA: 0x000BD003 File Offset: 0x000BB203
		public override bool IsLeapMonth(int year, int month, int era)
		{
			return this.helper.IsLeapMonth(year, month, era);
		}

		// Token: 0x06003127 RID: 12583 RVA: 0x000BD014 File Offset: 0x000BB214
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			return this.helper.ToDateTime(year, month, day, hour, minute, second, millisecond, era);
		}

		// Token: 0x06003128 RID: 12584 RVA: 0x000BD03C File Offset: 0x000BB23C
		public override int ToFourDigitYear(int year)
		{
			if (year <= 0)
			{
				throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
			}
			if (year > this.helper.MaxYear)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 1, this.helper.MaxYear));
			}
			return year;
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06003129 RID: 12585 RVA: 0x000BD0A6 File Offset: 0x000BB2A6
		public override int[] Eras
		{
			get
			{
				return this.helper.Eras;
			}
		}

		// Token: 0x0600312A RID: 12586 RVA: 0x000BD0B4 File Offset: 0x000BB2B4
		internal static string[] EraNames()
		{
			EraInfo[] eraInfo = JapaneseCalendar.GetEraInfo();
			string[] array = new string[eraInfo.Length];
			for (int i = 0; i < eraInfo.Length; i++)
			{
				array[i] = eraInfo[eraInfo.Length - i - 1].eraName;
			}
			return array;
		}

		// Token: 0x0600312B RID: 12587 RVA: 0x000BD0F0 File Offset: 0x000BB2F0
		internal static string[] AbbrevEraNames()
		{
			EraInfo[] eraInfo = JapaneseCalendar.GetEraInfo();
			string[] array = new string[eraInfo.Length];
			for (int i = 0; i < eraInfo.Length; i++)
			{
				array[i] = eraInfo[eraInfo.Length - i - 1].abbrevEraName;
			}
			return array;
		}

		// Token: 0x0600312C RID: 12588 RVA: 0x000BD12C File Offset: 0x000BB32C
		internal static string[] EnglishEraNames()
		{
			EraInfo[] eraInfo = JapaneseCalendar.GetEraInfo();
			string[] array = new string[eraInfo.Length];
			for (int i = 0; i < eraInfo.Length; i++)
			{
				array[i] = eraInfo[eraInfo.Length - i - 1].englishEraName;
			}
			return array;
		}

		// Token: 0x0600312D RID: 12589 RVA: 0x000BD168 File Offset: 0x000BB368
		internal override bool IsValidYear(int year, int era)
		{
			return this.helper.IsValidYear(year, era);
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x0600312E RID: 12590 RVA: 0x000BD177 File Offset: 0x000BB377
		// (set) Token: 0x0600312F RID: 12591 RVA: 0x000BD19C File Offset: 0x000BB39C
		public override int TwoDigitYearMax
		{
			get
			{
				if (this.twoDigitYearMax == -1)
				{
					this.twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.ID, 99);
				}
				return this.twoDigitYearMax;
			}
			set
			{
				base.VerifyWritable();
				if (value < 99 || value > this.helper.MaxYear)
				{
					throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 99, this.helper.MaxYear));
				}
				this.twoDigitYearMax = value;
			}
		}

		// Token: 0x06003130 RID: 12592 RVA: 0x000BD1FF File Offset: 0x000BB3FF
		// Note: this type is marked as 'beforefieldinit'.
		static JapaneseCalendar()
		{
		}

		// Token: 0x04001509 RID: 5385
		internal static readonly DateTime calendarMinValue = new DateTime(1868, 9, 8);

		// Token: 0x0400150A RID: 5386
		internal static volatile EraInfo[] japaneseEraInfo;

		// Token: 0x0400150B RID: 5387
		private const string c_japaneseErasHive = "System\\CurrentControlSet\\Control\\Nls\\Calendars\\Japanese\\Eras";

		// Token: 0x0400150C RID: 5388
		private const string c_japaneseErasHivePermissionList = "HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Control\\Nls\\Calendars\\Japanese\\Eras";

		// Token: 0x0400150D RID: 5389
		internal static volatile Calendar s_defaultInstance;

		// Token: 0x0400150E RID: 5390
		internal GregorianCalendarHelper helper;

		// Token: 0x0400150F RID: 5391
		private const int DEFAULT_TWO_DIGIT_YEAR_MAX = 99;
	}
}
