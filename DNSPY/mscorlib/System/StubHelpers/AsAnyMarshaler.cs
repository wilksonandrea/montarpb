using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using Microsoft.Win32;

namespace System.StubHelpers
{
	// Token: 0x020005A1 RID: 1441
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	[SecurityCritical]
	internal struct AsAnyMarshaler
	{
		// Token: 0x06004317 RID: 17175 RVA: 0x000F9961 File Offset: 0x000F7B61
		private static bool IsIn(int dwFlags)
		{
			return (dwFlags & 268435456) != 0;
		}

		// Token: 0x06004318 RID: 17176 RVA: 0x000F996D File Offset: 0x000F7B6D
		private static bool IsOut(int dwFlags)
		{
			return (dwFlags & 536870912) != 0;
		}

		// Token: 0x06004319 RID: 17177 RVA: 0x000F9979 File Offset: 0x000F7B79
		private static bool IsAnsi(int dwFlags)
		{
			return (dwFlags & 16711680) != 0;
		}

		// Token: 0x0600431A RID: 17178 RVA: 0x000F9985 File Offset: 0x000F7B85
		private static bool IsThrowOn(int dwFlags)
		{
			return (dwFlags & 65280) != 0;
		}

		// Token: 0x0600431B RID: 17179 RVA: 0x000F9991 File Offset: 0x000F7B91
		private static bool IsBestFit(int dwFlags)
		{
			return (dwFlags & 255) != 0;
		}

		// Token: 0x0600431C RID: 17180 RVA: 0x000F999D File Offset: 0x000F7B9D
		internal AsAnyMarshaler(IntPtr pvArrayMarshaler)
		{
			this.pvArrayMarshaler = pvArrayMarshaler;
			this.backPropAction = AsAnyMarshaler.BackPropAction.None;
			this.layoutType = null;
			this.cleanupWorkList = null;
		}

		// Token: 0x0600431D RID: 17181 RVA: 0x000F99BC File Offset: 0x000F7BBC
		[SecurityCritical]
		private unsafe IntPtr ConvertArrayToNative(object pManagedHome, int dwFlags)
		{
			Type elementType = pManagedHome.GetType().GetElementType();
			VarEnum varEnum;
			switch (Type.GetTypeCode(elementType))
			{
			case TypeCode.Object:
				if (elementType == typeof(IntPtr))
				{
					varEnum = ((IntPtr.Size == 4) ? VarEnum.VT_I4 : VarEnum.VT_I8);
					goto IL_10D;
				}
				if (elementType == typeof(UIntPtr))
				{
					varEnum = ((IntPtr.Size == 4) ? VarEnum.VT_UI4 : VarEnum.VT_UI8);
					goto IL_10D;
				}
				break;
			case TypeCode.Boolean:
				varEnum = (VarEnum)254;
				goto IL_10D;
			case TypeCode.Char:
				varEnum = (AsAnyMarshaler.IsAnsi(dwFlags) ? ((VarEnum)253) : VarEnum.VT_UI2);
				goto IL_10D;
			case TypeCode.SByte:
				varEnum = VarEnum.VT_I1;
				goto IL_10D;
			case TypeCode.Byte:
				varEnum = VarEnum.VT_UI1;
				goto IL_10D;
			case TypeCode.Int16:
				varEnum = VarEnum.VT_I2;
				goto IL_10D;
			case TypeCode.UInt16:
				varEnum = VarEnum.VT_UI2;
				goto IL_10D;
			case TypeCode.Int32:
				varEnum = VarEnum.VT_I4;
				goto IL_10D;
			case TypeCode.UInt32:
				varEnum = VarEnum.VT_UI4;
				goto IL_10D;
			case TypeCode.Int64:
				varEnum = VarEnum.VT_I8;
				goto IL_10D;
			case TypeCode.UInt64:
				varEnum = VarEnum.VT_UI8;
				goto IL_10D;
			case TypeCode.Single:
				varEnum = VarEnum.VT_R4;
				goto IL_10D;
			case TypeCode.Double:
				varEnum = VarEnum.VT_R8;
				goto IL_10D;
			}
			throw new ArgumentException(Environment.GetResourceString("Arg_NDirectBadObject"));
			IL_10D:
			int num = (int)varEnum;
			if (AsAnyMarshaler.IsBestFit(dwFlags))
			{
				num |= 65536;
			}
			if (AsAnyMarshaler.IsThrowOn(dwFlags))
			{
				num |= 16777216;
			}
			MngdNativeArrayMarshaler.CreateMarshaler(this.pvArrayMarshaler, IntPtr.Zero, num);
			IntPtr intPtr2;
			IntPtr intPtr = new IntPtr((void*)(&intPtr2));
			MngdNativeArrayMarshaler.ConvertSpaceToNative(this.pvArrayMarshaler, ref pManagedHome, intPtr);
			if (AsAnyMarshaler.IsIn(dwFlags))
			{
				MngdNativeArrayMarshaler.ConvertContentsToNative(this.pvArrayMarshaler, ref pManagedHome, intPtr);
			}
			if (AsAnyMarshaler.IsOut(dwFlags))
			{
				this.backPropAction = AsAnyMarshaler.BackPropAction.Array;
			}
			return intPtr2;
		}

