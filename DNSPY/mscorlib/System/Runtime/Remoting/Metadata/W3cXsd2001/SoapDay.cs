using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007E7 RID: 2023
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapDay : ISoapXsd
	{
		// Token: 0x17000E6F RID: 3695
		// (get) Token: 0x06005765 RID: 22373 RVA: 0x00135ADC File Offset: 0x00133CDC
		public static string XsdType
		{
			get
			{
				return "gDay";
			}
		}

		// Token: 0x06005766 RID: 22374 RVA: 0x00135AE3 File Offset: 0x00133CE3
		public string GetXsdType()
		{
			return SoapDay.XsdType;
		}

		// Token: 0x06005767 RID: 22375 RVA: 0x00135AEA File Offset: 0x00133CEA
		public SoapDay()
		{
		}

		// Token: 0x06005768 RID: 22376 RVA: 0x00135AFD File Offset: 0x00133CFD
		public SoapDay(DateTime value)
		{
			this._value = value;
		}

		// Token: 0x17000E70 RID: 3696
		// (get) Token: 0x06005769 RID: 22377 RVA: 0x00135B17 File Offset: 0x00133D17
		// (set) Token: 0x0600576A RID: 22378 RVA: 0x00135B1F File Offset: 0x00133D1F
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

		// Token: 0x0600576B RID: 22379 RVA: 0x00135B28 File Offset: 0x00133D28
		public override string ToString()
		{
			return this._value.ToString("---dd", CultureInfo.InvariantCulture);
		}

		// Token: 0x0600576C RID: 22380 RVA: 0x00135B3F File Offset: 0x00133D3F
		public static SoapDay Parse(string value)
		{
			return new SoapDay(DateTime.ParseExact(value, SoapDay.formats, CultureInfo.InvariantCulture, DateTimeStyles.None));
		}

		// Token: 0x0600576D RID: 22381 RVA: 0x00135B57 File Offset: 0x00133D57
		// Note: this type is marked as 'beforefieldinit'.
		static SoapDay()
		{
		}

		// Token: 0x0400281B RID: 10267
		private DateTime _value = DateTime.MinValue;

		// Token: 0x0400281C RID: 10268
		private static string[] formats = new string[] { "---dd", "---ddzzz" };
	}
}
