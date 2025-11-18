using System;
using System.Reflection;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Security;
using System.Text;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000880 RID: 2176
	internal class SoapMessageSurrogate : ISerializationSurrogate
	{
		// Token: 0x06005C64 RID: 23652 RVA: 0x001437E2 File Offset: 0x001419E2
		[SecurityCritical]
		internal SoapMessageSurrogate(RemotingSurrogateSelector ss)
		{
			this._ss = ss;
		}

		// Token: 0x06005C65 RID: 23653 RVA: 0x001437FC File Offset: 0x001419FC
		internal void SetRootObject(object obj)
		{
			this._rootObj = obj;
		}

		// Token: 0x06005C66 RID: 23654 RVA: 0x00143808 File Offset: 0x00141A08
		[SecurityCritical]
		internal virtual string[] GetInArgNames(IMethodCallMessage m, int c)
		{
			string[] array = new string[c];
			for (int i = 0; i < c; i++)
			{
				string text = m.GetInArgName(i);
				if (text == null)
				{
					text = "__param" + i.ToString();
				}
				array[i] = text;
			}
			return array;
		}

		// Token: 0x06005C67 RID: 23655 RVA: 0x0014384C File Offset: 0x00141A4C
		[SecurityCritical]
		internal virtual string[] GetNames(IMethodCallMessage m, int c)
		{
			string[] array = new string[c];
			for (int i = 0; i < c; i++)
			{
				string text = m.GetArgName(i);
				if (text == null)
				{
					text = "__param" + i.ToString();
				}
				array[i] = text;
			}
			return array;
		}

		// Token: 0x06005C68 RID: 23656 RVA: 0x00143890 File Offset: 0x00141A90
		[SecurityCritical]
		public virtual void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			if (obj != null && obj != this._rootObj)
			{
				new MessageSurrogate(this._ss).GetObjectData(obj, info, context);
				return;
			}
			IMethodReturnMessage methodReturnMessage = obj as IMethodReturnMessage;
			if (methodReturnMessage != null)
			{
				if (methodReturnMessage.Exception != null)
				{
					object data = CallContext.GetData("__ClientIsClr");
					bool flag = data == null || (bool)data;
					info.FullTypeName = "FormatterWrapper";
					info.AssemblyName = this.DefaultFakeRecordAssemblyName;
					Exception ex = methodReturnMessage.Exception;
					StringBuilder stringBuilder = new StringBuilder();
					bool flag2 = false;
					while (ex != null)
					{
						if (ex.Message.StartsWith("MustUnderstand", StringComparison.Ordinal))
						{
							flag2 = true;
						}
						stringBuilder.Append(" **** ");
						stringBuilder.Append(ex.GetType().FullName);
						stringBuilder.Append(" - ");
						stringBuilder.Append(ex.Message);
						ex = ex.InnerException;
					}
					ServerFault serverFault;
					if (flag)
					{
						serverFault = new ServerFault(methodReturnMessage.Exception);
					}
					else
					{
						serverFault = new ServerFault(methodReturnMessage.Exception.GetType().AssemblyQualifiedName, stringBuilder.ToString(), methodReturnMessage.Exception.StackTrace);
					}
					string text = "Server";
					if (flag2)
					{
						text = "MustUnderstand";
					}
					SoapFault soapFault = new SoapFault(text, stringBuilder.ToString(), null, serverFault);
					info.AddValue("__WrappedObject", soapFault, SoapMessageSurrogate._soapFaultType);
					return;
				}
				MethodBase methodBase = methodReturnMessage.MethodBase;
				SoapMethodAttribute soapMethodAttribute = (SoapMethodAttribute)InternalRemotingServices.GetCachedSoapAttribute(methodBase);
				string responseXmlElementName = soapMethodAttribute.ResponseXmlElementName;
				string responseXmlNamespace = soapMethodAttribute.ResponseXmlNamespace;
				string returnXmlElementName = soapMethodAttribute.ReturnXmlElementName;
				ArgMapper argMapper = new ArgMapper(methodReturnMessage, true);
				object[] args = argMapper.Args;
				info.FullTypeName = responseXmlElementName;
				info.AssemblyName = responseXmlNamespace;
				Type returnType = ((MethodInfo)methodBase).ReturnType;
				if (!(returnType == null) && !(returnType == SoapMessageSurrogate._voidType))
				{
					info.AddValue(returnXmlElementName, methodReturnMessage.ReturnValue, returnType);
				}
				if (args != null)
				{
					Type[] argTypes = argMapper.ArgTypes;
					for (int i = 0; i < args.Length; i++)
					{
						string text2 = argMapper.GetArgName(i);
						if (text2 == null || text2.Length == 0)
						{
							text2 = "__param" + i.ToString();
						}
						info.AddValue(text2, args[i], argTypes[i].IsByRef ? argTypes[i].GetElementType() : argTypes[i]);
					}
					return;
				}
			}
			else
			{
				IMethodCallMessage methodCallMessage = (IMethodCallMessage)obj;
				MethodBase methodBase2 = methodCallMessage.MethodBase;
				string xmlNamespaceForMethodCall = SoapServices.GetXmlNamespaceForMethodCall(methodBase2);
				object[] inArgs = methodCallMessage.InArgs;
				string[] inArgNames = this.GetInArgNames(methodCallMessage, inArgs.Length);
				Type[] array = (Type[])methodCallMessage.MethodSignature;
				info.FullTypeName = methodCallMessage.MethodName;
				info.AssemblyName = xmlNamespaceForMethodCall;
				RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(methodBase2);
				int[] marshalRequestArgMap = reflectionCachedData.MarshalRequestArgMap;
				for (int j = 0; j < inArgs.Length; j++)
				{
					string text3;
					if (inArgNames[j] == null || inArgNames[j].Length == 0)
					{
						text3 = "__param" + j.ToString();
					}
					else
					{
						text3 = inArgNames[j];
					}
					int num = marshalRequestArgMap[j];
					Type type;
					if (array[num].IsByRef)
					{
						type = array[num].GetElementType();
					}
					else
					{
						type = array[num];
					}
					info.AddValue(text3, inArgs[j], type);
				}
			}
		}

		// Token: 0x06005C69 RID: 23657 RVA: 0x00143BE3 File Offset: 0x00141DE3
		[SecurityCritical]
		public virtual object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_PopulateData"));
		}

		// Token: 0x06005C6A RID: 23658 RVA: 0x00143BF4 File Offset: 0x00141DF4
		// Note: this type is marked as 'beforefieldinit'.
		static SoapMessageSurrogate()
		{
		}

		// Token: 0x040029BF RID: 10687
		private static Type _voidType = typeof(void);

		// Token: 0x040029C0 RID: 10688
		private static Type _soapFaultType = typeof(SoapFault);

		// Token: 0x040029C1 RID: 10689
		private string DefaultFakeRecordAssemblyName = "http://schemas.microsoft.com/urt/SystemRemotingSoapTopRecord";

		// Token: 0x040029C2 RID: 10690
		private object _rootObj;

		// Token: 0x040029C3 RID: 10691
		[SecurityCritical]
		private RemotingSurrogateSelector _ss;
	}
}
