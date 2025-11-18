using System;
using System.Reflection;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200092D RID: 2349
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class MarshalAsAttribute : Attribute
	{
		// Token: 0x06006024 RID: 24612 RVA: 0x0014B713 File Offset: 0x00149913
		[SecurityCritical]
		internal static Attribute GetCustomAttribute(RuntimeParameterInfo parameter)
		{
			return MarshalAsAttribute.GetCustomAttribute(parameter.MetadataToken, parameter.GetRuntimeModule());
		}

		// Token: 0x06006025 RID: 24613 RVA: 0x0014B726 File Offset: 0x00149926
		[SecurityCritical]
		internal static bool IsDefined(RuntimeParameterInfo parameter)
		{
			return MarshalAsAttribute.GetCustomAttribute(parameter) != null;
		}

		// Token: 0x06006026 RID: 24614 RVA: 0x0014B731 File Offset: 0x00149931
		[SecurityCritical]
		internal static Attribute GetCustomAttribute(RuntimeFieldInfo field)
		{
			return MarshalAsAttribute.GetCustomAttribute(field.MetadataToken, field.GetRuntimeModule());
		}

		// Token: 0x06006027 RID: 24615 RVA: 0x0014B744 File Offset: 0x00149944
		[SecurityCritical]
		internal static bool IsDefined(RuntimeFieldInfo field)
		{
			return MarshalAsAttribute.GetCustomAttribute(field) != null;
		}

		// Token: 0x06006028 RID: 24616 RVA: 0x0014B750 File Offset: 0x00149950
		[SecurityCritical]
		internal static Attribute GetCustomAttribute(int token, RuntimeModule scope)
		{
			int num = 0;
			int num2 = 0;
			string text = null;
			string text2 = null;
			string text3 = null;
			int num3 = 0;
			ConstArray fieldMarshal = ModuleHandle.GetMetadataImport(scope.GetNativeHandle()).GetFieldMarshal(token);
			if (fieldMarshal.Length == 0)
			{
				return null;
			}
			UnmanagedType unmanagedType;
			VarEnum varEnum;
			UnmanagedType unmanagedType2;
			MetadataImport.GetMarshalAs(fieldMarshal, out unmanagedType, out varEnum, out text3, out unmanagedType2, out num, out num2, out text, out text2, out num3);
			RuntimeType runtimeType = ((text3 == null || text3.Length == 0) ? null : RuntimeTypeHandle.GetTypeByNameUsingCARules(text3, scope));
			RuntimeType runtimeType2 = null;
			try
			{
				runtimeType2 = ((text == null) ? null : RuntimeTypeHandle.GetTypeByNameUsingCARules(text, scope));
			}
			catch (TypeLoadException)
			{
			}
			return new MarshalAsAttribute(unmanagedType, varEnum, runtimeType, unmanagedType2, (short)num, num2, text, runtimeType2, text2, num3);
		}

		// Token: 0x06006029 RID: 24617 RVA: 0x0014B804 File Offset: 0x00149A04
		internal MarshalAsAttribute(UnmanagedType val, VarEnum safeArraySubType, RuntimeType safeArrayUserDefinedSubType, UnmanagedType arraySubType, short sizeParamIndex, int sizeConst, string marshalType, RuntimeType marshalTypeRef, string marshalCookie, int iidParamIndex)
		{
			this._val = val;
			this.SafeArraySubType = safeArraySubType;
			this.SafeArrayUserDefinedSubType = safeArrayUserDefinedSubType;
			this.IidParameterIndex = iidParamIndex;
			this.ArraySubType = arraySubType;
			this.SizeParamIndex = sizeParamIndex;
			this.SizeConst = sizeConst;
			this.MarshalType = marshalType;
			this.MarshalTypeRef = marshalTypeRef;
			this.MarshalCookie = marshalCookie;
		}

		// Token: 0x0600602A RID: 24618 RVA: 0x0014B864 File Offset: 0x00149A64
		[__DynamicallyInvokable]
		public MarshalAsAttribute(UnmanagedType unmanagedType)
		{
			this._val = unmanagedType;
		}

		// Token: 0x0600602B RID: 24619 RVA: 0x0014B873 File Offset: 0x00149A73
		[__DynamicallyInvokable]
		public MarshalAsAttribute(short unmanagedType)
		{
			this._val = (UnmanagedType)unmanagedType;
		}

		// Token: 0x170010DF RID: 4319
		// (get) Token: 0x0600602C RID: 24620 RVA: 0x0014B882 File Offset: 0x00149A82
		[__DynamicallyInvokable]
		public UnmanagedType Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002B06 RID: 11014
		internal UnmanagedType _val;

		// Token: 0x04002B07 RID: 11015
		[__DynamicallyInvokable]
		public VarEnum SafeArraySubType;

		// Token: 0x04002B08 RID: 11016
		[__DynamicallyInvokable]
		public Type SafeArrayUserDefinedSubType;

		// Token: 0x04002B09 RID: 11017
		[__DynamicallyInvokable]
		public int IidParameterIndex;

		// Token: 0x04002B0A RID: 11018
		[__DynamicallyInvokable]
		public UnmanagedType ArraySubType;

		// Token: 0x04002B0B RID: 11019
		[__DynamicallyInvokable]
		public short SizeParamIndex;

		// Token: 0x04002B0C RID: 11020
		[__DynamicallyInvokable]
		public int SizeConst;

		// Token: 0x04002B0D RID: 11021
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public string MarshalType;

		// Token: 0x04002B0E RID: 11022
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public Type MarshalTypeRef;

		// Token: 0x04002B0F RID: 11023
		[__DynamicallyInvokable]
		public string MarshalCookie;
	}
}
