using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Serialization;
using System.Security;

namespace System.Reflection
{
	// Token: 0x020005E5 RID: 1509
	[Serializable]
	internal sealed class RuntimeEventInfo : EventInfo, ISerializable
	{
		// Token: 0x060045D7 RID: 17879 RVA: 0x0010125A File Offset: 0x000FF45A
		internal RuntimeEventInfo()
		{
		}

		// Token: 0x060045D8 RID: 17880 RVA: 0x00101264 File Offset: 0x000FF464
		[SecurityCritical]
		internal RuntimeEventInfo(int tkEvent, RuntimeType declaredType, RuntimeType.RuntimeTypeCache reflectedTypeCache, out bool isPrivate)
		{
			MetadataImport metadataImport = declaredType.GetRuntimeModule().MetadataImport;
			this.m_token = tkEvent;
			this.m_reflectedTypeCache = reflectedTypeCache;
			this.m_declaringType = declaredType;
			RuntimeType runtimeType = reflectedTypeCache.GetRuntimeType();
			metadataImport.GetEventProps(tkEvent, out this.m_utf8name, out this.m_flags);
			RuntimeMethodInfo runtimeMethodInfo;
			Associates.AssignAssociates(metadataImport, tkEvent, declaredType, runtimeType, out this.m_addMethod, out this.m_removeMethod, out this.m_raiseMethod, out runtimeMethodInfo, out runtimeMethodInfo, out this.m_otherMethod, out isPrivate, out this.m_bindingFlags);
		}

		// Token: 0x060045D9 RID: 17881 RVA: 0x001012E0 File Offset: 0x000FF4E0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal override bool CacheEquals(object o)
		{
			RuntimeEventInfo runtimeEventInfo = o as RuntimeEventInfo;
			return runtimeEventInfo != null && runtimeEventInfo.m_token == this.m_token && RuntimeTypeHandle.GetModule(this.m_declaringType).Equals(RuntimeTypeHandle.GetModule(runtimeEventInfo.m_declaringType));
		}

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x060045DA RID: 17882 RVA: 0x00101324 File Offset: 0x000FF524
		internal BindingFlags BindingFlags
		{
			get
			{
				return this.m_bindingFlags;
			}
		}

		// Token: 0x060045DB RID: 17883 RVA: 0x0010132C File Offset: 0x000FF52C
		public override string ToString()
		{
			if (this.m_addMethod == null || this.m_addMethod.GetParametersNoCopy().Length == 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NoPublicAddMethod"));
			}
			return this.m_addMethod.GetParametersNoCopy()[0].ParameterType.FormatTypeName() + " " + this.Name;
		}

		// Token: 0x060045DC RID: 17884 RVA: 0x0010138C File Offset: 0x000FF58C
		public override object[] GetCustomAttributes(bool inherit)
		{
			return CustomAttribute.GetCustomAttributes(this, typeof(object) as RuntimeType);
		}