		// Token: 0x0600431E RID: 17182 RVA: 0x000F9B4C File Offset: 0x000F7D4C
		[SecurityCritical]
		private static IntPtr ConvertStringToNative(string pManagedHome, int dwFlags)
		{
			IntPtr intPtr;
			if (AsAnyMarshaler.IsAnsi(dwFlags))
			{
				intPtr = CSTRMarshaler.ConvertToNative(dwFlags & 65535, pManagedHome, IntPtr.Zero);
			}
			else
			{
				StubHelpers.CheckStringLength(pManagedHome.Length);
				int num = (pManagedHome.Length + 1) * 2;
				intPtr = Marshal.AllocCoTaskMem(num);
				string.InternalCopy(pManagedHome, intPtr, num);
			}
			return intPtr;
		}

		// Token: 0x0600431F RID: 17183 RVA: 0x000F9B9C File Offset: 0x000F7D9C
		[SecurityCritical]
		private unsafe IntPtr ConvertStringBuilderToNative(StringBuilder pManagedHome, int dwFlags)
		{
			IntPtr intPtr;
			if (AsAnyMarshaler.IsAnsi(dwFlags))
			{
				StubHelpers.CheckStringLength(pManagedHome.Capacity);
				int num = pManagedHome.Capacity * Marshal.SystemMaxDBCSCharSize + 4;
				intPtr = Marshal.AllocCoTaskMem(num);
				byte* ptr = (byte*)(void*)intPtr;
				*(ptr + num - 3) = 0;
				*(ptr + num - 2) = 0;
				*(ptr + num - 1) = 0;
				if (AsAnyMarshaler.IsIn(dwFlags))
				{
					int num2;
					byte[] array = AnsiCharMarshaler.DoAnsiConversion(pManagedHome.ToString(), AsAnyMarshaler.IsBestFit(dwFlags), AsAnyMarshaler.IsThrowOn(dwFlags), out num2);
					Buffer.Memcpy(ptr, 0, array, 0, num2);
					ptr[num2] = 0;
				}
				if (AsAnyMarshaler.IsOut(dwFlags))
				{
					this.backPropAction = AsAnyMarshaler.BackPropAction.StringBuilderAnsi;
				}
			}
			else
			{
				int num3 = pManagedHome.Capacity * 2 + 4;
				intPtr = Marshal.AllocCoTaskMem(num3);
				byte* ptr2 = (byte*)(void*)intPtr;
				*(ptr2 + num3 - 1) = 0;
				*(ptr2 + num3 - 2) = 0;
				if (AsAnyMarshaler.IsIn(dwFlags))
				{
					int num4 = pManagedHome.Length * 2;
					pManagedHome.InternalCopy(intPtr, num4);
					ptr2[num4] = 0;
					(ptr2 + num4)[1] = 0;
				}
				if (AsAnyMarshaler.IsOut(dwFlags))
				{
					this.backPropAction = AsAnyMarshaler.BackPropAction.StringBuilderUnicode;
				}
			}
			return intPtr;
		}

		// Token: 0x06004320 RID: 17184 RVA: 0x000F9CA0 File Offset: 0x000F7EA0
		[SecurityCritical]
		private unsafe IntPtr ConvertLayoutToNative(object pManagedHome, int dwFlags)
		{
			int num = Marshal.SizeOfHelper(pManagedHome.GetType(), false);
			IntPtr intPtr = Marshal.AllocCoTaskMem(num);
			if (AsAnyMarshaler.IsIn(dwFlags))
			{
				StubHelpers.FmtClassUpdateNativeInternal(pManagedHome, (byte*)intPtr.ToPointer(), ref this.cleanupWorkList);
			}
			if (AsAnyMarshaler.IsOut(dwFlags))
			{
				this.backPropAction = AsAnyMarshaler.BackPropAction.Layout;
			}
			this.layoutType = pManagedHome.GetType();
			return intPtr;
		}

