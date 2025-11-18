using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007E0 RID: 2016
	[ComVisible(true)]
	public sealed class SoapDateTime
	{
		// Token: 0x17000E60 RID: 3680
		// (get) Token: 0x06005725 RID: 22309 RVA: 0x00134F37 File Offset: 0x00133137
		public static string XsdType
		{
			get
			{
				return "dateTime";
			}
		}

		// Token: 0x06005726 RID: 22310 RVA: 0x00134F3E File Offset: 0x0013313E
		public static string ToString(DateTime value)
		{
			return value.ToString("yyyy-MM-dd'T'HH:mm:ss.fffffffzzz", CultureInfo.InvariantCulture);
		}

		// Token: 0x06005727 RID: 22311 RVA: 0x00134F54 File Offset: 0x00133154
		public static DateTime Parse(string value)
		{
			DateTime dateTime;
			try
			{
				if (value == null)
				{
					dateTime = DateTime.MinValue;
				}
				else
				{
					string text = value;
					if (value.EndsWith("Z", StringComparison.Ordinal))
					{
						text = value.Substring(0, value.Length - 1) + "-00:00";
					}
					dateTime = DateTime.ParseExact(text, SoapDateTime.formats, CultureInfo.InvariantCulture, DateTimeStyles.None);
				}
			}
			catch (Exception)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), "xsd:dateTime", value));
			}
			return dateTime;
		}

		// Token: 0x06005728 RID: 22312 RVA: 0x00134FDC File Offset: 0x001331DC
		public SoapDateTime()
		{
		}

		// Token: 0x06005729 RID: 22313 RVA: 0x00134FE4 File Offset: 0x001331E4
		// Note: this type is marked as 'beforefieldinit'.
		static SoapDateTime()
		{
		}

		// Token: 0x0400280D RID: 10253
		private static string[] formats = new string[]
		{
			"yyyy-MM-dd'T'HH:mm:ss.fffffffzzz", "yyyy-MM-dd'T'HH:mm:ss.ffff", "yyyy-MM-dd'T'HH:mm:ss.ffffzzz", "yyyy-MM-dd'T'HH:mm:ss.fff", "yyyy-MM-dd'T'HH:mm:ss.fffzzz", "yyyy-MM-dd'T'HH:mm:ss.ff", "yyyy-MM-dd'T'HH:mm:ss.ffzzz", "yyyy-MM-dd'T'HH:mm:ss.f", "yyyy-MM-dd'T'HH:mm:ss.fzzz", "yyyy-MM-dd'T'HH:mm:ss",
			"yyyy-MM-dd'T'HH:mm:sszzz", "yyyy-MM-dd'T'HH:mm:ss.fffff", "yyyy-MM-dd'T'HH:mm:ss.fffffzzz", "yyyy-MM-dd'T'HH:mm:ss.ffffff", "yyyy-MM-dd'T'HH:mm:ss.ffffffzzz", "yyyy-MM-dd'T'HH:mm:ss.fffffff", "yyyy-MM-dd'T'HH:mm:ss.ffffffff", "yyyy-MM-dd'T'HH:mm:ss.ffffffffzzz", "yyyy-MM-dd'T'HH:mm:ss.fffffffff", "yyyy-MM-dd'T'HH:mm:ss.fffffffffzzz",
			"yyyy-MM-dd'T'HH:mm:ss.ffffffffff", "yyyy-MM-dd'T'HH:mm:ss.ffffffffffzzz"
		};
	}
}
