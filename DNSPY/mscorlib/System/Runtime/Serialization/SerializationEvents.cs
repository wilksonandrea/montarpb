using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security;

namespace System.Runtime.Serialization
{
	// Token: 0x02000759 RID: 1881
	internal class SerializationEvents
	{
		// Token: 0x060052E6 RID: 21222 RVA: 0x001231CC File Offset: 0x001213CC
		private List<MethodInfo> GetMethodsWithAttribute(Type attribute, Type t)
		{
			List<MethodInfo> list = new List<MethodInfo>();
			Type type = t;
			while (type != null && type != typeof(object))
			{
				RuntimeType runtimeType = (RuntimeType)type;
				MethodInfo[] methods = type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				foreach (MethodInfo methodInfo in methods)
				{
					if (methodInfo.IsDefined(attribute, false))
					{
						list.Add(methodInfo);
					}
				}
				type = type.BaseType;
			}
			list.Reverse();
			if (list.Count != 0)
			{
				return list;
			}
			return null;
		}

		// Token: 0x060052E7 RID: 21223 RVA: 0x00123258 File Offset: 0x00121458
		internal SerializationEvents(Type t)
		{
			this.m_OnSerializingMethods = this.GetMethodsWithAttribute(typeof(OnSerializingAttribute), t);
			this.m_OnSerializedMethods = this.GetMethodsWithAttribute(typeof(OnSerializedAttribute), t);
			this.m_OnDeserializingMethods = this.GetMethodsWithAttribute(typeof(OnDeserializingAttribute), t);
			this.m_OnDeserializedMethods = this.GetMethodsWithAttribute(typeof(OnDeserializedAttribute), t);
		}

		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x060052E8 RID: 21224 RVA: 0x001232C7 File Offset: 0x001214C7
		internal bool HasOnSerializingEvents
		{
			get
			{
				return this.m_OnSerializingMethods != null || this.m_OnSerializedMethods != null;
			}
		}

		// Token: 0x060052E9 RID: 21225 RVA: 0x001232DC File Offset: 0x001214DC
		[SecuritySafeCritical]
		internal void InvokeOnSerializing(object obj, StreamingContext context)
		{
			if (this.m_OnSerializingMethods != null)
			{
				object[] array = new object[] { context };
				SerializationEventHandler serializationEventHandler = null;
				foreach (MethodInfo methodInfo in this.m_OnSerializingMethods)
				{
					SerializationEventHandler serializationEventHandler2 = (SerializationEventHandler)Delegate.CreateDelegateNoSecurityCheck((RuntimeType)typeof(SerializationEventHandler), obj, methodInfo);
					serializationEventHandler = (SerializationEventHandler)Delegate.Combine(serializationEventHandler, serializationEventHandler2);
				}
				serializationEventHandler(context);
			}
		}

		// Token: 0x060052EA RID: 21226 RVA: 0x00123374 File Offset: 0x00121574
		[SecuritySafeCritical]
		internal void InvokeOnDeserializing(object obj, StreamingContext context)
		{
			if (this.m_OnDeserializingMethods != null)
			{
				object[] array = new object[] { context };
				SerializationEventHandler serializationEventHandler = null;
				foreach (MethodInfo methodInfo in this.m_OnDeserializingMethods)
				{
					SerializationEventHandler serializationEventHandler2 = (SerializationEventHandler)Delegate.CreateDelegateNoSecurityCheck((RuntimeType)typeof(SerializationEventHandler), obj, methodInfo);
					serializationEventHandler = (SerializationEventHandler)Delegate.Combine(serializationEventHandler, serializationEventHandler2);
				}
				serializationEventHandler(context);
			}
		}

		// Token: 0x060052EB RID: 21227 RVA: 0x0012340C File Offset: 0x0012160C
		[SecuritySafeCritical]
		internal void InvokeOnDeserialized(object obj, StreamingContext context)
		{
			if (this.m_OnDeserializedMethods != null)
			{
				object[] array = new object[] { context };
				SerializationEventHandler serializationEventHandler = null;
				foreach (MethodInfo methodInfo in this.m_OnDeserializedMethods)
				{
					SerializationEventHandler serializationEventHandler2 = (SerializationEventHandler)Delegate.CreateDelegateNoSecurityCheck((RuntimeType)typeof(SerializationEventHandler), obj, methodInfo);
					serializationEventHandler = (SerializationEventHandler)Delegate.Combine(serializationEventHandler, serializationEventHandler2);
				}
				serializationEventHandler(context);
			}
		}

		// Token: 0x060052EC RID: 21228 RVA: 0x001234A4 File Offset: 0x001216A4
		[SecurityCritical]
		internal SerializationEventHandler AddOnSerialized(object obj, SerializationEventHandler handler)
		{
			if (this.m_OnSerializedMethods != null)
			{
				foreach (MethodInfo methodInfo in this.m_OnSerializedMethods)
				{
					SerializationEventHandler serializationEventHandler = (SerializationEventHandler)Delegate.CreateDelegateNoSecurityCheck((RuntimeType)typeof(SerializationEventHandler), obj, methodInfo);
					handler = (SerializationEventHandler)Delegate.Combine(handler, serializationEventHandler);
				}
			}
			return handler;
		}

		// Token: 0x060052ED RID: 21229 RVA: 0x00123524 File Offset: 0x00121724
		[SecurityCritical]
		internal SerializationEventHandler AddOnDeserialized(object obj, SerializationEventHandler handler)
		{
			if (this.m_OnDeserializedMethods != null)
			{
				foreach (MethodInfo methodInfo in this.m_OnDeserializedMethods)
				{
					SerializationEventHandler serializationEventHandler = (SerializationEventHandler)Delegate.CreateDelegateNoSecurityCheck((RuntimeType)typeof(SerializationEventHandler), obj, methodInfo);
					handler = (SerializationEventHandler)Delegate.Combine(handler, serializationEventHandler);
				}
			}
			return handler;
		}

		// Token: 0x040024C8 RID: 9416
		private List<MethodInfo> m_OnSerializingMethods;

		// Token: 0x040024C9 RID: 9417
		private List<MethodInfo> m_OnSerializedMethods;

		// Token: 0x040024CA RID: 9418
		private List<MethodInfo> m_OnDeserializingMethods;

		// Token: 0x040024CB RID: 9419
		private List<MethodInfo> m_OnDeserializedMethods;
	}
}
