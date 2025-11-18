using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	// Token: 0x020003D2 RID: 978
	[ComVisible(true)]
	[Serializable]
	public class TaiwanCalendar : Calendar
	{
		// Token: 0x0600317F RID: 12671 RVA: 0x000BDDCB File Offset: 0x000BBFCB
		internal static Calendar GetDefaultInstance()
		{
			if (TaiwanCalendar.s_defaultInstance == null)
			{
				TaiwanCalendar.s_defaultInstance = new TaiwanCalendar();
			}
			return TaiwanCalendar.s_defaultInstance;
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06003180 RID: 12672 RVA: 0x000BDDE9 File Offset: 0x000BBFE9
		[ComVisible(false)]
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return TaiwanCalendar.calendarMinValue;
			}
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06003181 RID: 12673 RVA: 0x000BDDF0 File Offset: 0x000BBFF0
		[ComVisible(false)]
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return DateTime.MaxValue;
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06003182 RID: 12674 RVA: 0x000BDDF7 File Offset: 0x000BBFF7
		[ComVisible(false)]
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.SolarCalendar;
			}
		}

		// Token: 0x06003183 RID: 12675 RVA: 0x000BDDFC File Offset: 0x000BBFFC
		public TaiwanCalendar()
		{
			try
			{
				new CultureInfo("zh-TW");
			}
			catch (ArgumentException ex)
			{
				throw new TypeInitializationException(base.GetType().FullName, ex);
			}
			this.helper = new GregorianCalendarHelper(this, TaiwanCalendar.taiwanEraInfo);
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06003184 RID: 12676 RVA: 0x000BDE50 File Offset: 0x000BC050
		internal override int ID
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x06003185 RID: 12677 RVA: 0x000BDE53 File Offset: 0x000BC053
		public override DateTime AddMonths(DateTime time, int months)
		{
			return this.helper.AddMonths(time, months);
		}

		// Token: 0x06003186 RID: 12678 RVA: 0x000BDE62 File Offset: 0x000BC062
		public override DateTime AddYears(DateTime time, int years)
		{
			return this.helper.AddYears(time, years);
		}

		// Token: 0x06003187 RID: 12679 RVA: 0x000BDE71 File Offset: 0x000BC071
		public override int GetDaysInMonth(int year, int month, int era)
		{
			return this.helper.GetDaysInMonth(year, month, era);
		}

		// Token: 0x06003188 RID: 12680 RVA: 0x000BDE81 File Offset: 0x000BC081
		public override int GetDaysInYear(int year, int era)
		{
			return this.helper.GetDaysInYear(year, era);
		}

		// Token: 0x06003189 RID: 12681 RVA: 0x000BDE90 File Offset: 0x000BC090
		public override int GetDayOfMonth(DateTime time)
		{
			return this.helper.GetDayOfMonth(time);
		}

		// Token: 0x0600318A RID: 12682 RVA: 0x000BDE9E File Offset: 0x000BC09E
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			return this.helper.GetDayOfWeek(time);
		}

		// Token: 0x0600318B RID: 12683 RVA: 0x000BDEAC File Offset: 0x000BC0AC
		public override int GetDayOfYear(DateTime time)
		{
			return this.helper.GetDayOfYear(time);
		}

		// Token: 0x0600318C RID: 12684 RVA: 0x000BDEBA File Offset: 0x000BC0BA
		public override int GetMonthsInYear(int year, int era)
		{
			return this.helper.GetMonthsInYear(year, era);
		}

		// Token: 0x0600318D RID: 12685 RVA: 0x000BDEC9 File Offset: 0x000BC0C9
		[ComVisible(false)]
		public override int GetWeekOfYear(DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
		{
			return this.helper.GetWeekOfYear(time, rule, firstDayOfWeek);
		}

		// Token: 0x0600318E RID: 12686 RVA: 0x000BDED9 File Offset: 0x000BC0D9
		public override int GetEra(DateTime time)
		{
			return this.helper.GetEra(time);
		}

		// Token: 0x0600318F RID: 12687 RVA: 0x000BDEE7 File Offset: 0x000BC0E7
		public override int GetMonth(DateTime time)
		{
			return this.helper.GetMonth(time);
		}

		// Token: 0x06003190 RID: 12688 RVA: 0x000BDEF5 File Offset: 0x000BC0F5
		public override int GetYear(DateTime time)
		{
			return this.helper.GetYear(time);
		}

		// Token: 0x06003191 RID: 12689 RVA: 0x000BDF03 File Offset: 0x000BC103
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			return this.helper.IsLeapDay(year, month, day, era);
		}

		// Token: 0x06003192 RID: 12690 RVA: 0x000BDF15 File Offset: 0x000BC115
		public override bool IsLeapYear(int year, int era)
		{
			return this.helper.IsLeapYear(year, era);
		}

		// Token: 0x06003193 RID: 12691 RVA: 0x000BDF24 File Offset: 0x000BC124
		[ComVisible(false)]
		public override int GetLeapMonth(int year, int era)
		{
			return this.helper.GetLeapMonth(year, era);
		}

		// Token: 0x06003194 RID: 12692 RVA: 0x000BDF33 File Offset: 0x000BC133
		public override bool IsLeapMonth(int year, int month, int era)
		{
			return this.helper.IsLeapMonth(year, month, era);
		}

		// Token: 0x06003195 RID: 12693 RVA: 0x000BDF44 File Offset: 0x000BC144
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			return this.helper.ToDateTime(year, month, day, hour, minute, second, millisecond, era);
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06003196 RID: 12694 RVA: 0x000BDF69 File Offset: 0x000BC169
		public override int[] Eras
		{
			get
			{
				return this.helper.Eras;
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06003197 RID: 12695 RVA: 0x000BDF76 File Offset: 0x000BC176
		// (set) Token: 0x06003198 RID: 12696 RVA: 0x000BDF9C File Offset: 0x000BC19C
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

		// Token: 0x06003199 RID: 12697 RVA: 0x000BE000 File Offset: 0x000BC200
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

		// Token: 0x0600319A RID: 12698 RVA: 0x000BE06C File Offset: 0x000BC26C
		// Note: this type is marked as 'beforefieldinit'.
		static TaiwanCalendar()
		{
		}

		// Token: 0x04001521 RID: 5409
		internal static EraInfo[] taiwanEraInfo = new EraInfo[]
		{
			new EraInfo(1, 1912, 1, 1, 1911, 1, 8088)
		};

		// Token: 0x04001522 RID: 5410
		internal static volatile Calendar s_defaultInstance;

		// Token: 0x04001523 RID: 5411
		internal GregorianCalendarHelper helper;

		// Token: 0x04001524 RID: 5412
		internal static readonly DateTime calendarMinValue = new DateTime(1912, 1, 1);

		// Token: 0x04001525 RID: 5413
		private const int DEFAULT_TWO_DIGIT_YEAR_MAX = 99;
	}
}
