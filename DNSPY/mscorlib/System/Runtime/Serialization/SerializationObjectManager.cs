using System;
using System.Collections;
using System.Security;

namespace System.Runtime.Serialization
{
	// Token: 0x02000758 RID: 1880
	public sealed class SerializationObjectManager
	{
		// Token: 0x060052E2 RID: 21218 RVA: 0x0012310A File Offset: 0x0012130A
		public SerializationObjectManager(StreamingContext context)
		{
			this.m_context = context;
			this.m_objectSeenTable = new Hashtable();
		}

		// Token: 0x060052E3 RID: 21219 RVA: 0x00123130 File Offset: 0x00121330
		[SecurityCritical]
		public void RegisterObject(object obj)
		{
			SerializationEvents serializationEventsForType = SerializationEventsCache.GetSerializationEventsForType(obj.GetType());
			if (serializationEventsForType.HasOnSerializingEvents && this.m_objectSeenTable[obj] == null)
			{
				this.m_objectSeenTable[obj] = true;
				serializationEventsForType.InvokeOnSerializing(obj, this.m_context);
				this.AddOnSerialized(obj);
			}
		}

		// Token: 0x060052E4 RID: 21220 RVA: 0x00123185 File Offset: 0x00121385
		public void RaiseOnSerializedEvent()
		{
			if (this.m_onSerializedHandler != null)
			{
				this.m_onSerializedHandler(this.m_context);
			}
		}

		// Token: 0x060052E5 RID: 21221 RVA: 0x001231A0 File Offset: 0x001213A0
		[SecuritySafeCritical]
		private void AddOnSerialized(object obj)
		{
			SerializationEvents serializationEventsForType = SerializationEventsCache.GetSerializationEventsForType(obj.GetType());
			this.m_onSerializedHandler = serializationEventsForType.AddOnSerialized(obj, this.m_onSerializedHandler);
		}

		// Token: 0x040024C5 RID: 9413
		private Hashtable m_objectSeenTable = new Hashtable();

		// Token: 0x040024C6 RID: 9414
		private SerializationEventHandler m_onSerializedHandler;

		// Token: 0x040024C7 RID: 9415
		private StreamingContext m_context;
	}
}
