using System;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A23 RID: 2595
	internal sealed class CLRIKeyValuePairImpl<K, V> : IKeyValuePair<K, V>
	{
		// Token: 0x06006606 RID: 26118 RVA: 0x00159A54 File Offset: 0x00157C54
		public CLRIKeyValuePairImpl([In] ref KeyValuePair<K, V> pair)
		{
			this._pair = pair;
		}

		// Token: 0x17001188 RID: 4488
		// (get) Token: 0x06006607 RID: 26119 RVA: 0x00159A68 File Offset: 0x00157C68
		public K Key
		{
			get
			{
				return this._pair.Key;
			}
		}

		// Token: 0x17001189 RID: 4489
		// (get) Token: 0x06006608 RID: 26120 RVA: 0x00159A84 File Offset: 0x00157C84
		public V Value
		{
			get
			{
				return this._pair.Value;
			}
		}

		// Token: 0x06006609 RID: 26121 RVA: 0x00159AA0 File Offset: 0x00157CA0
		internal static object BoxHelper(object pair)
		{
			KeyValuePair<K, V> keyValuePair = (KeyValuePair<K, V>)pair;
			return new CLRIKeyValuePairImpl<K, V>(ref keyValuePair);
		}

		// Token: 0x0600660A RID: 26122 RVA: 0x00159ABC File Offset: 0x00157CBC
		internal static object UnboxHelper(object wrapper)
		{
			CLRIKeyValuePairImpl<K, V> clrikeyValuePairImpl = (CLRIKeyValuePairImpl<K, V>)wrapper;
			return clrikeyValuePairImpl._pair;
		}

		// Token: 0x0600660B RID: 26123 RVA: 0x00159ADC File Offset: 0x00157CDC
		public override string ToString()
		{
			return this._pair.ToString();
		}

		// Token: 0x04002D4C RID: 11596
		private readonly KeyValuePair<K, V> _pair;
	}
}
