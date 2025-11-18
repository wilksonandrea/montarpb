using System;
using System.Collections;

namespace System.Runtime.Serialization
{
	// Token: 0x0200075D RID: 1885
	internal class SurrogateHashtable : Hashtable
	{
		// Token: 0x060052F9 RID: 21241 RVA: 0x00123898 File Offset: 0x00121A98
		internal SurrogateHashtable(int size)
			: base(size)
		{
		}

		// Token: 0x060052FA RID: 21242 RVA: 0x001238A4 File Offset: 0x00121AA4
		protected override bool KeyEquals(object key, object item)
		{
			SurrogateKey surrogateKey = (SurrogateKey)item;
			SurrogateKey surrogateKey2 = (SurrogateKey)key;
			return surrogateKey2.m_type == surrogateKey.m_type && (surrogateKey2.m_context.m_state & surrogateKey.m_context.m_state) == surrogateKey.m_context.m_state && surrogateKey2.m_context.Context == surrogateKey.m_context.Context;
		}
	}
}
