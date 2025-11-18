using System;
using System.Diagnostics;

namespace System.Collections
{
	// Token: 0x020004A3 RID: 1187
	[DebuggerDisplay("{value}", Name = "[{key}]", Type = "")]
	internal class KeyValuePairs
	{
		// Token: 0x060038D0 RID: 14544 RVA: 0x000D9A65 File Offset: 0x000D7C65
		public KeyValuePairs(object key, object value)
		{
			this.value = value;
			this.key = key;
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x060038D1 RID: 14545 RVA: 0x000D9A7B File Offset: 0x000D7C7B
		public object Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x060038D2 RID: 14546 RVA: 0x000D9A83 File Offset: 0x000D7C83
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x04001903 RID: 6403
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private object key;

		// Token: 0x04001904 RID: 6404
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private object value;
	}
}
