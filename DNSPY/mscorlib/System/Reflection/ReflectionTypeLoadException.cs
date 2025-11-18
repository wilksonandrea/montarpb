using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Reflection
{
	// Token: 0x02000620 RID: 1568
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class ReflectionTypeLoadException : SystemException, ISerializable
	{
		// Token: 0x060048A8 RID: 18600 RVA: 0x00107313 File Offset: 0x00105513
		private ReflectionTypeLoadException()
			: base(Environment.GetResourceString("ReflectionTypeLoad_LoadFailed"))
		{
			base.SetErrorCode(-2146232830);
		}

		// Token: 0x060048A9 RID: 18601 RVA: 0x00107330 File Offset: 0x00105530
		private ReflectionTypeLoadException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146232830);
		}

		// Token: 0x060048AA RID: 18602 RVA: 0x00107344 File Offset: 0x00105544
		[__DynamicallyInvokable]
		public ReflectionTypeLoadException(Type[] classes, Exception[] exceptions)
			: base(null)
		{
			this._classes = classes;
			this._exceptions = exceptions;
			base.SetErrorCode(-2146232830);
		}

		// Token: 0x060048AB RID: 18603 RVA: 0x00107366 File Offset: 0x00105566
		[__DynamicallyInvokable]
		public ReflectionTypeLoadException(Type[] classes, Exception[] exceptions, string message)
			: base(message)
		{
			this._classes = classes;
			this._exceptions = exceptions;
			base.SetErrorCode(-2146232830);
		}

		// Token: 0x060048AC RID: 18604 RVA: 0x00107388 File Offset: 0x00105588
		internal ReflectionTypeLoadException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._classes = (Type[])info.GetValue("Types", typeof(Type[]));
			this._exceptions = (Exception[])info.GetValue("Exceptions", typeof(Exception[]));
		}

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x060048AD RID: 18605 RVA: 0x001073DD File Offset: 0x001055DD
		[__DynamicallyInvokable]
		public Type[] Types
		{
			[__DynamicallyInvokable]
			get
			{
				return this._classes;
			}
		}

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x060048AE RID: 18606 RVA: 0x001073E5 File Offset: 0x001055E5
		[__DynamicallyInvokable]
		public Exception[] LoaderExceptions
		{
			[__DynamicallyInvokable]
			get
			{
				return this._exceptions;
			}
		}

		// Token: 0x060048AF RID: 18607 RVA: 0x001073F0 File Offset: 0x001055F0
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("Types", this._classes, typeof(Type[]));
			info.AddValue("Exceptions", this._exceptions, typeof(Exception[]));
		}

		// Token: 0x04001E1F RID: 7711
		private Type[] _classes;

		// Token: 0x04001E20 RID: 7712
		private Exception[] _exceptions;
	}
}
