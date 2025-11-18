using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Reflection
{
	// Token: 0x02000618 RID: 1560
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_ParameterInfo))]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class ParameterInfo : _ParameterInfo, ICustomAttributeProvider, IObjectReference
	{
		// Token: 0x06004817 RID: 18455 RVA: 0x00105F1E File Offset: 0x0010411E
		protected ParameterInfo()
		{
		}

		// Token: 0x06004818 RID: 18456 RVA: 0x00105F26 File Offset: 0x00104126
		internal void SetName(string name)
		{
			this.NameImpl = name;
		}

		// Token: 0x06004819 RID: 18457 RVA: 0x00105F2F File Offset: 0x0010412F
		internal void SetAttributes(ParameterAttributes attributes)
		{
			this.AttrsImpl = attributes;
		}

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x0600481A RID: 18458 RVA: 0x00105F38 File Offset: 0x00104138
		[__DynamicallyInvokable]
		public virtual Type ParameterType
		{
			[__DynamicallyInvokable]
			get
			{
				return this.ClassImpl;
			}
		}

		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x0600481B RID: 18459 RVA: 0x00105F40 File Offset: 0x00104140
		[__DynamicallyInvokable]
		public virtual string Name
		{
			[__DynamicallyInvokable]
			get
			{
				return this.NameImpl;
			}
		}

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x0600481C RID: 18460 RVA: 0x00105F48 File Offset: 0x00104148
		[__DynamicallyInvokable]
		public virtual bool HasDefaultValue
		{
			[__DynamicallyInvokable]
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x0600481D RID: 18461 RVA: 0x00105F4F File Offset: 0x0010414F
		[__DynamicallyInvokable]
		public virtual object DefaultValue
		{
			[__DynamicallyInvokable]
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x0600481E RID: 18462 RVA: 0x00105F56 File Offset: 0x00104156
		public virtual object RawDefaultValue
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x0600481F RID: 18463 RVA: 0x00105F5D File Offset: 0x0010415D
		[__DynamicallyInvokable]
		public virtual int Position
		{
			[__DynamicallyInvokable]
			get
			{
				return this.PositionImpl;
			}
		}

		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x06004820 RID: 18464 RVA: 0x00105F65 File Offset: 0x00104165
		[__DynamicallyInvokable]
		public virtual ParameterAttributes Attributes
		{
			[__DynamicallyInvokable]
			get
			{
				return this.AttrsImpl;
			}
		}

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x06004821 RID: 18465 RVA: 0x00105F6D File Offset: 0x0010416D
		[__DynamicallyInvokable]
		public virtual MemberInfo Member
		{
			[__DynamicallyInvokable]
			get
			{
				return this.MemberImpl;
			}
		}

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x06004822 RID: 18466 RVA: 0x00105F75 File Offset: 0x00104175
		[__DynamicallyInvokable]
		public bool IsIn
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & ParameterAttributes.In) > ParameterAttributes.None;
			}
		}

		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x06004823 RID: 18467 RVA: 0x00105F82 File Offset: 0x00104182
		[__DynamicallyInvokable]
		public bool IsOut
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & ParameterAttributes.Out) > ParameterAttributes.None;
			}
		}

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x06004824 RID: 18468 RVA: 0x00105F8F File Offset: 0x0010418F
		[__DynamicallyInvokable]
		public bool IsLcid
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & ParameterAttributes.Lcid) > ParameterAttributes.None;
			}
		}

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x06004825 RID: 18469 RVA: 0x00105F9C File Offset: 0x0010419C
		[__DynamicallyInvokable]
		public bool IsRetval
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & ParameterAttributes.Retval) > ParameterAttributes.None;
			}
		}

		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x06004826 RID: 18470 RVA: 0x00105FA9 File Offset: 0x001041A9
		[__DynamicallyInvokable]
		public bool IsOptional
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & ParameterAttributes.Optional) > ParameterAttributes.None;
			}
		}

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x06004827 RID: 18471 RVA: 0x00105FB8 File Offset: 0x001041B8
		public virtual int MetadataToken
		{
			get
			{
				RuntimeParameterInfo runtimeParameterInfo = this as RuntimeParameterInfo;
				if (runtimeParameterInfo != null)
				{
					return runtimeParameterInfo.MetadataToken;
				}
				return 134217728;
			}
		}

		// Token: 0x06004828 RID: 18472 RVA: 0x00105FDB File Offset: 0x001041DB
		public virtual Type[] GetRequiredCustomModifiers()
		{
			return EmptyArray<Type>.Value;
		}

		// Token: 0x06004829 RID: 18473 RVA: 0x00105FE2 File Offset: 0x001041E2
		public virtual Type[] GetOptionalCustomModifiers()
		{
			return EmptyArray<Type>.Value;
		}

		// Token: 0x0600482A RID: 18474 RVA: 0x00105FE9 File Offset: 0x001041E9
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return this.ParameterType.FormatTypeName() + " " + this.Name;
		}

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x0600482B RID: 18475 RVA: 0x00106006 File Offset: 0x00104206
		[__DynamicallyInvokable]
		public virtual IEnumerable<CustomAttributeData> CustomAttributes
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetCustomAttributesData();
			}
		}

		// Token: 0x0600482C RID: 18476 RVA: 0x0010600E File Offset: 0x0010420E
		[__DynamicallyInvokable]
		public virtual object[] GetCustomAttributes(bool inherit)
		{
			return EmptyArray<object>.Value;
		}

		// Token: 0x0600482D RID: 18477 RVA: 0x00106015 File Offset: 0x00104215
		[__DynamicallyInvokable]
		public virtual object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			return EmptyArray<object>.Value;
		}

		// Token: 0x0600482E RID: 18478 RVA: 0x00106030 File Offset: 0x00104230
		[__DynamicallyInvokable]
		public virtual bool IsDefined(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			return false;
		}

		// Token: 0x0600482F RID: 18479 RVA: 0x00106047 File Offset: 0x00104247
		public virtual IList<CustomAttributeData> GetCustomAttributesData()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004830 RID: 18480 RVA: 0x0010604E File Offset: 0x0010424E
		void _ParameterInfo.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004831 RID: 18481 RVA: 0x00106055 File Offset: 0x00104255
		void _ParameterInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004832 RID: 18482 RVA: 0x0010605C File Offset: 0x0010425C
		void _ParameterInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004833 RID: 18483 RVA: 0x00106063 File Offset: 0x00104263
		void _ParameterInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004834 RID: 18484 RVA: 0x0010606C File Offset: 0x0010426C
		[SecurityCritical]
		public object GetRealObject(StreamingContext context)
		{
			if (this.MemberImpl == null)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_InsufficientState"));
			}
			MemberTypes memberType = this.MemberImpl.MemberType;
			if (memberType != MemberTypes.Constructor && memberType != MemberTypes.Method)
			{
				if (memberType != MemberTypes.Property)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_NoParameterInfo"));
				}
				ParameterInfo[] array = ((RuntimePropertyInfo)this.MemberImpl).GetIndexParametersNoCopy();
				if (array != null && this.PositionImpl > -1 && this.PositionImpl < array.Length)
				{
					return array[this.PositionImpl];
				}
				throw new SerializationException(Environment.GetResourceString("Serialization_BadParameterInfo"));
			}
			else if (this.PositionImpl == -1)
			{
				if (this.MemberImpl.MemberType == MemberTypes.Method)
				{
					return ((MethodInfo)this.MemberImpl).ReturnParameter;
				}
				throw new SerializationException(Environment.GetResourceString("Serialization_BadParameterInfo"));
			}
			else
			{
				ParameterInfo[] array = ((MethodBase)this.MemberImpl).GetParametersNoCopy();
				if (array != null && this.PositionImpl < array.Length)
				{
					return array[this.PositionImpl];
				}
				throw new SerializationException(Environment.GetResourceString("Serialization_BadParameterInfo"));
			}
		}

		// Token: 0x04001DF4 RID: 7668
		protected string NameImpl;

		// Token: 0x04001DF5 RID: 7669
		protected Type ClassImpl;

		// Token: 0x04001DF6 RID: 7670
		protected int PositionImpl;

		// Token: 0x04001DF7 RID: 7671
		protected ParameterAttributes AttrsImpl;

		// Token: 0x04001DF8 RID: 7672
		protected object DefaultValueImpl;

		// Token: 0x04001DF9 RID: 7673
		protected MemberInfo MemberImpl;

		// Token: 0x04001DFA RID: 7674
		[OptionalField]
		private IntPtr _importer;

		// Token: 0x04001DFB RID: 7675
		[OptionalField]
		private int _token;

		// Token: 0x04001DFC RID: 7676
		[OptionalField]
		private bool bExtraConstChecked;
	}
}
