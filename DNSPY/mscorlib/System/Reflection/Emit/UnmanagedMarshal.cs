using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
	// Token: 0x0200066D RID: 1645
	[ComVisible(true)]
	[Obsolete("An alternate API is available: Emit the MarshalAs custom attribute instead. http://go.microsoft.com/fwlink/?linkid=14202")]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public sealed class UnmanagedMarshal
	{
		// Token: 0x06004F10 RID: 20240 RVA: 0x0011BEAA File Offset: 0x0011A0AA
		public static UnmanagedMarshal DefineUnmanagedMarshal(UnmanagedType unmanagedType)
		{
			if (unmanagedType == UnmanagedType.ByValTStr || unmanagedType == UnmanagedType.SafeArray || unmanagedType == UnmanagedType.CustomMarshaler || unmanagedType == UnmanagedType.ByValArray || unmanagedType == UnmanagedType.LPArray)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NotASimpleNativeType"));
			}
			return new UnmanagedMarshal(unmanagedType, Guid.Empty, 0, (UnmanagedType)0);
		}

		// Token: 0x06004F11 RID: 20241 RVA: 0x0011BEE2 File Offset: 0x0011A0E2
		public static UnmanagedMarshal DefineByValTStr(int elemCount)
		{
			return new UnmanagedMarshal(UnmanagedType.ByValTStr, Guid.Empty, elemCount, (UnmanagedType)0);
		}

		// Token: 0x06004F12 RID: 20242 RVA: 0x0011BEF2 File Offset: 0x0011A0F2
		public static UnmanagedMarshal DefineSafeArray(UnmanagedType elemType)
		{
			return new UnmanagedMarshal(UnmanagedType.SafeArray, Guid.Empty, 0, elemType);
		}

		// Token: 0x06004F13 RID: 20243 RVA: 0x0011BF02 File Offset: 0x0011A102
		public static UnmanagedMarshal DefineByValArray(int elemCount)
		{
			return new UnmanagedMarshal(UnmanagedType.ByValArray, Guid.Empty, elemCount, (UnmanagedType)0);
		}

		// Token: 0x06004F14 RID: 20244 RVA: 0x0011BF12 File Offset: 0x0011A112
		public static UnmanagedMarshal DefineLPArray(UnmanagedType elemType)
		{
			return new UnmanagedMarshal(UnmanagedType.LPArray, Guid.Empty, 0, elemType);
		}

		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x06004F15 RID: 20245 RVA: 0x0011BF22 File Offset: 0x0011A122
		public UnmanagedType GetUnmanagedType
		{
			get
			{
				return this.m_unmanagedType;
			}
		}

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x06004F16 RID: 20246 RVA: 0x0011BF2A File Offset: 0x0011A12A
		public Guid IIDGuid
		{
			get
			{
				if (this.m_unmanagedType == UnmanagedType.CustomMarshaler)
				{
					return this.m_guid;
				}
				throw new ArgumentException(Environment.GetResourceString("Argument_NotACustomMarshaler"));
			}
		}

		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x06004F17 RID: 20247 RVA: 0x0011BF4C File Offset: 0x0011A14C
		public int ElementCount
		{
			get
			{
				if (this.m_unmanagedType != UnmanagedType.ByValArray && this.m_unmanagedType != UnmanagedType.ByValTStr)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_NoUnmanagedElementCount"));
				}
				return this.m_numElem;
			}
		}

		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x06004F18 RID: 20248 RVA: 0x0011BF78 File Offset: 0x0011A178
		public UnmanagedType BaseType
		{
			get
			{
				if (this.m_unmanagedType != UnmanagedType.LPArray && this.m_unmanagedType != UnmanagedType.SafeArray)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_NoNestedMarshal"));
				}
				return this.m_baseType;
			}
		}

		// Token: 0x06004F19 RID: 20249 RVA: 0x0011BFA4 File Offset: 0x0011A1A4
		private UnmanagedMarshal(UnmanagedType unmanagedType, Guid guid, int numElem, UnmanagedType type)
		{
			this.m_unmanagedType = unmanagedType;
			this.m_guid = guid;
			this.m_numElem = numElem;
			this.m_baseType = type;
		}

		// Token: 0x06004F1A RID: 20250 RVA: 0x0011BFCC File Offset: 0x0011A1CC
		internal byte[] InternalGetBytes()
		{
			if (this.m_unmanagedType == UnmanagedType.SafeArray || this.m_unmanagedType == UnmanagedType.LPArray)
			{
				int num = 2;
				byte[] array = new byte[num];
				array[0] = (byte)this.m_unmanagedType;
				array[1] = (byte)this.m_baseType;
				return array;
			}
			if (this.m_unmanagedType == UnmanagedType.ByValArray || this.m_unmanagedType == UnmanagedType.ByValTStr)
			{
				int num2 = 0;
				int num3;
				if (this.m_numElem <= 127)
				{
					num3 = 1;
				}
				else if (this.m_numElem <= 16383)
				{
					num3 = 2;
				}
				else
				{
					num3 = 4;
				}
				num3++;
				byte[] array = new byte[num3];
				array[num2++] = (byte)this.m_unmanagedType;
				if (this.m_numElem <= 127)
				{
					array[num2++] = (byte)(this.m_numElem & 255);
				}
				else if (this.m_numElem <= 16383)
				{
					array[num2++] = (byte)((this.m_numElem >> 8) | 128);
					array[num2++] = (byte)(this.m_numElem & 255);
				}
				else if (this.m_numElem <= 536870911)
				{
					array[num2++] = (byte)((this.m_numElem >> 24) | 192);
					array[num2++] = (byte)((this.m_numElem >> 16) & 255);
					array[num2++] = (byte)((this.m_numElem >> 8) & 255);
					array[num2++] = (byte)(this.m_numElem & 255);
				}
				return array;
			}
			return new byte[] { (byte)this.m_unmanagedType };
		}

		// Token: 0x040021E3 RID: 8675
		internal UnmanagedType m_unmanagedType;

		// Token: 0x040021E4 RID: 8676
		internal Guid m_guid;

		// Token: 0x040021E5 RID: 8677
		internal int m_numElem;

		// Token: 0x040021E6 RID: 8678
		internal UnmanagedType m_baseType;
	}
}
