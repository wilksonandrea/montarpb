using System;
using System.Security;
using System.Text;

namespace System.Globalization
{
	// Token: 0x020003D6 RID: 982
	internal static class TimeSpanFormat
	{
		// Token: 0x060031F2 RID: 12786 RVA: 0x000BECC3 File Offset: 0x000BCEC3
		[SecuritySafeCritical]
		private static string IntToString(int n, int digits)
		{
			return ParseNumbers.IntToString(n, 10, digits, '0', 0);
		}

		// Token: 0x060031F3 RID: 12787 RVA: 0x000BECD4 File Offset: 0x000BCED4
		internal static string Format(TimeSpan value, string format, IFormatProvider formatProvider)
		{
			if (format == null || format.Length == 0)
			{
				format = "c";
			}
			if (format.Length != 1)
			{
				return TimeSpanFormat.FormatCustomized(value, format, DateTimeFormatInfo.GetInstance(formatProvider));
			}
			char c = format[0];
			if (c == 'c' || c == 't' || c == 'T')
			{
				return TimeSpanFormat.FormatStandard(value, true, format, TimeSpanFormat.Pattern.Minimum);
			}
			if (c == 'g' || c == 'G')
			{
				DateTimeFormatInfo instance = DateTimeFormatInfo.GetInstance(formatProvider);
				if (value._ticks < 0L)
				{
					format = instance.FullTimeSpanNegativePattern;
				}
				else
				{
					format = instance.FullTimeSpanPositivePattern;
				}
				TimeSpanFormat.Pattern pattern;
				if (c == 'g')
				{
					pattern = TimeSpanFormat.Pattern.Minimum;
				}
				else
				{
					pattern = TimeSpanFormat.Pattern.Full;
				}
				return TimeSpanFormat.FormatStandard(value, false, format, pattern);
			}
			throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
		}

		// Token: 0x060031F4 RID: 12788 RVA: 0x000BED7C File Offset: 0x000BCF7C
		private static string FormatStandard(TimeSpan value, bool isInvariant, string format, TimeSpanFormat.Pattern pattern)
		{
			StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
			int num = (int)(value._ticks / 864000000000L);
			long num2 = value._ticks % 864000000000L;
			if (value._ticks < 0L)
			{
				num = -num;
				num2 = -num2;
			}
			int num3 = (int)(num2 / 36000000000L % 24L);
			int num4 = (int)(num2 / 600000000L % 60L);
			int num5 = (int)(num2 / 10000000L % 60L);
			int num6 = (int)(num2 % 10000000L);
			TimeSpanFormat.FormatLiterals formatLiterals;
			if (isInvariant)
			{
				if (value._ticks < 0L)
				{
					formatLiterals = TimeSpanFormat.NegativeInvariantFormatLiterals;
				}
				else
				{
					formatLiterals = TimeSpanFormat.PositiveInvariantFormatLiterals;
				}
			}
			else
			{
				formatLiterals = default(TimeSpanFormat.FormatLiterals);
				formatLiterals.Init(format, pattern == TimeSpanFormat.Pattern.Full);
			}
			if (num6 != 0)
			{
				num6 = (int)((long)num6 / (long)Math.Pow(10.0, (double)(7 - formatLiterals.ff)));
			}
			stringBuilder.Append(formatLiterals.Start);
			if (pattern == TimeSpanFormat.Pattern.Full || num != 0)
			{
				stringBuilder.Append(num);
				stringBuilder.Append(formatLiterals.DayHourSep);
			}
			stringBuilder.Append(TimeSpanFormat.IntToString(num3, formatLiterals.hh));
			stringBuilder.Append(formatLiterals.HourMinuteSep);
			stringBuilder.Append(TimeSpanFormat.IntToString(num4, formatLiterals.mm));
			stringBuilder.Append(formatLiterals.MinuteSecondSep);
			stringBuilder.Append(TimeSpanFormat.IntToString(num5, formatLiterals.ss));
			if (!isInvariant && pattern == TimeSpanFormat.Pattern.Minimum)
			{
				int num7 = formatLiterals.ff;
				while (num7 > 0 && num6 % 10 == 0)
				{
					num6 /= 10;
					num7--;
				}
				if (num7 > 0)
				{
					stringBuilder.Append(formatLiterals.SecondFractionSep);
					stringBuilder.Append(num6.ToString(DateTimeFormat.fixedNumberFormats[num7 - 1], CultureInfo.InvariantCulture));
				}
			}
			else if (pattern == TimeSpanFormat.Pattern.Full || num6 != 0)
			{
				stringBuilder.Append(formatLiterals.SecondFractionSep);
				stringBuilder.Append(TimeSpanFormat.IntToString(num6, formatLiterals.ff));
			}
			stringBuilder.Append(formatLiterals.End);
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}

