using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Reflection
{
	// Token: 0x02000619 RID: 1561
	[Serializable]
	internal sealed class RuntimeParameterInfo : ParameterInfo, ISerializable
	{
		// Token: 0x06004835 RID: 18485 RVA: 0x00106178 File Offset: 0x00104378
		[SecurityCritical]
		internal static ParameterInfo[] GetParameters(IRuntimeMethodInfo method, MemberInfo member, Signature sig)
		{
			ParameterInfo parameterInfo;
			return RuntimeParameterInfo.GetParameters(method, member, sig, out parameterInfo, false);
		}

		// Token: 0x06004836 RID: 18486 RVA: 0x00106190 File Offset: 0x00104390
		[SecurityCritical]
		internal static ParameterInfo GetReturnParameter(IRuntimeMethodInfo method, MemberInfo member, Signature sig)
		{
			ParameterInfo parameterInfo;
			RuntimeParameterInfo.GetParameters(method, member, sig, out parameterInfo, true);
			return parameterInfo;
		}

		// Token: 0x06004837 RID: 18487 RVA: 0x001061AC File Offset: 0x001043AC
		[SecurityCritical]
		internal static ParameterInfo[] GetParameters(IRuntimeMethodInfo methodHandle, MemberInfo member, Signature sig, out ParameterInfo returnParameter, bool fetchReturnParameter)
		{
			returnParameter = null;
			int num = sig.Arguments.Length;
			ParameterInfo[] array = (fetchReturnParameter ? null : new ParameterInfo[num]);
			int methodDef = RuntimeMethodHandle.GetMethodDef(methodHandle);
			int num2 = 0;
			if (!System.Reflection.MetadataToken.IsNullToken(methodDef))
			{
				MetadataImport metadataImport = RuntimeTypeHandle.GetMetadataImport(RuntimeMethodHandle.GetDeclaringType(methodHandle));
				MetadataEnumResult metadataEnumResult;
				metadataImport.EnumParams(methodDef, out metadataEnumResult);
				num2 = metadataEnumResult.Length;
				if (num2 > num + 1)
				{
					throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ParameterSignatureMismatch"));
				}
				for (int i = 0; i < num2; i++)
				{
					int num3 = metadataEnumResult[i];
					int num4;
					ParameterAttributes parameterAttributes;
					metadataImport.GetParamDefProps(num3, out num4, out parameterAttributes);
					num4--;
					if (fetchReturnParameter && num4 == -1)
					{
						if (returnParameter != null)
						{
							throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ParameterSignatureMismatch"));
						}
						returnParameter = new RuntimeParameterInfo(sig, metadataImport, num3, num4, parameterAttributes, member);
					}
					else if (!fetchReturnParameter && num4 >= 0)
					{
						if (num4 >= num)
						{
							throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ParameterSignatureMismatch"));
						}
						array[num4] = new RuntimeParameterInfo(sig, metadataImport, num3, num4, parameterAttributes, member);
					}
				}
			}
			if (fetchReturnParameter)
			{
				if (returnParameter == null)
				{
					returnParameter = new RuntimeParameterInfo(sig, MetadataImport.EmptyImport, 0, -1, ParameterAttributes.None, member);
				}
			}
			else if (num2 < array.Length + 1)
			{
				for (int j = 0; j < array.Length; j++)
				{
					if (array[j] == null)
					{
						array[j] = new RuntimeParameterInfo(sig, MetadataImport.EmptyImport, 0, j, ParameterAttributes.None, member);
					}
				}
			}
			return array;
		}

		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x06004838 RID: 18488 RVA: 0x00106304 File Offset: 0x00104504
		internal MethodBase DefiningMethod
		{
			get
			{
				return (this.m_originalMember != null) ? this.m_originalMember : (this.MemberImpl as MethodBase);
			}
		}

		// Token: 0x06004839 RID: 18489 RVA: 0x00106334 File Offset: 0x00104534
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.SetType(typeof(ParameterInfo));
			info.AddValue("AttrsImpl", this.Attributes);
			info.AddValue("ClassImpl", this.ParameterType);
			info.AddValue("DefaultValueImpl", this.DefaultValue);
			info.AddValue("MemberImpl", this.Member);
			info.AddValue("NameImpl", this.Name);
			info.AddValue("PositionImpl", this.Position);
			info.AddValue("_token", this.m_tkParamDef);
		}

		// Token: 0x0600483A RID: 18490 RVA: 0x001063DB File Offset: 0x001045DB
		internal RuntimeParameterInfo(RuntimeParameterInfo accessor, RuntimePropertyInfo property)
			: this(accessor, property)
		{
			this.m_signature = property.Signature;
		}

		// Token: 0x0600483B RID: 18491 RVA: 0x001063F4 File Offset: 0x001045F4
		private RuntimeParameterInfo(RuntimeParameterInfo accessor, MemberInfo member)
		{
			this.MemberImpl = member;
			this.m_originalMember = accessor.MemberImpl as MethodBase;
			this.NameImpl = accessor.Name;
			this.m_nameIsCached = true;
			this.ClassImpl = accessor.ParameterType;
			this.PositionImpl = accessor.Position;
			this.AttrsImpl = accessor.Attributes;
			this.m_tkParamDef = (System.Reflection.MetadataToken.IsNullToken(accessor.MetadataToken) ? 134217728 : accessor.MetadataToken);
			this.m_scope = accessor.m_scope;
		}

		// Token: 0x0600483C RID: 18492 RVA: 0x00106484 File Offset: 0x00104684
		private RuntimeParameterInfo(Signature signature, MetadataImport scope, int tkParamDef, int position, ParameterAttributes attributes, MemberInfo member)
		{
			this.PositionImpl = position;
			this.MemberImpl = member;
			this.m_signature = signature;
			this.m_tkParamDef = (System.Reflection.MetadataToken.IsNullToken(tkParamDef) ? 134217728 : tkParamDef);
			this.m_scope = scope;
			this.AttrsImpl = attributes;
			this.ClassImpl = null;
			this.NameImpl = null;
		}

		// Token: 0x0600483D RID: 18493 RVA: 0x001064E4 File Offset: 0x001046E4
		internal RuntimeParameterInfo(MethodInfo owner, string name, Type parameterType, int position)
		{
			this.MemberImpl = owner;
			this.NameImpl = name;
			this.m_nameIsCached = true;
			this.m_noMetadata = true;
			this.ClassImpl = parameterType;
			this.PositionImpl = position;
			this.AttrsImpl = ParameterAttributes.None;
			this.m_tkParamDef = 134217728;
			this.m_scope = MetadataImport.EmptyImport;
		}

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x0600483E RID: 18494 RVA: 0x00106544 File Offset: 0x00104744
		public override Type ParameterType
		{
			get
			{
				if (this.ClassImpl == null)
				{
					RuntimeType runtimeType;
					if (this.PositionImpl == -1)
					{
						runtimeType = this.m_signature.ReturnType;
					}
					else
					{
						runtimeType = this.m_signature.Arguments[this.PositionImpl];
					}
					this.ClassImpl = runtimeType;
				}
				return this.ClassImpl;
			}
		}

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x0600483F RID: 18495 RVA: 0x00106598 File Offset: 0x00104798
		public override string Name
		{
			[SecuritySafeCritical]
			get
			{
				if (!this.m_nameIsCached)
				{
					if (!System.Reflection.MetadataToken.IsNullToken(this.m_tkParamDef))
					{
						string text = this.m_scope.GetName(this.m_tkParamDef).ToString();
						this.NameImpl = text;
					}
					this.m_nameIsCached = true;
				}
				return this.NameImpl;
			}
		}

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x06004840 RID: 18496 RVA: 0x001065F4 File Offset: 0x001047F4
		public override bool HasDefaultValue
		{
			get
			{
				if (this.m_noMetadata || this.m_noDefaultValue)
				{
					return false;
				}
				object defaultValueInternal = this.GetDefaultValueInternal(false);
				return defaultValueInternal != DBNull.Value;
			}
		}

		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x06004841 RID: 18497 RVA: 0x00106626 File Offset: 0x00104826
		public override object DefaultValue
		{
			get
			{
				return this.GetDefaultValue(false);
			}
		}

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x06004842 RID: 18498 RVA: 0x0010662F File Offset: 0x0010482F
		public override object RawDefaultValue
		{
			get
			{
				return this.GetDefaultValue(true);
			}
		}

		// Token: 0x06004843 RID: 18499 RVA: 0x00106638 File Offset: 0x00104838
		private object GetDefaultValue(bool raw)
		{
			if (this.m_noMetadata)
			{
				return null;
			}
			object obj = this.GetDefaultValueInternal(raw);
			if (obj == DBNull.Value && base.IsOptional)
			{
				obj = Type.Missing;
			}
			return obj;
		}

		// Token: 0x06004844 RID: 18500 RVA: 0x00106670 File Offset: 0x00104870
		[SecuritySafeCritical]
		private object GetDefaultValueInternal(bool raw)
		{
			if (this.m_noDefaultValue)
			{
				return DBNull.Value;
			}
			object obj = null;
			if (this.ParameterType == typeof(DateTime))
			{
				if (raw)
				{
					CustomAttributeTypedArgument customAttributeTypedArgument = CustomAttributeData.Filter(CustomAttributeData.GetCustomAttributes(this), typeof(DateTimeConstantAttribute), 0);
					if (customAttributeTypedArgument.ArgumentType != null)
					{
						return new DateTime((long)customAttributeTypedArgument.Value);
					}
				}
				else
				{
					object[] customAttributes = this.GetCustomAttributes(typeof(DateTimeConstantAttribute), false);
					if (customAttributes != null && customAttributes.Length != 0)
					{
						return ((DateTimeConstantAttribute)customAttributes[0]).Value;
					}
				}
			}
			if (!System.Reflection.MetadataToken.IsNullToken(this.m_tkParamDef))
			{
				obj = MdConstant.GetValue(this.m_scope, this.m_tkParamDef, this.ParameterType.GetTypeHandleInternal(), raw);
			}
			if (obj == DBNull.Value)
			{
				if (raw)
				{
					using (IEnumerator<CustomAttributeData> enumerator = CustomAttributeData.GetCustomAttributes(this).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							CustomAttributeData customAttributeData = enumerator.Current;
							Type declaringType = customAttributeData.Constructor.DeclaringType;
							if (declaringType == typeof(DateTimeConstantAttribute))
							{
								obj = DateTimeConstantAttribute.GetRawDateTimeConstant(customAttributeData);
							}
							else if (declaringType == typeof(DecimalConstantAttribute))
							{
								obj = DecimalConstantAttribute.GetRawDecimalConstant(customAttributeData);
							}
							else if (declaringType.IsSubclassOf(RuntimeParameterInfo.s_CustomConstantAttributeType))
							{
								obj = CustomConstantAttribute.GetRawConstant(customAttributeData);
							}
						}
						goto IL_1A7;
					}
				}
				object[] array = this.GetCustomAttributes(RuntimeParameterInfo.s_CustomConstantAttributeType, false);
				if (array.Length != 0)
				{
					obj = ((CustomConstantAttribute)array[0]).Value;
				}
				else
				{
					array = this.GetCustomAttributes(RuntimeParameterInfo.s_DecimalConstantAttributeType, false);
					if (array.Length != 0)
					{
						obj = ((DecimalConstantAttribute)array[0]).Value;
					}
				}
			}
			IL_1A7:
			if (obj == DBNull.Value)
			{
				this.m_noDefaultValue = true;
			}
			return obj;
		}

		// Token: 0x06004845 RID: 18501 RVA: 0x00106844 File Offset: 0x00104A44
		internal RuntimeModule GetRuntimeModule()
		{
			RuntimeMethodInfo runtimeMethodInfo = this.Member as RuntimeMethodInfo;
			RuntimeConstructorInfo runtimeConstructorInfo = this.Member as RuntimeConstructorInfo;
			RuntimePropertyInfo runtimePropertyInfo = this.Member as RuntimePropertyInfo;
			if (runtimeMethodInfo != null)
			{
				return runtimeMethodInfo.GetRuntimeModule();
			}
			if (runtimeConstructorInfo != null)
			{
				return runtimeConstructorInfo.GetRuntimeModule();
			}
			if (runtimePropertyInfo != null)
			{
				return runtimePropertyInfo.GetRuntimeModule();
			}
			return null;
		}

		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x06004846 RID: 18502 RVA: 0x001068A6 File Offset: 0x00104AA6
		public override int MetadataToken
		{
			get
			{
				return this.m_tkParamDef;
			}
		}

		// Token: 0x06004847 RID: 18503 RVA: 0x001068AE File Offset: 0x00104AAE
		public override Type[] GetRequiredCustomModifiers()
		{
			return this.m_signature.GetCustomModifiers(this.PositionImpl + 1, true);
		}

		// Token: 0x06004848 RID: 18504 RVA: 0x001068C4 File Offset: 0x00104AC4
		public override Type[] GetOptionalCustomModifiers()
		{
			return this.m_signature.GetCustomModifiers(this.PositionImpl + 1, false);
		}

		// Token: 0x06004849 RID: 18505 RVA: 0x001068DA File Offset: 0x00104ADA
		public override object[] GetCustomAttributes(bool inherit)
		{
			if (System.Reflection.MetadataToken.IsNullToken(this.m_tkParamDef))
			{
				return EmptyArray<object>.Value;
			}
			return CustomAttribute.GetCustomAttributes(this, typeof(object) as RuntimeType);
		}

		// Token: 0x0600484A RID: 18506 RVA: 0x00106904 File Offset: 0x00104B04
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			if (System.Reflection.MetadataToken.IsNullToken(this.m_tkParamDef))
			{
				return EmptyArray<object>.Value;
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "attributeType");
			}
			return CustomAttribute.GetCustomAttributes(this, runtimeType);
		}

		// Token: 0x0600484B RID: 18507 RVA: 0x0010696C File Offset: 0x00104B6C
		[SecuritySafeCritical]
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			if (System.Reflection.MetadataToken.IsNullToken(this.m_tkParamDef))
			{
				return false;
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "attributeType");
			}
			return CustomAttribute.IsDefined(this, runtimeType);
		}

		// Token: 0x0600484C RID: 18508 RVA: 0x001069CD File Offset: 0x00104BCD
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributesInternal(this);
		}

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x0600484D RID: 18509 RVA: 0x001069D8 File Offset: 0x00104BD8
		internal RemotingParameterCachedData RemotingCache
		{
			get
			{
				RemotingParameterCachedData remotingParameterCachedData = this.m_cachedData;
				if (remotingParameterCachedData == null)
				{
					remotingParameterCachedData = new RemotingParameterCachedData(this);
					RemotingParameterCachedData remotingParameterCachedData2 = Interlocked.CompareExchange<RemotingParameterCachedData>(ref this.m_cachedData, remotingParameterCachedData, null);
					if (remotingParameterCachedData2 != null)
					{
						remotingParameterCachedData = remotingParameterCachedData2;
					}
				}
				return remotingParameterCachedData;
			}
		}

		// Token: 0x0600484E RID: 18510 RVA: 0x00106A0A File Offset: 0x00104C0A
		// Note: this type is marked as 'beforefieldinit'.
		static RuntimeParameterInfo()
		{
		}

		// Token: 0x04001DFD RID: 7677
		private static readonly Type s_DecimalConstantAttributeType = typeof(DecimalConstantAttribute);

		// Token: 0x04001DFE RID: 7678
		private static readonly Type s_CustomConstantAttributeType = typeof(CustomConstantAttribute);

		// Token: 0x04001DFF RID: 7679
		[NonSerialized]
		private int m_tkParamDef;

		// Token: 0x04001E00 RID: 7680
		[NonSerialized]
		private MetadataImport m_scope;

		// Token: 0x04001E01 RID: 7681
		[NonSerialized]
		private Signature m_signature;

		// Token: 0x04001E02 RID: 7682
		[NonSerialized]
		private volatile bool m_nameIsCached;

		// Token: 0x04001E03 RID: 7683
		[NonSerialized]
		private readonly bool m_noMetadata;

		// Token: 0x04001E04 RID: 7684
		[NonSerialized]
		private bool m_noDefaultValue;

		// Token: 0x04001E05 RID: 7685
		[NonSerialized]
		private MethodBase m_originalMember;

		// Token: 0x04001E06 RID: 7686
		private RemotingParameterCachedData m_cachedData;
	}
}
