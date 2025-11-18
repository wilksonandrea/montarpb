using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.Remoting.Metadata;
using System.Security;
using System.Threading;

namespace System.Runtime.Serialization
{
	// Token: 0x02000742 RID: 1858
	internal sealed class SerializationFieldInfo : FieldInfo
	{
		// Token: 0x17000D75 RID: 3445
		// (get) Token: 0x060051DF RID: 20959 RVA: 0x0011FC7F File Offset: 0x0011DE7F
		public override Module Module
		{
			get
			{
				return this.m_field.Module;
			}
		}

		// Token: 0x17000D76 RID: 3446
		// (get) Token: 0x060051E0 RID: 20960 RVA: 0x0011FC8C File Offset: 0x0011DE8C
		public override int MetadataToken
		{
			get
			{
				return this.m_field.MetadataToken;
			}
		}

		// Token: 0x060051E1 RID: 20961 RVA: 0x0011FC99 File Offset: 0x0011DE99
		internal SerializationFieldInfo(RuntimeFieldInfo field, string namePrefix)
		{
			this.m_field = field;
			this.m_serializationName = namePrefix + "+" + this.m_field.Name;
		}

		// Token: 0x17000D77 RID: 3447
		// (get) Token: 0x060051E2 RID: 20962 RVA: 0x0011FCC4 File Offset: 0x0011DEC4
		public override string Name
		{
			get
			{
				return this.m_serializationName;
			}
		}

		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x060051E3 RID: 20963 RVA: 0x0011FCCC File Offset: 0x0011DECC
		public override Type DeclaringType
		{
			get
			{
				return this.m_field.DeclaringType;
			}
		}

		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x060051E4 RID: 20964 RVA: 0x0011FCD9 File Offset: 0x0011DED9
		public override Type ReflectedType
		{
			get
			{
				return this.m_field.ReflectedType;
			}
		}

		// Token: 0x060051E5 RID: 20965 RVA: 0x0011FCE6 File Offset: 0x0011DEE6
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.m_field.GetCustomAttributes(inherit);
		}

		// Token: 0x060051E6 RID: 20966 RVA: 0x0011FCF4 File Offset: 0x0011DEF4
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.m_field.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x060051E7 RID: 20967 RVA: 0x0011FD03 File Offset: 0x0011DF03
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.m_field.IsDefined(attributeType, inherit);
		}

		// Token: 0x17000D7A RID: 3450
		// (get) Token: 0x060051E8 RID: 20968 RVA: 0x0011FD12 File Offset: 0x0011DF12
		public override Type FieldType
		{
			get
			{
				return this.m_field.FieldType;
			}
		}

		// Token: 0x060051E9 RID: 20969 RVA: 0x0011FD1F File Offset: 0x0011DF1F
		public override object GetValue(object obj)
		{
			return this.m_field.GetValue(obj);
		}

		// Token: 0x060051EA RID: 20970 RVA: 0x0011FD30 File Offset: 0x0011DF30
		[SecurityCritical]
		internal object InternalGetValue(object obj)
		{
			RtFieldInfo rtFieldInfo = this.m_field as RtFieldInfo;
			if (rtFieldInfo != null)
			{
				rtFieldInfo.CheckConsistency(obj);
				return rtFieldInfo.UnsafeGetValue(obj);
			}
			return this.m_field.GetValue(obj);
		}

		// Token: 0x060051EB RID: 20971 RVA: 0x0011FD6D File Offset: 0x0011DF6D
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			this.m_field.SetValue(obj, value, invokeAttr, binder, culture);
		}

		// Token: 0x060051EC RID: 20972 RVA: 0x0011FD84 File Offset: 0x0011DF84
		[SecurityCritical]
		internal void InternalSetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			RtFieldInfo rtFieldInfo = this.m_field as RtFieldInfo;
			if (rtFieldInfo != null)
			{
				rtFieldInfo.CheckConsistency(obj);
				rtFieldInfo.UnsafeSetValue(obj, value, invokeAttr, binder, culture);
				return;
			}
			this.m_field.SetValue(obj, value, invokeAttr, binder, culture);
		}

		// Token: 0x17000D7B RID: 3451
		// (get) Token: 0x060051ED RID: 20973 RVA: 0x0011FDCD File Offset: 0x0011DFCD
		internal RuntimeFieldInfo FieldInfo
		{
			get
			{
				return this.m_field;
			}
		}

		// Token: 0x17000D7C RID: 3452
		// (get) Token: 0x060051EE RID: 20974 RVA: 0x0011FDD5 File Offset: 0x0011DFD5
		public override RuntimeFieldHandle FieldHandle
		{
			get
			{
				return this.m_field.FieldHandle;
			}
		}

		// Token: 0x17000D7D RID: 3453
		// (get) Token: 0x060051EF RID: 20975 RVA: 0x0011FDE2 File Offset: 0x0011DFE2
		public override FieldAttributes Attributes
		{
			get
			{
				return this.m_field.Attributes;
			}
		}

		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x060051F0 RID: 20976 RVA: 0x0011FDF0 File Offset: 0x0011DFF0
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

		// Token: 0x0400244D RID: 9293
		internal const string FakeNameSeparatorString = "+";

		// Token: 0x0400244E RID: 9294
		private RuntimeFieldInfo m_field;

		// Token: 0x0400244F RID: 9295
		private string m_serializationName;

		// Token: 0x04002450 RID: 9296
		private RemotingFieldCachedData m_cachedData;
	}
}
