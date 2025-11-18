using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000879 RID: 2169
	internal class SmuggledMethodCallMessage : MessageSmuggler
	{
		// Token: 0x06005C38 RID: 23608 RVA: 0x001430B4 File Offset: 0x001412B4
		[SecurityCritical]
		internal static SmuggledMethodCallMessage SmuggleIfPossible(IMessage msg)
		{
			IMethodCallMessage methodCallMessage = msg as IMethodCallMessage;
			if (methodCallMessage == null)
			{
				return null;
			}
			return new SmuggledMethodCallMessage(methodCallMessage);
		}

		// Token: 0x06005C39 RID: 23609 RVA: 0x001430D3 File Offset: 0x001412D3
		private SmuggledMethodCallMessage()
		{
		}

		// Token: 0x06005C3A RID: 23610 RVA: 0x001430DC File Offset: 0x001412DC
		[SecurityCritical]
		private SmuggledMethodCallMessage(IMethodCallMessage mcm)
		{
			this._uri = mcm.Uri;
			this._methodName = mcm.MethodName;
			this._typeName = mcm.TypeName;
			ArrayList arrayList = null;
			IInternalMessage internalMessage = mcm as IInternalMessage;
			if (internalMessage == null || internalMessage.HasProperties())
			{
				this._propertyCount = MessageSmuggler.StoreUserPropertiesForMethodMessage(mcm, ref arrayList);
			}
			if (mcm.MethodBase.IsGenericMethod)
			{
				Type[] genericArguments = mcm.MethodBase.GetGenericArguments();
				if (genericArguments != null && genericArguments.Length != 0)
				{
					if (arrayList == null)
					{
						arrayList = new ArrayList();
					}
					this._instantiation = new MessageSmuggler.SerializedArg(arrayList.Count);
					arrayList.Add(genericArguments);
				}
			}
			if (RemotingServices.IsMethodOverloaded(mcm))
			{
				if (arrayList == null)
				{
					arrayList = new ArrayList();
				}
				this._methodSignature = new MessageSmuggler.SerializedArg(arrayList.Count);
				arrayList.Add(mcm.MethodSignature);
			}
			LogicalCallContext logicalCallContext = mcm.LogicalCallContext;
			if (logicalCallContext == null)
			{
				this._callContext = null;
			}
			else if (logicalCallContext.HasInfo)
			{
				if (arrayList == null)
				{
					arrayList = new ArrayList();
				}
				this._callContext = new MessageSmuggler.SerializedArg(arrayList.Count);
				arrayList.Add(logicalCallContext);
			}
			else
			{
				this._callContext = logicalCallContext.RemotingData.LogicalCallID;
			}
			this._args = MessageSmuggler.FixupArgs(mcm.Args, ref arrayList);
			if (arrayList != null)
			{
				MemoryStream memoryStream = CrossAppDomainSerializer.SerializeMessageParts(arrayList);
				this._serializedArgs = memoryStream.GetBuffer();
			}
		}

		// Token: 0x06005C3B RID: 23611 RVA: 0x00143224 File Offset: 0x00141424
		[SecurityCritical]
		internal ArrayList FixupForNewAppDomain()
		{
			ArrayList arrayList = null;
			if (this._serializedArgs != null)
			{
				arrayList = CrossAppDomainSerializer.DeserializeMessageParts(new MemoryStream(this._serializedArgs));
				this._serializedArgs = null;
			}
			return arrayList;
		}

		// Token: 0x17000FD9 RID: 4057
		// (get) Token: 0x06005C3C RID: 23612 RVA: 0x00143254 File Offset: 0x00141454
		internal string Uri
		{
			get
			{
				return this._uri;
			}
		}

		// Token: 0x17000FDA RID: 4058
		// (get) Token: 0x06005C3D RID: 23613 RVA: 0x0014325C File Offset: 0x0014145C
		internal string MethodName
		{
			get
			{
				return this._methodName;
			}
		}

		// Token: 0x17000FDB RID: 4059
		// (get) Token: 0x06005C3E RID: 23614 RVA: 0x00143264 File Offset: 0x00141464
		internal string TypeName
		{
			get
			{
				return this._typeName;
			}
		}

		// Token: 0x06005C3F RID: 23615 RVA: 0x0014326C File Offset: 0x0014146C
		internal Type[] GetInstantiation(ArrayList deserializedArgs)
		{
			if (this._instantiation != null)
			{
				return (Type[])deserializedArgs[this._instantiation.Index];
			}
			return null;
		}

		// Token: 0x06005C40 RID: 23616 RVA: 0x0014328E File Offset: 0x0014148E
		internal object[] GetMethodSignature(ArrayList deserializedArgs)
		{
			if (this._methodSignature != null)
			{
				return (object[])deserializedArgs[this._methodSignature.Index];
			}
			return null;
		}

		// Token: 0x06005C41 RID: 23617 RVA: 0x001432B0 File Offset: 0x001414B0
		[SecurityCritical]
		internal object[] GetArgs(ArrayList deserializedArgs)
		{
			return MessageSmuggler.UndoFixupArgs(this._args, deserializedArgs);
		}

		// Token: 0x06005C42 RID: 23618 RVA: 0x001432C0 File Offset: 0x001414C0
		[SecurityCritical]
		internal LogicalCallContext GetCallContext(ArrayList deserializedArgs)
		{
			if (this._callContext == null)
			{
				return null;
			}
			if (this._callContext is string)
			{
				return new LogicalCallContext
				{
					RemotingData = 
					{
						LogicalCallID = (string)this._callContext
					}
				};
			}
			return (LogicalCallContext)deserializedArgs[((MessageSmuggler.SerializedArg)this._callContext).Index];
		}

		// Token: 0x17000FDC RID: 4060
		// (get) Token: 0x06005C43 RID: 23619 RVA: 0x0014331D File Offset: 0x0014151D
		internal int MessagePropertyCount
		{
			get
			{
				return this._propertyCount;
			}
		}

		// Token: 0x06005C44 RID: 23620 RVA: 0x00143328 File Offset: 0x00141528
		internal void PopulateMessageProperties(IDictionary dict, ArrayList deserializedArgs)
		{
			for (int i = 0; i < this._propertyCount; i++)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)deserializedArgs[i];
				dict[dictionaryEntry.Key] = dictionaryEntry.Value;
			}
		}

		// Token: 0x040029A7 RID: 10663
		private string _uri;

		// Token: 0x040029A8 RID: 10664
		private string _methodName;

		// Token: 0x040029A9 RID: 10665
		private string _typeName;

		// Token: 0x040029AA RID: 10666
		private object[] _args;

		// Token: 0x040029AB RID: 10667
		private byte[] _serializedArgs;

		// Token: 0x040029AC RID: 10668
		private MessageSmuggler.SerializedArg _methodSignature;

		// Token: 0x040029AD RID: 10669
		private MessageSmuggler.SerializedArg _instantiation;

		// Token: 0x040029AE RID: 10670
		private object _callContext;

		// Token: 0x040029AF RID: 10671
		private int _propertyCount;
	}
}
