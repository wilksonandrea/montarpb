using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;

namespace System.Runtime.Serialization
{
	// Token: 0x02000757 RID: 1879
	[Serializable]
	internal sealed class SafeSerializationManager : IObjectReference, ISerializable
	{
		// Token: 0x1400001D RID: 29
		// (add) Token: 0x060052D8 RID: 21208 RVA: 0x00122E40 File Offset: 0x00121040
		// (remove) Token: 0x060052D9 RID: 21209 RVA: 0x00122E78 File Offset: 0x00121078
		internal event EventHandler<SafeSerializationEventArgs> SerializeObjectState
		{
			[CompilerGenerated]
			add
			{
				EventHandler<SafeSerializationEventArgs> eventHandler = this.SerializeObjectState;
				EventHandler<SafeSerializationEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<SafeSerializationEventArgs> eventHandler3 = (EventHandler<SafeSerializationEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<SafeSerializationEventArgs>>(ref this.SerializeObjectState, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<SafeSerializationEventArgs> eventHandler = this.SerializeObjectState;
				EventHandler<SafeSerializationEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<SafeSerializationEventArgs> eventHandler3 = (EventHandler<SafeSerializationEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<SafeSerializationEventArgs>>(ref this.SerializeObjectState, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x060052DA RID: 21210 RVA: 0x00122EAD File Offset: 0x001210AD
		internal SafeSerializationManager()
		{
		}

		// Token: 0x060052DB RID: 21211 RVA: 0x00122EB8 File Offset: 0x001210B8
		[SecurityCritical]
		private SafeSerializationManager(SerializationInfo info, StreamingContext context)
		{
			RuntimeType runtimeType = info.GetValueNoThrow("CLR_SafeSerializationManager_RealType", typeof(RuntimeType)) as RuntimeType;
			if (runtimeType == null)
			{
				this.m_serializedStates = info.GetValue("m_serializedStates", typeof(List<object>)) as List<object>;
				return;
			}
			this.m_realType = runtimeType;
			this.m_savedSerializationInfo = info;
		}

		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x060052DC RID: 21212 RVA: 0x00122F1E File Offset: 0x0012111E
		internal bool IsActive
		{
			get
			{
				return this.SerializeObjectState != null;
			}
		}

		// Token: 0x060052DD RID: 21213 RVA: 0x00122F2C File Offset: 0x0012112C
		[SecurityCritical]
		internal void CompleteSerialization(object serializedObject, SerializationInfo info, StreamingContext context)
		{
			this.m_serializedStates = null;
			EventHandler<SafeSerializationEventArgs> serializeObjectState = this.SerializeObjectState;
			if (serializeObjectState != null)
			{
				SafeSerializationEventArgs safeSerializationEventArgs = new SafeSerializationEventArgs(context);
				serializeObjectState(serializedObject, safeSerializationEventArgs);
				this.m_serializedStates = safeSerializationEventArgs.SerializedStates;
				info.AddValue("CLR_SafeSerializationManager_RealType", serializedObject.GetType(), typeof(RuntimeType));
				info.SetType(typeof(SafeSerializationManager));
			}
		}

		// Token: 0x060052DE RID: 21214 RVA: 0x00122F90 File Offset: 0x00121190
		internal void CompleteDeserialization(object deserializedObject)
		{
			if (this.m_serializedStates != null)
			{
				foreach (object obj in this.m_serializedStates)
				{
					ISafeSerializationData safeSerializationData = (ISafeSerializationData)obj;
					safeSerializationData.CompleteDeserialization(deserializedObject);
				}
			}
		}

		// Token: 0x060052DF RID: 21215 RVA: 0x00122FEC File Offset: 0x001211EC
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("m_serializedStates", this.m_serializedStates, typeof(List<IDeserializationCallback>));
		}

		// Token: 0x060052E0 RID: 21216 RVA: 0x0012300C File Offset: 0x0012120C
		[SecurityCritical]
		object IObjectReference.GetRealObject(StreamingContext context)
		{
			if (this.m_realObject != null)
			{
				return this.m_realObject;
			}
			if (this.m_realType == null)
			{
				return this;
			}
			Stack stack = new Stack();
			RuntimeType runtimeType = this.m_realType;
			do
			{
				stack.Push(runtimeType);
				runtimeType = runtimeType.BaseType as RuntimeType;
			}
			while (runtimeType != typeof(object));
			RuntimeType runtimeType2;
			RuntimeConstructorInfo runtimeConstructorInfo;
			do
			{
				runtimeType2 = runtimeType;
				runtimeType = stack.Pop() as RuntimeType;
				runtimeConstructorInfo = runtimeType.GetSerializationCtor();
			}
			while (runtimeConstructorInfo != null && runtimeConstructorInfo.IsSecurityCritical);
			runtimeConstructorInfo = ObjectManager.GetConstructor(runtimeType2);
			object uninitializedObject = FormatterServices.GetUninitializedObject(this.m_realType);
			runtimeConstructorInfo.SerializationInvoke(uninitializedObject, this.m_savedSerializationInfo, context);
			this.m_savedSerializationInfo = null;
			this.m_realType = null;
			this.m_realObject = uninitializedObject;
			return uninitializedObject;
		}

		// Token: 0x060052E1 RID: 21217 RVA: 0x001230D0 File Offset: 0x001212D0
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			if (this.m_realObject != null)
			{
				SerializationEvents serializationEventsForType = SerializationEventsCache.GetSerializationEventsForType(this.m_realObject.GetType());
				serializationEventsForType.InvokeOnDeserialized(this.m_realObject, context);
				this.m_realObject = null;
			}
		}

		// Token: 0x040024BF RID: 9407
		private IList<object> m_serializedStates;

		// Token: 0x040024C0 RID: 9408
		private SerializationInfo m_savedSerializationInfo;

		// Token: 0x040024C1 RID: 9409
		private object m_realObject;

		// Token: 0x040024C2 RID: 9410
		private RuntimeType m_realType;

		// Token: 0x040024C3 RID: 9411
		[CompilerGenerated]
		private EventHandler<SafeSerializationEventArgs> SerializeObjectState;

		// Token: 0x040024C4 RID: 9412
		private const string RealTypeSerializationName = "CLR_SafeSerializationManager_RealType";
	}
}
