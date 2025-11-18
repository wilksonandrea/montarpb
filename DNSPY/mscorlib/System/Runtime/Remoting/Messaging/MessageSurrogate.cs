using System;
using System.Collections;
using System.Runtime.Remoting.Activation;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000881 RID: 2177
	internal class MessageSurrogate : ISerializationSurrogate
	{
		// Token: 0x06005C6B RID: 23659 RVA: 0x00143C14 File Offset: 0x00141E14
		[SecuritySafeCritical]
		static MessageSurrogate()
		{
		}

		// Token: 0x06005C6C RID: 23660 RVA: 0x00143C7B File Offset: 0x00141E7B
		[SecurityCritical]
		internal MessageSurrogate(RemotingSurrogateSelector ss)
		{
			this._ss = ss;
		}

		// Token: 0x06005C6D RID: 23661 RVA: 0x00143C8C File Offset: 0x00141E8C
		[SecurityCritical]
		public virtual void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			bool flag = false;
			bool flag2 = false;
			IMethodMessage methodMessage = obj as IMethodMessage;
			if (methodMessage != null)
			{
				IDictionaryEnumerator enumerator = methodMessage.Properties.GetEnumerator();
				if (methodMessage is IMethodCallMessage)
				{
					if (obj is IConstructionCallMessage)
					{
						flag2 = true;
					}
					info.SetType(flag2 ? MessageSurrogate._constructionCallType : MessageSurrogate._methodCallType);
				}
				else
				{
					IMethodReturnMessage methodReturnMessage = methodMessage as IMethodReturnMessage;
					if (methodReturnMessage == null)
					{
						throw new RemotingException(Environment.GetResourceString("Remoting_InvalidMsg"));
					}
					flag = true;
					info.SetType((obj is IConstructionReturnMessage) ? MessageSurrogate._constructionResponseType : MessageSurrogate._methodResponseType);
					if (((IMethodReturnMessage)methodMessage).Exception != null)
					{
						info.AddValue("__fault", ((IMethodReturnMessage)methodMessage).Exception, MessageSurrogate._exceptionType);
					}
				}
				while (enumerator.MoveNext())
				{
					if (obj != this._ss.GetRootObject() || this._ss.Filter == null || !this._ss.Filter((string)enumerator.Key, enumerator.Value))
					{
						if (enumerator.Value != null)
						{
							string text = enumerator.Key.ToString();
							if (text.Equals("__CallContext"))
							{
								LogicalCallContext logicalCallContext = (LogicalCallContext)enumerator.Value;
								if (logicalCallContext.HasInfo)
								{
									info.AddValue(text, logicalCallContext);
								}
								else
								{
									info.AddValue(text, logicalCallContext.RemotingData.LogicalCallID);
								}
							}
							else if (text.Equals("__MethodSignature"))
							{
								if (flag2 || RemotingServices.IsMethodOverloaded(methodMessage))
								{
									info.AddValue(text, enumerator.Value);
								}
							}
							else
							{
								flag = flag;
								info.AddValue(text, enumerator.Value);
							}
						}
						else
						{
							info.AddValue(enumerator.Key.ToString(), enumerator.Value, MessageSurrogate._objectType);
						}
					}
				}
				return;
			}
			throw new RemotingException(Environment.GetResourceString("Remoting_InvalidMsg"));
		}

		// Token: 0x06005C6E RID: 23662 RVA: 0x00143E77 File Offset: 0x00142077
		[SecurityCritical]
		public virtual object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_PopulateData"));
		}

		// Token: 0x040029C4 RID: 10692
		private static Type _constructionCallType = typeof(ConstructionCall);

		// Token: 0x040029C5 RID: 10693
		private static Type _methodCallType = typeof(MethodCall);

		// Token: 0x040029C6 RID: 10694
		private static Type _constructionResponseType = typeof(ConstructionResponse);

		// Token: 0x040029C7 RID: 10695
		private static Type _methodResponseType = typeof(MethodResponse);

		// Token: 0x040029C8 RID: 10696
		private static Type _exceptionType = typeof(Exception);

		// Token: 0x040029C9 RID: 10697
		private static Type _objectType = typeof(object);

		// Token: 0x040029CA RID: 10698
		[SecurityCritical]
		private RemotingSurrogateSelector _ss;
	}
}
