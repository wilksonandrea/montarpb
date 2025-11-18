using System;
using System.Reflection;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000936 RID: 2358
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class DllImportAttribute : Attribute
	{
		// Token: 0x06006040 RID: 24640 RVA: 0x0014B97C File Offset: 0x00149B7C
		[SecurityCritical]
		internal static Attribute GetCustomAttribute(RuntimeMethodInfo method)
		{
			if ((method.Attributes & MethodAttributes.PinvokeImpl) == MethodAttributes.PrivateScope)
			{
				return null;
			}
			MetadataImport metadataImport = ModuleHandle.GetMetadataImport(method.Module.ModuleHandle.GetRuntimeModule());
			string text = null;
			int metadataToken = method.MetadataToken;
			PInvokeAttributes pinvokeAttributes = PInvokeAttributes.CharSetNotSpec;
			string text2;
			metadataImport.GetPInvokeMap(metadataToken, out pinvokeAttributes, out text2, out text);
			CharSet charSet = CharSet.None;
			switch (pinvokeAttributes & PInvokeAttributes.CharSetMask)
			{
			case PInvokeAttributes.CharSetNotSpec:
				charSet = CharSet.None;
				break;
			case PInvokeAttributes.CharSetAnsi:
				charSet = CharSet.Ansi;
				break;
			case PInvokeAttributes.CharSetUnicode:
				charSet = CharSet.Unicode;
				break;
			case PInvokeAttributes.CharSetMask:
				charSet = CharSet.Auto;
				break;
			}
			CallingConvention callingConvention = CallingConvention.Cdecl;
			PInvokeAttributes pinvokeAttributes2 = pinvokeAttributes & PInvokeAttributes.CallConvMask;
			if (pinvokeAttributes2 <= PInvokeAttributes.CallConvCdecl)
			{
				if (pinvokeAttributes2 != PInvokeAttributes.CallConvWinapi)
				{
					if (pinvokeAttributes2 == PInvokeAttributes.CallConvCdecl)
					{
						callingConvention = CallingConvention.Cdecl;
					}
				}
				else
				{
					callingConvention = CallingConvention.Winapi;
				}
			}
			else if (pinvokeAttributes2 != PInvokeAttributes.CallConvStdcall)
			{
				if (pinvokeAttributes2 != PInvokeAttributes.CallConvThiscall)
				{
					if (pinvokeAttributes2 == PInvokeAttributes.CallConvFastcall)
					{
						callingConvention = CallingConvention.FastCall;
					}
				}
				else
				{
					callingConvention = CallingConvention.ThisCall;
				}
			}
			else
			{
				callingConvention = CallingConvention.StdCall;
			}
			bool flag = (pinvokeAttributes & PInvokeAttributes.NoMangle) > PInvokeAttributes.CharSetNotSpec;
			bool flag2 = (pinvokeAttributes & PInvokeAttributes.SupportsLastError) > PInvokeAttributes.CharSetNotSpec;
			bool flag3 = (pinvokeAttributes & PInvokeAttributes.BestFitMask) == PInvokeAttributes.BestFitEnabled;
			bool flag4 = (pinvokeAttributes & PInvokeAttributes.ThrowOnUnmappableCharMask) == PInvokeAttributes.ThrowOnUnmappableCharEnabled;
			bool flag5 = (method.GetMethodImplementationFlags() & MethodImplAttributes.PreserveSig) > MethodImplAttributes.IL;
			return new DllImportAttribute(text, text2, charSet, flag, flag2, flag5, callingConvention, flag3, flag4);
		}

		// Token: 0x06006041 RID: 24641 RVA: 0x0014BAC0 File Offset: 0x00149CC0
		internal static bool IsDefined(RuntimeMethodInfo method)
		{
			return (method.Attributes & MethodAttributes.PinvokeImpl) > MethodAttributes.PrivateScope;
		}

		// Token: 0x06006042 RID: 24642 RVA: 0x0014BAD4 File Offset: 0x00149CD4
		internal DllImportAttribute(string dllName, string entryPoint, CharSet charSet, bool exactSpelling, bool setLastError, bool preserveSig, CallingConvention callingConvention, bool bestFitMapping, bool throwOnUnmappableChar)
		{
			this._val = dllName;
			this.EntryPoint = entryPoint;
			this.CharSet = charSet;
			this.ExactSpelling = exactSpelling;
			this.SetLastError = setLastError;
			this.PreserveSig = preserveSig;
			this.CallingConvention = callingConvention;
			this.BestFitMapping = bestFitMapping;
			this.ThrowOnUnmappableChar = throwOnUnmappableChar;
		}

		// Token: 0x06006043 RID: 24643 RVA: 0x0014BB2C File Offset: 0x00149D2C
		[__DynamicallyInvokable]
		public DllImportAttribute(string dllName)
		{
			this._val = dllName;
		}

		// Token: 0x170010E2 RID: 4322
		// (get) Token: 0x06006044 RID: 24644 RVA: 0x0014BB3B File Offset: 0x00149D3B
		[__DynamicallyInvokable]
		public string Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002B1A RID: 11034
		internal string _val;

		// Token: 0x04002B1B RID: 11035
		[__DynamicallyInvokable]
		public string EntryPoint;

		// Token: 0x04002B1C RID: 11036
		[__DynamicallyInvokable]
		public CharSet CharSet;

		// Token: 0x04002B1D RID: 11037
		[__DynamicallyInvokable]
		public bool SetLastError;

		// Token: 0x04002B1E RID: 11038
		[__DynamicallyInvokable]
		public bool ExactSpelling;

		// Token: 0x04002B1F RID: 11039
		[__DynamicallyInvokable]
		public bool PreserveSig;

		// Token: 0x04002B20 RID: 11040
		[__DynamicallyInvokable]
		public CallingConvention CallingConvention;

		// Token: 0x04002B21 RID: 11041
		[__DynamicallyInvokable]
		public bool BestFitMapping;

		// Token: 0x04002B22 RID: 11042
		[__DynamicallyInvokable]
		public bool ThrowOnUnmappableChar;
	}
}
