using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection
{
	// Token: 0x02000600 RID: 1536
	internal struct MetadataImport
	{
		// Token: 0x0600469C RID: 18076 RVA: 0x00102984 File Offset: 0x00100B84
		public override int GetHashCode()
		{
			return ValueType.GetHashCodeOfPtr(this.m_metadataImport2);
		}

		// Token: 0x0600469D RID: 18077 RVA: 0x00102991 File Offset: 0x00100B91
		public override bool Equals(object obj)
		{
			return obj is MetadataImport && this.Equals((MetadataImport)obj);
		}

		// Token: 0x0600469E RID: 18078 RVA: 0x001029A9 File Offset: 0x00100BA9
		private bool Equals(MetadataImport import)
		{
			return import.m_metadataImport2 == this.m_metadataImport2;
		}

		// Token: 0x0600469F RID: 18079
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetMarshalAs(IntPtr pNativeType, int cNativeType, out int unmanagedType, out int safeArraySubType, out string safeArrayUserDefinedSubType, out int arraySubType, out int sizeParamIndex, out int sizeConst, out string marshalType, out string marshalCookie, out int iidParamIndex);

		// Token: 0x060046A0 RID: 18080 RVA: 0x001029BC File Offset: 0x00100BBC
		[SecurityCritical]
		internal static void GetMarshalAs(ConstArray nativeType, out UnmanagedType unmanagedType, out VarEnum safeArraySubType, out string safeArrayUserDefinedSubType, out UnmanagedType arraySubType, out int sizeParamIndex, out int sizeConst, out string marshalType, out string marshalCookie, out int iidParamIndex)
		{
			int num;
			int num2;
			int num3;
			MetadataImport._GetMarshalAs(nativeType.Signature, nativeType.Length, out num, out num2, out safeArrayUserDefinedSubType, out num3, out sizeParamIndex, out sizeConst, out marshalType, out marshalCookie, out iidParamIndex);
			unmanagedType = (UnmanagedType)num;
			safeArraySubType = (VarEnum)num2;
			arraySubType = (UnmanagedType)num3;
		}

		// Token: 0x060046A1 RID: 18081 RVA: 0x001029F7 File Offset: 0x00100BF7
		internal static void ThrowError(int hResult)
		{
			throw new MetadataException(hResult);
		}

		// Token: 0x060046A2 RID: 18082 RVA: 0x001029FF File Offset: 0x00100BFF
		internal MetadataImport(IntPtr metadataImport2, object keepalive)
		{
			this.m_metadataImport2 = metadataImport2;
			this.m_keepalive = keepalive;
		}

		// Token: 0x060046A3 RID: 18083
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _Enum(IntPtr scope, int type, int parent, out MetadataEnumResult result);

		// Token: 0x060046A4 RID: 18084 RVA: 0x00102A0F File Offset: 0x00100C0F
		[SecurityCritical]
		public void Enum(MetadataTokenType type, int parent, out MetadataEnumResult result)
		{
			MetadataImport._Enum(this.m_metadataImport2, (int)type, parent, out result);
		}

		// Token: 0x060046A5 RID: 18085 RVA: 0x00102A1F File Offset: 0x00100C1F
		[SecurityCritical]
		public void EnumNestedTypes(int mdTypeDef, out MetadataEnumResult result)
		{
			this.Enum(MetadataTokenType.TypeDef, mdTypeDef, out result);
		}

		// Token: 0x060046A6 RID: 18086 RVA: 0x00102A2E File Offset: 0x00100C2E
		[SecurityCritical]
		public void EnumCustomAttributes(int mdToken, out MetadataEnumResult result)
		{
			this.Enum(MetadataTokenType.CustomAttribute, mdToken, out result);
		}

		// Token: 0x060046A7 RID: 18087 RVA: 0x00102A3D File Offset: 0x00100C3D
		[SecurityCritical]
		public void EnumParams(int mdMethodDef, out MetadataEnumResult result)
		{
			this.Enum(MetadataTokenType.ParamDef, mdMethodDef, out result);
		}

		// Token: 0x060046A8 RID: 18088 RVA: 0x00102A4C File Offset: 0x00100C4C
		[SecurityCritical]
		public void EnumFields(int mdTypeDef, out MetadataEnumResult result)
		{
			this.Enum(MetadataTokenType.FieldDef, mdTypeDef, out result);
		}

		// Token: 0x060046A9 RID: 18089 RVA: 0x00102A5B File Offset: 0x00100C5B
		[SecurityCritical]
		public void EnumProperties(int mdTypeDef, out MetadataEnumResult result)
		{
			this.Enum(MetadataTokenType.Property, mdTypeDef, out result);
		}

		// Token: 0x060046AA RID: 18090 RVA: 0x00102A6A File Offset: 0x00100C6A
		[SecurityCritical]
		public void EnumEvents(int mdTypeDef, out MetadataEnumResult result)
		{
			this.Enum(MetadataTokenType.Event, mdTypeDef, out result);
		}

		// Token: 0x060046AB RID: 18091
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string _GetDefaultValue(IntPtr scope, int mdToken, out long value, out int length, out int corElementType);

		// Token: 0x060046AC RID: 18092 RVA: 0x00102A7C File Offset: 0x00100C7C
		[SecurityCritical]
		public string GetDefaultValue(int mdToken, out long value, out int length, out CorElementType corElementType)
		{
			int num;
			string text = MetadataImport._GetDefaultValue(this.m_metadataImport2, mdToken, out value, out length, out num);
			corElementType = (CorElementType)num;
			return text;
		}

		// Token: 0x060046AD RID: 18093
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void _GetUserString(IntPtr scope, int mdToken, void** name, out int length);

		// Token: 0x060046AE RID: 18094 RVA: 0x00102AA0 File Offset: 0x00100CA0
		[SecurityCritical]
		public unsafe string GetUserString(int mdToken)
		{
			void* ptr;
			int num;
			MetadataImport._GetUserString(this.m_metadataImport2, mdToken, &ptr, out num);
			if (ptr == null)
			{
				return null;
			}
			char[] array = new char[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = (char)(*(ushort*)((byte*)ptr + (IntPtr)i * 2));
			}
			return new string(array);
		}

		// Token: 0x060046AF RID: 18095
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void _GetName(IntPtr scope, int mdToken, void** name);

		// Token: 0x060046B0 RID: 18096 RVA: 0x00102AE8 File Offset: 0x00100CE8
		[SecurityCritical]
		public unsafe Utf8String GetName(int mdToken)
		{
			void* ptr;
			MetadataImport._GetName(this.m_metadataImport2, mdToken, &ptr);
			return new Utf8String(ptr);
		}

		// Token: 0x060046B1 RID: 18097
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void _GetNamespace(IntPtr scope, int mdToken, void** namesp);

		// Token: 0x060046B2 RID: 18098 RVA: 0x00102B0C File Offset: 0x00100D0C
		[SecurityCritical]
		public unsafe Utf8String GetNamespace(int mdToken)
		{
			void* ptr;
			MetadataImport._GetNamespace(this.m_metadataImport2, mdToken, &ptr);
			return new Utf8String(ptr);
		}

		// Token: 0x060046B3 RID: 18099
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void _GetEventProps(IntPtr scope, int mdToken, void** name, out int eventAttributes);

		// Token: 0x060046B4 RID: 18100 RVA: 0x00102B30 File Offset: 0x00100D30
		[SecurityCritical]
		public unsafe void GetEventProps(int mdToken, out void* name, out EventAttributes eventAttributes)
		{
			void* ptr;
			int num;
			MetadataImport._GetEventProps(this.m_metadataImport2, mdToken, &ptr, out num);
			name = ptr;
			eventAttributes = (EventAttributes)num;
		}

		// Token: 0x060046B5 RID: 18101
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetFieldDefProps(IntPtr scope, int mdToken, out int fieldAttributes);

		// Token: 0x060046B6 RID: 18102 RVA: 0x00102B54 File Offset: 0x00100D54
		[SecurityCritical]
		public void GetFieldDefProps(int mdToken, out FieldAttributes fieldAttributes)
		{
			int num;
			MetadataImport._GetFieldDefProps(this.m_metadataImport2, mdToken, out num);
			fieldAttributes = (FieldAttributes)num;
		}

		// Token: 0x060046B7 RID: 18103
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void _GetPropertyProps(IntPtr scope, int mdToken, void** name, out int propertyAttributes, out ConstArray signature);

		// Token: 0x060046B8 RID: 18104 RVA: 0x00102B74 File Offset: 0x00100D74
		[SecurityCritical]
		public unsafe void GetPropertyProps(int mdToken, out void* name, out PropertyAttributes propertyAttributes, out ConstArray signature)
		{
			void* ptr;
			int num;
			MetadataImport._GetPropertyProps(this.m_metadataImport2, mdToken, &ptr, out num, out signature);
			name = ptr;
			propertyAttributes = (PropertyAttributes)num;
		}

		// Token: 0x060046B9 RID: 18105
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetParentToken(IntPtr scope, int mdToken, out int tkParent);

		// Token: 0x060046BA RID: 18106 RVA: 0x00102B9C File Offset: 0x00100D9C
		[SecurityCritical]
		public int GetParentToken(int tkToken)
		{
			int num;
			MetadataImport._GetParentToken(this.m_metadataImport2, tkToken, out num);
			return num;
		}

		// Token: 0x060046BB RID: 18107
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetParamDefProps(IntPtr scope, int parameterToken, out int sequence, out int attributes);

		// Token: 0x060046BC RID: 18108 RVA: 0x00102BB8 File Offset: 0x00100DB8
		[SecurityCritical]
		public void GetParamDefProps(int parameterToken, out int sequence, out ParameterAttributes attributes)
		{
			int num;
			MetadataImport._GetParamDefProps(this.m_metadataImport2, parameterToken, out sequence, out num);
			attributes = (ParameterAttributes)num;
		}

		// Token: 0x060046BD RID: 18109
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetGenericParamProps(IntPtr scope, int genericParameter, out int flags);

		// Token: 0x060046BE RID: 18110 RVA: 0x00102BD8 File Offset: 0x00100DD8
		[SecurityCritical]
		public void GetGenericParamProps(int genericParameter, out GenericParameterAttributes attributes)
		{
			int num;
			MetadataImport._GetGenericParamProps(this.m_metadataImport2, genericParameter, out num);
			attributes = (GenericParameterAttributes)num;
		}

		// Token: 0x060046BF RID: 18111
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetScopeProps(IntPtr scope, out Guid mvid);

		// Token: 0x060046C0 RID: 18112 RVA: 0x00102BF6 File Offset: 0x00100DF6
		[SecurityCritical]
		public void GetScopeProps(out Guid mvid)
		{
			MetadataImport._GetScopeProps(this.m_metadataImport2, out mvid);
		}

		// Token: 0x060046C1 RID: 18113 RVA: 0x00102C04 File Offset: 0x00100E04
		[SecurityCritical]
		public ConstArray GetMethodSignature(MetadataToken token)
		{
			if (token.IsMemberRef)
			{
				return this.GetMemberRefProps(token);
			}
			return this.GetSigOfMethodDef(token);
		}

		// Token: 0x060046C2 RID: 18114
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetSigOfMethodDef(IntPtr scope, int methodToken, ref ConstArray signature);

		// Token: 0x060046C3 RID: 18115 RVA: 0x00102C28 File Offset: 0x00100E28
		[SecurityCritical]
		public ConstArray GetSigOfMethodDef(int methodToken)
		{
			ConstArray constArray = default(ConstArray);
			MetadataImport._GetSigOfMethodDef(this.m_metadataImport2, methodToken, ref constArray);
			return constArray;
		}

		// Token: 0x060046C4 RID: 18116
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetSignatureFromToken(IntPtr scope, int methodToken, ref ConstArray signature);

		// Token: 0x060046C5 RID: 18117 RVA: 0x00102C4C File Offset: 0x00100E4C
		[SecurityCritical]
		public ConstArray GetSignatureFromToken(int token)
		{
			ConstArray constArray = default(ConstArray);
			MetadataImport._GetSignatureFromToken(this.m_metadataImport2, token, ref constArray);
			return constArray;
		}

		// Token: 0x060046C6 RID: 18118
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetMemberRefProps(IntPtr scope, int memberTokenRef, out ConstArray signature);

		// Token: 0x060046C7 RID: 18119 RVA: 0x00102C70 File Offset: 0x00100E70
		[SecurityCritical]
		public ConstArray GetMemberRefProps(int memberTokenRef)
		{
			ConstArray constArray = default(ConstArray);
			MetadataImport._GetMemberRefProps(this.m_metadataImport2, memberTokenRef, out constArray);
			return constArray;
		}

		// Token: 0x060046C8 RID: 18120
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetCustomAttributeProps(IntPtr scope, int customAttributeToken, out int constructorToken, out ConstArray signature);

		// Token: 0x060046C9 RID: 18121 RVA: 0x00102C94 File Offset: 0x00100E94
		[SecurityCritical]
		public void GetCustomAttributeProps(int customAttributeToken, out int constructorToken, out ConstArray signature)
		{
			MetadataImport._GetCustomAttributeProps(this.m_metadataImport2, customAttributeToken, out constructorToken, out signature);
		}

		// Token: 0x060046CA RID: 18122
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetClassLayout(IntPtr scope, int typeTokenDef, out int packSize, out int classSize);

		// Token: 0x060046CB RID: 18123 RVA: 0x00102CA4 File Offset: 0x00100EA4
		[SecurityCritical]
		public void GetClassLayout(int typeTokenDef, out int packSize, out int classSize)
		{
			MetadataImport._GetClassLayout(this.m_metadataImport2, typeTokenDef, out packSize, out classSize);
		}

		// Token: 0x060046CC RID: 18124
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool _GetFieldOffset(IntPtr scope, int typeTokenDef, int fieldTokenDef, out int offset);

		// Token: 0x060046CD RID: 18125 RVA: 0x00102CB4 File Offset: 0x00100EB4
		[SecurityCritical]
		public bool GetFieldOffset(int typeTokenDef, int fieldTokenDef, out int offset)
		{
			return MetadataImport._GetFieldOffset(this.m_metadataImport2, typeTokenDef, fieldTokenDef, out offset);
		}

		// Token: 0x060046CE RID: 18126
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetSigOfFieldDef(IntPtr scope, int fieldToken, ref ConstArray fieldMarshal);

		// Token: 0x060046CF RID: 18127 RVA: 0x00102CC4 File Offset: 0x00100EC4
		[SecurityCritical]
		public ConstArray GetSigOfFieldDef(int fieldToken)
		{
			ConstArray constArray = default(ConstArray);
			MetadataImport._GetSigOfFieldDef(this.m_metadataImport2, fieldToken, ref constArray);
			return constArray;
		}

		// Token: 0x060046D0 RID: 18128
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetFieldMarshal(IntPtr scope, int fieldToken, ref ConstArray fieldMarshal);

		// Token: 0x060046D1 RID: 18129 RVA: 0x00102CE8 File Offset: 0x00100EE8
		[SecurityCritical]
		public ConstArray GetFieldMarshal(int fieldToken)
		{
			ConstArray constArray = default(ConstArray);
			MetadataImport._GetFieldMarshal(this.m_metadataImport2, fieldToken, ref constArray);
			return constArray;
		}

		// Token: 0x060046D2 RID: 18130
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void _GetPInvokeMap(IntPtr scope, int token, out int attributes, void** importName, void** importDll);

		// Token: 0x060046D3 RID: 18131 RVA: 0x00102D0C File Offset: 0x00100F0C
		[SecurityCritical]
		public unsafe void GetPInvokeMap(int token, out PInvokeAttributes attributes, out string importName, out string importDll)
		{
			int num;
			void* ptr;
			void* ptr2;
			MetadataImport._GetPInvokeMap(this.m_metadataImport2, token, out num, &ptr, &ptr2);
			importName = new Utf8String(ptr).ToString();
			importDll = new Utf8String(ptr2).ToString();
			attributes = (PInvokeAttributes)num;
		}

		// Token: 0x060046D4 RID: 18132
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool _IsValidToken(IntPtr scope, int token);

		// Token: 0x060046D5 RID: 18133 RVA: 0x00102D5D File Offset: 0x00100F5D
		[SecurityCritical]
		public bool IsValidToken(int token)
		{
			return MetadataImport._IsValidToken(this.m_metadataImport2, token);
		}

		// Token: 0x060046D6 RID: 18134 RVA: 0x00102D6B File Offset: 0x00100F6B
		// Note: this type is marked as 'beforefieldinit'.
		static MetadataImport()
		{
		}

		// Token: 0x04001D5B RID: 7515
		private IntPtr m_metadataImport2;

		// Token: 0x04001D5C RID: 7516
		private object m_keepalive;

		// Token: 0x04001D5D RID: 7517
		internal static readonly MetadataImport EmptyImport = new MetadataImport((IntPtr)0, null);
	}
}
