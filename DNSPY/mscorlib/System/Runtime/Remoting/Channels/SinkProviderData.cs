using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000850 RID: 2128
	[ComVisible(true)]
	public class SinkProviderData
	{
		// Token: 0x06005A3B RID: 23099 RVA: 0x0013D5B7 File Offset: 0x0013B7B7
		public SinkProviderData(string name)
		{
			this._name = name;
		}

		// Token: 0x17000F01 RID: 3841
		// (get) Token: 0x06005A3C RID: 23100 RVA: 0x0013D5E1 File Offset: 0x0013B7E1
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000F02 RID: 3842
		// (get) Token: 0x06005A3D RID: 23101 RVA: 0x0013D5E9 File Offset: 0x0013B7E9
		public IDictionary Properties
		{
			get
			{
				return this._properties;
			}
		}

		// Token: 0x17000F03 RID: 3843
		// (get) Token: 0x06005A3E RID: 23102 RVA: 0x0013D5F1 File Offset: 0x0013B7F1
		public IList Children
		{
			get
			{
				return this._children;
			}
		}

		// Token: 0x04002906 RID: 10502
		private string _name;

		// Token: 0x04002907 RID: 10503
		private Hashtable _properties = new Hashtable(StringComparer.InvariantCultureIgnoreCase);

		// Token: 0x04002908 RID: 10504
		private ArrayList _children = new ArrayList();
	}
}
