using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Reflection
{
	// Token: 0x020005E8 RID: 1512
	[Serializable]
	internal abstract class RuntimeFieldInfo : FieldInfo, ISerializable
	{
		// Token: 0x06004615 RID: 17941 RVA: 0x001017FF File Offset: 0x000FF9FF
		protected RuntimeFieldInfo()
		{
		}

		// Token: 0x06004616 RID: 17942 RVA: 0x00101807 File Offset: 0x000FFA07
		protected RuntimeFieldInfo(RuntimeType.RuntimeTypeCache reflectedTypeCache, RuntimeType declaringType, BindingFlags bindingFlags)
		{
			this.m_bindingFlags = bindingFlags;
			this.m_declaringType = declaringType;
			this.m_reflectedTypeCache = reflectedTypeCache;
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x06004617 RID: 17943 RVA: 0x00101824 File Offset: 0x000FFA24
		internal RemotingFieldCachedData RemotingCache
		{
			get
			{
				RemotingFieldCachedData remotingFieldCachedData = this.m_cachedData;
				if (remotingFieldCachedData == null)
				{
					remotingFieldCachedData = new RemotingFieldCachedData(this);
					RemotingFieldCachedData remotingFieldCachedData2 = Interlocked.CompareExchange<RemotingFieldCachedData>(ref this.m_cachedData, remotingFieldCachedData, null);
					if (remotingFieldCachedData2 != null)
					{
						remotingFieldCachedData = remotingFieldCachedData2;
					}
				}
				return remotingFieldCachedData;
			}
		}

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x06004618 RID: 17944 RVA: 0x00101856 File Offset: 0x000FFA56
		internal BindingFlags BindingFlags
		{
			get
			{
				return this.m_bindingFlags;
			}
		}

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x06004619 RID: 17945 RVA: 0x0010185E File Offset: 0x000FFA5E
		private RuntimeType ReflectedTypeInternal
		{
			get
			{
				return this.m_reflectedTypeCache.GetRuntimeType();
			}
		}

		// Token: 0x0600461A RID: 17946 RVA: 0x0010186B File Offset: 0x000FFA6B
		internal RuntimeType GetDeclaringTypeInternal()
		{
			return this.m_declaringType;
		}

		// Token: 0x0600461B RID: 17947 RVA: 0x00101873 File Offset: 0x000FFA73
		internal RuntimeType GetRuntimeType()
		{
			return this.m_declaringType;
		}

		// Token: 0x0600461C RID: 17948
		internal abstract RuntimeModule GetRuntimeModule();

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x0600461D RID: 17949 RVA: 0x0010187B File Offset: 0x000FFA7B
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Field;
			}
		}

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x0600461E RID: 17950 RVA: 0x0010187E File Offset: 0x000FFA7E
		public override Type ReflectedType
		{
			get
			{
				if (!this.m_reflectedTypeCache.IsGlobal)
				{
					return this.ReflectedTypeInternal;
				}
				return null;
			}
		}

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x0600461F RID: 17951 RVA: 0x00101895 File Offset: 0x000FFA95
		public override Type DeclaringType
		{
			get
			{
				if (!this.m_reflectedTypeCache.IsGlobal)
				{
					return this.m_declaringType;
				}
				return null;
			}
		}

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x06004620 RID: 17952 RVA: 0x001018AC File Offset: 0x000FFAAC
		public override Module Module
		{
			get
			{
				return this.GetRuntimeModule();
			}
		}

		// Token: 0x06004621 RID: 17953 RVA: 0x001018B4 File Offset: 0x000FFAB4
		public override string ToString()
		{
			if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
			{
				return this.FieldType.ToString() + " " + this.Name;
			}
			return this.FieldType.FormatTypeName() + " " + this.Name;
		}

		// Token: 0x06004622 RID: 17954 RVA: 0x001018F4 File Offset: 0x000FFAF4
		public override object[] GetCustomAttributes(bool inherit)
		{
			return CustomAttribute.GetCustomAttributes(this, typeof(object) as RuntimeType);
		}

		// Token: 0x06004623 RID: 17955 RVA: 0x0010190C File Offset: 0x000FFB0C
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

		// Token: 0x06004624 RID: 17956 RVA: 0x00101960 File Offset: 0x000FFB60
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

		// Token: 0x06004625 RID: 17957 RVA: 0x001019B2 File Offset: 0x000FFBB2
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributesInternal(this);
		}

		// Token: 0x06004626 RID: 17958 RVA: 0x001019BA File Offset: 0x000FFBBA
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			MemberInfoSerializationHolder.GetSerializationInfo(info, this.Name, this.ReflectedTypeInternal, this.ToString(), MemberTypes.Field);
		}

		// Token: 0x04001CC0 RID: 7360
		private BindingFlags m_bindingFlags;

		// Token: 0x04001CC1 RID: 7361
		protected RuntimeType.RuntimeTypeCache m_reflectedTypeCache;

		// Token: 0x04001CC2 RID: 7362
		protected RuntimeType m_declaringType;

		// Token: 0x04001CC3 RID: 7363
		private RemotingFieldCachedData m_cachedData;
	}
}
