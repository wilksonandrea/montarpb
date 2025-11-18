using System;
using System.Collections;

namespace System.Runtime.Serialization
{
	// Token: 0x0200075A RID: 1882
	internal static class SerializationEventsCache
	{
		// Token: 0x060052EE RID: 21230 RVA: 0x001235A4 File Offset: 0x001217A4
		internal static SerializationEvents GetSerializationEventsForType(Type t)
		{
			SerializationEvents serializationEvents;
			if ((serializationEvents = (SerializationEvents)SerializationEventsCache.cache[t]) == null)
			{
				object syncRoot = SerializationEventsCache.cache.SyncRoot;
				lock (syncRoot)
				{
					if ((serializationEvents = (SerializationEvents)SerializationEventsCache.cache[t]) == null)
					{
						serializationEvents = new SerializationEvents(t);
						SerializationEventsCache.cache[t] = serializationEvents;
					}
				}
			}
			return serializationEvents;
		}

		// Token: 0x060052EF RID: 21231 RVA: 0x00123620 File Offset: 0x00121820
		// Note: this type is marked as 'beforefieldinit'.
		static SerializationEventsCache()
		{
		}

		// Token: 0x040024CC RID: 9420
		private static Hashtable cache = new Hashtable();
	}
}
