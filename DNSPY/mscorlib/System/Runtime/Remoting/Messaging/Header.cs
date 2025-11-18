using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200088B RID: 2187
	[ComVisible(true)]
	[Serializable]
	public class Header
	{
		// Token: 0x06005CAB RID: 23723 RVA: 0x00144DBF File Offset: 0x00142FBF
		public Header(string _Name, object _Value)
			: this(_Name, _Value, true)
		{
		}

		// Token: 0x06005CAC RID: 23724 RVA: 0x00144DCA File Offset: 0x00142FCA
		public Header(string _Name, object _Value, bool _MustUnderstand)
		{
			this.Name = _Name;
			this.Value = _Value;
			this.MustUnderstand = _MustUnderstand;
		}

		// Token: 0x06005CAD RID: 23725 RVA: 0x00144DE7 File Offset: 0x00142FE7
		public Header(string _Name, object _Value, bool _MustUnderstand, string _HeaderNamespace)
		{
			this.Name = _Name;
			this.Value = _Value;
			this.MustUnderstand = _MustUnderstand;
			this.HeaderNamespace = _HeaderNamespace;
		}

		// Token: 0x040029DB RID: 10715
		public string Name;

		// Token: 0x040029DC RID: 10716
		public object Value;

		// Token: 0x040029DD RID: 10717
		public bool MustUnderstand;

		// Token: 0x040029DE RID: 10718
		public string HeaderNamespace;
	}
}
