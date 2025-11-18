using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000943 RID: 2371
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	[ComVisible(false)]
	public sealed class ManagedToNativeComInteropStubAttribute : Attribute
	{
		// Token: 0x06006068 RID: 24680 RVA: 0x0014BDEB File Offset: 0x00149FEB
		public ManagedToNativeComInteropStubAttribute(Type classType, string methodName)
		{
			this._classType = classType;
			this._methodName = methodName;
		}

		// Token: 0x170010F4 RID: 4340
		// (get) Token: 0x06006069 RID: 24681 RVA: 0x0014BE01 File Offset: 0x0014A001
		public Type ClassType
		{
			get
			{
				return this._classType;
			}
		}

		// Token: 0x170010F5 RID: 4341
		// (get) Token: 0x0600606A RID: 24682 RVA: 0x0014BE09 File Offset: 0x0014A009
		public string MethodName
		{
			get
			{
				return this._methodName;
			}
		}

		// Token: 0x04002B39 RID: 11065
		internal Type _classType;

		// Token: 0x04002B3A RID: 11066
		internal string _methodName;
	}
}