		// Token: 0x060031F5 RID: 12789 RVA: 0x000BEF78 File Offset: 0x000BD178
		internal static string FormatCustomized(TimeSpan value, string format, DateTimeFormatInfo dtfi)
		{
			int num = (int)(value._ticks / 864000000000L);
			long num2 = value._ticks % 864000000000L;
			if (value._ticks < 0L)
			{
				num = -num;
				num2 = -num2;
			}
			int num3 = (int)(num2 / 36000000000L % 24L);
			int num4 = (int)(num2 / 600000000L % 60L);
			int num5 = (int)(num2 / 10000000L % 60L);
			int num6 = (int)(num2 % 10000000L);
			int i = 0;
			StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
			while (i < format.Length)
			{
				char c = format[i];
				int num8;
				if (c <= 'F')
				{
					if (c <= '%')
					{
						if (c != '"')
						{
							if (c != '%')
							{
								goto IL_34D;
							}
							int num7 = DateTimeFormat.ParseNextChar(format, i);
							if (num7 >= 0 && num7 != 37)
							{
								stringBuilder.Append(TimeSpanFormat.FormatCustomized(value, ((char)num7).ToString(), dtfi));
								num8 = 2;
								goto IL_35D;
							}
							throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
						}
					}
					else if (c != '\'')
					{
						if (c != 'F')
						{
							goto IL_34D;
						}
						num8 = DateTimeFormat.ParseRepeatPattern(format, i, c);
						if (num8 > 7)
						{
							throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
						}
						long num9 = (long)num6;
						num9 /= (long)Math.Pow(10.0, (double)(7 - num8));
						int num10 = num8;
						while (num10 > 0 && num9 % 10L == 0L)
						{
							num9 /= 10L;
							num10--;
						}
						if (num10 > 0)
						{
							stringBuilder.Append(num9.ToString(DateTimeFormat.fixedNumberFormats[num10 - 1], CultureInfo.InvariantCulture));
							goto IL_35D;
						}
						goto IL_35D;
					}
					StringBuilder stringBuilder2 = new StringBuilder();
					num8 = DateTimeFormat.ParseQuoteString(format, i, stringBuilder2);
					stringBuilder.Append(stringBuilder2);
				}
				else if (c <= 'h')
				{
					if (c != '\\')
					{
						switch (c)
						{
						case 'd':
							num8 = DateTimeFormat.ParseRepeatPattern(format, i, c);
							if (num8 > 8)
							{
								throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
							}
							DateTimeFormat.FormatDigits(stringBuilder, num, num8, true);
							break;
						case 'e':
						case 'g':
							goto IL_34D;
						case 'f':
						{
							num8 = DateTimeFormat.ParseRepeatPattern(format, i, c);
							if (num8 > 7)
							{
								throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
							}
							long num9 = (long)num6;
							stringBuilder.Append((num9 / (long)Math.Pow(10.0, (double)(7 - num8))).ToString(DateTimeFormat.fixedNumberFormats[num8 - 1], CultureInfo.InvariantCulture));
							break;
						}
						case 'h':
							num8 = DateTimeFormat.ParseRepeatPattern(format, i, c);
							if (num8 > 2)
							{
								throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
							}
							DateTimeFormat.FormatDigits(stringBuilder, num3, num8);
							break;
						default:
							goto IL_34D;
						}
					}
					else
					{
						int num7 = DateTimeFormat.ParseNextChar(format, i);
						if (num7 < 0)
						{
							throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
						}
						stringBuilder.Append((char)num7);
						num8 = 2;
					}
				}
				else if (c != 'm')
				{
					if (c != 's')
					{
						goto IL_34D;
					}
					num8 = DateTimeFormat.ParseRepeatPattern(format, i, c);
					if (num8 > 2)
					{
						throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
					}
					DateTimeFormat.FormatDigits(stringBuilder, num5, num8);
				}
				else
				{
					num8 = DateTimeFormat.ParseRepeatPattern(format, i, c);
					if (num8 > 2)
					{
						throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
					}
					DateTimeFormat.FormatDigits(stringBuilder, num4, num8);
				}
				IL_35D:
				i += num8;
				continue;
				IL_34D:
				throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
			}
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}

