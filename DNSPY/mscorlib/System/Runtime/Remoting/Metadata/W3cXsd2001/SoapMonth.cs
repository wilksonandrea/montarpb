using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007E8 RID: 2024
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapMonth : ISoapXsd
	{
		// Token: 0x17000E71 RID: 3697
		// (get) Token: 0x0600576E RID: 22382 RVA: 0x00135B74 File Offset: 0x00133D74
		public static string XsdType
		{
			get
			{
				return "gMonth";
			}
		}

		// Token: 0x0600576F RID: 22383 RVA: 0x00135B7B File Offset: 0x00133D7B
		public string GetXsdType()
		{
			return SoapMonth.XsdType;
		}

		// Token: 0x06005770 RID: 22384 RVA: 0x00135B82 File Offset: 0x00133D82
		public SoapMonth()
		{
		}

		// Token: 0x06005771 RID: 22385 RVA: 0x00135B95 File Offset: 0x00133D95
		public SoapMonth(DateTime value)
		{
			this._value = value;
		}

		// Token: 0x17000E72 RID: 3698
		// (get) Token: 0x06005772 RID: 22386 RVA: 0x00135BAF File Offset: 0x00133DAF
		// (set) Token: 0x06005773 RID: 22387 RVA: 0x00135BB7 File Offset: 0x00133DB7
		public DateTime Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x06005774 RID: 22388 RVA: 0x00135BC0 File Offset: 0x00133DC0
		public override string ToString()
		{
			return this._value.ToString("--MM--", CultureInfo.InvariantCulture);
		}

		// Token: 0x06005775 RID: 22389 RVA: 0x00135BD7 File Offset: 0x00133DD7
		public static SoapMonth Parse(string value)
		{
			return new SoapMonth(DateTime.ParseExact(value, SoapMonth.formats, CultureInfo.InvariantCulture, DateTimeStyles.None));
		}

		// Token: 0x06005776 RID: 22390 RVA: 0x00135BEF File Offset: 0x00133DEF
		// Note: this type is marked as 'beforefieldinit'.
		static SoapMonth()
		{
		}

		// Token: 0x0400281D RID: 10269
		private DateTime _value = DateTime.MinValue;

		// Token: 0x0400281E RID: 10270
		private static string[] formats = new string[] { "--MM--", "--MM--zzz" };
	}
}
