using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	// Token: 0x02000499 RID: 1177
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct DictionaryEntry
	{
		// Token: 0x060038A6 RID: 14502 RVA: 0x000D9A33 File Offset: 0x000D7C33
		[__DynamicallyInvokable]
		public DictionaryEntry(object key, object value)
		{
			this._key = key;
			this._value = value;
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x060038A7 RID: 14503 RVA: 0x000D9A43 File Offset: 0x000D7C43
		// (set) Token: 0x060038A8 RID: 14504 RVA: 0x000D9A4B File Offset: 0x000D7C4B
		[__DynamicallyInvokable]
		public object Key
		{
			[__DynamicallyInvokable]
			get
			{
				return this._key;
			}
			[__DynamicallyInvokable]
			set
			{
				this._key = value;
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x060038A9 RID: 14505 RVA: 0x000D9A54 File Offset: 0x000D7C54
		// (set) Token: 0x060038AA RID: 14506 RVA: 0x000D9A5C File Offset: 0x000D7C5C
		[__DynamicallyInvokable]
		public object Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this._value;
			}
			[__DynamicallyInvokable]
			set
			{
				this._value = value;
			}
		}

		// Token: 0x04001901 RID: 6401
		private object _key;

		// Token: 0x04001902 RID: 6402
		private object _value;
	}
}
