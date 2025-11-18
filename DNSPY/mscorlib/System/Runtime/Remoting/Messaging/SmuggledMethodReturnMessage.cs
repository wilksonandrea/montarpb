using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200087A RID: 2170
	internal class SmuggledMethodReturnMessage : MessageSmuggler
	{
		// Token: 0x06005C45 RID: 23621 RVA: 0x00143368 File Offset: 0x00141568
		[SecurityCritical]
		internal static SmuggledMethodReturnMessage SmuggleIfPossible(IMessage msg)
		{
			IMethodReturnMessage methodReturnMessage = msg as IMethodReturnMessage;
			if (methodReturnMessage == null)
			{
				return null;
			}
			return new SmuggledMethodReturnMessage(methodReturnMessage);
		}

		// Token: 0x06005C46 RID: 23622 RVA: 0x00143387 File Offset: 0x00141587
		private SmuggledMethodReturnMessage()
		{
		}

		// Token: 0x06005C47 RID: 23623 RVA: 0x00143390 File Offset: 0x00141590
		[SecurityCritical]
		private SmuggledMethodReturnMessage(IMethodReturnMessage mrm)
		{
			ArrayList arrayList = null;
			ReturnMessage returnMessage = mrm as ReturnMessage;
			if (returnMessage == null || returnMessage.HasProperties())
			{
				this._propertyCount = MessageSmuggler.StoreUserPropertiesForMethodMessage(mrm, ref arrayList);
			}
			Exception exception = mrm.Exception;
			if (exception != null)
			{
				if (arrayList == null)
				{
					arrayList = new ArrayList();
				}
				this._exception = new MessageSmuggler.SerializedArg(arrayList.Count);
				arrayList.Add(exception);
			}
			LogicalCallContext logicalCallContext = mrm.LogicalCallContext;
			if (logicalCallContext == null)
			{
				this._callContext = null;
			}
			else if (logicalCallContext.HasInfo)
			{
				if (logicalCallContext.Principal != null)
				{
					logicalCallContext.Principal = null;
				}
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
			this._returnValue = MessageSmuggler.FixupArg(mrm.ReturnValue, ref arrayList);
			this._args = MessageSmuggler.FixupArgs(mrm.Args, ref arrayList);
			if (arrayList != null)
			{
				MemoryStream memoryStream = CrossAppDomainSerializer.SerializeMessageParts(arrayList);
				this._serializedArgs = memoryStream.GetBuffer();
			}
		}

		// Token: 0x06005C48 RID: 23624 RVA: 0x00143490 File Offset: 0x00141690
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

		// Token: 0x06005C49 RID: 23625 RVA: 0x001434C0 File Offset: 0x001416C0
		[SecurityCritical]
		internal object GetReturnValue(ArrayList deserializedArgs)
		{
			return MessageSmuggler.UndoFixupArg(this._returnValue, deserializedArgs);
		}

		// Token: 0x06005C4A RID: 23626 RVA: 0x001434D0 File Offset: 0x001416D0
		[SecurityCritical]
		internal object[] GetArgs(ArrayList deserializedArgs)
		{
			return MessageSmuggler.UndoFixupArgs(this._args, deserializedArgs);
		}

		// Token: 0x06005C4B RID: 23627 RVA: 0x001434EB File Offset: 0x001416EB
		internal Exception GetException(ArrayList deserializedArgs)
		{
			if (this._exception != null)
			{
				return (Exception)deserializedArgs[this._exception.Index];
			}
			return null;
		}

		// Token: 0x06005C4C RID: 23628 RVA: 0x00143510 File Offset: 0x00141710
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

		// Token: 0x17000FDD RID: 4061
		// (get) Token: 0x06005C4D RID: 23629 RVA: 0x0014356D File Offset: 0x0014176D
		internal int MessagePropertyCount
		{
			get
			{
				return this._propertyCount;
			}
		}

		// Token: 0x06005C4E RID: 23630 RVA: 0x00143578 File Offset: 0x00141778
		internal void PopulateMessageProperties(IDictionary dict, ArrayList deserializedArgs)
		{
			for (int i = 0; i < this._propertyCount; i++)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)deserializedArgs[i];
				dict[dictionaryEntry.Key] = dictionaryEntry.Value;
			}
		}

		// Token: 0x040029B0 RID: 10672
		private object[] _args;

		// Token: 0x040029B1 RID: 10673
		private object _returnValue;

		// Token: 0x040029B2 RID: 10674
		private byte[] _serializedArgs;

		// Token: 0x040029B3 RID: 10675
		private MessageSmuggler.SerializedArg _exception;

		// Token: 0x040029B4 RID: 10676
		private object _callContext;

		// Token: 0x040029B5 RID: 10677
		private int _propertyCount;
	}
}
