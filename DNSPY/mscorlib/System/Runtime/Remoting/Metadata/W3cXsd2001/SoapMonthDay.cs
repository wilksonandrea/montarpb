using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007E6 RID: 2022
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapMonthDay : ISoapXsd
	{
		// Token: 0x17000E6D RID: 3693
		// (get) Token: 0x0600575C RID: 22364 RVA: 0x00135A44 File Offset: 0x00133C44
		public static string XsdType
		{
			get
			{
				return "gMonthDay";
			}
		}

		// Token: 0x0600575D RID: 22365 RVA: 0x00135A4B File Offset: 0x00133C4B
		public string GetXsdType()
		{
			return SoapMonthDay.XsdType;
		}

		// Token: 0x0600575E RID: 22366 RVA: 0x00135A52 File Offset: 0x00133C52
		public SoapMonthDay()
		{
		}

		// Token: 0x0600575F RID: 22367 RVA: 0x00135A65 File Offset: 0x00133C65
		public SoapMonthDay(DateTime value)
		{
			this._value = value;
		}

		// Token: 0x17000E6E RID: 3694
		// (get) Token: 0x06005760 RID: 22368 RVA: 0x00135A7F File Offset: 0x00133C7F
		// (set) Token: 0x06005761 RID: 22369 RVA: 0x00135A87 File Offset: 0x00133C87
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

		// Token: 0x06005762 RID: 22370 RVA: 0x00135A90 File Offset: 0x00133C90
		public override string ToString()
		{
			return this._value.ToString("'--'MM'-'dd", CultureInfo.InvariantCulture);
		}

		// Token: 0x06005763 RID: 22371 RVA: 0x00135AA7 File Offset: 0x00133CA7
		public static SoapMonthDay Parse(string value)
		{
			return new SoapMonthDay(DateTime.ParseExact(value, SoapMonthDay.formats, CultureInfo.InvariantCulture, DateTimeStyles.None));
		}

		// Token: 0x06005764 RID: 22372 RVA: 0x00135ABF File Offset: 0x00133CBF
		// Note: this type is marked as 'beforefieldinit'.
		static SoapMonthDay()
		{
		}

		// Token: 0x04002819 RID: 10265
		private DateTime _value = DateTime.MinValue;

		// Token: 0x0400281A RID: 10266
		private static string[] formats = new string[] { "--MM-dd", "--MM-ddzzz" };
	}
}
