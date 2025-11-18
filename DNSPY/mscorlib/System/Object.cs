using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
	// Token: 0x0200003D RID: 61
	[ClassInterface(ClassInterfaceType.AutoDual)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class Object
	{
		// Token: 0x0600022A RID: 554 RVA: 0x00005BC9 File Offset: 0x00003DC9
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[NonVersionable]
		[__DynamicallyInvokable]
		public Object()
		{
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00005BCB File Offset: 0x00003DCB
		[__DynamicallyInvokable]
		public virtual string ToString()
		{
			return this.GetType().ToString();
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00005BD8 File Offset: 0x00003DD8
		[__DynamicallyInvokable]
		public virtual bool Equals(object obj)
		{
			return RuntimeHelpers.Equals(this, obj);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00005BE1 File Offset: 0x00003DE1
		[__DynamicallyInvokable]
		public static bool Equals(object objA, object objB)
		{
			return objA == objB || (objA != null && objB != null && objA.Equals(objB));
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00005BF8 File Offset: 0x00003DF8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[NonVersionable]
		[__DynamicallyInvokable]
		public static bool ReferenceEquals(object objA, object objB)
		{
			return objA == objB;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00005BFE File Offset: 0x00003DFE
		[__DynamicallyInvokable]
		public virtual int GetHashCode()
		{
			return RuntimeHelpers.GetHashCode(this);
		}

		// Token: 0x06000230 RID: 560
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Type GetType();

		// Token: 0x06000231 RID: 561 RVA: 0x00005C06 File Offset: 0x00003E06
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[NonVersionable]
		[__DynamicallyInvokable]
		protected virtual void Finalize()
		{
		}

		// Token: 0x06000232 RID: 562
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		protected extern object MemberwiseClone();

		// Token: 0x06000233 RID: 563 RVA: 0x00005C08 File Offset: 0x00003E08
		[SecurityCritical]
		private void FieldSetter(string typeName, string fieldName, object val)
		{
			FieldInfo fieldInfo = this.GetFieldInfo(typeName, fieldName);
			if (fieldInfo.IsInitOnly)
			{
				throw new FieldAccessException(Environment.GetResourceString("FieldAccess_InitOnly"));
			}
			Message.CoerceArg(val, fieldInfo.FieldType);
			fieldInfo.SetValue(this, val);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00005C4C File Offset: 0x00003E4C
		private void FieldGetter(string typeName, string fieldName, ref object val)
		{
			FieldInfo fieldInfo = this.GetFieldInfo(typeName, fieldName);
			val = fieldInfo.GetValue(this);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00005C6C File Offset: 0x00003E6C
		private FieldInfo GetFieldInfo(string typeName, string fieldName)
		{
			Type type = this.GetType();
			while (null != type && !type.FullName.Equals(typeName))
			{
				type = type.BaseType;
			}
			if (null == type)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadType"), typeName));
			}
			FieldInfo field = type.GetField(fieldName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
			if (null == field)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadField"), fieldName, typeName));
			}
			return field;
		}
	}
}
