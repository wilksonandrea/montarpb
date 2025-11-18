using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.Remoting
{
	// Token: 0x020007CD RID: 1997
	[SecurityCritical]
	[ComVisible(true)]
	public class InternalRemotingServices
	{
		// Token: 0x0600568B RID: 22155 RVA: 0x00132F15 File Offset: 0x00131115
		[SecurityCritical]
		[Conditional("_LOGGING")]
		public static void DebugOutChnl(string s)
		{
			Message.OutToUnmanagedDebugger("CHNL:" + s + "\n");
		}

		// Token: 0x0600568C RID: 22156 RVA: 0x00132F2C File Offset: 0x0013112C
		[Conditional("_LOGGING")]
		public static void RemotingTrace(params object[] messages)
		{
		}

		// Token: 0x0600568D RID: 22157 RVA: 0x00132F2E File Offset: 0x0013112E
		[Conditional("_DEBUG")]
		public static void RemotingAssert(bool condition, string message)
		{
		}

		// Token: 0x0600568E RID: 22158 RVA: 0x00132F30 File Offset: 0x00131130
		[SecurityCritical]
		[CLSCompliant(false)]
		public static void SetServerIdentity(MethodCall m, object srvID)
		{
			((IInternalMessage)m).ServerIdentityObject = (ServerIdentity)srvID;
		}

		// Token: 0x0600568F RID: 22159 RVA: 0x00132F4C File Offset: 0x0013114C
		internal static RemotingMethodCachedData GetReflectionCachedData(MethodBase mi)
		{
			RuntimeMethodInfo runtimeMethodInfo;
			if ((runtimeMethodInfo = mi as RuntimeMethodInfo) != null)
			{
				return runtimeMethodInfo.RemotingCache;
			}
			RuntimeConstructorInfo runtimeConstructorInfo;
			if ((runtimeConstructorInfo = mi as RuntimeConstructorInfo) != null)
			{
				return runtimeConstructorInfo.RemotingCache;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeReflectionObject"));
		}

		// Token: 0x06005690 RID: 22160 RVA: 0x00132F9A File Offset: 0x0013119A
		internal static RemotingTypeCachedData GetReflectionCachedData(RuntimeType type)
		{
			return type.RemotingCache;
		}

		// Token: 0x06005691 RID: 22161 RVA: 0x00132FA4 File Offset: 0x001311A4
		internal static RemotingCachedData GetReflectionCachedData(MemberInfo mi)
		{
			MethodBase methodBase;
			if ((methodBase = mi as MethodBase) != null)
			{
				return InternalRemotingServices.GetReflectionCachedData(methodBase);
			}
			RuntimeType runtimeType;
			if ((runtimeType = mi as RuntimeType) != null)
			{
				return InternalRemotingServices.GetReflectionCachedData(runtimeType);
			}
			RuntimeFieldInfo runtimeFieldInfo;
			if ((runtimeFieldInfo = mi as RuntimeFieldInfo) != null)
			{
				return runtimeFieldInfo.RemotingCache;
			}
			SerializationFieldInfo serializationFieldInfo;
			if ((serializationFieldInfo = mi as SerializationFieldInfo) != null)
			{
				return serializationFieldInfo.RemotingCache;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeReflectionObject"));
		}

		// Token: 0x06005692 RID: 22162 RVA: 0x00133024 File Offset: 0x00131224
		internal static RemotingCachedData GetReflectionCachedData(RuntimeParameterInfo reflectionObject)
		{
			return reflectionObject.RemotingCache;
		}

		// Token: 0x06005693 RID: 22163 RVA: 0x0013302C File Offset: 0x0013122C
		[SecurityCritical]
		public static SoapAttribute GetCachedSoapAttribute(object reflectionObject)
		{
			MemberInfo memberInfo = reflectionObject as MemberInfo;
			RuntimeParameterInfo runtimeParameterInfo = reflectionObject as RuntimeParameterInfo;
			if (memberInfo != null)
			{
				return InternalRemotingServices.GetReflectionCachedData(memberInfo).GetSoapAttribute();
			}
			if (runtimeParameterInfo != null)
			{
				return InternalRemotingServices.GetReflectionCachedData(runtimeParameterInfo).GetSoapAttribute();
			}
			return null;
		}

		// Token: 0x06005694 RID: 22164 RVA: 0x0013306C File Offset: 0x0013126C
		public InternalRemotingServices()
		{
		}
	}
}
