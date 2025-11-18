using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Serialization.Formatters
{
	// Token: 0x02000762 RID: 1890
	[ComVisible(true)]
	public interface ISoapMessage
	{
		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x060052FF RID: 21247
		// (set) Token: 0x06005300 RID: 21248
		string[] ParamNames { get; set; }

		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x06005301 RID: 21249
		// (set) Token: 0x06005302 RID: 21250
		object[] ParamValues { get; set; }

		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x06005303 RID: 21251
		// (set) Token: 0x06005304 RID: 21252
		Type[] ParamTypes { get; set; }

		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x06005305 RID: 21253
		// (set) Token: 0x06005306 RID: 21254
		string MethodName { get; set; }

		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x06005307 RID: 21255
		// (set) Token: 0x06005308 RID: 21256
		string XmlNameSpace { get; set; }

		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x06005309 RID: 21257
		// (set) Token: 0x0600530A RID: 21258
		Header[] Headers { get; set; }
	}
}
