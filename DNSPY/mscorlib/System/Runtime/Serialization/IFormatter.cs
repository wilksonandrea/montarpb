using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x02000734 RID: 1844
	[ComVisible(true)]
	public interface IFormatter
	{
		// Token: 0x060051AD RID: 20909
		object Deserialize(Stream serializationStream);

		// Token: 0x060051AE RID: 20910
		void Serialize(Stream serializationStream, object graph);

		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x060051AF RID: 20911
		// (set) Token: 0x060051B0 RID: 20912
		ISurrogateSelector SurrogateSelector { get; set; }

		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x060051B1 RID: 20913
		// (set) Token: 0x060051B2 RID: 20914
		SerializationBinder Binder { get; set; }

		// Token: 0x17000D73 RID: 3443
		// (get) Token: 0x060051B3 RID: 20915
		// (set) Token: 0x060051B4 RID: 20916
		StreamingContext Context { get; set; }
	}
}
