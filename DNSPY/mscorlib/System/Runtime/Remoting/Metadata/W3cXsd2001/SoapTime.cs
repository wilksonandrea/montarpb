using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007E2 RID: 2018
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapTime : ISoapXsd
	{
		// Token: 0x17000E62 RID: 3682
		// (get) Token: 0x0600572F RID: 22319 RVA: 0x001354E4 File Offset: 0x001336E4
		public static string XsdType
		{
			get
			{
				return "time";
			}
		}

		// Token: 0x06005730 RID: 22320 RVA: 0x001354EB File Offset: 0x001336EB
		public string GetXsdType()
		{
			return SoapTime.XsdType;
		}

		// Token: 0x06005731 RID: 22321 RVA: 0x001354F2 File Offset: 0x001336F2
		public SoapTime()
		{
		}

		// Token: 0x06005732 RID: 22322 RVA: 0x00135505 File Offset: 0x00133705
		public SoapTime(DateTime value)
		{
			this._value = value;
		}

		// Token: 0x17000E63 RID: 3683
		// (get) Token: 0x06005733 RID: 22323 RVA: 0x0013551F File Offset: 0x0013371F
		// (set) Token: 0x06005734 RID: 22324 RVA: 0x00135527 File Offset: 0x00133727
		public DateTime Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = new DateTime(1, 1, 1, value.Hour, value.Minute, value.Second, value.Millisecond);
			}
		}

		// Token: 0x06005735 RID: 22325 RVA: 0x00135553 File Offset: 0x00133753
		public override string ToString()
		{
			return this._value.ToString("HH:mm:ss.fffffffzzz", CultureInfo.InvariantCulture);
		}

		// Token: 0x06005736 RID: 22326 RVA: 0x0013556C File Offset: 0x0013376C
		public static SoapTime Parse(string value)
		{
			string text = value;
			if (value.EndsWith("Z", StringComparison.Ordinal))
			{
				text = value.Substring(0, value.Length - 1) + "-00:00";
			}
			return new SoapTime(DateTime.ParseExact(text, SoapTime.formats, CultureInfo.InvariantCulture, DateTimeStyles.None));
		}

		// Token: 0x06005737 RID: 22327 RVA: 0x001355BC File Offset: 0x001337BC
		// Note: this type is marked as 'beforefieldinit'.
		static SoapTime()
		{
		}

		// Token: 0x0400280E RID: 10254
		private DateTime _value = DateTime.MinValue;

		// Token: 0x0400280F RID: 10255
		private static string[] formats = new string[]
		{
			"HH:mm:ss.fffffffzzz", "HH:mm:ss.ffff", "HH:mm:ss.ffffzzz", "HH:mm:ss.fff", "HH:mm:ss.fffzzz", "HH:mm:ss.ff", "HH:mm:ss.ffzzz", "HH:mm:ss.f", "HH:mm:ss.fzzz", "HH:mm:ss",
			"HH:mm:sszzz", "HH:mm:ss.fffff", "HH:mm:ss.fffffzzz", "HH:mm:ss.ffffff", "HH:mm:ss.ffffffzzz", "HH:mm:ss.fffffff", "HH:mm:ss.ffffffff", "HH:mm:ss.ffffffffzzz", "HH:mm:ss.fffffffff", "HH:mm:ss.fffffffffzzz",
			"HH:mm:ss.fffffffff", "HH:mm:ss.fffffffffzzz"
		};
	}
}
