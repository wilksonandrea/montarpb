using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Reflection
{
	// Token: 0x0200060C RID: 1548
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class Missing : ISerializable
	{
		// Token: 0x0600478E RID: 18318 RVA: 0x00104873 File Offset: 0x00102A73
		private Missing()
		{
		}

		// Token: 0x0600478F RID: 18319 RVA: 0x0010487B File Offset: 0x00102A7B
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			UnitySerializationHolder.GetUnitySerializationInfo(info, this);
		}

		// Token: 0x06004790 RID: 18320 RVA: 0x00104892 File Offset: 0x00102A92
		// Note: this type is marked as 'beforefieldinit'.
		static Missing()
		{
		}

		// Token: 0x04001DB5 RID: 7605
		[__DynamicallyInvokable]
		public static readonly Missing Value = new Missing();
	}
}
