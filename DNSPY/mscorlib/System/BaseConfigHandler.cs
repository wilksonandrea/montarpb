using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System
{
	// Token: 0x020000BB RID: 187
	internal abstract class BaseConfigHandler
	{
		// Token: 0x06000AE0 RID: 2784 RVA: 0x0002272F File Offset: 0x0002092F
		public BaseConfigHandler()
		{
			this.InitializeCallbacks();
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x00022740 File Offset: 0x00020940
		private void InitializeCallbacks()
		{
			if (this.eventCallbacks == null)
			{
				this.eventCallbacks = new Delegate[6];
				this.eventCallbacks[0] = new BaseConfigHandler.NotifyEventCallback(this.NotifyEvent);
				this.eventCallbacks[1] = new BaseConfigHandler.BeginChildrenCallback(this.BeginChildren);
				this.eventCallbacks[2] = new BaseConfigHandler.EndChildrenCallback(this.EndChildren);
				this.eventCallbacks[3] = new BaseConfigHandler.ErrorCallback(this.Error);
				this.eventCallbacks[4] = new BaseConfigHandler.CreateNodeCallback(this.CreateNode);
				this.eventCallbacks[5] = new BaseConfigHandler.CreateAttributeCallback(this.CreateAttribute);
			}
		}

		// Token: 0x06000AE2 RID: 2786
		public abstract void NotifyEvent(ConfigEvents nEvent);

		// Token: 0x06000AE3 RID: 2787
		public abstract void BeginChildren(int size, ConfigNodeSubType subType, ConfigNodeType nType, int terminal, [MarshalAs(UnmanagedType.LPWStr)] string text, int textLength, int prefixLength);

		// Token: 0x06000AE4 RID: 2788
		public abstract void EndChildren(int fEmpty, int size, ConfigNodeSubType subType, ConfigNodeType nType, int terminal, [MarshalAs(UnmanagedType.LPWStr)] string text, int textLength, int prefixLength);

		// Token: 0x06000AE5 RID: 2789
		public abstract void Error(int size, ConfigNodeSubType subType, ConfigNodeType nType, int terminal, [MarshalAs(UnmanagedType.LPWStr)] string text, int textLength, int prefixLength);

		// Token: 0x06000AE6 RID: 2790
		public abstract void CreateNode(int size, ConfigNodeSubType subType, ConfigNodeType nType, int terminal, [MarshalAs(UnmanagedType.LPWStr)] string text, int textLength, int prefixLength);

		// Token: 0x06000AE7 RID: 2791
		public abstract void CreateAttribute(int size, ConfigNodeSubType subType, ConfigNodeType nType, int terminal, [MarshalAs(UnmanagedType.LPWStr)] string text, int textLength, int prefixLength);

		// Token: 0x06000AE8 RID: 2792
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void RunParser(string fileName);

		// Token: 0x0400044C RID: 1100
		protected Delegate[] eventCallbacks;

		// Token: 0x02000AD8 RID: 2776
		// (Invoke) Token: 0x060069DE RID: 27102
		private delegate void NotifyEventCallback(ConfigEvents nEvent);

		// Token: 0x02000AD9 RID: 2777
		// (Invoke) Token: 0x060069E2 RID: 27106
		private delegate void BeginChildrenCallback(int size, ConfigNodeSubType subType, ConfigNodeType nType, int terminal, [MarshalAs(UnmanagedType.LPWStr)] string text, int textLength, int prefixLength);

		// Token: 0x02000ADA RID: 2778
		// (Invoke) Token: 0x060069E6 RID: 27110
		private delegate void EndChildrenCallback(int fEmpty, int size, ConfigNodeSubType subType, ConfigNodeType nType, int terminal, [MarshalAs(UnmanagedType.LPWStr)] string text, int textLength, int prefixLength);

		// Token: 0x02000ADB RID: 2779
		// (Invoke) Token: 0x060069EA RID: 27114
		private delegate void ErrorCallback(int size, ConfigNodeSubType subType, ConfigNodeType nType, int terminal, [MarshalAs(UnmanagedType.LPWStr)] string text, int textLength, int prefixLength);

		// Token: 0x02000ADC RID: 2780
		// (Invoke) Token: 0x060069EE RID: 27118
		private delegate void CreateNodeCallback(int size, ConfigNodeSubType subType, ConfigNodeType nType, int terminal, [MarshalAs(UnmanagedType.LPWStr)] string text, int textLength, int prefixLength);

		// Token: 0x02000ADD RID: 2781
		// (Invoke) Token: 0x060069F2 RID: 27122
		private delegate void CreateAttributeCallback(int size, ConfigNodeSubType subType, ConfigNodeType nType, int terminal, [MarshalAs(UnmanagedType.LPWStr)] string text, int textLength, int prefixLength);
	}
}
