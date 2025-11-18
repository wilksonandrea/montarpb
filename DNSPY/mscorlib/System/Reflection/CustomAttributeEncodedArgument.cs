using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection
{
	// Token: 0x020005DB RID: 1499
	[Serializable]
	[StructLayout(LayoutKind.Auto)]
	internal struct CustomAttributeEncodedArgument
	{
		// Token: 0x0600456A RID: 17770
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParseAttributeArguments(IntPtr pCa, int cCa, ref CustomAttributeCtorParameter[] CustomAttributeCtorParameters, ref CustomAttributeNamedParameter[] CustomAttributeTypedArgument, RuntimeAssembly assembly);

		// Token: 0x0600456B RID: 17771 RVA: 0x000FF2D0 File Offset: 0x000FD4D0
		[SecurityCritical]
		internal static void ParseAttributeArguments(ConstArray attributeBlob, ref CustomAttributeCtorParameter[] customAttributeCtorParameters, ref CustomAttributeNamedParameter[] customAttributeNamedParameters, RuntimeModule customAttributeModule)
		{
			if (customAttributeModule == null)
			{
				throw new ArgumentNullException("customAttributeModule");
			}
			if (customAttributeCtorParameters.Length != 0 || customAttributeNamedParameters.Length != 0)
			{
				CustomAttributeEncodedArgument.ParseAttributeArguments(attributeBlob.Signature, attributeBlob.Length, ref customAttributeCtorParameters, ref customAttributeNamedParameters, (RuntimeAssembly)customAttributeModule.Assembly);
			}
		}

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x0600456C RID: 17772 RVA: 0x000FF310 File Offset: 0x000FD510
		public CustomAttributeType CustomAttributeType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x0600456D RID: 17773 RVA: 0x000FF318 File Offset: 0x000FD518
		public long PrimitiveValue
		{
			get
			{
				return this.m_primitiveValue;
			}
		}

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x0600456E RID: 17774 RVA: 0x000FF320 File Offset: 0x000FD520
		public CustomAttributeEncodedArgument[] ArrayValue
		{
			get
			{
				return this.m_arrayValue;
			}
		}

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x0600456F RID: 17775 RVA: 0x000FF328 File Offset: 0x000FD528
		public string StringValue
		{
			get
			{
				return this.m_stringValue;
			}
		}

		// Token: 0x04001C83 RID: 7299
		private long m_primitiveValue;

		// Token: 0x04001C84 RID: 7300
		private CustomAttributeEncodedArgument[] m_arrayValue;

		// Token: 0x04001C85 RID: 7301
		private string m_stringValue;

		// Token: 0x04001C86 RID: 7302
		private CustomAttributeType m_type;
	}
}
