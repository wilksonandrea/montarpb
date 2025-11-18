using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007F1 RID: 2033
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapQName : ISoapXsd
	{
		// Token: 0x17000E83 RID: 3715
		// (get) Token: 0x060057B9 RID: 22457 RVA: 0x001362DF File Offset: 0x001344DF
		public static string XsdType
		{
			get
			{
				return "QName";
			}
		}

		// Token: 0x060057BA RID: 22458 RVA: 0x001362E6 File Offset: 0x001344E6
		public string GetXsdType()
		{
			return SoapQName.XsdType;
		}

		// Token: 0x060057BB RID: 22459 RVA: 0x001362ED File Offset: 0x001344ED
		public SoapQName()
		{
		}

		// Token: 0x060057BC RID: 22460 RVA: 0x001362F5 File Offset: 0x001344F5
		public SoapQName(string value)
		{
			this._name = value;
		}

		// Token: 0x060057BD RID: 22461 RVA: 0x00136304 File Offset: 0x00134504
		public SoapQName(string key, string name)
		{
			this._name = name;
			this._key = key;
		}

		// Token: 0x060057BE RID: 22462 RVA: 0x0013631A File Offset: 0x0013451A
		public SoapQName(string key, string name, string namespaceValue)
		{
			this._name = name;
			this._namespace = namespaceValue;
			this._key = key;
		}

		// Token: 0x17000E84 RID: 3716
		// (get) Token: 0x060057BF RID: 22463 RVA: 0x00136337 File Offset: 0x00134537
		// (set) Token: 0x060057C0 RID: 22464 RVA: 0x0013633F File Offset: 0x0013453F
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x060057C1 RID: 22465 RVA: 0x00136348 File Offset: 0x00134548
		// (set) Token: 0x060057C2 RID: 22466 RVA: 0x00136350 File Offset: 0x00134550
		public string Namespace
		{
			get
			{
				return this._namespace;
			}
			set
			{
				this._namespace = value;
			}
		}

		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x060057C3 RID: 22467 RVA: 0x00136359 File Offset: 0x00134559
		// (set) Token: 0x060057C4 RID: 22468 RVA: 0x00136361 File Offset: 0x00134561
		public string Key
		{
			get
			{
				return this._key;
			}
			set
			{
				this._key = value;
			}
		}

		// Token: 0x060057C5 RID: 22469 RVA: 0x0013636A File Offset: 0x0013456A
		public override string ToString()
		{
			if (this._key == null || this._key.Length == 0)
			{
				return this._name;
			}
			return this._key + ":" + this._name;
		}

		// Token: 0x060057C6 RID: 22470 RVA: 0x001363A0 File Offset: 0x001345A0
		public static SoapQName Parse(string value)
		{
			if (value == null)
			{
				return new SoapQName();
			}
			string text = "";
			string text2 = value;
			int num = value.IndexOf(':');
			if (num > 0)
			{
				text = value.Substring(0, num);
				text2 = value.Substring(num + 1);
			}
			return new SoapQName(text, text2);
		}

		// Token: 0x04002828 RID: 10280
		private string _name;

		// Token: 0x04002829 RID: 10281
		private string _namespace;

		// Token: 0x0400282A RID: 10282
		private string _key;
	}
}
