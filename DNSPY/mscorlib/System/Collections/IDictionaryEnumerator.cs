using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	// Token: 0x0200049D RID: 1181
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public interface IDictionaryEnumerator : IEnumerator
	{
		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x060038BB RID: 14523
		[__DynamicallyInvokable]
		object Key
		{
			[__DynamicallyInvokable]
			get;
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x060038BC RID: 14524
		[__DynamicallyInvokable]
		object Value
		{
			[__DynamicallyInvokable]
			get;
		}

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x060038BD RID: 14525
		[__DynamicallyInvokable]
		DictionaryEntry Entry
		{
			[__DynamicallyInvokable]
			get;
		}
	}
}