		// Token: 0x06004321 RID: 17185 RVA: 0x000F9CF8 File Offset: 0x000F7EF8
		[SecurityCritical]
		internal IntPtr ConvertToNative(object pManagedHome, int dwFlags)
		{
			if (pManagedHome == null)
			{
				return IntPtr.Zero;
			}
			if (pManagedHome is ArrayWithOffset)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MarshalAsAnyRestriction"));
			}
			IntPtr intPtr;
			string text;
			StringBuilder stringBuilder;
			if (pManagedHome.GetType().IsArray)
			{
				intPtr = this.ConvertArrayToNative(pManagedHome, dwFlags);
			}
			else if ((text = pManagedHome as string) != null)
			{
				intPtr = AsAnyMarshaler.ConvertStringToNative(text, dwFlags);
			}
			else if ((stringBuilder = pManagedHome as StringBuilder) != null)
			{
				intPtr = this.ConvertStringBuilderToNative(stringBuilder, dwFlags);
			}
			else
			{
				if (!pManagedHome.GetType().IsLayoutSequential && !pManagedHome.GetType().IsExplicitLayout)
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_NDirectBadObject"));
				}
				intPtr = this.ConvertLayoutToNative(pManagedHome, dwFlags);
			}
			return intPtr;
		}

		// Token: 0x06004322 RID: 17186 RVA: 0x000F9DA0 File Offset: 0x000F7FA0
		[SecurityCritical]
		internal unsafe void ConvertToManaged(object pManagedHome, IntPtr pNativeHome)
		{
			switch (this.backPropAction)
			{
			case AsAnyMarshaler.BackPropAction.Array:
				MngdNativeArrayMarshaler.ConvertContentsToManaged(this.pvArrayMarshaler, ref pManagedHome, new IntPtr((void*)(&pNativeHome)));
				return;
			case AsAnyMarshaler.BackPropAction.Layout:
				StubHelpers.FmtClassUpdateCLRInternal(pManagedHome, (byte*)pNativeHome.ToPointer());
				return;
			case AsAnyMarshaler.BackPropAction.StringBuilderAnsi:
			{
				sbyte* ptr = (sbyte*)pNativeHome.ToPointer();
				((StringBuilder)pManagedHome).ReplaceBufferAnsiInternal(ptr, Win32Native.lstrlenA(pNativeHome));
				return;
			}
			case AsAnyMarshaler.BackPropAction.StringBuilderUnicode:
			{
				char* ptr2 = (char*)pNativeHome.ToPointer();
				((StringBuilder)pManagedHome).ReplaceBufferInternal(ptr2, Win32Native.lstrlenW(pNativeHome));
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x06004323 RID: 17187 RVA: 0x000F9E26 File Offset: 0x000F8026
		[SecurityCritical]
		internal void ClearNative(IntPtr pNativeHome)
		{
			if (pNativeHome != IntPtr.Zero)
			{
				if (this.layoutType != null)
				{
					Marshal.DestroyStructure(pNativeHome, this.layoutType);
				}
				Win32Native.CoTaskMemFree(pNativeHome);
			}
			StubHelpers.DestroyCleanupList(ref this.cleanupWorkList);
		}

		// Token: 0x04001BDA RID: 7130
		private const ushort VTHACK_ANSICHAR = 253;

		// Token: 0x04001BDB RID: 7131
		private const ushort VTHACK_WINBOOL = 254;

		// Token: 0x04001BDC RID: 7132
		private IntPtr pvArrayMarshaler;

		// Token: 0x04001BDD RID: 7133
		private AsAnyMarshaler.BackPropAction backPropAction;

		// Token: 0x04001BDE RID: 7134
		private Type layoutType;

		// Token: 0x04001BDF RID: 7135
		private CleanupWorkList cleanupWorkList;

		// Token: 0x02000C38 RID: 3128
		private enum BackPropAction
		{
			// Token: 0x0400372F RID: 14127
			None,
			// Token: 0x04003730 RID: 14128
			Array,
			// Token: 0x04003731 RID: 14129
			Layout,
			// Token: 0x04003732 RID: 14130
			StringBuilderAnsi,
			// Token: 0x04003733 RID: 14131
			StringBuilderUnicode
		}
	}
}
