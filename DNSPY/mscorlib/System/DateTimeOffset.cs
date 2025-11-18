using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	// Token: 0x0200007F RID: 127
	[__DynamicallyInvokable]
	[Serializable]
	[StructLayout(LayoutKind.Auto)]
	public struct DateTimeOffset : IComparable, IFormattable, ISerializable, IDeserializationCallback, IComparable<DateTimeOffset>, IEquatable<DateTimeOffset>
	{
		// Token: 0x0600066E RID: 1646 RVA: 0x000169E4 File Offset: 0x00014BE4
		[__DynamicallyInvokable]
		public DateTimeOffset(long ticks, TimeSpan offset)
		{
			this.m_offsetMinutes = DateTimeOffset.ValidateOffset(offset);
			DateTime dateTime = new DateTime(ticks);
			this.m_dateTime = DateTimeOffset.ValidateDate(dateTime, offset);
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00016A14 File Offset: 0x00014C14
		[__DynamicallyInvokable]
		public DateTimeOffset(DateTime dateTime)
		{
			TimeSpan localUtcOffset;
			if (dateTime.Kind != DateTimeKind.Utc)
			{
				localUtcOffset = TimeZoneInfo.GetLocalUtcOffset(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime);
			}
			else
			{
				localUtcOffset = new TimeSpan(0L);
			}
			this.m_offsetMinutes = DateTimeOffset.ValidateOffset(localUtcOffset);
			this.m_dateTime = DateTimeOffset.ValidateDate(dateTime, localUtcOffset);
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00016A58 File Offset: 0x00014C58
		[__DynamicallyInvokable]
		public DateTimeOffset(DateTime dateTime, TimeSpan offset)
		{
			if (dateTime.Kind == DateTimeKind.Local)
			{
				if (offset != TimeZoneInfo.GetLocalUtcOffset(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_OffsetLocalMismatch"), "offset");
				}
			}
			else if (dateTime.Kind == DateTimeKind.Utc && offset != TimeSpan.Zero)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_OffsetUtcMismatch"), "offset");
			}
			this.m_offsetMinutes = DateTimeOffset.ValidateOffset(offset);
			this.m_dateTime = DateTimeOffset.ValidateDate(dateTime, offset);
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00016AD8 File Offset: 0x00014CD8
		[__DynamicallyInvokable]
		public DateTimeOffset(int year, int month, int day, int hour, int minute, int second, TimeSpan offset)
		{
			this.m_offsetMinutes = DateTimeOffset.ValidateOffset(offset);
			int num = second;
			if (second == 60 && DateTime.s_isLeapSecondsSupportedSystem)
			{
				second = 59;
			}
			this.m_dateTime = DateTimeOffset.ValidateDate(new DateTime(year, month, day, hour, minute, second), offset);
			if (num == 60 && !DateTime.IsValidTimeWithLeapSeconds(this.m_dateTime.Year, this.m_dateTime.Month, this.m_dateTime.Day, this.m_dateTime.Hour, this.m_dateTime.Minute, 60, DateTimeKind.Utc))
			{
				throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("ArgumentOutOfRange_BadHourMinuteSecond"));
			}
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00016B78 File Offset: 0x00014D78
		[__DynamicallyInvokable]
		public DateTimeOffset(int year, int month, int day, int hour, int minute, int second, int millisecond, TimeSpan offset)
		{
			this.m_offsetMinutes = DateTimeOffset.ValidateOffset(offset);
			int num = second;
			if (second == 60 && DateTime.s_isLeapSecondsSupportedSystem)
			{
				second = 59;
			}
			this.m_dateTime = DateTimeOffset.ValidateDate(new DateTime(year, month, day, hour, minute, second, millisecond), offset);
			if (num == 60 && !DateTime.IsValidTimeWithLeapSeconds(this.m_dateTime.Year, this.m_dateTime.Month, this.m_dateTime.Day, this.m_dateTime.Hour, this.m_dateTime.Minute, 60, DateTimeKind.Utc))
			{
				throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("ArgumentOutOfRange_BadHourMinuteSecond"));
			}
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00016C1C File Offset: 0x00014E1C
		public DateTimeOffset(int year, int month, int day, int hour, int minute, int second, int millisecond, Calendar calendar, TimeSpan offset)
		{
			this.m_offsetMinutes = DateTimeOffset.ValidateOffset(offset);
			int num = second;
			if (second == 60 && DateTime.s_isLeapSecondsSupportedSystem)
			{
				second = 59;
			}
			this.m_dateTime = DateTimeOffset.ValidateDate(new DateTime(year, month, day, hour, minute, second, millisecond, calendar), offset);
			if (num == 60 && !DateTime.IsValidTimeWithLeapSeconds(this.m_dateTime.Year, this.m_dateTime.Month, this.m_dateTime.Day, this.m_dateTime.Hour, this.m_dateTime.Minute, 60, DateTimeKind.Utc))
			{
				throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("ArgumentOutOfRange_BadHourMinuteSecond"));
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x00016CC0 File Offset: 0x00014EC0
		[__DynamicallyInvokable]
		public static DateTimeOffset Now
		{
			[__DynamicallyInvokable]
			get
			{
				return new DateTimeOffset(DateTime.Now);
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x00016CCC File Offset: 0x00014ECC
		[__DynamicallyInvokable]
		public static DateTimeOffset UtcNow
		{
			[__DynamicallyInvokable]
			get
			{
				return new DateTimeOffset(DateTime.UtcNow);
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x00016CD8 File Offset: 0x00014ED8
		[__DynamicallyInvokable]
		public DateTime DateTime
		{
			[__DynamicallyInvokable]
			get
			{
				return this.ClockDateTime;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x00016CE0 File Offset: 0x00014EE0
		[__DynamicallyInvokable]
		public DateTime UtcDateTime
		{
			[__DynamicallyInvokable]
			get
			{
				return DateTime.SpecifyKind(this.m_dateTime, DateTimeKind.Utc);
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x00016CF0 File Offset: 0x00014EF0
		[__DynamicallyInvokable]
		public DateTime LocalDateTime
		{
			[__DynamicallyInvokable]
			get
			{
				return this.UtcDateTime.ToLocalTime();
			}
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00016D0C File Offset: 0x00014F0C
		[__DynamicallyInvokable]
		public DateTimeOffset ToOffset(TimeSpan offset)
		{
			return new DateTimeOffset((this.m_dateTime + offset).Ticks, offset);
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x00016D34 File Offset: 0x00014F34
		private DateTime ClockDateTime
		{
			get
			{
				return new DateTime((this.m_dateTime + this.Offset).Ticks, DateTimeKind.Unspecified);
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x00016D60 File Offset: 0x00014F60
		[__DynamicallyInvokable]
		public DateTime Date
		{
			[__DynamicallyInvokable]
			get
			{
				return this.ClockDateTime.Date;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x00016D7C File Offset: 0x00014F7C
		[__DynamicallyInvokable]
		public int Day
		{
			[__DynamicallyInvokable]
			get
			{
				return this.ClockDateTime.Day;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x00016D98 File Offset: 0x00014F98
		[__DynamicallyInvokable]
		public DayOfWeek DayOfWeek
		{
			[__DynamicallyInvokable]
			get
			{
				return this.ClockDateTime.DayOfWeek;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x00016DB4 File Offset: 0x00014FB4
		[__DynamicallyInvokable]
		public int DayOfYear
		{
			[__DynamicallyInvokable]
			get
			{
				return this.ClockDateTime.DayOfYear;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x00016DD0 File Offset: 0x00014FD0
		[__DynamicallyInvokable]
		public int Hour
		{
			[__DynamicallyInvokable]
			get
			{
				return this.ClockDateTime.Hour;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x00016DEC File Offset: 0x00014FEC
		[__DynamicallyInvokable]
		public int Millisecond
		{
			[__DynamicallyInvokable]
			get
			{
				return this.ClockDateTime.Millisecond;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x00016E08 File Offset: 0x00015008
		[__DynamicallyInvokable]
		public int Minute
		{
			[__DynamicallyInvokable]
			get
			{
				return this.ClockDateTime.Minute;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x00016E24 File Offset: 0x00015024
		[__DynamicallyInvokable]
		public int Month
		{
			[__DynamicallyInvokable]
			get
			{
				return this.ClockDateTime.Month;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x00016E3F File Offset: 0x0001503F
		[__DynamicallyInvokable]
		public TimeSpan Offset
		{
			[__DynamicallyInvokable]
			get
			{
				return new TimeSpan(0, (int)this.m_offsetMinutes, 0);
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x00016E50 File Offset: 0x00015050
		[__DynamicallyInvokable]
		public int Second
		{
			[__DynamicallyInvokable]
			get
			{
				return this.ClockDateTime.Second;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x00016E6C File Offset: 0x0001506C
		[__DynamicallyInvokable]
		public long Ticks
		{
			[__DynamicallyInvokable]
			get
			{
				return this.ClockDateTime.Ticks;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x00016E88 File Offset: 0x00015088
		[__DynamicallyInvokable]
		public long UtcTicks
		{
			[__DynamicallyInvokable]
			get
			{
				return this.UtcDateTime.Ticks;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x00016EA4 File Offset: 0x000150A4
		[__DynamicallyInvokable]
		public TimeSpan TimeOfDay
		{
			[__DynamicallyInvokable]
			get
			{
				return this.ClockDateTime.TimeOfDay;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x00016EC0 File Offset: 0x000150C0
		[__DynamicallyInvokable]
		public int Year
		{
			[__DynamicallyInvokable]
			get
			{
				return this.ClockDateTime.Year;
			}
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00016EDC File Offset: 0x000150DC
		[__DynamicallyInvokable]
		public DateTimeOffset Add(TimeSpan timeSpan)
		{
			return new DateTimeOffset(this.ClockDateTime.Add(timeSpan), this.Offset);
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x00016F04 File Offset: 0x00015104
		[__DynamicallyInvokable]
		public DateTimeOffset AddDays(double days)
		{
			return new DateTimeOffset(this.ClockDateTime.AddDays(days), this.Offset);
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x00016F2C File Offset: 0x0001512C
		[__DynamicallyInvokable]
		public DateTimeOffset AddHours(double hours)
		{
			return new DateTimeOffset(this.ClockDateTime.AddHours(hours), this.Offset);
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x00016F54 File Offset: 0x00015154
		[__DynamicallyInvokable]
		public DateTimeOffset AddMilliseconds(double milliseconds)
		{
			return new DateTimeOffset(this.ClockDateTime.AddMilliseconds(milliseconds), this.Offset);
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00016F7C File Offset: 0x0001517C
		[__DynamicallyInvokable]
		public DateTimeOffset AddMinutes(double minutes)
		{
			return new DateTimeOffset(this.ClockDateTime.AddMinutes(minutes), this.Offset);
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x00016FA4 File Offset: 0x000151A4
		[__DynamicallyInvokable]
		public DateTimeOffset AddMonths(int months)
		{
			return new DateTimeOffset(this.ClockDateTime.AddMonths(months), this.Offset);
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x00016FCC File Offset: 0x000151CC
		[__DynamicallyInvokable]
		public DateTimeOffset AddSeconds(double seconds)
		{
			return new DateTimeOffset(this.ClockDateTime.AddSeconds(seconds), this.Offset);
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00016FF4 File Offset: 0x000151F4
		[__DynamicallyInvokable]
		public DateTimeOffset AddTicks(long ticks)
		{
			return new DateTimeOffset(this.ClockDateTime.AddTicks(ticks), this.Offset);
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0001701C File Offset: 0x0001521C
		[__DynamicallyInvokable]
		public DateTimeOffset AddYears(int years)
		{
			return new DateTimeOffset(this.ClockDateTime.AddYears(years), this.Offset);
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x00017043 File Offset: 0x00015243
		[__DynamicallyInvokable]
		public static int Compare(DateTimeOffset first, DateTimeOffset second)
		{
			return DateTime.Compare(first.UtcDateTime, second.UtcDateTime);
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x00017058 File Offset: 0x00015258
		[__DynamicallyInvokable]
		int IComparable.CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			if (!(obj is DateTimeOffset))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDateTimeOffset"));
			}
			DateTime utcDateTime = ((DateTimeOffset)obj).UtcDateTime;
			DateTime utcDateTime2 = this.UtcDateTime;
			if (utcDateTime2 > utcDateTime)
			{
				return 1;
			}
			if (utcDateTime2 < utcDateTime)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x000170B0 File Offset: 0x000152B0
		[__DynamicallyInvokable]
		public int CompareTo(DateTimeOffset other)
		{
			DateTime utcDateTime = other.UtcDateTime;
			DateTime utcDateTime2 = this.UtcDateTime;
			if (utcDateTime2 > utcDateTime)
			{
				return 1;
			}
			if (utcDateTime2 < utcDateTime)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x000170E4 File Offset: 0x000152E4
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return obj is DateTimeOffset && this.UtcDateTime.Equals(((DateTimeOffset)obj).UtcDateTime);
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x00017118 File Offset: 0x00015318
		[__DynamicallyInvokable]
		public bool Equals(DateTimeOffset other)
		{
			return this.UtcDateTime.Equals(other.UtcDateTime);
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0001713C File Offset: 0x0001533C
		[__DynamicallyInvokable]
		public bool EqualsExact(DateTimeOffset other)
		{
			return this.ClockDateTime == other.ClockDateTime && this.Offset == other.Offset && this.ClockDateTime.Kind == other.ClockDateTime.Kind;
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00017192 File Offset: 0x00015392
		[__DynamicallyInvokable]
		public static bool Equals(DateTimeOffset first, DateTimeOffset second)
		{
			return DateTime.Equals(first.UtcDateTime, second.UtcDateTime);
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x000171A7 File Offset: 0x000153A7
		[__DynamicallyInvokable]
		public static DateTimeOffset FromFileTime(long fileTime)
		{
			return new DateTimeOffset(DateTime.FromFileTime(fileTime));
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x000171B4 File Offset: 0x000153B4
		[__DynamicallyInvokable]
		public static DateTimeOffset FromUnixTimeSeconds(long seconds)
		{
			if (seconds < -62135596800L || seconds > 253402300799L)
			{
				throw new ArgumentOutOfRangeException("seconds", string.Format(Environment.GetResourceString("ArgumentOutOfRange_Range"), -62135596800L, 253402300799L));
			}
			long num = seconds * 10000000L + 621355968000000000L;
			return new DateTimeOffset(num, TimeSpan.Zero);
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x00017230 File Offset: 0x00015430
		[__DynamicallyInvokable]
		public static DateTimeOffset FromUnixTimeMilliseconds(long milliseconds)
		{
			if (milliseconds < -62135596800000L || milliseconds > 253402300799999L)
			{
				throw new ArgumentOutOfRangeException("milliseconds", string.Format(Environment.GetResourceString("ArgumentOutOfRange_Range"), -62135596800000L, 253402300799999L));
			}
			long num = milliseconds * 10000L + 621355968000000000L;
			return new DateTimeOffset(num, TimeSpan.Zero);
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x000172AC File Offset: 0x000154AC
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			try
			{
				this.m_offsetMinutes = DateTimeOffset.ValidateOffset(this.Offset);
				this.m_dateTime = DateTimeOffset.ValidateDate(this.ClockDateTime, this.Offset);
			}
			catch (ArgumentException ex)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"), ex);
			}
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x00017308 File Offset: 0x00015508
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("DateTime", this.m_dateTime);
			info.AddValue("OffsetMinutes", this.m_offsetMinutes);
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0001733C File Offset: 0x0001553C
		private DateTimeOffset(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.m_dateTime = (DateTime)info.GetValue("DateTime", typeof(DateTime));
			this.m_offsetMinutes = (short)info.GetValue("OffsetMinutes", typeof(short));
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x00017398 File Offset: 0x00015598
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this.UtcDateTime.GetHashCode();
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x000173B4 File Offset: 0x000155B4
		[__DynamicallyInvokable]
		public static DateTimeOffset Parse(string input)
		{
			TimeSpan timeSpan;
			return new DateTimeOffset(DateTimeParse.Parse(input, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out timeSpan).Ticks, timeSpan);
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x000173DD File Offset: 0x000155DD
		[__DynamicallyInvokable]
		public static DateTimeOffset Parse(string input, IFormatProvider formatProvider)
		{
			return DateTimeOffset.Parse(input, formatProvider, DateTimeStyles.None);
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x000173E8 File Offset: 0x000155E8
		[__DynamicallyInvokable]
		public static DateTimeOffset Parse(string input, IFormatProvider formatProvider, DateTimeStyles styles)
		{
			styles = DateTimeOffset.ValidateStyles(styles, "styles");
			TimeSpan timeSpan;
			return new DateTimeOffset(DateTimeParse.Parse(input, DateTimeFormatInfo.GetInstance(formatProvider), styles, out timeSpan).Ticks, timeSpan);
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0001741F File Offset: 0x0001561F
		[__DynamicallyInvokable]
		public static DateTimeOffset ParseExact(string input, string format, IFormatProvider formatProvider)
		{
			return DateTimeOffset.ParseExact(input, format, formatProvider, DateTimeStyles.None);
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0001742C File Offset: 0x0001562C
		[__DynamicallyInvokable]
		public static DateTimeOffset ParseExact(string input, string format, IFormatProvider formatProvider, DateTimeStyles styles)
		{
			styles = DateTimeOffset.ValidateStyles(styles, "styles");
			TimeSpan timeSpan;
			return new DateTimeOffset(DateTimeParse.ParseExact(input, format, DateTimeFormatInfo.GetInstance(formatProvider), styles, out timeSpan).Ticks, timeSpan);
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x00017464 File Offset: 0x00015664
		[__DynamicallyInvokable]
		public static DateTimeOffset ParseExact(string input, string[] formats, IFormatProvider formatProvider, DateTimeStyles styles)
		{
			styles = DateTimeOffset.ValidateStyles(styles, "styles");
			TimeSpan timeSpan;
			return new DateTimeOffset(DateTimeParse.ParseExactMultiple(input, formats, DateTimeFormatInfo.GetInstance(formatProvider), styles, out timeSpan).Ticks, timeSpan);
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0001749C File Offset: 0x0001569C
		[__DynamicallyInvokable]
		public TimeSpan Subtract(DateTimeOffset value)
		{
			return this.UtcDateTime.Subtract(value.UtcDateTime);
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x000174C0 File Offset: 0x000156C0
		[__DynamicallyInvokable]
		public DateTimeOffset Subtract(TimeSpan value)
		{
			return new DateTimeOffset(this.ClockDateTime.Subtract(value), this.Offset);
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x000174E8 File Offset: 0x000156E8
		[__DynamicallyInvokable]
		public long ToFileTime()
		{
			return this.UtcDateTime.ToFileTime();
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00017504 File Offset: 0x00015704
		[__DynamicallyInvokable]
		public long ToUnixTimeSeconds()
		{
			long num = this.UtcDateTime.Ticks / 10000000L;
			return num - 62135596800L;
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00017534 File Offset: 0x00015734
		[__DynamicallyInvokable]
		public long ToUnixTimeMilliseconds()
		{
			long num = this.UtcDateTime.Ticks / 10000L;
			return num - 62135596800000L;
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00017562 File Offset: 0x00015762
		[__DynamicallyInvokable]
		public DateTimeOffset ToLocalTime()
		{
			return this.ToLocalTime(false);
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0001756C File Offset: 0x0001576C
		internal DateTimeOffset ToLocalTime(bool throwOnOverflow)
		{
			return new DateTimeOffset(this.UtcDateTime.ToLocalTime(throwOnOverflow));
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0001758D File Offset: 0x0001578D
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return DateTimeFormat.Format(this.ClockDateTime, null, DateTimeFormatInfo.CurrentInfo, this.Offset);
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x000175A6 File Offset: 0x000157A6
		[__DynamicallyInvokable]
		public string ToString(string format)
		{
			return DateTimeFormat.Format(this.ClockDateTime, format, DateTimeFormatInfo.CurrentInfo, this.Offset);
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x000175BF File Offset: 0x000157BF
		[__DynamicallyInvokable]
		public string ToString(IFormatProvider formatProvider)
		{
			return DateTimeFormat.Format(this.ClockDateTime, null, DateTimeFormatInfo.GetInstance(formatProvider), this.Offset);
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x000175D9 File Offset: 0x000157D9
		[__DynamicallyInvokable]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return DateTimeFormat.Format(this.ClockDateTime, format, DateTimeFormatInfo.GetInstance(formatProvider), this.Offset);
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x000175F3 File Offset: 0x000157F3
		[__DynamicallyInvokable]
		public DateTimeOffset ToUniversalTime()
		{
			return new DateTimeOffset(this.UtcDateTime);
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00017600 File Offset: 0x00015800
		[__DynamicallyInvokable]
		public static bool TryParse(string input, out DateTimeOffset result)
		{
			DateTime dateTime;
			TimeSpan timeSpan;
			bool flag = DateTimeParse.TryParse(input, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out dateTime, out timeSpan);
			result = new DateTimeOffset(dateTime.Ticks, timeSpan);
			return flag;
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00017634 File Offset: 0x00015834
		[__DynamicallyInvokable]
		public static bool TryParse(string input, IFormatProvider formatProvider, DateTimeStyles styles, out DateTimeOffset result)
		{
			styles = DateTimeOffset.ValidateStyles(styles, "styles");
			DateTime dateTime;
			TimeSpan timeSpan;
			bool flag = DateTimeParse.TryParse(input, DateTimeFormatInfo.GetInstance(formatProvider), styles, out dateTime, out timeSpan);
			result = new DateTimeOffset(dateTime.Ticks, timeSpan);
			return flag;
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00017674 File Offset: 0x00015874
		[__DynamicallyInvokable]
		public static bool TryParseExact(string input, string format, IFormatProvider formatProvider, DateTimeStyles styles, out DateTimeOffset result)
		{
			styles = DateTimeOffset.ValidateStyles(styles, "styles");
			DateTime dateTime;
			TimeSpan timeSpan;
			bool flag = DateTimeParse.TryParseExact(input, format, DateTimeFormatInfo.GetInstance(formatProvider), styles, out dateTime, out timeSpan);
			result = new DateTimeOffset(dateTime.Ticks, timeSpan);
			return flag;
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x000176B8 File Offset: 0x000158B8
		[__DynamicallyInvokable]
		public static bool TryParseExact(string input, string[] formats, IFormatProvider formatProvider, DateTimeStyles styles, out DateTimeOffset result)
		{
			styles = DateTimeOffset.ValidateStyles(styles, "styles");
			DateTime dateTime;
			TimeSpan timeSpan;
			bool flag = DateTimeParse.TryParseExactMultiple(input, formats, DateTimeFormatInfo.GetInstance(formatProvider), styles, out dateTime, out timeSpan);
			result = new DateTimeOffset(dateTime.Ticks, timeSpan);
			return flag;
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x000176FC File Offset: 0x000158FC
		private static short ValidateOffset(TimeSpan offset)
		{
			long ticks = offset.Ticks;
			if (ticks % 600000000L != 0L)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_OffsetPrecision"), "offset");
			}
			if (ticks < -504000000000L || ticks > 504000000000L)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("Argument_OffsetOutOfRange"));
			}
			return (short)(offset.Ticks / 600000000L);
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0001776C File Offset: 0x0001596C
		private static DateTime ValidateDate(DateTime dateTime, TimeSpan offset)
		{
			long num = dateTime.Ticks - offset.Ticks;
			if (num < 0L || num > 3155378975999999999L)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("Argument_UTCOutOfRange"));
			}
			return new DateTime(num, DateTimeKind.Unspecified);
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x000177B8 File Offset: 0x000159B8
		private static DateTimeStyles ValidateStyles(DateTimeStyles style, string parameterName)
		{
			if ((style & ~(DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite | DateTimeStyles.AllowInnerWhite | DateTimeStyles.NoCurrentDateDefault | DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeLocal | DateTimeStyles.AssumeUniversal | DateTimeStyles.RoundtripKind)) != DateTimeStyles.None)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidDateTimeStyles"), parameterName);
			}
			if ((style & DateTimeStyles.AssumeLocal) != DateTimeStyles.None && (style & DateTimeStyles.AssumeUniversal) != DateTimeStyles.None)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ConflictingDateTimeStyles"), parameterName);
			}
			if ((style & DateTimeStyles.NoCurrentDateDefault) != DateTimeStyles.None)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeOffsetInvalidDateTimeStyles"), parameterName);
			}
			style &= ~DateTimeStyles.RoundtripKind;
			style &= ~DateTimeStyles.AssumeLocal;
			return style;
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00017822 File Offset: 0x00015A22
		[__DynamicallyInvokable]
		public static implicit operator DateTimeOffset(DateTime dateTime)
		{
			return new DateTimeOffset(dateTime);
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0001782A File Offset: 0x00015A2A
		[__DynamicallyInvokable]
		public static DateTimeOffset operator +(DateTimeOffset dateTimeOffset, TimeSpan timeSpan)
		{
			return new DateTimeOffset(dateTimeOffset.ClockDateTime + timeSpan, dateTimeOffset.Offset);
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x00017845 File Offset: 0x00015A45
		[__DynamicallyInvokable]
		public static DateTimeOffset operator -(DateTimeOffset dateTimeOffset, TimeSpan timeSpan)
		{
			return new DateTimeOffset(dateTimeOffset.ClockDateTime - timeSpan, dateTimeOffset.Offset);
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x00017860 File Offset: 0x00015A60
		[__DynamicallyInvokable]
		public static TimeSpan operator -(DateTimeOffset left, DateTimeOffset right)
		{
			return left.UtcDateTime - right.UtcDateTime;
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00017875 File Offset: 0x00015A75
		[__DynamicallyInvokable]
		public static bool operator ==(DateTimeOffset left, DateTimeOffset right)
		{
			return left.UtcDateTime == right.UtcDateTime;
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0001788A File Offset: 0x00015A8A
		[__DynamicallyInvokable]
		public static bool operator !=(DateTimeOffset left, DateTimeOffset right)
		{
			return left.UtcDateTime != right.UtcDateTime;
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0001789F File Offset: 0x00015A9F
		[__DynamicallyInvokable]
		public static bool operator <(DateTimeOffset left, DateTimeOffset right)
		{
			return left.UtcDateTime < right.UtcDateTime;
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x000178B4 File Offset: 0x00015AB4
		[__DynamicallyInvokable]
		public static bool operator <=(DateTimeOffset left, DateTimeOffset right)
		{
			return left.UtcDateTime <= right.UtcDateTime;
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x000178C9 File Offset: 0x00015AC9
		[__DynamicallyInvokable]
		public static bool operator >(DateTimeOffset left, DateTimeOffset right)
		{
			return left.UtcDateTime > right.UtcDateTime;
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x000178DE File Offset: 0x00015ADE
		[__DynamicallyInvokable]
		public static bool operator >=(DateTimeOffset left, DateTimeOffset right)
		{
			return left.UtcDateTime >= right.UtcDateTime;
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x000178F3 File Offset: 0x00015AF3
		// Note: this type is marked as 'beforefieldinit'.
		static DateTimeOffset()
		{
		}

		// Token: 0x040002EA RID: 746
		internal const long MaxOffset = 504000000000L;

		// Token: 0x040002EB RID: 747
		internal const long MinOffset = -504000000000L;

		// Token: 0x040002EC RID: 748
		private const long UnixEpochTicks = 621355968000000000L;

		// Token: 0x040002ED RID: 749
		private const long UnixEpochSeconds = 62135596800L;

		// Token: 0x040002EE RID: 750
		private const long UnixEpochMilliseconds = 62135596800000L;

		// Token: 0x040002EF RID: 751
		[__DynamicallyInvokable]
		public static readonly DateTimeOffset MinValue = new DateTimeOffset(0L, TimeSpan.Zero);

		// Token: 0x040002F0 RID: 752
		[__DynamicallyInvokable]
		public static readonly DateTimeOffset MaxValue = new DateTimeOffset(3155378975999999999L, TimeSpan.Zero);

		// Token: 0x040002F1 RID: 753
		private DateTime m_dateTime;

		// Token: 0x040002F2 RID: 754
		private short m_offsetMinutes;
	}
}