		// Token: 0x060031F6 RID: 12790 RVA: 0x000BF2FD File Offset: 0x000BD4FD
		// Note: this type is marked as 'beforefieldinit'.
		static TimeSpanFormat()
		{
		}

		// Token: 0x04001541 RID: 5441
		internal static readonly TimeSpanFormat.FormatLiterals PositiveInvariantFormatLiterals = TimeSpanFormat.FormatLiterals.InitInvariant(false);

		// Token: 0x04001542 RID: 5442
		internal static readonly TimeSpanFormat.FormatLiterals NegativeInvariantFormatLiterals = TimeSpanFormat.FormatLiterals.InitInvariant(true);

		// Token: 0x02000B75 RID: 2933
		internal enum Pattern
		{
			// Token: 0x04003481 RID: 13441
			None,
			// Token: 0x04003482 RID: 13442
			Minimum,
			// Token: 0x04003483 RID: 13443
			Full
		}

		// Token: 0x02000B76 RID: 2934
		internal struct FormatLiterals
		{
			// Token: 0x1700124E RID: 4686
			// (get) Token: 0x06006C34 RID: 27700 RVA: 0x001761D5 File Offset: 0x001743D5
			internal string Start
			{
				get
				{
					return this.literals[0];
				}
			}

			// Token: 0x1700124F RID: 4687
			// (get) Token: 0x06006C35 RID: 27701 RVA: 0x001761DF File Offset: 0x001743DF
			internal string DayHourSep
			{
				get
				{
					return this.literals[1];
				}
			}

			// Token: 0x17001250 RID: 4688
			// (get) Token: 0x06006C36 RID: 27702 RVA: 0x001761E9 File Offset: 0x001743E9
			internal string HourMinuteSep
			{
				get
				{
					return this.literals[2];
				}
			}

			// Token: 0x17001251 RID: 4689
			// (get) Token: 0x06006C37 RID: 27703 RVA: 0x001761F3 File Offset: 0x001743F3
			internal string MinuteSecondSep
			{
				get
				{
					return this.literals[3];
				}
			}

			// Token: 0x17001252 RID: 4690
			// (get) Token: 0x06006C38 RID: 27704 RVA: 0x001761FD File Offset: 0x001743FD
			internal string SecondFractionSep
			{
				get
				{
					return this.literals[4];
				}
			}

			// Token: 0x17001253 RID: 4691
			// (get) Token: 0x06006C39 RID: 27705 RVA: 0x00176207 File Offset: 0x00174407
			internal string End
			{
				get
				{
					return this.literals[5];
				}
			}

			// Token: 0x06006C3A RID: 27706 RVA: 0x00176214 File Offset: 0x00174414
			internal static TimeSpanFormat.FormatLiterals InitInvariant(bool isNegative)
			{
				TimeSpanFormat.FormatLiterals formatLiterals = new TimeSpanFormat.FormatLiterals
				{
					literals = new string[6]
				};
				formatLiterals.literals[0] = (isNegative ? "-" : string.Empty);
				formatLiterals.literals[1] = ".";
				formatLiterals.literals[2] = ":";
				formatLiterals.literals[3] = ":";
				formatLiterals.literals[4] = ".";
				formatLiterals.literals[5] = string.Empty;
				formatLiterals.AppCompatLiteral = ":.";
				formatLiterals.dd = 2;
				formatLiterals.hh = 2;
				formatLiterals.mm = 2;
				formatLiterals.ss = 2;
				formatLiterals.ff = 7;
				return formatLiterals;
			}

