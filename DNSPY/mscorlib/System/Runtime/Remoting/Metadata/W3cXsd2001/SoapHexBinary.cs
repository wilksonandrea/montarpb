using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007E9 RID: 2025
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapHexBinary : ISoapXsd
	{
		// Token: 0x17000E73 RID: 3699
		// (get) Token: 0x06005777 RID: 22391 RVA: 0x00135C0C File Offset: 0x00133E0C
		public static string XsdType
		{
			get
			{
				return "hexBinary";
			}
		}

		// Token: 0x06005778 RID: 22392 RVA: 0x00135C13 File Offset: 0x00133E13
		public string GetXsdType()
		{
			return SoapHexBinary.XsdType;
		}

		// Token: 0x06005779 RID: 22393 RVA: 0x00135C1A File Offset: 0x00133E1A
		public SoapHexBinary()
		{
		}

		// Token: 0x0600577A RID: 22394 RVA: 0x00135C2F File Offset: 0x00133E2F
		public SoapHexBinary(byte[] value)
		{
			this._value = value;
		}

		// Token: 0x17000E74 RID: 3700
		// (get) Token: 0x0600577B RID: 22395 RVA: 0x00135C4B File Offset: 0x00133E4B
		// (set) Token: 0x0600577C RID: 22396 RVA: 0x00135C53 File Offset: 0x00133E53
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

		// Token: 0x0600577D RID: 22397 RVA: 0x00135C5C File Offset: 0x00133E5C
		public override string ToString()
		{
			this.sb.Length = 0;
			for (int i = 0; i < this._value.Length; i++)
			{
				string text = this._value[i].ToString("X", CultureInfo.InvariantCulture);
				if (text.Length == 1)
				{
					this.sb.Append('0');
				}
				this.sb.Append(text);
			}
			return this.sb.ToString();
		}

		// Token: 0x0600577E RID: 22398 RVA: 0x00135CD3 File Offset: 0x00133ED3
		public static SoapHexBinary Parse(string value)
		{
			return new SoapHexBinary(SoapHexBinary.ToByteArray(SoapType.FilterBin64(value)));
		}

		// Token: 0x0600577F RID: 22399 RVA: 0x00135CE8 File Offset: 0x00133EE8
		private static byte[] ToByteArray(string value)
		{
			char[] array = value.ToCharArray();
			if (array.Length % 2 != 0)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), "xsd:hexBinary", value));
			}
			byte[] array2 = new byte[array.Length / 2];
			for (int i = 0; i < array.Length / 2; i++)
			{
				array2[i] = SoapHexBinary.ToByte(array[i * 2], value) * 16 + SoapHexBinary.ToByte(array[i * 2 + 1], value);
			}
			return array2;
		}

		// Token: 0x06005780 RID: 22400 RVA: 0x00135D60 File Offset: 0x00133F60
		private static byte ToByte(char c, string value)
		{
			byte b = 0;
			string text = c.ToString();
			try
			{
				text = c.ToString();
				b = byte.Parse(text, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			}
			catch (Exception)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid", new object[] { "xsd:hexBinary", value }));
			}
			return b;
		}

		// Token: 0x0400281F RID: 10271
		private byte[] _value;

		// Token: 0x04002820 RID: 10272
		private StringBuilder sb = new StringBuilder(100);
	}
}