		// Token: 0x060045DD RID: 17885 RVA: 0x001013A4 File Offset: 0x000FF5A4
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "attributeType");
			}
			return CustomAttribute.GetCustomAttributes(this, runtimeType);
		}

		// Token: 0x060045DE RID: 17886 RVA: 0x001013F8 File Offset: 0x000FF5F8
		[SecuritySafeCritical]
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "attributeType");
			}
			return CustomAttribute.IsDefined(this, runtimeType);
		}

		// Token: 0x060045DF RID: 17887 RVA: 0x0010144A File Offset: 0x000FF64A
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributesInternal(this);
		}

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x060045E0 RID: 17888 RVA: 0x00101452 File Offset: 0x000FF652
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Event;
			}
		}

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x060045E1 RID: 17889 RVA: 0x00101458 File Offset: 0x000FF658
		public override string Name
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_name == null)
				{
					this.m_name = new Utf8String(this.m_utf8name).ToString();
				}
				return this.m_name;
			}
		}

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x060045E2 RID: 17890 RVA: 0x00101492 File Offset: 0x000FF692
		public override Type DeclaringType
		{
			get
			{
				return this.m_declaringType;
			}
		}

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x060045E3 RID: 17891 RVA: 0x0010149A File Offset: 0x000FF69A
		public override Type ReflectedType
		{
			get
			{
				return this.ReflectedTypeInternal;
			}
		}

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x060045E4 RID: 17892 RVA: 0x001014A2 File Offset: 0x000FF6A2
		private RuntimeType ReflectedTypeInternal
		{
			get
			{
				return this.m_reflectedTypeCache.GetRuntimeType();
			}
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x060045E5 RID: 17893 RVA: 0x001014AF File Offset: 0x000FF6AF
		public override int MetadataToken
		{
			get
			{
				return this.m_token;
			}
		}

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x060045E6 RID: 17894 RVA: 0x001014B7 File Offset: 0x000FF6B7
		public override Module Module
		{
			get
			{
				return this.GetRuntimeModule();
			}
		}

		// Token: 0x060045E7 RID: 17895 RVA: 0x001014BF File Offset: 0x000FF6BF
		internal RuntimeModule GetRuntimeModule()
		{
			return this.m_declaringType.GetRuntimeModule();
		}

		// Token: 0x060045E8 RID: 17896 RVA: 0x001014CC File Offset: 0x000FF6CC
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			MemberInfoSerializationHolder.GetSerializationInfo(info, this.Name, this.ReflectedTypeInternal, null, MemberTypes.Event);
		}

		// Token: 0x060045E9 RID: 17897 RVA: 0x001014F0 File Offset: 0x000FF6F0
		public override MethodInfo[] GetOtherMethods(bool nonPublic)
		{
			List<MethodInfo> list = new List<MethodInfo>();
			if (this.m_otherMethod == null)
			{
				return new MethodInfo[0];
			}
			for (int i = 0; i < this.m_otherMethod.Length; i++)
			{
				if (Associates.IncludeAccessor(this.m_otherMethod[i], nonPublic))
				{
					list.Add(this.m_otherMethod[i]);
				}
			}
			return list.ToArray();
		}

		// Token: 0x060045EA RID: 17898 RVA: 0x00101549 File Offset: 0x000FF749
		public override MethodInfo GetAddMethod(bool nonPublic)
		{
			if (!Associates.IncludeAccessor(this.m_addMethod, nonPublic))
			{
				return null;
			}
			return this.m_addMethod;
		}

		// Token: 0x060045EB RID: 17899 RVA: 0x00101561 File Offset: 0x000FF761
		public override MethodInfo GetRemoveMethod(bool nonPublic)
		{
			if (!Associates.IncludeAccessor(this.m_removeMethod, nonPublic))
			{
				return null;
			}
			return this.m_removeMethod;
		}

		// Token: 0x060045EC RID: 17900 RVA: 0x00101579 File Offset: 0x000FF779
		public override MethodInfo GetRaiseMethod(bool nonPublic)
		{
			if (!Associates.IncludeAccessor(this.m_raiseMethod, nonPublic))
			{
				return null;
			}
			return this.m_raiseMethod;
		}

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x060045ED RID: 17901 RVA: 0x00101591 File Offset: 0x000FF791
		public override EventAttributes Attributes
		{
			get
			{
				return this.m_flags;
			}
		}

		// Token: 0x04001CA1 RID: 7329
		private int m_token;

		// Token: 0x04001CA2 RID: 7330
		private EventAttributes m_flags;

		// Token: 0x04001CA3 RID: 7331
		private string m_name;

		// Token: 0x04001CA4 RID: 7332
		[SecurityCritical]
		private unsafe void* m_utf8name;

		// Token: 0x04001CA5 RID: 7333
		private RuntimeType.RuntimeTypeCache m_reflectedTypeCache;

		// Token: 0x04001CA6 RID: 7334
		private RuntimeMethodInfo m_addMethod;

		// Token: 0x04001CA7 RID: 7335
		private RuntimeMethodInfo m_removeMethod;

		// Token: 0x04001CA8 RID: 7336
		private RuntimeMethodInfo m_raiseMethod;

		// Token: 0x04001CA9 RID: 7337
		private MethodInfo[] m_otherMethod;

		// Token: 0x04001CAA RID: 7338
		private RuntimeType m_declaringType;

		// Token: 0x04001CAB RID: 7339
		private BindingFlags m_bindingFlags;
	}
}
