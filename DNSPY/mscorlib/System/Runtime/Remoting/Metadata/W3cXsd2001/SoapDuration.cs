using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007E1 RID: 2017
	[ComVisible(true)]
	public sealed class SoapDuration
	{
		// Token: 0x17000E61 RID: 3681
		// (get) Token: 0x0600572A RID: 22314 RVA: 0x001350BA File Offset: 0x001332BA
		public static string XsdType
		{
			get
			{
				return "duration";
			}
		}

		// Token: 0x0600572B RID: 22315 RVA: 0x001350C4 File Offset: 0x001332C4
		private static void CarryOver(int inDays, out int years, out int months, out int days)
		{
			years = inDays / 360;
			int num = years * 360;
			months = Math.Max(0, inDays - num) / 30;
			int num2 = months * 30;
			days = Math.Max(0, inDays - (num + num2));
			days = inDays % 30;
		}

		// Token: 0x0600572C RID: 22316 RVA: 0x0013510C File Offset: 0x0013330C
		[SecuritySafeCritical]
		public static string ToString(TimeSpan timeSpan)
		{
			StringBuilder stringBuilder = new StringBuilder(10);
			stringBuilder.Length = 0;
			if (TimeSpan.Compare(timeSpan, TimeSpan.Zero) < 1)
			{
				stringBuilder.Append('-');
			}
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			SoapDuration.CarryOver(Math.Abs(timeSpan.Days), out num, out num2, out num3);
			stringBuilder.Append('P');
			stringBuilder.Append(num);
			stringBuilder.Append('Y');
			stringBuilder.Append(num2);
			stringBuilder.Append('M');
			stringBuilder.Append(num3);
			stringBuilder.Append("DT");
			stringBuilder.Append(Math.Abs(timeSpan.Hours));
			stringBuilder.Append('H');
			stringBuilder.Append(Math.Abs(timeSpan.Minutes));
			stringBuilder.Append('M');
			stringBuilder.Append(Math.Abs(timeSpan.Seconds));
			long num4 = Math.Abs(timeSpan.Ticks % 864000000000L);
			int num5 = (int)(num4 % 10000000L);
			if (num5 != 0)
			{
				string text = ParseNumbers.IntToString(num5, 10, 7, '0', 0);
				stringBuilder.Append('.');
				stringBuilder.Append(text);
			}
			stringBuilder.Append('S');
			return stringBuilder.ToString();
		}

		// Token: 0x0600572D RID: 22317 RVA: 0x00135240 File Offset: 0x00133440
		public static TimeSpan Parse(string value)
		{
			int num = 1;
			TimeSpan timeSpan;
			try
			{
				if (value == null)
				{
					timeSpan = TimeSpan.Zero;
				}
				else
				{
					if (value[0] == '-')
					{
						num = -1;
					}
					char[] array = value.ToCharArray();
					int[] array2 = new int[7];
					string text = "0";
					string text2 = "0";
					string text3 = "0";
					string text4 = "0";
					string text5 = "0";
					string text6 = "0";
					string text7 = "0";
					bool flag = false;
					bool flag2 = false;
					int num2 = 0;
					for (int i = 0; i < array.Length; i++)
					{
						char c = array[i];
						if (c <= 'H')
						{
							if (c != '.')
							{
								if (c != 'D')
								{
									if (c == 'H')
									{
										text4 = new string(array, num2, i - num2);
										num2 = i + 1;
									}
								}
								else
								{
									text3 = new string(array, num2, i - num2);
									num2 = i + 1;
								}
							}
							else
							{
								flag2 = true;
								text6 = new string(array, num2, i - num2);
								num2 = i + 1;
							}
						}
						else if (c <= 'T')
						{
							if (c != 'M')
							{
								switch (c)
								{
								case 'P':
									num2 = i + 1;
									break;
								case 'S':
									if (!flag2)
									{
										text6 = new string(array, num2, i - num2);
									}
									else
									{
										text7 = new string(array, num2, i - num2);
									}
									break;
								case 'T':
									flag = true;
									num2 = i + 1;
									break;
								}
							}
							else
							{
								if (flag)
								{
									text5 = new string(array, num2, i - num2);
								}
								else
								{
									text2 = new string(array, num2, i - num2);
								}
								num2 = i + 1;
							}
						}
						else if (c != 'Y')
						{
							if (c != 'Z')
							{
							}
						}
						else
						{
							text = new string(array, num2, i - num2);
							num2 = i + 1;
						}
					}
					long num3 = (long)num * ((long.Parse(text, CultureInfo.InvariantCulture) * 360L + long.Parse(text2, CultureInfo.InvariantCulture) * 30L + long.Parse(text3, CultureInfo.InvariantCulture)) * 864000000000L + long.Parse(text4, CultureInfo.InvariantCulture) * 36000000000L + long.Parse(text5, CultureInfo.InvariantCulture) * 600000000L + Convert.ToInt64(double.Parse(text6 + "." + text7, CultureInfo.InvariantCulture) * 10000000.0));
					timeSpan = new TimeSpan(num3);
				}
			}
			catch (Exception)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), "xsd:duration", value));
			}
			return timeSpan;
		}

		// Token: 0x0600572E RID: 22318 RVA: 0x001354DC File Offset: 0x001336DC
		public SoapDuration()
		{
		}
	}
}
