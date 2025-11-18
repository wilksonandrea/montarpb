using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Services;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Remoting
{
	// Token: 0x020007CC RID: 1996
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public static class RemotingServices
	{
		// Token: 0x06005628 RID: 22056
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsTransparentProxy(object proxy);

		// Token: 0x06005629 RID: 22057 RVA: 0x001314B0 File Offset: 0x0012F6B0
		[SecuritySafeCritical]
		public static bool IsObjectOutOfContext(object tp)
		{
			if (!RemotingServices.IsTransparentProxy(tp))
			{
				return false;
			}
			RealProxy realProxy = RemotingServices.GetRealProxy(tp);
			Identity identityObject = realProxy.IdentityObject;
			ServerIdentity serverIdentity = identityObject as ServerIdentity;
			return serverIdentity == null || !(realProxy is RemotingProxy) || Thread.CurrentContext != serverIdentity.ServerContext;
		}

		// Token: 0x0600562A RID: 22058 RVA: 0x001314F9 File Offset: 0x0012F6F9
		[__DynamicallyInvokable]
		public static bool IsObjectOutOfAppDomain(object tp)
		{
			return RemotingServices.IsClientProxy(tp);
		}

		// Token: 0x0600562B RID: 22059 RVA: 0x00131504 File Offset: 0x0012F704
		internal static bool IsClientProxy(object obj)
		{
			MarshalByRefObject marshalByRefObject = obj as MarshalByRefObject;
			if (marshalByRefObject == null)
			{
				return false;
			}
			bool flag = false;
			bool flag2;
			Identity identity = MarshalByRefObject.GetIdentity(marshalByRefObject, out flag2);
			if (identity != null && !(identity is ServerIdentity))
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x0600562C RID: 22060 RVA: 0x00131538 File Offset: 0x0012F738
		[SecurityCritical]
		internal static bool IsObjectOutOfProcess(object tp)
		{
			if (!RemotingServices.IsTransparentProxy(tp))
			{
				return false;
			}
			RealProxy realProxy = RemotingServices.GetRealProxy(tp);
			Identity identityObject = realProxy.IdentityObject;
			if (identityObject is ServerIdentity)
			{
				return false;
			}
			if (identityObject != null)
			{
				ObjRef objectRef = identityObject.ObjectRef;
				return objectRef == null || !objectRef.IsFromThisProcess();
			}
			return true;
		}

		// Token: 0x0600562D RID: 22061
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern RealProxy GetRealProxy(object proxy);

		// Token: 0x0600562E RID: 22062
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object CreateTransparentProxy(RealProxy rp, RuntimeType typeToProxy, IntPtr stub, object stubData);

		// Token: 0x0600562F RID: 22063 RVA: 0x00131584 File Offset: 0x0012F784
		[SecurityCritical]
		internal static object CreateTransparentProxy(RealProxy rp, Type typeToProxy, IntPtr stub, object stubData)
		{
			RuntimeType runtimeType = typeToProxy as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), "typeToProxy"));
			}
			return RemotingServices.CreateTransparentProxy(rp, runtimeType, stub, stubData);
		}

		// Token: 0x06005630 RID: 22064
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern MarshalByRefObject AllocateUninitializedObject(RuntimeType objectType);

		// Token: 0x06005631 RID: 22065
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void CallDefaultCtor(object o);

		// Token: 0x06005632 RID: 22066 RVA: 0x001315CC File Offset: 0x0012F7CC
		[SecurityCritical]
		internal static MarshalByRefObject AllocateUninitializedObject(Type objectType)
		{
			RuntimeType runtimeType = objectType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), "objectType"));
			}
			return RemotingServices.AllocateUninitializedObject(runtimeType);
		}

		// Token: 0x06005633 RID: 22067
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern MarshalByRefObject AllocateInitializedObject(RuntimeType objectType);

		// Token: 0x06005634 RID: 22068 RVA: 0x00131610 File Offset: 0x0012F810
		[SecurityCritical]
		internal static MarshalByRefObject AllocateInitializedObject(Type objectType)
		{
			RuntimeType runtimeType = objectType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), "objectType"));
			}
			return RemotingServices.AllocateInitializedObject(runtimeType);
		}

		// Token: 0x06005635 RID: 22069 RVA: 0x00131654 File Offset: 0x0012F854
		[SecurityCritical]
		internal static bool RegisterWellKnownChannels()
		{
			if (!RemotingServices.s_bRegisteredWellKnownChannels)
			{
				bool flag = false;
				object configLock = Thread.GetDomain().RemotingData.ConfigLock;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					Monitor.Enter(configLock, ref flag);
					if (!RemotingServices.s_bRegisteredWellKnownChannels && !RemotingServices.s_bInProcessOfRegisteringWellKnownChannels)
					{
						RemotingServices.s_bInProcessOfRegisteringWellKnownChannels = true;
						CrossAppDomainChannel.RegisterChannel();
						RemotingServices.s_bRegisteredWellKnownChannels = true;
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
			return true;
		}

		// Token: 0x06005636 RID: 22070 RVA: 0x001316CC File Offset: 0x0012F8CC
		[SecurityCritical]
		internal static void InternalSetRemoteActivationConfigured()
		{
			if (!RemotingServices.s_bRemoteActivationConfigured)
			{
				RemotingServices.nSetRemoteActivationConfigured();
				RemotingServices.s_bRemoteActivationConfigured = true;
			}
		}

		// Token: 0x06005637 RID: 22071
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void nSetRemoteActivationConfigured();

		// Token: 0x06005638 RID: 22072 RVA: 0x001316E4 File Offset: 0x0012F8E4
		[SecurityCritical]
		public static string GetSessionIdForMethodMessage(IMethodMessage msg)
		{
			return msg.Uri;
		}

		// Token: 0x06005639 RID: 22073 RVA: 0x001316EC File Offset: 0x0012F8EC
		[SecuritySafeCritical]
		public static object GetLifetimeService(MarshalByRefObject obj)
		{
			if (obj != null)
			{
				return obj.GetLifetimeService();
			}
			return null;
		}

		// Token: 0x0600563A RID: 22074 RVA: 0x001316FC File Offset: 0x0012F8FC
		[SecurityCritical]
		public static string GetObjectUri(MarshalByRefObject obj)
		{
			bool flag;
			Identity identity = MarshalByRefObject.GetIdentity(obj, out flag);
			if (identity != null)
			{
				return identity.URI;
			}
			return null;
		}

		// Token: 0x0600563B RID: 22075 RVA: 0x00131720 File Offset: 0x0012F920
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static void SetObjectUriForMarshal(MarshalByRefObject obj, string uri)
		{
			bool flag;
			Identity identity = MarshalByRefObject.GetIdentity(obj, out flag);
			Identity identity2 = identity as ServerIdentity;
			if (identity != null && identity2 == null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_SetObjectUriForMarshal__ObjectNeedsToBeLocal"));
			}
			if (identity != null && identity.URI != null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_SetObjectUriForMarshal__UriExists"));
			}
			if (identity == null)
			{
				Context defaultContext = Thread.GetDomain().GetDefaultContext();
				ServerIdentity serverIdentity = new ServerIdentity(obj, defaultContext, uri);
				identity = obj.__RaceSetServerIdentity(serverIdentity);
				if (identity != serverIdentity)
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_SetObjectUriForMarshal__UriExists"));
				}
			}
			else
			{
				identity.SetOrCreateURI(uri, true);
			}
		}

		// Token: 0x0600563C RID: 22076 RVA: 0x001317B2 File Offset: 0x0012F9B2
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static ObjRef Marshal(MarshalByRefObject Obj)
		{
			return RemotingServices.MarshalInternal(Obj, null, null);
		}

		// Token: 0x0600563D RID: 22077 RVA: 0x001317BC File Offset: 0x0012F9BC
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static ObjRef Marshal(MarshalByRefObject Obj, string URI)
		{
			return RemotingServices.MarshalInternal(Obj, URI, null);
		}

		// Token: 0x0600563E RID: 22078 RVA: 0x001317C6 File Offset: 0x0012F9C6
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static ObjRef Marshal(MarshalByRefObject Obj, string ObjURI, Type RequestedType)
		{
			return RemotingServices.MarshalInternal(Obj, ObjURI, RequestedType);
		}

		// Token: 0x0600563F RID: 22079 RVA: 0x001317D0 File Offset: 0x0012F9D0
		[SecurityCritical]
		internal static ObjRef MarshalInternal(MarshalByRefObject Obj, string ObjURI, Type RequestedType)
		{
			return RemotingServices.MarshalInternal(Obj, ObjURI, RequestedType, true);
		}

		// Token: 0x06005640 RID: 22080 RVA: 0x001317DB File Offset: 0x0012F9DB
		[SecurityCritical]
		internal static ObjRef MarshalInternal(MarshalByRefObject Obj, string ObjURI, Type RequestedType, bool updateChannelData)
		{
			return RemotingServices.MarshalInternal(Obj, ObjURI, RequestedType, updateChannelData, false);
		}

		// Token: 0x06005641 RID: 22081 RVA: 0x001317E8 File Offset: 0x0012F9E8
		[SecurityCritical]
		internal static ObjRef MarshalInternal(MarshalByRefObject Obj, string ObjURI, Type RequestedType, bool updateChannelData, bool isInitializing)
		{
			if (Obj == null)
			{
				return null;
			}
			ObjRef objRef = null;
			Identity orCreateIdentity = RemotingServices.GetOrCreateIdentity(Obj, ObjURI, isInitializing);
			if (RequestedType != null)
			{
				ServerIdentity serverIdentity = orCreateIdentity as ServerIdentity;
				if (serverIdentity != null)
				{
					serverIdentity.ServerType = RequestedType;
					serverIdentity.MarshaledAsSpecificType = true;
				}
			}
			objRef = orCreateIdentity.ObjectRef;
			if (objRef == null)
			{
				if (RemotingServices.IsTransparentProxy(Obj))
				{
					RealProxy realProxy = RemotingServices.GetRealProxy(Obj);
					objRef = realProxy.CreateObjRef(RequestedType);
				}
				else
				{
					objRef = Obj.CreateObjRef(RequestedType);
				}
				if (orCreateIdentity == null || objRef == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidMarshalByRefObject"), "Obj");
				}
				objRef = orCreateIdentity.RaceSetObjRef(objRef);
			}
			ServerIdentity serverIdentity2 = orCreateIdentity as ServerIdentity;
			if (serverIdentity2 != null)
			{
				MarshalByRefObject marshalByRefObject = null;
				serverIdentity2.GetServerObjectChain(out marshalByRefObject);
				Lease lease = orCreateIdentity.Lease;
				if (lease != null)
				{
					Lease lease2 = lease;
					lock (lease2)
					{
						if (lease.CurrentState == LeaseState.Expired)
						{
							lease.ActivateLease();
						}
						else
						{
							lease.RenewInternal(orCreateIdentity.Lease.InitialLeaseTime);
						}
					}
				}
				if (updateChannelData && objRef.ChannelInfo != null)
				{
					object[] currentChannelData = ChannelServices.CurrentChannelData;
					if (!(Obj is AppDomain))
					{
						objRef.ChannelInfo.ChannelData = currentChannelData;
					}
					else
					{
						int num = currentChannelData.Length;
						object[] array = new object[num];
						Array.Copy(currentChannelData, array, num);
						for (int i = 0; i < num; i++)
						{
							if (!(array[i] is CrossAppDomainData))
							{
								array[i] = null;
							}
						}
						objRef.ChannelInfo.ChannelData = array;
					}
				}
			}
			TrackingServices.MarshaledObject(Obj, objRef);
			return objRef;
		}

		// Token: 0x06005642 RID: 22082 RVA: 0x00131970 File Offset: 0x0012FB70
		[SecurityCritical]
		private static Identity GetOrCreateIdentity(MarshalByRefObject Obj, string ObjURI, bool isInitializing)
		{
			int num = 2;
			if (isInitializing)
			{
				num |= 4;
			}
			Identity identity;
			if (RemotingServices.IsTransparentProxy(Obj))
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(Obj);
				identity = realProxy.IdentityObject;
				if (identity == null)
				{
					identity = IdentityHolder.FindOrCreateServerIdentity(Obj, ObjURI, num);
					identity.RaceSetTransparentProxy(Obj);
				}
				ServerIdentity serverIdentity = identity as ServerIdentity;
				if (serverIdentity != null)
				{
					identity = IdentityHolder.FindOrCreateServerIdentity(serverIdentity.TPOrObject, ObjURI, num);
					if (ObjURI != null && ObjURI != Identity.RemoveAppNameOrAppGuidIfNecessary(identity.ObjURI))
					{
						throw new RemotingException(Environment.GetResourceString("Remoting_URIExists"));
					}
				}
				else if (ObjURI != null && ObjURI != identity.ObjURI)
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_URIToProxy"));
				}
			}
			else
			{
				identity = IdentityHolder.FindOrCreateServerIdentity(Obj, ObjURI, num);
			}
			return identity;
		}

		// Token: 0x06005643 RID: 22083 RVA: 0x00131A20 File Offset: 0x0012FC20
		[SecurityCritical]
		public static void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			ObjRef objRef = RemotingServices.MarshalInternal((MarshalByRefObject)obj, null, null);
			objRef.GetObjectData(info, context);
		}

		// Token: 0x06005644 RID: 22084 RVA: 0x00131A5F File Offset: 0x0012FC5F
		[SecurityCritical]
		public static object Unmarshal(ObjRef objectRef)
		{
			return RemotingServices.InternalUnmarshal(objectRef, null, false);
		}

		// Token: 0x06005645 RID: 22085 RVA: 0x00131A69 File Offset: 0x0012FC69
		[SecurityCritical]
		public static object Unmarshal(ObjRef objectRef, bool fRefine)
		{
			return RemotingServices.InternalUnmarshal(objectRef, null, fRefine);
		}

		// Token: 0x06005646 RID: 22086 RVA: 0x00131A73 File Offset: 0x0012FC73
		[SecurityCritical]
		[ComVisible(true)]
		public static object Connect(Type classToProxy, string url)
		{
			return RemotingServices.Unmarshal(classToProxy, url, null);
		}

		// Token: 0x06005647 RID: 22087 RVA: 0x00131A7D File Offset: 0x0012FC7D
		[SecurityCritical]
		[ComVisible(true)]
		public static object Connect(Type classToProxy, string url, object data)
		{
			return RemotingServices.Unmarshal(classToProxy, url, data);
		}

		// Token: 0x06005648 RID: 22088 RVA: 0x00131A87 File Offset: 0x0012FC87
		[SecurityCritical]
		public static bool Disconnect(MarshalByRefObject obj)
		{
			return RemotingServices.Disconnect(obj, true);
		}

		// Token: 0x06005649 RID: 22089 RVA: 0x00131A90 File Offset: 0x0012FC90
		[SecurityCritical]
		internal static bool Disconnect(MarshalByRefObject obj, bool bResetURI)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			bool flag;
			Identity identity = MarshalByRefObject.GetIdentity(obj, out flag);
			bool flag2 = false;
			if (identity != null)
			{
				if (!(identity is ServerIdentity))
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_CantDisconnectClientProxy"));
				}
				if (identity.IsInIDTable())
				{
					IdentityHolder.RemoveIdentity(identity.URI, bResetURI);
					flag2 = true;
				}
				TrackingServices.DisconnectedObject(obj);
			}
			return flag2;
		}

		// Token: 0x0600564A RID: 22090 RVA: 0x00131AF0 File Offset: 0x0012FCF0
		[SecurityCritical]
		public static IMessageSink GetEnvoyChainForProxy(MarshalByRefObject obj)
		{
			IMessageSink messageSink = null;
			if (RemotingServices.IsObjectOutOfContext(obj))
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(obj);
				Identity identityObject = realProxy.IdentityObject;
				if (identityObject != null)
				{
					messageSink = identityObject.EnvoyChain;
				}
			}
			return messageSink;
		}

		// Token: 0x0600564B RID: 22091 RVA: 0x00131B20 File Offset: 0x0012FD20
		[SecurityCritical]
		public static ObjRef GetObjRefForProxy(MarshalByRefObject obj)
		{
			ObjRef objRef = null;
			if (!RemotingServices.IsTransparentProxy(obj))
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Proxy_BadType"));
			}
			RealProxy realProxy = RemotingServices.GetRealProxy(obj);
			Identity identityObject = realProxy.IdentityObject;
			if (identityObject != null)
			{
				objRef = identityObject.ObjectRef;
			}
			return objRef;
		}

		// Token: 0x0600564C RID: 22092 RVA: 0x00131B60 File Offset: 0x0012FD60
		[SecurityCritical]
		internal static object Unmarshal(Type classToProxy, string url)
		{
			return RemotingServices.Unmarshal(classToProxy, url, null);
		}

		// Token: 0x0600564D RID: 22093 RVA: 0x00131B6C File Offset: 0x0012FD6C
		[SecurityCritical]
		internal static object Unmarshal(Type classToProxy, string url, object data)
		{
			if (null == classToProxy)
			{
				throw new ArgumentNullException("classToProxy");
			}
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			if (!classToProxy.IsMarshalByRef && !classToProxy.IsInterface)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_NotRemotableByReference"));
			}
			Identity identity = IdentityHolder.ResolveIdentity(url);
			if (identity == null || identity.ChannelSink == null || identity.EnvoyChain == null)
			{
				IMessageSink messageSink = null;
				IMessageSink messageSink2 = null;
				string text = RemotingServices.CreateEnvoyAndChannelSinks(url, data, out messageSink, out messageSink2);
				if (messageSink == null)
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Connect_CantCreateChannelSink"), url));
				}
				if (text == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidUrl"));
				}
				identity = IdentityHolder.FindOrCreateIdentity(text, url, null);
				RemotingServices.SetEnvoyAndChannelSinks(identity, messageSink, messageSink2);
			}
			return RemotingServices.GetOrCreateProxy(classToProxy, identity);
		}

		// Token: 0x0600564E RID: 22094 RVA: 0x00131C36 File Offset: 0x0012FE36
		[SecurityCritical]
		internal static object Wrap(ContextBoundObject obj)
		{
			return RemotingServices.Wrap(obj, null, true);
		}

		// Token: 0x0600564F RID: 22095 RVA: 0x00131C40 File Offset: 0x0012FE40
		[SecurityCritical]
		internal static object Wrap(ContextBoundObject obj, object proxy, bool fCreateSinks)
		{
			if (obj != null && !RemotingServices.IsTransparentProxy(obj))
			{
				Identity identity;
				if (proxy != null)
				{
					RealProxy realProxy = RemotingServices.GetRealProxy(proxy);
					if (realProxy.UnwrappedServerObject == null)
					{
						realProxy.AttachServerHelper(obj);
					}
					identity = MarshalByRefObject.GetIdentity(obj);
				}
				else
				{
					identity = IdentityHolder.FindOrCreateServerIdentity(obj, null, 0);
				}
				proxy = RemotingServices.GetOrCreateProxy(identity, proxy, true);
				RemotingServices.GetRealProxy(proxy).Wrap();
				if (fCreateSinks)
				{
					IMessageSink messageSink = null;
					IMessageSink messageSink2 = null;
					RemotingServices.CreateEnvoyAndChannelSinks((MarshalByRefObject)proxy, null, out messageSink, out messageSink2);
					RemotingServices.SetEnvoyAndChannelSinks(identity, messageSink, messageSink2);
				}
				RealProxy realProxy2 = RemotingServices.GetRealProxy(proxy);
				if (realProxy2.UnwrappedServerObject == null)
				{
					realProxy2.AttachServerHelper(obj);
				}
				return proxy;
			}
			return obj;
		}

		// Token: 0x06005650 RID: 22096 RVA: 0x00131CD8 File Offset: 0x0012FED8
		internal static string GetObjectUriFromFullUri(string fullUri)
		{
			if (fullUri == null)
			{
				return null;
			}
			int num = fullUri.LastIndexOf('/');
			if (num == -1)
			{
				return fullUri;
			}
			return fullUri.Substring(num + 1);
		}

		// Token: 0x06005651 RID: 22097
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object Unwrap(ContextBoundObject obj);

		// Token: 0x06005652 RID: 22098
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object AlwaysUnwrap(ContextBoundObject obj);

		// Token: 0x06005653 RID: 22099 RVA: 0x00131D04 File Offset: 0x0012FF04
		[SecurityCritical]
		internal static object InternalUnmarshal(ObjRef objectRef, object proxy, bool fRefine)
		{
			Context context = Thread.CurrentContext;
			if (!ObjRef.IsWellFormed(objectRef))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_BadObjRef"), "Unmarshal"));
			}
			object obj;
			Identity identity;
			if (objectRef.IsWellKnown())
			{
				obj = RemotingServices.Unmarshal(typeof(MarshalByRefObject), objectRef.URI);
				identity = IdentityHolder.ResolveIdentity(objectRef.URI);
				if (identity.ObjectRef == null)
				{
					identity.RaceSetObjRef(objectRef);
				}
				return obj;
			}
			identity = IdentityHolder.FindOrCreateIdentity(objectRef.URI, null, objectRef);
			context = Thread.CurrentContext;
			ServerIdentity serverIdentity = identity as ServerIdentity;
			if (serverIdentity != null)
			{
				context = Thread.CurrentContext;
				if (!serverIdentity.IsContextBound)
				{
					if (proxy != null)
					{
						throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadInternalState_ProxySameAppDomain"), Array.Empty<object>()));
					}
					obj = serverIdentity.TPOrObject;
				}
				else
				{
					IMessageSink messageSink = null;
					IMessageSink messageSink2 = null;
					RemotingServices.CreateEnvoyAndChannelSinks(serverIdentity.TPOrObject, null, out messageSink, out messageSink2);
					RemotingServices.SetEnvoyAndChannelSinks(identity, messageSink, messageSink2);
					obj = RemotingServices.GetOrCreateProxy(identity, proxy, true);
				}
			}
			else
			{
				IMessageSink messageSink3 = null;
				IMessageSink messageSink4 = null;
				if (!objectRef.IsObjRefLite())
				{
					RemotingServices.CreateEnvoyAndChannelSinks(null, objectRef, out messageSink3, out messageSink4);
				}
				else
				{
					RemotingServices.CreateEnvoyAndChannelSinks(objectRef.URI, null, out messageSink3, out messageSink4);
				}
				RemotingServices.SetEnvoyAndChannelSinks(identity, messageSink3, messageSink4);
				if (objectRef.HasProxyAttribute())
				{
					fRefine = true;
				}
				obj = RemotingServices.GetOrCreateProxy(identity, proxy, fRefine);
			}
			TrackingServices.UnmarshaledObject(obj, objectRef);
			return obj;
		}

		// Token: 0x06005654 RID: 22100 RVA: 0x00131E54 File Offset: 0x00130054
		[SecurityCritical]
		private static object GetOrCreateProxy(Identity idObj, object proxy, bool fRefine)
		{
			if (proxy == null)
			{
				ServerIdentity serverIdentity = idObj as ServerIdentity;
				Type type;
				if (serverIdentity != null)
				{
					type = serverIdentity.ServerType;
				}
				else
				{
					IRemotingTypeInfo typeInfo = idObj.ObjectRef.TypeInfo;
					type = null;
					if ((typeInfo is TypeInfo && !fRefine) || typeInfo == null)
					{
						type = typeof(MarshalByRefObject);
					}
					else
					{
						string typeName = typeInfo.TypeName;
						if (typeName != null)
						{
							string text = null;
							string text2 = null;
							TypeInfo.ParseTypeAndAssembly(typeName, out text, out text2);
							Assembly assembly = FormatterServices.LoadAssemblyFromStringNoThrow(text2);
							if (assembly != null)
							{
								type = assembly.GetType(text, false, false);
							}
						}
					}
					if (null == type)
					{
						throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadType"), typeInfo.TypeName));
					}
				}
				proxy = RemotingServices.SetOrCreateProxy(idObj, type, null);
			}
			else
			{
				proxy = RemotingServices.SetOrCreateProxy(idObj, null, proxy);
			}
			if (proxy == null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_UnexpectedNullTP"));
			}
			return proxy;
		}

		// Token: 0x06005655 RID: 22101 RVA: 0x00131F34 File Offset: 0x00130134
		[SecurityCritical]
		private static object GetOrCreateProxy(Type classToProxy, Identity idObj)
		{
			object obj = idObj.TPOrObject;
			if (obj == null)
			{
				obj = RemotingServices.SetOrCreateProxy(idObj, classToProxy, null);
			}
			ServerIdentity serverIdentity = idObj as ServerIdentity;
			if (serverIdentity != null)
			{
				Type serverType = serverIdentity.ServerType;
				if (!classToProxy.IsAssignableFrom(serverType))
				{
					throw new InvalidCastException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("InvalidCast_FromTo"), serverType.FullName, classToProxy.FullName));
				}
			}
			return obj;
		}

		// Token: 0x06005656 RID: 22102 RVA: 0x00131F98 File Offset: 0x00130198
		[SecurityCritical]
		private static MarshalByRefObject SetOrCreateProxy(Identity idObj, Type classToProxy, object proxy)
		{
			RealProxy realProxy = null;
			if (proxy == null)
			{
				ServerIdentity serverIdentity = idObj as ServerIdentity;
				if (idObj.ObjectRef != null)
				{
					ProxyAttribute proxyAttribute = ActivationServices.GetProxyAttribute(classToProxy);
					realProxy = proxyAttribute.CreateProxy(idObj.ObjectRef, classToProxy, null, null);
				}
				if (realProxy == null)
				{
					ProxyAttribute defaultProxyAttribute = ActivationServices.DefaultProxyAttribute;
					realProxy = defaultProxyAttribute.CreateProxy(idObj.ObjectRef, classToProxy, null, (serverIdentity == null) ? null : serverIdentity.ServerContext);
				}
			}
			else
			{
				realProxy = RemotingServices.GetRealProxy(proxy);
			}
			realProxy.IdentityObject = idObj;
			proxy = realProxy.GetTransparentProxy();
			proxy = idObj.RaceSetTransparentProxy(proxy);
			return (MarshalByRefObject)proxy;
		}

		// Token: 0x06005657 RID: 22103 RVA: 0x0013201C File Offset: 0x0013021C
		private static bool AreChannelDataElementsNull(object[] channelData)
		{
			foreach (object obj in channelData)
			{
				if (obj != null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06005658 RID: 22104 RVA: 0x00132044 File Offset: 0x00130244
		[SecurityCritical]
		internal static void CreateEnvoyAndChannelSinks(MarshalByRefObject tpOrObject, ObjRef objectRef, out IMessageSink chnlSink, out IMessageSink envoySink)
		{
			chnlSink = null;
			envoySink = null;
			if (objectRef == null)
			{
				chnlSink = ChannelServices.GetCrossContextChannelSink();
				envoySink = Thread.CurrentContext.CreateEnvoyChain(tpOrObject);
				return;
			}
			object[] channelData = objectRef.ChannelInfo.ChannelData;
			if (channelData != null && !RemotingServices.AreChannelDataElementsNull(channelData))
			{
				for (int i = 0; i < channelData.Length; i++)
				{
					chnlSink = ChannelServices.CreateMessageSink(channelData[i]);
					if (chnlSink != null)
					{
						break;
					}
				}
				if (chnlSink == null)
				{
					object obj = RemotingServices.s_delayLoadChannelLock;
					lock (obj)
					{
						for (int j = 0; j < channelData.Length; j++)
						{
							chnlSink = ChannelServices.CreateMessageSink(channelData[j]);
							if (chnlSink != null)
							{
								break;
							}
						}
						if (chnlSink == null)
						{
							foreach (object obj2 in channelData)
							{
								string text;
								chnlSink = RemotingConfigHandler.FindDelayLoadChannelForCreateMessageSink(null, obj2, out text);
								if (chnlSink != null)
								{
									break;
								}
							}
						}
					}
				}
			}
			if (objectRef.EnvoyInfo != null && objectRef.EnvoyInfo.EnvoySinks != null)
			{
				envoySink = objectRef.EnvoyInfo.EnvoySinks;
				return;
			}
			envoySink = EnvoyTerminatorSink.MessageSink;
		}

		// Token: 0x06005659 RID: 22105 RVA: 0x00132158 File Offset: 0x00130358
		[SecurityCritical]
		internal static string CreateEnvoyAndChannelSinks(string url, object data, out IMessageSink chnlSink, out IMessageSink envoySink)
		{
			string text = RemotingServices.CreateChannelSink(url, data, out chnlSink);
			envoySink = EnvoyTerminatorSink.MessageSink;
			return text;
		}

		// Token: 0x0600565A RID: 22106 RVA: 0x00132178 File Offset: 0x00130378
		[SecurityCritical]
		private static string CreateChannelSink(string url, object data, out IMessageSink chnlSink)
		{
			string text = null;
			chnlSink = ChannelServices.CreateMessageSink(url, data, out text);
			if (chnlSink == null)
			{
				object obj = RemotingServices.s_delayLoadChannelLock;
				lock (obj)
				{
					chnlSink = ChannelServices.CreateMessageSink(url, data, out text);
					if (chnlSink == null)
					{
						chnlSink = RemotingConfigHandler.FindDelayLoadChannelForCreateMessageSink(url, data, out text);
					}
				}
			}
			return text;
		}

		// Token: 0x0600565B RID: 22107 RVA: 0x001321E0 File Offset: 0x001303E0
		internal static void SetEnvoyAndChannelSinks(Identity idObj, IMessageSink chnlSink, IMessageSink envoySink)
		{
			if (idObj.ChannelSink == null && chnlSink != null)
			{
				idObj.RaceSetChannelSink(chnlSink);
			}
			if (idObj.EnvoyChain != null)
			{
				return;
			}
			if (envoySink != null)
			{
				idObj.RaceSetEnvoyChain(envoySink);
				return;
			}
			throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadInternalState_FailEnvoySink"), Array.Empty<object>()));
		}

		// Token: 0x0600565C RID: 22108 RVA: 0x00132234 File Offset: 0x00130434
		[SecurityCritical]
		private static bool CheckCast(RealProxy rp, RuntimeType castType)
		{
			bool flag = false;
			if (castType == typeof(object))
			{
				return true;
			}
			if (!castType.IsInterface && !castType.IsMarshalByRef)
			{
				return false;
			}
			if (castType != typeof(IObjectReference))
			{
				IRemotingTypeInfo remotingTypeInfo = rp as IRemotingTypeInfo;
				if (remotingTypeInfo != null)
				{
					flag = remotingTypeInfo.CanCastTo(castType, rp.GetTransparentProxy());
				}
				else
				{
					Identity identityObject = rp.IdentityObject;
					if (identityObject != null)
					{
						ObjRef objectRef = identityObject.ObjectRef;
						if (objectRef != null)
						{
							remotingTypeInfo = objectRef.TypeInfo;
							if (remotingTypeInfo != null)
							{
								flag = remotingTypeInfo.CanCastTo(castType, rp.GetTransparentProxy());
							}
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x0600565D RID: 22109 RVA: 0x001322C2 File Offset: 0x001304C2
		[SecurityCritical]
		internal static bool ProxyCheckCast(RealProxy rp, RuntimeType castType)
		{
			return RemotingServices.CheckCast(rp, castType);
		}

		// Token: 0x0600565E RID: 22110
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object CheckCast(object objToExpand, RuntimeType type);

		// Token: 0x0600565F RID: 22111 RVA: 0x001322CC File Offset: 0x001304CC
		[SecurityCritical]
		internal static GCHandle CreateDelegateInvocation(WaitCallback waitDelegate, object state)
		{
			return GCHandle.Alloc(new object[] { waitDelegate, state });
		}

		// Token: 0x06005660 RID: 22112 RVA: 0x001322EE File Offset: 0x001304EE
		[SecurityCritical]
		internal static void DisposeDelegateInvocation(GCHandle delegateCallToken)
		{
			delegateCallToken.Free();
		}

		// Token: 0x06005661 RID: 22113 RVA: 0x001322F8 File Offset: 0x001304F8
		[SecurityCritical]
		internal static object CreateProxyForDomain(int appDomainId, IntPtr defCtxID)
		{
			ObjRef objRef = RemotingServices.CreateDataForDomain(appDomainId, defCtxID);
			return (AppDomain)RemotingServices.Unmarshal(objRef);
		}

		// Token: 0x06005662 RID: 22114 RVA: 0x0013231C File Offset: 0x0013051C
		[SecurityCritical]
		internal static object CreateDataForDomainCallback(object[] args)
		{
			RemotingServices.RegisterWellKnownChannels();
			ObjRef objRef = RemotingServices.MarshalInternal(Thread.CurrentContext.AppDomain, null, null, false);
			ServerIdentity serverIdentity = (ServerIdentity)MarshalByRefObject.GetIdentity(Thread.CurrentContext.AppDomain);
			serverIdentity.SetHandle();
			objRef.SetServerIdentity(serverIdentity.GetHandle());
			objRef.SetDomainID(AppDomain.CurrentDomain.GetId());
			return objRef;
		}

		// Token: 0x06005663 RID: 22115 RVA: 0x0013237C File Offset: 0x0013057C
		[SecurityCritical]
		internal static ObjRef CreateDataForDomain(int appDomainId, IntPtr defCtxID)
		{
			RemotingServices.RegisterWellKnownChannels();
			InternalCrossContextDelegate internalCrossContextDelegate = new InternalCrossContextDelegate(RemotingServices.CreateDataForDomainCallback);
			return (ObjRef)Thread.CurrentThread.InternalCrossContextCallback(null, defCtxID, appDomainId, internalCrossContextDelegate, null);
		}

		// Token: 0x06005664 RID: 22116 RVA: 0x001323B0 File Offset: 0x001305B0
		[SecurityCritical]
		public static MethodBase GetMethodBaseFromMethodMessage(IMethodMessage msg)
		{
			return RemotingServices.InternalGetMethodBaseFromMethodMessage(msg);
		}

		// Token: 0x06005665 RID: 22117 RVA: 0x001323C8 File Offset: 0x001305C8
		[SecurityCritical]
		internal static MethodBase InternalGetMethodBaseFromMethodMessage(IMethodMessage msg)
		{
			if (msg == null)
			{
				return null;
			}
			Type type = RemotingServices.InternalGetTypeFromQualifiedTypeName(msg.TypeName);
			if (type == null)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadType"), msg.TypeName));
			}
			Type[] array = (Type[])msg.MethodSignature;
			return RemotingServices.GetMethodBase(msg, type, array);
		}

		// Token: 0x06005666 RID: 22118 RVA: 0x00132424 File Offset: 0x00130624
		[SecurityCritical]
		public static bool IsMethodOverloaded(IMethodMessage msg)
		{
			RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(msg.MethodBase);
			return reflectionCachedData.IsOverloaded();
		}

		// Token: 0x06005667 RID: 22119 RVA: 0x00132444 File Offset: 0x00130644
		[SecurityCritical]
		private static MethodBase GetMethodBase(IMethodMessage msg, Type t, Type[] signature)
		{
			MethodBase methodBase = null;
			if (msg is IConstructionCallMessage || msg is IConstructionReturnMessage)
			{
				if (signature == null)
				{
					RuntimeType runtimeType = t as RuntimeType;
					ConstructorInfo[] array;
					if (runtimeType == null)
					{
						array = t.GetConstructors();
					}
					else
					{
						array = runtimeType.GetConstructors();
					}
					if (1 != array.Length)
					{
						throw new AmbiguousMatchException(Environment.GetResourceString("Remoting_AmbiguousCTOR"));
					}
					methodBase = array[0];
				}
				else
				{
					RuntimeType runtimeType2 = t as RuntimeType;
					if (runtimeType2 == null)
					{
						methodBase = t.GetConstructor(signature);
					}
					else
					{
						methodBase = runtimeType2.GetConstructor(signature);
					}
				}
			}
			else if (msg is IMethodCallMessage || msg is IMethodReturnMessage)
			{
				if (signature == null)
				{
					RuntimeType runtimeType3 = t as RuntimeType;
					if (runtimeType3 == null)
					{
						methodBase = t.GetMethod(msg.MethodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
					}
					else
					{
						methodBase = runtimeType3.GetMethod(msg.MethodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
					}
				}
				else
				{
					RuntimeType runtimeType4 = t as RuntimeType;
					if (runtimeType4 == null)
					{
						methodBase = t.GetMethod(msg.MethodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, signature, null);
					}
					else
					{
						methodBase = runtimeType4.GetMethod(msg.MethodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, CallingConventions.Any, signature, null);
					}
				}
			}
			return methodBase;
		}

		// Token: 0x06005668 RID: 22120 RVA: 0x00132554 File Offset: 0x00130754
		[SecurityCritical]
		internal static bool IsMethodAllowedRemotely(MethodBase method)
		{
			if (RemotingServices.s_FieldGetterMB == null || RemotingServices.s_FieldSetterMB == null || RemotingServices.s_IsInstanceOfTypeMB == null || RemotingServices.s_InvokeMemberMB == null || RemotingServices.s_CanCastToXmlTypeMB == null)
			{
				CodeAccessPermission.Assert(true);
				if (RemotingServices.s_FieldGetterMB == null)
				{
					RemotingServices.s_FieldGetterMB = typeof(object).GetMethod("FieldGetter", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				if (RemotingServices.s_FieldSetterMB == null)
				{
					RemotingServices.s_FieldSetterMB = typeof(object).GetMethod("FieldSetter", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				if (RemotingServices.s_IsInstanceOfTypeMB == null)
				{
					RemotingServices.s_IsInstanceOfTypeMB = typeof(MarshalByRefObject).GetMethod("IsInstanceOfType", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				if (RemotingServices.s_CanCastToXmlTypeMB == null)
				{
					RemotingServices.s_CanCastToXmlTypeMB = typeof(MarshalByRefObject).GetMethod("CanCastToXmlType", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				if (RemotingServices.s_InvokeMemberMB == null)
				{
					RemotingServices.s_InvokeMemberMB = typeof(MarshalByRefObject).GetMethod("InvokeMember", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
			}
			return method == RemotingServices.s_FieldGetterMB || method == RemotingServices.s_FieldSetterMB || method == RemotingServices.s_IsInstanceOfTypeMB || method == RemotingServices.s_InvokeMemberMB || method == RemotingServices.s_CanCastToXmlTypeMB;
		}

		// Token: 0x06005669 RID: 22121 RVA: 0x001326DC File Offset: 0x001308DC
		[SecurityCritical]
		public static bool IsOneWay(MethodBase method)
		{
			if (method == null)
			{
				return false;
			}
			RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(method);
			return reflectionCachedData.IsOneWayMethod();
		}

		// Token: 0x0600566A RID: 22122 RVA: 0x00132704 File Offset: 0x00130904
		internal static bool FindAsyncMethodVersion(MethodInfo method, out MethodInfo beginMethod, out MethodInfo endMethod)
		{
			beginMethod = null;
			endMethod = null;
			string text = "Begin" + method.Name;
			string text2 = "End" + method.Name;
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			Type typeFromHandle = typeof(IAsyncResult);
			Type returnType = method.ReturnType;
			ParameterInfo[] parameters = method.GetParameters();
			foreach (ParameterInfo parameterInfo in parameters)
			{
				if (parameterInfo.IsOut)
				{
					arrayList2.Add(parameterInfo);
				}
				else if (parameterInfo.ParameterType.IsByRef)
				{
					arrayList.Add(parameterInfo);
					arrayList2.Add(parameterInfo);
				}
				else
				{
					arrayList.Add(parameterInfo);
				}
			}
			arrayList.Add(typeof(AsyncCallback));
			arrayList.Add(typeof(object));
			arrayList2.Add(typeof(IAsyncResult));
			Type declaringType = method.DeclaringType;
			MethodInfo[] methods = declaringType.GetMethods();
			foreach (MethodInfo methodInfo in methods)
			{
				ParameterInfo[] parameters2 = methodInfo.GetParameters();
				if (methodInfo.Name.Equals(text) && methodInfo.ReturnType == typeFromHandle && RemotingServices.CompareParameterList(arrayList, parameters2))
				{
					beginMethod = methodInfo;
				}
				else if (methodInfo.Name.Equals(text2) && methodInfo.ReturnType == returnType && RemotingServices.CompareParameterList(arrayList2, parameters2))
				{
					endMethod = methodInfo;
				}
			}
			return beginMethod != null && endMethod != null;
		}

		// Token: 0x0600566B RID: 22123 RVA: 0x0013289C File Offset: 0x00130A9C
		private static bool CompareParameterList(ArrayList params1, ParameterInfo[] params2)
		{
			if (params1.Count != params2.Length)
			{
				return false;
			}
			int num = 0;
			foreach (object obj in params1)
			{
				ParameterInfo parameterInfo = params2[num];
				ParameterInfo parameterInfo2 = obj as ParameterInfo;
				if (parameterInfo2 != null)
				{
					if (parameterInfo2.ParameterType != parameterInfo.ParameterType || parameterInfo2.IsIn != parameterInfo.IsIn || parameterInfo2.IsOut != parameterInfo.IsOut)
					{
						return false;
					}
				}
				else if ((Type)obj != parameterInfo.ParameterType && parameterInfo.IsIn)
				{
					return false;
				}
				num++;
			}
			return true;
		}

		// Token: 0x0600566C RID: 22124 RVA: 0x00132968 File Offset: 0x00130B68
		[SecurityCritical]
		public static Type GetServerTypeForUri(string URI)
		{
			Type type = null;
			if (URI != null)
			{
				ServerIdentity serverIdentity = (ServerIdentity)IdentityHolder.ResolveIdentity(URI);
				if (serverIdentity == null)
				{
					type = RemotingConfigHandler.GetServerTypeForUri(URI);
				}
				else
				{
					type = serverIdentity.ServerType;
				}
			}
			return type;
		}

		// Token: 0x0600566D RID: 22125 RVA: 0x0013299A File Offset: 0x00130B9A
		[SecurityCritical]
		internal static void DomainUnloaded(int domainID)
		{
			IdentityHolder.FlushIdentityTable();
			CrossAppDomainSink.DomainUnloaded(domainID);
		}

		// Token: 0x0600566E RID: 22126 RVA: 0x001329A8 File Offset: 0x00130BA8
		[SecurityCritical]
		internal static IntPtr GetServerContextForProxy(object tp)
		{
			ObjRef objRef = null;
			bool flag;
			int num;
			return RemotingServices.GetServerContextForProxy(tp, out objRef, out flag, out num);
		}

		// Token: 0x0600566F RID: 22127 RVA: 0x001329C4 File Offset: 0x00130BC4
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal static int GetServerDomainIdForProxy(object tp)
		{
			RealProxy realProxy = RemotingServices.GetRealProxy(tp);
			Identity identityObject = realProxy.IdentityObject;
			return identityObject.ObjectRef.GetServerDomainId();
		}

		// Token: 0x06005670 RID: 22128 RVA: 0x001329EC File Offset: 0x00130BEC
		[SecurityCritical]
		internal static void GetServerContextAndDomainIdForProxy(object tp, out IntPtr contextId, out int domainId)
		{
			ObjRef objRef;
			bool flag;
			contextId = RemotingServices.GetServerContextForProxy(tp, out objRef, out flag, out domainId);
		}

		// Token: 0x06005671 RID: 22129 RVA: 0x00132A08 File Offset: 0x00130C08
		[SecurityCritical]
		private static IntPtr GetServerContextForProxy(object tp, out ObjRef objRef, out bool bSameDomain, out int domainId)
		{
			IntPtr intPtr = IntPtr.Zero;
			objRef = null;
			bSameDomain = false;
			domainId = 0;
			if (RemotingServices.IsTransparentProxy(tp))
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(tp);
				Identity identityObject = realProxy.IdentityObject;
				if (identityObject != null)
				{
					ServerIdentity serverIdentity = identityObject as ServerIdentity;
					if (serverIdentity != null)
					{
						bSameDomain = true;
						intPtr = serverIdentity.ServerContext.InternalContextID;
						domainId = Thread.GetDomain().GetId();
					}
					else
					{
						objRef = identityObject.ObjectRef;
						if (objRef != null)
						{
							intPtr = objRef.GetServerContext(out domainId);
						}
						else
						{
							intPtr = IntPtr.Zero;
						}
					}
				}
				else
				{
					intPtr = Context.DefaultContext.InternalContextID;
				}
			}
			return intPtr;
		}

		// Token: 0x06005672 RID: 22130 RVA: 0x00132A90 File Offset: 0x00130C90
		[SecurityCritical]
		internal static Context GetServerContext(MarshalByRefObject obj)
		{
			Context context = null;
			if (!RemotingServices.IsTransparentProxy(obj) && obj is ContextBoundObject)
			{
				context = Thread.CurrentContext;
			}
			else
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(obj);
				Identity identityObject = realProxy.IdentityObject;
				ServerIdentity serverIdentity = identityObject as ServerIdentity;
				if (serverIdentity != null)
				{
					context = serverIdentity.ServerContext;
				}
			}
			return context;
		}

		// Token: 0x06005673 RID: 22131 RVA: 0x00132AD8 File Offset: 0x00130CD8
		[SecurityCritical]
		private static object GetType(object tp)
		{
			Type type = null;
			RealProxy realProxy = RemotingServices.GetRealProxy(tp);
			Identity identityObject = realProxy.IdentityObject;
			if (identityObject != null && identityObject.ObjectRef != null && identityObject.ObjectRef.TypeInfo != null)
			{
				IRemotingTypeInfo typeInfo = identityObject.ObjectRef.TypeInfo;
				string typeName = typeInfo.TypeName;
				if (typeName != null)
				{
					type = RemotingServices.InternalGetTypeFromQualifiedTypeName(typeName);
				}
			}
			return type;
		}

		// Token: 0x06005674 RID: 22132 RVA: 0x00132B30 File Offset: 0x00130D30
		[SecurityCritical]
		internal static byte[] MarshalToBuffer(object o, bool crossRuntime)
		{
			if (crossRuntime)
			{
				if (RemotingServices.IsTransparentProxy(o))
				{
					if (RemotingServices.GetRealProxy(o) is RemotingProxy && ChannelServices.RegisteredChannels.Length == 0)
					{
						return null;
					}
				}
				else
				{
					MarshalByRefObject marshalByRefObject = o as MarshalByRefObject;
					if (marshalByRefObject != null)
					{
						ProxyAttribute proxyAttribute = ActivationServices.GetProxyAttribute(marshalByRefObject.GetType());
						if (proxyAttribute == ActivationServices.DefaultProxyAttribute && ChannelServices.RegisteredChannels.Length == 0)
						{
							return null;
						}
					}
				}
			}
			MemoryStream memoryStream = new MemoryStream();
			RemotingSurrogateSelector remotingSurrogateSelector = new RemotingSurrogateSelector();
			new BinaryFormatter
			{
				SurrogateSelector = remotingSurrogateSelector,
				Context = new StreamingContext(StreamingContextStates.Other)
			}.Serialize(memoryStream, o, null, false);
			return memoryStream.GetBuffer();
		}

		// Token: 0x06005675 RID: 22133 RVA: 0x00132BC0 File Offset: 0x00130DC0
		[SecurityCritical]
		internal static object UnmarshalFromBuffer(byte[] b, bool crossRuntime)
		{
			MemoryStream memoryStream = new MemoryStream(b);
			object obj = new BinaryFormatter
			{
				AssemblyFormat = FormatterAssemblyStyle.Simple,
				SurrogateSelector = null,
				Context = new StreamingContext(StreamingContextStates.Other)
			}.Deserialize(memoryStream, null, false);
			if (crossRuntime && RemotingServices.IsTransparentProxy(obj))
			{
				if (!(RemotingServices.GetRealProxy(obj) is RemotingProxy))
				{
					return obj;
				}
				if (ChannelServices.RegisteredChannels.Length == 0)
				{
					return null;
				}
				obj.GetHashCode();
			}
			return obj;
		}

		// Token: 0x06005676 RID: 22134 RVA: 0x00132C2C File Offset: 0x00130E2C
		internal static object UnmarshalReturnMessageFromBuffer(byte[] b, IMethodCallMessage msg)
		{
			MemoryStream memoryStream = new MemoryStream(b);
			return new BinaryFormatter
			{
				SurrogateSelector = null,
				Context = new StreamingContext(StreamingContextStates.Other)
			}.DeserializeMethodResponse(memoryStream, null, msg);
		}

		// Token: 0x06005677 RID: 22135 RVA: 0x00132C68 File Offset: 0x00130E68
		[SecurityCritical]
		public static IMethodReturnMessage ExecuteMessage(MarshalByRefObject target, IMethodCallMessage reqMsg)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			RealProxy realProxy = RemotingServices.GetRealProxy(target);
			if (realProxy is RemotingProxy && !realProxy.DoContextsMatch())
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Proxy_WrongContext"));
			}
			StackBuilderSink stackBuilderSink = new StackBuilderSink(target);
			return (IMethodReturnMessage)stackBuilderSink.SyncProcessMessage(reqMsg);
		}

		// Token: 0x06005678 RID: 22136 RVA: 0x00132CC0 File Offset: 0x00130EC0
		[SecurityCritical]
		internal static string DetermineDefaultQualifiedTypeName(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			string text = null;
			string text2 = null;
			if (SoapServices.GetXmlTypeForInteropType(type, out text, out text2))
			{
				return "soap:" + text + ", " + text2;
			}
			return type.AssemblyQualifiedName;
		}

		// Token: 0x06005679 RID: 22137 RVA: 0x00132D0C File Offset: 0x00130F0C
		[SecurityCritical]
		internal static string GetDefaultQualifiedTypeName(RuntimeType type)
		{
			RemotingTypeCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(type);
			return reflectionCachedData.QualifiedTypeName;
		}

		// Token: 0x0600567A RID: 22138 RVA: 0x00132D28 File Offset: 0x00130F28
		internal static string InternalGetClrTypeNameFromQualifiedTypeName(string qualifiedTypeName)
		{
			if (qualifiedTypeName.Length > 4 && string.CompareOrdinal(qualifiedTypeName, 0, "clr:", 0, 4) == 0)
			{
				return qualifiedTypeName.Substring(4);
			}
			return null;
		}

		// Token: 0x0600567B RID: 22139 RVA: 0x00132D59 File Offset: 0x00130F59
		private static int IsSoapType(string qualifiedTypeName)
		{
			if (qualifiedTypeName.Length > 5 && string.CompareOrdinal(qualifiedTypeName, 0, "soap:", 0, 5) == 0)
			{
				return qualifiedTypeName.IndexOf(',', 5);
			}
			return -1;
		}

		// Token: 0x0600567C RID: 22140 RVA: 0x00132D80 File Offset: 0x00130F80
		[SecurityCritical]
		internal static string InternalGetSoapTypeNameFromQualifiedTypeName(string xmlTypeName, string xmlTypeNamespace)
		{
			string text;
			string text2;
			if (!SoapServices.DecodeXmlNamespaceForClrTypeNamespace(xmlTypeNamespace, out text, out text2))
			{
				return null;
			}
			string text3;
			if (text != null && text.Length > 0)
			{
				text3 = text + "." + xmlTypeName;
			}
			else
			{
				text3 = xmlTypeName;
			}
			try
			{
				return text3 + ", " + text2;
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x0600567D RID: 22141 RVA: 0x00132DE4 File Offset: 0x00130FE4
		[SecurityCritical]
		internal static string InternalGetTypeNameFromQualifiedTypeName(string qualifiedTypeName)
		{
			if (qualifiedTypeName == null)
			{
				throw new ArgumentNullException("qualifiedTypeName");
			}
			string text = RemotingServices.InternalGetClrTypeNameFromQualifiedTypeName(qualifiedTypeName);
			if (text != null)
			{
				return text;
			}
			int num = RemotingServices.IsSoapType(qualifiedTypeName);
			if (num != -1)
			{
				string text2 = qualifiedTypeName.Substring(5, num - 5);
				string text3 = qualifiedTypeName.Substring(num + 2, qualifiedTypeName.Length - (num + 2));
				text = RemotingServices.InternalGetSoapTypeNameFromQualifiedTypeName(text2, text3);
				if (text != null)
				{
					return text;
				}
			}
			return qualifiedTypeName;
		}

		// Token: 0x0600567E RID: 22142 RVA: 0x00132E44 File Offset: 0x00131044
		[SecurityCritical]
		internal static RuntimeType InternalGetTypeFromQualifiedTypeName(string qualifiedTypeName, bool partialFallback)
		{
			if (qualifiedTypeName == null)
			{
				throw new ArgumentNullException("qualifiedTypeName");
			}
			string text = RemotingServices.InternalGetClrTypeNameFromQualifiedTypeName(qualifiedTypeName);
			if (text != null)
			{
				return RemotingServices.LoadClrTypeWithPartialBindFallback(text, partialFallback);
			}
			int num = RemotingServices.IsSoapType(qualifiedTypeName);
			if (num != -1)
			{
				string text2 = qualifiedTypeName.Substring(5, num - 5);
				string text3 = qualifiedTypeName.Substring(num + 2, qualifiedTypeName.Length - (num + 2));
				RuntimeType runtimeType = (RuntimeType)SoapServices.GetInteropTypeFromXmlType(text2, text3);
				if (runtimeType != null)
				{
					return runtimeType;
				}
				text = RemotingServices.InternalGetSoapTypeNameFromQualifiedTypeName(text2, text3);
				if (text != null)
				{
					return RemotingServices.LoadClrTypeWithPartialBindFallback(text, true);
				}
			}
			return RemotingServices.LoadClrTypeWithPartialBindFallback(qualifiedTypeName, partialFallback);
		}

		// Token: 0x0600567F RID: 22143 RVA: 0x00132ED0 File Offset: 0x001310D0
		[SecurityCritical]
		internal static Type InternalGetTypeFromQualifiedTypeName(string qualifiedTypeName)
		{
			return RemotingServices.InternalGetTypeFromQualifiedTypeName(qualifiedTypeName, true);
		}

		// Token: 0x06005680 RID: 22144 RVA: 0x00132EDC File Offset: 0x001310DC
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static RuntimeType LoadClrTypeWithPartialBindFallback(string typeName, bool partialFallback)
		{
			if (!partialFallback)
			{
				return (RuntimeType)Type.GetType(typeName, false);
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeTypeHandle.GetTypeByName(typeName, false, false, false, ref stackCrawlMark, true);
		}

		// Token: 0x06005681 RID: 22145
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool CORProfilerTrackRemoting();

		// Token: 0x06005682 RID: 22146
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool CORProfilerTrackRemotingCookie();

		// Token: 0x06005683 RID: 22147
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool CORProfilerTrackRemotingAsync();

		// Token: 0x06005684 RID: 22148
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void CORProfilerRemotingClientSendingMessage(out Guid id, bool fIsAsync);

		// Token: 0x06005685 RID: 22149
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void CORProfilerRemotingClientReceivingReply(Guid id, bool fIsAsync);

		// Token: 0x06005686 RID: 22150
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void CORProfilerRemotingServerReceivingMessage(Guid id, bool fIsAsync);

		// Token: 0x06005687 RID: 22151
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void CORProfilerRemotingServerSendingReply(out Guid id, bool fIsAsync);

		// Token: 0x06005688 RID: 22152 RVA: 0x00132F07 File Offset: 0x00131107
		[SecurityCritical]
		[Conditional("REMOTING_PERF")]
		[Obsolete("Use of this method is not recommended. The LogRemotingStage existed for internal diagnostic purposes only.")]
		public static void LogRemotingStage(int stage)
		{
		}

		// Token: 0x06005689 RID: 22153
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ResetInterfaceCache(object proxy);

		// Token: 0x0600568A RID: 22154 RVA: 0x00132F09 File Offset: 0x00131109
		// Note: this type is marked as 'beforefieldinit'.
		static RemotingServices()
		{
		}

		// Token: 0x04002799 RID: 10137
		private const BindingFlags LookupAll = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x0400279A RID: 10138
		private const string FieldGetterName = "FieldGetter";

		// Token: 0x0400279B RID: 10139
		private const string FieldSetterName = "FieldSetter";

		// Token: 0x0400279C RID: 10140
		private const string IsInstanceOfTypeName = "IsInstanceOfType";

		// Token: 0x0400279D RID: 10141
		private const string CanCastToXmlTypeName = "CanCastToXmlType";

		// Token: 0x0400279E RID: 10142
		private const string InvokeMemberName = "InvokeMember";

		// Token: 0x0400279F RID: 10143
		private static volatile MethodBase s_FieldGetterMB;

		// Token: 0x040027A0 RID: 10144
		private static volatile MethodBase s_FieldSetterMB;

		// Token: 0x040027A1 RID: 10145
		private static volatile MethodBase s_IsInstanceOfTypeMB;

		// Token: 0x040027A2 RID: 10146
		private static volatile MethodBase s_CanCastToXmlTypeMB;

		// Token: 0x040027A3 RID: 10147
		private static volatile MethodBase s_InvokeMemberMB;

		// Token: 0x040027A4 RID: 10148
		private static volatile bool s_bRemoteActivationConfigured;

		// Token: 0x040027A5 RID: 10149
		private static volatile bool s_bRegisteredWellKnownChannels;

		// Token: 0x040027A6 RID: 10150
		private static bool s_bInProcessOfRegisteringWellKnownChannels;

		// Token: 0x040027A7 RID: 10151
		private static readonly object s_delayLoadChannelLock = new object();
	}
}
