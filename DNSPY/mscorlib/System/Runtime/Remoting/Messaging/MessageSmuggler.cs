using System;
using System.Collections;
using System.Runtime.Remoting.Proxies;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000877 RID: 2167
	internal class MessageSmuggler
	{
		// Token: 0x06005C2F RID: 23599 RVA: 0x00142DA1 File Offset: 0x00140FA1
		private static bool CanSmuggleObjectDirectly(object obj)
		{
			return obj is string || obj.GetType() == typeof(void) || obj.GetType().IsPrimitive;
		}

		// Token: 0x06005C30 RID: 23600 RVA: 0x00142DD4 File Offset: 0x00140FD4
		[SecurityCritical]
		protected static object[] FixupArgs(object[] args, ref ArrayList argsToSerialize)
		{
			object[] array = new object[args.Length];
			int num = args.Length;
			for (int i = 0; i < num; i++)
			{
				array[i] = MessageSmuggler.FixupArg(args[i], ref argsToSerialize);
			}
			return array;
		}

		// Token: 0x06005C31 RID: 23601 RVA: 0x00142E08 File Offset: 0x00141008
		[SecurityCritical]
		protected static object FixupArg(object arg, ref ArrayList argsToSerialize)
		{
			if (arg == null)
			{
				return null;
			}
			MarshalByRefObject marshalByRefObject = arg as MarshalByRefObject;
			int num;
			if (marshalByRefObject != null)
			{
				if (!RemotingServices.IsTransparentProxy(marshalByRefObject) || RemotingServices.GetRealProxy(marshalByRefObject) is RemotingProxy)
				{
					ObjRef objRef = RemotingServices.MarshalInternal(marshalByRefObject, null, null);
					if (objRef.CanSmuggle())
					{
						if (!RemotingServices.IsTransparentProxy(marshalByRefObject))
						{
							ServerIdentity serverIdentity = (ServerIdentity)MarshalByRefObject.GetIdentity(marshalByRefObject);
							serverIdentity.SetHandle();
							objRef.SetServerIdentity(serverIdentity.GetHandle());
							objRef.SetDomainID(AppDomain.CurrentDomain.GetId());
						}
						ObjRef objRef2 = objRef.CreateSmuggleableCopy();
						objRef2.SetMarshaledObject();
						return new SmuggledObjRef(objRef2);
					}
				}
				if (argsToSerialize == null)
				{
					argsToSerialize = new ArrayList();
				}
				num = argsToSerialize.Count;
				argsToSerialize.Add(arg);
				return new MessageSmuggler.SerializedArg(num);
			}
			if (MessageSmuggler.CanSmuggleObjectDirectly(arg))
			{
				return arg;
			}
			Array array = arg as Array;
			if (array != null)
			{
				Type elementType = array.GetType().GetElementType();
				if (elementType.IsPrimitive || elementType == typeof(string))
				{
					return array.Clone();
				}
			}
			if (argsToSerialize == null)
			{
				argsToSerialize = new ArrayList();
			}
			num = argsToSerialize.Count;
			argsToSerialize.Add(arg);
			return new MessageSmuggler.SerializedArg(num);
		}

		// Token: 0x06005C32 RID: 23602 RVA: 0x00142F28 File Offset: 0x00141128
		[SecurityCritical]
		protected static object[] UndoFixupArgs(object[] args, ArrayList deserializedArgs)
		{
			object[] array = new object[args.Length];
			int num = args.Length;
			for (int i = 0; i < num; i++)
			{
				array[i] = MessageSmuggler.UndoFixupArg(args[i], deserializedArgs);
			}
			return array;
		}

		// Token: 0x06005C33 RID: 23603 RVA: 0x00142F5C File Offset: 0x0014115C
		[SecurityCritical]
		protected static object UndoFixupArg(object arg, ArrayList deserializedArgs)
		{
			SmuggledObjRef smuggledObjRef = arg as SmuggledObjRef;
			if (smuggledObjRef != null)
			{
				return smuggledObjRef.ObjRef.GetRealObjectHelper();
			}
			MessageSmuggler.SerializedArg serializedArg = arg as MessageSmuggler.SerializedArg;
			if (serializedArg != null)
			{
				return deserializedArgs[serializedArg.Index];
			}
			return arg;
		}

		// Token: 0x06005C34 RID: 23604 RVA: 0x00142F98 File Offset: 0x00141198
		[SecurityCritical]
		protected static int StoreUserPropertiesForMethodMessage(IMethodMessage msg, ref ArrayList argsToSerialize)
		{
			IDictionary properties = msg.Properties;
			MessageDictionary messageDictionary = properties as MessageDictionary;
			if (messageDictionary == null)
			{
				int num = 0;
				foreach (object obj in properties)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					if (argsToSerialize == null)
					{
						argsToSerialize = new ArrayList();
					}
					argsToSerialize.Add(dictionaryEntry);
					num++;
				}
				return num;
			}
			if (messageDictionary.HasUserData())
			{
				int num2 = 0;
				foreach (object obj2 in messageDictionary.InternalDictionary)
				{
					DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
					if (argsToSerialize == null)
					{
						argsToSerialize = new ArrayList();
					}
					argsToSerialize.Add(dictionaryEntry2);
					num2++;
				}
				return num2;
			}
			return 0;
		}

		// Token: 0x06005C35 RID: 23605 RVA: 0x00143094 File Offset: 0x00141294
		public MessageSmuggler()
		{
		}

		// Token: 0x02000C7D RID: 3197
		protected class SerializedArg
		{
			// Token: 0x060070C6 RID: 28870 RVA: 0x001849FD File Offset: 0x00182BFD
			public SerializedArg(int index)
			{
				this._index = index;
			}

			// Token: 0x17001355 RID: 4949
			// (get) Token: 0x060070C7 RID: 28871 RVA: 0x00184A0C File Offset: 0x00182C0C
			public int Index
			{
				get
				{
					return this._index;
				}
			}

			// Token: 0x04003814 RID: 14356
			private int _index;
		}
	}
}
