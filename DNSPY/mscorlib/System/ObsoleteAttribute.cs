using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x0200011A RID: 282
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class ObsoleteAttribute : Attribute
	{
		// Token: 0x060010BC RID: 4284 RVA: 0x0003285D File Offset: 0x00030A5D
		[__DynamicallyInvokable]
		public ObsoleteAttribute()
		{
			this._message = null;
			this._error = false;
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x00032873 File Offset: 0x00030A73
		[__DynamicallyInvokable]
		public ObsoleteAttribute(string message)
		{
			this._message = message;
			this._error = false;
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x00032889 File Offset: 0x00030A89
		[__DynamicallyInvokable]
		public ObsoleteAttribute(string message, bool error)
		{
			this._message = message;
			this._error = error;
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060010BF RID: 4287 RVA: 0x0003289F File Offset: 0x00030A9F
		[__DynamicallyInvokable]
		public string Message
		{
			[__DynamicallyInvokable]
			get
			{
				return this._message;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060010C0 RID: 4288 RVA: 0x000328A7 File Offset: 0x00030AA7
		[__DynamicallyInvokable]
		public bool IsError
		{
			[__DynamicallyInvokable]
			get
			{
				return this._error;
			}
		}

		// Token: 0x040005CD RID: 1485
		private string _message;

		// Token: 0x040005CE RID: 1486
		private bool _error;
	}
}