			// Token: 0x06006C3B RID: 27707 RVA: 0x001762C4 File Offset: 0x001744C4
			internal void Init(string format, bool useInvariantFieldLengths)
			{
				this.literals = new string[6];
				for (int i = 0; i < this.literals.Length; i++)
				{
					this.literals[i] = string.Empty;
				}
				this.dd = 0;
				this.hh = 0;
				this.mm = 0;
				this.ss = 0;
				this.ff = 0;
				StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
				bool flag = false;
				char c = '\'';
				int num = 0;
				int j = 0;
				while (j < format.Length)
				{
					char c2 = format[j];
					if (c2 <= 'F')
					{
						if (c2 <= '%')
						{
							if (c2 != '"')
							{
								if (c2 != '%')
								{
									goto IL_1AF;
								}
								goto IL_1AF;
							}
						}
						else if (c2 != '\'')
						{
							if (c2 != 'F')
							{
								goto IL_1AF;
							}
							goto IL_19A;
						}
						if (flag && c == format[j])
						{
							if (num < 0 || num > 5)
							{
								return;
							}
							this.literals[num] = stringBuilder.ToString();
							stringBuilder.Length = 0;
							flag = false;
						}
						else if (!flag)
						{
							c = format[j];
							flag = true;
						}
					}
					else if (c2 <= 'h')
					{
						if (c2 != '\\')
						{
							switch (c2)
							{
							case 'd':
								if (!flag)
								{
									num = 1;
									this.dd++;
								}
								break;
							case 'e':
							case 'g':
								goto IL_1AF;
							case 'f':
								goto IL_19A;
							case 'h':
								if (!flag)
								{
									num = 2;
									this.hh++;
								}
								break;
							default:
								goto IL_1AF;
							}
						}
						else
						{
							if (flag)
							{
								goto IL_1AF;
							}
							j++;
						}
					}
					else if (c2 != 'm')
					{
						if (c2 != 's')
						{
							goto IL_1AF;
						}
						if (!flag)
						{
							num = 4;
							this.ss++;
						}
					}
					else if (!flag)
					{
						num = 3;
						this.mm++;
					}
					IL_1BE:
					j++;
					continue;
					IL_19A:
					if (!flag)
					{
						num = 5;
						this.ff++;
						goto IL_1BE;
					}
					goto IL_1BE;
					IL_1AF:
					stringBuilder.Append(format[j]);
					goto IL_1BE;
				}
				this.AppCompatLiteral = this.MinuteSecondSep + this.SecondFractionSep;
				if (useInvariantFieldLengths)
				{
					this.dd = 2;
					this.hh = 2;
					this.mm = 2;
					this.ss = 2;
					this.ff = 7;
				}
				else
				{
					if (this.dd < 1 || this.dd > 2)
					{
						this.dd = 2;
					}
					if (this.hh < 1 || this.hh > 2)
					{
						this.hh = 2;
					}
					if (this.mm < 1 || this.mm > 2)
					{
						this.mm = 2;
					}
					if (this.ss < 1 || this.ss > 2)
					{
						this.ss = 2;
					}
					if (this.ff < 1 || this.ff > 7)
					{
						this.ff = 7;
					}
				}
				StringBuilderCache.Release(stringBuilder);
			}

			// Token: 0x04003484 RID: 13444
			internal string AppCompatLiteral;

			// Token: 0x04003485 RID: 13445
			internal int dd;

			// Token: 0x04003486 RID: 13446
			internal int hh;

			// Token: 0x04003487 RID: 13447
			internal int mm;

			// Token: 0x04003488 RID: 13448
			internal int ss;

			// Token: 0x04003489 RID: 13449
			internal int ff;

			// Token: 0x0400348A RID: 13450
			private string[] literals;
		}
	}
}
