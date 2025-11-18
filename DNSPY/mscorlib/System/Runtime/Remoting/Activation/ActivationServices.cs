using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x02000893 RID: 2195
	internal static class ActivationServices
	{
		// Token: 0x06005CEC RID: 23788 RVA: 0x001457DC File Offset: 0x001439DC
		[SecuritySafeCritical]
		static ActivationServices()
		{
		}

		// Token: 0x06005CED RID: 23789 RVA: 0x00145828 File Offset: 0x00143A28
		[SecurityCritical]
		private static void Startup()
		{
			DomainSpecificRemotingData remotingData = Thread.GetDomain().RemotingData;
			if (!remotingData.ActivationInitialized || remotingData.InitializingActivation)
			{
				object configLock = remotingData.ConfigLock;
				bool flag = false;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					Monitor.Enter(configLock, ref flag);
					remotingData.InitializingActivation = true;
					if (!remotingData.ActivationInitialized)
					{
						remotingData.LocalActivator = new LocalActivator();
						remotingData.ActivationListener = new ActivationListener();
						remotingData.ActivationInitialized = true;
					}
					remotingData.InitializingActivation = false;
				}
				finally
				{
					if (flag)
					{
						Monitor.Exit(configLock);
					}
				}
			}
		}

		// Token: 0x06005CEE RID: 23790 RVA: 0x001458B8 File Offset: 0x00143AB8
		[SecurityCritical]
		private static void InitActivationServices()
		{
			if (ActivationServices.activator == null)
			{
				ActivationServices.activator = ActivationServices.GetActivator();
				if (ActivationServices.activator == null)
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadInternalState_ActivationFailure"), Array.Empty<object>()));
				}
			}
		}

		// Token: 0x06005CEF RID: 23791 RVA: 0x001458F8 File Offset: 0x00143AF8
		[SecurityCritical]
		private static MarshalByRefObject IsCurrentContextOK(RuntimeType serverType, object[] props, bool bNewObj)
		{
			ActivationServices.InitActivationServices();
			ProxyAttribute proxyAttribute = ActivationServices.GetProxyAttribute(serverType);
			MarshalByRefObject marshalByRefObject;
			if (proxyAttribute == ActivationServices.DefaultProxyAttribute)
			{
				marshalByRefObject = proxyAttribute.CreateInstanceInternal(serverType);
			}
			else
			{
				marshalByRefObject = proxyAttribute.CreateInstance(serverType);
				if (marshalByRefObject != null && !RemotingServices.IsTransparentProxy(marshalByRefObject) && !serverType.IsAssignableFrom(marshalByRefObject.GetType()))
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Activation_BadObject"), serverType));
				}
			}
			return marshalByRefObject;
		}

		// Token: 0x06005CF0 RID: 23792 RVA: 0x00145964 File Offset: 0x00143B64
		[SecurityCritical]
		private static MarshalByRefObject CreateObjectForCom(RuntimeType serverType, object[] props, bool bNewObj)
		{
			if (ActivationServices.PeekActivationAttributes(serverType) != null)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_ActivForCom"));
			}
			ActivationServices.InitActivationServices();
			ProxyAttribute proxyAttribute = ActivationServices.GetProxyAttribute(serverType);
			MarshalByRefObject marshalByRefObject;
			if (proxyAttribute is ICustomFactory)
			{
				marshalByRefObject = ((ICustomFactory)proxyAttribute).CreateInstance(serverType);
			}
			else
			{
				marshalByRefObject = (MarshalByRefObject)Activator.CreateInstance(serverType, true);
			}
			return marshalByRefObject;
		}

		// Token: 0x06005CF1 RID: 23793 RVA: 0x001459BC File Offset: 0x00143BBC
		[SecurityCritical]
		private static bool IsCurrentContextOK(RuntimeType serverType, object[] props, ref ConstructorCallMessage ctorCallMsg)
		{
			object[] array = ActivationServices.PeekActivationAttributes(serverType);
			if (array != null)
			{
				ActivationServices.PopActivationAttributes(serverType);
			}
			object[] array2 = new object[] { ActivationServices.GetGlobalAttribute() };
			object[] contextAttributesForType = ActivationServices.GetContextAttributesForType(serverType);
			object[] array3 = contextAttributesForType;
			Context currentContext = Thread.CurrentContext;
			ctorCallMsg = new ConstructorCallMessage(array, array2, array3, serverType);
			ctorCallMsg.Activator = new ConstructionLevelActivator();
			bool flag = ActivationServices.QueryAttributesIfContextOK(currentContext, ctorCallMsg, array2);
			if (flag)
			{
				flag = ActivationServices.QueryAttributesIfContextOK(currentContext, ctorCallMsg, array);
				if (flag)
				{
					flag = ActivationServices.QueryAttributesIfContextOK(currentContext, ctorCallMsg, array3);
				}
			}
			return flag;
		}

		// Token: 0x06005CF2 RID: 23794 RVA: 0x00145A3C File Offset: 0x00143C3C
		[SecurityCritical]
		private static void CheckForInfrastructurePermission(RuntimeAssembly asm)
		{
			if (asm != ActivationServices.s_MscorlibAssembly)
			{
				SecurityPermission securityPermission = new SecurityPermission(SecurityPermissionFlag.Infrastructure);
				CodeAccessSecurityEngine.CheckAssembly(asm, securityPermission);
			}
		}

		// Token: 0x06005CF3 RID: 23795 RVA: 0x00145A68 File Offset: 0x00143C68
		[SecurityCritical]
		private static bool QueryAttributesIfContextOK(Context ctx, IConstructionCallMessage ctorMsg, object[] attributes)
		{
			bool flag = true;
			if (attributes != null)
			{
				for (int i = 0; i < attributes.Length; i++)
				{
					IContextAttribute contextAttribute = attributes[i] as IContextAttribute;
					if (contextAttribute == null)
					{
						throw new RemotingException(Environment.GetResourceString("Remoting_Activation_BadAttribute"));
					}
					RuntimeAssembly runtimeAssembly = (RuntimeAssembly)contextAttribute.GetType().Assembly;
					ActivationServices.CheckForInfrastructurePermission(runtimeAssembly);
					flag = contextAttribute.IsContextOK(ctx, ctorMsg);
					if (!flag)
					{
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x06005CF4 RID: 23796 RVA: 0x00145ACC File Offset: 0x00143CCC
		[SecurityCritical]
		internal static void GetPropertiesFromAttributes(IConstructionCallMessage ctorMsg, object[] attributes)
		{
			if (attributes != null)
			{
				for (int i = 0; i < attributes.Length; i++)
				{
					IContextAttribute contextAttribute = attributes[i] as IContextAttribute;
					if (contextAttribute == null)
					{
						throw new RemotingException(Environment.GetResourceString("Remoting_Activation_BadAttribute"));
					}
					RuntimeAssembly runtimeAssembly = (RuntimeAssembly)contextAttribute.GetType().Assembly;
					ActivationServices.CheckForInfrastructurePermission(runtimeAssembly);
					contextAttribute.GetPropertiesForNewContext(ctorMsg);
				}
			}
		}

		// Token: 0x17000FFB RID: 4091
		// (get) Token: 0x06005CF5 RID: 23797 RVA: 0x00145B26 File Offset: 0x00143D26
		internal static ProxyAttribute DefaultProxyAttribute
		{
			[SecurityCritical]
			get
			{
				return ActivationServices._proxyAttribute;
			}
		}

		// Token: 0x06005CF6 RID: 23798 RVA: 0x00145B30 File Offset: 0x00143D30
		[SecurityCritical]
		internal static ProxyAttribute GetProxyAttribute(Type serverType)
		{
			if (!serverType.HasProxyAttribute)
			{
				return ActivationServices.DefaultProxyAttribute;
			}
			ProxyAttribute proxyAttribute = ActivationServices._proxyTable[serverType] as ProxyAttribute;
			if (proxyAttribute == null)
			{
				object[] customAttributes = Attribute.GetCustomAttributes(serverType, ActivationServices.proxyAttributeType, true);
				object[] array = customAttributes;
				if (array != null && array.Length != 0)
				{
					if (!serverType.IsContextful)
					{
						throw new RemotingException(Environment.GetResourceString("Remoting_Activation_MBR_ProxyAttribute"));
					}
					proxyAttribute = array[0] as ProxyAttribute;
				}
				if (!ActivationServices._proxyTable.Contains(serverType))
				{
					Hashtable proxyTable = ActivationServices._proxyTable;
					lock (proxyTable)
					{
						if (!ActivationServices._proxyTable.Contains(serverType))
						{
							ActivationServices._proxyTable.Add(serverType, proxyAttribute);
						}
					}
				}
			}
			return proxyAttribute;
		}

		// Token: 0x06005CF7 RID: 23799 RVA: 0x00145BEC File Offset: 0x00143DEC
		[SecurityCritical]
		internal static MarshalByRefObject CreateInstance(RuntimeType serverType)
		{
			ConstructorCallMessage constructorCallMessage = null;
			bool flag = ActivationServices.IsCurrentContextOK(serverType, null, ref constructorCallMessage);
			MarshalByRefObject marshalByRefObject;
			if (flag && !serverType.IsContextful)
			{
				marshalByRefObject = RemotingServices.AllocateUninitializedObject(serverType);
			}
			else
			{
				marshalByRefObject = (MarshalByRefObject)ActivationServices.ConnectIfNecessary(constructorCallMessage);
				RemotingProxy remotingProxy;
				if (marshalByRefObject == null)
				{
					remotingProxy = new RemotingProxy(serverType);
					marshalByRefObject = (MarshalByRefObject)remotingProxy.GetTransparentProxy();
				}
				else
				{
					remotingProxy = (RemotingProxy)RemotingServices.GetRealProxy(marshalByRefObject);
				}
				remotingProxy.ConstructorMessage = constructorCallMessage;
				if (!flag)
				{
					ContextLevelActivator contextLevelActivator = new ContextLevelActivator();
					contextLevelActivator.NextActivator = constructorCallMessage.Activator;
					constructorCallMessage.Activator = contextLevelActivator;
				}
				else
				{
					constructorCallMessage.ActivateInContext = true;
				}
			}
			return marshalByRefObject;
		}

		// Token: 0x06005CF8 RID: 23800 RVA: 0x00145C7C File Offset: 0x00143E7C
		[SecurityCritical]
		internal static IConstructionReturnMessage Activate(RemotingProxy remProxy, IConstructionCallMessage ctorMsg)
		{
			IConstructionReturnMessage constructionReturnMessage;
			if (((ConstructorCallMessage)ctorMsg).ActivateInContext)
			{
				constructionReturnMessage = ctorMsg.Activator.Activate(ctorMsg);
				if (constructionReturnMessage.Exception != null)
				{
					throw constructionReturnMessage.Exception;
				}
			}
			else
			{
				ActivationServices.GetPropertiesFromAttributes(ctorMsg, ctorMsg.CallSiteActivationAttributes);
				ActivationServices.GetPropertiesFromAttributes(ctorMsg, ((ConstructorCallMessage)ctorMsg).GetWOMAttributes());
				ActivationServices.GetPropertiesFromAttributes(ctorMsg, ((ConstructorCallMessage)ctorMsg).GetTypeAttributes());
				IMessageSink clientContextChain = Thread.CurrentContext.GetClientContextChain();
				IMethodReturnMessage methodReturnMessage = (IMethodReturnMessage)clientContextChain.SyncProcessMessage(ctorMsg);
				constructionReturnMessage = methodReturnMessage as IConstructionReturnMessage;
				if (methodReturnMessage == null)
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_Activation_Failed"));
				}
				if (methodReturnMessage.Exception != null)
				{
					throw methodReturnMessage.Exception;
				}
			}
			return constructionReturnMessage;
		}

		// Token: 0x06005CF9 RID: 23801 RVA: 0x00145D24 File Offset: 0x00143F24
		[SecurityCritical]
		internal static IConstructionReturnMessage DoCrossContextActivation(IConstructionCallMessage reqMsg)
		{
			bool isContextful = reqMsg.ActivationType.IsContextful;
			Context context = null;
			if (isContextful)
			{
				context = new Context();
				ArrayList arrayList = (ArrayList)reqMsg.ContextProperties;
				for (int i = 0; i < arrayList.Count; i++)
				{
					IContextProperty contextProperty = arrayList[i] as IContextProperty;
					if (contextProperty == null)
					{
						throw new RemotingException(Environment.GetResourceString("Remoting_Activation_BadAttribute"));
					}
					RuntimeAssembly runtimeAssembly = (RuntimeAssembly)contextProperty.GetType().Assembly;
					ActivationServices.CheckForInfrastructurePermission(runtimeAssembly);
					if (context.GetProperty(contextProperty.Name) == null)
					{
						context.SetProperty(contextProperty);
					}
				}
				context.Freeze();
				for (int j = 0; j < arrayList.Count; j++)
				{
					if (!((IContextProperty)arrayList[j]).IsNewContextOK(context))
					{
						throw new RemotingException(Environment.GetResourceString("Remoting_Activation_PropertyUnhappy"));
					}
				}
			}
			InternalCrossContextDelegate internalCrossContextDelegate = new InternalCrossContextDelegate(ActivationServices.DoCrossContextActivationCallback);
			object[] array = new object[] { reqMsg };
			IConstructionReturnMessage constructionReturnMessage;
			if (isContextful)
			{
				constructionReturnMessage = Thread.CurrentThread.InternalCrossContextCallback(context, internalCrossContextDelegate, array) as IConstructionReturnMessage;
			}
			else
			{
				constructionReturnMessage = internalCrossContextDelegate(array) as IConstructionReturnMessage;
			}
			return constructionReturnMessage;
		}

		// Token: 0x06005CFA RID: 23802 RVA: 0x00145E4C File Offset: 0x0014404C
		[SecurityCritical]
		internal static object DoCrossContextActivationCallback(object[] args)
		{
			IConstructionCallMessage constructionCallMessage = (IConstructionCallMessage)args[0];
			IMethodReturnMessage methodReturnMessage = (IMethodReturnMessage)Thread.CurrentContext.GetServerContextChain().SyncProcessMessage(constructionCallMessage);
			IConstructionReturnMessage constructionReturnMessage = methodReturnMessage as IConstructionReturnMessage;
			if (constructionReturnMessage == null)
			{
				Exception ex;
				if (methodReturnMessage != null)
				{
					ex = methodReturnMessage.Exception;
				}
				else
				{
					ex = new RemotingException(Environment.GetResourceString("Remoting_Activation_Failed"));
				}
				constructionReturnMessage = new ConstructorReturnMessage(ex, null);
				((ConstructorReturnMessage)constructionReturnMessage).SetLogicalCallContext((LogicalCallContext)constructionCallMessage.Properties[Message.CallContextKey]);
			}
			return constructionReturnMessage;
		}

		// Token: 0x06005CFB RID: 23803 RVA: 0x00145ECC File Offset: 0x001440CC
		[SecurityCritical]
		internal static IConstructionReturnMessage DoServerContextActivation(IConstructionCallMessage reqMsg)
		{
			Exception ex = null;
			Type activationType = reqMsg.ActivationType;
			object obj = ActivationServices.ActivateWithMessage(activationType, reqMsg, null, out ex);
			return ActivationServices.SetupConstructionReply(obj, reqMsg, ex);
		}

		// Token: 0x06005CFC RID: 23804 RVA: 0x00145EF8 File Offset: 0x001440F8
		[SecurityCritical]
		internal static IConstructionReturnMessage SetupConstructionReply(object serverObj, IConstructionCallMessage ctorMsg, Exception e)
		{
			IConstructionReturnMessage constructionReturnMessage;
			if (e == null)
			{
				constructionReturnMessage = new ConstructorReturnMessage((MarshalByRefObject)serverObj, null, 0, (LogicalCallContext)ctorMsg.Properties[Message.CallContextKey], ctorMsg);
			}
			else
			{
				constructionReturnMessage = new ConstructorReturnMessage(e, null);
				((ConstructorReturnMessage)constructionReturnMessage).SetLogicalCallContext((LogicalCallContext)ctorMsg.Properties[Message.CallContextKey]);
			}
			return constructionReturnMessage;
		}

		// Token: 0x06005CFD RID: 23805 RVA: 0x00145F5C File Offset: 0x0014415C
		[SecurityCritical]
		internal static object ActivateWithMessage(Type serverType, IMessage msg, ServerIdentity srvIdToBind, out Exception e)
		{
			e = null;
			object obj = RemotingServices.AllocateUninitializedObject(serverType);
			object obj2;
			if (serverType.IsContextful)
			{
				if (msg is ConstructorCallMessage)
				{
					obj2 = ((ConstructorCallMessage)msg).GetThisPtr();
				}
				else
				{
					obj2 = null;
				}
				obj2 = RemotingServices.Wrap((ContextBoundObject)obj, obj2, false);
			}
			else
			{
				if (Thread.CurrentContext != Context.DefaultContext)
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_Activation_Failed"));
				}
				obj2 = obj;
			}
			IMessageSink messageSink = new StackBuilderSink(obj2);
			IMethodReturnMessage methodReturnMessage = (IMethodReturnMessage)messageSink.SyncProcessMessage(msg);
			if (methodReturnMessage.Exception != null)
			{
				e = methodReturnMessage.Exception;
				return null;
			}
			if (serverType.IsContextful)
			{
				return RemotingServices.Wrap((ContextBoundObject)obj);
			}
			return obj;
		}

		// Token: 0x06005CFE RID: 23806 RVA: 0x00146000 File Offset: 0x00144200
		[SecurityCritical]
		internal static void StartListeningForRemoteRequests()
		{
			ActivationServices.Startup();
			DomainSpecificRemotingData remotingData = Thread.GetDomain().RemotingData;
			if (!remotingData.ActivatorListening)
			{
				object configLock = remotingData.ConfigLock;
				bool flag = false;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					Monitor.Enter(configLock, ref flag);
					if (!remotingData.ActivatorListening)
					{
						RemotingServices.MarshalInternal(Thread.GetDomain().RemotingData.ActivationListener, "RemoteActivationService.rem", typeof(IActivator));
						ServerIdentity serverIdentity = (ServerIdentity)IdentityHolder.ResolveIdentity("RemoteActivationService.rem");
						serverIdentity.SetSingletonObjectMode();
						remotingData.ActivatorListening = true;
					}
				}
				finally
				{
					if (flag)
					{
						Monitor.Exit(configLock);
					}
				}
			}
		}

		// Token: 0x06005CFF RID: 23807 RVA: 0x001460A0 File Offset: 0x001442A0
		[SecurityCritical]
		internal static IActivator GetActivator()
		{
			DomainSpecificRemotingData remotingData = Thread.GetDomain().RemotingData;
			if (remotingData.LocalActivator == null)
			{
				ActivationServices.Startup();
			}
			return remotingData.LocalActivator;
		}

		// Token: 0x06005D00 RID: 23808 RVA: 0x001460CB File Offset: 0x001442CB
		[SecurityCritical]
		internal static void Initialize()
		{
			ActivationServices.GetActivator();
		}

		// Token: 0x06005D01 RID: 23809 RVA: 0x001460D4 File Offset: 0x001442D4
		[SecurityCritical]
		internal static ContextAttribute GetGlobalAttribute()
		{
			DomainSpecificRemotingData remotingData = Thread.GetDomain().RemotingData;
			if (remotingData.LocalActivator == null)
			{
				ActivationServices.Startup();
			}
			return remotingData.LocalActivator;
		}

		// Token: 0x06005D02 RID: 23810 RVA: 0x00146100 File Offset: 0x00144300
		[SecurityCritical]
		internal static IContextAttribute[] GetContextAttributesForType(Type serverType)
		{
			if (!typeof(ContextBoundObject).IsAssignableFrom(serverType) || serverType.IsCOMObject)
			{
				return new ContextAttribute[0];
			}
			int num = 8;
			IContextAttribute[] array = new IContextAttribute[num];
			int num2 = 0;
			object[] customAttributes = serverType.GetCustomAttributes(typeof(IContextAttribute), true);
			foreach (IContextAttribute contextAttribute in customAttributes)
			{
				Type type = contextAttribute.GetType();
				bool flag = false;
				for (int j = 0; j < num2; j++)
				{
					if (type.Equals(array[j].GetType()))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					num2++;
					if (num2 > num - 1)
					{
						IContextAttribute[] array3 = new IContextAttribute[2 * num];
						Array.Copy(array, 0, array3, 0, num);
						array = array3;
						num *= 2;
					}
					array[num2 - 1] = contextAttribute;
				}
			}
			IContextAttribute[] array4 = new IContextAttribute[num2];
			Array.Copy(array, array4, num2);
			return array4;
		}

		// Token: 0x06005D03 RID: 23811 RVA: 0x001461F8 File Offset: 0x001443F8
		[SecurityCritical]
		internal static object ConnectIfNecessary(IConstructionCallMessage ctorMsg)
		{
			string text = (string)ctorMsg.Properties["Connect"];
			object obj = null;
			if (text != null)
			{
				obj = RemotingServices.Connect(ctorMsg.ActivationType, text);
			}
			return obj;
		}

		// Token: 0x06005D04 RID: 23812 RVA: 0x00146230 File Offset: 0x00144430
		[SecurityCritical]
		internal static object CheckIfConnected(RemotingProxy proxy, IConstructionCallMessage ctorMsg)
		{
			string text = (string)ctorMsg.Properties["Connect"];
			object obj = null;
			if (text != null)
			{
				obj = proxy.GetTransparentProxy();
			}
			return obj;
		}

		// Token: 0x06005D05 RID: 23813 RVA: 0x00146260 File Offset: 0x00144460
		internal static void PushActivationAttributes(Type serverType, object[] attributes)
		{
			if (ActivationServices._attributeStack == null)
			{
				ActivationServices._attributeStack = new ActivationAttributeStack();
			}
			ActivationServices._attributeStack.Push(serverType, attributes);
		}

		// Token: 0x06005D06 RID: 23814 RVA: 0x0014627F File Offset: 0x0014447F
		internal static object[] PeekActivationAttributes(Type serverType)
		{
			if (ActivationServices._attributeStack == null)
			{
				return null;
			}
			return ActivationServices._attributeStack.Peek(serverType);
		}

		// Token: 0x06005D07 RID: 23815 RVA: 0x00146295 File Offset: 0x00144495
		internal static void PopActivationAttributes(Type serverType)
		{
			ActivationServices._attributeStack.Pop(serverType);
		}

		// Token: 0x040029EC RID: 10732
		private static volatile IActivator activator = null;

		// Token: 0x040029ED RID: 10733
		private static Hashtable _proxyTable = new Hashtable();

		// Token: 0x040029EE RID: 10734
		private static readonly Type proxyAttributeType = typeof(ProxyAttribute);

		// Token: 0x040029EF RID: 10735
		[SecurityCritical]
		private static ProxyAttribute _proxyAttribute = new ProxyAttribute();

		// Token: 0x040029F0 RID: 10736
		[ThreadStatic]
		internal static ActivationAttributeStack _attributeStack;

		// Token: 0x040029F1 RID: 10737
		internal static readonly Assembly s_MscorlibAssembly = typeof(object).Assembly;

		// Token: 0x040029F2 RID: 10738
		internal const string ActivationServiceURI = "RemoteActivationService.rem";

		// Token: 0x040029F3 RID: 10739
		internal const string RemoteActivateKey = "Remote";

		// Token: 0x040029F4 RID: 10740
		internal const string PermissionKey = "Permission";

		// Token: 0x040029F5 RID: 10741
		internal const string ConnectKey = "Connect";
	}
}
