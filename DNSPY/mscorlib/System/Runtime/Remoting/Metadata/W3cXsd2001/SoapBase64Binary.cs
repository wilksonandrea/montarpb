using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007EA RID: 2026
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapBase64Binary : ISoapXsd
	{
		// Token: 0x17000E75 RID: 3701
		// (get) Token: 0x06005781 RID: 22401 RVA: 0x00135DC8 File Offset: 0x00133FC8
		public static string XsdType
		{
			get
			{
				return "base64Binary";
			}
		}

		// Token: 0x06005782 RID: 22402 RVA: 0x00135DCF File Offset: 0x00133FCF
		public string GetXsdType()
		{
			return SoapBase64Binary.XsdType;
		}

		// Token: 0x06005783 RID: 22403 RVA: 0x00135DD6 File Offset: 0x00133FD6
		public SoapBase64Binary()
		{
		}

		// Token: 0x06005784 RID: 22404 RVA: 0x00135DDE File Offset: 0x00133FDE
		public SoapBase64Binary(byte[] value)
		{
			this._value = value;
		}

		// Token: 0x17000E76 RID: 3702
		// (get) Token: 0x06005785 RID: 22405 RVA: 0x00135DED File Offset: 0x00133FED
		// (set) Token: 0x06005786 RID: 22406 RVA: 0x00135DF5 File Offset: 0x00133FF5
		public byte[] Value
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

		// Token: 0x06005787 RID: 22407 RVA: 0x00135DFE File Offset: 0x00133FFE
		public override string ToString()
		{
			if (this._value == null)
			{
				return null;
			}
			return SoapType.LineFeedsBin64(Convert.ToBase64String(this._value));
		}

		// Token: 0x06005788 RID: 22408 RVA: 0x00135E1C File Offset: 0x0013401C
		public static SoapBase64Binary Parse(string value)
		{
			if (value == null || value.Length == 0)
			{
				return new SoapBase64Binary(new byte[0]);
			}
			byte[] array;
			try
			{
				array = Convert.FromBase64String(SoapType.FilterBin64(value));
			}
			catch (Exception)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), "base64Binary", value));
			}
			return new SoapBase64Binary(array);
		}

		// Token: 0x04002821 RID: 10273
		private byte[] _value;
	}
}
