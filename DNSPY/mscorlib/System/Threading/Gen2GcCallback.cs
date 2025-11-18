using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
	// Token: 0x0200050E RID: 1294
	internal sealed class Gen2GcCallback : CriticalFinalizerObject
	{
		// Token: 0x06003CD9 RID: 15577 RVA: 0x000E593C File Offset: 0x000E3B3C
		[SecuritySafeCritical]
		public Gen2GcCallback()
		{
		}

		// Token: 0x06003CDA RID: 15578 RVA: 0x000E5944 File Offset: 0x000E3B44
		public static void Register(Func<object, bool> callback, object targetObj)
		{
			Gen2GcCallback gen2GcCallback = new Gen2GcCallback();
			gen2GcCallback.Setup(callback, targetObj);
		}

		// Token: 0x06003CDB RID: 15579 RVA: 0x000E595F File Offset: 0x000E3B5F
		[SecuritySafeCritical]
		private void Setup(Func<object, bool> callback, object targetObj)
		{
			this.m_callback = callback;
			this.m_weakTargetObj = GCHandle.Alloc(targetObj, GCHandleType.Weak);
		}

		// Token: 0x06003CDC RID: 15580 RVA: 0x000E5978 File Offset: 0x000E3B78
		[SecuritySafeCritical]
		protected override void Finalize()
		{
			try
			{
				if (this.m_weakTargetObj.IsAllocated)
				{
					object target = this.m_weakTargetObj.Target;
					if (target == null)
					{
						this.m_weakTargetObj.Free();
					}
					else
					{
						try
						{
							if (!this.m_callback(target))
							{
								return;
							}
						}
						catch
						{
						}
						if (!Environment.HasShutdownStarted && !AppDomain.CurrentDomain.IsFinalizingForUnload())
						{
							GC.ReRegisterForFinalize(this);
						}
					}
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x040019D8 RID: 6616
		private Func<object, bool> m_callback;

		// Token: 0x040019D9 RID: 6617
		private GCHandle m_weakTargetObj;
	}
}
